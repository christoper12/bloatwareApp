Imports System.IO
Imports System.Text

Public Class frmInstallApp
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Installer (*.exe;*.msi)|*.exe;*.msi"

        If ofd.ShowDialog() = DialogResult.OK Then
            txtInstallerPath.Text = ofd.FileName
        End If
    End Sub

    Private Sub btnSaveToJson_Click(sender As Object, e As EventArgs) Handles btnSaveToJson.Click
        saveEsentialData(txtName.Text.Trim(), txtInstallerPath.Text.Trim())
    End Sub

    Public Sub saveEsentialData(ByVal name As String, ByVal installerPath As String)
        Dim jsonPath As String = IO.Path.Combine(Application.StartupPath, "config", "esential-apps.json")

        ' 1. Load JSON lama (kalau ada)
        Dim root As AppConfigRoot

        If File.Exists(jsonPath) Then
            Dim existingJson = File.ReadAllText(jsonPath)
            root = Newtonsoft.Json.JsonConvert.DeserializeObject(Of AppConfigRoot)(existingJson)
        Else
            root = New AppConfigRoot With {
                .applications = New List(Of AppConfig)
            }
        End If

        ' 2. Detect extension installer
        Dim ext As String = IO.Path.GetExtension(installerPath).ToLower()

        Dim silentArgs As String = "/install"   ' default untuk EXE

        If ext = ".msi" Then
            silentArgs = ""      ' MSI → Full UI
            ' atau: silentArgs = "/qf"
        End If

        ' 3. Build object
        Dim app As New AppConfig With {
            .name = name,
            .installerPath = installerPath,
            .silentArgs = silentArgs,
            .detectName = name
        }

        ' 4. Cek existing
        Dim existingApp = root.applications.FirstOrDefault(
        Function(a) a.name.Equals(name, StringComparison.OrdinalIgnoreCase)
        )

        If existingApp IsNot Nothing Then
            existingApp.installerPath = installerPath
            existingApp.silentArgs = silentArgs
            existingApp.detectName = name
        Else
            root.applications.Add(app)
        End If

        ' 5. Serialize ulang ke JSON
        Dim newJson = Newtonsoft.Json.JsonConvert.SerializeObject(
            root,
            Newtonsoft.Json.Formatting.Indented
        )

        ' 6. Simpan ke file
        File.WriteAllText(jsonPath, newJson)

        'MessageBox.Show("Data aplikasi berhasil disimpan ke JSON")
    End Sub


    Public Sub saveInstallOptionalData(ByVal name As String, ByVal installerPath As String)
        Dim jsonPathUninstall As String = IO.Path.Combine(Application.StartupPath, "config", "install-optional-apps.json")

        Dim emptyJson As String = "{""applications"": []}"
        IO.File.WriteAllText(jsonPathUninstall, emptyJson, Encoding.UTF8)



        Dim jsonPath As String = IO.Path.Combine(Application.StartupPath, "config", "install-optional-apps.json")

        ' 1. Load JSON lama (kalau ada)
        Dim root As AppConfigRoot

        If File.Exists(jsonPath) Then
            Dim existingJson = File.ReadAllText(jsonPath)
            root = Newtonsoft.Json.JsonConvert.DeserializeObject(Of AppConfigRoot)(existingJson)
        Else
            root = New AppConfigRoot With {
                .applications = New List(Of AppConfig)
            }
        End If

        ' 2. Detect extension installer
        Dim ext As String = IO.Path.GetExtension(installerPath).ToLower()

        Dim silentArgs As String = "/install"   ' default untuk EXE

        If ext = ".msi" Then
            silentArgs = ""      ' MSI → Full UI
            ' atau: silentArgs = "/qf"
        End If

        ' 3. Build object
        Dim app As New AppConfig With {
            .name = name,
            .installerPath = installerPath,
            .silentArgs = silentArgs,
            .detectName = name
        }

        ' 4. Cek existing
        Dim existingApp = root.applications.FirstOrDefault(
        Function(a) a.name.Equals(name, StringComparison.OrdinalIgnoreCase)
        )

        If existingApp IsNot Nothing Then
            existingApp.installerPath = installerPath
            existingApp.silentArgs = silentArgs
            existingApp.detectName = name
        Else
            root.applications.Add(app)
        End If

        ' 4. Serialize ulang ke JSON
        Dim newJson = Newtonsoft.Json.JsonConvert.SerializeObject(
            root,
            Newtonsoft.Json.Formatting.Indented
        )

        ' 5. Simpan ke file
        File.WriteAllText(jsonPath, newJson)

        'MessageBox.Show("Data aplikasi berhasil disimpan ke JSON")
    End Sub

    Private Sub frmInstallApp_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class