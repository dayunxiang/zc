'
'
' Class OPCControl
'   Representing the character as OPCServer's container
'
'
Public Class OPCControl
    Private server As Opc.Server
    Private subscription As Opc.Da.Subscription
    Private m_items() As Opc.Da.ItemResult

    Private strVersionInfo As String
    Private strVendorInfo As String
    Private strStatusInfo As String

    Private fConnected As Boolean
    Private fAdvised As Boolean

    Public Sub OPCControl()
        fConnected = False
    End Sub

    Public Function GetSubscription() As Opc.Da.Subscription
        Return subscription
    End Function

    Public Function GetServerConnestStatus() As Boolean
        Return fConnected
    End Function

    Public Function GetServer() As Opc.Server
        Return server
    End Function

    Public Function GetAdviseStatus() As Boolean
        Return fAdvised
    End Function

    Public Sub ResetAdviseStatus()
        fAdvised = False
    End Sub

    Public Function GetVendorInfo() As String
        Return strVendorInfo
    End Function

    Public Function GetStatusInfo() As String
        Return strStatusInfo
    End Function

    Public Function GetVersionInfo() As String
        Return strVersionInfo
    End Function

    '
    ' Assitant function to translate the "result" into "Quality" string, "Good" or "Bad"
    '
    Public Shared Function resultToString(ByVal result As Opc.Da.ItemValueResult) As String
        Dim qb As Opc.Da.qualityBits
        If (result.QualitySpecified And result.Quality.QualityBits = Opc.Da.qualityBits.good) Then
            qb = result.Quality.QualityBits
        Else
            qb = Nothing
        End If
        Return Opc.Convert.ToString(qb)
    End Function

    '
    ' Create the server(Using the specified URL)
    '
    Public Function GetServerForURL(ByVal cur_url As Opc.URL) As Opc.Server
        If IsNothing(cur_url) Then
            Throw New ArgumentNullException("cur_url")
        End If

        If cur_url.Scheme = Opc.UrlScheme.DA Then
            Return New Opc.Da.Server(New OpcCom.Factory, cur_url)
        Else
            Return Nothing
        End If
    End Function

    '
    ' Connect/Disconnect to the server.
    '
    Public Function ConnectDisconnect(ByVal strMachine As String) As Boolean
        If fConnected = False Then
            ' Connect server
            Dim strServerDest As String
            If Trim(strMachine) <> "" Then
                strServerDest = "opcda://" & strMachine
                strServerDest += "/RSLinx Remote OPC Server"
            Else
                strServerDest = "opcda://localhost/RSLinx OPC Server"
            End If
            strServerDest += "/{a05bb6d5-2f8a-11d1-9bb0-080009d01446}"

            Dim cur_url As Opc.URL = New Opc.URL(strServerDest)
            server = GetServerForURL(cur_url)   ' Create an unconnected server object.

            ' Invoke the connect server callback.
            If Not IsNothing(server) Then
                Try
                    Dim credentials As System.Net.NetworkCredential
                    Dim m_proxy As System.Net.WebProxy
                    Dim cur_connectData As Opc.ConnectData
                    Dim m_server As Opc.Da.Server
                    Dim status As Opc.Da.ServerStatus

                    credentials = Nothing
                    m_proxy = Nothing

                    cur_connectData = New Opc.ConnectData(credentials, m_proxy)
                    server.Connect(cur_connectData)

                    m_server = server
                    status = m_server.GetStatus()

                    strVendorInfo = status.VendorInfo
                    strVersionInfo = status.ProductVersion
                    strStatusInfo = status.StatusInfo


                    ' Assign a globally unique handle to the subscription.
                    Dim state As Opc.Da.SubscriptionState = New Opc.Da.SubscriptionState

                    state.ClientHandle = Nothing
                    state.ServerHandle = Nothing
                    state.Name = "OPCSample"
                    state.Active = False
                    state.UpdateRate = CInt(1000)
                    state.KeepAlive = CInt(0)
                    state.Deadband = CDec(0)
                    state.Locale = Nothing

                    state.ClientHandle = Guid.NewGuid().ToString()

                    ' Create the subscription.
                    subscription = m_server.CreateSubscription(state)
                    fConnected = True
                Catch e As Exception
                    strStatusInfo = e.Message
                    MessageBox.Show(e.Message)
                End Try
            End If
        Else
            ' Disconnect server
            fConnected = False

            If Not IsNothing(server) Then
                Try
                    server.Disconnect()
                Catch
                End Try

                server.Dispose()
                server = Nothing
            End If
        End If
        Return True
    End Function

    '
    ' Add items (according to "itemsList" parameter) into OPC.Subscription
    '
    Public Function AddItem(ByVal itemsList As ArrayList, ByVal strUpdateRate As String) As Boolean
        Dim fAddItemSuccess As Boolean = False
        ' Set state tp inactive
        Try
            Dim state As Opc.Da.SubscriptionState = New Opc.Da.SubscriptionState

            ' Remove items
            If Not IsNothing(m_items) Then
                subscription.RemoveItems(m_items)
            End If

            state.Active = False
            subscription.ModifyState(CInt(Opc.Da.StateMask.Active), state)

            Dim iUpdateRate As Int16 = System.Convert.ToInt32(strUpdateRate)
            If iUpdateRate > 0 Then
                state.UpdateRate = iUpdateRate
            Else
                state.UpdateRate = 1000
            End If

            subscription.ModifyState(Opc.Da.StateMask.UpdateRate, state)

            ' Begin to add items
            Dim items() As Opc.Da.Item = CType(itemsList.ToArray(GetType(Opc.Da.Item)), Opc.Da.Item())
            For Each item As Opc.Da.Item In items
                item.ClientHandle = Guid.NewGuid().ToString()
            Next

            m_items = subscription.AddItems(items)

            fAdvised = False
            fAddItemSuccess = True
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try

        Return fAddItemSuccess
    End Function

    '
    ' Synchronize read from the OPC Server
    '
    Public Function SyncRead(ByRef results() As Opc.Da.ItemValueResult) As Boolean
        Try
            If IsNothing(subscription) Then Return False

            results = subscription.Read(subscription.Items)

            If IsNothing(results) Or results.Length = 0 Then
                Return False
            Else
                Return True
            End If
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Function

    '
    ' Synchronize write to the OPC Server
    '
    Public Overridable Function SyncWrite(ByRef itemsList As ArrayList) As Boolean
        Try
            If IsNothing(subscription) Then Return False

            Dim itemsvalue() As Opc.Da.ItemValue = _
                CType(itemsList.ToArray(GetType(Opc.Da.ItemValue)), Opc.Da.ItemValue())

            Dim results() As Opc.IdentifiedResult = subscription.Write(itemsvalue)
            Return True
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Function

    '
    ' Active/Deactive the OPC Server as the "auto" mode.
    '   When timer reaches, update the data from OPC server automatically.
    '
    Public Sub AdviseDeadvise()
        Try
            If IsNothing(subscription) Then Return

            fAdvised = Not fAdvised

            Dim state As Opc.Da.SubscriptionState = New Opc.Da.SubscriptionState
            state.Active = fAdvised
            subscription.ModifyState(Opc.Da.StateMask.Active, state)
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub

    '
    ' Refesh the data, only works in "auto" mode.
    '
    Public Sub Refresh()
        Try
            subscription.Refresh()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub
End Class
