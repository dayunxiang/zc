RSLINX = "E:\PROGRAM FILES\ROCKWELL SOFTWARE\RSLINX"
CPPFLAGS = /nologo /MLd /W3 /Gm /GX /ZI /Od /D "WIN32" /D "DEBUG" /D "CONSOLE" /D "_WIN32_DCOM"
LINK32_FLAGS=kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib odbccp32.lib /nologo /subsystem:console /incremental:no /pdb:"$(OUTDIR)\ConnPt.pdb" /machine:I386 /out:"$(OUTDIR)\ConnPt.exe" 

connpt.exe:	connpt.obj utils.obj sink.obj
	link/debug connpt.obj utils.obj sink.obj

connpt.obj:	connpt.cpp utils.h sink.h
utils.obj:	utils.cpp utils.h
sink.obj:	sink.cpp sink.h

