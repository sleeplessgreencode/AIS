Public Class FrmJaminan
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "Jaminan") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Call BindJob()
            Call BindGrid()
        End If

    End Sub

    Private Sub BindGrid()
        Dim CmdGrid As New Data.SqlClient.SqlCommand
        With CmdGrid
            .Connection = Conn
            .CommandType = CommandType.Text
            '.CommandText = "SELECT * FROM Jaminan WHERE JobNo=@P1 ORDER BY DrTgl"
            .CommandText = "SELECT A.*, B.CompanyNm FROM Jaminan A JOIN Company B ON A.CompanyId=B.CompanyId WHERE A.JobNo=@P1 ORDER BY A.DrTgl"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
        End With

        Dim DaGrid As New Data.SqlClient.SqlDataAdapter
        DaGrid.SelectCommand = CmdGrid
        Dim DtGrid As New Data.DataTable
        DaGrid.Fill(DtGrid)
        GridData.DataSource = DtGrid
        GridData.DataBind()

        DaGrid.Dispose()
        DtGrid.Dispose()
        CmdGrid.Dispose()

    End Sub

    Private Sub GridData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            Session("Jaminan") = DDLJob.Value & "|" & SelectRecord.Cells(3).Text & "|" & SelectRecord.Cells(1).Text

            TglKembali.Value = DBNull.Value

            TxtAction.Text = "UPD"
            If SelectRecord.Cells(10).Text <> "&nbsp;" Then TglKembali.Date = SelectRecord.Cells(10).Text

            PopEntry.ShowOnPageLoad = True

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
            .CommandText = "SELECT JobNo,JobNm FROM Job WHERE StatusJob!=@P1"
            .Parameters.AddWithValue("@P1", "Proposal")
        End With
        Dim RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
        While RsIsi.Read
            If AksesJob = "*" Then
                DDLJob.Items.Add(RsIsi("JobNo") & " - " & RsIsi("JobNm"), RsIsi("JobNo"))
            Else
                If Array.IndexOf(AksesJob.Split(","), RsIsi("JobNo")) >= 0 Then
                    DDLJob.Items.Add(RsIsi("JobNo") & " - " & RsIsi("JobNm"), RsIsi("JobNo"))
                End If
            End If
        End While
        RsIsi.Close()
        CmdIsi.Dispose()

        DDLJob.Value = "0"
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click

        Dim CmdEdit As New Data.SqlClient.SqlCommand
        With CmdEdit
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "UPDATE Jaminan SET TglKembali=@P1,UserKembali=@P2 WHERE JobNo=@P3 AND Tipe=@P4 AND CompanyId=@P5"
            .Parameters.AddWithValue("@P1", TglKembali.Date)
            .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P3", Session("Jaminan").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P4", Session("Jaminan").ToString.Split("|")(1))
            .Parameters.AddWithValue("@P5", Session("Jaminan").ToString.Split("|")(2))
            .ExecuteNonQuery()
            .Dispose()
        End With

        Call BindGrid()

        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As System.EventArgs) Handles BtnCancel.Click
        Session.Remove("Jaminan")
        PopEntry.ShowOnPageLoad = False
    End Sub

    Protected Sub DDLJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindGrid()
    End Sub

End Class