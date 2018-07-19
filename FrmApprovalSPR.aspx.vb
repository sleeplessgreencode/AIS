Public Class FrmApprovalSPR
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "ApprovalKO") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            PrdAwal.Date = DateSerial(Year(Today), Month(Today), 1)

            If Month(Today) = 12 Then
                PrdAkhir.Date = DateSerial(Year(Today) + 1, 1, 1).AddDays(-1)
            Else
                PrdAkhir.Date = DateSerial(Year(Today), Month(Today) + 1, 1).AddDays(-1)
            End If

            Call BindGridBelumApproved()
            Call BindGridApproved()
        End If
    End Sub
    Private Sub BindGridBelumApproved()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM PRHdr WHERE TglSPR>=@P1 AND TglSPR<=@P2 " & _
                               "AND TimeApproved IS NULL ORDER BY NoSPR DESC"
                .Parameters.AddWithValue("@P1", PrdAwal.Date)
                .Parameters.AddWithValue("@P2", PrdAkhir.Date)
            End With

            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    GridBelumApproved.DataSource = DtGrid
                    GridBelumApproved.DataBind()
                End Using
            End Using
        End Using

    End Sub
    Private Sub BindGridApproved()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM PRHdr WHERE TglSPR>=@P1 AND TglSPR<=@P2 AND TimeApproved IS NOT NULL ORDER BY NoSPR DESC"
                .Parameters.AddWithValue("@P1", PrdAwal.Date)
                .Parameters.AddWithValue("@P2", PrdAkhir.Date)
            End With

            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    GridApproved.DataSource = DtGrid
                    GridApproved.DataBind()
                End Using
            End Using
        End Using
    End Sub
    Private Sub GridBelumApproved_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridBelumApproved.PageIndexChanging
        GridBelumApproved.PageIndex = e.NewPageIndex
        GridBelumApproved.DataBind()
        Call BindGridBelumApproved()
    End Sub
    Private Sub GridApproved_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridApproved.PageIndexChanging
        GridApproved.PageIndex = e.NewPageIndex
        GridApproved.DataBind()
        Call BindGridApproved()
    End Sub
    Private Sub PrdAwal_ValueChanged(sender As Object, e As System.EventArgs) Handles PrdAwal.ValueChanged
        Call BindGridBelumApproved()
        Call BindGridApproved()
    End Sub
    Private Sub PrdAkhir_ValueChanged(sender As Object, e As System.EventArgs) Handles PrdAkhir.ValueChanged
        Call BindGridBelumApproved()
        Call BindGridApproved()
    End Sub
    Private Sub GridBelumApproved_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridBelumApproved.RowCommand
        If e.CommandName = "BtnSelectBlmApprove" Then
            Dim SelectRecord As GridViewRow = GridBelumApproved.Rows(e.CommandArgument)

            Session("SPR") = "SEE_APPROVALSPR|" & SelectRecord.Cells(1).Text & "|" & SelectRecord.Cells(2).Text & "|"
            Response.Redirect("FrmEntrySPR.aspx")

        ElseIf e.CommandName = "BtnApprove" Then
            Dim SelectRecord As GridViewRow = GridBelumApproved.Rows(e.CommandArgument)

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT ApprovedBy FROM PRHdr WHERE NoSPR=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(2).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        If String.IsNullOrEmpty(RsFind("ApprovedBy").ToString) = False Then
                            LblErr.Text = SelectRecord.Cells(2).Text & " sudah di-Approve."
                            ErrMsg.ShowOnPageLoad = True
                            Exit Sub
                        End If
                    End If
                End Using
            End Using

            Session("Approve") = "Approve|" & SelectRecord.Cells(2).Text

            LblApproval.Text = "Anda yakin ingin Approve data berikut?" & "<br />" & _
                            SelectRecord.Cells(2).Text & " - " & SelectRecord.Cells(5).Text
            ConfirmMsg.ShowOnPageLoad = True
        End If
    End Sub
    Private Sub GridBelumApproved_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridBelumApproved.RowDataBound
    End Sub
    Protected Sub BtnConfirm_Click(sender As Object, e As EventArgs) Handles BtnConfirm.Click
        Dim NoSPR As String = Session("Approve").ToString.Split("|")(1)
        Dim Action As String = Session("Approve").ToString.Split("|")(0)

        Using CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE PRHdr SET ApprovedBy=@P1,TimeApproved=@P2 WHERE NoSPR=@P3"
                .Parameters.AddWithValue("@P1", If(Action = "UnApprove", DBNull.Value, Session("User").ToString.Split("|")(0)))
                .Parameters.AddWithValue("@P2", If(Action = "UnApprove", DBNull.Value, Now))
                .Parameters.AddWithValue("@P3", NoSPR)
                .ExecuteNonQuery()
            End With
        End Using

        Session.Remove("Approve")
        Call BindGridBelumApproved()
        Call BindGridApproved()
    End Sub
    Private Sub GridApproved_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridApproved.RowCommand
        If e.CommandName = "BtnSelectApprove" Then
            Dim SelectRecord As GridViewRow = GridApproved.Rows(e.CommandArgument)

            Session("SPR") = "SEE_APPROVALSPR|" & SelectRecord.Cells(1).Text & "|" & SelectRecord.Cells(2).Text & "|"
            Response.Redirect("FrmEntrySPR.aspx")

        ElseIf e.CommandName = "BtnUnApprove" Then
            Dim SelectRecord As GridViewRow = GridApproved.Rows(e.CommandArgument)

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT NoSPR FROM KoHdr WHERE NoSPR=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(2).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        LblErr.Text = "Surat Permintaan Material/Alat tidak dapat di-unapprove karena " & _
                                      "sudah digunakan oleh salah satu KO."
                        ErrMsg.ShowOnPageLoad = True
                        Exit Sub
                    End If
                End Using
            End Using

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT ApprovedBy FROM PRHdr WHERE NoSPR=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(2).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        If String.IsNullOrEmpty(RsFind("ApprovedBy").ToString) = True Then
                            LblErr.Text = SelectRecord.Cells(0).Text & " sudah di-UnApprove."
                            ErrMsg.ShowOnPageLoad = True
                            Exit Sub
                        End If
                    End If
                End Using
            End Using

            Session("Approve") = "UnApprove|" & SelectRecord.Cells(2).Text

            BtnConfirm.Text = "UNAPPROVE"
            LblApproval.Text = "Anda yakin ingin UnApprove data berikut?" & "<br />" & _
                            SelectRecord.Cells(2).Text & " - " & SelectRecord.Cells(5).Text
            ConfirmMsg.ShowOnPageLoad = True
        End If

    End Sub

    Private Sub GridApproved_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridApproved.RowDataBound
    End Sub
End Class