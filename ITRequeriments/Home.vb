Public Class Home
    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            openFD.InitialDirectory = "c:/"
            openFD.DefaultExt = "*.xls"
            openFD.Filter = "Libro de Excel | *.xls"
            If openFD.ShowDialog = System.Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            Else
                txtFileDirectory.Text = openFD.FileName
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtFileDirectory_TextChanged(sender As Object, e As EventArgs) Handles txtFileDirectory.TextChanged
        If txtFileDirectory.Text <> "" Then
            btnConfirm.Enabled = True
        Else
            btnConfirm.Enabled = False
        End If
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Page.Show()
    End Sub
End Class
