Public Class FrmEntryRKD
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim TmpDt As New DataTable() 'Untuk uraian PD

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "PD") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        LblAction.Text = Session("PD").ToString.Split("|")(0)
        LblJobNo.Text = Session("PD").ToString.Split("|")(1)
        LblNoPD.Text = Session("PD").ToString.Split("|")(2)
        LblSource.Text = Session("PD").ToString.Split("|")(3)

        If IsPostBack = False Then
            TabPage.ActiveTabIndex = 0
            Call BindBank()
            Call BindAlokasi()
            Call BindGrid()
        End If

    End Sub

    Private Sub BindForm()
        DDLForm.Items.Clear()
        DDLForm.Items.Add("Pilih salah satu", "0")

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TipeForm,Keterangan FROM TipeForm WHERE Alokasi=@P1"
            .Parameters.AddWithValue("@P1", DDLAlokasi.Value)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        While RsFind.Read
            DDLForm.Items.Add(RsFind("TipeForm") & " - " & RsFind("Keterangan"), RsFind("TipeForm"))
        End While
        RsFind.Close()
        CmdFind.Dispose()

        DDLForm.Value = "0"

    End Sub

    Private Sub BindBank()
        DDLBank.Items.Clear()
        DDLBank.Items.Add("Pilih salah satu", "0")

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT Bank FROM Bank"
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        While RsFind.Read
            DDLBank.Items.Add(RsFind("Bank"), RsFind("Bank"))
        End While
        RsFind.Close()
        CmdFind.Dispose()

        DDLBank.Value = "0"

    End Sub

    Private Sub BindKO()
        DDLKo.Items.Clear()

        If DDLForm.Value <> "04B" And DDLForm.Value <> "05B" Then
            DDLKo.Items.Add(Trim(LblJobNo.Text.Split("-")(0)) & "_INT", Trim(LblJobNo.Text.Split("-")(0)) & "_INT")
            DDLKo.SelectedIndex = 1
            DDLKo.Enabled = False
        Else
            DDLKo.Items.Add("Pilih salah satu", "0")

            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                If DDLForm.Value = "04B" Then
                    .CommandText = "SELECT * FROM KoHdr WHERE JobNo=@P1 AND KategoriId=@P2 AND ApprovedBy IS NOT NULL"
                Else
                    .CommandText = "SELECT * FROM KoHdr WHERE JobNo=@P1 AND KategoriId!=@P2 AND ApprovedBy IS NOT NULL"
                End If
                .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P2", "KONTRAK")
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                If Left(LblAction.Text, 3) = "SEE" Then
                    DDLKo.Items.Add(RsFind("NoKO"), RsFind("NoKO"))
                Else
                    If IsDBNull(RsFind("ClosedBy")) = False Then Continue While 'Jika status kontrak sudah tutup
                    If RsFind("SubTotal") - RsFind("DiscAmount") + RsFind("PPN") > RsFind("TotalTerbayar") Then 'Jika Nilai KO terbayar belum lunas
                        DDLKo.Items.Add(RsFind("NoKO"), RsFind("NoKO"))
                    End If
                End If
            End While
            RsFind.Close()
            CmdFind.Dispose()
            DDLKo.Value = "0"
            DDLKo.Enabled = True
        End If

        DDLKo_SelectedIndexChanged(DDLKo, New EventArgs())

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

    Private Sub BindRAP()
        DDLRap.Items.Clear()
        DDLRap.Items.Add("Pilih salah satu", "0")

        Dim CmdBind As New Data.SqlClient.SqlCommand
        With CmdBind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT KdRAP,Uraian,Tipe FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2 ORDER BY NoUrut ASC"
            .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
            .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
        End With
        Dim RsBind As Data.SqlClient.SqlDataReader = CmdBind.ExecuteReader
        While RsBind.Read
            DDLRap.Items.Add(RsBind("KdRAP") & " - " & RsBind("Uraian"), RsBind("Tipe") & "|" & RsBind("KdRAP"))
        End While
        RsBind.Close()
        CmdBind.Dispose()

    End Sub

    Private Sub BindVendor()
        Dim CmdBind As New Data.SqlClient.SqlCommand
        With CmdBind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Vendor WHERE VendorId=@P1"
            .Parameters.AddWithValue("@P1", TxtVendor.Text)
        End With
        Dim RsBind As Data.SqlClient.SqlDataReader = CmdBind.ExecuteReader
        If RsBind.Read Then
            TxtNama.Text = RsBind("VendorNm")
            TxtAlamat.Text = RsBind("Alamat").ToString
            TxtTelepon.Text = RsBind("Telepon1").ToString
            TxtNPWP.Text = RsBind("NPWP").ToString
            TxtNoRek.Text = RsBind("NoRek").ToString
            TxtAN.Text = RsBind("AtasNama").ToString
            DDLBank.Value = RsBind("Bank").ToString
        End If
        RsBind.Close()
        CmdBind.Dispose()

    End Sub

    Private Sub BindGrid()
        TglPD.Date = Format(Now, "dd-MMM-yyyy")
        TglPD_DateChanged(TglPD, New EventArgs())

        TxtJob.Text = LblJobNo.Text

        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("NoUrut", GetType(Integer)), _
                                                 New DataColumn("Tipe", GetType(String)), _
                                                 New DataColumn("KdRAP", GetType(String)), _
                                                 New DataColumn("Uraian", GetType(String)), _
                                                 New DataColumn("Vol", GetType(Decimal)), _
                                                 New DataColumn("Uom", GetType(String)), _
                                                 New DataColumn("HrgSatuan", GetType(Decimal))})

        If LblAction.Text <> "NEW" Then
            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM PdHdr WHERE NoPD=@P1"
                .Parameters.AddWithValue("@P1", LblNoPD.Text)
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                TxtNoPD.Text = RsFind("NoPD")
                TxtNoRef.Text = RsFind("NoRef").ToString
                DDLAlokasi.Value = RsFind("Alokasi")
                DDLAlokasi_SelectedIndexChanged(DDLAlokasi, New EventArgs())
                PrdAwal.Date = RsFind("PrdAwal")
                PrdAkhir.Date = RsFind("PrdAkhir")
                TxtMinggu.Text = RsFind("Minggu")
                TglPD.Date = RsFind("TglPD")
                DDLForm.Value = RsFind("TipeForm")
                'DDLForm_SelectedIndexChanged(DDLForm, New EventArgs())
                TxtDesc.Text = RsFind("Deskripsi")
                AssignCB(RsFind("BuktiPendukung").ToString)
                TxtNoTagihan.Text = RsFind("NoTagihan").ToString
                Call BindKO()
                If DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT" Then 'Ambil value dari PdHdr
                    TxtNama.Text = RsFind("Nama").ToString
                    TxtAlamat.Text = RsFind("Alamat").ToString
                    TxtTelepon.Text = RsFind("Telepon").ToString
                    TxtNPWP.Text = RsFind("NPWP").ToString
                    TxtNoRek.Text = RsFind("NoRek").ToString
                    TxtAN.Text = RsFind("AtasNama").ToString
                    DDLBank.SelectedItem = DDLBank.Items.FindByValue(RsFind("Bank").ToString)

                    TxtNama.Enabled = True
                    TxtAlamat.Enabled = True
                    TxtTelepon.Enabled = True
                    TxtNPWP.Enabled = True

                    TxtNoRek.Enabled = True
                    TxtAN.Enabled = True
                    DDLBank.Enabled = True
                Else
                    DDLKo.Value = RsFind("NoKO")
                    DDLKo_SelectedIndexChanged(DDLKo, New EventArgs())
                End If

                Dim CmdGrid As New Data.SqlClient.SqlCommand
                With CmdGrid
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM PdDtl WHERE NoPD=@P1 ORDER BY NoUrut"
                    .Parameters.AddWithValue("@P1", LblNoPD.Text)
                End With
                Dim RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                While RsGrid.Read
                    TmpDt.Rows.Add(RsGrid("NoUrut"), AssignTipe(RsGrid("KdRAP").ToString), RsGrid("KdRAP"), RsGrid("Uraian"), RsGrid("Vol"), RsGrid("Uom"), RsGrid("HrgSatuan"))
                End While
                RsGrid.Close()
                CmdGrid.Dispose()

                DDLForm.Enabled = False
                DDLAlokasi.Enabled = False

                If IsDBNull(RsFind("RejectBy")) = False Then
                    ImgReject.Visible = True
                    LblRejectBy.Text = "By " + RsFind("RejectBy")
                    LblRejectBy.Visible = True
                    LblRejectOn.Text = "On " + Format(RsFind("TimeReject"), "dd-MMM-yyyy HH:mm")
                    LblRejectOn.Visible = True
                End If

            End If
            RsFind.Close()
            CmdFind.Dispose()

        End If

        Session("TmpDt") = TmpDt
        GridPD.DataSource = TmpDt
        Call GetSubTotal()
        GridPD.DataBind()
        Call GetSaldo()

        If Left(LblAction.Text, 3) = "SEE" Or ImgReject.Visible = True Then
            DisableControls(Form)
        End If

    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Session("Job") = LblJobNo.Text

        Session.Remove("TmpDt")
        TmpDt.Dispose()
        Session.Remove("PD")

        Response.Redirect(LblSource.Text)

        Exit Sub
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As System.EventArgs) Handles BtnSave.Click
        If DDLAlokasi.Value = "0" Then
            LblErr.Text = "Alokasi belum di-pilih."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If DDLForm.Value = "0" Then
            LblErr.Text = "Tipe form belum di-pilih."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT StatusJob FROM Job WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    If (RsFind("StatusJob") = "Proposal") And DDLAlokasi.Value <> "A" Then
                        LblErr.Text = "Job: " & TxtJob.Text & "<br />" & "Status = " & RsFind("StatusJob") & "<br />" & _
                                      "Hanya Alokasi A yang bisa di-PD kan."
                        ErrMsg.ShowOnPageLoad = True
                        Exit Sub
                    End If
                End If
            End Using
        End Using

        If DDLForm.Value = "04B" Or DDLForm.Value = "05B" Or DDLForm.Value = "05C" Then 'Khusus subkon/supplier
            If SaveCB() = "" Then
                LblErr.Text = "Belum pilih bukti pendukung."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            If DDLForm.Value = "04B" Or DDLForm.Value = "05B" Then
                If DDLKo.Value = "0" Then
                    LblErr.Text = "Belum pilih No Kontrak/PO."
                    ErrMsg.ShowOnPageLoad = True
                    Exit Sub
                End If

                Dim TotalKO As Decimal = 0
                Dim TotalPD As Decimal = 0
                Dim TotalTerbayar As Decimal = 0

                Using CmdFind As New Data.SqlClient.SqlCommand
                    With CmdFind
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        If LblAction.Text = "NEW" Then
                            .CommandText = "SELECT * FROM PdHdr WHERE NoKO=@P2 AND RejectBy IS NULL"
                        Else
                            .CommandText = "SELECT * FROM PdHdr WHERE NoPD!=@P1 AND NoKO=@P2 AND RejectBy IS NULL"
                        End If
                        .Parameters.AddWithValue("@P1", TxtNoPD.Text)
                        .Parameters.AddWithValue("@P2", DDLKo.Value)
                    End With
                    Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                        While RsFind.Read
                            TotalPD += RsFind("TotalPD")
                            TotalTerbayar += If(IsDBNull(RsFind("TglBayar")) = True, 0, RsFind("TotalPD"))
                        End While
                    End Using
                End Using

                TotalPD += CDec(TxtTotal.Text)
                Using CmdFind As New Data.SqlClient.SqlCommand
                    With CmdFind
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT * FROM KoHdr WHERE NoKO=@P1"
                        .Parameters.AddWithValue("@P1", DDLKo.Value)
                    End With
                    Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                        If RsFind.Read Then
                            TotalKO = RsFind("SubTotal") - RsFind("DiscAmount") + RsFind("PPN")

                            If TotalKO < TotalPD Then
                                LblErr.Text = "Total Akumulasi PD melebihi nilai Kontrak/PO." & "<br />" & _
                                              "Nilai Kontrak/PO " & RsFind("NoKO") & " : " & Format(TotalKO, "N0") & "<br />" & _
                                              "Total Akumulasi PD termasuk current : " & Format(TotalPD, "N0") & "<br />" & _
                                              "Nilai Kontrak/PO yang sudah dibayar : " & Format(TotalTerbayar, "N0")
                                ErrMsg.ShowOnPageLoad = True
                                Exit Sub
                            End If
                        End If
                    End Using
                End Using

            End If
        End If
        If DDLBank.Value = "0" Then
            LblErr.Text = "Bank rekening penerima belum di-pilih."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtEditSaldo.Visible = True And Trim(TxtEditSaldo.Text) = "" Then
            LblErr.Text = "Remark Edit Saldo belum diisi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        TmpDt = Session("TmpDt")
        If TmpDt.Rows.Count = 0 Then
            TabPage.ActiveTabIndex = 0
            LblErr.Text = "Belum input uraian permintaan dana."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        'For Each row As DataRow In TmpDt.Rows
        '    Dim CmdFind1 As New Data.SqlClient.SqlCommand
        '    With CmdFind1
        '        .Connection = Conn
        '        .CommandType = CommandType.Text
        '        .CommandText = "SELECT * FROM RAP WHERE JobNo=@P1 AND KdRAP=@P2"
        '        .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
        '        .Parameters.AddWithValue("@P2", row("KdRAP"))
        '    End With
        '    Dim RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
        '    If RsFind1.Read Then
        '        If RsFind1("TotalTerserap") + (row("Vol") * row("HrgSatuan")) > RsFind1("Vol") * RsFind1("HrgSatuan") Then
        '            LblErr.Text = "PD untuk kode RAP " + RsFind1("KdRAP") + " - " + RsFind1("Uraian") + " <br />" +
        '                          "sudah melebihi RAP" + "<br />" + _
        '                          "Anggaran RAP : " + Format(RsFind1("Vol") * RsFind1("HrgSatuan"), "N0") + "<br />" + _
        '                          "Yang sudah terserap : " + Format(RsFind1("TotalTerserap"), "N0") + "<br /> " + _
        '                          "Nominal PD (Current) : " + Format(row("Vol") * row("HrgSatuan"), "N0")
        '            RsFind1.Close()
        '            CmdFind1.Dispose()
        '            ErrMsg.ShowOnPageLoad = True
        '            Exit Sub
        '        End If
        '    End If
        '    RsFind1.Close()
        '    CmdFind1.Dispose()
        'Next

        If LblAction.Text = "NEW" Then
            TxtNoPD.Text = AssignNoPD()
            If TxtNoPD.Text = "" Then
                LblErr.Text = "Error while generate No PD."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If

            Using CmdInsert As New Data.SqlClient.SqlCommand
                With CmdInsert
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO PdHdr (NoPD,NoRef,JobNo,TglPD,Deskripsi,PrdAwal,PrdAkhir,Minggu,Alokasi,TipeForm,TotalPD,BuktiPendukung," & _
                                   "NoKO,NoTagihan,Nama,Alamat,Telepon,NPWP,NoRek,Bank,AtasNama,UserEntry,TimeEntry,KSO) VALUES " & _
                                   "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22,@P23,@P24)"
                    .Parameters.AddWithValue("@P1", TxtNoPD.Text)
                    .Parameters.AddWithValue("@P2", TxtNoRef.Text)
                    .Parameters.AddWithValue("@P3", Trim(LblJobNo.Text.Split("-")(0)))
                    .Parameters.AddWithValue("@P4", TglPD.Date)
                    .Parameters.AddWithValue("@P5", TxtDesc.Text)
                    .Parameters.AddWithValue("@P6", PrdAwal.Date)
                    .Parameters.AddWithValue("@P7", PrdAkhir.Date)
                    .Parameters.AddWithValue("@P8", TxtMinggu.Text)
                    .Parameters.AddWithValue("@P9", DDLAlokasi.Value)
                    .Parameters.AddWithValue("@P10", DDLForm.Value)
                    .Parameters.AddWithValue("@P11", TxtSubTotal.Text)
                    .Parameters.AddWithValue("@P12", SaveCB)
                    .Parameters.AddWithValue("@P13", DDLKo.Value)
                    .Parameters.AddWithValue("@P14", TxtNoTagihan.Text.ToString)
                    .Parameters.AddWithValue("@P15", If(DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT", TxtNama.Text, ""))
                    .Parameters.AddWithValue("@P16", If(DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT", TxtAlamat.Text, ""))
                    .Parameters.AddWithValue("@P17", If(DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT", TxtTelepon.Text, ""))
                    .Parameters.AddWithValue("@P18", If(DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT", TxtNPWP.Text, ""))
                    .Parameters.AddWithValue("@P19", If(DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT", TxtNoRek.Text, ""))
                    .Parameters.AddWithValue("@P20", If(DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT", DDLBank.Value, ""))
                    .Parameters.AddWithValue("@P21", If(DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT", TxtAN.Text, ""))
                    .Parameters.AddWithValue("@P22", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P23", Now)
                    .Parameters.AddWithValue("@P24", 1)
                    .ExecuteNonQuery()
                End With
            End Using

        Else
            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE PdHdr SET TglPD=@P1,Deskripsi=@P2,PrdAwal=@P3,PrdAkhir=@P4,Minggu=@P5,TotalPD=@P6,BuktiPendukung=@P7," & _
                                   "NoKO=@P8,NoTagihan=@P9,Nama=@P10,Alamat=@P11,Telepon=@P12,NPWP=@P13,NoRek=@P14,Bank=@P15," & _
                                   "AtasNama=@P16,UserEntry=@P17,TimeEntry=@P18,NoRef=@P19 WHERE NoPD=@P20"
                    .Parameters.AddWithValue("@P1", TglPD.Date)
                    .Parameters.AddWithValue("@P2", TxtDesc.Text)
                    .Parameters.AddWithValue("@P3", PrdAwal.Date)
                    .Parameters.AddWithValue("@P4", PrdAkhir.Date)
                    .Parameters.AddWithValue("@P5", TxtMinggu.Text)
                    .Parameters.AddWithValue("@P6", TxtSubTotal.Text)
                    .Parameters.AddWithValue("@P7", SaveCB)
                    .Parameters.AddWithValue("@P8", DDLKo.Value)
                    .Parameters.AddWithValue("@P9", TxtNoTagihan.Text.ToString)
                    .Parameters.AddWithValue("@P10", If(DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT", TxtNama.Text, ""))
                    .Parameters.AddWithValue("@P11", If(DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT", TxtAlamat.Text, ""))
                    .Parameters.AddWithValue("@P12", If(DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT", TxtTelepon.Text, ""))
                    .Parameters.AddWithValue("@P13", If(DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT", TxtNPWP.Text, ""))
                    .Parameters.AddWithValue("@P14", If(DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT", TxtNoRek.Text, ""))
                    .Parameters.AddWithValue("@P15", If(DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT", DDLBank.Value, ""))
                    .Parameters.AddWithValue("@P16", If(DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT", TxtAN.Text, ""))
                    .Parameters.AddWithValue("@P17", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P18", Now)
                    .Parameters.AddWithValue("@P19", TxtNoRef.Text)
                    .Parameters.AddWithValue("@P20", TxtNoPD.Text)
                    .ExecuteNonQuery()
                End With
            End Using

            Using CmdDel As New Data.SqlClient.SqlCommand
                With CmdDel
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "DELETE PdDtl WHERE NoPD=@P1"
                    .Parameters.AddWithValue("@P1", TxtNoPD.Text)
                    .ExecuteNonQuery()
                End With
            End Using

        End If

        Using CmdInsert As New Data.SqlClient.SqlCommand
            For Each row As DataRow In TmpDt.Rows
                CmdInsert.Parameters.Clear()
                With CmdInsert
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO PdDtl (NoPD,NoUrut,KdRAP,Uraian,Vol,Uom,HrgSatuan,UserEntry,TimeEntry) VALUES " & _
                                        "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)"
                    .Parameters.AddWithValue("@P1", TxtNoPD.Text)
                    .Parameters.AddWithValue("@P2", row("NoUrut"))
                    .Parameters.AddWithValue("@P3", row("KdRAP"))
                    .Parameters.AddWithValue("@P4", row("Uraian"))
                    .Parameters.AddWithValue("@P5", row("Vol"))
                    .Parameters.AddWithValue("@P6", row("Uom"))
                    .Parameters.AddWithValue("@P7", row("HrgSatuan"))
                    .Parameters.AddWithValue("@P8", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P9", Now)
                    .ExecuteNonQuery()
                End With
            Next row
        End Using

        'Override saldo untuk PD sebelumnya.
        'If Trim(TxtEditSaldo.Text) <> "" Then
        '    Using CmdFind As New Data.SqlClient.SqlCommand
        '        With CmdFind
        '            .Connection = Conn
        '            .CommandType = CommandType.Text
        '            If LblAction.Text = "NEW" Then
        '                .CommandText = "SELECT TOP 1 * FROM PdHdr WHERE JobNo=@P2 AND TipeForm=@P3 AND KSO=1 AND RejectBy IS NULL ORDER BY NoPD DESC"
        '            Else
        '                .CommandText = "SELECT TOP 1 * FROM PdHdr WHERE NoPD<@P1 AND JobNo=@P2 AND TipeForm=@P3 AND KSO=1 AND RejectBy IS NULL ORDER BY NoPD DESC"
        '            End If
        '            .Parameters.AddWithValue("@P1", LblNoPD.Text)
        '            .Parameters.AddWithValue("@P2", Trim(LblJobNo.Text.Split("-")(0)))
        '            .Parameters.AddWithValue("@P3", Trim(DDLForm.Value))
        '        End With
        '        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        '            If RsFind.Read Then
        '                Using CmdEdit As New Data.SqlClient.SqlCommand
        '                    With CmdEdit
        '                        .Connection = Conn
        '                        .CommandType = CommandType.Text
        '                        .CommandText = "UPDATE PdHdr SET OverrideSaldo='1', RemarkOverrideSaldo=@P1, Saldo=@P3 WHERE NoPD=@P2"
        '                        .Parameters.AddWithValue("@P1", TxtEditSaldo.Text)
        '                        .Parameters.AddWithValue("@P2", RsFind("NoPD"))
        '                        .Parameters.AddWithValue("@P3", TxtSaldo.Value)
        '                        .ExecuteNonQuery()
        '                    End With
        '                End Using
        '            End If
        '        End Using
        '    End Using
        'End If

        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub

    Protected Sub TglPD_DateChanged(sender As Object, e As EventArgs) Handles TglPD.DateChanged
        PrdAwal.Value = FindMondayDate(TglPD.Value)
        PrdAkhir.Value = (PrdAwal.Value).AddDays(6)

        TxtMinggu.Text = If(FindWeekNumber(TglPD.Value) < 0, "0", FindWeekNumber(TglPD.Value))
    End Sub

    Private Sub GridPD_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridPD.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridPD.Rows(e.CommandArgument)

            TxtAction.Text = "UPD"
            TxtNo.Text = SelectRecord.Cells(0).Text
            Call BindRAP()
            DDLRap.Value = SelectRecord.Cells(1).Text + "|" + SelectRecord.Cells(2).Text
            TxtUraian.Text = TryCast(SelectRecord.FindControl("LblUraian"), Label).Text.Replace("<br />", Environment.NewLine)
            TxtVol.Text = SelectRecord.Cells(4).Text
            TxtUom.Text = SelectRecord.Cells(5).Text
            TxtHrgSatuan.Text = SelectRecord.Cells(6).Text
            PopEntry.ShowOnPageLoad = True

        ElseIf e.CommandName = "BtnDelete" Then
            Dim SelectRecord As GridViewRow = GridPD.Rows(e.CommandArgument)

            TmpDt = Session("TmpDt")
            'dt.DefaultView.Sort = "regNumber DESC" 'Sort lbh dahulu baru delete, jika tidak ada akan salah delete krn sort.
            TmpDt.Rows(e.CommandArgument).Delete()
            TmpDt.AcceptChanges()

            GridPD.DataSource = TmpDt
            Call GetSubTotal()
            GridPD.DataBind()
            Session("TmpDt") = TmpDt
        End If
    End Sub

    Private Function AssignNoPD() As String
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT CounterKSO FROM Counter WHERE JobNo=@P1 AND Alokasi=@P2"
                .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    Using CmdEdit As New Data.SqlClient.SqlCommand
                        With CmdEdit
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "UPDATE Counter SET CounterKSO=@P1,UserEntry=@P2,TimeEntry=@P3 WHERE JobNo=@P4 AND Alokasi=@P5"
                            .Parameters.AddWithValue("@P1", If(IsDBNull(RsFind("CounterKSO")) = True, 1, RsFind("CounterKSO") + 1))
                            .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                            .Parameters.AddWithValue("@P3", Now)
                            .Parameters.AddWithValue("@P4", Trim(LblJobNo.Text.Split("-")(0)))
                            .Parameters.AddWithValue("@P5", DDLAlokasi.Value)
                            .ExecuteNonQuery()
                        End With
                    End Using
                    AssignNoPD = "PDRKD" & Trim(LblJobNo.Text.Split("-")(0)) & DDLAlokasi.Value & Format(If(IsDBNull(RsFind("CounterKSO")) = True, 1, RsFind("CounterKSO") + 1), "00000")

                Else
                    Using CmdInsert As New Data.SqlClient.SqlCommand
                        With CmdInsert
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "INSERT INTO Counter(JobNo,Alokasi,CounterKSO,UserEntry,TimeEntry) VALUES " & _
                                           "(@P1,@P2,@P3,@P4,@P5)"
                            .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
                            .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                            .Parameters.AddWithValue("@P3", 1)
                            .Parameters.AddWithValue("@P4", Session("User").ToString.Split("|")(0))
                            .Parameters.AddWithValue("@P5", Now)
                            .ExecuteNonQuery()
                        End With
                    End Using
                    AssignNoPD = "PDRKD" & Trim(LblJobNo.Text.Split("-")(0)) & DDLAlokasi.Value & "00001"

                End If
            End Using
        End Using

    End Function

    Protected Sub BtnSave1_Click(sender As Object, e As EventArgs) Handles BtnSave1.Click

        If DDLRap.Value = "0" Then
            LblErr.Text = "Belum ada RAP yang di-pilih"
            ErrMsg.ShowOnPageLoad = True
            Call BindRAP()
            Exit Sub
        Else
            If DDLForm.Value <> "04B" And DDLRap.Value.ToString.Split("|")(0) = "Header" Then
                LblErr.Text = "Tidak bisa pilih RAP dengan Tipe Header."
                ErrMsg.ShowOnPageLoad = True
                Call BindRAP()
                Exit Sub
            End If
        End If
        If TxtUraian.Text = "" Then
            LblErr.Text = "Uraian belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtVol.Text = "" Or TxtVol.Text = "0" Then
            LblErr.Text = "Volume belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtUom.Text = "" Then
            LblErr.Text = "Satuan belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtHrgSatuan.Text = "" Or TxtHrgSatuan.Text = "0" Then
            LblErr.Text = "Harga satuan belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        TmpDt = Session("TmpDt")

        If TxtAction.Text = "NEW" Then
            Dim Counter As Integer
            Dim result As DataRow = TmpDt.Select("NoUrut > 0", "NoUrut DESC").FirstOrDefault
            If result Is Nothing Then
                Counter = 1
            Else
                Counter = result("NoUrut") + 1
            End If
            TmpDt.Rows.Add(Counter, AssignTipe(DDLRap.Value.ToString.Split("|")(1)), DDLRap.Value.ToString.Split("|")(1), TxtUraian.Text, TxtVol.Text, UCase(TxtUom.Text), TxtHrgSatuan.Text)
        Else
            Dim result As DataRow = TmpDt.Select("NoUrut='" & TxtNo.Text & "'").FirstOrDefault
            If result IsNot Nothing Then
                result("Tipe") = AssignTipe(DDLRap.Value.ToString.Split("|")(1))
                result("KdRAP") = DDLRap.Value.ToString.Split("|")(1)
                result("Uraian") = TxtUraian.Text
                result("Uom") = UCase(TxtUom.Text)
                result("Vol") = TxtVol.Text
                result("HrgSatuan") = TxtHrgSatuan.Text
            End If
        End If

        Session("TmpDt") = TmpDt
        GridPD.DataSource = TmpDt
        Call GetSubTotal()
        GridPD.DataBind()

        DDLAlokasi.Enabled = False 'Apabila sudah entry detail, maka tidak bisa rubah alokasi, supaya Kode RAP tetap konsisten.
        DDLForm.Enabled = False
        PopEntry.ShowOnPageLoad = False

    End Sub

    Private Sub AssignCB(ByVal ListCB As String)
        If ListCB <> "" Then
            CBInvoice.Checked = If(Array.IndexOf(ListCB.Split(","), "INV") >= 0, True, False)
            CBSJ.Checked = If(Array.IndexOf(ListCB.Split(","), "SJ") >= 0, True, False)
            CBPO.Checked = If(Array.IndexOf(ListCB.Split(","), "PO") >= 0, True, False)
            CBFP.Checked = If(Array.IndexOf(ListCB.Split(","), "FP") >= 0, True, False)
            CBBayar.Checked = If(Array.IndexOf(ListCB.Split(","), "BAP") >= 0, True, False)
            CBFisik.Checked = If(Array.IndexOf(ListCB.Split(","), "BAOP") >= 0, True, False)

        End If

    End Sub

    Private Function SaveCB() As String
        Dim ListCB As String

        ListCB = If(CBInvoice.Checked = True, "INV", "")
        ListCB = If(CBSJ.Checked = True, If(ListCB = "", "SJ", ListCB + "," + "SJ"), ListCB)
        ListCB = If(CBPO.Checked = True, If(ListCB = "", "PO", ListCB + "," + "PO"), ListCB)
        ListCB = If(CBFP.Checked = True, If(ListCB = "", "FP", ListCB + "," + "FP"), ListCB)
        ListCB = If(CBBayar.Checked = True, If(ListCB = "", "BAP", ListCB + "," + "BAP"), ListCB)
        ListCB = If(CBFisik.Checked = True, If(ListCB = "", "BAOP", ListCB + "," + "BAOP"), ListCB)

        Return ListCB

    End Function

    Private Function FindWeekNumber(Tglv As Date) As Integer
        'Function untuk cari minggu ke berapa untuk parameter input Tglv; convert masing2 tgl ke hari senin
        '1 minggu = Senin s.d. Minggu    

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT PrdAwal FROM Job WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    If IsDBNull(RsFind("PrdAwal")) Then
                        Return 0
                    Else
                        Return DateDiff(DateInterval.Weekday, FindMondayDate(RsFind("PrdAwal")), FindMondayDate(Tglv), Microsoft.VisualBasic.FirstDayOfWeek.Monday) + 1
                    End If
                Else
                    Return 0
                End If
            End Using
        End Using

    End Function

    Private Function FindMondayDate(Tglv As Date) As Date 'Function untuk cari tanggal hari senin untuk parameter input Tglv
        If Tglv.DayOfWeek = DayOfWeek.Sunday Then Tglv = Tglv.AddDays(-1)
        Dim MondayDate As DateTime
        MondayDate = Tglv.AddDays(DayOfWeek.Monday - Tglv.DayOfWeek)

        Return MondayDate

    End Function

    Protected Sub DDLForm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLForm.SelectedIndexChanged
        If DDLForm.Value = "0" Then
            DDLKo.Items.Clear()
            DDLKo.Items.Add("Pilih salah satu", "0")
            DDLKo.Value = "0"
            DDLKo.Enabled = False
            TxtNama.Enabled = False
            TxtAlamat.Enabled = False
            TxtTelepon.Enabled = False
            TxtNPWP.Enabled = False

            TxtNoRek.Enabled = False
            TxtAN.Enabled = False
            DDLBank.Enabled = False

            TxtVendor.Text = ""
            TxtNama.Text = ""
            TxtAlamat.Text = ""
            TxtTelepon.Text = ""
            TxtNPWP.Text = ""

            TxtNoRek.Text = ""
            TxtAN.Text = ""
            DDLBank.Value = "0"
        Else
            Call BindKO()
            Call GetSaldo()
        End If

    End Sub

    Protected Sub DDLAlokasi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLAlokasi.SelectedIndexChanged
        Call BindForm()
        DDLForm_SelectedIndexChanged(DDLForm, New EventArgs())
    End Sub

    Private Sub DisableControls(control As System.Web.UI.Control)

        For Each c As System.Web.UI.Control In control.Controls

            ' Get the Enabled property by reflection.
            Dim type As Type = c.GetType
            Dim prop As Reflection.PropertyInfo = type.GetProperty("Enabled")

            ' Set it to False to disable the control.
            If Not prop Is Nothing Then
                prop.SetValue(c, False, Nothing)
            End If

            ' Recurse into child controls.
            If c.Controls.Count > 0 Then
                Me.DisableControls(c)
            End If
        Next

        BtnSave.Visible = False
        BtnCancel.Text = "OK"
        BtnCancel.Enabled = True
        TabPage.Enabled = True

    End Sub

    Private Sub GetSaldo()
        TxtNoPJ.Text = ""
        TxtSaldo.Text = "0"

        If DDLForm.Value = "" Or DDLForm.Value = "0" Then
            Exit Sub
        End If

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                If LblAction.Text = "NEW" Then
                    .CommandText = "SELECT TOP 1 * FROM PdHdr WHERE JobNo=@P2 AND TipeForm=@P3 AND KSO=1 AND RejectBy IS NULL ORDER BY NoPD DESC"
                Else
                    .CommandText = "SELECT TOP 1 * FROM PdHdr WHERE NoPD<@P1 AND JobNo=@P2 AND TipeForm=@P3 AND KSO=1 AND RejectBy IS NULL ORDER BY NoPD DESC"
                End If
                .Parameters.AddWithValue("@P1", LblNoPD.Text)
                .Parameters.AddWithValue("@P2", Trim(LblJobNo.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P3", Trim(DDLForm.Value))
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    If RsFind("OverrideSaldo") = "1" Then
                        TxtEditSaldo.Text = RsFind("RemarkOverrideSaldo").ToString
                        TxtEditSaldo.Visible = True
                    End If
                    TxtNoPJ.Text = RsFind("NoPJ").ToString
                    TxtSaldo.Text = Format(If(IsDBNull(RsFind("Saldo")) = True, 0, RsFind("Saldo")), "N0")
                End If
            End Using
        End Using

        Call GetTotal()

    End Sub

    Private Sub DDLKo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLKo.SelectedIndexChanged
        TxtNama.Enabled = False
        TxtAlamat.Enabled = False
        TxtTelepon.Enabled = False
        TxtNPWP.Enabled = False

        TxtNoRek.Enabled = False
        TxtAN.Enabled = False
        DDLBank.Enabled = False

        TxtVendor.Text = ""
        TxtNama.Text = ""
        TxtAlamat.Text = ""
        TxtTelepon.Text = ""
        TxtNPWP.Text = ""

        TxtNoRek.Text = ""
        TxtAN.Text = ""
        DDLBank.Value = "0"

        If DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT" Then
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM Job WHERE JobNo=@P1"
                    .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        TxtNama.Text = RsFind("Nama").ToString
                        TxtAlamat.Text = RsFind("Alamat").ToString
                        TxtTelepon.Text = RsFind("Telepon").ToString
                        TxtNPWP.Text = RsFind("NPWP").ToString
                        TxtNoRek.Text = RsFind("NoRek").ToString
                        TxtAN.Text = RsFind("AtasNama").ToString
                        DDLBank.Value = RsFind("Bank").ToString
                    End If
                End Using
            End Using

            TxtNama.Enabled = True
            TxtAlamat.Enabled = True
            TxtTelepon.Enabled = True
            TxtNPWP.Enabled = True

            TxtNoRek.Enabled = True
            TxtAN.Enabled = True
            DDLBank.Enabled = True
        ElseIf DDLKo.Value <> "0" Then
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT VendorId FROM KoHdr WHERE NoKO=@P1"
                    .Parameters.AddWithValue("@P1", DDLKo.Value)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        TxtVendor.Text = RsFind("VendorId")
                        Call BindVendor()
                    End If
                End Using
            End Using

        End If
    End Sub

    Private Function AssignTipe(ByVal KdRAP As String) As String
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT Tipe FROM RAP WHERE JobNo=@P1 AND KdRAP=@P2"
                .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P2", KdRAP)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    Return RsFind("Tipe")
                End If
            End Using
        End Using

        Return ""

    End Function

    Private Sub GetSubTotal()
        Dim ttl As Decimal

        TmpDt = Session("TmpDt")

        For Each row As DataRow In TmpDt.Rows
            ttl += row("Vol") * row("HrgSatuan")
        Next row

        TxtSubTotal.Text = Format(ttl, "N0")

        Call GetTotal()
    End Sub

    Private Sub GetTotal()
        TxtTotal.Text = Format(CDec(TxtSubTotal.Text) - CDec(TxtSaldo.Text), "N0")
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As System.EventArgs) Handles BtnAdd.Click
        TxtAction.Text = "NEW"
        TxtNo.Text = ""
        Call BindRAP()
        DDLRap.Value = "0"
        TxtUraian.Text = ""
        TxtVol.Text = "0"
        TxtUom.Text = ""
        TxtHrgSatuan.Text = "0"
        PopEntry.ShowOnPageLoad = True
    End Sub

    'Private Sub EditLink_Click(sender As Object, e As System.EventArgs) Handles EditLink.Click
    '    TxtSaldo.Enabled = True
    '    TxtEditSaldo.Visible = True
    '    TxtSaldo.Focus()
    '    EditLink.Visible = False
    'End Sub

    'Private Sub TxtSaldo_ValueChanged(sender As Object, e As System.EventArgs) Handles TxtSaldo.ValueChanged
    '    Call GetTotal()
    'End Sub

End Class