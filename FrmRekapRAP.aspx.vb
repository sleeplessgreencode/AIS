Public Class FrmRekapRAP
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If CheckAkses() = False Then
            Response.Redirect("Default.aspx")
            Exit Sub
        End If

        If IsPostBack = False Then
            Call BindJob()
            Call BindAlokasi()
        End If

        If Session("Job") <> "" Then
            DDLJob.Value = Session("Job").ToString.Split("|")(0)
            DDLAlokasi.Value = Session("Job").ToString.Split("|")(1)
            Session.Remove("Job")
        End If
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindJob()
        Dim AksesJob As String = ""
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT AksesJob FROM Login WHERE UserID=@P1"
            .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            AksesJob = RsFind("AksesJob")
        End If
        RsFind.Close()
        CmdFind.Dispose()

        DDLJob.Items.Clear()
        DDLJob.Items.Add("Pilih salah satu", "0")
        Dim CmdIsi As New Data.SqlClient.SqlCommand
        With CmdIsi
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT JobNo,JobNm FROM Job"
        End With
        Dim RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
        While RsIsi.Read
            If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsIsi("JobNo")) >= 0 Then
                DDLJob.Items.Add(RsIsi("JobNo") & " - " & RsIsi("JobNm"), RsIsi("JobNo"))
            End If
        End While
        RsIsi.Close()
        CmdIsi.Dispose()

        DDLJob.Value = "0"
    End Sub

    Private Sub BindAlokasi()
        DDLAlokasi.Items.Clear()
        DDLAlokasi.Items.Add("Pilih salah satu", "0")

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT Alokasi,Keterangan FROM Alokasi"
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        While RsFind.Read
            DDLAlokasi.Items.Add(RsFind("Alokasi") & " - " & RsFind("Keterangan"), RsFind("Alokasi"))
        End While
        RsFind.Close()
        CmdFind.Dispose()

        DDLAlokasi.Value = "0"

    End Sub

    Private Function CheckAkses() As Boolean
        If Session("User") = "" Then
            Return False
        Else
            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT AksesMenu FROM Login WHERE UserID=@P1"
                .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
            End With
            Dim AksesMenu As String = ""
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                AksesMenu = RsFind("AksesMenu").ToString
            End If
            RsFind.Close()
            CmdFind.Dispose()

            If AksesMenu = "*" Then Return True
            If Array.IndexOf(AksesMenu.Split(","), _
                HttpContext.Current.Request.Url.AbsolutePath.Split("/")(1).Split(".")(0)) < 0 Then
                Return False
            End If
        End If

        Return True
    End Function

    Protected Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        If DDLJob.Value = "0" Then
            LblErr.Text = "Belum pilih Job."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If DDLAlokasi.Value = "0" Then
            LblErr.Text = "Belum pilih Alokasi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        Session("Print") = DDLJob.Value + "|" + DDLAlokasi.Value
        Response.Redirect("FrmRptRekapRAP.aspx")
    End Sub
End Class