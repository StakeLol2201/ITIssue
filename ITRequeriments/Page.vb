Public Class Page
    Dim elements As HtmlElementCollection
    Dim hilo As System.Threading.Thread
    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Browser.Navigate("http://scotiabank.service-now.com/")
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        elements = Browser.Document.All
        For Each elemento As HtmlElement In elements
            If elemento.GetAttribute("name") = "callback_0" Then
                elemento.SetAttribute("value", "s5145837")
            End If
            If elemento.GetAttribute("name") = "callback_1" Then
                elemento.SetAttribute("value", "abcd.1234")
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
        elements = Browser.Document.All
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
                ElseIf elemento.GetAttribute("name") = "category" Then
                    _timer.Stop()
                    doForm()
                End If
            Next
            cant = cant + 1
        Loop While cant = 20
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
                elemento.SetAttribute("value", "s5145837")
            End If
            If elemento.GetAttribute("name") = "callback_1" Then
                elemento.SetAttribute("value", "abcd.1234")
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
    Public Function doForm(sender As Object, e As EventArgs)
        elements = Browser.Document.All
        For Each elemento As HtmlElement In elements
            If elemento.GetAttribute("class") = "select2-input" _
                And elemento.GetAttribute("id") = "s2id_autogen7_search" Then
                elemento.SetAttribute("value", "Software")
                SendKeys.Send("{ENTER}")
            End If
            If elemento.GetAttribute("class") = "select2-input" _
                And elemento.GetAttribute("id") = "s2id_autogen8_search" Then

            End If
        Next
    End Function

End Class