Imports System.IO
Imports Ionic.Zip
Imports System.Collections.Generic

Public Class Unzip
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "KO") = False Then
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

    Protected Sub Upload(sender As Object, e As EventArgs)
        Dim extractPath As String = Server.MapPath("~/Files/")
        Using zip As ZipFile = ZipFile.Read(FileUpload1.PostedFile.InputStream)
            zip.Password = "123456"
            zip.ExtractAll(extractPath, ExtractExistingFileAction.DoNotOverwrite)
            GridView1.DataSource = zip.Entries
            GridView1.DataBind()
        End Using

    End Sub

End Class