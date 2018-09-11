// forLuoyangDlg.h : header file
//

#if !defined(AFX_FORLUOYANGDLG_H__6BFE46B7_B3B2_4313_87A8_3778D4AA1A02__INCLUDED_)
#define AFX_FORLUOYANGDLG_H__6BFE46B7_B3B2_4313_87A8_3778D4AA1A02__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
#include "cipsoft.h"

/////////////////////////////////////////////////////////////////////////////
// CForLuoyangDlg dialog

class CForLuoyangDlg : public CDialog
{
// Construction
public:
	CForLuoyangDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CForLuoyangDlg)
	enum { IDD = IDD_FORLUOYANG_DIALOG };
	CComboBox	m_datatype;
	CListBox	m_list1;
	CString	m_ipaddr;
	UINT	m_slot;
	short	m_intvalue;
	int		m_dintvalue;
	BYTE	m_sintvalue;
	float	m_realvalue;
	CString	m_tagname;
	UINT	m_tagnumber;
	UINT	m_index;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CForLuoyangDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL
	public:
	 	CCIP2 cip1;
		int m_tagtype;  //数据类型
		int iSessionHandler;    //会话句柄
		int intItemLength;
		BYTE bWriteType;
		DWORD dwSenderContext;
// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CForLuoyangDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnConnect();
	afx_msg void OnRead();
	afx_msg void OnWrite();
	afx_msg void OnSelchangeDatatype();
	virtual void OnOK();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_FORLUOYANGDLG_H__6BFE46B7_B3B2_4313_87A8_3778D4AA1A02__INCLUDED_)
