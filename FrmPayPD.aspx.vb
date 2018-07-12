Public Class FrmPayPD
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "PayPD") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            TabPage.ActiveTabIndex = 0
            Call BindGrid()
            Call BindGrid1()
        End If

    End Sub

    Private Sub BindGrid()
        'Dim AksesJob As String = ""
        'Dim CSV As String = ""
        'Using CmdFind As New Data.SqlClient.SqlCommand
        '    With CmdFind
        '        .Connection = Conn
        '        .CommandType = CommandType.Text
        '        .CommandText = "SELECT AksesJob FROM Login WHERE UserID=@P1"
        '        .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
        '    End With
        '    Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        '        If RsFind.Read Then
        '            AksesJob = RsFind("AksesJob")
        '        End If
        '    End Using
        'End Using

        'Using CmdFind As New Data.SqlClient.SqlCommand
        '    With CmdFind
        '        .Connection = Conn
        '        .CommandType = CommandType.Text
        '        .CommandText = "SELECT JobNo FROM Job"
        '    End With
        '    Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        '        While RsFind.Read
        '            If Array.IndexOf(AksesJob.Split(","), RsFind("JobNo").ToString) > -1 Then
        '                CSV = If(CSV = "", "'" + RsFind("JobNo") + "'", CSV + "," + "'" + RsFind("JobNo") + "'")
        '            End If
        '        End While
        '    End Using
        'End Using

        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, B.* FROM PdHdr A JOIN Job B ON A.JobNo=B.JobNo " + _
                               "WHERE A.KSO=0 AND ApprovedByDK IS NOT NULL AND TglBayar IS NULL AND RejectBy IS NULL " + GetFilter1() + GetFilter2() + _
                               " ORDER BY TimeApprovedDK DESC, TotalPD DESC"
            End With
            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    GridPD.DataSource = DtGrid
                    GridPD.DataBind()
                End Using
            End Using
        End Using

    End Sub

    Private Sub BindGrid1()
        'Dim AksesJob As String = ""
        'Dim CSV As String = ""
        'Using CmdFind As New Data.SqlClient.SqlCommand
        '    With CmdFind
        '        .Connection = Conn
        '        .CommandType = CommandType.Text
        '        .CommandText = "SELECT AksesJob FROM Login WHERE UserID=@P1"
        '        .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
        '    End With
        '    Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        '        If RsFind.Read Then
        '            AksesJob = RsFind("AksesJob")
        '        End If
        '    End Using
        'End Using

        'Using CmdFind As New Data.SqlClient.SqlCommand
        '    With CmdFind
        '        .Connection = Conn
        '        .CommandType = CommandType.Text
        '        .CommandText = "SELECT JobNo FROM Job"
        '    End With
        '    Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        '        While RsFind.Read
        '            If Array.IndexOf(AksesJob.Split(","), RsFind("JobNo").ToString) > -1 Then
        '                CSV = If(CSV = "", "'" + RsFind("JobNo") + "'", CSV + "," + "'" + RsFind("JobNo") + "'")
        '            End If
        '        End While
        '    End Using
        'End Using

        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, B.* FROM PdHdr A JOIN Job B ON A.JobNo=B.JobNo " + _
                               "WHERE A.KSO=0 AND A.ApprovedByDK IS NOT NULL AND A.TglBayar IS NOT NULL " + GetFilter1() + GetFilter2() + _
                               " ORDER BY A.TglBayar DESC"
            End With
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

    Private Sub GridPD_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridPD.PageIndexChanging
        GridPD.PageIndex = e.NewPageIndex
        GridPD.DataBind()
        Call BindGrid()
    End Sub

    Private Sub GridPD_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridPD.RowCommand
        If e.CommandName = "BtnPay" Then
            Dim SelectRecord As GridViewRow = GridPD.Rows(e.CommandArgument)

            If SelectRecord.Cells(10).Text = "&nbsp;" Then
                Session("PD") = "UPD|" + SelectRecord.Cells(1).Text + "|FrmPayPD.aspx"
            Else
                Session("PD") = "SEE|" + SelectRecord.Cells(1).Text + "|FrmPayPD.aspx"
            End If

            Response.Redirect("FrmEntryPayment.aspx")

        End If
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Function GetFilter1() As String
        If DDLField1.Value <> "0" Then
            Return "AND " + DDLField1.Value + GetFilterFunction1()
        End If

        Return ""
    End Function

    Private Function GetFilterFunction1() As String
        If DDLFilterBy1.Value = "0" Then
            If DDLField1.Value <> "TglPD" And DDLField1.Value <> "TglBayar" Then
                Return "='" + TxtFind1.Text + "'"
            Else
                Return "='" + TglFilter1.Date + "'"
            End If
        ElseIf DDLFilterBy1.Value = "1" Then
            If DDLField1.Value <> "TglPD" And DDLField1.Value <> "TglBayar" Then
                Return " LIKE '%" + TxtFind1.Text + "%'"
            Else
                Return ""
            End If
        ElseIf DDLFilterBy1.Value = "2" Then
            If DDLField1.Value <> "TglPD" And DDLField1.Value <> "TglBayar" Then
                Return ">=" + TxtFind1.Text
            Else
                Return ">='" + TglFilter1.Date + "'"
            End If
        ElseIf DDLFilterBy1.Value = "3" Then
            If DDLField1.Value <> "TglPD" And DDLField1.Value <> "TglBayar" Then
                Return "<=" + TxtFind1.Text
            Else
                Return "<='" + TglFilter1.Date + "'"
            End If
        End If

        Return ""
    End Function

    Protected Sub DDLField1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLField1.SelectedIndexChanged
        TxtFind1.Text = ""
        TglFilter1.Text = ""
        DDLFilterBy1.Items.Clear()
        TxtFind1.Visible = True
        TglFilter1.Visible = False
        If DDLField1.Value = "TglPD" Or DDLField1.Value = "TglBayar" Then
            TxtFind1.Visible = False
            TglFilter1.Visible = True
            DDLFilterBy1.Items.Add("Equals", "0")
            DDLFilterBy1.Items.Add("Is Greather Than Or Equal To", "2")
            DDLFilterBy1.Items.Add("Is Less Than Or Equal To", "3")
        ElseIf DDLField1.Value = "TotalPD" Then
            TxtFind1.Text = "0"
            DDLFilterBy1.Items.Add("Equals", "0")
            DDLFilterBy1.Items.Add("Is Greather Than Or Equal To", "2")
            DDLFilterBy1.Items.Add("Is Less Than Or Equal To", "3")
        Else
            DDLFilterBy1.Items.Add("Equals", "0")
            DDLFilterBy1.Items.Add("Contains", "1")
        End If
        DDLFilterBy1.Value = "0"
    End Sub

    Protected Sub DDLField2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLField2.SelectedIndexChanged
        TxtFind2.Text = ""
        TglFilter2.Text = ""
        DDLFilterBy2.Items.Clear()
        TxtFind2.Visible = True
        TglFilter2.Visible = False
        If DDLField2.Value = "TglPD" Or DDLField2.Value = "TglBayar" Then
            TxtFind2.Visible = False
            TglFilter2.Visible = True
            DDLFilterBy2.Items.Add("Equals", "0")
            DDLFilterBy2.Items.Add("Is Greather Than Or Equal To", "2")
            DDLFilterBy2.Items.Add("Is Less Than Or Equal To", "3")
        ElseIf DDLField2.Value = "TotalPD" Then
            TxtFind2.Text = "0"
            DDLFilterBy2.Items.Add("Equals", "0")
            DDLFilterBy2.Items.Add("Is Greather Than Or Equal To", "2")
            DDLFilterBy2.Items.Add("Is Less Than Or Equal To", "3")
        Else
            DDLFilterBy2.Items.Add("Equals", "0")
            DDLFilterBy2.Items.Add("Contains", "1")
        End If
        DDLFilterBy2.Value = "0"
    End Sub

    Private Function GetFilter2() As String
        If DDLField2.Value <> "0" Then
            Return " AND " + DDLField2.Value + " " + GetFilterFunction2()
        End If

        Return ""
    End Function

    Private Function GetFilterFunction2() As String
        If DDLFilterBy2.Value = "0" Then
            If DDLField2.Value <> "TglPD" And DDLField2.Value <> "TglBayar" Then
                Return "='" + TxtFind2.Text + "'"
            Else
                Return "='" + TglFilter2.Date + "'"
            End If
        ElseIf DDLFilterBy2.Value = "1" Then
            If DDLField2.Value <> "TglPD" And DDLField2.Value <> "TglBayar" Then
                Return "LIKE '%" + TxtFind2.Text + "%'"
            Else
                Return ""
            End If
        ElseIf DDLFilterBy2.Value = "2" Then
            If DDLField2.Value <> "TglPD" And DDLField2.Value <> "TglBayar" Then
                Return ">=" + TxtFind2.Text
            Else
                Return ">='" + TglFilter2.Date + "'"
            End If
        ElseIf DDLFilterBy2.Value = "3" Then
            If DDLField2.Value <> "TglPD" And DDLField2.Value <> "TglBayar" Then
                Return "<=" + TxtFind2.Text
            Else
                Return "<='" + TglFilter2.Date + "'"
            End If
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

    Private Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        GridView1.DataBind()
        Call BindGrid1()
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "BtnPay" Then
            Dim SelectRecord As GridViewRow = GridView1.Rows(e.CommandArgument)

            If SelectRecord.Cells(10).Text = "&nbsp;" Then
                Session("PD") = "UPD|" + SelectRecord.Cells(1).Text + "|FrmPayPD.aspx"
            Else
                Session("PD") = "SEE|" + SelectRecord.Cells(1).Text + "|FrmPayPD.aspx"
            End If

            Response.Redirect("FrmEntryPayment.aspx")

        End If
    End Sub
End Class