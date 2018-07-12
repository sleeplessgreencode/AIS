Public Class FrmAlokasi
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "Alokasi") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Call BindGrid()
        Call CfgObject()
        TxtAlokasi.Focus()

    End Sub

    Private Sub BindGrid()
        Dim CmdGrid As New Data.SqlClient.SqlCommand
        With CmdGrid
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT Alokasi, Keterangan FROM Alokasi"
        End With
        Dim DaGrid As New Data.SqlClient.SqlDataAdapter
        DaGrid.SelectCommand = CmdGrid
        Dim DtGrid As New Data.DataTable
        DaGrid.Fill(DtGrid)
        With GridBiaya
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
        TxtAlokasi.Text = ""
        TxtKeterangan.Text = ""
        TxtAction.Text = "NEW"
        Session.Remove("Alokasi")
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If TxtAlokasi.Text = "" Or TxtKeterangan.Text = "" Then
            LblErr.Text = "Alokasi atau keterangan belum diinput."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtAction.Text = "NEW" Then
            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Alokasi WHERE Alokasi=@P1"
                .Parameters.AddWithValue("@P1", TxtAlokasi.Text)
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                RsFind.Close()
                CmdFind.Dispose()
                LblErr.Text = "Alokasi " & TxtAlokasi.Text & " sudah ada."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            RsFind.Close()
            CmdFind.Dispose()

            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Alokasi (Alokasi,Keterangan,UserEntry,TimeEntry) VALUES " & _
                                "(@P1,@P2,@P3,@P4)"
                .Parameters.AddWithValue("@P1", UCase(TxtAlokasi.Text))
                .Parameters.AddWithValue("@P2", TxtKeterangan.Text)
                .Parameters.AddWithValue("@P3", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P4", Now)
                .ExecuteNonQuery()
                .Dispose()
            End With
        Else
            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE Alokasi SET " & _
                                "Keterangan=@P1,UserEntry=@P2,TimeEntry = GETDATE() WHERE Alokasi=@P3"
                .Parameters.AddWithValue("@P1", TxtKeterangan.Text)
                .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P3", Session("Alokasi"))
                .ExecuteNonQuery()
                .Dispose()
            End With
        End If
        Call BindCleaning()
        Call BindGrid()
        Call CfgObject()
    End Sub

    Private Sub GridBiaya_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridBiaya.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridBiaya.Rows(e.CommandArgument)

            TxtAlokasi.Text = SelectRecord.Cells(0).Text
            TxtKeterangan.Text = SelectRecord.Cells(1).Text
            Session("Alokasi") = SelectRecord.Cells(0).Text
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
            TxtAlokasi.Enabled = True
            TxtAlokasi.Focus()
        ElseIf TxtAction.Text = "UPD" Then
            BtnSave.Text = "SIMPAN"
            BtnCancel.Visible = True
            TxtAlokasi.Enabled = False
            TxtKeterangan.Focus()
        End If
    End Sub

End Class