Public Class FrmNotaAkuntansi
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "NotaAkuntansi") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            ViewState("ColumnName") = "TglJurnal"
            ViewState("SortOrder") = "DESC"

            TxtTgl2.Date = Today
            TxtTgl1.Date = Today.AddDays(-30)
            Call BindJob()
            Call BindGrid()
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindSite()
        DDLSite.Items.Clear()
        DDLSite.Items.Add(String.Empty, String.Empty)

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM GlReff WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                DDLSite.Items.Add(RsFind("Site"), RsFind("Site"))
            End While
        End Using

        DDLSite.SelectedIndex = 0

    End Sub

    Private Sub BindJob()
        Dim AksesJob As String = ""
        Using CmdFind As New Data.SqlClient.SqlCommand
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
        End Using

        DDLJob.Items.Clear()
        DDLJob.Items.Add(String.Empty, String.Empty)

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo, JobNm FROM Job"
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsFind("JobNo")) >= 0 Then
                    DDLJob.Items.Add(RsFind("JobNo") & " - " & RsFind("JobNm"), RsFind("JobNo"))
                End If
            End While
        End Using

    End Sub

    Private Sub BindGrid()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                If TxtFind.Text = "" Then
                    .CommandText = "SELECT A.*, B.Nama FROM JurnalEntry A JOIN Identitas B ON A.JobNo=B.JobNo AND A.Identitas=B.Identitas WHERE " + _
                                   "A.JobNo=@P1 AND A.Site=@P2 AND A.TglJurnal>=@P3 AND A.TglJurnal<=@P4 AND A.PC='P' AND ApprovedBy IS NOT NULL "
                Else
                    .CommandText = "SELECT A.*, B.Nama FROM JurnalEntry A JOIN Identitas B ON A.JobNo=B.JobNo AND A.Identitas=B.Identitas WHERE " + _
                                   "A.JobNo=@P1 AND A.Site=@P2 AND A.TglJurnal>=@P3 AND A.TglJurnal<=@P4 AND A.PC='P' AND ApprovedBy IS NOT NULL " + _
                                   "AND A.NoJurnal LIKE '%" + TxtFind.Text + "%' "
                End If
                .CommandText = .CommandText + If(ViewState("ColumnName") = "", "", "ORDER BY " + ViewState("ColumnName") + " " + ViewState("SortOrder"))
                .Parameters.AddWithValue("@P1", If(DDLJob.Text = String.Empty, String.Empty, DDLJob.Value))
                .Parameters.AddWithValue("@P2", If(DDLSite.Text = String.Empty, String.Empty, DDLSite.Value))
                .Parameters.AddWithValue("@P3", TxtTgl1.Date)
                .Parameters.AddWithValue("@P4", TxtTgl2.Date)
            End With
            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DsGrid As New Data.DataSet
                    DaGrid.Fill(DsGrid)
                    With GridView
                        .DataSource = DsGrid
                        .DataBind()
                    End With
                End Using
            End Using
        End Using

    End Sub

    Private Sub DDLJob_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindSite()
        Call BindGrid()
    End Sub

    Private Sub DDLSite_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLSite.SelectedIndexChanged
        Call BindGrid()
    End Sub

    Private Sub GridView_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView.PageIndexChanging
        GridView.PageIndex = e.NewPageIndex
        GridView.DataBind()
        Call BindGrid()
    End Sub

    Private Sub GridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
        If e.CommandName = "BtnPrint" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)

            Session("Data") = DDLJob.Value + "|" + DDLSite.Value + "|" + SelectRecord.Cells(1).Text
            Response.Redirect("FrmRptNA.aspx")

        End If
    End Sub

    Private Sub GridView_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim link As LinkButton = TryCast(e.Row.Cells(9).Controls(0), LinkButton)

            If link IsNot Nothing Then
                link.Attributes.Add("onclick", "OpenNewTab();")
            End If

        End If
    End Sub

    Private Sub GridView_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView.Sorting
        If e.SortExpression = ViewState("ColumnName").ToString() Then
            If ViewState("SortOrder").ToString() = "ASC" Then
                ViewState("SortOrder") = "DESC"
            Else
                ViewState("SortOrder") = "ASC"
            End If
        Else
            ViewState("ColumnName") = e.SortExpression
            ViewState("SortOrder") = "ASC"
        End If

        Call BindGrid()

    End Sub

    Private Sub BtnFind_Click(sender As Object, e As System.EventArgs) Handles BtnFind.Click
        Call BindGrid()
    End Sub

    Private Sub TxtTgl1_ValueChanged(sender As Object, e As System.EventArgs) Handles TxtTgl1.ValueChanged
        Call BindGrid()
    End Sub

    Private Sub TxtTgl2_ValueChanged(sender As Object, e As System.EventArgs) Handles TxtTgl2.ValueChanged
        Call BindGrid()
    End Sub

End Class