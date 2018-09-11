// CIPSoft.h : main header file for the CIPSOFT DLL
//
#ifdef CIP2_EXPORTS
#define CIP2_API __declspec(dllexport)
#else
#define CIP2_API __declspec(dllimport)
#endif

#if !defined(AFX_CIPSOFT_H__51923EFF_28B8_47D1_B884_ACFC848B8AFA__INCLUDED_)
#define AFX_CIPSOFT_H__51923EFF_28B8_47D1_B884_ACFC848B8AFA__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#define MAX_LENGTH 10000
#define MAX_ITEM 1000

#define SEND_RR_DATA 0X006F
#define REGISTER_SESSION 0X0065
#define UNREGISTER_SESSION 0x0066
#define STORE_TASKS 10

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CCIPSoftApp
// See CIPSoft.cpp for the implementation of this class
//
//存放标签名称和元素的个数的结构体
typedef struct _TagAndElements
{
	CString strName;  //名称
	unsigned short int usintElements;  //元素数
}TagAndElements;

//存放要写的标签名称、元素个数、值类型、值
typedef struct _TagsWrite
{
	CString strName;
    BYTE bDataType;
	BYTE bReserver;
	unsigned short int usintElements; 
	unsigned short int uintAddtionalLength;
}TagsWrite;
    
//IOS字串
typedef struct _IOS
{
	BYTE bExtendedSymbolSegment; //字串类型,0x91为标签名称
	BYTE      bDataSize;   //标签名称的字节数，
	BYTE      *bData;      //标签名称
	BYTE      *pad;        //标签名称的字符数为奇数时的填充字节
}IOS;//RequirePath;

//服务包
typedef struct _ServicePack
{
	BYTE bService;  //服务类型， 0x4c为读, 0x4d为写
	BYTE bRequirePathWords;  //IOS字串的16bit数
	BYTE bExtendedSymbolSegment; //字串类型,0x91为标签名称
	BYTE      bDataSize;   //标签名称的字节数，
}ServicePack;


typedef struct _ValueToWrite
{
	BYTE bDataType;
	BYTE bReserve;
	unsigned short int usintElements;
}ValueToWrite;



//封装协议头,24字节
typedef struct _EncapsulationCIPHead
{
	short int sintCommand;   
	//命令代码,0x65:RegisterSession,0x66: UnRegisterSession,0x04:ListServices, 
	//0x63:ListIdentity, 0x64:ListInterface, 0x6f:SendRRData,0x70:SendUnitData
	short int sintLength;  //CIP协议字节数，不包括封装协议
	int       intSessionHandle;  //会话句柄,由目标方传给原始方
	int       intStatus;         //状态码,成功为0，原始方应设置此值为0
	BYTE      bSenderContext[8];  //发送方上下文，根据此值判断响应是由所对应的发送
	int       intOption;          //可选项，应为0
}EncapsulationCIPHead;


//地址和数据项
typedef struct _AddressAndDataItem
{
	short int sintTypeID;  //地址或数据项类型,对应UnConnected Message, Address应设为0x00,
	//Data应设为0xb2;
	short int sintLength;  //地址和数据的数据部分长度
}AddressAndDataItem;

//各种命令的专有数据
typedef struct _CommandSpecificData
{
    int intInterfaceHandle;  //接口句柄,CIP是应为0；
	short int sintTimeout;   //操作超时，指封装协议
	short int sintItemCount; //地址和数据的项数，一般为2
	short int sintAddressTypeID; //地址
	short int sintAddressItemLength;
	short int sintDataTypeID;    //数据
	short int sintDataItemLength;
	//AddressAndDataItem address;  //地址
	//AddressAndDataItem data;     //数据
}CommandSpecificData;

//CIP协议
typedef struct _CommondIndustrialProtocol
{
	BYTE bService;  //服务类型, 0x52为非连接发送服务请求
	BYTE bRequestPath;  //请求路径16bit数(为2)
	BYTE bLogicalClassSegment;  //逻辑类，20:指8位逻辑类
	BYTE bClass;                //类，06:指连接管理器类，
	BYTE bLogicalInstanceSegment; //逻辑实例,24:指8位逻辑实例
	BYTE bInstance;             //实例,01:实例01
    //要加
}CommonIndustrialProtocol;

//上面服务所对应的数据
typedef struct _ServiceRequestData
{
	BYTE bPriorityAndTime_tick;  //优先权和超时因子
	BYTE bTime_out_ticks;  //超时时间常数
	unsigned short int sintMessageRequestSize; //请求报文的字节数
	BYTE bService;  //服务类型,0x0a:多个服务包
	BYTE bRequestPathSize;    //请求服务路径
	BYTE bRequestPathClassSegment; //请求服务路径的类所属的段
	BYTE bClass;                   //段类型
	BYTE bRequestPathInstacneSegment; //请求服务路径的实例所属的段
	BYTE bInstance;                //实例
	//要加
}ServiceRequestData;

