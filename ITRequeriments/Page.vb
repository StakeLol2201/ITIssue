Public Class Page
    Dim elements As HtmlElementCollection
    Dim hilo As System.Threading.Thread
    Dim directory As String = My.Settings.fileDirectory
    Private counter As Integer
    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Browser.Navigate("http://scotiabank.service-now.com/")
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
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
            End If
        Next
        MessageBox.Show(Browser.ReadyState)
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
        If counter >= My.Settings.setTimer Then
            elements = Browser.Document.All
            For Each elemento As HtmlElement In elements
                If elemento.GetAttribute("name") = "callback_0" Then
                    _timer.Stop()
                    setData()
                ElseIf elemento.GetAttribute("ng-src") = "13bc78e26f99f200a51cb4ecbb3ee44c.iix" Then
                    _timer.Stop()
                    doClick()
                ElseIf elemento.GetAttribute("title").Contains("Click to Preview") Then
                    _timer.Stop()
                    doForm()
                End If
            Next
            counter = 0
            _timer.Enabled = False
        Else
            counter = counter + 1
        End If
    End Sub
    Public Sub setTimer()
        counter = 0
        _timer.Interval = 600
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
        For i As Integer = 0 To 3
            SendKeys.Send("{TAB}")
        Next
        'SendKeys.Send("{ENTER}")
        Clipboard.SetText(My.Settings.category)
        thirdTimer()
        SendKeys.SendWait("^v")
        thirdTimer()
        SendKeys.SendWait("{ENTER}")
        SendKeys.SendWait("{ENTER}")
        'Segundo Campo
        thirdTimer()
        SendKeys.SendWait("{TAB}")
        Clipboard.SetText(My.Settings.subCategory)
        thirdTimer()
        SendKeys.SendWait("^v")
        thirdTimer()
        SendKeys.SendWait("{ENTER}")
        SendKeys.SendWait("{ENTER}")
        'Tercer Campo
        thirdTimer()
        SendKeys.SendWait("{TAB}")
        Clipboard.SetText(My.Settings.whoAffecting)
        thirdTimer()
        SendKeys.SendWait("^v")
        thirdTimer()
        SendKeys.SendWait("{ENTER}")
        SendKeys.SendWait("{ENTER}")
        'CuartoCampo
        'SendKeys.Send("{TAB}")
        'SendKeys.Send("{ENTER}")
        'Clipboard.SetText(My.Settings.enviroment)
        'SendKeys.Send("^V")
        'thirdTimer()
        'SendKeys.Send("{ENTER}")
        'For Each elemento As HtmlElement In elements
        'If elemento.GetAttribute("name") = "short_description" Then
        'elemento.SetAttribute("value", "This is a short description")
        'thirdTimer()
        'End If
        'If elemento.GetAttribute("name") = "description" Then
        'elemento.SetAttribute("value", "This is a description")
        'thirdTimer()
        'End If
        'If elemento.GetAttribute("title") = "Add attachment" Then
        'elemento.InvokeMember("click")
        'secTimer()
        'End If
        'Next
    End Sub
    Public Sub pasteDirectory()
        SendKeys.Send("^(V)")
        SendKeys.Send("{ENTER}")
    End Sub
    Public Sub secTimer()
        counter = 0
        _secondTimer.Interval = 600
        _secondTimer.Enabled = True
        _secondTimer.Start()
        Application.DoEvents()
    End Sub
    Private Sub _secondTimer_Tick(sender As Object, e As EventArgs) Handles _secondTimer.Tick
        If counter >= (My.Settings.setTimer / 2) Then
            counter = 0
            _secondTimer.Enabled = False
            Clipboard.SetText(directory)
            pasteDirectory()
        Else
            counter = counter + 1
        End If
    End Sub
    Public Sub thirdTimer()
        counter = 0
        _thirdTimer.Interval = 500
        _thirdTimer.Enabled = True
        _thirdTimer.Start()
        Application.DoEvents()
    End Sub
    Private Sub _thirdTimer_Tick(sender As Object, e As EventArgs) Handles _thirdTimer.Tick
        If counter >= (My.Settings.setTimer / 4) Then
            _thirdTimer.Enabled = False
            counter = 0
        Else
            counter = counter + 1
        End If
    End Sub
End Class