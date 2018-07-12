Public Class FrmBank
    Inherits System.Web.UI.Page

    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "Bank") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Call BindGrid()
        Call CfgObject()

    End Sub

    Private Sub BindGrid()
        Dim CmdGrid As New Data.SqlClient.SqlCommand
        With CmdGrid
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT Bank FROM Bank"
        End With

        Dim DaGrid As New Data.SqlClient.SqlDataAdapter
        DaGrid.SelectCommand = CmdGrid
        Dim DtGrid As New Data.DataTable
        DaGrid.Fill(DtGrid)
        With GridBank
            .DataSource = DtGrid
            .DataBind()
        End With
        DtGrid.Dispose()
        DaGrid.Dispose()
        CmdGrid.Dispose()
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindCleaning()
        TxtBank.Text = ""
        TxtAction.Text = "NEW"
        Session.Remove("Bank")
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If TxtBank.Text = "" Then
            LblErr.Text = "Bank belum diinput."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        If validasi(TxtBank.Text) = False Then Exit Sub

        If TxtAction.Text = "NEW" Then
            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Bank (Bank,UserEntry,TimeEntry) VALUES " & _
                                "(@P1,@P2,@P3)"
                .Parameters.AddWithValue("@P1", UCase(TxtBank.Text))
                .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P3", Now)
                .ExecuteNonQuery()
                .Dispose()
            End With
        Else
            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE Bank SET " & _
                                "Bank=@P1,UserEntry=@P2,TimeEntry = GETDATE() WHERE Bank=@P3"
                .Parameters.AddWithValue("@P1", UCase(TxtBank.Text))
                .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P3", Session("Bank"))
                .ExecuteNonQuery()
                .Dispose()
            End With
        End If
        Call BindCleaning()
        Call BindGrid()
        Call CfgObject()
    End Sub

    Private Sub GridBank_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridBank.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridBank.Rows(e.CommandArgument)

            TxtBank.Text = SelectRecord.Cells(0).Text
            Session("Bank") = SelectRecord.Cells(0).Text
            TxtAction.Text = "UPD"

            Call CfgObject()
        End If
    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        TxtAction.Text = "NEW"
        Call CfgObject()
        Call BindCleaning()

    End Sub

    Private Sub CfgObject()
        If TxtAction.Text = "NEW" Then
            BtnSave.Text = "TAMBAH"
            BtnCancel.Visible = False
        ElseIf TxtAction.Text = "UPD" Then
            BtnSave.Text = "SIMPAN"
            BtnCancel.Visible = True
        End If
    End Sub

    Private Function validasi(ByVal vBank As String) As Boolean
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Bank WHERE Bank=@P1"
            .Parameters.AddWithValue("@P1", vBank)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            RsFind.Close()
            CmdFind.Dispose()
            LblErr.Text = "Bank " & vBank & " sudah ada."
            ErrMsg.ShowOnPageLoad = True
            Return False
        Else
            RsFind.Close()
            CmdFind.Dispose()
            Return True
        End If

    End Function

End Class