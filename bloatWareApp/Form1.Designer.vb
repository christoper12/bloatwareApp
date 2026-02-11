<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnRefreshOptional = New System.Windows.Forms.Button()
        Me.dgOptional = New System.Windows.Forms.DataGridView()
        Me.btnClearOptional = New System.Windows.Forms.Button()
        Me.btnInstallUninstallOptional = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnRefeshDataEssential = New System.Windows.Forms.Button()
        Me.dgEsential = New System.Windows.Forms.DataGridView()
        Me.btnClearEsential = New System.Windows.Forms.Button()
        Me.btnInstall = New System.Windows.Forms.Button()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnRefreshDataBloatware = New System.Windows.Forms.Button()
        Me.dgUninstallList = New System.Windows.Forms.DataGridView()
        Me.btnDeleteUninstall = New System.Windows.Forms.Button()
        Me.btnUninstall = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnMarkToOptional = New System.Windows.Forms.Button()
        Me.btnMarkToEssential = New System.Windows.Forms.Button()
        Me.btnInstallApp = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnAddToUnintallList = New System.Windows.Forms.Button()
        Me.dgListInstall = New System.Windows.Forms.DataGridView()
        Me.cbList = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.StartupSettingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.dgOptional, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.dgEsential, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgUninstallList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgListInstall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(6, 19)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(198, 30)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "Scan Installed apps"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.MenuStrip1)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1444, 921)
        Me.Panel1.TabIndex = 1
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(7, 397)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1416, 323)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox5)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1408, 294)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Optional"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.btnRefreshOptional)
        Me.GroupBox5.Controls.Add(Me.dgOptional)
        Me.GroupBox5.Controls.Add(Me.btnClearOptional)
        Me.GroupBox5.Controls.Add(Me.btnInstallUninstallOptional)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(1396, 282)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        '
        'btnRefreshOptional
        '
        Me.btnRefreshOptional.Location = New System.Drawing.Point(252, 16)
        Me.btnRefreshOptional.Name = "btnRefreshOptional"
        Me.btnRefreshOptional.Size = New System.Drawing.Size(120, 30)
        Me.btnRefreshOptional.TabIndex = 4
        Me.btnRefreshOptional.Text = "Refresh Data"
        Me.btnRefreshOptional.UseVisualStyleBackColor = True
        '
        'dgOptional
        '
        Me.dgOptional.AllowUserToAddRows = False
        Me.dgOptional.AllowUserToDeleteRows = False
        Me.dgOptional.AllowUserToOrderColumns = True
        Me.dgOptional.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgOptional.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgOptional.Location = New System.Drawing.Point(6, 52)
        Me.dgOptional.Name = "dgOptional"
        Me.dgOptional.ReadOnly = True
        Me.dgOptional.RowHeadersWidth = 51
        Me.dgOptional.RowTemplate.Height = 24
        Me.dgOptional.Size = New System.Drawing.Size(1384, 224)
        Me.dgOptional.TabIndex = 2
        '
        'btnClearOptional
        '
        Me.btnClearOptional.Location = New System.Drawing.Point(87, 16)
        Me.btnClearOptional.Name = "btnClearOptional"
        Me.btnClearOptional.Size = New System.Drawing.Size(159, 30)
        Me.btnClearOptional.TabIndex = 3
        Me.btnClearOptional.Text = "Clear Optional List"
        Me.btnClearOptional.UseVisualStyleBackColor = True
        '
        'btnInstallUninstallOptional
        '
        Me.btnInstallUninstallOptional.Location = New System.Drawing.Point(6, 16)
        Me.btnInstallUninstallOptional.Name = "btnInstallUninstallOptional"
        Me.btnInstallUninstallOptional.Size = New System.Drawing.Size(75, 30)
        Me.btnInstallUninstallOptional.TabIndex = 2
        Me.btnInstallUninstallOptional.Text = "-"
        Me.btnInstallUninstallOptional.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1408, 296)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Essential"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.btnRefeshDataEssential)
        Me.GroupBox4.Controls.Add(Me.dgEsential)
        Me.GroupBox4.Controls.Add(Me.btnClearEsential)
        Me.GroupBox4.Controls.Add(Me.btnInstall)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(1396, 284)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        '
        'btnRefeshDataEssential
        '
        Me.btnRefeshDataEssential.Location = New System.Drawing.Point(252, 16)
        Me.btnRefeshDataEssential.Name = "btnRefeshDataEssential"
        Me.btnRefeshDataEssential.Size = New System.Drawing.Size(120, 30)
        Me.btnRefeshDataEssential.TabIndex = 5
        Me.btnRefeshDataEssential.Text = "Refresh Data"
        Me.btnRefeshDataEssential.UseVisualStyleBackColor = True
        '
        'dgEsential
        '
        Me.dgEsential.AllowUserToAddRows = False
        Me.dgEsential.AllowUserToDeleteRows = False
        Me.dgEsential.AllowUserToOrderColumns = True
        Me.dgEsential.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgEsential.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgEsential.Location = New System.Drawing.Point(6, 52)
        Me.dgEsential.Name = "dgEsential"
        Me.dgEsential.ReadOnly = True
        Me.dgEsential.RowHeadersWidth = 51
        Me.dgEsential.RowTemplate.Height = 24
        Me.dgEsential.Size = New System.Drawing.Size(1384, 226)
        Me.dgEsential.TabIndex = 2
        '
        'btnClearEsential
        '
        Me.btnClearEsential.Location = New System.Drawing.Point(87, 16)
        Me.btnClearEsential.Name = "btnClearEsential"
        Me.btnClearEsential.Size = New System.Drawing.Size(159, 30)
        Me.btnClearEsential.TabIndex = 3
        Me.btnClearEsential.Text = "Clear Esential List"
        Me.btnClearEsential.UseVisualStyleBackColor = True
        '
        'btnInstall
        '
        Me.btnInstall.Location = New System.Drawing.Point(6, 16)
        Me.btnInstall.Name = "btnInstall"
        Me.btnInstall.Size = New System.Drawing.Size(75, 30)
        Me.btnInstall.TabIndex = 2
        Me.btnInstall.Text = "Install"
        Me.btnInstall.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.GroupBox2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(1408, 296)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Bloatware"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.btnRefreshDataBloatware)
        Me.GroupBox2.Controls.Add(Me.dgUninstallList)
        Me.GroupBox2.Controls.Add(Me.btnDeleteUninstall)
        Me.GroupBox2.Controls.Add(Me.btnUninstall)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1396, 284)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'btnRefreshDataBloatware
        '
        Me.btnRefreshDataBloatware.Location = New System.Drawing.Point(252, 16)
        Me.btnRefreshDataBloatware.Name = "btnRefreshDataBloatware"
        Me.btnRefreshDataBloatware.Size = New System.Drawing.Size(120, 30)
        Me.btnRefreshDataBloatware.TabIndex = 6
        Me.btnRefreshDataBloatware.Text = "Refresh Data"
        Me.btnRefreshDataBloatware.UseVisualStyleBackColor = True
        '
        'dgUninstallList
        '
        Me.dgUninstallList.AllowUserToAddRows = False
        Me.dgUninstallList.AllowUserToDeleteRows = False
        Me.dgUninstallList.AllowUserToOrderColumns = True
        Me.dgUninstallList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgUninstallList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgUninstallList.Location = New System.Drawing.Point(6, 52)
        Me.dgUninstallList.Name = "dgUninstallList"
        Me.dgUninstallList.ReadOnly = True
        Me.dgUninstallList.RowHeadersWidth = 51
        Me.dgUninstallList.RowTemplate.Height = 24
        Me.dgUninstallList.Size = New System.Drawing.Size(1384, 226)
        Me.dgUninstallList.TabIndex = 2
        '
        'btnDeleteUninstall
        '
        Me.btnDeleteUninstall.Location = New System.Drawing.Point(87, 16)
        Me.btnDeleteUninstall.Name = "btnDeleteUninstall"
        Me.btnDeleteUninstall.Size = New System.Drawing.Size(159, 30)
        Me.btnDeleteUninstall.TabIndex = 3
        Me.btnDeleteUninstall.Text = "Clear Bloatware List"
        Me.btnDeleteUninstall.UseVisualStyleBackColor = True
        '
        'btnUninstall
        '
        Me.btnUninstall.Location = New System.Drawing.Point(6, 16)
        Me.btnUninstall.Name = "btnUninstall"
        Me.btnUninstall.Size = New System.Drawing.Size(75, 30)
        Me.btnUninstall.TabIndex = 2
        Me.btnUninstall.Text = "Uninstall"
        Me.btnUninstall.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.txtLog)
        Me.GroupBox3.Location = New System.Drawing.Point(7, 726)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1431, 190)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Log"
        '
        'txtLog
        '
        Me.txtLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLog.Location = New System.Drawing.Point(6, 21)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ReadOnly = True
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(1419, 163)
        Me.txtLog.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnMarkToOptional)
        Me.GroupBox1.Controls.Add(Me.btnMarkToEssential)
        Me.GroupBox1.Controls.Add(Me.btnInstallApp)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.btnAddToUnintallList)
        Me.GroupBox1.Controls.Add(Me.btnRefresh)
        Me.GroupBox1.Controls.Add(Me.dgListInstall)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 39)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1427, 352)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "List of installed apps"
        '
        'btnMarkToOptional
        '
        Me.btnMarkToOptional.Location = New System.Drawing.Point(209, 19)
        Me.btnMarkToOptional.Name = "btnMarkToOptional"
        Me.btnMarkToOptional.Size = New System.Drawing.Size(175, 30)
        Me.btnMarkToOptional.TabIndex = 7
        Me.btnMarkToOptional.Text = "Mark To Optional List"
        Me.btnMarkToOptional.UseVisualStyleBackColor = True
        '
        'btnMarkToEssential
        '
        Me.btnMarkToEssential.Location = New System.Drawing.Point(390, 19)
        Me.btnMarkToEssential.Name = "btnMarkToEssential"
        Me.btnMarkToEssential.Size = New System.Drawing.Size(175, 30)
        Me.btnMarkToEssential.TabIndex = 6
        Me.btnMarkToEssential.Text = "Mark To Essential List"
        Me.btnMarkToEssential.UseVisualStyleBackColor = True
        '
        'btnInstallApp
        '
        Me.btnInstallApp.Location = New System.Drawing.Point(1304, 67)
        Me.btnInstallApp.Name = "btnInstallApp"
        Me.btnInstallApp.Size = New System.Drawing.Size(98, 31)
        Me.btnInstallApp.TabIndex = 5
        Me.btnInstallApp.Text = "Install Apps"
        Me.btnInstallApp.UseVisualStyleBackColor = True
        Me.btnInstallApp.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(894, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Search App"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(978, 28)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(430, 22)
        Me.txtSearch.TabIndex = 3
        '
        'btnAddToUnintallList
        '
        Me.btnAddToUnintallList.Location = New System.Drawing.Point(571, 19)
        Me.btnAddToUnintallList.Name = "btnAddToUnintallList"
        Me.btnAddToUnintallList.Size = New System.Drawing.Size(175, 30)
        Me.btnAddToUnintallList.TabIndex = 2
        Me.btnAddToUnintallList.Text = "Mark To Bloatware List"
        Me.btnAddToUnintallList.UseVisualStyleBackColor = True
        '
        'dgListInstall
        '
        Me.dgListInstall.AllowUserToAddRows = False
        Me.dgListInstall.AllowUserToDeleteRows = False
        Me.dgListInstall.AllowUserToOrderColumns = True
        Me.dgListInstall.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgListInstall.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgListInstall.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cbList})
        Me.dgListInstall.Location = New System.Drawing.Point(6, 56)
        Me.dgListInstall.Name = "dgListInstall"
        Me.dgListInstall.RowHeadersWidth = 51
        Me.dgListInstall.RowTemplate.Height = 24
        Me.dgListInstall.Size = New System.Drawing.Size(1402, 274)
        Me.dgListInstall.TabIndex = 1
        '
        'cbList
        '
        Me.cbList.Frozen = True
        Me.cbList.HeaderText = ""
        Me.cbList.MinimumWidth = 6
        Me.cbList.Name = "cbList"
        Me.cbList.Width = 50
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StartupSettingToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1444, 28)
        Me.MenuStrip1.TabIndex = 5
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'StartupSettingToolStripMenuItem
        '
        Me.StartupSettingToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.StartupSettingToolStripMenuItem.Name = "StartupSettingToolStripMenuItem"
        Me.StartupSettingToolStripMenuItem.Size = New System.Drawing.Size(122, 24)
        Me.StartupSettingToolStripMenuItem.Text = "Startup Setting"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1446, 928)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bloatware App"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.dgOptional, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.dgEsential, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgUninstallList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgListInstall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnRefresh As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents dgListInstall As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnUninstall As Button
    Friend WithEvents dgUninstallList As DataGridView
    Friend WithEvents btnAddToUnintallList As Button
    Friend WithEvents cbList As DataGridViewCheckBoxColumn
    Friend WithEvents btnDeleteUninstall As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents txtLog As TextBox
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnInstallApp As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents dgOptional As DataGridView
    Friend WithEvents btnClearOptional As Button
    Friend WithEvents btnInstallUninstallOptional As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents dgEsential As DataGridView
    Friend WithEvents btnClearEsential As Button
    Friend WithEvents btnInstall As Button
    Friend WithEvents btnMarkToOptional As Button
    Friend WithEvents btnMarkToEssential As Button
    Friend WithEvents btnRefreshOptional As Button
    Friend WithEvents btnRefeshDataEssential As Button
    Friend WithEvents btnRefreshDataBloatware As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents StartupSettingToolStripMenuItem As ToolStripMenuItem
End Class
