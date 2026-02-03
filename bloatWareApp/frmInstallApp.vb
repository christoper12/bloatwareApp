Imports System.IO

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

        ' 2. Ambil data dari UI (DINAMIS)
        Dim app As New AppConfig With {
            .name = name,
            .installerPath = installerPath,
            .silentArgs = "/silent /install",
            .detectName = name
        }

        ' 3. Cek apakah aplikasi sudah ada (berdasarkan name)
        Dim existingApp = root.applications.FirstOrDefault(Function(a) a.name.Equals(name, StringComparison.OrdinalIgnoreCase))

        If existingApp IsNot Nothing Then
            ' Update data
            existingApp.installerPath = installerPath
            existingApp.silentArgs = "/silent /install"
            existingApp.detectName = name
        Else
            ' Insert baru
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
End Class