Imports System.ComponentModel
Imports System.IO
Imports System.Security.Principal
Imports System.Text
Imports Newtonsoft.Json

Public Class startupForm
    Private IsLoading As Boolean = False

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim installPsPath As String = IO.Path.Combine(Application.StartupPath, "powershell", "Get-Startup.ps1")

        Dim psi As New ProcessStartInfo With {
            .FileName = "powershell.exe",
            .Arguments = $"-ExecutionPolicy Bypass -File ""{installPsPath}""",
            .UseShellExecute = False,
            .RedirectStandardOutput = True,
            .RedirectStandardError = True,
            .CreateNoWindow = True
        }

        Dim p = Process.Start(psi)

        Dim json As String = p.StandardOutput.ReadToEnd()
        Dim err As String = p.StandardError.ReadToEnd()

        p.WaitForExit()

        If p.ExitCode <> 0 OrElse String.IsNullOrWhiteSpace(json) Then
            MessageBox.Show(
        "PowerShell error:" & Environment.NewLine & err,
        "Startup Loader Error",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error
    )
            Return
        End If

        Dim rawItems As List(Of StartupRaw) = JsonConvert.DeserializeObject(Of List(Of StartupRaw))(json)

        Dim finalItems As New List(Of StartupItem)

        For Each raw In rawItems

            Dim item As New StartupItem With {
                .Name = raw.Name,
                .Command = raw.Command,
                .Source = raw.Source,
                .Location = raw.Location,
                .IsEnabled = raw.Enabled,
                .RegistryValueName = raw.RegistryValueName,
                .RegistryPath = raw.RegistryPath,
                .TaskName = raw.TaskName,
                .TaskPath = raw.TaskPath,
                .FilePath = raw.FilePath
            }

            ' === LOGIC INTI ===
            Select Case raw.Source
                Case "Registry", "Startup Folder", "Scheduled Task"
                    item.Type = StartupType.Classic
                    item.CanDisable = True

                Case Else
                    item.Type = StartupType.UWP
                    item.CanDisable = False
                    item.DisableReason = "Managed by Windows"
            End Select

            finalItems.Add(item)


            Dim bindingList As New BindingList(Of StartupItem)(finalItems)
            Dim bs As New BindingSource(bindingList, Nothing)

            IsLoading = True
            dgStartup.DataSource = bs
            IsLoading = False
        Next

        Dim basePath = Path.Combine(Application.StartupPath, "config")

        LoadJsonToGrid(dgEssentialStratup, Path.Combine(basePath, "essentialStratup.json"))

        LoadJsonToGrid(dgOptionalStratup, Path.Combine(basePath, "optionalStratup.json"))

        LoadJsonToGrid(dgBloatwareStratup, Path.Combine(basePath, "bloatwareStratup.json"))
    End Sub

    Private Function DetermineEnabledState(raw As StartupRaw) As Boolean

        Select Case raw.Source

            Case "Registry"
                ' kalau registry value aslinya tidak ada → disabled
                Return Not raw.Name.EndsWith(".disabled", StringComparison.OrdinalIgnoreCase)

            Case "Startup Folder"
                ' file .disabled = disabled
                Return Not raw.Command.EndsWith(".disabled", StringComparison.OrdinalIgnoreCase)

            Case "Scheduled Task"
                ' scheduled task sudah punya state native
                Return raw.Enabled

            Case Else
                ' UWP / unknown → managed by system
                Return raw.Enabled
        End Select

    End Function


    Private Sub startupForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If Not IsRunAsAdministrator() Then

        '    Dim result = MessageBox.Show(
        '    "The application requires Administrator rights to continue." & vbCrLf &
        '    "Click OK to restart as Administrator.",
        '    "Administrator Access Rights",
        '    MessageBoxButtons.OKCancel,
        '    MessageBoxIcon.Warning
        ')

        '    If result = DialogResult.OK Then
        '        Try
        '            Dim psi As New ProcessStartInfo()
        '            psi.FileName = Application.ExecutablePath
        '            psi.UseShellExecute = True
        '            psi.Verb = "runas" ' Windows akan minta username/password admin

        '            Process.Start(psi)

        '        Catch ex As Exception
        '            MessageBox.Show(
        '            "Administrator request was canceled or failed.",
        '            "Elevation Failed",
        '            MessageBoxButtons.OK,
        '            MessageBoxIcon.Error
        '        )
        '        End Try
        '    End If

        '    Application.Exit()
        '    Return
        'End If

        prepareDgStartup()
        prepareDgOptionalStartup()
        prepareDgEssentialStartup()
        prepareDgBloatwareStartup()
    End Sub

    Sub prepareDgStartup()
        With dgStartup
            .AutoGenerateColumns = False
            .AllowUserToAddRows = False
            .ReadOnly = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
        End With

        dgStartup.Columns.Clear()

        dgStartup.Columns.Add(New DataGridViewCheckBoxColumn With {
            .Name = "colEnabled",
            .HeaderText = "Enabled",
            .DataPropertyName = "IsEnabled"
        })

        dgStartup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colName",
            .HeaderText = "Name",
            .DataPropertyName = "Name",
            .ReadOnly = True
        })

        dgStartup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colSource",
            .HeaderText = "Source",
            .DataPropertyName = "Source",
            .ReadOnly = True
        })

        dgStartup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colLocation",
            .HeaderText = "Location",
            .DataPropertyName = "Location",
            .ReadOnly = True
        })

        dgStartup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colRegistryPath",
            .HeaderText = "RegistryPath",
            .DataPropertyName = "RegistryPath",
            .ReadOnly = True
        })

        ' ✅ INI DIA – commit checkbox langsung
        AddHandler dgStartup.CurrentCellDirtyStateChanged,
            Sub()
                If dgStartup.IsCurrentCellDirty Then
                    dgStartup.CommitEdit(DataGridViewDataErrorContexts.Commit)
                End If
            End Sub
    End Sub

    Sub prepareDgOptionalStartup()
        With dgOptionalStratup
            .AutoGenerateColumns = False
            .AllowUserToAddRows = False
            .ReadOnly = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
        End With

        dgOptionalStratup.Columns.Clear()

        dgOptionalStratup.Columns.Add(New DataGridViewCheckBoxColumn With {
            .Name = "colEnabled",
            .HeaderText = "Enabled",
            .DataPropertyName = "IsEnabled"
        })

        dgOptionalStratup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colName",
            .HeaderText = "Name",
            .DataPropertyName = "Name",
            .ReadOnly = True
        })

        dgStartup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colSource",
            .HeaderText = "Source",
            .DataPropertyName = "Source",
            .ReadOnly = True
        })

        dgStartup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colLocation",
            .HeaderText = "Location",
            .DataPropertyName = "Location",
            .ReadOnly = True
        })

        dgOptionalStratup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colRegistryPath",
            .HeaderText = "RegistryPath",
            .DataPropertyName = "RegistryPath",
            .ReadOnly = True
        })

        ' ✅ INI DIA – commit checkbox langsung
        AddHandler dgOptionalStratup.CurrentCellDirtyStateChanged,
            Sub()
                If dgOptionalStratup.IsCurrentCellDirty Then
                    dgOptionalStratup.CommitEdit(DataGridViewDataErrorContexts.Commit)
                End If
            End Sub
    End Sub

    Sub prepareDgEssentialStartup()
        With dgEssentialStratup
            .AutoGenerateColumns = False
            .AllowUserToAddRows = False
            .ReadOnly = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
        End With

        dgEssentialStratup.Columns.Clear()

        dgEssentialStratup.Columns.Add(New DataGridViewCheckBoxColumn With {
            .Name = "colEnabled",
            .HeaderText = "Enabled",
            .DataPropertyName = "IsEnabled"
        })

        dgEssentialStratup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colName",
            .HeaderText = "Name",
            .DataPropertyName = "Name",
            .ReadOnly = True
        })

        dgStartup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colSource",
            .HeaderText = "Source",
            .DataPropertyName = "Source",
            .ReadOnly = True
        })

        dgStartup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colLocation",
            .HeaderText = "Location",
            .DataPropertyName = "Location",
            .ReadOnly = True
        })

        dgEssentialStratup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colRegistryPath",
            .HeaderText = "RegistryPath",
            .DataPropertyName = "RegistryPath",
            .ReadOnly = True
        })

        ' ✅ INI DIA – commit checkbox langsung
        AddHandler dgEssentialStratup.CurrentCellDirtyStateChanged,
            Sub()
                If dgEssentialStratup.IsCurrentCellDirty Then
                    dgEssentialStratup.CommitEdit(DataGridViewDataErrorContexts.Commit)
                End If
            End Sub
    End Sub

    Sub prepareDgBloatwareStartup()
        With dgBloatwareStratup
            .AutoGenerateColumns = False
            .AllowUserToAddRows = False
            .ReadOnly = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
        End With

        dgBloatwareStratup.Columns.Clear()

        dgBloatwareStratup.Columns.Add(New DataGridViewCheckBoxColumn With {
            .Name = "colEnabled",
            .HeaderText = "Enabled",
            .DataPropertyName = "IsEnabled"
        })

        dgBloatwareStratup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colName",
            .HeaderText = "Name",
            .DataPropertyName = "Name",
            .ReadOnly = True
        })

        dgStartup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colSource",
            .HeaderText = "Source",
            .DataPropertyName = "Source",
            .ReadOnly = True
        })

        dgStartup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colLocation",
            .HeaderText = "Location",
            .DataPropertyName = "Location",
            .ReadOnly = True
        })

        dgBloatwareStratup.Columns.Add(New DataGridViewTextBoxColumn With {
            .Name = "colRegistryPath",
            .HeaderText = "RegistryPath",
            .DataPropertyName = "RegistryPath",
            .ReadOnly = True
        })

        ' ✅ INI DIA – commit checkbox langsung
        AddHandler dgBloatwareStratup.CurrentCellDirtyStateChanged,
            Sub()
                If dgBloatwareStratup.IsCurrentCellDirty Then
                    dgBloatwareStratup.CommitEdit(DataGridViewDataErrorContexts.Commit)
                End If
            End Sub
    End Sub

    Public Function IsRunAsAdministrator() As Boolean
        Dim identity = WindowsIdentity.GetCurrent()
        Dim principal = New WindowsPrincipal(identity)
        Return principal.IsInRole(WindowsBuiltInRole.Administrator)
    End Function

    Private Sub dgStartup_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgStartup.CellFormatting

        If dgStartup.Columns(e.ColumnIndex).Name = "colEnabled" Then

            Dim item = CType(dgStartup.Rows(e.RowIndex).DataBoundItem, StartupItem)

            If Not item.CanDisable Then
                dgStartup.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                dgStartup.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Gray
            End If
        End If
    End Sub

    Private Sub dgStartup_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles dgStartup.CellToolTipTextNeeded

        If e.RowIndex < 0 Then Return

        Dim item = CType(dgStartup.Rows(e.RowIndex).DataBoundItem, StartupItem)

        If Not item.CanDisable AndAlso Not String.IsNullOrEmpty(item.DisableReason) Then
            e.ToolTipText = item.DisableReason
        End If
    End Sub

    Private Sub dgStartup_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgStartup.DataBindingComplete
        For Each row As DataGridViewRow In dgStartup.Rows
            Dim item = CType(row.DataBoundItem, StartupItem)

            If Not item.CanDisable Then
                row.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
            End If
        Next
    End Sub

    Private Sub dgStartup_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgStartup.CellValueChanged

        'If IsLoading Then Return

        'If e.RowIndex < 0 Then Return
        'If dgStartup.Columns(e.ColumnIndex).Name <> "colEnabled" Then Return

        'Dim item = CType(dgStartup.Rows(e.RowIndex).DataBoundItem, StartupItem)

        'If Not item.CanDisable Then
        '    MessageBox.Show(item.DisableReason, "Action not allowed")
        '    dgStartup.CancelEdit()
        '    Return
        'End If

        'If item.IsEnabled Then
        '    EnableStartupItem(item)
        'Else
        '    DisableStartupItem(item)
        'End If
    End Sub

    Private Sub DisableStartupItem(item As StartupItem)

        Dim psPath As String = IO.Path.Combine(
        Application.StartupPath,
        "powershell",
        "Disable-Startup.ps1"
    )

        Dim args As New Text.StringBuilder()

        args.Append("-ExecutionPolicy Bypass ")
        args.Append("-File ").Append(QuoteArg(psPath)).Append(" ")
        args.Append("-Source ").Append(QuoteArg(item.Source)).Append(" ")
        args.Append("-Name ").Append(QuoteArg(item.Name)).Append(" ")
        args.Append("-Location ").Append(QuoteArg(item.Location))

        Dim psi As New ProcessStartInfo With {
            .FileName = "powershell.exe",
            .Arguments = args.ToString(),
            .UseShellExecute = False,
            .RedirectStandardError = True,
            .CreateNoWindow = True
        }

        Dim p = Process.Start(psi)
        Dim err = p.StandardError.ReadToEnd()
        p.WaitForExit()

        If p.ExitCode <> 0 Then
            MessageBox.Show(err, "Disable failed")
            item.IsEnabled = True
        End If
    End Sub


    Private Sub EnableStartupItem(item As StartupItem)

        Dim psPath = IO.Path.Combine(
        Application.StartupPath,
        "powershell",
        "Enable-Startup.ps1"
    )

        Dim args As New Text.StringBuilder()

        args.Append("-ExecutionPolicy Bypass ")
        args.Append("-File ").Append(QuoteArg(psPath)).Append(" ")
        args.Append("-Source ").Append(QuoteArg(item.Source)).Append(" ")
        args.Append("-Name ").Append(QuoteArg(item.Name)).Append(" ")
        args.Append("-Location ").Append(QuoteArg(item.Location))

        Dim psi As New ProcessStartInfo With {
            .FileName = "powershell.exe",
            .Arguments = args.ToString(),
            .UseShellExecute = False,
            .RedirectStandardError = True,
            .CreateNoWindow = True
        }

        Dim p = Process.Start(psi)
        Dim err = p.StandardError.ReadToEnd()
        p.WaitForExit()

        If p.ExitCode <> 0 Then
            MessageBox.Show(err, "Enable failed")
            item.IsEnabled = False
        End If
    End Sub


    Private Function QuoteArg(value As String) As String
        If String.IsNullOrWhiteSpace(value) Then
            Return """"""
        End If

        Return """" & value.Replace("""", "\""") & """"
    End Function

    Private Sub SaveSelectedToFile(targetFile As String)

        If dgStartup.SelectedRows.Count = 0 Then
            MessageBox.Show("Select at least one item.")
            Exit Sub
        End If

        Dim basePath = Path.Combine(Application.StartupPath, "config")

        Dim files = New List(Of String) From {
            Path.Combine(basePath, "essentialStratup.json"),
            Path.Combine(basePath, "optionalStratup.json"),
            Path.Combine(basePath, "bloatwareStratup.json")
        }

        ' load all files
        Dim data As New Dictionary(Of String, StartupTagFile)

        For Each f In files
            If File.Exists(f) Then
                data(f) =
                    JsonConvert.DeserializeObject(Of StartupTagFile)(
                        File.ReadAllText(f))
            Else
                data(f) = New StartupTagFile()
            End If
        Next

        ' process selection
        For Each row As DataGridViewRow In dgStartup.SelectedRows

            Dim startup = CType(row.DataBoundItem, StartupItem)

            Dim key As String = BuildStartupKey(startup)

            ' hapus dari semua kategori dulu
            For Each d In data.Values
                d.Items.RemoveAll(Function(x) x.Key = key)
            Next

            ' tambahkan ke kategori target
            data(targetFile).Items.Add(New StartupTagItem With {
                .Name = startup.Name,
                .Source = startup.Source,
                .Location = startup.Location,
                .RegistryPath = startup.RegistryPath
            })

        Next

        ' save all
        For Each kv In data
            File.WriteAllText(
                kv.Key,
                JsonConvert.SerializeObject(kv.Value, Formatting.Indented)
            )
        Next

        MessageBox.Show("Saved.")
    End Sub

    Private Function BuildStartupKey(item As StartupItem) As String
        Return $"{item.Source}|{item.Location}|{item.Name}"
    End Function

    Private Sub btnMarkToOptional_Click(sender As Object, e As EventArgs) Handles btnMarkToOptional.Click
        SaveSelectedToFile(Path.Combine(Application.StartupPath, "config\optionalStratup.json"))
    End Sub

    Private Sub btnMarkToEssential_Click(sender As Object, e As EventArgs) Handles btnMarkToEssential.Click
        SaveSelectedToFile(Path.Combine(Application.StartupPath, "config\essentialStratup.json"))
    End Sub

    Private Sub btnAddToUnintallList_Click(sender As Object, e As EventArgs) Handles btnAddToUnintallList.Click
        SaveSelectedToFile(Path.Combine(Application.StartupPath, "config\bloatwareStratup.json"))
    End Sub

    Private Sub LoadJsonToGrid(grid As DataGridView, jsonPath As String)

        If Not File.Exists(jsonPath) Then
            grid.DataSource = Nothing
            Exit Sub
        End If

        Dim json = File.ReadAllText(jsonPath)

        Dim data =
        JsonConvert.DeserializeObject(Of StartupTagFile)(json)

        grid.AutoGenerateColumns = True
        grid.DataSource = data.Items
    End Sub
End Class