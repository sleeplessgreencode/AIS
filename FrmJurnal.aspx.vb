Public Class FrmJurnal
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "JurnalEntry") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            ViewState("ColumnName") = ""
            ViewState("SortOrder") = ""

            TxtTgl2.Date = Today
            TxtTgl1.Date = Today.AddDays(-60)
            Call BindJob()

            If Session("Job") <> String.Empty Then
                DDLJob.Value = Session("Job").ToString.Split("|")(0)
                Call BindSite()
                DDLSite.Value = Session("Job").ToString.Split("|")(1)
                TxtTgl1.Date = Session("Job").ToString.Split("|")(2)
                TxtTgl2.Date = Session("Job").ToString.Split("|")(3)
                Session.Remove("Job")
            End If

            Call BindGrid()

        End If

        If Request.Params("Delete") = 1 Then
            Dim LedgerNo As Integer = 0, NoJurnal As String = String.Empty, JobNo As String = String.Empty
            Dim LedgerNo1 As Integer = 0, Bulan As Integer = 0, Tahun As Integer = 0, Site As String = String.Empty

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM JurnalEntry WHERE JobNo=@P1 AND NoJurnal=@P2"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", Session("DelJurnal"))
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        LedgerNo1 = RsFind("LedgerNo")
                        Bulan = RsFind("Bulan")
                        Tahun = RsFind("Tahun")
                        Site = RsFind("Site")
                        JobNo = RsFind("JobNo")

                        Using CmdDelete As New Data.SqlClient.SqlCommand
                            With CmdDelete
                                .Connection = Conn
                                .CommandType = CommandType.Text
                                .CommandText = "DELETE JurnalEntry WHERE JobNo=@P1 AND NoJurnal=@P2"
                                .Parameters.AddWithValue("@P1", RsFind("JobNo"))
                                .Parameters.AddWithValue("@P2", RsFind("NoJurnal"))
                                .ExecuteNonQuery()
                            End With
                        End Using

                        Using CmdFind1 As New Data.SqlClient.SqlCommand
                            With CmdFind1
                                .Connection = Conn
                                .CommandType = CommandType.Text
                                .CommandText = "SELECT * FROM JurnalEntry WHERE JobNo=@P1 AND LedgerNo>@P2 AND Bulan=@P3 AND Tahun=@P4 AND Site=@P5 ORDER BY LedgerNo"
                                .Parameters.AddWithValue("@P1", JobNo)
                                .Parameters.AddWithValue("@P2", LedgerNo1)
                                .Parameters.AddWithValue("@P3", Bulan)
                                .Parameters.AddWithValue("@P4", Tahun)
                                .Parameters.AddWithValue("@P5", Site)
                            End With
                            Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                                While RsFind1.Read
                                    LedgerNo = RsFind1("LedgerNo") - 1
                                    NoJurnal = RsFind1("NoJurnal").ToString.Split("/")(0) & "/" & Format(LedgerNo, "000") & "/" & _
                                               RsFind1("NoJurnal").ToString.Split("/")(2) & "/" & RsFind1("NoJurnal").ToString.Split("/")(3) & "/" & _
                                               RsFind1("NoJurnal").ToString.Split("/")(4)
                                    Using CmdEdit As New Data.SqlClient.SqlCommand
                                        With CmdEdit
                                            .Connection = Conn
                                            .CommandType = CommandType.Text
                                            .CommandText = "UPDATE JurnalEntry SET LedgerNo=@P1,NoJurnal=@P2,UserEntry=@P3,TimeEntry=@P4 WHERE JobNo=@P5 AND NoJurnal=@P6"
                                            .Parameters.AddWithValue("@P1", LedgerNo)
                                            .Parameters.AddWithValue("@P2", NoJurnal)
                                            .Parameters.AddWithValue("@P3", Session("User").ToString.Split("|")(0))
                                            .Parameters.AddWithValue("@P4", Now)
                                            .Parameters.AddWithValue("@P5", RsFind1("JobNo"))
                                            .Parameters.AddWithValue("@P6", RsFind1("NoJurnal"))
                                            .ExecuteNonQuery()
                                        End With
                                    End Using
                                End While
                            End Using
                        End Using

                        Session.Remove("DelJurnal")
                    End If
                End Using
            End Using

            Call BindGrid()
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
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

    Private Sub BindGrid()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, B.Nama FROM JurnalEntry A LEFT JOIN Identitas B ON A.JobNo=B.JobNo AND A.Identitas=B.Identitas WHERE " + _
                                "A.JobNo=@P1 AND A.Site=@P2 AND A.TglJurnal>=@P3 AND A.TglJurnal<=@P4 AND A.PC='P' "
                .CommandText = .CommandText + GetFilter1() + GetFilter2()
                .CommandText = .CommandText + If(ViewState("ColumnName") = "", " ORDER BY Bulan DESC, Tahun DESC, NoJurnal DESC", " ORDER BY " + ViewState("ColumnName") + " " + ViewState("SortOrder"))
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

    Private Sub GridView_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView.PageIndexChanging
        GridView.PageIndex = e.NewPageIndex
        GridView.DataBind()
        Call BindGrid()
    End Sub

    Private Sub GridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
        If e.CommandName = "BtnView" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)
            Session("Data") = "SEE|" + DDLJob.Value + "|" + DDLSite.Value + "|" + SelectRecord.Cells(1).Text + "|FrmJurnal.aspx|" & TxtTgl1.Date & "|" & TxtTgl2.Date
            Response.Redirect("FrmEntryJurnal.aspx")

        ElseIf e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT ApprovedBy FROM JurnalEntry WHERE JobNo=@P1 AND NoJurnal=@P2 AND PC='P'"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        If String.IsNullOrEmpty(RsFind("ApprovedBy").ToString) = False Then
                            msgBox1.alert("No Jurnal " + SelectRecord.Cells(1).Text + " sudah diapproved.")
                            Exit Sub
                        End If
                    End If
                End Using
            End Using

            Session("Data") = "UPD|" + DDLJob.Value + "|" + DDLSite.Value + "|" + SelectRecord.Cells(1).Text + "|FrmJurnal.aspx|" & TxtTgl1.Date & "|" & TxtTgl2.Date
            Response.Redirect("FrmEntryJurnal.aspx")

        ElseIf e.CommandName = "BtnDelete" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT ApprovedBy FROM JurnalEntry WHERE JobNo=@P1 AND NoJurnal=@P2 AND PC='P'"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        If String.IsNullOrEmpty(RsFind("ApprovedBy").ToString) = False Then
                            msgBox1.alert("No Jurnal " + SelectRecord.Cells(1).Text + " sudah diapproved.")
                            Exit Sub
                        End If
                    End If
                End Using
            End Using

            Session("DelJurnal") = SelectRecord.Cells(1).Text
            msgBox1.confirm("Konfirmasi DELETE untuk No Jurnal " & SelectRecord.Cells(1).Text & " ?", "Delete")

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

    Private Sub BtnAdd_Click(sender As Object, e As System.EventArgs) Handles BtnAdd.Click
        If DDLJob.Text = String.Empty Then
            msgBox1.alert("Job belum dipilih.")
            DDLJob.Focus()
            Exit Sub
        End If
        If DDLSite.Text = String.Empty Then
            msgBox1.alert("Site belum dipilih.")
            DDLSite.Focus()
            Exit Sub
        End If

        Session("Data") = "NEW|" & DDLJob.Value & "|" & DDLSite.Value & "||FrmJurnal.aspx|" & TxtTgl1.Date & "|" & TxtTgl2.Date
        Response.Redirect("FrmEntryJurnal.aspx")
    End Sub

    Private Sub DDLJob_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindSite()
        Call BindGrid()
    End Sub

    Private Sub DDLSite_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLSite.SelectedIndexChanged
        Call BindGrid()
    End Sub

    Private Function GetFilter1() As String
        If DDLField1.Value <> "0" Then
            Return "AND " + DDLField1.Value + GetFilterFunction1()
        End If

        Return ""
    End Function

    Private Function GetFilterFunction1() As String
        If DDLFilterBy1.Value = "0" Then
            If DDLField1.Value <> "TglJurnal" Then
                Return "='" + TxtFind1.Text + "'"
            Else
                Return "='" + TglFilter1.Date + "'"
            End If
        ElseIf DDLFilterBy1.Value = "1" Then
            If DDLField1.Value <> "TglJurnal" Then
                Return " LIKE '%" + TxtFind1.Text + "%'"
            Else
                Return ""
            End If
        ElseIf DDLFilterBy1.Value = "2" Then
            If DDLField1.Value <> "TglJurnal" Then
                Return ">=" + TxtFind1.Text
            Else
                Return ">='" + TglFilter1.Date + "'"
            End If
        ElseIf DDLFilterBy1.Value = "3" Then
            If DDLField1.Value <> "TglJurnal" Then
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
        If DDLField1.Value = "TglJurnal" Then
            TxtFind1.Visible = False
            TglFilter1.Visible = True
            DDLFilterBy1.Items.Add("Equals", "0")
            DDLFilterBy1.Items.Add("Is Greather Than Or Equal To", "2")
            DDLFilterBy1.Items.Add("Is Less Than Or Equal To", "3")
        ElseIf DDLField1.Value = "DebetBalance" Or DDLField1.Value = "KreditBalance" Then
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
        If DDLField2.Value = "TglJurnal" Then
            TxtFind2.Visible = False
            TglFilter2.Visible = True
            DDLFilterBy2.Items.Add("Equals", "0")
            DDLFilterBy2.Items.Add("Is Greather Than Or Equal To", "2")
            DDLFilterBy2.Items.Add("Is Less Than Or Equal To", "3")
        ElseIf DDLField2.Value = "DebetBalance" Or DDLField2.Value = "KreditBalance" Then
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
            If DDLField2.Value <> "TglJurnal" Then
                Return "='" + TxtFind2.Text + "'"
            Else
                Return "='" + TglFilter2.Date + "'"
            End If
        ElseIf DDLFilterBy2.Value = "1" Then
            If DDLField2.Value <> "TglJurnal" Then
                Return "LIKE '%" + TxtFind2.Text + "%'"
            Else
                Return ""
            End If
        ElseIf DDLFilterBy2.Value = "2" Then
            If DDLField2.Value <> "TglJurnal" Then
                Return ">=" + TxtFind2.Text
            Else
                Return ">='" + TglFilter2.Date + "'"
            End If
        ElseIf DDLFilterBy2.Value = "3" Then
            If DDLField2.Value <> "TglJurnal" Then
                Return "<=" + TxtFind2.Text
            Else
                Return "<='" + TglFilter2.Date + "'"
            End If
        End If

        Return ""
    End Function

End Class