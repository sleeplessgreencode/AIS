Public Class FrmSetupAkses
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "SetupAkses") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Call BindGrid()

    End Sub

    Private Sub BindGrid()
        Dim CmdGrid As New Data.SqlClient.SqlCommand
        With CmdGrid
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT UserID,UserName FROM Login WHERE UserName LIKE @P1"
            .Parameters.AddWithValue("@P1", "%" + TxtFind.Text + "%")
        End With

        Dim DaGrid As New Data.SqlClient.SqlDataAdapter
        DaGrid.SelectCommand = CmdGrid
        Dim DtGrid As New Data.DataTable
        DaGrid.Fill(DtGrid)
        GridData.DataSource = DtGrid
        GridData.DataBind()

        DaGrid.Dispose()
        DtGrid.Dispose()
        CmdGrid.Dispose()

    End Sub

    Private Sub GridData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridData.PageIndexChanging
        GridData.PageIndex = e.NewPageIndex
        GridData.DataBind()
        Call BindGrid()
    End Sub

    Private Sub GridData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            Session("Setup") = SelectRecord.Cells(1).Text
            Response.Redirect("FrmEntrySetup.aspx")

        ElseIf e.CommandName = "BtnDelete" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)
            Session("Delete") = SelectRecord.Cells(1).Text

            LblDel.Text = "Anda yakin ingin menghapus data berikut?" & "<br />" & _
                          "User ID: " & SelectRecord.Cells(1).Text & " - " & SelectRecord.Cells(2).Text
            DelMsg.ShowOnPageLoad = True

        End If
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Session("Setup") = "NEW"

        Response.Redirect("FrmEntrySetup.aspx")
        Exit Sub
    End Sub

    Protected Sub BtnDel_Click(sender As Object, e As EventArgs) Handles BtnDel.Click
        Dim CmdDelete As New Data.SqlClient.SqlCommand
        With CmdDelete
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "DELETE FROM Login WHERE UserID=@P1"
            .Parameters.AddWithValue("@P1", Session("Delete"))
            .ExecuteNonQuery()
            .Dispose()
        End With
        Session.Remove("Delete")

        Call BindGrid()
    End Sub
End Class