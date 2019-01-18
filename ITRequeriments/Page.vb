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
        'If Browser.StatusText = "Loaded" Then
        '       MessageBox.Show("La pagina cargó")
        'End If
        If Browser.DocumentTitle.Equals("Esta página no se puede mostrar") Then
            MessageBox.Show("La página sufrió una caída temporal", "El programa se cerrará de manera inmediata" _
                            , MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Application.Exit()
        ElseIf Browser.DocumentTitle.Equals("BNS Employee Login") Then
            If Browser.ReadyState = WebBrowserReadyState.Interactive Then
                elements = Browser.Document.All
                If Browser.StatusText = "Finalizado" Then
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
                End If
            End If
        End If



    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Browser.Refresh()
    End Sub

    Public Function startPage()

    End Function

End Class