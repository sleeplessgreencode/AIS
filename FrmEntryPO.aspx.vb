Public Class FrmEntryPO
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim TmpDt As New DataTable()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "KO") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        LblAction.Text = Session("KO").ToString.Split("|")(0)
        LblJobNo.Text = Session("KO").ToString.Split("|")(1)
        LblNoKO.Text = Session("KO").ToString.Split("|")(2)
        LblTglKO.Text = Session("KO").ToString.Split("|")(3)
        LblMix.Text = Session("KO").ToString.Split("|")(4)

        If IsPostBack = False Then
            Call BindDDLSPR()
            Call BindVendor()
            'Call BindKategori()
            'Call BindSubKategori()
            Call BindGrid()
            Call BindAlokasi()
        End If

    End Sub

    Private Sub BindVendor()
        DDLVendor.Items.Clear()
        DDLVendor.Items.Add("Pilih salah satu", "0")

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT VendorId,VendorNm FROM Vendor"
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        While RsFind.Read
            DDLVendor.Items.Add(RsFind("VendorNm") & " - " & RsFind("VendorId"), RsFind("VendorId"))
        End While
        RsFind.Close()
        CmdFind.Dispose()
        DDLVendor.Value = "0"

        DDLVendor_SelectedIndexChanged(DDLVendor, New EventArgs())

    End Sub

    Private Sub BindAlokasi()
        DDLAlokasi.Items.Clear()
        DDLAlokasi.Items.Add("Pilih salah satu", "0")

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT Alokasi,Keterangan FROM Alokasi WHERE Alokasi='B' OR Alokasi='C' OR Alokasi='L'"
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                DDLAlokasi.Items.Add(RsFind("Alokasi") & " - " & RsFind("Keterangan"), RsFind("Alokasi"))
            End While
        End Using

        DDLAlokasi.Value = "B"

    End Sub

    'Private Sub BindKategori()
    '    DDLKategori.Items.Clear()
    '    DDLKategori.Items.Add("Pilih salah satu", "0")
    '    Dim CmdIsi As New Data.SqlClient.SqlCommand
    '    With CmdIsi
    '        .Connection = Conn
    '        .CommandType = CommandType.Text
    '        .CommandText = "SELECT KategoriId FROM Kategori WHERE KategoriId!=@P1"
    '        .Parameters.AddWithValue("@P1", "SUBKON")
    '    End With
    '    Dim RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
    '    While RsIsi.Read
    '        DDLKategori.Items.Add(RsIsi(0), RsIsi(0))
    '    End While
    '    RsIsi.Close()
    '    CmdIsi.Dispose()

    '    DDLKategori.Value = "0"
    'End Sub

    'Private Sub BindSubKategori()
    '    DDLSubKategori.Items.Clear()
    '    DDLSubKategori.Items.Add("Pilih salah satu", "0")
    '    Dim CmdIsi As New Data.SqlClient.SqlCommand
    '    With CmdIsi
    '        .Connection = Conn
    '        .CommandType = CommandType.Text
    '        .CommandText = "SELECT SubKategoriId FROM SubKategori WHERE KategoriId=@P1"
    '        .Parameters.AddWithValue("@P1", DDLKategori.Value)
    '    End With
    '    Dim RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
    '    While RsIsi.Read
    '        DDLSubKategori.Items.Add(RsIsi(0), RsIsi(0))
    '    End While
    '    RsIsi.Close()
    '    CmdIsi.Dispose()

    '    DDLSubKategori.Value = "0"
    'End Sub

    Private Sub BindRAP()
        DDLRap.Items.Clear()
        DDLRap.Items.Add("Pilih salah satu", "0")

        Dim CmdBind As New Data.SqlClient.SqlCommand
        With CmdBind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT KdRAP,Uraian,Tipe FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2 ORDER BY NoUrut ASC"
            .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
            .Parameters.AddWithValue("@P2", If(DDLAlokasi.Value = String.Empty, String.Empty, DDLAlokasi.Value))
        End With
        Dim RsBind As Data.SqlClient.SqlDataReader = CmdBind.ExecuteReader
        While RsBind.Read
            'Diminta oleh pak Leo saat meeting tgl 12 Des 2017
            'If RsBind("Tipe") = "Header" Then
            '    DDLRap.Items.Add(RsBind("KdRAP") & " - " & RsBind("Uraian"), RsBind("Tipe"))
            'Else
            DDLRap.Items.Add(RsBind("KdRAP") & " - " & RsBind("Uraian"), RsBind("KdRAP"))
            'End If
        End While
        RsBind.Close()
        CmdBind.Dispose()

    End Sub
    Private Sub BindDDLSPR()
        DDLSPR.Items.Clear()
        DDLSPR.Items.Add("Pilih salah satu", "0")

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT NoSPR, UtkPekerjaan FROM PRHdr WHERE JobNo=@P1 AND ApprovedBy IS NOT NULL"
                .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    DDLSPR.Items.Add(RsFind("NoSPR") & " - " & RsFind("UtkPekerjaan"), RsFind("NoSPR"))
                End While
            End Using
        End Using
    End Sub
    Private Sub BindGrid()
        Dim Ttl As Decimal = 0
        TglKO.Date = Format(Now, "dd-MMM-yyyy")

        TxtJob.Text = LblJobNo.Text

        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("NoUrut", GetType(Integer)), _
                                                 New DataColumn("Alokasi", GetType(String)), _
                                                 New DataColumn("KdRAP", GetType(String)), _
                                                 New DataColumn("Uraian", GetType(String)), _
                                                 New DataColumn("Vol", GetType(Decimal)), _
                                                 New DataColumn("Uom", GetType(String)), _
                                                 New DataColumn("HrgSatuan", GetType(Decimal))})

        GridData.Columns(5).FooterText = "Sub Total"
        GridData.Columns(5).FooterStyle.Font.Bold = True
        GridData.Columns(5).FooterStyle.HorizontalAlign = HorizontalAlign.Center
        GridData.Columns(6).FooterStyle.HorizontalAlign = HorizontalAlign.Right

        If LblAction.Text <> "NEW" Then
            If DDLSPR.Value <> "0" Then
                BtnAdd.Visible = False
            Else
                BtnAdd.Visible = True
            End If

            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                If LblAction.Text = "SEE_KOADDENDUMH" Then
                    .CommandText = "SELECT * FROM KoHdrH WHERE NoKO=@P1 AND TglKO=@P2"
                Else
                    .CommandText = "SELECT * FROM KoHdr WHERE NoKO=@P1"
                End If
                .Parameters.AddWithValue("@P1", LblNoKO.Text)
                .Parameters.AddWithValue("@P2", LblTglKO.Text)
                '.CommandText = "SELECT * FROM KoHdr WHERE NoKO=@P1"
                '.Parameters.AddWithValue("@P1", LblNoKO.Text)
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                TxtNoKO.Text = RsFind("NoKO")
                TglKO.Date = RsFind("TglKO")
                LblTglKO1.Text = RsFind("TglKO")
                DDLVendor.Value = RsFind("VendorId")
                DDLVendor_SelectedIndexChanged(DDLVendor, New EventArgs())
                DDLSPR.Value = RsFind("NoSPR")
                'DDLKategori.Value = RsFind("KategoriId")
                'Call BindSubKategori()
                'DDLSubKategori.Value = RsFind("SubKategoriId")
                CheckBoxMA.Checked = If(RsFind("MaterialApproval").ToString = "1", True, False)
                CheckBoxRAP.Checked = If(RsFind("RAP").ToString = "1", True, False)
                AssignCB(RsFind("SyaratPembayaran"))
                TxtAlamatKirim.Text = RsFind("AlamatKirim").ToString
                TxtNamaKirim.Text = RsFind("NamaKirim").ToString
                TxtTeleponKirim.Text = RsFind("TeleponKirim").ToString
                CheckBoxK3.Checked = If(RsFind("K3").ToString = "1", True, False)
                TxtSyaratTeknis.Text = RsFind("SyaratTeknis").ToString
                TxtJadwalPengiriman.Text = RsFind("JadwalPengiriman").ToString
                TxtJadwalPembayaran.Text = RsFind("JadwalPembayaran").ToString
                TxtSanksi.Text = RsFind("Sanksi").ToString
                TxtKeterangan.Text = RsFind("Keterangan").ToString
                TxtDiscPersen.Text = Format(RsFind("DiscPercentage"), "N2")
                TxtDiscNominal.Text = Format(RsFind("DiscAmount"), "N0")
                TxtPPN.Text = Format(RsFind("PPN"), "N0")
                If RsFind("OverridePPN") = "1" Then
                    CbOverride.Checked = True
                    TxtPPN.Enabled = True
                End If

                Dim CmdGrid As New Data.SqlClient.SqlCommand
                With CmdGrid
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM KoDtl WHERE NoKO=@P1 ORDER BY NoUrut"
                    .Parameters.AddWithValue("@P1", LblNoKO.Text)
                End With
                Dim RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                While RsGrid.Read
                    TmpDt.Rows.Add(RsGrid("NoUrut"), RsGrid("Alokasi"), RsGrid("KdRAP"), RsGrid("Uraian"), RsGrid("Vol"), RsGrid("Uom"), RsGrid("HrgSatuan"))
                    Ttl += RsGrid("HrgSatuan") * RsGrid("Vol")
                End While
                RsGrid.Close()
                CmdGrid.Dispose()

                'DDLVendor.Enabled = False
                'DDLKategori.Enabled = False
                'DDLSubKategori.Enabled = False
            End If
            RsFind.Close()
            CmdFind.Dispose()

        End If

        GridData.DataSource = TmpDt
        Session("TmpDt") = TmpDt

        GridData.Columns(6).FooterText = Format(Ttl, "N0")
        TxtDiscPersen.Text = If(TxtDiscPersen.Text = "", "0", TxtDiscPersen.Text)
        TxtDiscNominal.Text = Format(CDec(GridData.Columns(6).FooterText) * (CDec(TxtDiscPersen.Text) / 100), "N0")
        TxtTotal.Text = Format((CDec(GridData.Columns(6).FooterText) - CDec(TxtDiscNominal.Text)) + CDec(TxtPPN.Text), "N0")

        GridData.DataBind()

        If Left(LblAction.Text, 3) = "SEE" Then
            DisableControls(Form)
        End If

    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Session("Job") = Trim(LblJobNo.Text.Split("-")(0))

        Session.Remove("TmpDt")
        Session.Remove("KO")
        TmpDt.Dispose()

        If LblAction.Text = "SEE_APPROVALKO" Then
            Response.Redirect("FrmApprovalKO.aspx")
        ElseIf LblAction.Text = "SEE_KOADDENDUM" Then
            Session("Job") = Trim(LblJobNo.Text.Split("-")(0)) & "|" & TxtNoKO.Text
            Response.Redirect("FrmKOAddendum.aspx")
        ElseIf LblAction.Text = "SEE_KOADDENDUMH" Then
            Session("Job") = Trim(LblJobNo.Text.Split("-")(0)) & "|" & TxtNoKO.Text
            Response.Redirect("FrmKOAddendum.aspx")
        ElseIf LblAction.Text = "SEE_CANCELKO" Then
            Session("Job") = Trim(LblJobNo.Text.Split("-")(0))
            Response.Redirect("FrmCancelPO.aspx")
        Else
            Response.Redirect("FrmKO.aspx")
        End If

        Exit Sub
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As System.EventArgs) Handles BtnSave.Click
        If DDLVendor.Value = "0" Then
            LblErr.Text = "Belum pilih ID."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        ' Jika NoSPR diisi, cek setiap item di GridData, ditambah dengan item & nospr yang sama yang 
        ' ada di tabel KoHdr dan cek ke KoDtl jumlahnya. Total item yang ada ditambah dengan Total di KoDtl
        ' tidak boleh melebihi item di SPR atau pop up error
        If DDLSPR.Value <> "0" Then
            For Each row As GridViewRow In GridData.Rows
                Dim UraianSPR As String = TryCast(row.FindControl("LblUraian"), Label).Text
                Dim VolSPRinKO As Decimal = Convert.ToDecimal(row.Cells(3).Text)
                Dim CmdFindVol As New Data.SqlClient.SqlCommand
                With CmdFindVol
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT SUM(Vol) Volume FROM KoHdr a LEFT JOIN KoDtl b " & _
                                   "ON a.NoKO = b.NoKO WHERE a.NoSPR=@P1 AND b.Uraian=@P2 AND a.NoKO NOT LIKE @P3"
                    .Parameters.AddWithValue("@P1", DDLSPR.Value)
                    .Parameters.AddWithValue("@P2", UraianSPR)
                    .Parameters.AddWithValue("@P3", "%" & TxtNoKO.Text & "%")
                End With
                Using RsFindVol As Data.SqlClient.SqlDataReader = CmdFindVol.ExecuteReader
                    If RsFindVol.Read Then
                        VolSPRinKO = VolSPRinKO + If(RsFindVol("Volume") Is DBNull.Value, 0, RsFindVol("Volume"))
                    End If
                End Using
                LblErr.Text = VolSPRinKO
                ErrMsg.ShowOnPageLoad = True
                Dim VolSPR As Decimal
                Dim CmdFindVolSPR As New Data.SqlClient.SqlCommand
                With CmdFindVolSPR
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT Vol FROM PRDtl WHERE NoSPR=@P1 AND Uraian=@P2"
                    .Parameters.AddWithValue("@P1", DDLSPR.Value)
                    .Parameters.AddWithValue("@P2", UraianSPR)
                End With
                Using RsFindVolSPR As Data.SqlClient.SqlDataReader = CmdFindVolSPR.ExecuteReader
                    If RsFindVolSPR.Read Then
                        VolSPR = If(RsFindVolSPR("Vol") Is DBNull.Value, 0, RsFindVolSPR("Vol"))
                    End If
                End Using
                If VolSPRinKO > VolSPR Then
                    LblErr.Text = "Jumlah volume untuk item " & UraianSPR & " sudah melebihi volume yang terdapat dalam SPR."
                    ErrMsg.ShowOnPageLoad = True
                    Exit Sub
                End If
            Next
        End If
        'If DDLKategori.Value = "0" Then
        '    LblErr.Text = "Kategori belum dipilih."
        '    ErrMsg.ShowOnPageLoad = True
        '    Exit Sub
        'End If
        'If DDLSubKategori.Value = "0" Then
        '    LblErr.Text = "Sub kategori belum dipilih."
        '    ErrMsg.ShowOnPageLoad = True
        '    Exit Sub
        'End If
        If TxtAlamatKirim.Text = "" Or TxtNamaKirim.Text = "" Or TxtTeleponKirim.Text = "" Then
            LblErr.Text = "Harap lengkapi lokasi pengiriman."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If CheckBoxRAP.Checked = False Then
            LblErr.Text = "RAP belum dicentang."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If SaveCB() = "" Then
            LblErr.Text = "Belum pilih syarat pembayaran."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        TmpDt = Session("TmpDt")
        If TmpDt.Rows.Count = 0 Then
            LblErr.Text = "Belum input order list."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        'If LblTglKO1.Text <> TglKO.Date Then
        '    Using CmdFind As New Data.SqlClient.SqlCommand
        '        With CmdFind
        '            .Connection = Conn
        '            .CommandType = CommandType.Text
        '            .CommandText = "SELECT TOP 1 * FROM KoHdr WHERE JobNo=@P1 AND NoKO<>@P2 AND TglKO>@P3 AND KategoriId='PO'"
        '            .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
        '            .Parameters.AddWithValue("@P2", TxtNoKO.Text)
        '            .Parameters.AddWithValue("@P3", TglKO.Date)
        '        End With
        '        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        '            If RsFind.Read Then
        '                LblErr.Text = "Sudah ada tanggal KO diatas " & Format(TglKO.Date, "dd-MMM-yyyy") & ". <br /> Tidak bisa backdate."
        '                ErrMsg.ShowOnPageLoad = True
        '                Exit Sub
        '            End If
        '        End Using
        '    End Using
        'End If

        If LblAction.Text = "NEW" Then
            TxtNoKO.Text = AssignNoKO()
            If TxtNoKO.Text = "" Then
                LblErr.Text = "Error while generate No KO."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If

            Dim CmdInsert1 As New Data.SqlClient.SqlCommand
            With CmdInsert1
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO KoHdr (NoKO,TglKO,JobNo,VendorId,SubTotal,DiscPercentage,DiscAmount,PPN,AlamatKirim,NamaKirim,TeleponKirim," & _
                               "MaterialApproval,RAP,K3,SyaratTeknis,JadwalPengiriman,JadwalPembayaran,SyaratPembayaran,Sanksi,Keterangan,UserEntry," & _
                               "TimeEntry,KategoriId,QRCode,OverridePPN, NoSPR) VALUES " & _
                               "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22,@P23,@P24,@P25, @P26)"
                .Parameters.AddWithValue("@P1", TxtNoKO.Text)
                .Parameters.AddWithValue("@P2", TglKO.Date)
                .Parameters.AddWithValue("@P3", Trim(LblJobNo.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P4", DDLVendor.Value)
                .Parameters.AddWithValue("@P5", GridData.Columns(6).FooterText)
                .Parameters.AddWithValue("@P6", TxtDiscPersen.Text)
                .Parameters.AddWithValue("@P7", TxtDiscNominal.Text)
                .Parameters.AddWithValue("@P8", TxtPPN.Text)
                .Parameters.AddWithValue("@P9", TxtAlamatKirim.Text)
                .Parameters.AddWithValue("@P10", TxtNamaKirim.Text)
                .Parameters.AddWithValue("@P11", TxtTeleponKirim.Text)
                .Parameters.AddWithValue("@P12", If(CheckBoxMA.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P13", If(CheckBoxRAP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P14", If(CheckBoxK3.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P15", Trim(TxtSyaratTeknis.Text))
                .Parameters.AddWithValue("@P16", Trim(TxtJadwalPengiriman.Text))
                .Parameters.AddWithValue("@P17", Trim(TxtJadwalPembayaran.Text))
                .Parameters.AddWithValue("@P18", SaveCB)
                .Parameters.AddWithValue("@P19", Trim(TxtSanksi.Text))
                .Parameters.AddWithValue("@P20", Trim(TxtKeterangan.Text))
                .Parameters.AddWithValue("@P21", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P22", Now)
                .Parameters.AddWithValue("@P23", "PO")
                .Parameters.AddWithValue("@P24", GetQRCode)
                .Parameters.AddWithValue("@P25", If(CbOverride.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P26", DDLSPR.Value)
                .ExecuteNonQuery()
                .Dispose()
            End With

        Else
            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE KoHdr SET SubTotal=@P1,DiscPercentage=@P2,DiscAmount=@P3,PPN=@P4,AlamatKirim=@P5,NamaKirim=@P6," & _
                               "TeleponKirim=@P7,MaterialApproval=@P8,RAP=@P9,K3=@P10,SyaratTeknis=@P11,JadwalPengiriman=@P12," & _
                               "JadwalPembayaran=@P13,SyaratPembayaran=@P14,Sanksi=@P15,Keterangan=@P16,UserEntry=@P17,TimeEntry=@P18," & _
                               "TglKO=@P19,VendorId=@P20,OverridePPN=@P21,NoSPR=@P23 WHERE NoKO=@P22"
                .Parameters.AddWithValue("@P1", GridData.Columns(6).FooterText)
                .Parameters.AddWithValue("@P2", TxtDiscPersen.Text)
                .Parameters.AddWithValue("@P3", TxtDiscNominal.Text)
                .Parameters.AddWithValue("@P4", TxtPPN.Text)
                .Parameters.AddWithValue("@P5", TxtAlamatKirim.Text)
                .Parameters.AddWithValue("@P6", TxtNamaKirim.Text)
                .Parameters.AddWithValue("@P7", TxtTeleponKirim.Text)
                .Parameters.AddWithValue("@P8", If(CheckBoxMA.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P9", If(CheckBoxRAP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P10", If(CheckBoxK3.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P11", TxtSyaratTeknis.Text)
                .Parameters.AddWithValue("@P12", TxtJadwalPengiriman.Text)
                .Parameters.AddWithValue("@P13", TxtJadwalPembayaran.Text)
                .Parameters.AddWithValue("@P14", SaveCB)
                .Parameters.AddWithValue("@P15", TxtSanksi.Text)
                .Parameters.AddWithValue("@P16", TxtKeterangan.Text)
                .Parameters.AddWithValue("@P17", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P18", Now)
                .Parameters.AddWithValue("@P19", TglKO.Date)
                .Parameters.AddWithValue("@P20", DDLVendor.Value)
                .Parameters.AddWithValue("@P21", If(CbOverride.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P22", TxtNoKO.Text)
                .Parameters.AddWithValue("@P23", DDLSPR.Value)
                .ExecuteNonQuery()
                .Dispose()
            End With

            Dim CmdDel As New Data.SqlClient.SqlCommand
            With CmdDel
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "DELETE KoDtl WHERE NoKO=@P1"
                .Parameters.AddWithValue("@P1", TxtNoKO.Text)
                .ExecuteNonQuery()
                .Dispose()
            End With

        End If

        Dim Counter As Integer = 0
        Dim CmdInsert2 As New Data.SqlClient.SqlCommand
        For Each row As DataRow In TmpDt.Rows
            Counter += 1
            CmdInsert2.Parameters.Clear()
            With CmdInsert2
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO KoDtl (NoKO,NoUrut,KdRAP,Uraian,Vol,Uom,HrgSatuan,UserEntry,TimeEntry,Alokasi) VALUES " & _
                                    "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10)"
                .Parameters.AddWithValue("@P1", TxtNoKO.Text)
                .Parameters.AddWithValue("@P2", Counter)
                .Parameters.AddWithValue("@P3", row("KdRAP"))
                .Parameters.AddWithValue("@P4", row("Uraian"))
                .Parameters.AddWithValue("@P5", row("Vol"))
                .Parameters.AddWithValue("@P6", row("Uom"))
                .Parameters.AddWithValue("@P7", row("HrgSatuan"))
                .Parameters.AddWithValue("@P8", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P9", Now)
                .Parameters.AddWithValue("@P10", row("Alokasi"))
                .ExecuteNonQuery()
                .Dispose()
            End With
        Next row

        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub

    Private Sub GetSubTotal()
        Dim ttl As Decimal

        TmpDt = Session("TmpDt")

        For Each row As DataRow In TmpDt.Rows
            ttl += row("Vol") * row("HrgSatuan")
        Next row

        GridData.Columns(6).FooterText = Format(ttl, "N0")

        TxtDiscPersen_NumberChanged(TxtDiscPersen, New EventArgs())

        Call GetTotal()

    End Sub

    Private Sub GetTotal()
        TmpDt = Session("TmpDt")
        If TmpDt.Rows.Count = 0 Then Exit Sub

        If CbOverride.Checked = False And LblPPN.Text = "PPN" Then
            TxtPPN.Text = Format(((CDec(GridData.Columns(6).FooterText) - CDec(TxtDiscNominal.Text)) * 0.1), "N0")
            Call BindNPWP()
        End If
        TxtTotal.Text = Format((CDec(GridData.Columns(6).FooterText) - CDec(TxtDiscNominal.Text)) + CDec(TxtPPN.Text), "N0")

    End Sub

    Private Sub GridData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            TxtAction.Text = "UPD"
            TxtNo.Text = SelectRecord.Cells(0).Text
            DDLAlokasi.Value = If(SelectRecord.Cells(7).Text = "&nbsp;", String.Empty, SelectRecord.Cells(7).Text)
            Call BindRAP()
            DDLRap.Value = SelectRecord.Cells(1).Text
            TxtUraian.Text = TryCast(SelectRecord.FindControl("LblUraian"), Label).Text.Replace("<br />", Environment.NewLine)
            TxtVol.Text = SelectRecord.Cells(3).Text
            TxtUom.Text = SelectRecord.Cells(4).Text
            TxtHrgSatuan.Text = SelectRecord.Cells(5).Text
            If DDLSPR.Value <> "0" Then
                DDLAlokasi.Enabled = False
                DDLRap.ClientEnabled = False
                TxtUraian.Enabled = False
                TxtUom.Enabled = False
            End If
            'If DDLSPR.Value <> "" Then
            '    DDLAlokasi.Enabled = False
            '    DDLRap.Enabled = False
            '    TxtUraian.Enabled = False
            '    TxtUom.Enabled = False
            'End If
            PopEntry.ShowOnPageLoad = True

        ElseIf e.CommandName = "BtnDelete" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            TmpDt = Session("TmpDt")
            'dt.DefaultView.Sort = "regNumber DESC" 'Sort lbh dahulu baru delete, jika tidak ada akan salah delete krn sort.
            TmpDt.Rows(e.CommandArgument).Delete()
            TmpDt.AcceptChanges()

            GridData.DataSource = TmpDt
            Call GetSubTotal()
            GridData.DataBind()
            Session("TmpDt") = TmpDt
        End If
    End Sub

    Private Function AssignNoKO() As String
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            If LblMix.Text = "MIX" Then
                .CommandText = "SELECT CounterKOMIX FROM Counter WHERE JobNo=@P1 AND Alokasi=@P2"
            Else
                .CommandText = "SELECT CounterKO FROM Counter WHERE JobNo=@P1 AND Alokasi=@P2"
            End If
            .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
            .Parameters.AddWithValue("@P2", "B")
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                If LblMix.Text = "MIX" Then
                    .CommandText = "UPDATE Counter SET CounterKOMIX=@P1,UserEntry=@P2,TimeEntry=@P3 WHERE JobNo=@P4 AND Alokasi=@P5"
                    .Parameters.AddWithValue("@P1", If(String.IsNullOrEmpty(RsFind("CounterKOMIX").ToString) = True, 1, RsFind("CounterKOMIX") + 1))
                Else
                    .CommandText = "UPDATE Counter SET CounterKO=@P1,UserEntry=@P2,TimeEntry=@P3 WHERE JobNo=@P4 AND Alokasi=@P5"
                    .Parameters.AddWithValue("@P1", If(String.IsNullOrEmpty(RsFind("CounterKO").ToString) = True, 1, RsFind("CounterKO") + 1))
                End If
                .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P3", Now)
                .Parameters.AddWithValue("@P4", Trim(LblJobNo.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P5", "B")
                .ExecuteNonQuery()
                .Dispose()
            End With
            If LblMix.Text = "MIX" Then
                AssignNoKO = "KO" & Trim(LblJobNo.Text.Split("-")(0)) & "MIX" & Format(If(String.IsNullOrEmpty(RsFind("CounterKOMIX").ToString) = True, 1, RsFind("CounterKOMIX") + 1), "000")
            Else
                AssignNoKO = "KO" & Trim(LblJobNo.Text.Split("-")(0)) & Format(If(String.IsNullOrEmpty(RsFind("CounterKO").ToString) = True, 1, RsFind("CounterKO") + 1), "000")
            End If

        Else
            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                If LblMix.Text = "MIX" Then
                    .CommandText = "INSERT INTO Counter(JobNo,Alokasi,CounterKOMIX,UserEntry,TimeEntry) VALUES " & _
                               "(@P1,@P2,@P3,@P4,@P5)"
                Else
                    .CommandText = "INSERT INTO Counter(JobNo,Alokasi,CounterKO,UserEntry,TimeEntry) VALUES " & _
                               "(@P1,@P2,@P3,@P4,@P5)"
                End If
                .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P2", "B")
                .Parameters.AddWithValue("@P3", 1)
                .Parameters.AddWithValue("@P4", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P5", Now)
                .ExecuteNonQuery()
                .Dispose()
            End With
            If LblMix.Text = "MIX" Then
                AssignNoKO = "KO" & Trim(LblJobNo.Text.Split("-")(0)) & "MIX001"
            Else
                AssignNoKO = "KO" & Trim(LblJobNo.Text.Split("-")(0)) & "001"
            End If

        End If
        RsFind.Close()
        CmdFind.Dispose()

    End Function

    Protected Sub BtnSave1_Click(sender As Object, e As EventArgs) Handles BtnSave1.Click
        If DDLAlokasi.Value = "0" Then
            LblErr.Text = "Belum pilih alokasi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If DDLRap.Value = "0" Then
            LblErr.Text = "Belum ada RAP yang di-pilih"
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        Else
            'Diminta oleh pak Leo saat meeting tgl 12 Des 2017
            'If DDLRap.Value = "Header" Then
            '    LblErr.Text = "Tidak bisa pilih RAP dengan Tipe Header."
            '    ErrMsg.ShowOnPageLoad = True
            '    Call BindRAP()
            '    Exit Sub
            'End If
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
        'If TxtHrgSatuan.Text = "" Or TxtHrgSatuan.Text = "0" Then
        '    LblErr.Text = "Harga satuan belum di-isi."
        '    ErrMsg.ShowOnPageLoad = True
        '    Exit Sub
        'End If

        TmpDt = Session("TmpDt")

        If TxtAction.Text = "NEW" Then
            Dim Counter As Integer
            Dim result As DataRow = TmpDt.Select("NoUrut > 0", "NoUrut DESC").FirstOrDefault
            If result Is Nothing Then
                Counter = 1
            Else
                Counter = result("NoUrut") + 1
            End If
            TmpDt.Rows.Add(Counter, DDLAlokasi.Value, DDLRap.Value, TxtUraian.Text, TxtVol.Text, UCase(TxtUom.Text), TxtHrgSatuan.Text)
        Else
            Dim result As DataRow = TmpDt.Select("NoUrut='" & TxtNo.Text & "'").FirstOrDefault
            If result IsNot Nothing Then
                result("Alokasi") = DDLAlokasi.Value
                result("KdRAP") = DDLRap.Value
                result("Uraian") = Trim(TxtUraian.Text)
                result("Uom") = UCase(TxtUom.Text)
                result("Vol") = TxtVol.Text
                result("HrgSatuan") = TxtHrgSatuan.Text
            End If
        End If

        GridData.DataSource = TmpDt
        Call GetSubTotal()

        GridData.DataBind()

        Session("TmpDt") = TmpDt
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

    End Sub
    Protected Sub DDLSPR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLSPR.SelectedIndexChanged
        TmpDt = Session("TmpDt")
        TmpDt.Rows.Clear()

        If DDLSPR.Value <> "0" Then
            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM PRDtl WHERE NoSPR=@P1"
                .Parameters.AddWithValue("@P1", DDLSPR.Value)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    TmpDt.Rows.Add(RsFind("NoUrut"), RsFind("Alokasi"), RsFind("KdRap"), RsFind("Uraian"), RsFind("Vol"), RsFind("Uom"), 0)
                End While
            End Using
            BtnAdd.Visible = False
        Else
            BtnAdd.Visible = True
        End If

        Session("TmpDt") = TmpDt
        GridData.DataSource = TmpDt
        GridData.DataBind()

        If DDLSPR.Value <> "0" Then
            BtnAdd.Visible = False
        Else
            BtnAdd.Visible = True
        End If
    End Sub
    Protected Sub DDLVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLVendor.SelectedIndexChanged
        TxtNama.Text = ""
        TxtAlamat.Text = ""
        TxtTelepon.Text = ""
        TxtNPWP.Text = ""
        TxtUP.Text = ""
        TxtPPN.Text = "0"
        TxtPPN.Enabled = False
        LblPPN.Text = ""
        CbOverride.Checked = False
        CbOverride.Enabled = False
        Call BindNPWP()

        Dim CmdBind As New Data.SqlClient.SqlCommand
        With CmdBind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Vendor WHERE VendorId=@P1"
            .Parameters.AddWithValue("@P1", DDLVendor.Value)
        End With
        Dim RsBind As Data.SqlClient.SqlDataReader = CmdBind.ExecuteReader
        If RsBind.Read Then
            TxtNama.Text = RsBind("VendorNm")
            TxtAlamat.Text = RsBind("Alamat").ToString
            TxtTelepon.Text = RsBind("Telepon1").ToString
            TxtNPWP.Text = RsBind("NPWP").ToString
            TxtUP.Text = RsBind("ContactPerson").ToString
            If RsBind("PKP") = "1" Then
                LblPPN.Text = "PPN"
                CbOverride.Enabled = True
            End If

            TxtKategori.Text = RsBind("Kategori").ToString
            TxtUsaha.Text = RsBind("BidangUsaha").ToString

        End If
        RsBind.Close()
        CmdBind.Dispose()
    End Sub

    Protected Sub TxtDiscPersen_NumberChanged(sender As Object, e As EventArgs) Handles TxtDiscPersen.NumberChanged
        TxtDiscPersen.Text = If(TxtDiscPersen.Text = "", "0", TxtDiscPersen.Text)
        TxtDiscNominal.Text = Format(CDec(GridData.Columns(6).FooterText) * (CDec(TxtDiscPersen.Text) / 100), "N0")
        Call GetTotal()
    End Sub

    'Protected Sub DDLKategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLKategori.SelectedIndexChanged
    '    Call BindSubKategori()
    'End Sub

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

    Private Function GetQRCode() As String
        Dim Ulang As Boolean = True

        Dim RandomValue As String = String.Empty

        While Ulang = True
            RandomValue = RandomX()
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT QRCode FROM KOHdr WHERE QRCode=@P1"
                    .Parameters.AddWithValue("@P1", RandomValue)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If Not RsFind.Read Then
                        Return RandomValue
                        Ulang = False
                    End If

                End Using
            End Using

        End While

        Return RandomValue

    End Function

    Private Sub TxtPPN_ValueChanged(sender As Object, e As System.EventArgs) Handles TxtPPN.ValueChanged
        Call BindNPWP()
        Call GetTotal()
    End Sub

    Private Sub DDLAlokasi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLAlokasi.SelectedIndexChanged
        Call BindRAP()
    End Sub

    Private Sub GridData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridData.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then e.Row.Cells.RemoveAt(7)
    End Sub

    Private Sub CbOverride_CheckedChanged(sender As Object, e As System.EventArgs) Handles CbOverride.CheckedChanged
        If CbOverride.Checked = True Then
            TxtPPN.Enabled = True
            TxtPPN.Text = "0"
            Call BindNPWP()
        Else
            TxtPPN.Enabled = False
        End If

        TmpDt = Session("TmpDt")
        GridData.DataSource = TmpDt
        If TmpDt.Rows.Count > 0 Then
            Call GetSubTotal()            
        End If
        Call GetTotal()

        GridData.DataBind()

    End Sub

    Private Sub TxtDiscNominal_NumberChanged(sender As Object, e As System.EventArgs) Handles TxtDiscNominal.NumberChanged
        Call GetTotal()
    End Sub

    Private Sub BindNPWP()
        Dim NPWP As String = String.Empty
        TxtKeterangan.Enabled = True
        If TxtPPN.Text <> "0" Then
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT NPWPName,NPWPAddress,NPWPCompany FROM Job WHERE JobNo=@P1"
                    .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        If String.IsNullOrEmpty(RsFind("NPWPCompany").ToString) = False Then NPWP = "NPWP: " & RsFind("NPWPCompany").ToString
                        If String.IsNullOrEmpty(RsFind("NPWPName").ToString) = False Then NPWP = NPWP & " - " & RsFind("NPWPName").ToString
                        If String.IsNullOrEmpty(RsFind("NPWPAddress").ToString) = False Then NPWP = NPWP & Chr(13) & RsFind("NPWPAddress").ToString
                    End If
                End Using
            End Using
            TxtKeterangan.Enabled = False
        End If

        TxtKeterangan.Text = NPWP
    End Sub

End Class