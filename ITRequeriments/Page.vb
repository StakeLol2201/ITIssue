Public Class Page
    Dim elements As HtmlElementCollection
    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Browser.Navigate("http://scotiabank.service-now.com/")
        While Browser.ReadyState = WebBrowserReadyState.Complete
            elements = Browser.Document.All
            For Each elemento As HtmlElement In elements
                If elemento.GetAttribute("name") = "callback_0" Then
                    elemento.SetAttribute("value", "s5145837")
                End If
                If elemento.GetAttribute("name") = "callback_1" Then
                    elemento.SetAttribute("value", "abcd.1234")
                End If
                If elemento.GetAttribute("name") = "callback_2" Then
                    elemento.InvokeMember("Click")
                End If
            Next
        End While
    End Sub

    'Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
    '   elements = Browser.Document.All
    'For Each elemento As HtmlElement In elements
    'If elemento.GetAttribute("name") = "callback_0" Then
    '           elemento.SetAttribute("value", "s5145837")
    'End If
    'If elemento.GetAttribute("name") = "callback_1" Then
    '           elemento.SetAttribute("value", "abcd.1234")
    'End If
    'If elemento.GetAttribute("name") = "callback_2" Then
    '           elemento.InvokeMember("click")
    'End If
    'Next
    'End Sub

    'Private Sub Browser_DocumentCompleted(sender As Object, e As EventArgs) Handles Browser.Navigated
    'If Browser.StatusText = "Loaded" Then
    '       MessageBox.Show("La pagina cargó")
    'End If
    'End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Browser.Refresh()
    End Sub

End Class