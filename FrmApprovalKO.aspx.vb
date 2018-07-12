Public Class FrmApprovalKO
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

            Call BindGrid()
            Call BindGrid1()
        End If

    End Sub

    Private Sub BindGrid()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, B.VendorNm, C.JobNm FROM KoHdr A " & _
                               "JOIN Vendor B ON A.VendorId=B.VendorId " & _
                               "JOIN Job C ON A.JobNo=C.JobNo " & _
                               "WHERE A.TglKO>=@P1 AND A.TglKO<=@P2 AND A.TimeApproved IS NULL AND A.CanceledBy IS NULL " & _
                               GetFilter1() & " ORDER BY A.NoKO DESC"
                .Parameters.AddWithValue("@P1", PrdAwal.Date)
                .Parameters.AddWithValue("@P2", PrdAkhir.Date)
            End With

            Dim Ttl As Decimal = 0
            Using RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                While RsGrid.Read
                    Ttl += RsGrid("SubTotal") - RsGrid("DiscAmount") + RsGrid("PPN")
                End While
                GridData.Columns(7).FooterText = Format(Ttl, "N0")
                GridData.Columns(7).FooterStyle.HorizontalAlign = HorizontalAlign.Right
            End Using

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

    Private Sub BindGrid1()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, B.VendorNm, C.JobNm FROM KoHdr A " +
                               "JOIN Vendor B ON A.VendorId=B.VendorId " +
                               "JOIN Job C ON A.JobNo=C.JobNo WHERE A.TglKO>=@P1 AND A.TglKO<=@P2 AND A.TimeApproved IS NOT NULL " + GetFilter1() + " ORDER BY A.NoKO DESC"
                .Parameters.AddWithValue("@P1", PrdAwal.Date)
                .Parameters.AddWithValue("@P2", PrdAkhir.Date)
            End With

            Dim Ttl As Decimal = 0
            Using RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                While RsGrid.Read
                    Ttl += RsGrid("SubTotal") - RsGrid("DiscAmount") + RsGrid("PPN")
                End While
                GridView1.Columns(7).FooterText = Format(Ttl, "N0")
                GridView1.Columns(7).FooterStyle.HorizontalAlign = HorizontalAlign.Right
            End Using

            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    GridView1.DataSource = DtGrid
                    GridView1.DataBind()
                End Using
            End Using
        End Using

    End Sub

    Private Sub GridData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridData.PageIndexChanging
        GridData.PageIndex = e.NewPageIndex
        GridData.DataBind()
        Call BindGrid()
    End Sub

    Private Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        GridView1.DataBind()
        Call BindGrid1()
    End Sub

    Private Sub GridData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            Session("KO") = "SEE_APPROVALKO|" & SelectRecord.Cells(1).Text & "|" & SelectRecord.Cells(3).Text & "|"

            If SelectRecord.Cells(6).Text = "KONTRAK" Then
                Response.Redirect("FrmEntryKO.aspx")
            Else
                Session("KO") = Session("KO") & "|"
                Response.Redirect("FrmEntryPO.aspx")
            End If

        ElseIf e.CommandName = "BtnApprove" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT ApprovedBy, CanceledBy FROM KoHdr WHERE NoKO=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(3).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        If String.IsNullOrEmpty(RsFind("ApprovedBy").ToString) = False Then
                            LblErr.Text = SelectRecord.Cells(3).Text & " sudah di-Approve."
                            ErrMsg.ShowOnPageLoad = True
                            Exit Sub
                        End If
                        If String.IsNullOrEmpty(RsFind("CanceledBy").ToString) = False Then
                            LblErr.Text = SelectRecord.Cells(3).Text & " sudah di-batalkan."
                            ErrMsg.ShowOnPageLoad = True
                            Exit Sub
                        End If
                    End If
                End Using
            End Using

            Session("Approve") = "Approve|" & SelectRecord.Cells(3).Text

            LblApproval.Text = "Anda yakin ingin Approve data berikut?" & "<br />" & _
                            SelectRecord.Cells(3).Text & " - " & SelectRecord.Cells(5).Text
            ConfirmMsg.ShowOnPageLoad = True
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub GridData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridData.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(6).Text = "Total : "
            e.Row.Cells(6).Font.Bold = True
        End If

    End Sub

    Protected Sub BtnConfirm_Click(sender As Object, e As EventArgs) Handles BtnConfirm.Click
        Dim NoKO As String = Session("Approve").ToString.Split("|")(1)
        Dim Action As String = Session("Approve").ToString.Split("|")(0)

        Using CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE KoHdr SET ApprovedBy=@P1,TimeApproved=@P2 WHERE NoKO=@P3"
                .Parameters.AddWithValue("@P1", If(Action = "UnApprove", DBNull.Value, Session("User").ToString.Split("|")(0)))
                .Parameters.AddWithValue("@P2", If(Action = "UnApprove", DBNull.Value, Now))
                .Parameters.AddWithValue("@P3", NoKO)
                .ExecuteNonQuery()
            End With
        End Using

        Session.Remove("Approve")
        Call BindGrid()
        Call BindGrid1()

    End Sub

    Private Function GetFilter1() As String
        If DDLField1.Value <> "0" Then
            Return "AND " + DDLField1.Value + GetFilterFunction1()
        End If

        Return ""
    End Function

    Private Function GetFilterFunction1() As String
        If DDLFilterBy1.Value = "0" Then
            Return "='" + TxtFind1.Text + "'"
        ElseIf DDLFilterBy1.Value = "1" Then
            Return " LIKE '%" + TxtFind1.Text + "%'"
        End If

        Return ""
    End Function

    Private Sub BtnFind_Click(sender As Object, e As System.EventArgs) Handles BtnFind.Click
        If TabPage.ActiveTabIndex = 0 Then
            Call BindGrid()
        Else
            Call BindGrid1()
        End If

    End Sub

    Private Sub PrdAwal_ValueChanged(sender As Object, e As System.EventArgs) Handles PrdAwal.ValueChanged
        Call BindGrid()
        Call BindGrid1()

    End Sub

    Private Sub PrdAkhir_ValueChanged(sender As Object, e As System.EventArgs) Handles PrdAkhir.ValueChanged
        Call BindGrid()
        Call BindGrid1()

    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridView1.Rows(e.CommandArgument)

            Session("KO") = "SEE_APPROVALKO|" & SelectRecord.Cells(1).Text & "|" & SelectRecord.Cells(3).Text & "|"

            If SelectRecord.Cells(6).Text = "KONTRAK" Then
                Response.Redirect("FrmEntryKO.aspx")
            Else
                Session("KO") = Session("KO") & "|"
                Response.Redirect("FrmEntryPO.aspx")
            End If

        ElseIf e.CommandName = "BtnUnApprove" Then
            Dim SelectRecord As GridViewRow = GridView1.Rows(e.CommandArgument)

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT ApprovedBy FROM KoHdr WHERE NoKO=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(3).Text)
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

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT NoPD FROM PdHdr WHERE NoKO=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(3).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        LblErr.Text = SelectRecord.Cells(3).Text & " sudah digunakan untuk " & RsFind("NoPD") & "."
                        ErrMsg.ShowOnPageLoad = True
                        Exit Sub
                    End If
                End Using
            End Using

            Session("Approve") = "UnApprove|" & SelectRecord.Cells(3).Text

            LblApproval.Text = "Anda yakin ingin UnApprove data berikut?" & "<br />" & _
                            SelectRecord.Cells(3).Text & " - " & SelectRecord.Cells(5).Text
            ConfirmMsg.ShowOnPageLoad = True
        End If

    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(6).Text = "Total : "
            e.Row.Cells(6).Font.Bold = True
        End If
    End Sub
End Class