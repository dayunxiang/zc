#include "opc.h"
#include "utils.h"
#include <stdio.h>

extern IOPCServer		*pOPCServer;

void DisplayError(LPCTSTR szError, HRESULT hr)
{
	// This function displays error string from an OPC error code
	//
	//		22-AUG-99 (EPD)
	//			Function created
	//
	
	LPWSTR	pswErrorString;
	HRESULT	result;

	if (pOPCServer != NULL){ 
		result = pOPCServer->GetErrorString(hr, LOCALE_SYSTEM_DEFAULT, &pswErrorString);
		if (FAILED(result)){
			printf("GetErrorString failed\n");
			DisplaySystemError(szError, hr);
			return;
		}
		else if (result == E_INVALIDARG){
			printf ("GetErrorString could not interpret error code %lx\n", hr);
		}
		else{
			printf ("%s: %ls\n", szError, pswErrorString);
			CoTaskMemFree(pswErrorString);
		}
	}
}

void DisplaySystemError(LPCTSTR ErrorStr, HRESULT hr)
{
	// This function creates a string from an error code
	// using the Win32 API function 'FormatMessage' 
	//
	//		26-AUG-1999	(EPD)
	//			Function created
	//
	LPVOID	lpszErrorString = NULL;

	if (HRESULT_FACILITY(hr) == FACILITY_WINDOWS)
		hr = HRESULT_CODE(hr);

	FormatMessage(
					FORMAT_MESSAGE_ALLOCATE_BUFFER|
					FORMAT_MESSAGE_FROM_SYSTEM|
					FORMAT_MESSAGE_IGNORE_INSERTS,
					NULL,
					hr,
					MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
					(LPTSTR)&lpszErrorString,
					0,
					NULL);

	printf("%s: %s\n", ErrorStr, (LPCTSTR)lpszErrorString);

	LocalFree(lpszErrorString);
}



void DisplayDataFromVariant(VARIANT *pValues)
{

	//This function prints the data received from server.
	//
	//

	// Determine if data received is an array, otherwise it is a single value.
	if (pValues->vt & VT_ARRAY){

		HRESULT r1;
		short iValue;
		long lValue;
		float fltValue;
		double dblValue;
		BSTR bstrValue;
		UINT uintValue;
		VARIANT_BOOL boolValue;
		long lLowerBound, lUpperBound, lj;
		unsigned short ArrayDataType;
		
		// Get the variant descriptor. It used to by 'SafeArray' functions to extract
		// information from the variant structure.
		SAFEARRAY *Descriptor = pValues->parray;

		//Get lower bound of the array
		r1 = SafeArrayGetLBound(Descriptor, 1, &lLowerBound);
		if (FAILED(r1)){
			printf("DisplayData: SafeArrayGetLBound failed\n");
			DisplaySystemError("Error at SafeArrayGetLBound", r1);
			return;
		}

		//Get upper bound of the array
		r1 = SafeArrayGetUBound(Descriptor, 1, &lUpperBound);
		if (FAILED(r1)){
			printf("DisplayData: SafeArrayGetUBound failed\n");
			DisplaySystemError("Error at SafeArrayGetUBound", r1);
			return;
		}

		//Extract data type from variant type
		ArrayDataType = pValues->vt - VT_ARRAY;

		switch (ArrayDataType){
		//print the data arrays
		case VT_BOOL: //boolean
			for (lj = lLowerBound; lj <= lUpperBound; lj++){
				r1 = SafeArrayGetElement(Descriptor, &lj, &boolValue);
				if (FAILED(r1)){
					printf("DisplayData: SafeArrayGetElement failed\n");
					break;
				}
				printf("boolean array value [%ld] is %ux\n", lj, boolValue);
			}
			break;
		case VT_UI1: //unsigned integer
			for (lj = lLowerBound; lj <= lUpperBound; lj++){
				r1 = SafeArrayGetElement(Descriptor, &lj, &uintValue);
				if (FAILED(r1)){
					printf("DisplayData: SafeArrayGetElement failed\n");
					break;
				}
				printf("unsigned integer array value [%ld] is %hu\n", lj, uintValue);
			}
			break;
		case VT_I2: //integer
			for (lj = lLowerBound; lj <= lUpperBound; lj++){
				r1 = SafeArrayGetElement(Descriptor, &lj, &iValue);
				if (FAILED(r1)){
					printf("DisplayData: SafeArrayGetElement failed\n");
					break;
				}
				printf("signed integer array value [%ld] is %d\n", lj, iValue);
			}
			break;
		case VT_I4: //long integer
			for (lj = lLowerBound; lj <= lUpperBound; lj++){
				r1 = SafeArrayGetElement(Descriptor, &lj, &lValue);
				if (FAILED(r1)){
					printf("DisplayData: SafeArrayGetElement failed\n");
					break;
				}
				printf("long integer array value [%ld] is %ld\n", lj, lValue);
			}
			break;
		case VT_R4: //single precision float
			for (lj = lLowerBound; lj <= lUpperBound; lj++){
				r1 = SafeArrayGetElement(Descriptor, &lj, &fltValue);
				if (FAILED(r1)){
					printf("DisplayData: SafeArrayGetElement failed\n");
					break;
				}
				printf("float array value [%ld] is %f\n", lj, fltValue);
			}
			break;
		case VT_R8: //double precision float
			for (lj = lLowerBound; lj <= lUpperBound; lj++){
				r1 = SafeArrayGetElement(Descriptor, &lj, &dblValue);
				if (FAILED(r1)){
					printf("DisplayData: SafeArrayGetElement failed\n");
					break;
				}
				printf("double float array value [%ld] is %lf\n", lj, dblValue);
			}
			break;
		case VT_BSTR: // character string
			int nLength;
			char *buffer;
			for (lj = lLowerBound; lj <= lUpperBound; lj++){
				r1 = SafeArrayGetElement(Descriptor, &lj, &bstrValue);
				if (FAILED(r1)){
					printf("DisplayData: SafeArrayGetElement failed\n");
					break;
				}			
				else{
					nLength = SysStringLen(bstrValue);
					buffer = (char *)malloc(nLength + 1);
					memset(buffer, '\0', nLength + 1);
					wcstombs(buffer, bstrValue, nLength );
					printf("string array value [%ld] is %s\n", lj, buffer);
					free(buffer);
				}
			}
			break;
	
		default:
			printf ("array data type not supported\n");
		}
	}
	else{
		//print single data values
		switch (pValues->vt){
		case VT_BOOL: //boolean
			printf("the boolean value is %ux\n", pValues->boolVal);
			break;
		case VT_UI1: //unsigned integer
			printf("the unsigned integer value is %hu\n", pValues->bVal);
			break;
		case VT_I2: // integer
			printf("the signed integer value is %d\n", pValues->iVal);
			break;
		case VT_I4: // long integer
			printf("the long integer value is %ld\n", pValues->lVal);
			break;
		case VT_R4: // single precision float
			printf("the float value is %f\n", pValues->fltVal);
			break;
		case VT_R8: // double precision float
			printf("the double float value is %lf\n", pValues->dblVal);
			break;
		case VT_BSTR: // character string
			printf("the string value is %ls\n",pValues->bstrVal);
			break;
		default:
			printf ("single data type not supported\n");
		}
	}
}



