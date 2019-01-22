Public Class Page
    Dim elements As HtmlElementCollection
    Dim hilo As System.Threading.Thread
    Dim directory As String = My.Settings.fileDirectory
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
        Dim cant As Integer = 0
        Do
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
            cant = cant + 1
        Loop While cant = My.Settings.setTimer
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
        SendKeys.Send("{TAB} 5")
        SendKeys.Send("{ENTER}")
        Clipboard.SetText(My.Settings.category)
        SendKeys.Send("^V")
        thirdTimer()
        SendKeys.Send("{ENTER}")
        'Segundo Campo
        SendKeys.Send("{TAB}")
        SendKeys.Send("{ENTER}")
        Clipboard.SetText(My.Settings.subCategory)
        SendKeys.Send("^V")
        thirdTimer()
        SendKeys.Send("{ENTER}")
        'Tercer Campo
        SendKeys.Send("{TAB}")
        SendKeys.Send("{ENTER}")
        Clipboard.SetText(My.Settings.whoAffecting)
        SendKeys.Send("^V")
        thirdTimer()
        SendKeys.Send("{ENTER}")
        'CuartoCampo
        SendKeys.Send("{TAB}")
        SendKeys.Send("{ENTER}")
        Clipboard.SetText(My.Settings.enviroment)
        SendKeys.Send("^V")
        thirdTimer()
        SendKeys.Send("{ENTER}")
        For Each elemento As HtmlElement In elements
            If elemento.GetAttribute("name") = "short_description" Then
                elemento.SetAttribute("value", "This is a short description")
                thirdTimer()
            End If
            If elemento.GetAttribute("name") = "description" Then
                elemento.SetAttribute("value", "This is a description")
                thirdTimer()
            End If
            If elemento.GetAttribute("title") = "Add attachment" Then
                elemento.InvokeMember("click")
                secTimer()
            End If
        Next
    End Sub
    Public Sub pasteDirectory()
        SendKeys.Send("^(V)")
        SendKeys.Send("{ENTER}")
    End Sub
    Public Sub secTimer()
        _secondTimer.Interval = 1000
        _secondTimer.Enabled = True
        _secondTimer.Start()
        Application.DoEvents()
    End Sub
    Private Sub _secondTimer_Tick(sender As Object, e As EventArgs) Handles _secondTimer.Tick
        Dim i As Integer = 0
        Do
            i = i + 1
        Loop While i = (My.Settings.setTimer / 2)
        Clipboard.SetText(directory)
        _secondTimer.Stop()
        pasteDirectory()
    End Sub
    Public Sub thirdTimer()
        _thirdTimer.Interval = 1000
        _thirdTimer.Enabled = True
        _thirdTimer.Start()
        Application.DoEvents()
    End Sub
    Private Sub _thirdTimer_Tick(sender As Object, e As EventArgs) Handles _thirdTimer.Tick
        Dim i As Integer = 0
        Do
            i = i + 1
        Loop While i = (My.Settings.setTimer / 2)
        _thirdTimer.Stop()
    End Sub
End Class