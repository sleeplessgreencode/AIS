Public Class FrmClosingKO
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "ClosingKO") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Call BindJob()
        End If

        If Session("Job") <> "" Then
            DDLJob.Value = Session("Job")
            Session.Remove("Job")
        End If

        Call BindGrid()

    End Sub

    Private Sub BindGrid()
        Dim CmdGrid As New Data.SqlClient.SqlCommand
        With CmdGrid
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT A.*, B.VendorNm, " & _
                           "(SELECT ISNULL(SUM(Amount),0) FROM BLE WHERE NoKO=A.NoKO) AS PaymentAmount " & _
                           "FROM KoHdr A JOIN Vendor B ON A.VendorId=B.VendorId WHERE JobNo=@P1 " & _
                           "AND NoKO LIKE @P2 AND KategoriId=@P3 AND ApprovedBy IS NOT NULL ORDER BY NoKO DESC"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", "%" + TxtFind.Text + "%")
            .Parameters.AddWithValue("@P3", "KONTRAK")
        End With

        Dim DaGrid As New Data.SqlClient.SqlDataAdapter
        DaGrid.SelectCommand = CmdGrid
        Dim DtGrid As New Data.DataTable
        DaGrid.Fill(DtGrid)
        GridData.DataSource = DtGrid
        'Call GetTotal()
        GridData.DataBind()

        DaGrid.Dispose()
        DtGrid.Dispose()
        CmdGrid.Dispose()


    End Sub

    Private Sub GridData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridData.PageIndexChanging
        GridData.PageIndex = e.NewPageIndex
        GridData.DataBind()
    End Sub

    Private Sub GridData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            Session("KO") = "SEE_CLOSINGKO|" & DDLJob.Text & "|" & SelectRecord.Cells(1).Text & "|"
            Response.Redirect("FrmEntryKO.aspx")

        ElseIf e.CommandName = "BtnClose" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            If SelectRecord.Cells(7).Text <> "&nbsp;" Then
                LblErr.Text = SelectRecord.Cells(1).Text & " sudah di-tutup."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If

            Session("KO") = SelectRecord.Cells(1).Text

            LblDel.Text = "Anda yakin ingin tutup kontrak berikut?" & "<br />" & _
                           SelectRecord.Cells(1).Text & " - " & SelectRecord.Cells(3).Text
            DelMsg.ShowOnPageLoad = True
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

    Protected Sub DDLJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindGrid()
    End Sub

    'Private Sub GridData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridData.RowDataBound
    '    If e.Row.RowType = DataControlRowType.Footer Then
    '        e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
    '        e.Row.Cells(4).Text = "Total : "
    '        e.Row.Cells(4).Font.Bold = True
    '    End If

    'End Sub

    'Private Sub GetTotal()
    '    Dim CmdFind As New Data.SqlClient.SqlCommand
    '    With CmdFind
    '        .Connection = Conn
    '        .CommandType = CommandType.Text
    '        .CommandText = "SELECT FORMAT(SUM(A.SubTotal - A.DiscAmount + A.PPN),'N0'), " & _
    '                       "(SELECT FORMAT(ISNULL(SUM(Amount),0),'N0') FROM BLE WHERE NoKO=A.NoKO) AS PaymentAmount " & _
    '                       "FROM KoHdr A JOIN Vendor B ON A.VendorId=B.VendorId WHERE JobNo=@P1 " & _
    '                       "AND NoKO LIKE @P2 AND KategoriId=@P3 AND ApprovedBy IS NOT NULL"
    '        .Parameters.AddWithValue("@P1", DDLJob.Value)
    '        .Parameters.AddWithValue("@P2", "%" + TxtFind.Text + "%")
    '        .Parameters.AddWithValue("@P3", "KONTRAK")
    '    End With
    '    Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
    '    If RsFind.Read Then
    '        GridData.Columns(5).FooterText = RsFind(0).ToString
    '        GridData.Columns(6).FooterText = RsFind(1).ToString
    '    Else
    '        GridData.Columns(5).FooterText = "0"
    '        GridData.Columns(6).FooterText = "0"
    '    End If

    '    GridData.Columns(5).FooterStyle.HorizontalAlign = HorizontalAlign.Right
    '    GridData.Columns(6).FooterStyle.HorizontalAlign = HorizontalAlign.Right
    'End Sub

    Protected Sub BtnDel_Click(sender As Object, e As EventArgs) Handles BtnDel.Click
        Dim CmdEdit As New Data.SqlClient.SqlCommand
        With CmdEdit
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "UPDATE KoHdr SET ClosedBy=@P1,TimeClosed=@P2 WHERE NoKO=@P3"
            .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P2", Now)
            .Parameters.AddWithValue("@P3", Session("KO"))
            .ExecuteNonQuery()
            .Dispose()
        End With
        Session.Remove("KO")

        Call BindGrid()
    End Sub

End Class