//
// RsiOpcConnect.cpp
// Example demonstrating the use of an OPC client to perform asynchronous
// functions to a device using "Connection Points".
//
// The asynchronous functionality in this sample program is based on the
// "OPC Data Access Custom Interface Standard", v2.0.
//
// Refer to Microsoft documentation for information on "Connection Points".
//
//		Revision history
//			Program created (EPD 02/11/00)
//
//

// Standard OPC header files
#include "opccomn.h"
#include "opccomn_i.c"
#include "opcda.h"
#include "opcda_i.c"

// Comdef.h required for Connection Point definitions
#include <comdef.h>	

#include "Sink.h"
#include "utils.h"

#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <ctype.h>


//Function prototypes
OPCITEMRESULT *CreateItems(IOPCItemMgt* pOPCItemMgt, OPCITEMDEF *ItemArray, DWORD *dwCount);
IUnknown *CreateLocalServer(WCHAR *pwszServerName);
IUnknown *CreateRemoteServer(WCHAR *pwszServerName, WCHAR *pwszComputerName);
void GetWriteData(DWORD dwCount, VARIANT *pItemValues);

IOPCServer		*pOPCServer = NULL;


main()
{

	HRESULT			r1;
	IUnknown		*pUnkSvr = NULL;
	char			c;
	IOPCItemMgt		*pOPCItemMgt=NULL;
	OPCITEMDEF		*pItemArray=NULL;
	OPCITEMRESULT	*pAddResults=NULL;
	HRESULT			*pErrors=NULL;
	DWORD			n, dwCount;

	wchar_t			szwServerName[80]={L'\0'},
//					szwAccessPath[80]={L'\0'},
					szwItemName[80]={L'\0'},
					szwComputerName[80]={L'\0'};

	SetConsoleTitle("OPC Async Client Test (Connection Points)");

	// User enters OPC server name
	// For RSLinx, the server names are
	//   1.'rslinx opc server' (local)
	//   2.'rslinx remote opc server' (remote)
	printf("Enter the OPC server name\n>");
	_getws(szwServerName);
	

	// Initialize COM. To use this function, define _WIN32_DCOM in 'Settings'
	r1 = CoInitializeEx(NULL, COINIT_APARTMENTTHREADED);
	if (FAILED(r1)){
		DisplaySystemError("Error at CoInitializeEx", r1);
		exit (1);
	}

	// User enters the type of OPC server
	printf("Enter the server type to create: L-local, R-Remote\n>");

	c = getch();
	char x = toupper(c);

	switch(x){

	case 'L':
		// Create a local RSLinx server object and obtain the IUnknown pointer
		pUnkSvr = CreateLocalServer(szwServerName);
		if (pUnkSvr == NULL){
			printf ("Error creating IUnknown pointer\n");
			CoUninitialize();
			exit(1);
		}
		break;


	case 'R':
		//User enters the computer node name
		printf("Enter the computer name:\n");
		_getws(szwComputerName);
		// Create a remote RSLinx server object and obtain the IUnknown pointer
		pUnkSvr = CreateRemoteServer(szwServerName, szwComputerName);
		if (pUnkSvr == NULL){
			printf ("Error creating IUnknown pointer\n");
			CoUninitialize();
			exit(1);
		}
		break;

	default:
		printf ("Unknown server type\nProgram ends\n");
		CoUninitialize();
		exit(1);
	}

	// Obtain a pointer to the OPC Server interface
	r1 = pUnkSvr->QueryInterface(IID_IOPCServer, (void**)&pOPCServer);
	if (FAILED(r1)){
		DisplaySystemError("Error attempting to get OPC Server pointer", r1);
		CoUninitialize();
		exit (1);
	}


	// Parameters required to add a group
	DWORD dwRequestedUpdateRate;	// Update rate requested by user
	DWORD dwRevisedUpdateRate;		// Update rate provided by server
	OPCHANDLE hClientGroup;			// Group handle assigned by user
	OPCHANDLE hServerGroup;			// Group handle returned by server
	LONG	lTimeBias = 240L;		
	BOOL	GroupActiveState = TRUE; // active state of the group
	wchar_t	szwGroupName[80];

	wcscpy(szwGroupName, L"_Group1"); // assigned the group a name

	hClientGroup = 0x100;
	dwRequestedUpdateRate = 1000;


	printf("\n...Attempting to add group\n");

	// Add a group to the server. A pointer to a group interface is returned.
	// In this case, it is the Item Management interface.
	r1 = pOPCServer->AddGroup(szwGroupName, GroupActiveState, dwRequestedUpdateRate, 
						   hClientGroup, &lTimeBias, NULL, 0,
						   &hServerGroup, &dwRevisedUpdateRate,
						   IID_IOPCItemMgt, (LPUNKNOWN*)&pOPCItemMgt);
	if (FAILED(r1)){
		DisplayError("Error attempting to add group", r1);
		pOPCServer->Release();
		exit (1);
	}
	else printf("Group %ls added successfully\n", szwGroupName);

	// Item count is set to 3. User will create three items.
	dwCount = 3;
	pItemArray = (OPCITEMDEF*)CoTaskMemAlloc(dwCount*sizeof(OPCITEMDEF));

	// Call function that allows user to add items to the group
	pAddResults = CreateItems(pOPCItemMgt, pItemArray, &dwCount);
	if (pAddResults == NULL){
		printf ("Could not add items to the group %ls\n", szwGroupName);
		pOPCItemMgt->Release();
		pOPCServer->Release();
		pUnkSvr->Release();
		exit(1);
	}

	IConnectionPointContainer *pCPC=NULL;

	// Get pointer the Connection Point Container
	r1 = pOPCItemMgt->QueryInterface(IID_IConnectionPointContainer, (void**)&pCPC);
	if (FAILED(r1)){
		DisplayError("Error attempting get Connection Point Container", r1);
		pOPCServer->Release();
		CoUninitialize();
		exit (1);
	}

	// Get pointer to the Data Callback connection point
	IConnectionPoint	*pConnPt=NULL;
	r1 = pCPC->FindConnectionPoint(IID_IOPCDataCallback, (IConnectionPoint**)&pConnPt);
	if (FAILED(r1)){
		DisplaySystemError("Error attempting to get Connection Point", r1);
		pOPCServer->Release();
		CoUninitialize();
		exit (1);
	}

	// Don't need this interface anymore. Get rid of it.
	pCPC->Release();

	// Create a sink object.
	COPCDataCallback	*pOPCDataCallback=NULL;
	pOPCDataCallback = new COPCDataCallback;
	
	// dwCookie is used as an identifier for the callbacks.
	DWORD dwCookie;
	// Start the advise. Pass the address of the sink to the OPC server.
	// The server will callback into the functions referenced by this pointer
	// after I/O is completed. A unique identifier is assigned to dwCookie.
	r1 = pConnPt->Advise(pOPCDataCallback, &dwCookie);
	if (FAILED(r1)){
		DisplayError("Error attempting to start Advise", r1);
		pConnPt->Release();
		pOPCServer->Release();
		CoUninitialize();
		exit (1);
	}

	IOPCAsyncIO2	 *pOPCAsyncIO2=NULL;
	
	// Get a pointer to the IOPCAsyncIO2 interface
	r1 = pOPCItemMgt->QueryInterface(IID_IOPCAsyncIO2, (void**)&pOPCAsyncIO2);
	if (FAILED(r1)){
		DisplaySystemError("Error attempting to get IOPCAsyncIO2 pointer", r1);
		CoUninitialize();
		exit (1);
	}

	// Set the group subscription mode to inactive.
	r1 = pOPCAsyncIO2->SetEnable(FALSE);
	if (FAILED(r1)){
		DisplayError("Error attempting to set subscription mode", r1);
	}


	HRESULT			*WriteErrors = NULL;
	HRESULT			*ReadErrors = NULL;
	OPCHANDLE *pItemHandles = (OPCHANDLE*)CoTaskMemAlloc(dwCount*sizeof(OPCHANDLE));
	DWORD			dwTransactionId = 0;
	VARIANT *pItemValues = (VARIANT*)CoTaskMemAlloc(dwCount*sizeof(VARIANT));

	for (n=0 ; n < dwCount; n++){
		//Initialize the variant to be used for writing data to target.
		VariantInit(&pItemValues[n]);
	
		//Set the variant data type.
		pItemValues[n].vt = pAddResults[n].vtCanonicalDataType;
	
		// get handle assigned to item by server and insert it into item handle array
		pItemHandles[n] = pAddResults[n].hServer;
	}


	// Get a pointer to the Group State Management interface. 
	IOPCGroupStateMgt	*pOPCGroupStateMgt;
	r1 = pOPCItemMgt->QueryInterface(IID_IOPCGroupStateMgt, (void**)&pOPCGroupStateMgt);
	if (FAILED(r1)){
		DisplaySystemError("Error attempting to get IOPCGroupStateMgt pointer", r1);
		CoUninitialize();
		exit (1);
	}

	BOOL Done = FALSE;
	HRESULT *StateErrors=NULL;


	// This section of code runs in a loop, allowing the user to enter the following
	// commands:
	//   1. R to perform a single asynchronous read
	//   2. W to perform an asynchronous write
	//   3. T to toggle the subscription mode between active and 
	//      inactive (default)
	//   4. X to exit
	//
	// When the group is active, subscription mode is on. Subscription mode allows the
	// data items to be automatically updated on a change in value.
	// When the group is inactive, subscription mode is off. The data item can be updated
	// via an asynchronous read.

	printf ("\nCommands are performed using the following keystrokes:\n");
	printf ("1. F to refresh data\n");
	printf ("2. R to perform a single asynchronous read\n");
	printf ("3. W to perform an asynchronous write\n");
	printf ("4. T to toggle the subscription mode between active and inactive\n");
	printf ("5. U to change group update rate\n");
	printf ("6. X to exit\n");

	BOOL OnDataChangeActive = FALSE;
	DWORD	dwCancelId;
	
	while(!Done){
		if (kbhit()){

			// Check for character from keyboard.
			c = getch();
			
			switch(c){
			
			// Perform data refresh
			case 'F':
			case 'f':
				DWORD dwCancelID;
				r1 = pOPCAsyncIO2->Refresh2(OPC_DS_CACHE, dwTransactionId++, &dwCancelID);
				if (FAILED(r1)){
					DisplayError("Failed attempting data refresh:", r1);
				}
				break;
			
			// Perform asynchronous write
			case 'W':
			case 'w':
//				char szWriteValue[8];
//				printf("\nEnter the integer value to be written:\n");
//				gets(szWriteValue);
//				pItemValues[0].iVal = atoi(szWriteValue);
				GetWriteData(dwCount, pItemValues);
				
			r1 = pOPCAsyncIO2->Write(dwCount, pItemHandles,
									 pItemValues, dwTransactionId++, &dwCancelId, 
									 &WriteErrors);
			if (FAILED(r1)){
					DWORD dwLastError;
					dwLastError = GetLastError();
					DisplayError("Main:Failed attempting asynchronous write", r1);
					//CoUninitialize();
					//exit (EXIT_FAILURE);
				}
				else if (r1 == S_FALSE){
					if (FAILED(WriteErrors[0])){
						DisplayError("Main:Error writing data", WriteErrors[0]);
						//CoUninitialize();
						//exit(EXIT_FAILURE);
					}
				}
				CoTaskMemFree(WriteErrors);
				break;

			// Perform asynchronous read
			case 'R':
			case 'r':
				r1 = pOPCAsyncIO2->Read(dwCount, pItemHandles, dwTransactionId++, &dwCancelId, &ReadErrors);
				if (FAILED(r1)){
					DWORD dwLastError;
					dwLastError = GetLastError();
					DisplayError("Asynchronous read failed", r1);
				}
				else if (r1 == S_FALSE){
					if (FAILED(ReadErrors[0])){
						DisplayError("Error reading data", ReadErrors[0]);
					}
				}
				CoTaskMemFree(ReadErrors);
				break;
			
			// Toggle the subscription mode between active and inactive
			case 'T':
			case 't':
				if (OnDataChangeActive == TRUE){
					OnDataChangeActive = FALSE;
					printf("\nSubscription is inactive\n");
				}
				else if (OnDataChangeActive == FALSE){
					OnDataChangeActive = TRUE;
					printf("\nSubscription is active\n");
				}
				
				r1 = pOPCAsyncIO2->SetEnable(OnDataChangeActive);
				if (FAILED(r1)){
					DisplayError("Error attempting to set subscription mode", r1);
				}
				break;
			// Change the group update rate
			case 'U':
			case 'u':
				char *errptr, szUserInput[20];
				DWORD dwActualRate, dwUpdateRate;
				errptr = NULL;
				printf("\nEnter a new group update rate in milliseconds\n");
				gets(szUserInput);
				dwUpdateRate = strtoul(szUserInput, &errptr, 10);
				if (*errptr != NULL)
					printf ("Error entering update rate\n");
				else {
					r1 = pOPCGroupStateMgt->SetState(&dwUpdateRate, &dwActualRate, NULL,
													 NULL, NULL, NULL, NULL);
					if (FAILED(r1))
						DisplayError("Error at SetState", r1);
					else {
						printf("the new update rate entered by user is %ld\n", dwUpdateRate);
						printf("the actual rate used by the server is %ld\n", dwActualRate);
					}
				}

				break;

			// Exit the application
			case 'X':
			case 'x':
				printf("\nProgram terminated by user\n");
				Done = TRUE;
				break;
			} //end of switch
		}
		else{
			MSG msg;
			// Message pump. Required to run with COM Single Threaded Apartment
			while(PeekMessage(&msg, NULL, NULL, NULL, PM_REMOVE)){
				TranslateMessage(&msg);
				DispatchMessage(&msg);
			}
		}
		// Go to sleep. Give someone else a chance to run.
		Sleep(1);
	}//end while

	// Using the dwCookie identifier, the Advise callback is turned off.
	if (pConnPt != NULL){
		r1 = pConnPt->Unadvise(dwCookie);
		if (FAILED(r1)){
			DisplayError("Error attempting to release the Advise pointer", r1);
		}
	}

	// remove items from the group
	if ((pOPCItemMgt != NULL) && (pItemHandles != NULL)){
		r1 = pOPCItemMgt->RemoveItems(dwCount, pItemHandles, &pErrors);
		if (FAILED(r1)){
			DisplayError("Failed attempting to remove item(s)", r1);
		}
		else if (r1 == S_FALSE){
			if (FAILED(pErrors[0])){
				DisplayError("Error attempting to remove item", pErrors[0]);
			}
		}
	}

	CoTaskMemFree(pErrors);

	// remove group from the server
	r1 = pOPCServer->RemoveGroup(hServerGroup, TRUE);
	if (FAILED(r1)){
		DisplayError("Failed attempting to remove group", r1);
	}


	// client is responsible for releasing any resources allocated by the server

	// release interfaces
	pConnPt->Release();
	pOPCAsyncIO2->Release();
	pOPCServer->Release();
	pOPCItemMgt->Release();
	pUnkSvr->Release();
	
	// release any memory allocated by server
	CoTaskMemFree(pAddResults);
	
	// uninitialize COM
	CoUninitialize();

	return 0;
}


