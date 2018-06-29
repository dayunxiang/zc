Imports System.Runtime.InteropServices

Public Class RSLinxNetOPCSampleDialog
    Inherits System.Windows.Forms.Form
    Private opccontroller As OPCControl
    Private lUpdate As Long
    Private m_request As Opc.IRequest
    Private m_handle As Long

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        Dim ret As Integer
        '1 - RPC_C_AUTHN_LEVEL_NONE
        '3 - RPC_C_IMP_LEVEL_IMPERSONATE
        ret = CoInitializeSecurity(0, -1, 0, 0, 1, 3, 0, 0, 0)

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents groupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents ASyncReadButton As System.Windows.Forms.Button
    Friend WithEvents ASyncWriteButton As System.Windows.Forms.Button
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents AdviseButton As System.Windows.Forms.Button
    Friend WithEvents SyncReadButton As System.Windows.Forms.Button
    Friend WithEvents SyncWriteButton As System.Windows.Forms.Button
    Friend WithEvents StatusBar As System.Windows.Forms.StatusBar
    Friend WithEvents OPCGroupDatagroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents UpdateCounttextBox As System.Windows.Forms.TextBox
    Friend WithEvents label9 As System.Windows.Forms.Label
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents OPCGroupgroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents UpdateRatetextBox As System.Windows.Forms.TextBox
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents AddItemButton As System.Windows.Forms.Button
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents OPCServergroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents VersionInfotextBox As System.Windows.Forms.TextBox
    Friend WithEvents VendorInfotextBox As System.Windows.Forms.TextBox
    Friend WithEvents MachineNameTxtBox As System.Windows.Forms.TextBox
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents ConnnectButton As System.Windows.Forms.Button
    Friend WithEvents QualitytextBox0 As System.Windows.Forms.TextBox
    Friend WithEvents ValuetextBox0 As System.Windows.Forms.TextBox
    Friend WithEvents ItemtextBox0 As System.Windows.Forms.TextBox
    Friend WithEvents TopictextBox0 As System.Windows.Forms.TextBox
    Friend WithEvents ItemtextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TopictextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ItemtextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TopictextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents ItemtextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TopictextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents QualitytextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ValuetextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents QualitytextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents ValuetextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents QualitytextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents ValuetextBox3 As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.groupBox4 = New System.Windows.Forms.GroupBox
        Me.ASyncReadButton = New System.Windows.Forms.Button
        Me.ASyncWriteButton = New System.Windows.Forms.Button
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.AdviseButton = New System.Windows.Forms.Button
        Me.SyncReadButton = New System.Windows.Forms.Button
        Me.SyncWriteButton = New System.Windows.Forms.Button
        Me.StatusBar = New System.Windows.Forms.StatusBar
        Me.OPCGroupDatagroupBox = New System.Windows.Forms.GroupBox
        Me.QualitytextBox3 = New System.Windows.Forms.TextBox
        Me.ValuetextBox3 = New System.Windows.Forms.TextBox
        Me.QualitytextBox2 = New System.Windows.Forms.TextBox
        Me.ValuetextBox2 = New System.Windows.Forms.TextBox
        Me.QualitytextBox1 = New System.Windows.Forms.TextBox
        Me.ValuetextBox1 = New System.Windows.Forms.TextBox
        Me.UpdateCounttextBox = New System.Windows.Forms.TextBox
        Me.label9 = New System.Windows.Forms.Label
        Me.QualitytextBox0 = New System.Windows.Forms.TextBox
        Me.ValuetextBox0 = New System.Windows.Forms.TextBox
        Me.label8 = New System.Windows.Forms.Label
        Me.label7 = New System.Windows.Forms.Label
        Me.OPCGroupgroupBox = New System.Windows.Forms.GroupBox
        Me.ItemtextBox3 = New System.Windows.Forms.TextBox
        Me.TopictextBox3 = New System.Windows.Forms.TextBox
        Me.ItemtextBox2 = New System.Windows.Forms.TextBox
        Me.TopictextBox2 = New System.Windows.Forms.TextBox
        Me.ItemtextBox1 = New System.Windows.Forms.TextBox
        Me.TopictextBox1 = New System.Windows.Forms.TextBox
        Me.UpdateRatetextBox = New System.Windows.Forms.TextBox
        Me.label6 = New System.Windows.Forms.Label
        Me.AddItemButton = New System.Windows.Forms.Button
        Me.ItemtextBox0 = New System.Windows.Forms.TextBox
        Me.TopictextBox0 = New System.Windows.Forms.TextBox
        Me.label5 = New System.Windows.Forms.Label
        Me.label4 = New System.Windows.Forms.Label
        Me.OPCServergroupBox = New System.Windows.Forms.GroupBox
        Me.VersionInfotextBox = New System.Windows.Forms.TextBox
        Me.VendorInfotextBox = New System.Windows.Forms.TextBox
        Me.MachineNameTxtBox = New System.Windows.Forms.TextBox
        Me.label3 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.ConnnectButton = New System.Windows.Forms.Button
        Me.groupBox4.SuspendLayout()
        Me.OPCGroupDatagroupBox.SuspendLayout()
        Me.OPCGroupgroupBox.SuspendLayout()
        Me.OPCServergroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupBox4
        '
        Me.groupBox4.Controls.Add(Me.ASyncReadButton)
        Me.groupBox4.Controls.Add(Me.ASyncWriteButton)
        Me.groupBox4.Controls.Add(Me.RefreshButton)
        Me.groupBox4.Controls.Add(Me.AdviseButton)
        Me.groupBox4.Controls.Add(Me.SyncReadButton)
        Me.groupBox4.Controls.Add(Me.SyncWriteButton)
        Me.groupBox4.Location = New System.Drawing.Point(8, 384)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Size = New System.Drawing.Size(616, 64)
        Me.groupBox4.TabIndex = 29
        Me.groupBox4.TabStop = False
        Me.groupBox4.Text = "OPC Group functions"
        '
        'ASyncReadButton
        '
        Me.ASyncReadButton.Enabled = False
        Me.ASyncReadButton.Location = New System.Drawing.Point(12, 24)
        Me.ASyncReadButton.Name = "ASyncReadButton"
        Me.ASyncReadButton.Size = New System.Drawing.Size(88, 24)
        Me.ASyncReadButton.TabIndex = 24
        Me.ASyncReadButton.Text = "Async Read"
        '
        'ASyncWriteButton
        '
        Me.ASyncWriteButton.Enabled = False
        Me.ASyncWriteButton.Location = New System.Drawing.Point(112, 24)
        Me.ASyncWriteButton.Name = "ASyncWriteButton"
        Me.ASyncWriteButton.Size = New System.Drawing.Size(88, 24)
        Me.ASyncWriteButton.TabIndex = 26
        Me.ASyncWriteButton.Text = "Async Write"
        '
        'RefreshButton
        '
        Me.RefreshButton.Enabled = False
        Me.RefreshButton.Location = New System.Drawing.Point(212, 24)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(88, 24)
        Me.RefreshButton.TabIndex = 27
        Me.RefreshButton.Text = "Refresh"
        '
        'AdviseButton
        '
        Me.AdviseButton.Enabled = False
        Me.AdviseButton.Location = New System.Drawing.Point(312, 24)
        Me.AdviseButton.Name = "AdviseButton"
        Me.AdviseButton.Size = New System.Drawing.Size(88, 24)
        Me.AdviseButton.TabIndex = 28
        Me.AdviseButton.Text = "Advise"
        '
        'SyncReadButton
        '
        Me.SyncReadButton.Enabled = False
        Me.SyncReadButton.Location = New System.Drawing.Point(412, 24)
        Me.SyncReadButton.Name = "SyncReadButton"
        Me.SyncReadButton.Size = New System.Drawing.Size(88, 24)
        Me.SyncReadButton.TabIndex = 29
        Me.SyncReadButton.Text = "Sync Read"
        '
        'SyncWriteButton
        '
        Me.SyncWriteButton.Enabled = False
        Me.SyncWriteButton.Location = New System.Drawing.Point(512, 24)
        Me.SyncWriteButton.Name = "SyncWriteButton"
        Me.SyncWriteButton.Size = New System.Drawing.Size(88, 24)
        Me.SyncWriteButton.TabIndex = 30
        Me.SyncWriteButton.Text = "Sync Write"
        '
        'StatusBar
        '
        Me.StatusBar.Location = New System.Drawing.Point(0, 471)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(632, 24)
        Me.StatusBar.TabIndex = 26
        '
        'OPCGroupDatagroupBox
        '
        Me.OPCGroupDatagroupBox.Controls.Add(Me.QualitytextBox3)
        Me.OPCGroupDatagroupBox.Controls.Add(Me.ValuetextBox3)
        Me.OPCGroupDatagroupBox.Controls.Add(Me.QualitytextBox2)
        Me.OPCGroupDatagroupBox.Controls.Add(Me.ValuetextBox2)
        Me.OPCGroupDatagroupBox.Controls.Add(Me.QualitytextBox1)
        Me.OPCGroupDatagroupBox.Controls.Add(Me.ValuetextBox1)
        Me.OPCGroupDatagroupBox.Controls.Add(Me.UpdateCounttextBox)
        Me.OPCGroupDatagroupBox.Controls.Add(Me.label9)
        Me.OPCGroupDatagroupBox.Controls.Add(Me.QualitytextBox0)
        Me.OPCGroupDatagroupBox.Controls.Add(Me.ValuetextBox0)
        Me.OPCGroupDatagroupBox.Controls.Add(Me.label8)
        Me.OPCGroupDatagroupBox.Controls.Add(Me.label7)
        Me.OPCGroupDatagroupBox.Location = New System.Drawing.Point(320, 96)
        Me.OPCGroupDatagroupBox.Name = "OPCGroupDatagroupBox"
        Me.OPCGroupDatagroupBox.Size = New System.Drawing.Size(304, 280)
        Me.OPCGroupDatagroupBox.TabIndex = 28
        Me.OPCGroupDatagroupBox.TabStop = False
        Me.OPCGroupDatagroupBox.Text = "OPC Group Data"
        '
        'QualitytextBox3
        '
        Me.QualitytextBox3.Location = New System.Drawing.Point(160, 136)
        Me.QualitytextBox3.Name = "QualitytextBox3"
        Me.QualitytextBox3.Size = New System.Drawing.Size(120, 21)
        Me.QualitytextBox3.TabIndex = 29
        Me.QualitytextBox3.Text = ""
        '
        'ValuetextBox3
        '
        Me.ValuetextBox3.Location = New System.Drawing.Point(8, 136)
        Me.ValuetextBox3.Name = "ValuetextBox3"
        Me.ValuetextBox3.Size = New System.Drawing.Size(120, 21)
        Me.ValuetextBox3.TabIndex = 28
        Me.ValuetextBox3.Text = ""
        '
        'QualitytextBox2
        '
        Me.QualitytextBox2.Location = New System.Drawing.Point(160, 104)
        Me.QualitytextBox2.Name = "QualitytextBox2"
        Me.QualitytextBox2.Size = New System.Drawing.Size(120, 21)
        Me.QualitytextBox2.TabIndex = 27
        Me.QualitytextBox2.Text = ""
        '
        'ValuetextBox2
        '
        Me.ValuetextBox2.Location = New System.Drawing.Point(8, 104)
        Me.ValuetextBox2.Name = "ValuetextBox2"
        Me.ValuetextBox2.Size = New System.Drawing.Size(120, 21)
        Me.ValuetextBox2.TabIndex = 26
        Me.ValuetextBox2.Text = ""
        '
        'QualitytextBox1
        '
        Me.QualitytextBox1.Location = New System.Drawing.Point(160, 72)
        Me.QualitytextBox1.Name = "QualitytextBox1"
        Me.QualitytextBox1.Size = New System.Drawing.Size(120, 21)
        Me.QualitytextBox1.TabIndex = 25
        Me.QualitytextBox1.Text = ""
        '
        'ValuetextBox1
        '
        Me.ValuetextBox1.Location = New System.Drawing.Point(8, 72)
        Me.ValuetextBox1.Name = "ValuetextBox1"
        Me.ValuetextBox1.Size = New System.Drawing.Size(120, 21)
        Me.ValuetextBox1.TabIndex = 24
        Me.ValuetextBox1.Text = ""
        '
        'UpdateCounttextBox
        '
        Me.UpdateCounttextBox.Location = New System.Drawing.Point(160, 248)
        Me.UpdateCounttextBox.Name = "UpdateCounttextBox"
        Me.UpdateCounttextBox.ReadOnly = True
        Me.UpdateCounttextBox.Size = New System.Drawing.Size(120, 21)
        Me.UpdateCounttextBox.TabIndex = 23
        Me.UpdateCounttextBox.Text = ""
        '
        'label9
        '
        Me.label9.Location = New System.Drawing.Point(8, 248)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(88, 16)
        Me.label9.TabIndex = 22
        Me.label9.Text = "Total Update:"
        '
        'QualitytextBox0
        '
        Me.QualitytextBox0.Location = New System.Drawing.Point(160, 40)
        Me.QualitytextBox0.Name = "QualitytextBox0"
        Me.QualitytextBox0.Size = New System.Drawing.Size(120, 21)
        Me.QualitytextBox0.TabIndex = 21
        Me.QualitytextBox0.Text = ""
        '
        'ValuetextBox0
        '
        Me.ValuetextBox0.Location = New System.Drawing.Point(8, 40)
        Me.ValuetextBox0.Name = "ValuetextBox0"
        Me.ValuetextBox0.Size = New System.Drawing.Size(120, 21)
        Me.ValuetextBox0.TabIndex = 19
        Me.ValuetextBox0.Text = ""
        '
        'label8
        '
        Me.label8.Location = New System.Drawing.Point(160, 24)
        Me.label8.Name = "label8"
        Me.label8.TabIndex = 20
        Me.label8.Text = "Quality"
        '
        'label7
        '
        Me.label7.Location = New System.Drawing.Point(8, 24)
        Me.label7.Name = "label7"
        Me.label7.TabIndex = 18
        Me.label7.Text = "Value"
        '
        'OPCGroupgroupBox
        '
        Me.OPCGroupgroupBox.Controls.Add(Me.ItemtextBox3)
        Me.OPCGroupgroupBox.Controls.Add(Me.TopictextBox3)
        Me.OPCGroupgroupBox.Controls.Add(Me.ItemtextBox2)
        Me.OPCGroupgroupBox.Controls.Add(Me.TopictextBox2)
        Me.OPCGroupgroupBox.Controls.Add(Me.ItemtextBox1)
        Me.OPCGroupgroupBox.Controls.Add(Me.TopictextBox1)
        Me.OPCGroupgroupBox.Controls.Add(Me.UpdateRatetextBox)
        Me.OPCGroupgroupBox.Controls.Add(Me.label6)
        Me.OPCGroupgroupBox.Controls.Add(Me.AddItemButton)
        Me.OPCGroupgroupBox.Controls.Add(Me.ItemtextBox0)
        Me.OPCGroupgroupBox.Controls.Add(Me.TopictextBox0)
        Me.OPCGroupgroupBox.Controls.Add(Me.label5)
        Me.OPCGroupgroupBox.Controls.Add(Me.label4)
        Me.OPCGroupgroupBox.Location = New System.Drawing.Point(8, 96)
        Me.OPCGroupgroupBox.Name = "OPCGroupgroupBox"
        Me.OPCGroupgroupBox.Size = New System.Drawing.Size(304, 280)
        Me.OPCGroupgroupBox.TabIndex = 27
        Me.OPCGroupgroupBox.TabStop = False
        Me.OPCGroupgroupBox.Text = "OPC Group Configuration"
        '
        'ItemtextBox3
        '
        Me.ItemtextBox3.Location = New System.Drawing.Point(136, 136)
        Me.ItemtextBox3.Name = "ItemtextBox3"
        Me.ItemtextBox3.Size = New System.Drawing.Size(120, 21)
        Me.ItemtextBox3.TabIndex = 24
        Me.ItemtextBox3.Text = ""
        '
        'TopictextBox3
        '
        Me.TopictextBox3.Location = New System.Drawing.Point(8, 136)
        Me.TopictextBox3.Name = "TopictextBox3"
        Me.TopictextBox3.Size = New System.Drawing.Size(120, 21)
        Me.TopictextBox3.TabIndex = 23
        Me.TopictextBox3.Text = ""
        '
        'ItemtextBox2
        '
        Me.ItemtextBox2.Location = New System.Drawing.Point(136, 104)
        Me.ItemtextBox2.Name = "ItemtextBox2"
        Me.ItemtextBox2.Size = New System.Drawing.Size(120, 21)
        Me.ItemtextBox2.TabIndex = 21
        Me.ItemtextBox2.Text = ""
        '
        'TopictextBox2
        '
        Me.TopictextBox2.Location = New System.Drawing.Point(8, 104)
        Me.TopictextBox2.Name = "TopictextBox2"
        Me.TopictextBox2.Size = New System.Drawing.Size(120, 21)
        Me.TopictextBox2.TabIndex = 20
        Me.TopictextBox2.Text = ""
        '
        'ItemtextBox1
        '
        Me.ItemtextBox1.Location = New System.Drawing.Point(136, 72)
        Me.ItemtextBox1.Name = "ItemtextBox1"
        Me.ItemtextBox1.Size = New System.Drawing.Size(120, 21)
        Me.ItemtextBox1.TabIndex = 18
        Me.ItemtextBox1.Text = ""
        '
        'TopictextBox1
        '
        Me.TopictextBox1.Location = New System.Drawing.Point(8, 72)
        Me.TopictextBox1.Name = "TopictextBox1"
        Me.TopictextBox1.Size = New System.Drawing.Size(120, 21)
        Me.TopictextBox1.TabIndex = 17
        Me.TopictextBox1.Text = ""
        '
        'UpdateRatetextBox
        '
        Me.UpdateRatetextBox.Location = New System.Drawing.Point(136, 248)
        Me.UpdateRatetextBox.Name = "UpdateRatetextBox"
        Me.UpdateRatetextBox.Size = New System.Drawing.Size(120, 21)
        Me.UpdateRatetextBox.TabIndex = 16
        Me.UpdateRatetextBox.Text = ""
        '
        'label6
        '
        Me.label6.Location = New System.Drawing.Point(136, 232)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(112, 23)
        Me.label6.TabIndex = 15
        Me.label6.Text = "Update Rate (msec)"
        '
        'AddItemButton
        '
        Me.AddItemButton.Enabled = False
        Me.AddItemButton.Location = New System.Drawing.Point(8, 248)
        Me.AddItemButton.Name = "AddItemButton"
        Me.AddItemButton.TabIndex = 14
        Me.AddItemButton.Text = "Add Items"
        '
        'ItemtextBox0
        '
        Me.ItemtextBox0.Location = New System.Drawing.Point(136, 40)
        Me.ItemtextBox0.Name = "ItemtextBox0"
        Me.ItemtextBox0.Size = New System.Drawing.Size(120, 21)
        Me.ItemtextBox0.TabIndex = 12
        Me.ItemtextBox0.Text = ""
        '
        'TopictextBox0
        '
        Me.TopictextBox0.Location = New System.Drawing.Point(8, 40)
        Me.TopictextBox0.Name = "TopictextBox0"
        Me.TopictextBox0.Size = New System.Drawing.Size(120, 21)
        Me.TopictextBox0.TabIndex = 10
        Me.TopictextBox0.Text = ""
        '
        'label5
        '
        Me.label5.Location = New System.Drawing.Point(136, 24)
        Me.label5.Name = "label5"
        Me.label5.TabIndex = 11
        Me.label5.Text = "Item:"
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(8, 24)
        Me.label4.Name = "label4"
        Me.label4.TabIndex = 9
        Me.label4.Text = "Topic:"
        '
        'OPCServergroupBox
        '
        Me.OPCServergroupBox.Controls.Add(Me.VersionInfotextBox)
        Me.OPCServergroupBox.Controls.Add(Me.VendorInfotextBox)
        Me.OPCServergroupBox.Controls.Add(Me.MachineNameTxtBox)
        Me.OPCServergroupBox.Controls.Add(Me.label3)
        Me.OPCServergroupBox.Controls.Add(Me.label2)
        Me.OPCServergroupBox.Controls.Add(Me.label1)
        Me.OPCServergroupBox.Controls.Add(Me.ConnnectButton)
        Me.OPCServergroupBox.Location = New System.Drawing.Point(8, 8)
        Me.OPCServergroupBox.Name = "OPCServergroupBox"
        Me.OPCServergroupBox.Size = New System.Drawing.Size(616, 80)
        Me.OPCServergroupBox.TabIndex = 25
        Me.OPCServergroupBox.TabStop = False
        Me.OPCServergroupBox.Text = "OPC Server functions"
        '
        'VersionInfotextBox
        '
        Me.VersionInfotextBox.Location = New System.Drawing.Point(472, 40)
        Me.VersionInfotextBox.Name = "VersionInfotextBox"
        Me.VersionInfotextBox.ReadOnly = True
        Me.VersionInfotextBox.Size = New System.Drawing.Size(104, 21)
        Me.VersionInfotextBox.TabIndex = 7
        Me.VersionInfotextBox.Text = ""
        '
        'VendorInfotextBox
        '
        Me.VendorInfotextBox.Location = New System.Drawing.Point(256, 40)
        Me.VendorInfotextBox.Name = "VendorInfotextBox"
        Me.VendorInfotextBox.ReadOnly = True
        Me.VendorInfotextBox.Size = New System.Drawing.Size(200, 21)
        Me.VendorInfotextBox.TabIndex = 5
        Me.VendorInfotextBox.Text = ""
        '
        'MachineNameTxtBox
        '
        Me.MachineNameTxtBox.Location = New System.Drawing.Point(128, 40)
        Me.MachineNameTxtBox.Name = "MachineNameTxtBox"
        Me.MachineNameTxtBox.Size = New System.Drawing.Size(96, 21)
        Me.MachineNameTxtBox.TabIndex = 3
        Me.MachineNameTxtBox.Text = ""
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(472, 16)
        Me.label3.Name = "label3"
        Me.label3.TabIndex = 6
        Me.label3.Text = "Version Info:"
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(256, 16)
        Me.label2.Name = "label2"
        Me.label2.TabIndex = 4
        Me.label2.Text = "Vendor Info:"
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(128, 16)
        Me.label1.Name = "label1"
        Me.label1.TabIndex = 2
        Me.label1.Text = "Machine Name:"
        '
        'ConnnectButton
        '
        Me.ConnnectButton.Location = New System.Drawing.Point(16, 40)
        Me.ConnnectButton.Name = "ConnnectButton"
        Me.ConnnectButton.Size = New System.Drawing.Size(88, 24)
        Me.ConnnectButton.TabIndex = 1
        Me.ConnnectButton.Text = "Connect"
        '
        'RSLinxNetOPCSampleDialog
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(632, 495)
        Me.Controls.Add(Me.groupBox4)
        Me.Controls.Add(Me.StatusBar)
        Me.Controls.Add(Me.OPCGroupDatagroupBox)
        Me.Controls.Add(Me.OPCGroupgroupBox)
        Me.Controls.Add(Me.OPCServergroupBox)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "RSLinxNetOPCSampleDialog"
        Me.Text = "RSLinxNetOPCSampleDialog"
        Me.groupBox4.ResumeLayout(False)
        Me.OPCGroupDatagroupBox.ResumeLayout(False)
        Me.OPCGroupgroupBox.ResumeLayout(False)
        Me.OPCServergroupBox.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    '
    ' Connect the server, Local/Remote, enable the relative controls
    '
    Private Sub ConnnectButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnnectButton.Click
        Dim strMachineName As String = MachineNameTxtBox.Text

        Cursor = Cursors.WaitCursor
        opccontroller.ConnectDisconnect(strMachineName)
        Cursor = Cursors.Default

        If opccontroller.GetServerConnestStatus() = True Then
            ' Server connnected
            Dim m_subscription As Opc.Da.Subscription = opccontroller.GetSubscription()

            AddHandler m_subscription.DataChanged, AddressOf OnDataChange

            ' Set button captions to default state
            ConnnectButton.Text = "Disonnect"
            StatusBar.Text = "Connected to server"

            VendorInfotextBox.Text = opccontroller.GetVendorInfo()
            VersionInfotextBox.Text = opccontroller.GetVersionInfo()
            StatusBar.Text = opccontroller.GetStatusInfo()

            UpdateRatetextBox.Text = "1000"

            AddItemButton.Enabled = True
        Else
            ' server disconnected
            MachineNameTxtBox.Text = ""
            VendorInfotextBox.Text = ""
            VersionInfotextBox.Text = ""

            ' Set button captions to default state
            ConnnectButton.Text = "Connect"
            StatusBar.Text = "Disconnected from server"
            AdviseButton.Text = "Advise"

            AddItemButton.Enabled = False
            RefreshButton.Enabled = False
            AdviseButton.Enabled = False
            ASyncReadButton.Enabled = False
            ASyncWriteButton.Enabled = False
            SyncReadButton.Enabled = False
            SyncWriteButton.Enabled = False
        End If
    End Sub

    '
    ' Use control's name and index to enable the specified control
    '
    Private Sub EnableGroupControls(ByVal control_name As String, ByVal count As Long, ByVal enable As Boolean)
        For i As Long = 0 To count - 1 Step 1
            Dim cc As Control = FindControlByName(control_name, CStr(i), Me)
            cc.Enabled = enable
        Next
    End Sub

    '
    ' Use control's name and index to set the text
    '
    Private Sub TextGroupControls(ByVal control_name As String, ByVal count As Long, ByVal txt As String)
        For i As Long = 0 To count - 1 Step 1
            FindControlByName(control_name, CStr(i), Me).Text = txt
        Next
    End Sub

    '
    ' Add the items into the OPC Server(If the topic name and item name are not NULL).
    '
    Private Sub AddItemButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddItemButton.Click
        ' Disable all buttons
        RefreshButton.Enabled = False
        AdviseButton.Enabled = False
        ASyncReadButton.Enabled = False
        ASyncWriteButton.Enabled = False
        SyncReadButton.Enabled = False
        SyncWriteButton.Enabled = False

        TextGroupControls("ValuetextBox", 4, "")
        TextGroupControls("QualitytextBox", 4, "")
        StatusBar.Text = "Adding OPC items ..."

        Dim itemsList As ArrayList = New ArrayList
        PrepareAddItemList(itemsList)
        If opccontroller.AddItem(itemsList, UpdateRatetextBox.Text) = True Then
            AdviseButton.Enabled = True
            ASyncReadButton.Enabled = True
            ASyncWriteButton.Enabled = True
            SyncReadButton.Enabled = True
            SyncWriteButton.Enabled = True

            AdviseButton.Text = "Advise"
            opccontroller.ResetAdviseStatus()
            lUpdate = 0

            StatusBar.Text = "OPC Items added successfully."
        End If
    End Sub


    '
    ' Read the data from OPC Server in Synchronize mode.
    '
    Private Sub SyncReadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SyncReadButton.Click
        StatusBar.Text = "OPC Group Sync Read in progress ..."

        Dim results() As Opc.Da.ItemValueResult
        If (opccontroller.SyncRead(results) = True) Then
            StatusBar.Text = "OPC Group Sync Read complete."
            WriteControlValues(results)
        Else
            StatusBar.Text = "OPC Group Sync Read fail."
        End If
    End Sub


    '
    ' Write the data to OPC Server under Synchronize mode.
    '
    Private Sub SyncWriteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SyncWriteButton.Click
        StatusBar.Text = "OPC Group Sync Write in progress ..."
        Dim itemsList As ArrayList = New ArrayList
        PrepareControlValueItemList(itemsList)

        If (opccontroller.SyncWrite(itemsList) = True) Then
            StatusBar.Text = "OPC Group Sync Write complete."
        Else
            StatusBar.Text = "OPC Group Sync Write fail."
        End If
    End Sub


    '
    ' Read the data to OPC Server in Asynchronize mode.
    '
    Private Sub ASyncReadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ASyncReadButton.Click
        Try
            m_request = Nothing
            m_handle = 0

            Dim m_subscription As Opc.Da.Subscription = opccontroller.GetSubscription()
            If Not IsNothing(m_subscription) Then
                ' Begin the asynchronous read request.
                If Not IsNothing(m_subscription.Items) Then
                    StatusBar.Text = "OPC Group Async Read in progress ..."
                    Dim rceh As Opc.Da.ReadCompleteEventHandler
                    rceh = AddressOf OnReadComplete
                    m_subscription.Read(m_subscription.Items, ++m_handle, rceh, m_request)
                End If
            End If

            ' Update controls if request successful.
            If Not IsNothing(m_request) Then
                ASyncReadButton.Enabled = False
            End If
        Catch em As Exception
            MessageBox.Show(em.Message)
        End Try
    End Sub

    '
    ' Write the data to OPC Server under Asynchronize mode.
    '
    Private Sub ASyncWriteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ASyncWriteButton.Click
        Try
            Dim itemsList As ArrayList = New ArrayList
            Dim m_subscription As Opc.Da.Subscription = opccontroller.GetSubscription()
            PrepareControlValueItemList(itemsList)

            ' Convert to array of item objects.
            Dim itemsvalue() As Opc.Da.ItemValue = CType(itemsList.ToArray(GetType(Opc.Da.ItemValue)), Opc.Da.ItemValue())

            m_request = Nothing
            m_handle = 0

            If Not IsNothing(m_subscription) Then
                ' Begin the asynchronous read request.
                If Not IsNothing(itemsvalue) Then
                    StatusBar.Text = "OPC Group Async Write operation in progress ..."
                    m_subscription.Write(itemsvalue, ++m_handle, AddressOf OnWriteComplete, m_request)
                End If
            End If

            ' Update controls if request successful.
            If Not IsNothing(m_request) Then
                ASyncWriteButton.Enabled = False
            End If
        Catch em As Exception
            MessageBox.Show(em.Message)
        End Try
    End Sub


    '
    ' The event handler to recieve the returned results from OPC Server's read acton.
    '
    Private Sub OnReadComplete(ByVal clientHandle As Object, ByVal results() As Opc.Da.ItemValueResult)
        Try
            If Not m_handle.Equals(clientHandle) Then
                Return
            End If

            ' Save results.
            m_request = Nothing
            ' Check if there is nothing to do.
            If (IsNothing(results) Or results.Length = 0) Then
                Return
            End If

            WriteControlValues(results)

            ASyncReadButton.Enabled = True
            StatusBar.Text = "OPC Group Async Read operation is complete."
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub


    '
    ' The event handler to recieve the returned results from the Write action.
    '
    Private Sub OnWriteComplete(ByVal clientHandle As Object, ByVal results() As Opc.IdentifiedResult)
        If Not m_handle.Equals(clientHandle) Then
            Return
        End If

        m_request = Nothing
        ASyncWriteButton.Enabled = True
        StatusBar.Text = "OPC Group Async Write operation is complete."
    End Sub

    '
    ' Write the values into the control for the read action, using their name as the identifier.
    '
    Private Sub WriteControlValues(ByVal values() As Opc.Da.ItemValueResult)
        Dim start_pos As Long = 0
        Dim subs As Opc.Da.Subscription = opccontroller.GetSubscription()
        For Each result As Opc.Da.ItemValueResult In values
            For idx As Long = start_pos To 3 Step 1
                Dim txtTopic As TextBox = FindControlByName("TopictextBox", CStr(idx), Me)
                Dim txtItem As TextBox = FindControlByName("ItemtextBox", CStr(idx), Me)
                Dim strTmpItemName As String = ("[" + Trim(txtTopic.Text) + "]" + Trim(txtItem.Text))
                If strTmpItemName = result.ItemName Then
                    FindControlByName("ValuetextBox", CStr(idx), Me).Text = result.Value
                    FindControlByName("QualitytextBox", CStr(idx), Me).Text = opccontroller.resultToString(result)
                    Exit For
                End If
            Next
        Next
    End Sub

    '
    ' Build up "itemsList" for the write action, using their name as the identifier.
    '
    Private Sub PrepareControlValueItemList(ByRef itemsList As ArrayList)
        Dim m_subscription As Opc.Da.Subscription = opccontroller.GetSubscription()

        Dim start_pos As Long = 0
        For Each item1 As Opc.Da.Item In m_subscription.Items
            For idx As Long = start_pos To 3 Step 1
                Dim txtTopic As TextBox = FindControlByName("TopictextBox", CStr(idx), Me)
                Dim txtItem As TextBox = FindControlByName("ItemtextBox", CStr(idx), Me)
                Dim strTmpItemName As String = ("[" + Trim(txtTopic.Text) + "]" + Trim(txtItem.Text))
                If strTmpItemName = item1.ItemName Then
                    Dim itemToWrite As Opc.Da.ItemValue
                    itemToWrite = New Opc.Da.ItemValue(item1)
                    itemToWrite.Value = FindControlByName("ValuetextBox", CStr(idx), Me).Text
                    itemsList.Add(itemToWrite)
                    start_pos += 1
                End If
            Next
        Next
    End Sub

    '
    ' Build up "itemsList" for the additem action.
    '
    Private Sub PrepareAddItemList(ByRef itemsList As ArrayList)
        Dim itemToAdd As Opc.Da.Item
        Dim cur_itemIdentifier As Opc.ItemIdentifier

        Dim start_pos As Long = 0
        For idx As Long = start_pos To 3 Step 1
            Dim txtTopic As TextBox = FindControlByName("TopictextBox", CStr(idx), Me)
            Dim txtItem As TextBox = FindControlByName("ItemtextBox", CStr(idx), Me)

            Dim strTmpItemName As String = ("[" + Trim(txtTopic.Text) + "]" + Trim(txtItem.Text))

            cur_itemIdentifier = New Opc.ItemIdentifier(Nothing, strTmpItemName)
            itemToAdd = New Opc.Da.Item(cur_itemIdentifier)

            itemToAdd.ReqType = Nothing

            itemToAdd.MaxAge = 0
            itemToAdd.MaxAgeSpecified = False

            itemToAdd.Active = False
            itemToAdd.ActiveSpecified = False

            itemToAdd.Deadband = 0
            itemToAdd.DeadbandSpecified = False

            itemToAdd.SamplingRate = 0
            itemToAdd.SamplingRateSpecified = False

            itemToAdd.EnableBuffering = False
            itemToAdd.EnableBufferingSpecified = False

            itemsList.Add(itemToAdd)
        Next
    End Sub

    '
    ' The event handler to recieve the returned results from the Advise action.
    '
    Private Sub OnDataChange(ByVal subscriptionHandle As Object, ByVal requestHandle As Object, ByVal values() As Opc.Da.ItemValueResult)
        Try
            ' Do nothing more if only a keep alive callback.
            If IsNothing(values) Or values.Length = 0 Then
                Return
            End If

            lUpdate += 1
            WriteControlValues(values)
            UpdateCounttextBox.Text = System.Convert.ToString(lUpdate)
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub

    '
    ' Advise the OPC Server into "auto" mode.
    '
    Private Sub AdviseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdviseButton.Click
        opccontroller.AdviseDeadvise()

        If True = opccontroller.GetAdviseStatus() Then
            AdviseButton.Text = "Deadvise"
            RefreshButton.Enabled = True
            StatusBar.Text = "Advise Started"
            lUpdate = 0
        Else
            AdviseButton.Text = "Advise"
            RefreshButton.Enabled = False
            StatusBar.Text = "Advise Stoped"
        End If
    End Sub

    '
    ' Refresh data under "auto" mode.
    '
    Private Sub RefreshButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshButton.Click
        opccontroller.Refresh()
    End Sub

    '
    ' Assistant function to get the control handle according to it's name and index.
    '
    Private Function FindControlByName(ByVal strmain As String, ByVal stridx As String, ByRef ctrl As Control) As Control
        Dim strName As String = strmain & stridx
        Dim rnControl As Control
        For Each cc As Control In ctrl.Controls
            If cc.Name = strName Then
                Return cc
            End If
            If Not IsNothing(cc.Controls) Then
                If cc.Controls.Count > 0 Then
                    rnControl = FindControlByName(strmain, stridx, cc)
                    If (Not IsNothing(rnControl)) Then
                        Return rnControl
                    End If
                End If
            End If
        Next
        Return Nothing
    End Function

    '
    ' Load the dialog.
    '
    Private Sub RSLinxNetOPCSampleDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        opccontroller = New OPCControl
    End Sub
End Class

Module Hello
    '
    ' The main entrance.
    '
    <System.Runtime.Interopservices.DllImport("ole32.dll", EntryPoint:="CoInitializeSecurity")> _
    Public Function CoInitializeSecurity(ByVal psd As Integer, ByVal cauthz As Integer, ByVal authzinfo As Integer, ByVal reserveval As Integer, ByVal authlevel As Integer, ByVal implevel As Integer, ByVal authlist As Integer, ByVal cap As Integer, ByVal resev3 As Integer) As Integer
    End Function

    Public Sub Main()

        Dim dlg As RSLinxNetOPCSampleDialog
        dlg.ShowDialog()
    End Sub
End Module
