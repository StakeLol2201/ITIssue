Public Class Page
    Public WithEvents otherTimer As New System.Windows.Forms.Timer()
    Dim elements As HtmlElementCollection
    Dim hilo As System.Threading.Thread
    Dim directory As String = My.Settings.fileDirectory
    Private Shared exitFlag As Boolean = False
    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Browser.Navigate("http://scotiabank.service-now.com/")
    End Sub
    Private Sub Browser_DocumentCompleted(sender As Object, e As EventArgs) Handles Browser.DocumentTitleChanged
        If Browser.DocumentTitle.Equals("Esta página no se puede mostrar") Then
            MessageBox.Show("La página sufrió una caída temporal", "El programa se cerrará de manera inmediata" _
                            , MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Application.Exit()
        ElseIf Browser.DocumentTitle.Equals("BNS Employee Login") Then
            If Browser.StatusText = "Finalizado" Then
                setTimer()
            End If
        End If
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Browser.Refresh()
    End Sub
    Private Sub _timer_Tick(sender As Object, e As EventArgs) Handles _timer.Tick
        For i = 0 To My.Settings.setTimer * 3
            elements = Browser.Document.All
            For Each elemento As HtmlElement In elements
                If elemento.GetAttribute("name") = "callback_0" Then
                    setData()
                ElseIf elemento.GetAttribute("ng-src") = "13bc78e26f99f200a51cb4ecbb3ee44c.iix" Then
                    doClick()
                ElseIf elemento.GetAttribute("id") = "sp_formfield_short_description" Then
                    If exitFlag = False Then
                        doForm()
                        exitFlag = True
                    End If
                End If
            Next
            i = i + 1
        Next
    End Sub
    Public Sub setTimer()
        _timer.Interval = 1000
        _timer.Enabled = True
        _timer.Start()
        Application.DoEvents()
    End Sub
    Public Sub setData()
        elements = Browser.Document.All
        For Each elemento As HtmlElement In elements
            If elemento.GetAttribute("name") = "callback_0" Then
                elemento.SetAttribute("value", My.Settings.scotiaID)
            End If
            If elemento.GetAttribute("name") = "callback_1" Then
                elemento.SetAttribute("value", My.Settings.passID)
            End If
            If elemento.GetAttribute("name") = "callback_2" Then
                elemento.InvokeMember("click")
                setTimer()
            End If
        Next
    End Sub
    Public Sub doClick()
        elements = Browser.Document.All
        For Each elemento As HtmlElement In elements
            If elemento.GetAttribute("ng-src") = "13bc78e26f99f200a51cb4ecbb3ee44c.iix" Then
                elemento.InvokeMember("Click")
                setTimer()
            End If
        Next
    End Sub
    Public Sub doForm()
        elements = Browser.Document.All
        'Primer Campo
        For i = 0 To 4
            SendKeys.Send("{TAB}")
        Next
        Clipboard.SetText(My.Settings.category)
        SendKeys.Send("^v")
        thirdTimer()
        _thirdTimer.Stop()
        SendKeys.Send("{ENTER}")
        'Segundo Campo 
        'SendKeys.Send("{TAB}")
        'Clipboard.SetText(My.Settings.subCategory)
        'SendKeys.Send("^v") 
        'thirdTimer() 
        '_thirdTimer.Stop() 
        'SendKeys.Send("{ENTER}") 
        'Tercer Campo
        'SendKeys.Send("{TAB}")
        'Clipboard.SetText(My.Settings.whoAffecting)
        'SendKeys.Send("^v")
        'thirdTimer()
        '_thirdTimer.Stop()
        'SendKeys.Send("{ENTER}")
        'CuartoCampo
        'SendKeys.Send("{TAB}")
        'Clipboard.SetText(My.Settings.enviroment)
        'SendKeys.Send("^v")
        'thirdTimer()
        '_thirdTimer.Stop()
        'SendKeys.Send("{ENTER}")
        'For Each elemento As HtmlElement In elements
        'If elemento.GetAttribute("name") = "short_description" Then
        'elemento.SetAttribute("value", "This is a short description")
        'thirdTimer()
        '_thirdTimer.Stop()
        'End If
        'If elemento.GetAttribute("name") = "description" Then
        'elemento.SetAttribute("value", "This is a description")
        'thirdTimer()
        '_thirdTimer.Stop()
        'End If
        'If elemento.GetAttribute("title") = "Add attachment" Then
        'elemento.InvokeMember("click")
        'secTimer()
        '_thirdTimer.Stop()
        'End If
        'Next
    End Sub
    Public Sub pasteDirectory()
        SendKeys.Send("^v")
        SendKeys.Send("{ENTER}")
        _secondTimer.Stop()
    End Sub
    Public Sub secTimer()
        _secondTimer.Interval = 1000
        _secondTimer.Enabled = True
        _secondTimer.Start()
        Application.DoEvents()
    End Sub
    Private Sub _secondTimer_Tick(sender As Object, e As EventArgs) Handles _secondTimer.Tick
        For i = 0 To My.Settings.setTimer
            i = i + 1
        Next
        Clipboard.SetText(directory)
        pasteDirectory()
    End Sub
    Public Sub thirdTimer()
        _thirdTimer.Interval = 1000
        _thirdTimer.Enabled = True
        _thirdTimer.Start()
        Application.DoEvents()
    End Sub
    Private Sub _thirdTimer_Tick(sender As Object, e As EventArgs) Handles _thirdTimer.Tick
        For i = 0 To My.Settings.setTimer
            i = i + 1
        Next
    End Sub
End Class