//请求报文
typedef struct _MessageRequest
{
	BYTE bService;  //服务类型,0x0a:多个服务包
	BYTE bRequestPathSize;    //
	BYTE bRequestPathClassSegment;
	BYTE bClass;
	BYTE bRequestPathInstacneSegment;
	BYTE bInstance;
	//要加
}MessageRequest;

//多个服务
typedef struct _ServicesAggregate
{
	unsigned short int usintNumberOfServices; //服务数
//	unsigned short int *pusintOffsets;        //各个服务的偏移字节
//	ServicePack *spPack[];                     //服务包
}ServicesAggregate;

typedef struct _RoutePath
{
	BYTE bRoutePathSizeWord;  //16bit数：0x01
	BYTE BResever;            //保留   :0
	BYTE bExtendLinkAddress;  //是否扩展链接地址:01
	BYTE bLinkAddress;        //地址:0
}RoutePath;

typedef struct _recCIP
{
	BYTE bService;  //服务类型,0x8a:多个服务包
	BYTE bReserve;
	BYTE bGeneralStatus;   //错误代码
	BYTE bAddtionalStatusSizeWords;  //扩展错误代码字大小
}recCIP;

typedef struct _recServices
{
	BYTE bService;  //服务代码
	BYTE bReserve;  //保留
}recServices;

typedef struct _RecordTask
{
	DWORD dwContext;
	bool blFinished;
}RecordTask;

typedef struct _ElementOffset
{
	BYTE bFourBytes;
	BYTE bZero;
	DWORD dwOffset;
}ElementOffset;

class CCIP2; 
typedef void (CALLBACK *CB_Receive)(bool bSucceess,bool read,DWORD dwSendContext,int iSize,DWORD *dwData);


class CMySocket:public CSocket
{
public:
	virtual void OnReceive(int nErrorCode);
	virtual void OnClose(int nErrorCode);
	virtual void OnConnect(int nErrorCode);
	virtual void OnSend(int nErrorCode);
    virtual void OnAccept(int nErrorCode);
	int Readn(char *bp, int len);
	int Readvrec(char *bp, int head_len,int len);
	CCIP2 *cParent;
	int ReadFixedOne(BYTE *bp,int len);

};

class Ring
{
private:
	int start;
	int end;
	char *buf;
	bool Order();
public:
	Ring(unsigned int len);
	int Get(char *buf1,int len);
	int Put(const char *buf1,int len);
	int GetNoDelete(char *buf1,int len);
	int GetLength();
	void Clear();
	~Ring();
};


class CIP2_API CCIP2 
{
public:
	CCIP2(void);

	int Create(LPCTSTR ipAdd,BYTE iSlotNumber,int *intHandler,CB_Receive pf);
	void Close();
	void RegisterSession();
    bool SendData(unsigned int iLength);
	void FillSendRRData();
	bool ReadDINT(DWORD dwSendContext,unsigned int offset,int iSize,CString strName);
	bool WriteDINT(DWORD dwSendContext,unsigned int offset,int iSize,CString strName,BYTE bType,BYTE bData[MAX_ITEM]);
    int FillReadData(unsigned int uintOffset,int intTagNumbers,CString strName);
	int FillWriteData(unsigned int uintOffset,int intTagNumbers,CString strName,BYTE bType,BYTE *bData);
	void InitializeReceive();
	void ReadPacket();
	void ReadPacket2();
	void ReadPacket3();
	void ReadOnePacket();
    void CancelBlock();
public:
    RecordTask rtReadWrite[STORE_TASKS];
    CB_Receive mypf;
	Ring *r1;
	char chReceive[MAX_LENGTH];
	char buffer[MAX_LENGTH];
	EncapsulationCIPHead * CIPHead;

private:
	CommandSpecificData *cpdSendRRData;      //填充CommandSpecificData结构体
	ServiceRequestData *sqdSendRRData;    //服务器请求数据
	CMySocket myClient;
	int intSessionHandle;
	int pos;
	int intTagsDescriptionPos;
	int intCIPPos; //CIP的起始位置，用于计算CIP(不包括InterfaceHandle,timeout和Address,data)的长度
	int intMRPos;  //Message Route的起始位置，用于计算和Message Route的长度
    bool blRegister;
	BYTE bSlot;
	int intCurrentTask;

	bool IsHead();
	void ResetPacket();
	void ProcessData();
	void SendErrorTask(bool blRead,DWORD dwContextSuccess);
	bool SeperateStr(CString src, CString & dest1,CString & dest2);
	bool blPacketFinished;
	bool blHeadFinished;
    int intNextReadLen;
	int intBufferPointer;
	int intPacketSize;

};
class CCIPSoftApp : public CWinApp
{
public:
	CCIPSoftApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CCIPSoftApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

	//{{AFX_MSG(CCIPSoftApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};



/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CIPSOFT_H__51923EFF_28B8_47D1_B884_ACFC848B8AFA__INCLUDED_)
