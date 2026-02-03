<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInstallApp
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtInstallerPath = New System.Windows.Forms.TextBox()
        Me.btnSaveToJson = New System.Windows.Forms.Button()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtDetectName = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(70, 63)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Browse"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtInstallerPath
        '
        Me.txtInstallerPath.Location = New System.Drawing.Point(70, 35)
        Me.txtInstallerPath.Name = "txtInstallerPath"
        Me.txtInstallerPath.Size = New System.Drawing.Size(405, 22)
        Me.txtInstallerPath.TabIndex = 1
        '
        'btnSaveToJson
        '
        Me.btnSaveToJson.Location = New System.Drawing.Point(400, 174)
        Me.btnSaveToJson.Name = "btnSaveToJson"
        Me.btnSaveToJson.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveToJson.TabIndex = 2
        Me.btnSaveToJson.Text = "Save"
        Me.btnSaveToJson.UseVisualStyleBackColor = True
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(70, 109)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(405, 22)
        Me.txtName.TabIndex = 3
        '
        'txtDetectName
        '
        Me.txtDetectName.Location = New System.Drawing.Point(70, 146)
        Me.txtDetectName.Name = "txtDetectName"
        Me.txtDetectName.Size = New System.Drawing.Size(405, 22)
        Me.txtDetectName.TabIndex = 4
        '
        'frmInstallApp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.txtDetectName)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.btnSaveToJson)
        Me.Controls.Add(Me.txtInstallerPath)
        Me.Controls.Add(Me.Button1)
        Me.Name = "frmInstallApp"
        Me.Text = "Install App"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents txtInstallerPath As TextBox
    Friend WithEvents btnSaveToJson As Button
    Friend WithEvents txtName As TextBox
    Friend WithEvents txtDetectName As TextBox
End Class
