Imports System.Timers
Public Class Page
    Dim elements As HtmlElementCollection = Nothing
    Dim elments As HtmlElementCollection
    Private directory As String = My.Settings.fileDirectory
    Private counter As Integer
    Private scounter As Integer
    Private tcounter As Integer
    Private _firstTimer As New Timer
    Private _secondTimer As New Timer
    Private _thirdTimer As New Timer
    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Browser.Navigate("http://scotiabank.service-now.com/")
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Try
            elements = Browser.Document.All
        Catch ex As InvalidCastException
        End Try
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
    Private Sub _firstTimer_Tick(sender As Object, e As ElapsedEventArgs)
        If counter >= (My.Settings.setTimer / 2) Then
            Try
                elements = Browser.Document.All
            Catch ex As InvalidCastException
            End Try
            If elements.Equals(Nothing) Then
            Else
                For Each elemento As HtmlElement In elements
                    If elemento.GetAttribute("name") = "callback_0" Then
                        counter = 0
                        _firstTimer.Enabled = False
                        setData()
                    ElseIf elemento.GetAttribute("ng-src") = "13bc78e26f99f200a51cb4ecbb3ee44c.iix" Then
                        counter = 0
                        _firstTimer.Enabled = False
                        doClick()
                    ElseIf elemento.GetAttribute("id").Contains("s2id_sp_formfield_curr_caller") Then
                        counter = 0
                        _firstTimer.Enabled = False
                        doForm()
                    End If
                Next
            End If
        Else
            counter = counter + 1
        End If
    End Sub
    Public Sub setTimer()
        counter = 0
        _firstTimer.Interval = 600
        AddHandler _firstTimer.Elapsed, AddressOf _firstTimer_Tick
        _firstTimer.AutoReset = True
        _firstTimer.Enabled = True
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
        Dim cnter As Integer = 0
        'Primer Campo
        For i As Integer = 0 To 3
            SendKeys.Send("{TAB}")
        Next
        SendKeys.Send("{ENTER}")
        Clipboard.SetText(My.Settings.category)
        SendKeys.Send("^v")
        thirdTimer()
        'Segundo Campo
        'SendKeys.Send("{ENTER}")
        'Clipboard.SetText(My.Settings.subCategory)
        'SendKeys.Send("^v")
        'thirdTimer()
        'Tercer Campo
        'SendKeys.Send("{TAB}")
        'SendKeys.Send("{ENTER}")
        'Tercer Campo
        'thirdTimer()
        'SendKeys.SendWait("{TAB}")
        'Clipboard.SetText(My.Settings.whoAffecting)
        'thirdTimer()
        'SendKeys.SendWait("^v")
        'thirdTimer()
        'SendKeys.SendWait("{ENTER}")
        'thirdTimer()
        'SendKeys.SendWait("{ENTER}")
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
        SendKeys.Send("^v")
        SendKeys.Send("{ENTER}")
    End Sub
    Public Sub secTimer()
        scounter = 0
        _secondTimer.Interval = 600
        AddHandler _secondTimer.Elapsed, AddressOf _secondTimer_Tick
        _secondTimer.Enabled = True
        _secondTimer.AutoReset = True
    End Sub
    Private Sub _secondTimer_Tick(sender As Object, e As ElapsedEventArgs)
        If scounter >= (My.Settings.setTimer / 2) Then
            scounter = 0
            _secondTimer.Enabled = False
            Clipboard.SetText(directory)
            pasteDirectory()
        Else
            scounter = scounter + 1
        End If
    End Sub
    Public Sub thirdTimer()
        tcounter = 0
        _thirdTimer.Interval = 600
        AddHandler _thirdTimer.Elapsed, AddressOf _thirdTimer_Tick
        _thirdTimer.AutoReset = True
        _thirdTimer.Enabled = True
    End Sub
    Private Sub _thirdTimer_Tick(sender As Object, e As ElapsedEventArgs)
        If tcounter >= My.Settings.setTimer Then
            SendKeys.Send("{ENTER}")
            SendKeys.Send("{TAB}")
            _thirdTimer.Enabled = False
            tcounter = 0
        Else
            tcounter = tcounter + 1
        End If
    End Sub
End Class