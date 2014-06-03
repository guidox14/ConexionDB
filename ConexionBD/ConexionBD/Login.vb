Imports System.Data.SqlClient

Public Class Login

    Private Sub Login_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Login_Button.Click
        Dim nombreUsuario As String
        Dim password As String

        nombreUsuario = NombreUsuario_TextBox.Text
        password = Password_TextBox.Text

        If (IsNumeric(password)) Then
            'Llama al método que hace la conexión a la base de datos
            If (LoginBD(nombreUsuario, password)) Then
                Dim pp As New PantallaPrincipal
                pp.Show()
            Else
                Dim noLogin = "El usuario o la contraseña están incorrectos"
                MsgBox(noLogin, MsgBoxStyle.Critical, "Error en el login")
            End If
        Else
            Dim mensajeError = "El password no viene en formato numerico"
            MsgBox(mensajeError, MsgBoxStyle.Exclamation, "Error en el login")
        End If

    End Sub

    Private Function LoginBD(ByVal nombre As String, ByVal pass As Integer) As Boolean
        Dim connectionBuilder As New SqlConnectionStringBuilder
        connectionBuilder.DataSource = "COMARODRIGUEZG_\SQLEXPRESS"
        'connectionBuilder.AttachDBFilename =
        '   "Programacion_2.mdf"
        connectionBuilder.IntegratedSecurity = True
        connectionBuilder.ConnectTimeout = 30
        connectionBuilder.UserInstance = True

        Dim conexion As New SqlConnection(connectionBuilder.ConnectionString)
        conexion.Open()
        'conexion.ChangeDatabase("Programacion_2")
        Dim sqlCommand As SqlCommand = conexion.CreateCommand
        Dim sqlTrans As SqlTransaction
        sqlTrans = conexion.BeginTransaction(IsolationLevel.ReadCommitted)
        sqlCommand.Connection = conexion
        sqlCommand.Transaction = sqlTrans

        sqlCommand.CommandText = "use Programacion_2"
        sqlCommand.ExecuteNonQuery()

        Dim command = "SELECT Nombre" &
                            ", Apellidos" &
                            ", Alias" &
                            ", Pass" &
                            "FROM Usuarios WHERE Nombre = 'Adrian'"
        sqlCommand.CommandText = command
        Dim sqlReader As SqlDataReader
        sqlReader = sqlCommand.ExecuteReader()
        sqlTrans.Commit()

        conexion.Close()

        Return True
    End Function

End Class
