Public Class FrmCOA
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "COA") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            ViewState("ColumnName") = "AccNo"
            ViewState("SortOrder") = "ASC"
            Call BindGrid()
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindGrid()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                If TxtFind.Text = "" Then
                    .CommandText = "SELECT * FROM COA  "
                Else
                    .CommandText = "SELECT * FROM COA WHERE AccName LIKE '%" + TxtFind.Text + "%' "
                End If
                .CommandText = .CommandText + If(ViewState("ColumnName") = "", "", "ORDER BY " + ViewState("ColumnName") + " " + ViewState("SortOrder"))
                .Parameters.AddWithValue("@P2", "%" + TxtFind.Text + "%")
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
        LblAction.Text = "NEW"
        TxtAccNo.Enabled = True
        TxtAccNo.Text = ""
        TxtAccDesc.Text = ""
        TxtAccNo.Focus()

        ModalEntry.ShowOnPageLoad = True
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As System.EventArgs) Handles BtnSave.Click
        If LblAction.Text = "NEW" Then
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM COA WHERE AccNo=@P2"
                    .Parameters.AddWithValue("@P2", TxtAccNo.Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        msgBox1.alert("Account No. " + TxtAccNo.Text + " sudah pernah di-input.")
                        Exit Sub
                    End If
                End Using
            End Using

            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO COA(AccNo,AccName,UserEntry,TimeEntry) VALUES " & _
                               "(@P1,@P2,@P3,@P4)"
                .Parameters.AddWithValue("@P1", TxtAccNo.Text)
                .Parameters.AddWithValue("@P2", TxtAccDesc.Text)
                .Parameters.AddWithValue("@P3", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P4", Now)
                .ExecuteNonQuery()
            End With
        Else
            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE COA SET AccName=@P1,UserEntry=@P2,TimeEntry=@P3 WHERE AccNo=@P4"
                .Parameters.AddWithValue("@P1", TxtAccDesc.Text)
                .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P3", Now)
                .Parameters.AddWithValue("@P4", Session("COA"))
                .ExecuteNonQuery()
            End With

        End If

        Call BindGrid()

        BtnCancel_Click(BtnCancel, New EventArgs())
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As System.EventArgs) Handles BtnCancel.Click
        Session.Remove("COA")
        ModalEntry.ShowOnPageLoad = False
    End Sub

    Private Sub GridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)

            Session("COA") = SelectRecord.Cells(0).Text

            LblAction.Text = "UPD"
            TxtAccNo.Text = SelectRecord.Cells(0).Text
            TxtAccNo.Enabled = False
            TxtAccDesc.Text = SelectRecord.Cells(1).Text
            TxtAccDesc.Focus()

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

    Private Sub BtnFind_Click(sender As Object, e As System.EventArgs) Handles BtnFind.Click
        Call BindGrid()
    End Sub

End Class