OPCITEMRESULT *CreateItems(IOPCItemMgt* pOPCItemMgt, OPCITEMDEF *ItemArray, DWORD *dwCount)
{
//
// This function allows the user to create items, validate them and add them 
// to a group.
//
	int n;
	HRESULT			*AddErrors;
	BYTE			*MyBlob=NULL;
	WCHAR			szwItemName[80];
	HRESULT			r1;
	OPCITEMRESULT	*pResults;
	char			szErrorString[80];
	OPCITEMRESULT	*ValidationResults;
	HRESULT			*pErrors=0;

	
	// Create new items.
	printf("\n...Create new items\n\n");

	for (n=0; n < (int)*dwCount; n++){
		// User enters item names
		BOOL CurrentItemValid=FALSE;
		ItemArray[n].szItemID = (LPWSTR)CoTaskMemAlloc(sizeof(szwItemName));
		while (!CurrentItemValid){
		printf("\nEnter the item number %d in the form\n", n);
		printf("[OPC topic name]item, for example: [MyTopic]theTag\n>");
		_getws(szwItemName);
		// Create the item
		ItemArray[n].szAccessPath = L"\0"; //szwAccessPath
//		ItemArray[n].szItemID = (LPWSTR)CoTaskMemAlloc(sizeof(szwItemName));
		wcscpy(ItemArray[n].szItemID, szwItemName);
		ItemArray[n].bActive = TRUE;
		ItemArray[n].hClient = 0x200 + n;
		ItemArray[n].dwBlobSize = 0;
		ItemArray[n].pBlob = NULL;
		ItemArray[n].vtRequestedDataType = VT_EMPTY;
//	}	

		// Validate the item(s) in the group
		r1 = pOPCItemMgt->ValidateItems(1, &ItemArray[n],0, 
							   &ValidationResults, &pErrors);

		// Check for errors
		if (FAILED(r1)){
			DisplayError("Failed attempting to validate item(s)", r1);
		}
		else if (r1 == S_FALSE){
//			for (n=0; n < (int)*dwCount; n++){
				if (FAILED(*pErrors)){
					sprintf(szErrorString, "Error attempting to validate item %d",n);
					DisplayError(szErrorString, *pErrors);
				}
//			}
		}
		else{
			printf ("Item is valid\n");
			CurrentItemValid = TRUE;
		}
		CoTaskMemFree(pErrors);
		CoTaskMemFree(ValidationResults);
		}
	}
	
	
	// If the items are valid, add them to the group
	r1 = pOPCItemMgt->AddItems(*dwCount, ItemArray, 
							   &pResults, &AddErrors);

	// Check for errors
	if (FAILED(r1)){
		DisplayError("Failed attempting to add items", r1);
		CoTaskMemFree(AddErrors);
		CoTaskMemFree(pResults);
		pResults = NULL;
	}
	else if (r1 == S_FALSE){
		for (n=0; n < (int)*dwCount; n++){
			if (FAILED(AddErrors[n])){
				sprintf(szErrorString, "Error attempting to add item %d",n);
				DisplayError(szErrorString, AddErrors[n]);
			}
		}
		CoTaskMemFree(AddErrors);
		CoTaskMemFree(pResults);
		pResults = NULL;
	}
	else printf ("\n...Items added to group successfully\n");

	// Free the memory allocated to the item names in ItemArray

	CoTaskMemFree(AddErrors);
//	CoTaskMemFree(pErrors);
//	CoTaskMemFree(ValidationResults);

	for (n=0; n < (int)*dwCount; n++)
		CoTaskMemFree(ItemArray[n].szItemID);

	return pResults;
}

