/*
 * SINK.CPP
 *
 * Implementation of a sink with the IOPCDataCallback interface.
 *
 * Refer to Microsoft documentation for information on implementing Connection Point
 * sinks.
 *					
 *					Revision history
 *						File created (02/12/00 EPD)
 */


#include "Sink.h"
#include "utils.h"
#include <stdio.h>

/*
 * COPCDataCallback::COPCDataCallback
 * COPCDataCallback::~COPCDataCallback
 *
 */

COPCDataCallback::COPCDataCallback(void)
    {
    m_cRef=0;
    return;
    }

COPCDataCallback::~COPCDataCallback(void)
    {
    return;
    }



/*
 * COPCDataCallback::QueryInterface
 * COPCDataCallback::AddRef
 * COPCDataCallback::Release
 *
 * Purpose:
 *  Non-delegating IUnknown members for COPCDataCallback.
 */

STDMETHODIMP COPCDataCallback::QueryInterface(REFIID riid
    , void** ppv)
{
    *ppv=NULL;

//    if (IID_IUnknown==riid || IID_IOPCDataCallback==riid)
//      *ppv=this;

    if (IID_IUnknown==riid)
		 *ppv=reinterpret_cast<IUnknown*>(this);
	else if (IID_IOPCDataCallback==riid)
		*ppv = (IOPCDataCallback*)this;

    if (NULL!=*ppv)
        {
        ((LPUNKNOWN)*ppv)->AddRef();
        return NOERROR;
        }

    return ResultFromScode(E_NOINTERFACE);
}

STDMETHODIMP_(ULONG) COPCDataCallback::AddRef(void)
{
    return ++m_cRef;
}

STDMETHODIMP_(ULONG) COPCDataCallback::Release(void)
{
    if (0!=--m_cRef)
        return m_cRef;

    delete this;
    return 0;
}


        
/*
 * COPCDataCallback::OnDataChange
 *
 * Purpose: Called by the OPC server when a group is in subscription mode. If a data value
 * changes, the new value and other information is returned. If there is a problem with
 * an item, error information is returned.
 *  
 */

STDMETHODIMP COPCDataCallback::OnDataChange(DWORD dwTransid, OPCHANDLE hGroup, HRESULT hrMasterquality,
											HRESULT hrMastererror, DWORD dwCount,
								            OPCHANDLE *phClientItems, VARIANT *pvValues,
											WORD *pwQualities, FILETIME *pftTimeStamps,
											HRESULT *pErrors)
{
 

	DWORD n;
	FILETIME LocalFileTime;
	SYSTEMTIME SystemTimeStamp;

	printf ("\n...data changed or refreshed...\n");
	printf ("the transaction id is %ld\n", dwTransid);
	printf ("the group handle is %ld\n", hGroup);
	printf ("the number of items is %ld\n", dwCount);

	for (n=0; n < dwCount; n++){
	
		printf ("\nitem handle %d is %ld\nthe data follows:\n", n, phClientItems[n]);
		FileTimeToLocalFileTime(&pftTimeStamps[n], &LocalFileTime);
		FileTimeToSystemTime(&LocalFileTime, &SystemTimeStamp);
		printf ("data timestamp: %02d-%02d-%4d  %02d:%02d:%02d\n", 
			     SystemTimeStamp.wMonth, SystemTimeStamp.wDay, SystemTimeStamp.wYear,
				 SystemTimeStamp.wHour, SystemTimeStamp.wMinute, SystemTimeStamp.wSecond);

		switch(pwQualities[n]){
		case OPC_QUALITY_GOOD:
			DisplayDataFromVariant(&pvValues[n]);
			break;
		case OPC_QUALITY_BAD:
			printf("Bad quality\n");
    		if (FAILED(pErrors[n])){
				DisplayError("Error reading data", pErrors[n]);
			}
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

    return NOERROR;
}



/*
 * COPCDataCallback::OnReadComplete
 *
 * Purpose: Called by the OPC server after an asynchronous read is performed. If successful,
 * the current data value and other information is returned. If the read fails,
 * error information is returned.
 *
 */

STDMETHODIMP COPCDataCallback::OnReadComplete(DWORD dwTransid, OPCHANDLE hGroup, HRESULT hrMasterquality,
											HRESULT hrMastererror, DWORD dwCount,
								            OPCHANDLE *phClientItems, VARIANT *pvValues,
											WORD *pwQualities, FILETIME *pftTimeStamps,
											HRESULT *pErrors)
{


	DWORD n;
	FILETIME LocalFileTime;
	SYSTEMTIME SystemTimeStamp;

	printf ("\n...read completed...\n");
	printf ("the transaction id is %ld\n", dwTransid);
	printf ("the group handle is %ld\n", hGroup);
	printf ("the number of items is %ld\n", dwCount);

	for (n=0; n < dwCount; n++){
	
		printf ("\nitem handle %d is %ld\nthe data follows:\n", n, phClientItems[n]);
		FileTimeToLocalFileTime(&pftTimeStamps[n], &LocalFileTime);
		FileTimeToSystemTime(&LocalFileTime, &SystemTimeStamp);
		printf ("data timestamp: %02d-%02d-%4d  %02d:%02d:%02d\n", 
			     SystemTimeStamp.wMonth, SystemTimeStamp.wDay, SystemTimeStamp.wYear,
				 SystemTimeStamp.wHour, SystemTimeStamp.wMinute, SystemTimeStamp.wSecond);

		switch(pwQualities[n]){
		case OPC_QUALITY_GOOD:
			DisplayDataFromVariant(&pvValues[n]);
			break;
		case OPC_QUALITY_BAD:
			printf("Bad quality\n");
    		if (FAILED(pErrors[n])){
				DisplayError("Error reading data", pErrors[n]);
			}
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


    return NOERROR;
}



/*
 * COPCDataCallback::OnWriteComplete
 *
 * Purpose: Called when an asynchronous write is performed. If successful, the 
 * transaction id and other information is returned. If the write fails, error
 * information is returned.
 */

STDMETHODIMP COPCDataCallback::OnWriteComplete(DWORD dwTransid, OPCHANDLE hGroup,
											   HRESULT hrMastererror, DWORD dwCount,
								               OPCHANDLE *phClientItems, HRESULT *pErrors)
  
{
	DWORD n;

	printf("\n...write completed...\n");
	printf ("the transaction id is %ld\n", dwTransid);
	printf ("the group handle is %ld\n", hGroup);
	printf ("the number of items is %ld\n", dwCount);

	for (n=0; n < dwCount; n++){
	
		printf ("item handle %d is %ld\n", n, phClientItems[n]);
    	if (FAILED(pErrors[n])){
			DisplayError("Error writing data", pErrors[n]);
		}

		
	}

    return NOERROR;
}


/*
 * COPCDataCallback::OnCancelComplete
 *
 * Purpose: Called by OPC server after a transaction is cancelled. It returns the 
 * transaction id and the group handle.
 *
 */
STDMETHODIMP COPCDataCallback::OnCancelComplete(DWORD dwTransid, OPCHANDLE hGroup)
{

	printf ("\n...cancellation completed...\n");
	printf ("the transaction id is %ld\n", dwTransid);
	printf ("the group handle is %ld\n", hGroup);

	return NOERROR;
}




