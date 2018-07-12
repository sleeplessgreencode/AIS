Public Class FrmProposal
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "Proposal") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Call BindGrid()
    End Sub

    Private Sub BindGrid()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Job " & _
                               "WHERE StatusJob=@P1 AND JobNm LIKE @P2 ORDER BY JobNo DESC"
                .Parameters.AddWithValue("@P1", "Proposal")
                .Parameters.AddWithValue("@P2", "%" + TxtFind.Text + "%")
            End With
            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    With GridData
                        .DataSource = DtGrid
                        .DataBind()
                    End With
                End Using
            End Using
        End Using

    End Sub

    Private Sub GridData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridData.PageIndexChanging
        GridData.PageIndex = e.NewPageIndex
        GridData.DataBind()
        Call BindGrid()
    End Sub

    Private Sub GridData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            TxtAction.Text = "UPD"
            TxtJobNo.Text = SelectRecord.Cells(0).Text
            TxtNama.Text = SelectRecord.Cells(1).Text
            TxtDesc.Text = TryCast(SelectRecord.FindControl("LblDeskripsi"), Label).Text.Replace("<br />", Environment.NewLine)
            TxtLokasi.Text = SelectRecord.Cells(3).Text
            TxtInstansi.Text = SelectRecord.Cells(4).Text
            TxtJobNo.Enabled = False

            PopEntry.ShowOnPageLoad = True
        End If
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As System.EventArgs) Handles BtnSave.Click
        If TxtAction.Text = "NEW" Then
            If CheckJobNo() = False Then
                LblErr.Text = "Job No " & TxtJobNo.Text & " sudah pernah digunakan."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If

            Using CmdInsert As New Data.SqlClient.SqlCommand
                With CmdInsert
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO Job (JobNo,JobNm,Deskripsi,Lokasi,Instansi," & _
                                   "StatusJob,UserEntry,TimeEntry) VALUES " & _
                                    "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)"
                    .Parameters.AddWithValue("@P1", TxtJobNo.Text)
                    .Parameters.AddWithValue("@P2", TxtNama.Text)
                    .Parameters.AddWithValue("@P3", TxtDesc.Text)
                    .Parameters.AddWithValue("@P4", TxtLokasi.Text)
                    .Parameters.AddWithValue("@P5", TxtInstansi.Text)
                    .Parameters.AddWithValue("@P6", "Proposal")
                    .Parameters.AddWithValue("@P7", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P8", Now)
                    .ExecuteNonQuery()
                End With
            End Using
        Else
            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE Job SET JobNm=@P1," & _
                                   "Deskripsi=@P2,Lokasi=@P3,Instansi=@P4," & _
                                   "UserEntry=@P5,TimeEntry=@P6 WHERE JobNo=@P7"
                    .Parameters.AddWithValue("@P1", TxtNama.Text)
                    .Parameters.AddWithValue("@P2", TxtDesc.Text)
                    .Parameters.AddWithValue("@P3", TxtLokasi.Text)
                    .Parameters.AddWithValue("@P4", TxtInstansi.Text)
                    .Parameters.AddWithValue("@P5", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P6", Now)
                    .Parameters.AddWithValue("@P7", TxtJobNo.Text)
                    .ExecuteNonQuery()
                End With
            End Using
        End If

        PopEntry.ShowOnPageLoad = False

        Call BindGrid()
    End Sub

    Private Function CheckJobNo() As Boolean
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo FROM Job WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", TxtJobNo.Text)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    Return False
                Else
                    Return True
                End If
            End Using
        End Using

    End Function

    Private Sub BtnAdd_Click(sender As Object, e As System.EventArgs) Handles BtnAdd.Click
        TxtAction.Text = "NEW"
        TxtJobNo.Text = ""
        TxtNama.Text = ""
        TxtDesc.Text = ""
        TxtLokasi.Text = ""
        TxtInstansi.Text = ""
        TxtJobNo.Enabled = True
        PopEntry.ShowOnPageLoad = True

    End Sub

End Class