///////////////////////////////////////
// OPCVariantUnPack - Used by Client
// 'Marshalls' the Variant OUT of the 'HGLOBAL'
// The passed variant is assumed to be EMPTY 
// (see VariantInit or VariantClear)
//
void	UnMarshalDataToVariant( VARIANT *vp, char * src)
{
	unsigned short *bp;

	// copy the 'body' of the variant
	// This works OK for any of the numeric values 
	// within the union itself
	//
	memcpy(vp, src, sizeof(VARIANT));		
	
	// Check for special cases (things with pointers)
	//

	if (vp->vt & VT_ARRAY){
		if (vp->vt == (VT_BSTR|VT_ARRAY)){
			SAFEARRAY sa;
			int ElementSize;
			VARTYPE vtTemp;
			long count, n;
			BSTR bstrValue;
			char *StartOfString;

			vtTemp = vp->vt & 0x00FF;

			memcpy(&sa, src+sizeof(VARIANT), sizeof(SAFEARRAY));
			vp->parray = SafeArrayCreate(VT_BSTR, 1, sa.rgsabound);
			ElementSize = SafeArrayGetElemsize(vp->parray);
			SafeArrayGetUBound(vp->parray, 1, &count);
			StartOfString = src+sizeof(VARIANT)+sizeof(SAFEARRAY);
			DWORD dwStringLength;
			for (n=0; n <= count; n++){
				memcpy(&dwStringLength, StartOfString, sizeof(DWORD));
				StartOfString += sizeof(DWORD);
				bstrValue = SysAllocStringLen((OLECHAR*)StartOfString, dwStringLength/2);
				if (bstrValue != NULL){
					SafeArrayPutElement(vp->parray, &n, (void*)bstrValue);
					SysFreeString(bstrValue);
				}
				StartOfString += (dwStringLength+2);
			}
		}
		else {
			SAFEARRAY sa;
			int ElementSize;
			VARTYPE vtTemp;
			long count, j, n;
			char *ElementPtr;

			vtTemp = vp->vt & 0x00FF;

			memcpy(&sa, src+sizeof(VARIANT), sizeof(SAFEARRAY));
			vp->parray = SafeArrayCreate(vtTemp, 1, sa.rgsabound);
			ElementSize = SafeArrayGetElemsize(vp->parray);
			ElementPtr = src + sizeof(VARIANT) + sizeof(SAFEARRAY);
			SafeArrayGetUBound(vp->parray, 1, &count);
			for (n=0, j=0; n < count +1; n++, j+=ElementSize)
				SafeArrayPutElement(vp->parray, &n, (ElementPtr+j));
		}
	}
	else{
		switch(vp->vt){
		case VT_BSTR:
			DWORD strlen;
			// invalidate the pointer which is from the server's context
			//
			vp->bstrVal = NULL;

			src += sizeof(VARIANT);		// skip past the Variant 'core'
			strlen = *(DWORD*) src;		// get byte count (char count x 2)
			src += sizeof(DWORD);		// skip to the string (WIDECHAR)
			bp = (unsigned short *) src;	// get string pointer
			// Allocate memory, store the string, append the NUL
			//
			vp->bstrVal = SysAllocStringLen( (unsigned short*) bp, strlen/2);
			break;

		default:
			break;
		}
	}
}