void GetWriteData(DWORD dwCount, VARIANT *pItemValues)
{
	// This function takes user input and assigns the values to OPC items.
	// These values are then written to the target processor.
	// The allowed items are integer and float for this sample.
	
	DWORD n;
	char szWriteValue[16];

	// Prompt for new item values from user
	for (n=0; n < dwCount; n++){
		if (pItemValues[n].vt == VT_I2){
			printf("Enter the integer value for item %d\n", n);
			gets(szWriteValue);
			pItemValues[n].iVal = atoi(szWriteValue);
		}
		else if (pItemValues[n].vt == VT_R4){
			printf("Enter the float value for item %d\n", n);
			gets(szWriteValue);
			pItemValues[n].fltVal = (float)atof(szWriteValue);
		}
		else printf("Data type not supported by GetWriteData\n");
	}

}

IUnknown *CreateLocalServer(WCHAR *pwszServerName)
{
	HRESULT r1;
	IUnknown *pIUnknown;
	CLSID	Clsid;
	IClassFactory	*pIFactory;


	//Obtain class ID of RSlinx OPC Server from the Prog ID.
	r1 = CLSIDFromProgID(pwszServerName, &Clsid);
	if (FAILED(r1)){
		DisplaySystemError("Error at CLSIDFromProgID", r1);
		CoUninitialize();
		exit (1);
	}
	else{
		printf("found Class ID for RSLinx OPC server\n");
	}

	//Create class factory interface pointer
	r1 = CoGetClassObject(Clsid, CLSCTX_ALL, NULL,
						  IID_IClassFactory, (void**)&pIFactory);
	if (FAILED(r1)){
		DisplaySystemError("Error at CoGetClassObject", r1);
		CoUninitialize();
		exit (1);
	}
	
	// Create an instance of the RSLinx server object and obtain a pointer
	// to the IUnknown interface
	r1 = pIFactory->CreateInstance(NULL, IID_IUnknown,(void**)&pIUnknown);
	if (FAILED(r1)){
		DisplaySystemError("Error attempting to create server object", r1);
		CoUninitialize();
		exit (1);
	}

	// Release the class factory
	pIFactory->Release();

	return pIUnknown;
}

