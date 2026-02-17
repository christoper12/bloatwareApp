Imports System.Security.Principal
Imports Newtonsoft.Json
Imports System.IO
Imports System.ComponentModel
Imports System.Text

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not IsRunAsAdministrator() Then

            Dim result = MessageBox.Show(
            "The application requires Administrator rights to continue." & vbCrLf &
            "Click OK to restart as Administrator.",
            "Administrator Access Rights",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Warning
        )

            If result = DialogResult.OK Then
                Try
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = Application.ExecutablePath
                    psi.UseShellExecute = True
                    psi.Verb = "runas" ' Windows akan minta username/password admin

                    Process.Start(psi)

                Catch ex As Exception
                    MessageBox.Show(
                    "Administrator request was canceled or failed.",
                    "Elevation Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                )
                End Try
            End If

            Application.Exit()
            Return
        End If

        ' ==========================
        ' KODE NORMAL ANDA LANJUT DI SINI
        ' ==========================
        firstload()

        Dim jsonPath As String = IO.Path.Combine(Application.StartupPath, "config", "installedApp.json")
        LoadJsonToGrid(jsonPath)

        'Dim jsonPathUninstall As String = IO.Path.Combine(Application.StartupPath, "config", "bloatware-apps.json")
        'LoadJsonUninstallToGrid(jsonPathUninstall)

        'Dim jsonPathOptional As String = IO.Path.Combine(Application.StartupPath, "config", "optional-apps.json")
        'LoadJsonOptionalToGrid(jsonPathOptional)

        'Dim emptyLog As String = ""
        'Dim loadLogPath As String = IO.Path.Combine(Application.StartupPath, "logs", "app.log")
        'IO.File.WriteAllText(loadLogPath, emptyLog, Encoding.UTF8)
        'LoadLogger()
    End Sub

    Public Function IsRunAsAdministrator() As Boolean
        Dim identity = WindowsIdentity.GetCurrent()
        Dim principal = New WindowsPrincipal(identity)
        Return principal.IsInRole(WindowsBuiltInRole.Administrator)
    End Function

    Private Sub firstload()
        With dgListInstall
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = True
            '.ReadOnly = True
            .AllowUserToAddRows = False
        End With

        Dim jsonPathinstalled As String = IO.Path.Combine(Application.StartupPath, "config", "installedApp.json")

        Dim emptyJson As String = "[]"
        IO.File.WriteAllText(jsonPathinstalled, emptyJson, Encoding.UTF8)
        'LoadJsonUninstallToGrid(jsonPathinstalled)
    End Sub

    Private _allApps As List(Of InstalledApp)
    Private _checkedApps As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

    Private Sub LoadJsonToGrid(jsonPath As String)
        Try
            If Not File.Exists(jsonPath) Then
                MessageBox.Show("JSON file not found")
                Exit Sub
            End If

            Dim json As String = File.ReadAllText(jsonPath)

            _allApps = JsonConvert.DeserializeObject(Of List(Of InstalledApp))(json) _
            .OrderBy(Function(a) a.Name, StringComparer.OrdinalIgnoreCase) _
            .ToList()

            BindGrid(_allApps)

        Catch ex As Exception
            btnRefresh.PerformClick()
        End Try
    End Sub

    Private Sub BindGrid(data As List(Of InstalledApp))

        Dim sortableList As New SortableBindingList(Of InstalledApp)(data)
        Dim source As New BindingSource(sortableList, Nothing)

        dgListInstall.AutoGenerateColumns = True
        dgListInstall.DataSource = source


        'Dim bindingList As New BindingList(Of InstalledApp)(data)
        'Dim source As New BindingSource(bindingList, Nothing)

        dgListInstall.AutoGenerateColumns = True
        dgListInstall.DataSource = source

        With dgListInstall
            .Columns("Name").ReadOnly = True
            .Columns("Name").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

            .Columns("Version").ReadOnly = True
            .Columns("Version").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

            .Columns("Publisher").ReadOnly = True
            .Columns("Publisher").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

            .Columns("UninstallString").ReadOnly = True
        End With

        For Each row As DataGridViewRow In dgListInstall.Rows
            Dim name = row.Cells("Name").Value.ToString()
            row.Cells("cbList").Value = _checkedApps.Contains(name)
        Next

        dgListInstall.ClearSelection()
        dgListInstall.CurrentCell = Nothing
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim keyword As String = txtSearch.Text.Trim()

        If String.IsNullOrEmpty(keyword) Then
            BindGrid(_allApps)
            Exit Sub
        End If

        ' Dim filtered = _allApps.Where(Function(a) (a.Name IsNot Nothing AndAlso a.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) OrElse (a.Publisher IsNot Nothing AndAlso a.Publisher.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) OrElse (a.Version IsNot Nothing AndAlso a.Version.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)).ToList()
        Dim filtered = _allApps.Where(Function(a) (a.Name IsNot Nothing AndAlso a.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)).ToList()

        BindGrid(filtered)
    End Sub

    Private Sub dgListInstall_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgListInstall.CellValueChanged
        If e.RowIndex < 0 Then Exit Sub
        If dgListInstall.Columns(e.ColumnIndex).Name <> "cbList" Then Exit Sub

        Dim row = dgListInstall.Rows(e.RowIndex)
        Dim name = row.Cells("Name").Value?.ToString()
        If String.IsNullOrWhiteSpace(name) Then Exit Sub

        Dim isChecked As Boolean = False
        Boolean.TryParse(row.Cells("cbList").Value?.ToString(), isChecked)

        If isChecked Then
            _checkedApps.Add(name)
        Else
            _checkedApps.Remove(name)
        End If
    End Sub

    Private Sub dgListInstall_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgListInstall.CurrentCellDirtyStateChanged
        If dgListInstall.IsCurrentCellDirty Then
            dgListInstall.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub


    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        dgListInstall.DataSource = Nothing
        dgListInstall.Rows.Clear()
        dgListInstall.Refresh()

        Dim psPath As String = IO.Path.Combine(Application.StartupPath, "powershell", "getListInstalled.ps1")
        RunPowerShellScript(psPath)

        Dim jsonPath As String = IO.Path.Combine(Application.StartupPath, "config", "installedApp.json")
        LoadJsonToGrid(jsonPath)

        Dim loadLogPath As String = IO.Path.Combine(Application.StartupPath, "logs", "app.log")
        LoadLogger()

        Dim jsonPathOptional As String = IO.Path.Combine(Application.StartupPath, "config", "optional-apps.json")
        LoadJsonOptionalToGrid(jsonPathOptional)

        Dim jsonPathUninstall As String = IO.Path.Combine(Application.StartupPath, "config", "bloatware-apps.json")
        LoadJsonUninstallToGrid(jsonPathUninstall)

        Dim jsonPathEssential As String = IO.Path.Combine(Application.StartupPath, "config", "esential-apps.json")
        LoadJsonEssentialToGrid(jsonPathEssential)
    End Sub

    Private Sub RunPowerShellScript(ps1Path As String)

        If Not IO.File.Exists(ps1Path) Then
            MessageBox.Show("PowerShell script not found:" & vbCrLf & ps1Path)
            Exit Sub
        End If

        Dim psi As New ProcessStartInfo With {
        .FileName = "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe",
        .Arguments = $"-NoProfile -ExecutionPolicy Bypass -File ""{ps1Path}""",
        .WorkingDirectory = IO.Path.GetDirectoryName(ps1Path),
        .UseShellExecute = False,
        .RedirectStandardOutput = True,
        .RedirectStandardError = True,
        .CreateNoWindow = True
    }

        Using proc As Process = New Process()
            proc.StartInfo = psi
            proc.Start()

            Dim output As String = proc.StandardOutput.ReadToEnd()
            Dim err As String = proc.StandardError.ReadToEnd()

            proc.WaitForExit()

            ' Ambil Log directory: (defensive)
            For Each line In output.Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                Dim marker As String = "Log directory:"

                If line.IndexOf(marker, StringComparison.OrdinalIgnoreCase) >= 0 Then
                    _lastLogDir = line.Substring(line.IndexOf(marker, StringComparison.OrdinalIgnoreCase) + marker.Length).Trim()
                End If
            Next

            If proc.ExitCode <> 0 Then
                MessageBox.Show(
                "PowerShell exited with code " & proc.ExitCode & vbCrLf & err,
                "PowerShell Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
            End If
        End Using
    End Sub


    'Private Sub RunPowerShellScript(ps1Path As String)
    '    If Not IO.File.Exists(ps1Path) Then
    '        MessageBox.Show("PowerShell script not found:" & vbCrLf & ps1Path)
    '        Exit Sub
    '    End If

    '    Dim psi As New ProcessStartInfo()
    '    'psi.FileName = "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe"
    '    'psi.Arguments = $"-ExecutionPolicy Bypass -File ""{ps1Path}"""
    '    'psi.WorkingDirectory = IO.Path.GetDirectoryName(ps1Path)
    '    'psi.UseShellExecute = False
    '    'psi.CreateNoWindow = True
    '    'psi.RedirectStandardOutput = True
    '    'psi.RedirectStandardError = True

    '    psi.FileName = "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe"
    '    psi.Arguments = "-ExecutionPolicy Bypass -File """ & ps1Path & """"
    '    psi.UseShellExecute = False
    '    psi.RedirectStandardOutput = True
    '    psi.RedirectStandardError = True
    '    psi.CreateNoWindow = True

    '    Using proc As Process = Process.Start(psi)
    '        Dim output As String = proc.StandardOutput.ReadToEnd()
    '        Dim err As String = proc.StandardError.ReadToEnd()

    '        proc.WaitForExit()

    '        ' Tangkap LOG_DIR
    '        For Each line In output.Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
    '            If line.StartsWith("LOG_DIR=") Then
    '                _lastLogDir = line.Replace("LOG_DIR=", "").Trim()
    '            End If
    '        Next

    '        If Not String.IsNullOrWhiteSpace(err) Then
    '            MessageBox.Show(err, "PowerShell Error")
    '        End If
    '    End Using
    'End Sub

    Private Sub btnAddToUnintallList_Click(sender As Object, e As EventArgs) Handles btnAddToUnintallList.Click
        'If dgListInstall.SelectedRows.Count = 0 Then
        '    MessageBox.Show("Select at least one application.")
        '    Exit Sub
        'End If
        If _checkedApps.Count = 0 Then
            MessageBox.Show("Select at least one application.")
            Exit Sub
        End If

        dgListInstall.EndEdit()

        Dim savePath As String = IO.Path.Combine(Application.StartupPath, "config", "bloatware-apps.json")

        ' HashSet: tidak bisa duplikat + case-insensitive
        Dim appsSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        ' Load data lama
        If IO.File.Exists(savePath) Then
            Dim oldJson = IO.File.ReadAllText(savePath, Encoding.UTF8)
            Dim oldApps = JsonConvert.DeserializeObject(Of List(Of String))(oldJson)

            If oldApps IsNot Nothing Then
                For Each app In oldApps
                    appsSet.Add(app.Trim())
                Next
            End If
        End If

        ' Ambil yang dicentang
        For Each appName In _checkedApps
            If Not String.IsNullOrWhiteSpace(appName) Then
                appsSet.Add(appName.Trim())
            End If
        Next
        'For Each row As DataGridViewRow In dgListInstall.Rows
        '    If row.IsNewRow Then Continue For

        '    Dim isChecked As Boolean = False

        '    If row.Cells("cbList").Value IsNot Nothing Then
        '        Boolean.TryParse(row.Cells("cbList").Value.ToString(), isChecked)
        '    End If

        '    If isChecked Then
        '        Dim appName = row.Cells("Name").Value?.ToString()

        '        If Not String.IsNullOrWhiteSpace(appName) Then
        '            appsSet.Add(appName.Trim())
        '        End If
        '    End If
        'Next

        ' Reset checkbox
        txtSearch.Text = ""
        _checkedApps.Clear()
        For Each row As DataGridViewRow In dgListInstall.Rows
            If row.IsNewRow Then Continue For
            row.Cells("cbList").Value = False
        Next

        ' Simpan kembali sebagai array string
        Dim json = JsonConvert.SerializeObject(appsSet.ToList(), Formatting.Indented)
        IO.File.WriteAllText(savePath, json, Encoding.UTF8)

        LoadJsonUninstallToGrid(savePath)
    End Sub

    Private Sub LoadJsonUninstallToGrid(jsonPath As String)
        Try
            If Not File.Exists(jsonPath) Then
                MessageBox.Show("JSON file not found")
                Exit Sub
            End If

            Dim json As String = File.ReadAllText(jsonPath)

            Dim appNames As List(Of String) = JsonConvert.DeserializeObject(Of List(Of String))(json)

            If appNames Is Nothing Then
                appNames = New List(Of String)
            End If


            Dim appNamesSet As New HashSet(Of String)(appNames, StringComparer.OrdinalIgnoreCase)

            Dim bloatWareApps = _allApps.Where(Function(a) appNamesSet.Contains(a.Name)).ToList()

            ' 3. Render ke grid
            dgUninstallList.Rows.Clear()
            InitGridUninstall()

            For Each app In bloatWareApps
                dgUninstallList.Rows.Add(False, app.Name)
            Next

            dgUninstallList.ClearSelection()
            dgUninstallList.CurrentCell = Nothing

            'For Each Name As String In appNames
            '    dgUninstallList.Rows.Add(Name)
            'Next
        Catch ex As Exception
            Dim jsonPathUninstall As String = IO.Path.Combine(Application.StartupPath, "config", "bloatware-apps.json")

            Dim emptyJson As String = "[]"
            IO.File.WriteAllText(jsonPathUninstall, emptyJson, Encoding.UTF8)
            LoadJsonUninstallToGrid(jsonPathUninstall)
        End Try
    End Sub

    Private Sub InitGridUninstall()
        Try
            dgUninstallList.AutoGenerateColumns = False
            dgUninstallList.Columns.Clear()

            dgUninstallList.ReadOnly = False
            dgUninstallList.AllowUserToAddRows = False
            dgUninstallList.AllowUserToDeleteRows = False

            ' App name column
            Dim checkEssential As New DataGridViewCheckBoxColumn()
            checkEssential.Name = "checkBloatware"
            checkEssential.HeaderText = ""
            checkEssential.ReadOnly = False
            checkEssential.Width = 30

            Dim colName As New DataGridViewTextBoxColumn()
            colName.Name = "Name"
            colName.HeaderText = "Application Name"
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            dgUninstallList.Columns.Add(checkEssential)
            dgUninstallList.Columns.Add(colName)
        Catch ex As Exception
            MessageBox.Show("Failed to load Bloatware apps: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDeleteUninstall_Click(sender As Object, e As EventArgs) Handles btnDeleteUninstall.Click
        Dim jsonPathUninstall As String = IO.Path.Combine(Application.StartupPath, "config", "bloatware-apps.json")

        Dim emptyJson As String = "[]"
        IO.File.WriteAllText(jsonPathUninstall, emptyJson, Encoding.UTF8)

        LoadJsonUninstallToGrid(jsonPathUninstall)
    End Sub

    Private _checkedBloatwareList As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

    Private Sub btnUninstall_Click(sender As Object, e As EventArgs) Handles btnUninstall.Click
        If _checkedBloatwareList.Count = 0 Then
            MessageBox.Show("The application to Uninstall is empty. Please fill it in first before uninstalling the application.")
            Exit Sub
        End If
        'If dgUninstallList.SelectedRows.Count = 0 Then
        '    MessageBox.Show("The application to uninstall is empty. Please fill it in first before uninstalling the application.")
        '    Exit Sub
        'End If

        dgUninstallList.EndEdit()

        Dim selectedApps As New List(Of String)

        For Each row As DataGridViewRow In dgUninstallList.Rows
            If row.IsNewRow Then Continue For

            Dim isChecked As Boolean = row.Cells("checkBloatware").Value IsNot Nothing AndAlso CBool(row.Cells("checkBloatware").Value)

            If isChecked Then
                selectedApps.Add(row.Cells("Name").Value.ToString())
            End If
        Next

        If selectedApps.Count = 0 Then
            MessageBox.Show("The application to install is empty. Please select at least one application before installing.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        RunPowerShellUninstallScript(selectedApps)

        LoadLogger()
    End Sub

    Private Sub RunPowerShellUninstallScript(selectedApps As List(Of String))

        Dim uninstallPsPath As String = IO.Path.Combine(Application.StartupPath, "powershell", "uninstallProc.ps1")

        If Not IO.File.Exists(uninstallPsPath) Then
            MessageBox.Show("PowerShell script not found:" & vbCrLf & uninstallPsPath)
            Exit Sub
        End If

        ' ===== BUILD ARGUMENTS SAFELY =====
        Dim psArgs As New StringBuilder()
        psArgs.Append("-ExecutionPolicy Bypass ")
        psArgs.Append("-File """ & uninstallPsPath & """ ")

        For Each app In selectedApps
            psArgs.Append("-SelectedApps """ & app.Trim() & """ ")
        Next

        Dim psi As New ProcessStartInfo()
        psi.FileName = "powershell.exe"
        psi.Arguments = psArgs.ToString()
        psi.UseShellExecute = False
        psi.CreateNoWindow = True
        psi.RedirectStandardOutput = True
        psi.RedirectStandardError = True

        Using proc As Process = Process.Start(psi)

            Dim output As String = proc.StandardOutput.ReadToEnd()
            Dim err As String = proc.StandardError.ReadToEnd()

            proc.WaitForExit()

            ' ===== PARSE LOG DIRECTORY =====
            For Each line In output.Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)

                Dim marker As String = "Log directory:"

                If line.Contains(marker) Then
                    _lastLogDir = line.Substring(line.IndexOf(marker) + marker.Length).Trim()
                    Exit For
                End If
            Next

            If Not String.IsNullOrWhiteSpace(err) Then
                MessageBox.Show(err, "PowerShell Error")
            End If
        End Using
    End Sub

    Private Sub dgUninstallList_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgUninstallList.CellValueChanged
        If e.RowIndex < 0 Then Exit Sub
        If dgUninstallList.Columns(e.ColumnIndex).Name <> "checkBloatware" Then Exit Sub

        Dim row = dgUninstallList.Rows(e.RowIndex)
        Dim name = row.Cells("Name").Value?.ToString()
        If String.IsNullOrWhiteSpace(name) Then Exit Sub

        Dim isChecked As Boolean = False
        Boolean.TryParse(row.Cells("checkBloatware").Value?.ToString(), isChecked)

        If isChecked Then
            _checkedBloatwareList.Add(name)
        Else
            _checkedBloatwareList.Remove(name)
        End If
    End Sub

    Private Sub dgUninstallList_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgUninstallList.CurrentCellDirtyStateChanged
        If dgUninstallList.IsCurrentCellDirty Then
            dgUninstallList.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private _lastLogDir As String = Nothing
    Private Sub LoadLogger()

        If String.IsNullOrWhiteSpace(_lastLogDir) Then
            MessageBox.Show("Log directory not found (session info missing).")
            Exit Sub
        End If

        Dim logFile As String = IO.Path.Combine(_lastLogDir, "install.log")

        If Not File.Exists(logFile) Then
            MessageBox.Show("Log file not found:" & vbCrLf & logFile)
            Exit Sub
        End If

        If txtLog.Text = "" Then
            txtLog.Text = File.ReadAllText(logFile)
        Else
            txtLog.Text &= vbCrLf & File.ReadAllText(logFile)
        End If

    End Sub

    Private Sub btnInstallApp_Click(sender As Object, e As EventArgs) Handles btnInstallApp.Click
        frmInstallApp.Close()
        frmInstallApp.ShowDialog()
    End Sub

    Private Sub btnMarkToOptional_Click(sender As Object, e As EventArgs) Handles btnMarkToOptional.Click
        If _checkedApps.Count = 0 Then
            MessageBox.Show("Select at least one application.")
            Exit Sub
        End If

        dgListInstall.EndEdit()

        Dim savePath As String = IO.Path.Combine(Application.StartupPath, "config", "optional-apps.json")

        ' HashSet: tidak bisa duplikat + case-insensitive
        Dim appsSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        ' Load data lama
        If IO.File.Exists(savePath) Then
            Dim oldJson = IO.File.ReadAllText(savePath, Encoding.UTF8)
            Dim oldApps = JsonConvert.DeserializeObject(Of List(Of String))(oldJson)

            If oldApps IsNot Nothing Then
                For Each app In oldApps
                    appsSet.Add(app.Trim())
                Next
            End If
        End If

        ' Ambil yang dicentang
        For Each appName In _checkedApps
            If Not String.IsNullOrWhiteSpace(appName) Then
                appsSet.Add(appName.Trim())
            End If
        Next

        ' Reset checkbox
        txtSearch.Text = ""
        _checkedApps.Clear()
        For Each row As DataGridViewRow In dgListInstall.Rows
            If row.IsNewRow Then Continue For
            row.Cells("cbList").Value = False
        Next

        ' Simpan kembali sebagai array string
        Dim json = JsonConvert.SerializeObject(appsSet.ToList(), Formatting.Indented)
        IO.File.WriteAllText(savePath, json, Encoding.UTF8)

        LoadJsonOptionalToGrid(savePath)
    End Sub

    Private Sub LoadJsonOptionalToGrid(jsonPath As String)
        Try
            If Not File.Exists(jsonPath) Then
                MessageBox.Show("JSON file not found")
                Exit Sub
            End If

            Dim json As String = File.ReadAllText(jsonPath)

            Dim appNames As List(Of String) = JsonConvert.DeserializeObject(Of List(Of String))(json)

            If appNames Is Nothing Then
                appNames = New List(Of String)
            End If

            dgOptional.Rows.Clear()

            InitGridOptional()

            For Each Name As String In appNames
                dgOptional.Rows.Add(Name)
            Next

            dgOptional.ClearSelection()
            dgOptional.CurrentCell = Nothing
        Catch ex As Exception
            Dim jsonPathUninstall As String = IO.Path.Combine(Application.StartupPath, "config", "optional-apps.json")

            Dim emptyJson As String = "[]"
            IO.File.WriteAllText(jsonPathUninstall, emptyJson, Encoding.UTF8)
            LoadJsonOptionalToGrid(jsonPathUninstall)
        End Try

        'Try
        '    If Not File.Exists(jsonPath) Then Exit Sub

        '    ' 1. Load daftar Optional (niat user)
        '    Dim json As String = File.ReadAllText(jsonPath, Encoding.UTF8)
        '    Dim optionalNames As List(Of String) = JsonConvert.DeserializeObject(Of List(Of String))(json)

        '    If optionalNames Is Nothing Then Exit Sub

        '    '' 2. Ambil HANYA aplikasi yang:
        '    ''    - Ada di optional-apps.json
        '    ''    - DAN benar-benar terinstall (hasil scan)
        '    'Dim installedOptionalApps = _allApps.
        '    '        Where(Function(a)
        '    '                  optionalNames.
        '    '                      Any(Function(n)
        '    '                              String.Equals(n, a.Name, StringComparison.OrdinalIgnoreCase)
        '    '                          End Function)
        '    '              End Function).
        '    '        ToList()

        '    Dim optionalSet As New HashSet(Of String)(optionalNames, StringComparer.OrdinalIgnoreCase)

        '    Dim installedOptionalApps = _allApps.Where(Function(a) optionalSet.Contains(a.Name)).ToList()

        '    ' 3. Render ke grid
        '    dgOptional.Rows.Clear()
        '    InitGridOptional()

        '    For Each app In installedOptionalApps
        '        dgOptional.Rows.Add(app.Name)
        '    Next

        'Catch ex As Exception
        '    MessageBox.Show("Failed to load Optional apps: " & ex.Message)
        'End Try
    End Sub

    Private Sub InitGridOptional()
        dgOptional.AutoGenerateColumns = False
        dgOptional.Columns.Clear()

        ' App name column
        Dim colName As New DataGridViewTextBoxColumn()
        colName.Name = "Name"
        colName.HeaderText = "Application Name"
        colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        dgOptional.Columns.Add(colName)

        dgOptional.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgOptional.MultiSelect = False ' atau False, sesuai kebutuhan
    End Sub

    Private Sub btnClearOptional_Click(sender As Object, e As EventArgs) Handles btnClearOptional.Click
        Dim jsonPathUninstall As String = IO.Path.Combine(Application.StartupPath, "config", "optional-apps.json")

        Dim emptyJson As String = "[]"
        IO.File.WriteAllText(jsonPathUninstall, emptyJson, Encoding.UTF8)

        LoadJsonOptionalToGrid(jsonPathUninstall)
    End Sub

    Private Sub dgOptional_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgOptional.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim selectedAppName As String = dgOptional.Rows(e.RowIndex).Cells("Name").Value?.ToString()

        If String.IsNullOrWhiteSpace(selectedAppName) Then Exit Sub

        ' Cek apakah sudah terinstall
        Dim isInstalled As Boolean = dgListInstall.Rows.
                Cast(Of DataGridViewRow)().
                Any(Function(r)
                        Dim name = r.Cells("Name").Value?.ToString()
                        Return String.Equals(name, selectedAppName, StringComparison.OrdinalIgnoreCase)
                    End Function)

        btnInstallUninstallOptional.Text = If(isInstalled, "Uninstall", "Install")
    End Sub

    Private Sub btnInstallUninstallOptional_Click(sender As Object, e As EventArgs) Handles btnInstallUninstallOptional.Click
        If dgOptional.SelectedRows.Count = 0 Then
            MessageBox.Show("The application to uninstall is empty. Please fill it in first before uninstalling the application.")
            Exit Sub
        End If

        dgOptional.EndEdit()

        If btnInstallUninstallOptional.Text = "Uninstall" Then
            Dim selectedApps As New List(Of String)

            For Each row As DataGridViewRow In dgOptional.Rows
                If row.IsNewRow Then Continue For

                Dim appName As String = row.Cells("Name").Value?.ToString()

                If Not String.IsNullOrWhiteSpace(appName) Then
                    selectedApps.Add(appName)
                End If
            Next

            If selectedApps.Count = 0 Then
                MessageBox.Show("The application to install is empty. Please select at least one application before installing.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            RunPowerShellUninstallScript(selectedApps)

            LoadLogger()
        ElseIf btnInstallUninstallOptional.Text = "Install" Then
            Dim selectedApps As New List(Of String)

            For Each row As DataGridViewRow In dgOptional.SelectedRows
                If row.IsNewRow Then Continue For

                Dim appName As String = row.Cells("Name").Value?.ToString()

                If Not String.IsNullOrWhiteSpace(appName) Then
                    selectedApps.Add(appName)
                End If

                Dim normalizedName = appName.Trim()

                ' ===== BARU MASUK KE SINI JIKA BELUM ADA =====
                Dim installerPath As String = ""

                MsgBox("Please select the installer file for '" & normalizedName & "' in the next dialog.", MsgBoxStyle.Information, "Select Installer")
                Dim ofd As New OpenFileDialog()
                ofd.Title = "Please select the path for the 'Essential' installer."
                ofd.Filter = "Installer (*.exe;*.msi)|*.exe;*.msi"

                If ofd.ShowDialog() = DialogResult.OK Then
                    installerPath = ofd.FileName

                    dgOptional.EndEdit()

                    frmInstallApp.saveInstallOptionalData(normalizedName, installerPath)

                    RunPowershellForInstallOptional(selectedApps)

                    LoadLogger()
                Else
                    Exit Sub
                End If
            Next
        Else
            Exit Sub
        End If
    End Sub

    Sub RunPowershellForInstallOptional(selectedApps As List(Of String))
        Dim installPsPath As String = IO.Path.Combine(Application.StartupPath, "powershell", "InstallOptional.ps1")

        If Not IO.File.Exists(installPsPath) Then
            MessageBox.Show("PowerShell script not found:" & vbCrLf & installPsPath)
            Exit Sub
        End If

        Dim selectedArg As String = String.Join("|", selectedApps.Select(Function(x) x.Trim()))

        Dim psi As New ProcessStartInfo()
        psi.FileName = "powershell.exe"
        psi.Arguments = $"-ExecutionPolicy Bypass -File ""{installPsPath}"" -SelectedApps ""{selectedArg}"""
        psi.UseShellExecute = False
        psi.CreateNoWindow = True
        psi.RedirectStandardOutput = True
        psi.RedirectStandardError = True

        Using proc As Process = Process.Start(psi)
            Dim output As String = proc.StandardOutput.ReadToEnd()
            Dim err As String = proc.StandardError.ReadToEnd()

            proc.WaitForExit()

            ' Ambil Log directory: (defensive)
            For Each line In output.Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                Dim marker As String = "Log directory:"

                If line.IndexOf(marker, StringComparison.OrdinalIgnoreCase) >= 0 Then
                    _lastLogDir = line.Substring(line.IndexOf(marker, StringComparison.OrdinalIgnoreCase) + marker.Length).Trim()
                End If
            Next

            If Not String.IsNullOrWhiteSpace(err) Then
                MessageBox.Show(err, "PowerShell Error")
            End If
        End Using
    End Sub

    Private Sub btnRefreshOptional_Click(sender As Object, e As EventArgs) Handles btnRefreshOptional.Click
        btnRefresh.PerformClick()
    End Sub

    Private Sub btnMarkToEssential_Click(sender As Object, e As EventArgs) Handles btnMarkToEssential.Click
        If _checkedApps.Count = 0 Then
            MessageBox.Show("Select at least one application.")
            Exit Sub
        End If

        dgListInstall.EndEdit()

        Dim savePath As String = IO.Path.Combine(Application.StartupPath, "config", "esential-apps.json")

        ' HashSet: tidak bisa duplikat + case-insensitive
        Dim appsSet As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        ' Load data lama
        If IO.File.Exists(savePath) Then
            Dim oldJson = IO.File.ReadAllText(savePath, Encoding.UTF8)
            Dim data As AppConfigRoot = JsonConvert.DeserializeObject(Of AppConfigRoot)(oldJson)

            Dim oldApps As List(Of AppConfig) = data.applications

            If oldApps IsNot Nothing Then
                For Each app In oldApps
                    appsSet.Add(app.name.Trim())
                Next
            End If
        End If

        ' Ambil yang dicentang
        For Each appName In _checkedApps
            If String.IsNullOrWhiteSpace(appName) Then Continue For

            Dim normalizedName = appName.Trim()

            ' 🔴 JIKA SUDAH ADA → LANGSUNG LEWAT
            If appsSet.Contains(normalizedName) Then
                Continue For
            End If

            ' ===== BARU MASUK KE SINI JIKA BELUM ADA =====
            Dim installerPath As String = ""

            Dim result As DialogResult

            result = MessageBox.Show(
                "Are you sure you want to mark this application ('" & normalizedName & "') as essential?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )

            If result = DialogResult.Yes Then
                MsgBox("Please select the installer file for '" & normalizedName & "' in the next dialog.", MsgBoxStyle.Information, "Select Installer")
                Dim ofd As New OpenFileDialog()
                ofd.Title = "Please select the path for the 'Essential' installer."
                ofd.Filter = "Installer (*.exe;*.msi)|*.exe;*.msi"

                If ofd.ShowDialog() = DialogResult.OK Then
                    installerPath = ofd.FileName
                End If

                frmInstallApp.saveEsentialData(normalizedName, installerPath)
            Else
                Continue For
            End If
        Next

        ' Reset checkbox
        txtSearch.Text = ""
        _checkedApps.Clear()
        For Each row As DataGridViewRow In dgListInstall.Rows
            If row.IsNewRow Then Continue For
            row.Cells("cbList").Value = False
        Next

        LoadJsonEssentialToGrid(savePath)
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
            Dim installedSet As New HashSet(Of String)(_allApps.Select(Function(a) a.Name.Trim()), StringComparer.OrdinalIgnoreCase)

            ' 4. Essential yang BELUM terpasang
            Dim missingEssentialApps =
            essentialSet.Except(installedSet).ToList()

            ' 5. Render ke grid
            dgEsential.Rows.Clear()
            InitGridEssential()

            For Each appName In missingEssentialApps
                dgEsential.Rows.Add(False, appName)
            Next

            dgEsential.ClearSelection()
            dgEsential.CurrentCell = Nothing

        Catch ex As Exception
            MessageBox.Show("Failed to load Essential apps: " & ex.Message)
        End Try
    End Sub

    Private Sub InitGridEssential()
        Try
            dgEsential.AutoGenerateColumns = False
            dgEsential.Columns.Clear()

            dgEsential.ReadOnly = False
            dgEsential.AllowUserToAddRows = False
            dgEsential.AllowUserToDeleteRows = False

            ' App name column
            Dim checkEssential As New DataGridViewCheckBoxColumn()
            checkEssential.Name = "checkEssential"
            checkEssential.HeaderText = ""
            checkEssential.ReadOnly = False
            checkEssential.Width = 30

            Dim colName As New DataGridViewTextBoxColumn()
            colName.Name = "Name"
            colName.HeaderText = "Application Name"
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            dgEsential.Columns.Add(checkEssential)
            dgEsential.Columns.Add(colName)
        Catch ex As Exception
            MessageBox.Show("Failed to load Essential apps: " & ex.Message)
        End Try
    End Sub

    Private Sub btnClearEsential_Click(sender As Object, e As EventArgs) Handles btnClearEsential.Click
        Dim jsonPathUninstall As String = IO.Path.Combine(Application.StartupPath, "config", "esential-apps.json")

        Dim emptyJson As String = "{""applications"": []}"
        IO.File.WriteAllText(jsonPathUninstall, emptyJson, Encoding.UTF8)

        LoadJsonEssentialToGrid(jsonPathUninstall)
    End Sub

    Private _checkedEssentialList As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

    Private Sub btnInstall_Click(sender As Object, e As EventArgs) Handles btnInstall.Click
        If _checkedEssentialList.Count = 0 Then
            MessageBox.Show("The application to Install is empty. Please fill it in first before Install the application.")
            Exit Sub
        End If

        'If dgEsential.SelectedRows.Count = 0 Then
        '    MessageBox.Show("The application to uninstall is empty. Please fill it in first before uninstalling the application.")
        '    Exit Sub
        'End If

        dgEsential.EndEdit()

        Dim selectedApps As New List(Of String)

        For Each row As DataGridViewRow In dgEsential.Rows
            If row.IsNewRow Then Continue For

            Dim isChecked As Boolean = row.Cells("checkEssential").Value IsNot Nothing AndAlso CBool(row.Cells("checkEssential").Value)

            If isChecked Then
                selectedApps.Add(row.Cells("Name").Value.ToString())
            End If
        Next

        If selectedApps.Count = 0 Then
            MessageBox.Show("The application to install is empty. Please select at least one application before installing.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        RunPowerShellInstallScript(selectedApps)

        LoadLogger()
    End Sub

    Private Sub RunPowerShellInstallScript(selectedApps As List(Of String))
        Dim installPsPath As String = IO.Path.Combine(Application.StartupPath, "powershell", "Install.ps1")

        If Not IO.File.Exists(installPsPath) Then
            MessageBox.Show("PowerShell script not found:" & vbCrLf & installPsPath)
            Exit Sub
        End If

        Dim selectedArg As String = String.Join("|", selectedApps.Select(Function(x) x.Trim()))

        Dim psi As New ProcessStartInfo()
        psi.FileName = "powershell.exe"
        psi.Arguments = $"-ExecutionPolicy Bypass -File ""{installPsPath}"" -SelectedApps ""{selectedArg}"""
        psi.UseShellExecute = False
        psi.CreateNoWindow = True
        psi.RedirectStandardOutput = True
        psi.RedirectStandardError = True

        Using proc As Process = Process.Start(psi)
            Dim output As String = proc.StandardOutput.ReadToEnd()
            Dim err As String = proc.StandardError.ReadToEnd()

            proc.WaitForExit()

            ' Ambil Log directory: (defensive)
            For Each line In output.Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                Dim marker As String = "Log directory:"

                If line.IndexOf(marker, StringComparison.OrdinalIgnoreCase) >= 0 Then
                    _lastLogDir = line.Substring(line.IndexOf(marker, StringComparison.OrdinalIgnoreCase) + marker.Length).Trim()
                End If
            Next

            If Not String.IsNullOrWhiteSpace(err) Then
                MessageBox.Show(err, "PowerShell Error")
            End If
        End Using
    End Sub

    Private Sub dgEsential_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgEsential.CellValueChanged
        If e.RowIndex < 0 Then Exit Sub
        If dgEsential.Columns(e.ColumnIndex).Name <> "checkEssential" Then Exit Sub

        Dim row = dgEsential.Rows(e.RowIndex)
        Dim name = row.Cells("Name").Value?.ToString()
        If String.IsNullOrWhiteSpace(name) Then Exit Sub

        Dim isChecked As Boolean = False
        Boolean.TryParse(row.Cells("checkEssential").Value?.ToString(), isChecked)

        If isChecked Then
            _checkedEssentialList.Add(name)
        Else
            _checkedEssentialList.Remove(name)
        End If
    End Sub

    Private Sub dgEsentiall_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgEsential.CurrentCellDirtyStateChanged
        If dgEsential.IsCurrentCellDirty Then
            dgEsential.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub btnRefeshDataEssential_Click(sender As Object, e As EventArgs) Handles btnRefeshDataEssential.Click
        btnRefresh.PerformClick()
    End Sub

    Private Sub btnRefreshDataBloatware_Click(sender As Object, e As EventArgs) Handles btnRefreshDataBloatware.Click
        btnRefresh.PerformClick()
    End Sub

    Private Sub StartupSettingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartupSettingToolStripMenuItem.Click
        startupForm.ShowDialog(Me)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmMasterEssentialData.Close()
        frmMasterEssentialData.Show()
    End Sub
End Class
