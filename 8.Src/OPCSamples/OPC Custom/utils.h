//
// UTILS.H
//
//		REVISION HISTORY
//			File created (EPD 7/16/99)
//


void DisplayError(LPCTSTR, HRESULT);
void DisplayDataFromVariant(VARIANT*);
void DisplaySystemError(LPCTSTR, HRESULT);
void DisplayGroupData( DWORD, OPCHANDLE, DWORD,
					  OPCHANDLE *, VARIANT *, WORD *);
void DisplayGroupTimeStampData(DWORD, OPCHANDLE, DWORD, OPCHANDLE*, VARIANT *, WORD *,
                               FILETIME *);
void UnMarshalDataToVariant( VARIANT *vp, char * src);