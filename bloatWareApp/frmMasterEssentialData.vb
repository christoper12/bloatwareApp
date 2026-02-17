Imports Newtonsoft.Json
Imports System.IO
Imports System.Text
Public Class frmMasterEssentialData
    Private Sub frmMasterEssentialData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim jsonPathEssential As String = IO.Path.Combine(Application.StartupPath, "config", "esential-apps.json")
        LoadJsonEssentialToGrid(jsonPathEssential)
    End Sub

    Private Sub LoadJsonEssentialToGrid(jsonPath As String)
        Try
            If Not File.Exists(jsonPath) Then Exit Sub

            ' 1. Load JSON Essential
            Dim json As String = File.ReadAllText(jsonPath, Encoding.UTF8)
            Dim root As AppConfigRoot =
            JsonConvert.DeserializeObject(Of AppConfigRoot)(json)

            If root?.applications Is Nothing Then Exit Sub

            ' 2. Set Essential Apps (dari JSON)
            Dim essentialSet As New HashSet(Of String)(root.applications.Select(Function(a) a.name.Trim()), StringComparer.OrdinalIgnoreCase)

            ' 3. Set aplikasi yang SUDAH TERPASANG (hasil scan)
            'Dim installedSet As New HashSet(Of String)(_allApps.Select(Function(a) a.Name.Trim()), StringComparer.OrdinalIgnoreCase)

            ' 4. Essential yang BELUM terpasang
            'Dim missingEssentialApps = essentialSet.Except(installedSet).ToList()

            ' 5. Render ke grid
            dgEssentialData.Rows.Clear()
            InitGridEssential()

            For Each appName In essentialSet
                dgEssentialData.Rows.Add(False, appName)
            Next

            dgEssentialData.ClearSelection()
            dgEssentialData.CurrentCell = Nothing

        Catch ex As Exception
            MessageBox.Show("Failed to load Essential apps: " & ex.Message)
        End Try
    End Sub

    Private Sub InitGridEssential()
        Try
            dgEssentialData.AutoGenerateColumns = False
            dgEssentialData.Columns.Clear()

            dgEssentialData.ReadOnly = False
            dgEssentialData.AllowUserToAddRows = False
            dgEssentialData.AllowUserToDeleteRows = False

            ' App name column
            Dim checkEssential As New DataGridViewCheckBoxColumn()
            checkEssential.Name = "checkEssential"
            checkEssential.HeaderText = ""
            checkEssential.ReadOnly = False
            checkEssential.Width = 30
            checkEssential.Visible = False

            Dim colName As New DataGridViewTextBoxColumn()
            colName.Name = "Name"
            colName.HeaderText = "Application Name"
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            checkEssential.ReadOnly = True

            dgEssentialData.Columns.Add(checkEssential)
            dgEssentialData.Columns.Add(colName)
        Catch ex As Exception
            MessageBox.Show("Failed to load Essential apps: " & ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class