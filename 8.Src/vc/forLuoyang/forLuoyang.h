// forLuoyang.h : main header file for the FORLUOYANG application
//

#if !defined(AFX_FORLUOYANG_H__FD5975A3_73DE_47F7_BCB9_F7B05410DDBF__INCLUDED_)
#define AFX_FORLUOYANG_H__FD5975A3_73DE_47F7_BCB9_F7B05410DDBF__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CForLuoyangApp:
// See forLuoyang.cpp for the implementation of this class
//

class CForLuoyangApp : public CWinApp
{
public:
	CForLuoyangApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CForLuoyangApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CForLuoyangApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_FORLUOYANG_H__FD5975A3_73DE_47F7_BCB9_F7B05410DDBF__INCLUDED_)