IUnknown *CreateInProcServer(WCHAR *pwszServerName)
{
	HRESULT r1;
	IUnknown *pIUnknown;
	CLSID	Clsid;
	IClassFactory	*pIFactory;


	//Obtain class ID of RSlinx OPC Server from the Prog ID.
	r1 = CLSIDFromProgID(pwszServerName, &Clsid);
	if (FAILED(r1)){
		DisplaySystemError("Error at CLSIDFromProgID", r1);
		return (IUnknown*)NULL;
//		CoUninitialize();
//		exit (1);
	}
	else{
		printf("found Class ID for RSLinx OPC server\n");
	}

	//Create class factory interface pointer
	r1 = CoGetClassObject(Clsid, CLSCTX_INPROC_SERVER, NULL,
						  IID_IClassFactory, (void**)&pIFactory);
	if (FAILED(r1)){
		DisplaySystemError("Error at CoGetClassObject", r1);
		return (IUnknown*)NULL;
		//CoUninitialize();
		//exit (1);
	}
	
	// Create an instance of the RSLinx server object and obtain a pointer
	// to the IUnknown interface
	r1 = pIFactory->CreateInstance(NULL, IID_IUnknown,(void**)&pIUnknown);
	if (FAILED(r1)){
		DisplaySystemError("Error attempting to create server object", r1);
		return (IUnknown*)NULL;
		//CoUninitialize();
		//exit (1);
	}

	// Release the class factory
	pIFactory->Release();

	return pIUnknown;
}

