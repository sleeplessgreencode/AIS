Public Class FrmGlReff
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "GLReff") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            ViewState("ColumnName") = "Site"
            ViewState("SortOrder") = "ASC"
            Call BindJob()
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

    Private Sub BindGrid()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM GlReff WHERE JobNo=@P1 "
                .CommandText = .CommandText + If(ViewState("ColumnName") = "", "", "ORDER BY " + ViewState("ColumnName") + " " + ViewState("SortOrder"))
                .Parameters.AddWithValue("@P1", If(DDLJob.Text = String.Empty, String.Empty, DDLJob.Value))
            End With
            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    GridView.DataSource = DtGrid
                    GridView.DataBind()
                End Using
            End Using
        End Using

    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As System.EventArgs) Handles BtnAdd.Click
        If DDLJob.Text = String.Empty Then
            msgBox1.alert("Job belum dipilih.")
            DDLJob.Focus()
            Exit Sub
        End If

        LblAction.Text = "NEW"
        DDLSite.Enabled = True
        DDLSite.SelectedIndex = 0
        TxtMember.Text = ""
        DDLSite.Focus()

        ModalEntry.ShowOnPageLoad = True
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As System.EventArgs) Handles BtnSave.Click
        If LblAction.Text = "NEW" Then
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM GlReff WHERE JobNo=@P1 AND Site=@P2"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", DDLSite.Value)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        msgBox1.alert("Site " + DDLSite.Text + " sudah pernah di-input.")
                        Exit Sub
                    End If
                End Using
            End Using

            Using CmdInsert As New Data.SqlClient.SqlCommand
                With CmdInsert
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO GlReff(JobNo,Site,Member,UserEntry,TimeEntry,Logo,Kasir) VALUES " & _
                                   "(@P1,@P2,@P3,@P4,@P5,@P6,@P7)"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", DDLSite.Value)
                    .Parameters.AddWithValue("@P3", TxtMember.Text)
                    .Parameters.AddWithValue("@P4", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P5", Now)
                    If FileUpload1.HasFile Then
                        Dim fileName As String = Format(Now, "yyyy-MM-dd") & "_" & Format(Now, "hhmmss") & _
                            System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName)
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/") + fileName)
                        .Parameters.AddWithValue("@P6", "~/Images/" + fileName)
                    Else
                        .Parameters.AddWithValue("@P6", DBNull.Value)
                    End If
                    .Parameters.AddWithValue("@P7", TxtKasir.Text)
                    .ExecuteNonQuery()
                End With
            End Using
        Else
            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE GlReff SET Member=@P1,UserEntry=@P2,TimeEntry=@P3,Logo=@P4,Kasir=@P5 WHERE JobNo=@P6 AND Site=@P7"
                    .Parameters.AddWithValue("@P1", TxtMember.Text)
                    .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P3", Now)
                    If FileUpload1.HasFile Then
                        Dim fileName As String = Format(Now, "yyyy-MM-dd") & "_" & Format(Now, "hhmmss") & _
                            System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName)
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/") + fileName)
                        .Parameters.AddWithValue("@P4", "~/Images/" + fileName)
                    Else
                        .Parameters.AddWithValue("@P4", LblImage.Text)
                    End If
                    .Parameters.AddWithValue("@P5", TxtKasir.Text)
                    .Parameters.AddWithValue("@P6", DDLJob.Value)
                    .Parameters.AddWithValue("@P7", Session("Reff"))
                    .ExecuteNonQuery()
                End With
            End Using

        End If

        Call BindGrid()

        BtnCancel_Click(BtnCancel, New EventArgs())
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As System.EventArgs) Handles BtnCancel.Click
        Session.Remove("Reff")
        ModalEntry.ShowOnPageLoad = False
    End Sub

    Private Sub GridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)
            Dim Image As Image

            Session("Reff") = SelectRecord.Cells(0).Text

            LblAction.Text = "UPD"
            DDLSite.Value = SelectRecord.Cells(0).Text
            DDLSite.Enabled = False
            TxtMember.Text = SelectRecord.Cells(1).Text
            TxtKasir.Text = If(SelectRecord.Cells(2).Text = "&nbsp;", String.Empty, SelectRecord.Cells(2).Text)
            Image = SelectRecord.Cells(3).Controls(0)
            LblImage.Text = Image.ImageUrl
            TxtMember.Focus()

            ModalEntry.ShowOnPageLoad = True

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

    Private Sub DDLJob_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindGrid()
    End Sub

End Class