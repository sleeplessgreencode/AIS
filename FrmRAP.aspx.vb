Public Class FrmRAP
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim TtlRAB As Decimal = 0
    Dim TtlRAP As Decimal = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "RAP") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Call BindJob()
            Call BindAlokasi()
            Call BindVersi()
        End If

        If Session("Job") <> "" Then
            DDLJob.Value = Session("Job").ToString.Split("|")(0)

            If Session("Job").ToString.Split("|")(1) = "0" Or
               Session("Job").ToString.Split("|")(1) = "" Then
                DDLAlokasi.Value = "0"
            Else
                DDLAlokasi.Value = Session("Job").ToString.Split("|")(1)
            End If

            Call BindVersi()

            If Session("Job").ToString.Split("|")(2) = "RAP" Then
                DDLVersion.Value = "RAP"
            Else
                DDLVersion.Value = Session("Job").ToString.Split("|")(3)
            End If

            Session.Remove("Job")
        End If

        Call BindGrid()

    End Sub

    Private Sub BindVersi()
        DDLVersion.Items.Clear()

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TOP 1 Versi FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            DDLVersion.Items.Add(RsFind("Versi"), "RAP")
        Else
            DDLVersion.Items.Add("0.0", "RAP")
        End If
        RsFind.Close()
        CmdFind.Dispose()

        Dim CmdFind1 As New Data.SqlClient.SqlCommand
        With CmdFind1
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT DISTINCT Versi FROM RAPH WHERE JobNo=@P1 AND Alokasi=@P2 ORDER BY Versi DESC"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
        End With
        Dim RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
        While RsFind1.Read
            DDLVersion.Items.Add(RsFind1("Versi"), RsFind1("Versi"))
        End While
        RsFind1.Close()
        CmdFind1.Dispose()

        DDLVersion.Value = "RAP"

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
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                If Array.IndexOf(AksesAlokasi.Split(","), RsFind("Alokasi")) >= 0 Then
                    DDLAlokasi.Items.Add(RsFind("Alokasi") & " - " & RsFind("Keterangan"), RsFind("Alokasi"))
                End If
            End While
        End Using

        DDLAlokasi.Value = "0"

    End Sub

    Private Sub BindGrid()
        Dim CmdGrid As New Data.SqlClient.SqlCommand
        With CmdGrid
            .Connection = Conn
            .CommandType = CommandType.Text
            If DDLVersion.Value = "RAP" Then
                .CommandText = "SELECT * FROM RAP " & _
                               "WHERE JobNo=@P1 AND Alokasi=@P3 AND Uraian LIKE @P4 ORDER BY NoUrut ASC"
            Else
                .CommandText = "SELECT * FROM RAPH " & _
                               "WHERE JobNo=@P1 AND Versi=@P2 AND Alokasi=@P3 AND Uraian LIKE @P4 ORDER BY NoUrut ASC"
            End If
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", DDLVersion.Value)
            .Parameters.AddWithValue("@P3", DDLAlokasi.Value)
            .Parameters.AddWithValue("@P4", "%" + TxtFind.Text + "%")
        End With

        TtlRAB = 0
        TtlRAP = 0
        Dim RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
        While RsGrid.Read
            If RsGrid("Tipe") = "Header" Then Continue While
            TtlRAP = TtlRAP + (RsGrid("Vol") * RsGrid("HrgSatuan"))
            TtlRAB = TtlRAB + (RsGrid("Vol") * RsGrid("HrgRAB"))
        End While
        RsGrid.Close()
        GridRAP.Columns(7).FooterText = Format(TtlRAP, "N2")
        GridRAP.Columns(10).FooterText = Format(TtlRAB, "N2")
        GridRAP.Columns(7).FooterStyle.HorizontalAlign = HorizontalAlign.Right
        GridRAP.Columns(10).FooterStyle.HorizontalAlign = HorizontalAlign.Right
        GridRAP.Columns(12).FooterStyle.HorizontalAlign = HorizontalAlign.Right

        Dim DaGrid As New Data.SqlClient.SqlDataAdapter
        DaGrid.SelectCommand = CmdGrid
        Dim DtGrid As New Data.DataTable
        DaGrid.Fill(DtGrid)
        GridRAP.DataSource = DtGrid
        GridRAP.DataBind()

        DaGrid.Dispose()
        DtGrid.Dispose()
        CmdGrid.Dispose()

    End Sub

    Private Sub GridRAP_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridRAP.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridRAP.Rows(e.CommandArgument)

            If DDLVersion.Value <> "RAP" Then
                Session("RAP") = "SEE|" & SelectRecord.Cells(1).Text & "|" & DDLJob.Text & "|" & DDLVersion.Value & "|" & DDLVersion.Text & "|" & DDLAlokasi.Value
            Else
                Session("RAP") = "UPD|" & SelectRecord.Cells(1).Text & "|" & DDLJob.Text & "|" & DDLVersion.Value & "|" & DDLVersion.Text & "|" & DDLAlokasi.Value
            End If

            Response.Redirect("FrmEntryRAP.aspx")
            Exit Sub
        ElseIf e.CommandName = "BtnInsert" Then
            If DDLVersion.Value <> "RAP" Then Exit Sub

            Dim SelectRecord As GridViewRow = GridRAP.Rows(e.CommandArgument)
            Session("RAP") = "INS|" & SelectRecord.Cells(1).Text & "|" & DDLJob.Text & "|" & DDLVersion.Value & "|" & DDLVersion.Text & "|" & DDLAlokasi.Value
            
            Response.Redirect("FrmEntryRAP.aspx")
            Exit Sub
        ElseIf e.CommandName = "BtnDelete" Then
            If DDLVersion.Value <> "RAP" Then Exit Sub
            Dim SelectRecord As GridViewRow = GridRAP.Rows(e.CommandArgument)

            'Disable KdRAP jika sudah dipakai PD
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM PdHdr A JOIN PdDtl B ON A.NoPD=B.NoPD WHERE A.JobNo=@P1 AND A.Alokasi=@P2 AND A.RejectBy IS NULL AND B.KdRAP=@P3"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                    .Parameters.AddWithValue("@P3", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        LblErr.Text = "Tidak bisa hapus RAP.<br />Sudah dipakai untuk Permintaan Dana."
                        ErrMsg.ShowOnPageLoad = True
                        Exit Sub
                    End If
                End Using
            End Using
            'Disable KdRAP jika sudah dipakai KO
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM KoHdr A JOIN KoDtl B ON A.NoKO=B.NoKO WHERE A.JobNo=@P1 AND B.Alokasi=@P2 AND B.KdRAP=@P3"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                    .Parameters.AddWithValue("@P3", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        LblErr.Text = "Tidak bisa hapus RAP.<br />Sudah dipakai untuk Kontrak/PO."
                        ErrMsg.ShowOnPageLoad = True
                        Exit Sub
                    End If
                End Using
            End Using

            If SelectRecord.Cells(0).Text = "Header" Then
                Using CmdFind As New Data.SqlClient.SqlCommand
                    With CmdFind
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT * FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2 AND Tipe='Detail' AND Header=@P3"
                        .Parameters.AddWithValue("@P1", DDLJob.Value)
                        .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                        .Parameters.AddWithValue("@P3", SelectRecord.Cells(1).Text)
                    End With
                    Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                        If RsFind.Read Then
                            LblErr.Text = "Tidak bisa hapus RAP Tipe Header. Masih ada detail."
                            ErrMsg.ShowOnPageLoad = True
                            Exit Sub
                        End If
                    End Using
                End Using
            End If

            Session("Delete") = SelectRecord.Cells(1).Text

            LblDel.Text = "Anda yakin ingin menghapus data berikut?" & "<br />" & _
                          "Kode RAP: " & SelectRecord.Cells(1).Text & " - " & SelectRecord.Cells(3).Text
            DelMsg.ShowOnPageLoad = True

        ElseIf e.CommandName = "BtnUp" Or e.CommandName = "BtnDown" Then
            If DDLVersion.Value <> "RAP" Then Exit Sub

            Dim SelectRecord As GridViewRow = GridRAP.Rows(e.CommandArgument)

            If e.CommandArgument = 0 And e.CommandName = "BtnUp" Then Exit Sub
            If e.CommandArgument = GridRAP.Rows.Count - 1 And e.CommandName = "BtnDown" Then Exit Sub

            Dim KdRAPv As String
            Dim NoUrutv As Integer
            Dim RowIndex As GridViewRow = If(e.CommandName = "BtnUp", GridRAP.Rows(e.CommandArgument - 1), GridRAP.Rows(e.CommandArgument + 1)) 'untuk dapat baris sebelumnya/berikutnya

            KdRAPv = SelectRecord.Cells(1).Text
            NoUrutv = RowIndex.Cells(2).Text 'no urut di baris sebelumnya
            UpdatePreference(NoUrutv, KdRAPv)

            KdRAPv = RowIndex.Cells(1).Text
            NoUrutv = SelectRecord.Cells(2).Text 'no urut di baris terkini (current row)

            UpdatePreference(NoUrutv, KdRAPv)
            Call BindGrid()

        End If
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub UpdatePreference(NoUrutv As Integer, KdRAPv As String)
        Dim CmdEdit As New Data.SqlClient.SqlCommand
        With CmdEdit
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "UPDATE RAP SET NoUrut=@P1 WHERE JobNo=@P2 AND KdRAP=@P3"
            .Parameters.AddWithValue("@P1", NoUrutv)
            .Parameters.AddWithValue("@P2", DDLJob.Value)
            .Parameters.AddWithValue("@P3", KdRAPv)
            .ExecuteNonQuery()
            .Dispose()
        End With
    End Sub

    Private Sub BindJob()
        Dim AksesJob As String = ""
        Dim CmdFind As New Data.SqlClient.SqlCommand
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
        RsFind.Close()
        CmdFind.Dispose()

        DDLJob.Items.Clear()
        DDLJob.Items.Add("Pilih salah satu", "0")
        Dim CmdIsi As New Data.SqlClient.SqlCommand
        With CmdIsi
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT JobNo,JobNm FROM Job"
        End With
        Dim RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
        While RsIsi.Read
            If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsIsi("JobNo")) >= 0 Then
                DDLJob.Items.Add(RsIsi("JobNo") & " - " & RsIsi("JobNm"), RsIsi("JobNo"))
            End If
        End While
        RsIsi.Close()
        CmdIsi.Dispose()

        DDLJob.Value = "0"

    End Sub

    Protected Sub DDLJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindVersi()
        Call BindGrid()
    End Sub

    Private Sub GridRAP_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridRAP.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(7).Text = Format(e.Row.Cells(5).Text * e.Row.Cells(6).Text, "N2")
            e.Row.Cells(8).Text = Format((e.Row.Cells(7).Text / TtlRAP), "P")
            e.Row.Cells(10).Text = Format(e.Row.Cells(5).Text * e.Row.Cells(9).Text, "N2")
            If TtlRAB = 0 Then
                e.Row.Cells(11).Text = Format(0, "P")
            Else
                e.Row.Cells(11).Text = Format((e.Row.Cells(10).Text / TtlRAB), "P")
            End If
            If e.Row.Cells(10).Text = "0" Then
                e.Row.Cells(12).Text = Format(0, "P")
            Else
                e.Row.Cells(12).Text = Format(e.Row.Cells(7).Text / e.Row.Cells(10).Text, "P")
            End If

            If e.Row.Cells(0).Text = "Header" Then
                e.Row.Cells(1).Font.Bold = True
                e.Row.Cells(3).Font.Bold = True
                e.Row.Cells(7).Font.Bold = True
                e.Row.Cells(8).Font.Bold = True
                e.Row.Cells(10).Font.Bold = True
                e.Row.Cells(11).Font.Bold = True
                e.Row.Cells(12).Font.Bold = True
                e.Row.Cells(4).Text = ""
                e.Row.Cells(5).Text = ""
                e.Row.Cells(6).Text = ""
                e.Row.Cells(9).Text = ""
            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(4).Text = "Total : "
            e.Row.Cells(4).Font.Bold = True
            If TtlRAB = 0 Then
                e.Row.Cells(10).Text = Format(0, "P")
            Else
                e.Row.Cells(10).Text = Format(TtlRAP / TtlRAB, "P")
            End If

        End If

    End Sub

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If DDLJob.Value = "0" Then
            LblErr.Text = "Belum pilih Job."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If DDLAlokasi.Value = "0" Then
            LblErr.Text = "Belum pilih Alokasi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If DDLVersion.Value <> "RAP" Then
            LblErr.Text = "Hanya bisa tambah data untuk RAP Versi " & DDLVersion.Items.FindByValue("RAP").Text & "."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        Session("RAP") = "NEW||" & DDLJob.Text & "|" & DDLVersion.Value & "|" & DDLVersion.Text & "|" & DDLAlokasi.Value

        Response.Redirect("FrmEntryRAP.aspx")
        Exit Sub
    End Sub

    Protected Sub BtnDel_Click(sender As Object, e As EventArgs) Handles BtnDel.Click
        Dim KdRAP As String = String.Empty
        'Jika ada detail lain dalam header yg sama, hitung ulang total, else assign header = 0 -- Part 1
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2 AND Tipe='Detail' AND KdRAP=@P3"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                .Parameters.AddWithValue("@P3", Session("Delete"))
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    KdRAP = RsFind("Header")
                End If
            End Using
        End Using

        Dim CmdDelete As New Data.SqlClient.SqlCommand
        With CmdDelete
            .Connection = Conn
            .CommandType = CommandType.Text
            'If Session("Delete").ToString.Split("|")(0) = "Header" Then
            '    .CommandText = "DELETE FROM RAP WHERE JobNo=@P1 AND (KdRAP=@P2 OR Header=@P2)"
            'Else
            .CommandText = "DELETE FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2 AND KdRAP=@P3"
            'End If
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
            .Parameters.AddWithValue("@P3", Session("Delete"))
            .ExecuteNonQuery()
            .Dispose()
        End With

        Session.Remove("Delete")
        DelMsg.ShowOnPageLoad = False

        'Jika ada detail lain dalam header yg sama, hitung ulang total, else assign header = 0 -- Part 2
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2 AND Tipe='Detail' AND Header=@P3"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                .Parameters.AddWithValue("@P3", KdRAP)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    Call HitRecursive(KdRAP)
                Else
                    Using CmdEdit As New Data.SqlClient.SqlCommand
                        With CmdEdit
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "UPDATE RAP SET HrgSatuan=0,HrgRAB=0 WHERE JobNo=@P1 AND KdRAP=@P2 AND Alokasi=@P3"
                            .Parameters.AddWithValue("@P1", DDLJob.Value)
                            .Parameters.AddWithValue("@P2", KdRAP)
                            .Parameters.AddWithValue("@P3", DDLAlokasi.Value)
                            .ExecuteNonQuery()
                        End With
                    End Using
                End If
            End Using
        End Using

        Call BindGrid()

    End Sub

    Protected Sub DDLAlokasi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLAlokasi.SelectedIndexChanged
        Call BindVersi()
    End Sub

    Private Sub DDLVersion_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLVersion.SelectedIndexChanged
        'Call BindGrid()
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If ValidasiVersi = False Then Exit Sub

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO RAPH SELECT * FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
            .ExecuteNonQuery()
            .Dispose()
        End With

        Dim CmdEdit As New Data.SqlClient.SqlCommand
        With CmdEdit
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "UPDATE RAP SET Versi=@P1," & _
                           "UserEntry=@P2,TimeEntry=@P3 WHERE JobNo=@P4 AND Alokasi=@P5"
            .Parameters.AddWithValue("@P1", TxtVersi.Text)
            .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P3", Now)
            .Parameters.AddWithValue("@P4", DDLJob.Value)
            .Parameters.AddWithValue("@P5", DDLAlokasi.Value)
            .ExecuteNonQuery()
            .Dispose()
        End With

        'Session.Remove("CurrVersi")
        VersionEntry.ShowOnPageLoad = False
        Call BindVersi()
        Call BindGrid()

    End Sub
    
    Protected Sub BtnVersi_Click(sender As Object, e As EventArgs) Handles BtnVersi.Click
        If DDLJob.Value = "0" Then
            LblErr.Text = "Belum pilih Job."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If DDLAlokasi.Value = "0" Then
            LblErr.Text = "Belum pilih Alokasi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If GridRAP.Rows.Count = 0 Then
            LblErr.Text = "Belum ada data."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If DDLVersion.Value <> "RAP" Then
            LblErr.Text = "Hanya bisa tambah versi untuk RAP Versi " & DDLVersion.Items.FindByValue("RAP").Text & "."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        RdoMajor.Checked = False
        RdoMinor.Checked = False
        TxtCurrVersi.Text = DDLVersion.Items.FindByValue("RAP").Text
        TxtVersi.Text = ""
        'Session("CurrVersi") = TxtCurrVersi.Text
        VersionEntry.ShowOnPageLoad = True
    End Sub

    Private Function ValidasiVersi() As Boolean
        If TxtVersi.Text = "" Then
            LblErr.Text = "Belum pilih perubahan major atau minor."
            ErrMsg.ShowOnPageLoad = True
            Return False
        End If

        'Validasi Versi di RAP
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TOP 1 Versi FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2 AND Versi=@P3"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
            .Parameters.AddWithValue("@P3", TxtVersi.Text)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            RsFind.Close()
            CmdFind.Dispose()
            LblErr.Text = "Versi " & TxtVersi.Text & " sudah ada."
            ErrMsg.ShowOnPageLoad = True
            Return False
        End If
        RsFind.Close()
        CmdFind.Dispose()

        'Validasi Versi di RAPH
        Dim CmdFind1 As New Data.SqlClient.SqlCommand
        With CmdFind1
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TOP 1 Versi FROM RAPH WHERE JobNo=@P1 AND Alokasi=@P2 AND Versi=@P3"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
            .Parameters.AddWithValue("@P3", TxtVersi.Text)
        End With
        Dim RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
        If RsFind1.Read Then
            RsFind1.Close()
            CmdFind1.Dispose()
            LblErr.Text = "Versi " & TxtVersi.Text & " sudah ada."
            ErrMsg.ShowOnPageLoad = True
            Return False
        End If
        RsFind1.Close()
        CmdFind1.Dispose()

        Return True
    End Function

    Protected Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        If DDLJob.Value = "0" Then
            LblErr.Text = "Belum pilih Job."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        'Dim script As String = "window.onload = function() {OpenNewTab();};"
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", "OpenNewTab();", True)
        Session("Print") = DDLJob.Value & "|" & DDLAlokasi.Value & "|" & DDLVersion.Value & "|" & DDLVersion.Text
        Response.Redirect("FrmRptRAP.aspx")
    End Sub

    Private Sub RdoMajor_CheckedChanged(sender As Object, e As System.EventArgs) Handles RdoMajor.CheckedChanged
        'If Session("CurrVersi") Is Nothing Then Exit Sub
        'Dim CurrVersi As String = Session("CurrVersi").ToString
        'TxtVersi.Text = Left(Session("CurrVersi"), 1) + 1 & ".0"
        'TxtVersi.Text = Left(TxtCurrVersi.Text, 1) + 1 & ".0"
        TxtVersi.Text = TxtCurrVersi.Text.Split(".")(0) + 1 & ".0"
    End Sub

    Private Sub RdoMinor_CheckedChanged(sender As Object, e As System.EventArgs) Handles RdoMinor.CheckedChanged
        'If Session("CurrVersi") Is Nothing Then Exit Sub
        'Dim CurrVersi As String = Session("CurrVersi").ToString
        'TxtVersi.Text = Left(CurrVersi, 1) & "." & Right(CurrVersi, 1) + 1
        'TxtVersi.Text = Left(TxtCurrVersi.Text, 1) & "." & Right(TxtCurrVersi.Text, 1) + 1
        TxtVersi.Text = TxtCurrVersi.Text.Split(".")(0) & "." & TxtCurrVersi.Text.Split(".")(1) + 1
    End Sub
    
    'Private Sub BtnCcl_Click(sender As Object, e As System.EventArgs) Handles BtnCcl.Click
    'Session.Remove("CurrVersi")
    'VersionEntry.ShowOnPageLoad = False
    'End Sub

    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
        Dim cell As New TableHeaderCell()

        cell = New TableHeaderCell()
        cell.ColumnSpan = 4
        row.Controls.Add(cell)

        cell = New TableHeaderCell()
        cell.Text = "RAP"
        cell.ColumnSpan = 3
        row.Controls.Add(cell)

        cell = New TableHeaderCell()
        cell.ColumnSpan = 3
        cell.Text = "RAB"
        row.Controls.Add(cell)

        cell = New TableHeaderCell()
        cell.ColumnSpan = 6
        row.Controls.Add(cell)

        row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")
        row.ForeColor = System.Drawing.Color.Black
        GridRAP.HeaderRow.Parent.Controls.AddAt(0, row)
    End Sub

    Private Sub HitRecursive(ByVal KdRAP As String)
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT SUM(HrgSatuan*Vol), SUM(HrgRAB*Vol) FROM RAP WHERE JobNo=@P1 AND Header=@P2 AND Alokasi=@P3"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", KdRAP)
            .Parameters.AddWithValue("@P3", DDLAlokasi.Value)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        Dim Totalv As Decimal = 0
        Dim TotalRAB As Decimal = 0
        If RsFind.Read Then
            Totalv = RsFind(0)
            TotalRAB = RsFind(1)
        End If
        RsFind.Close()
        CmdFind.Dispose()

        Dim CmdEdit As New Data.SqlClient.SqlCommand
        With CmdEdit
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "UPDATE RAP SET HrgSatuan=@P1,HrgRAB=@P2 WHERE JobNo=@P3 AND KdRAP=@P4 AND Alokasi=@P5"
            .Parameters.AddWithValue("@P1", Totalv)
            .Parameters.AddWithValue("@P2", TotalRAB)
            .Parameters.AddWithValue("@P3", DDLJob.Value)
            .Parameters.AddWithValue("@P4", KdRAP)
            .Parameters.AddWithValue("@P5", DDLAlokasi.Value)
            .ExecuteNonQuery()
            .Dispose()
        End With

        Dim Header As String = "0" '0 adalah top header, selain itu adalah sub header
        Dim CmdFind1 As New Data.SqlClient.SqlCommand
        With CmdFind1
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TOP 1 Header FROM RAP WHERE JobNo=@P1 AND KdRAP=@P2 AND Alokasi=@P3"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", KdRAP)
            .Parameters.AddWithValue("@P3", DDLAlokasi.Value)
        End With
        Dim RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
        If RsFind1.Read Then
            Header = RsFind1("Header")
        End If
        RsFind1.Close()
        CmdFind1.Dispose()

        If Header = "0" Then
            Exit Sub
        Else
            Call HitRecursive(Header)
        End If

    End Sub

End Class