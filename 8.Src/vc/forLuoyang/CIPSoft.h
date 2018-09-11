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
//��ű�ǩ���ƺ�Ԫ�صĸ����Ľṹ��
typedef struct _TagAndElements
{
	CString strName;  //����
	unsigned short int usintElements;  //Ԫ����
}TagAndElements;

//���Ҫд�ı�ǩ���ơ�Ԫ�ظ�����ֵ���͡�ֵ
typedef struct _TagsWrite
{
	CString strName;
    BYTE bDataType;
	BYTE bReserver;
	unsigned short int usintElements; 
	unsigned short int uintAddtionalLength;
}TagsWrite;
    
//IOS�ִ�
typedef struct _IOS
{
	BYTE bExtendedSymbolSegment; //�ִ�����,0x91Ϊ��ǩ����
	BYTE      bDataSize;   //��ǩ���Ƶ��ֽ�����
	BYTE      *bData;      //��ǩ����
	BYTE      *pad;        //��ǩ���Ƶ��ַ���Ϊ����ʱ������ֽ�
}IOS;//RequirePath;

//�����
typedef struct _ServicePack
{
	BYTE bService;  //�������ͣ� 0x4cΪ��, 0x4dΪд
	BYTE bRequirePathWords;  //IOS�ִ���16bit��
	BYTE bExtendedSymbolSegment; //�ִ�����,0x91Ϊ��ǩ����
	BYTE      bDataSize;   //��ǩ���Ƶ��ֽ�����
}ServicePack;


typedef struct _ValueToWrite
{
	BYTE bDataType;
	BYTE bReserve;
	unsigned short int usintElements;
}ValueToWrite;



//��װЭ��ͷ,24�ֽ�
typedef struct _EncapsulationCIPHead
{
	short int sintCommand;   
	//�������,0x65:RegisterSession,0x66: UnRegisterSession,0x04:ListServices, 
	//0x63:ListIdentity, 0x64:ListInterface, 0x6f:SendRRData,0x70:SendUnitData
	short int sintLength;  //CIPЭ���ֽ�������������װЭ��
	int       intSessionHandle;  //�Ự���,��Ŀ�귽����ԭʼ��
	int       intStatus;         //״̬��,�ɹ�Ϊ0��ԭʼ��Ӧ���ô�ֵΪ0
	BYTE      bSenderContext[8];  //���ͷ������ģ����ݴ�ֵ�ж���Ӧ��������Ӧ�ķ���
	int       intOption;          //��ѡ�ӦΪ0
}EncapsulationCIPHead;


//��ַ��������
typedef struct _AddressAndDataItem
{
	short int sintTypeID;  //��ַ������������,��ӦUnConnected Message, AddressӦ��Ϊ0x00,
	//DataӦ��Ϊ0xb2;
	short int sintLength;  //��ַ�����ݵ����ݲ��ֳ���
}AddressAndDataItem;

//���������ר������
typedef struct _CommandSpecificData
{
    int intInterfaceHandle;  //�ӿھ��,CIP��ӦΪ0��
	short int sintTimeout;   //������ʱ��ָ��װЭ��
	short int sintItemCount; //��ַ�����ݵ�������һ��Ϊ2
	short int sintAddressTypeID; //��ַ
	short int sintAddressItemLength;
	short int sintDataTypeID;    //����
	short int sintDataItemLength;
	//AddressAndDataItem address;  //��ַ
	//AddressAndDataItem data;     //����
}CommandSpecificData;

//CIPЭ��
typedef struct _CommondIndustrialProtocol
{
	BYTE bService;  //��������, 0x52Ϊ�����ӷ��ͷ�������
	BYTE bRequestPath;  //����·��16bit��(Ϊ2)
	BYTE bLogicalClassSegment;  //�߼��࣬20:ָ8λ�߼���
	BYTE bClass;                //�࣬06:ָ���ӹ������࣬
	BYTE bLogicalInstanceSegment; //�߼�ʵ��,24:ָ8λ�߼�ʵ��
	BYTE bInstance;             //ʵ��,01:ʵ��01
    //Ҫ��
}CommonIndustrialProtocol;

//�����������Ӧ������
typedef struct _ServiceRequestData
{
	BYTE bPriorityAndTime_tick;  //����Ȩ�ͳ�ʱ����
	BYTE bTime_out_ticks;  //��ʱʱ�䳣��
	unsigned short int sintMessageRequestSize; //�����ĵ��ֽ���
	BYTE bService;  //��������,0x0a:��������
	BYTE bRequestPathSize;    //�������·��
	BYTE bRequestPathClassSegment; //�������·�����������Ķ�
	BYTE bClass;                   //������
	BYTE bRequestPathInstacneSegment; //�������·����ʵ�������Ķ�
	BYTE bInstance;                //ʵ��
	//Ҫ��
}ServiceRequestData;

//������
typedef struct _MessageRequest
{
	BYTE bService;  //��������,0x0a:��������
	BYTE bRequestPathSize;    //
	BYTE bRequestPathClassSegment;
	BYTE bClass;
	BYTE bRequestPathInstacneSegment;
	BYTE bInstance;
	//Ҫ��
}MessageRequest;

//�������
typedef struct _ServicesAggregate
{
	unsigned short int usintNumberOfServices; //������
//	unsigned short int *pusintOffsets;        //���������ƫ���ֽ�
//	ServicePack *spPack[];                     //�����
}ServicesAggregate;

typedef struct _RoutePath
{
	BYTE bRoutePathSizeWord;  //16bit����0x01
	BYTE BResever;            //����   :0
	BYTE bExtendLinkAddress;  //�Ƿ���չ���ӵ�ַ:01
	BYTE bLinkAddress;        //��ַ:0
}RoutePath;

typedef struct _recCIP
{
	BYTE bService;  //��������,0x8a:��������
	BYTE bReserve;
	BYTE bGeneralStatus;   //�������
	BYTE bAddtionalStatusSizeWords;  //��չ��������ִ�С
}recCIP;

typedef struct _recServices
{
	BYTE bService;  //�������
	BYTE bReserve;  //����
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
	CommandSpecificData *cpdSendRRData;      //���CommandSpecificData�ṹ��
	ServiceRequestData *sqdSendRRData;    //��������������
	CMySocket myClient;
	int intSessionHandle;
	int pos;
	int intTagsDescriptionPos;
	int intCIPPos; //CIP����ʼλ�ã����ڼ���CIP(������InterfaceHandle,timeout��Address,data)�ĳ���
	int intMRPos;  //Message Route����ʼλ�ã����ڼ����Message Route�ĳ���
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
