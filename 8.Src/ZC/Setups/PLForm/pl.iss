; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "���ܿ��Ƴ���"
#define MyAppVersion "1.0"
#define MyAppPublisher "PL"
#define MyAppURL "PL"
#define MyAppExeName "PLForm.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{0CC298D0-8CF1-4E32-A0DB-B67D40A1ACC6}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
OutputBaseFilename=PL_1.0.0.0
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\PLForm.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\DbNetLink.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\Microsoft.Data.ConnectionUI.Dialog.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\Microsoft.Data.ConnectionUI.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\NLog.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\OpcNetApi.Com.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\OpcNetApi.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\PL.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\PLC.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\RS.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\Xdgk.Common.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\Z.ExtensionMethods.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\log\log.txt"; DestDir: "{app}\log"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\address.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\address2.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "F:\ZCPROJECT\8.Src\ZC\PLForm\bin\Debug\PLForm.exe.config"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
