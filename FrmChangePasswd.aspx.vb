Public Class FrmChangePasswd
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "ChangePasswd") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT Password FROM Login WHERE UserID=@P1"
            .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            If Encrypt(TxtOldPw.Text) <> RsFind("Password") Then
                LblErr.Text = "Your old password is invalid."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
        End If
        RsFind.Close()
        CmdFind.Dispose()

        If TxtOldPw.Text = TxtNewPw.Text Then
            LblErr.Text = "Please use a different password from your old password."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtNewPw.Text <> TxtConfirmPw.Text Then
            LblErr.Text = "New password and Confirm password does not match."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If


        Dim CmdEdit As New Data.SqlClient.SqlCommand
        With CmdEdit
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "UPDATE Login SET Password=@P1,UserEntry=@P2,TimeEntry=@P3 WHERE UserID=@P4"
            .Parameters.AddWithValue("@P1", Encrypt(TxtNewPw.Text))
            .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P3", Now)
            .Parameters.AddWithValue("@P4", Session("User").ToString.Split("|")(1))
            .ExecuteNonQuery()
            .Dispose()
        End With

        LblErr.Text = "Changed password done."
        ErrMsg.ShowOnPageLoad = True
        Exit Sub

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