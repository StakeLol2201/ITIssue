﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Page
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Browser = New System.Windows.Forms.WebBrowser()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Browser
        '
        Me.Browser.Location = New System.Drawing.Point(12, 12)
        Me.Browser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.Browser.Name = "Browser"
        Me.Browser.Size = New System.Drawing.Size(836, 468)
        Me.Browser.TabIndex = 0
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(773, 491)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.Text = "Recargar"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Page
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(861, 526)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.Browser)
        Me.Name = "Page"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Browser As WebBrowser
    Friend WithEvents btnRefresh As Button
End Class
