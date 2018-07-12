Public Class _Default
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()
        Session.RemoveAll()

        TxtUser.Focus()

    End Sub

    Protected Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click
        Using CmdLogin As New Data.SqlClient.SqlCommand
            With CmdLogin
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Login WHERE UserID=@P1 AND Password=@P2"
                .Parameters.AddWithValue("@P1", TxtUser.Text)
                .Parameters.AddWithValue("@P2", Encrypt(TxtPasswd.Text))
            End With
            Using RsLogin As Data.SqlClient.SqlDataReader = CmdLogin.ExecuteReader
                If RsLogin.Read Then
                    Session("User") = UCase(RsLogin("UserName")) + "|" & UCase(RsLogin("UserID"))
                    Response.Redirect("FrmMain.aspx")
                Else
                    MsgBox1.alert("UserID / Password salah." + "\nSilahkan coba lagi.")
                    TxtUser.Focus()
                    Exit Sub
                End If
            End Using
        End Using

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Function Encrypt(clearText As String) As String

        Dim EncryptionKey As String = "sKlpxvB8hR43zYt3CQRwO1K94Q39k5m7"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create()
            Dim pdb As New System.Security.Cryptography.Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
             &H65, &H64, &H76, &H65, &H64, &H65, _
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New System.IO.MemoryStream()
                Using cs As New System.Security.Cryptography.CryptoStream(ms, encryptor.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText

    End Function
End Class