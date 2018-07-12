﻿Public Class FrmKategori
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "Kategori") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Call BindGrid()
        Call CfgObject()
        TxtKategori.Focus()

    End Sub

    Private Sub BindGrid()
        Dim CmdGrid As New Data.SqlClient.SqlCommand
        With CmdGrid
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT KategoriId, Keterangan FROM Kategori"
        End With
        Dim DaGrid As New Data.SqlClient.SqlDataAdapter
        DaGrid.SelectCommand = CmdGrid
        Dim DtGrid As New Data.DataTable
        DaGrid.Fill(DtGrid)
        With GridData
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
        TxtKategori.Text = ""
        TxtKeterangan.Text = ""
        TxtAction.Text = "NEW"
        Session.Remove("Kategori")
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If TxtKategori.Text = "" Then
            LblErr.Text = "Kategori belum diinput."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtAction.Text = "NEW" Then
            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Kategori WHERE KategoriId=@P1"
                .Parameters.AddWithValue("@P1", TxtKategori.Text)
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                RsFind.Close()
                CmdFind.Dispose()
                LblErr.Text = "Kategori " & TxtKategori.Text & " sudah ada."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            RsFind.Close()
            CmdFind.Dispose()

            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Kategori (KategoriId,Keterangan,UserEntry,TimeEntry) VALUES " & _
                                "(@P1,@P2,@P3,@P4)"
                .Parameters.AddWithValue("@P1", UCase(TxtKategori.Text))
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
                .CommandText = "UPDATE Kategori SET " & _
                                "Keterangan=@P1,UserEntry=@P2,TimeEntry = GETDATE() WHERE KategoriId=@P3"
                .Parameters.AddWithValue("@P1", TxtKeterangan.Text)
                .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P3", Session("Kategori"))
                .ExecuteNonQuery()
                .Dispose()
            End With
        End If
        Call BindCleaning()
        Call BindGrid()
        Call CfgObject()
    End Sub

    Private Sub GridData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            TxtKategori.Text = SelectRecord.Cells(0).Text
            TxtKeterangan.Text = SelectRecord.Cells(1).Text
            Session("Kategori") = SelectRecord.Cells(0).Text
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
            TxtKategori.Enabled = True
            TxtKategori.Focus()
        ElseIf TxtAction.Text = "UPD" Then
            BtnSave.Text = "SIMPAN"
            BtnCancel.Visible = True
            TxtKategori.Enabled = False
            TxtKeterangan.Focus()
        End If
    End Sub

End Class