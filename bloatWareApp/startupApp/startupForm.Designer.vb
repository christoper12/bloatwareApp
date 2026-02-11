<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class startupForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(startupForm))
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.dgStartup = New System.Windows.Forms.DataGridView()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageOptionalStratup = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnDeleteOptionalStartup = New System.Windows.Forms.Button()
        Me.dgOptionalStratup = New System.Windows.Forms.DataGridView()
        Me.TabPageEssentialStratup = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnDeleteEssentiallStartup = New System.Windows.Forms.Button()
        Me.dgEssentialStratup = New System.Windows.Forms.DataGridView()
        Me.TabPageBloatwareStratup = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnDeleteBloatwareStartup = New System.Windows.Forms.Button()
        Me.dgBloatwareStratup = New System.Windows.Forms.DataGridView()
        Me.btnMarkToOptional = New System.Windows.Forms.Button()
        Me.btnMarkToEssential = New System.Windows.Forms.Button()
        Me.btnAddToUnintallList = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.dgStartup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPageOptionalStratup.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgOptionalStratup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageEssentialStratup.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgEssentialStratup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageBloatwareStratup.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgBloatwareStratup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(6, 12)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(96, 31)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'dgStartup
        '
        Me.dgStartup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgStartup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgStartup.Location = New System.Drawing.Point(6, 49)
        Me.dgStartup.Name = "dgStartup"
        Me.dgStartup.RowHeadersWidth = 51
        Me.dgStartup.RowTemplate.Height = 24
        Me.dgStartup.Size = New System.Drawing.Size(1062, 242)
        Me.dgStartup.TabIndex = 1
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageOptionalStratup)
        Me.TabControl1.Controls.Add(Me.TabPageEssentialStratup)
        Me.TabControl1.Controls.Add(Me.TabPageBloatwareStratup)
        Me.TabControl1.Location = New System.Drawing.Point(6, 297)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1062, 336)
        Me.TabControl1.TabIndex = 2
        '
        'TabPageOptionalStratup
        '
        Me.TabPageOptionalStratup.Controls.Add(Me.GroupBox1)
        Me.TabPageOptionalStratup.Location = New System.Drawing.Point(4, 25)
        Me.TabPageOptionalStratup.Name = "TabPageOptionalStratup"
        Me.TabPageOptionalStratup.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageOptionalStratup.Size = New System.Drawing.Size(1054, 307)
        Me.TabPageOptionalStratup.TabIndex = 0
        Me.TabPageOptionalStratup.Text = "Optional"
        Me.TabPageOptionalStratup.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnDeleteOptionalStartup)
        Me.GroupBox1.Controls.Add(Me.dgOptionalStratup)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1042, 299)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnDeleteOptionalStartup
        '
        Me.btnDeleteOptionalStartup.Location = New System.Drawing.Point(6, 11)
        Me.btnDeleteOptionalStartup.Name = "btnDeleteOptionalStartup"
        Me.btnDeleteOptionalStartup.Size = New System.Drawing.Size(98, 30)
        Me.btnDeleteOptionalStartup.TabIndex = 11
        Me.btnDeleteOptionalStartup.Text = "Delete"
        Me.btnDeleteOptionalStartup.UseVisualStyleBackColor = True
        '
        'dgOptionalStratup
        '
        Me.dgOptionalStratup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgOptionalStratup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgOptionalStratup.Location = New System.Drawing.Point(6, 47)
        Me.dgOptionalStratup.Name = "dgOptionalStratup"
        Me.dgOptionalStratup.RowHeadersWidth = 51
        Me.dgOptionalStratup.RowTemplate.Height = 24
        Me.dgOptionalStratup.Size = New System.Drawing.Size(1030, 246)
        Me.dgOptionalStratup.TabIndex = 0
        '
        'TabPageEssentialStratup
        '
        Me.TabPageEssentialStratup.Controls.Add(Me.GroupBox2)
        Me.TabPageEssentialStratup.Location = New System.Drawing.Point(4, 25)
        Me.TabPageEssentialStratup.Name = "TabPageEssentialStratup"
        Me.TabPageEssentialStratup.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageEssentialStratup.Size = New System.Drawing.Size(1054, 307)
        Me.TabPageEssentialStratup.TabIndex = 1
        Me.TabPageEssentialStratup.Text = "Essential"
        Me.TabPageEssentialStratup.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.btnDeleteEssentiallStartup)
        Me.GroupBox2.Controls.Add(Me.dgEssentialStratup)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1042, 299)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'btnDeleteEssentiallStartup
        '
        Me.btnDeleteEssentiallStartup.Location = New System.Drawing.Point(6, 11)
        Me.btnDeleteEssentiallStartup.Name = "btnDeleteEssentiallStartup"
        Me.btnDeleteEssentiallStartup.Size = New System.Drawing.Size(98, 30)
        Me.btnDeleteEssentiallStartup.TabIndex = 12
        Me.btnDeleteEssentiallStartup.Text = "Delete"
        Me.btnDeleteEssentiallStartup.UseVisualStyleBackColor = True
        '
        'dgEssentialStratup
        '
        Me.dgEssentialStratup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgEssentialStratup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgEssentialStratup.Location = New System.Drawing.Point(6, 47)
        Me.dgEssentialStratup.Name = "dgEssentialStratup"
        Me.dgEssentialStratup.RowHeadersWidth = 51
        Me.dgEssentialStratup.RowTemplate.Height = 24
        Me.dgEssentialStratup.Size = New System.Drawing.Size(1030, 246)
        Me.dgEssentialStratup.TabIndex = 1
        '
        'TabPageBloatwareStratup
        '
        Me.TabPageBloatwareStratup.Controls.Add(Me.GroupBox3)
        Me.TabPageBloatwareStratup.Location = New System.Drawing.Point(4, 25)
        Me.TabPageBloatwareStratup.Name = "TabPageBloatwareStratup"
        Me.TabPageBloatwareStratup.Size = New System.Drawing.Size(1054, 307)
        Me.TabPageBloatwareStratup.TabIndex = 2
        Me.TabPageBloatwareStratup.Text = "Bloatware"
        Me.TabPageBloatwareStratup.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.btnDeleteBloatwareStartup)
        Me.GroupBox3.Controls.Add(Me.dgBloatwareStratup)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1042, 299)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        '
        'btnDeleteBloatwareStartup
        '
        Me.btnDeleteBloatwareStartup.Location = New System.Drawing.Point(6, 11)
        Me.btnDeleteBloatwareStartup.Name = "btnDeleteBloatwareStartup"
        Me.btnDeleteBloatwareStartup.Size = New System.Drawing.Size(98, 30)
        Me.btnDeleteBloatwareStartup.TabIndex = 12
        Me.btnDeleteBloatwareStartup.Text = "Delete"
        Me.btnDeleteBloatwareStartup.UseVisualStyleBackColor = True
        '
        'dgBloatwareStratup
        '
        Me.dgBloatwareStratup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgBloatwareStratup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgBloatwareStratup.Location = New System.Drawing.Point(6, 47)
        Me.dgBloatwareStratup.Name = "dgBloatwareStratup"
        Me.dgBloatwareStratup.RowHeadersWidth = 51
        Me.dgBloatwareStratup.RowTemplate.Height = 24
        Me.dgBloatwareStratup.Size = New System.Drawing.Size(1030, 246)
        Me.dgBloatwareStratup.TabIndex = 2
        '
        'btnMarkToOptional
        '
        Me.btnMarkToOptional.Location = New System.Drawing.Point(465, 12)
        Me.btnMarkToOptional.Name = "btnMarkToOptional"
        Me.btnMarkToOptional.Size = New System.Drawing.Size(175, 30)
        Me.btnMarkToOptional.TabIndex = 10
        Me.btnMarkToOptional.Text = "Mark To Optional List"
        Me.btnMarkToOptional.UseVisualStyleBackColor = True
        '
        'btnMarkToEssential
        '
        Me.btnMarkToEssential.Location = New System.Drawing.Point(646, 12)
        Me.btnMarkToEssential.Name = "btnMarkToEssential"
        Me.btnMarkToEssential.Size = New System.Drawing.Size(175, 30)
        Me.btnMarkToEssential.TabIndex = 9
        Me.btnMarkToEssential.Text = "Mark To Essential List"
        Me.btnMarkToEssential.UseVisualStyleBackColor = True
        '
        'btnAddToUnintallList
        '
        Me.btnAddToUnintallList.Location = New System.Drawing.Point(827, 12)
        Me.btnAddToUnintallList.Name = "btnAddToUnintallList"
        Me.btnAddToUnintallList.Size = New System.Drawing.Size(175, 30)
        Me.btnAddToUnintallList.TabIndex = 8
        Me.btnAddToUnintallList.Text = "Mark To Bloatware List"
        Me.btnAddToUnintallList.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.bloatWareApp.My.Resources.Resources.Loading
        Me.PictureBox1.InitialImage = Nothing
        Me.PictureBox1.Location = New System.Drawing.Point(449, 103)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(166, 152)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 11
        Me.PictureBox1.TabStop = False
        '
        'startupForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1080, 645)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnMarkToOptional)
        Me.Controls.Add(Me.btnMarkToEssential)
        Me.Controls.Add(Me.btnAddToUnintallList)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.dgStartup)
        Me.Controls.Add(Me.btnRefresh)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "startupForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Startup Form"
        CType(Me.dgStartup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageOptionalStratup.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgOptionalStratup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageEssentialStratup.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgEssentialStratup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageBloatwareStratup.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.dgBloatwareStratup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnRefresh As Button
    Friend WithEvents dgStartup As DataGridView
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageOptionalStratup As TabPage
    Friend WithEvents TabPageEssentialStratup As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TabPageBloatwareStratup As TabPage
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents dgOptionalStratup As DataGridView
    Friend WithEvents dgEssentialStratup As DataGridView
    Friend WithEvents dgBloatwareStratup As DataGridView
    Friend WithEvents btnMarkToOptional As Button
    Friend WithEvents btnMarkToEssential As Button
    Friend WithEvents btnAddToUnintallList As Button
    Friend WithEvents btnDeleteOptionalStartup As Button
    Friend WithEvents btnDeleteEssentiallStartup As Button
    Friend WithEvents btnDeleteBloatwareStartup As Button
    Friend WithEvents PictureBox1 As PictureBox
End Class