void DisplayGroupData( DWORD TransactionId, OPCHANDLE GroupHandle, DWORD NumberOfItems,
					  OPCHANDLE *ItemHandles, VARIANT *VariantData, WORD *Quality)
{
	DWORD n;

	printf ("the transaction id is %ld\n", TransactionId);
	printf ("the group handle is %ld\n", GroupHandle);
	printf ("the NumberOfItems is %ld\n", NumberOfItems);

	for (n=0; n < NumberOfItems; n++){
	
		printf ("item handle number %d is %ld\nthe data follows:\n", n, ItemHandles[n]);

		switch(Quality[0]){
		case OPC_QUALITY_GOOD:
			DisplayDataFromVariant(&VariantData[0]);
			break;
		case OPC_QUALITY_BAD:
			printf("Bad quality\n");
			break;
		case OPC_QUALITY_UNCERTAIN:
			printf("Quality uncertain\n");
			break;
		case OPC_QUALITY_DEVICE_FAILURE:
			printf("Device failure\n");
			break;
		case OPC_QUALITY_SENSOR_FAILURE:
			printf("Sensor failure\n");
			break;
		case OPC_QUALITY_COMM_FAILURE:
			printf("Comm failure\n");
			break;
		default:
			printf ("unknown quality value\n");
		}
	}
}


void DisplayGroupTimeStampData( DWORD TransactionId, OPCHANDLE GroupHandle, DWORD NumberOfItems,
					  OPCHANDLE *ItemHandles, VARIANT *VariantData, WORD *Quality,
					  FILETIME *TimeStamp)
{
	DWORD n;
	FILETIME LocalFileTime;
	SYSTEMTIME SystemTimeStamp;

	printf ("the transaction id is %ld\n", TransactionId);
	printf ("the group handle is %ld\n", GroupHandle);
	printf ("the number of items is %ld\n", NumberOfItems);

	for (n=0; n < NumberOfItems; n++){
	
		printf ("item handle %d is %ld\nthe data follows:\n", n, ItemHandles[n]);
		FileTimeToLocalFileTime(&TimeStamp[n], &LocalFileTime);
		FileTimeToSystemTime(&LocalFileTime, &SystemTimeStamp);
		printf ("data timestamp: %02d-%02d-%4d  %02d:%02d:%02d\n", 
			     SystemTimeStamp.wMonth, SystemTimeStamp.wDay, SystemTimeStamp.wYear,
				 SystemTimeStamp.wHour, SystemTimeStamp.wMinute, SystemTimeStamp.wSecond);

		switch(Quality[0]){
		case OPC_QUALITY_GOOD:
			DisplayDataFromVariant(&VariantData[0]);
			break;
		case OPC_QUALITY_BAD:
			printf("Bad quality\n");
			break;
		case OPC_QUALITY_UNCERTAIN:
			printf("Quality uncertain\n");
			break;
		case OPC_QUALITY_DEVICE_FAILURE:
			printf("Device failure\n");
			break;
		case OPC_QUALITY_SENSOR_FAILURE:
			printf("Sensor failure\n");
			break;
		case OPC_QUALITY_COMM_FAILURE:
			printf("Comm failure\n");
			break;
		default:
			printf ("unknown quality value\n");
		}
	}
}