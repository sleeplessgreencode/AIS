Public Class FrmSPR
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

        If IsPostBack = False Then
            Call BindJob()
            Call BindGrid()
        End If
    End Sub
    Private Sub BindGrid()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM PRHdr WHERE JobNo=@P1 ORDER BY NoSPR DESC"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With

            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    GridData.DataSource = DtGrid
                    GridData.DataBind()
                End Using
            End Using
        End Using
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
    Protected Sub DDLJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindGrid()
    End Sub
    Private Sub GridData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridData.PageIndexChanging
        GridData.PageIndex = e.NewPageIndex
        GridData.DataBind()
        Call BindGrid()
    End Sub
    Protected Sub BtnAddSPR_Click(sender As Object, e As EventArgs) Handles BtnAddSPR.Click
        If DDLJob.Value = "0" Then
            LblErr.Text = "Belum pilih Job."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        Session("SPR") = "NEW|" & DDLJob.Text & "||"

        Response.Redirect("FrmEntrySPR.aspx")
        Exit Sub
    End Sub
    Private Sub GridData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData.RowCommand
        Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)
        Dim Approved As Boolean = False

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT ApprovedBy FROM PRHdr Where NoSPR=@P1"
                .Parameters.AddWithValue("@P1", SelectRecord.Cells(1).Text)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    If String.IsNullOrEmpty(RsFind("ApprovedBy").ToString) = False Then
                        Approved = True
                    End If
                End If
            End Using
        End Using

        If e.CommandName = "BtnSelect" Then
            If Approved = False Then
                Session("SPR") = "UPD|" & DDLJob.Text & "|" & SelectRecord.Cells(1).Text & "|"
            Else
                Session("SPR") = "SEE|" & DDLJob.Text & "|" & SelectRecord.Cells(1).Text & "|"
            End If
            Response.Redirect("FrmEntrySPR.aspx")
        ElseIf e.CommandName = "BtnPrint" Then
            If Approved = False Then
                LblErr.Text = SelectRecord.Cells(1).Text & " belum di-Approved."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            Session("Print") = SelectRecord.Cells(1).Text & "|" & DDLJob.Text
            Dim Url As String = "FrmRptSPR.aspx"

            With DialogWindow1
                .TargetUrl = Url
                .Open()
            End With
        End If
    End Sub
End Class