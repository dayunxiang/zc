/*
 * SINK.H
 * Connectable Object Sample, Chapter 4
 *
 * Definitions, classes, and prototypes
 *
 * Copyright (c)1993-1995 Microsoft Corporation, All Rights Reserved
 *
 * Kraig Brockschmidt, Microsoft
 * Internet  :  kraigb@microsoft.com
 * Compuserve:  >INTERNET:kraigb@microsoft.com
 */

#include <windows.h>
#include "opcda.h"
#include "opccomn.h"




class COPCDataCallback : public IOPCDataCallback
{
    private:
        ULONG       m_cRef;     //Reference count

    public:
        COPCDataCallback(void);
        ~COPCDataCallback(void);

        //IUnknown members
        STDMETHODIMP         QueryInterface(REFIID, void**);
        STDMETHODIMP_(DWORD) AddRef(void);
        STDMETHODIMP_(DWORD) Release(void);

        //IOPCDataCallback members
        STDMETHODIMP OnDataChange(DWORD, OPCHANDLE, HRESULT, HRESULT, DWORD,
								  OPCHANDLE*, VARIANT*, WORD*, FILETIME*, HRESULT*);
        STDMETHODIMP OnReadComplete(DWORD, OPCHANDLE, HRESULT, HRESULT, DWORD,
								    OPCHANDLE*, VARIANT*, WORD*, FILETIME*, HRESULT*);
        STDMETHODIMP OnWriteComplete(DWORD, OPCHANDLE, HRESULT, DWORD,
								     OPCHANDLE*, HRESULT*);
        STDMETHODIMP OnCancelComplete(DWORD, OPCHANDLE);
};



