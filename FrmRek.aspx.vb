Public Class FrmRek
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "RekPengirim") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Call BindGrid()
        Call CfgObject()

        If IsPostBack = False Then
            Call BindBank()
        End If

    End Sub

    Private Sub BindGrid()
        Dim CmdGrid As New Data.SqlClient.SqlCommand
        With CmdGrid
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT RekId, Bank, NoRek, AtasNama FROM Rekening"
        End With
        Dim DaGrid As New Data.SqlClient.SqlDataAdapter
        DaGrid.SelectCommand = CmdGrid
        Dim DtGrid As New Data.DataTable
        DaGrid.Fill(DtGrid)
        With GridRekening
            .DataSource = DtGrid
            .DataBind()
        End With
        DtGrid.Dispose()
        DaGrid.Dispose()
        CmdGrid.Dispose()
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

    Private Sub BindBank()
        DDLBank.Items.Clear()
        DDLBank.Items.Add("Pilih salah satu", "0")
        Dim CmdIsi As New Data.SqlClient.SqlCommand
        With CmdIsi
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT Bank FROM Bank"
        End With
        Dim RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
        While RsIsi.Read
            DDLBank.Items.Add(RsIsi(0), RsIsi(0))
        End While
        RsIsi.Close()
        CmdIsi.Dispose()

        DDLBank.Value = "0"
    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        TxtAction.Text = "NEW"
        Call CfgObject()
        Call BindCleaning()
        Call BindBank()

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindCleaning()
        TxtRekId.Text = ""
        DDLBank.Value = "0"
        TxtNoRek.Text = ""
        TxtAtasNm.Text = ""
        TxtAction.Text = "NEW"
        Session.Remove("Rekening")
    End Sub

    Private Sub GridRekening_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridRekening.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridRekening.Rows(e.CommandArgument)

            TxtRekId.Text = SelectRecord.Cells(0).Text
            DDLBank.SelectedItem = DDLBank.Items.FindByValue(SelectRecord.Cells(1).Text)
            TxtNoRek.Text = If(SelectRecord.Cells(2).Text = "&nbsp;", String.Empty, String.Empty)
            TxtAtasNm.Text = If(SelectRecord.Cells(3).Text = "&nbsp;", String.Empty, String.Empty)
            Session("Rekening") = SelectRecord.Cells(0).Text
            TxtAction.Text = "UPD"

            Call CfgObject()
        End If
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click

        If TxtRekId.Text = "" Then
            LblErr.Text = "Rekening ID belum di-input."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        If TxtAction.Text = "NEW" Then
            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Rekening (RekId,Bank,NoRek,AtasNama,UserEntry,TimeEntry) VALUES " & _
                                "(@P1,@P2,@P3,@P4,@P5,@P6)"
                .Parameters.AddWithValue("@P1", TxtRekId.Text)
                .Parameters.AddWithValue("@P2", DDLBank.Value)
                .Parameters.AddWithValue("@P3", TxtNoRek.Text)
                .Parameters.AddWithValue("@P4", TxtAtasNm.Text)
                .Parameters.AddWithValue("@P5", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P6", Now)
                .ExecuteNonQuery()
                .Dispose()
            End With
        Else
            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE Rekening SET " & _
                                "Bank=@P1,NoRek=@P2,AtasNama=@P3,UserEntry=@P4,TimeEntry = GETDATE(),RekId=@P5 WHERE RekId=@P6"
                .Parameters.AddWithValue("@P1", DDLBank.Value)
                .Parameters.AddWithValue("@P2", TxtNoRek.Text)
                .Parameters.AddWithValue("@P3", TxtAtasNm.Text)
                .Parameters.AddWithValue("@P4", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P5", TxtRekId.Text)
                .Parameters.AddWithValue("@P6", Session("Rekening"))
                .ExecuteNonQuery()
                .Dispose()
            End With
        End If
        Call BindCleaning()
        Call BindGrid()
        Call CfgObject()
    End Sub

End Class