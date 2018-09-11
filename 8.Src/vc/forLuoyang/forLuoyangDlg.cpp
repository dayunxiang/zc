// forLuoyangDlg.cpp : implementation file
//

#include "stdafx.h"
#include "forLuoyang.h"
#include "forLuoyangDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

void CALLBACK ReceiveData(bool bSuccess,bool blRead,DWORD dwSendContext,int iSize,DWORD *dwData)
{
	BYTE *bData;
	short int *sintData;
	int *intData;
	float *ftData;

	CString str1;
	int i;
  
  	CForLuoyangDlg *mainwnd=(CForLuoyangDlg *)AfxGetMainWnd();
   
    int itemLength=mainwnd->intItemLength;
	if(blRead)
	{
		mainwnd->m_list1.ResetContent();
		switch(mainwnd->m_tagtype)
		{
		case 0:
			bData=(BYTE *)dwData;
			for(i=0;i<iSize/itemLength;i++)
			{
			str1.Format("%s[%d]:    %d",mainwnd->m_tagname,mainwnd->m_index+i,bData[i]);
			mainwnd->m_list1.AddString(str1);
			}
			break;
	
		case 1:
			sintData=(short int *)dwData;
			for(i=0;i<iSize/itemLength;i++)
			{
				str1.Format("%s[%d]:    %d",mainwnd->m_tagname,mainwnd->m_index+i,sintData[i]);
				mainwnd->m_list1.AddString(str1);
			}
			break;
		case 2:
			intData=(int *)dwData;
			for(i=0;i<iSize/itemLength;i++)
			{
				str1.Format("%s[%d]:    %d",mainwnd->m_tagname,mainwnd->m_index+i,intData[i]);
				mainwnd->m_list1.AddString(str1);
			}
			break;
		case 3:
			ftData=(float *)dwData;
			for(i=0;i<iSize/itemLength;i++)
			{
			str1.Format("%s[%d]:    %f",mainwnd->m_tagname,mainwnd->m_index+i,ftData[i]);
			mainwnd->m_list1.AddString(str1);
			}
			break;
		default:
			break;
		}
	}
}


class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CForLuoyangDlg dialog

CForLuoyangDlg::CForLuoyangDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CForLuoyangDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CForLuoyangDlg)
	m_ipaddr = _T("192.168.1.20");
	m_slot = 0;
	m_intvalue = 0;
	m_dintvalue = 0;
	m_sintvalue = 0;
	m_realvalue = 0.0f;
	m_tagname = _T("tag3");
	m_tagnumber = 100;
	m_index = 0;
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CForLuoyangDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CForLuoyangDlg)
	DDX_Control(pDX, IDC_DATATYPE, m_datatype);
	DDX_Control(pDX, IDC_LIST1, m_list1);
	DDX_Text(pDX, IDC_IPADDR, m_ipaddr);
	DDX_Text(pDX, IDC_SLOT, m_slot);
	DDX_Text(pDX, IDC_INTVALUE, m_intvalue);
	DDX_Text(pDX, IDC_DINTVALUE, m_dintvalue);
	DDX_Text(pDX, IDC_SINTVALUE, m_sintvalue);
	DDX_Text(pDX, IDC_REALVALUE, m_realvalue);
	DDX_Text(pDX, IDC_TAGNAME, m_tagname);
	DDX_Text(pDX, IDC_TAGNUMBER, m_tagnumber);
	DDX_Text(pDX, IDC_INDEX, m_index);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CForLuoyangDlg, CDialog)
	//{{AFX_MSG_MAP(CForLuoyangDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_CONNECT, OnConnect)
	ON_BN_CLICKED(IDC_READ, OnRead)
	ON_BN_CLICKED(IDC_WRITE, OnWrite)
	ON_CBN_SELCHANGE(IDC_DATATYPE, OnSelchangeDatatype)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CForLuoyangDlg message handlers

BOOL CForLuoyangDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
    m_datatype.SetCurSel(2);
	m_tagtype=2;
	bWriteType=0xc4;
	intItemLength=4;
	dwSenderContext=1;
	// TODO: Add extra initialization here
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CForLuoyangDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CForLuoyangDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CForLuoyangDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CForLuoyangDlg::OnConnect() 
{
	//建立连接，参数依次为PLC的IP地址，控制器模块槽号，会话句柄，回调函数
    UpdateData(true);
	if(!cip1.Create(m_ipaddr,m_slot,&iSessionHandler,ReceiveData))
	{
		GetDlgItem(IDC_READ)->EnableWindow(true);
		GetDlgItem(IDC_WRITE)->EnableWindow(true);
	}
	else
		MessageBox("Can't create socket or open Session");	
}



