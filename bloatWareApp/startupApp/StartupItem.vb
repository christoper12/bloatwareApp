Public Class StartupItem
    ' UI
    Public Property Name As String
    Public Property Command As String
    Public Property Source As String
    Public Property Location As String
    Public Property Type As StartupType
    Public Property IsEnabled As Boolean

    ' === IDENTIFIER ===
    Public Property RegistryValueName As String
    Public Property RegistryPath As String
    Public Property TaskName As String
    Public Property TaskPath As String
    Public Property FilePath As String

    ' UX
    Public Property CanDisable As Boolean
    Public Property DisableReason As String

    ' === UX CLASSIFICATION ===
    Public Property Category As StartupCategory
    Public Property Note As String
End Class
