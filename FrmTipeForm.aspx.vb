Public Class FrmTipeForm
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "TipeForm") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then Call BindAlokasi()
        Call BindGrid()
        Call CfgObject()
        TxtForm.Focus()

    End Sub

    Private Sub BindAlokasi()
        Dim AksesAlokasi As String = ""
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT AksesAlokasi FROM Login WHERE UserID=@P1"
                .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                AksesAlokasi = RsFind("AksesAlokasi")
            End If
        End Using

        DDLAlokasi.Items.Clear()
        DDLAlokasi.Items.Add("Pilih salah satu", "0")

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT Alokasi,Keterangan FROM Alokasi"
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    If Array.IndexOf(AksesAlokasi.Split(","), RsFind("Alokasi")) >= 0 Then
                        DDLAlokasi.Items.Add(RsFind("Alokasi") & " - " & RsFind("Keterangan"), RsFind("Alokasi"))
                    End If
                End While
            End Using
        End Using

        DDLAlokasi.Value = "0"


    End Sub

    Private Sub BindGrid()
        Dim CmdGrid As New Data.SqlClient.SqlCommand
        With CmdGrid
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TipeForm, Alokasi, Keterangan FROM TipeForm"
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
        TxtForm.Text = ""
        DDLAlokasi.Value = "0"
        TxtKeterangan.Text = ""
        TxtAction.Text = "NEW"
        Session.Remove("TipeForm")
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If TxtForm.Text = "" Or TxtKeterangan.Text = "" Then
            LblErr.Text = "Tipe Form atau Keterangan belum diinput."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If DDLAlokasi.Value = "0" Then
            LblErr.Text = "Alokasi belum di-pilih."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtAction.Text = "NEW" Then
            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM TipeForm WHERE TipeForm=@P1"
                .Parameters.AddWithValue("@P1", TxtForm.Text)
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                RsFind.Close()
                CmdFind.Dispose()
                LblErr.Text = "Tipe Form " & TxtForm.Text & " sudah ada."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            RsFind.Close()
            CmdFind.Dispose()

            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO TipeForm (TipeForm,Alokasi,Keterangan,UserEntry,TimeEntry) VALUES " & _
                                "(@P1,@P2,@P3,@P4,@P5)"
                .Parameters.AddWithValue("@P1", UCase(TxtForm.Text))
                .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                .Parameters.AddWithValue("@P3", TxtKeterangan.Text)
                .Parameters.AddWithValue("@P4", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P5", Now)
                .ExecuteNonQuery()
                .Dispose()
            End With
        Else
            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE TipeForm SET " & _
                                "Alokasi=@P1,Keterangan=@P2,UserEntry=@P3,TimeEntry = GETDATE() WHERE TipeForm=@P4"
                .Parameters.AddWithValue("@P1", DDLAlokasi.Value)
                .Parameters.AddWithValue("@P2", TxtKeterangan.Text)
                .Parameters.AddWithValue("@P3", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P4", Session("TipeForm"))
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

            TxtForm.Text = SelectRecord.Cells(0).Text
            DDLAlokasi.Value = SelectRecord.Cells(1).Text
            TxtKeterangan.Text = SelectRecord.Cells(2).Text
            Session("TipeForm") = SelectRecord.Cells(0).Text
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
            TxtForm.Enabled = True
            TxtForm.Focus()
        ElseIf TxtAction.Text = "UPD" Then
            BtnSave.Text = "SIMPAN"
            BtnCancel.Visible = True
            TxtForm.Enabled = False
            TxtKeterangan.Focus()
        End If
    End Sub

End Class