void CForLuoyangDlg::OnRead() 
{
	// TODO: Add your control notification handler code here
	UpdateData(true);
	if(m_tagnumber*intItemLength<=400)
	{
		//四个参数分别为：上下文相关(每次加1)、数组偏移、总的项数、标签名字
		cip1.ReadDINT(dwSenderContext++,(DWORD)m_index,m_tagnumber,m_tagname);
	}
	else
		MessageBox("Please make sure total data size is less than 400");
}

void CForLuoyangDlg::OnWrite() 
{
	// TODO: Add your control notification handler code here
	UpdateData(true);
	BYTE *bwriteData=new BYTE[400];
	short int *sintwriteData=new short int[200];
	int *intwriteData=new int[100];
	float *floatwriteData=new float[100];
	unsigned int i;
	switch(m_tagtype)
	{
	case 0:
	   
		for(i=0;i<m_tagnumber;i++)
			bwriteData[i]=m_sintvalue;
		//六个参数，1. 上下文相关，2. 数组起始，3. 总共写的字节数,4. 标签名字，5.数据类型,6.写的数据
		cip1.WriteDINT(dwSenderContext,m_index,m_tagnumber*intItemLength,m_tagname,bWriteType,(BYTE *)bwriteData);
		break;
	case 1:
	
		for(i=0;i<m_tagnumber;i++)
			sintwriteData[i]=m_intvalue;
		cip1.WriteDINT(dwSenderContext,m_index,m_tagnumber*intItemLength,m_tagname,bWriteType,(BYTE *)sintwriteData);
		break;
	case 2:
	
		for(i=0;i<m_tagnumber;i++)
			intwriteData[i]=m_dintvalue;
		cip1.WriteDINT(dwSenderContext,m_index,m_tagnumber*intItemLength,m_tagname,bWriteType,(BYTE *)intwriteData);
		break;
	case 3:
		for(i=0;i<m_tagnumber;i++)
			floatwriteData[i]=m_realvalue;
		cip1.WriteDINT(dwSenderContext,m_index,m_tagnumber*intItemLength,m_tagname,bWriteType,(BYTE *)floatwriteData);
		break;
	}

	delete bwriteData;
	delete sintwriteData;
	delete intwriteData;
	delete floatwriteData;


//			bool WriteDINT(DWORD dwSendContext,unsigned int offset,int iSize,CString strName,BYTE bType,DWORD dwData[MAX_ITEM]);

  }

void CForLuoyangDlg::OnSelchangeDatatype() 
{
	// TODO: Add your control notification handler code here
	m_tagtype=m_datatype.GetCurSel();

	GetDlgItem(IDC_SINTVALUE)->EnableWindow(false);
	GetDlgItem(IDC_INTVALUE)->EnableWindow(false);
	GetDlgItem(IDC_DINTVALUE)->EnableWindow(false);
	GetDlgItem(IDC_REALVALUE)->EnableWindow(false);
	switch(m_tagtype)
	{
	case 0:
		intItemLength=1;
		bWriteType=0xc2; //单字节整数，同PLC中的SINT类型
		GetDlgItem(IDC_SINTVALUE)->EnableWindow(true);
		break;
	case 1:
		intItemLength=2;
		bWriteType=0xc3; //双字节整数，同PLC中的INT
		GetDlgItem(IDC_INTVALUE)->EnableWindow(true);
		break;
	case 2:
		intItemLength=4;
		bWriteType=0xc4; //双字整数，同PLC中的DINT
		GetDlgItem(IDC_DINTVALUE)->EnableWindow(true);
		break;
	case 3:
		intItemLength=4;
		bWriteType=0xca; //浮点数，同PLC中的REAL
		GetDlgItem(IDC_REALVALUE)->EnableWindow(true);
		break;
	}
}


void CForLuoyangDlg::OnOK() 
{
	// TODO: Add extra validation here
	cip1.Close();
	CDialog::OnOK();
}