IUnknown *CreateRemoteServer(WCHAR *pwszServerName, WCHAR *pwszComputerName)
{
	HRESULT r1;
	CLSID	Clsid;
	MULTI_QI mqi;
	COSERVERINFO	sin, *sinptr;
	DWORD clsctx;
	
	//Obtain class ID of RSlinx OPC Server from the Prog ID.
	r1 = CLSIDFromProgID(pwszServerName, &Clsid);
	if (FAILED(r1)){
		DisplaySystemError("Error at CLSIDFromProgID", r1);
		CoUninitialize();
		exit (1);
	}
	else{
		printf("found Class ID for RSLinx OPC server\n");
	}

	// Configure server info structure
	//
	if(*pwszComputerName)
	{
		sinptr = &sin;
		sin.dwReserved1 = 0;
		sin.dwReserved2 = 0;
		sin.pwszName = pwszComputerName;
		sin.pAuthInfo = 0;
		clsctx = CLSCTX_ALL;
	} else
	{
		// If NODE is Nul then try local server
		sinptr = 0;		// pointer should be NULL if local
	}

	// set up mqi
	//
	mqi.pIID = &IID_IUnknown;
	mqi.hr = 0;
	mqi.pItf = 0;

	// To use this function, define _WIN32_DCOM in 'Settings'
	r1 = CoCreateInstanceEx(Clsid, NULL, 
		clsctx, sinptr, 1, &mqi);

	if (FAILED(r1) || FAILED(mqi.hr))
	{
		printf("CoCreateInstanceEx - failed for node:%ls ProgID:%ls (%lx)\n", pwszComputerName, pwszServerName, r1);
		DisplaySystemError("at CoInitialize instance", r1);
		DisplaySystemError("at CoInitialize instance", mqi.hr);
		
		return NULL;
	}

	printf("Remote Object (with IUnknown) Created for %ls\n", pwszServerName);
	return (IUnknown*)mqi.pItf;
}


