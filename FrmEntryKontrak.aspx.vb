Public Class FrmEntryKontrak
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim TmpDipa As New DataTable() 'Untuk tampung DIPA
    Dim TmpAddendum As New DataTable() 'Untuk tampung Addendum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "Job") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        LblAction.Text = Session("Kontrak").ToString.Split("|")(0)
        LblJobNo.Text = Session("Kontrak").ToString.Split("|")(1)

        If IsPostBack = False Then
            TabPage.ActiveTabIndex = 0
            Call BindBank()
            Call BindGrid()
        End If

    End Sub

    Private Sub BindBank()
        DDLBank.Items.Clear()
        DDLBankInduk.Items.Clear()
        Using CmdIsi As New Data.SqlClient.SqlCommand
            With CmdIsi
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT Bank FROM Bank"
            End With
            Using RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
                While RsIsi.Read
                    DDLBank.Items.Add(RsIsi(0), RsIsi(0))
                    DDLBankInduk.Items.Add(RsIsi(0), RsIsi(0))
                End While
            End Using
        End Using

    End Sub

    'Private Sub EnabledObject(ByVal Vbool As Boolean)
    '    TxtNoKontrak.Enabled = Vbool
    '    TglKontrak.Enabled = Vbool
    '    PrdAwal.Enabled = Vbool
    '    PrdAkhir.Enabled = Vbool
    '    TxtHari.Enabled = Vbool
    '    TxtMinggu.Enabled = Vbool
    '    TxtBruto.Enabled = Vbool

    'End Sub

    Private Sub BindGrid()
        TmpDipa.Columns.AddRange(New DataColumn() {New DataColumn("NoUrut", GetType(Integer)), _
                                                   New DataColumn("JobNo", GetType(String)), _
                                                   New DataColumn("Tahun", GetType(String)), _
                                                   New DataColumn("Budget", GetType(Decimal))})
        TmpAddendum.Columns.AddRange(New DataColumn() {New DataColumn("JobNo", GetType(String)), _
                                                       New DataColumn("Bruto", GetType(Decimal)), _
                                                       New DataColumn("Netto", GetType(Decimal)), _
                                                       New DataColumn("NoKontrak", GetType(String)), _
                                                       New DataColumn("TglKontrak", GetType(Date)), _
                                                       New DataColumn("AddendumKe", GetType(Integer)), _
                                                       New DataColumn("PrdAwal", GetType(Date)), _
                                                       New DataColumn("PrdAkhir", GetType(Date)), _
                                                       New DataColumn("Hari", GetType(Integer)), _
                                                       New DataColumn("RemarkAddendum", GetType(String)), _
                                                       New DataColumn("New", GetType(Integer))})

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Job WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", LblJobNo.Text)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    LblJudul.Text = "Job: " & RsFind("JobNo") & " - " & RsFind("JobNm")
                    TxtJobNo.Text = RsFind("JobNo")
                    TxtNama.Text = RsFind("JobNm")
                    TxtDesc.Text = RsFind("Deskripsi")
                    TxtLokasi.Text = RsFind("Lokasi")
                    TxtNoKontrak.Text = RsFind("NoKontrak").ToString
                    If String.IsNullOrEmpty(RsFind("NoKontrak").ToString) = False Then
                        BtnAddendum.Visible = True
                        'Call EnabledObject(False)
                        TxtRemarkAddendum.Visible = True
                    End If
                    TglKontrak.Value = RsFind("TglKontrak")
                    TxtInstansi.Text = RsFind("Instansi").ToString
                    TxtAddendumKe.Text = RsFind("AddendumKe").ToString
                    DDLKso.Value = RsFind("KSO").ToString
                    'DDLKso_SelectedIndexChanged(DDLKso, New EventArgs())
                    PrdAwal.Value = If(IsDBNull(RsFind("PrdAwal")), Now, RsFind("PrdAwal"))
                    PrdAkhir.Value = If(IsDBNull(RsFind("PrdAkhir")), Now, RsFind("PrdAkhir"))
                    TxtMinggu.Text = RsFind("Minggu").ToString
                    TxtBruto.Text = Format(RsFind("Bruto"), "N0")
                    TxtNetto.Text = Format(RsFind("Netto"), "N0")
                    TxtKontraktor.Text = RsFind("CompanyId").ToString
                    TxtNama1.Text = RsFind("Nama").ToString
                    TxtAlamat.Text = RsFind("Alamat").ToString
                    TxtTelepon.Text = RsFind("Telepon").ToString
                    TxtNPWP.Text = RsFind("NPWP").ToString
                    TxtNoRek.Text = RsFind("NoRek").ToString
                    TxtAN.Text = RsFind("AtasNama").ToString
                    DDLBank.Value = If(RsFind("Bank").ToString = String.Empty, String.Empty, RsFind("Bank").ToString)
                    TxtNoRekInduk.Text = RsFind("NoRekInduk").ToString
                    TxtANInduk.Text = RsFind("AtasNamaInduk").ToString
                    DDLBankInduk.Value = If(RsFind("BankInduk").ToString = String.Empty, String.Empty, RsFind("BankInduk").ToString)
                    Call BindStatus(RsFind("StatusJob").ToString)
                    DDLKategori.Text = RsFind("Kategori").ToString
                    Image1.ImageUrl = If(String.IsNullOrEmpty(RsFind("Logo").ToString) = True, "~/Images/NoLogo.jpg", RsFind("Logo"))
                    TxtHari.Text = RsFind("Hari").ToString
                    TxtNoPHO.Text = RsFind("NoPHO").ToString
                    TglPHO.Value = RsFind("TglPHO")
                    TxtNoFHO.Text = RsFind("NoFHO").ToString
                    TglFHO.Value = RsFind("TglFHO")
                    LblPath.Text = RsFind("FHOFile").ToString
                    DDLManajerial.Value = RsFind("TipeManajerial").ToString
                    TxtMember1.Text = RsFind("Member1").ToString
                    TxtMember2.Text = RsFind("Member2").ToString
                    RdoOwn1.Checked = If(RsFind("Own").ToString = "1", True, False)
                    RdoOwn2.Checked = If(RsFind("Own").ToString = "2", True, False)
                    TxtShare1.Text = RsFind("PersenShare1").ToString
                    TxtShare2.Text = RsFind("PersenShare2").ToString
                    TxtBruto1.Text = Format(RsFind("BrutoShare1"), "N0")
                    TxtBruto2.Text = Format(RsFind("BrutoShare2"), "N0")
                    Remark1.Text = RsFind("KetShare1").ToString
                    Remark2.Text = RsFind("KetShare2").ToString

                End If
            End Using
        End Using

        Dim Counter As Integer
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM DIPA WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", TxtJobNo.Text)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    Counter += 1
                    TmpDipa.Rows.Add(Counter, RsFind("JobNo"), RsFind("Tahun"), RsFind("Budget"))
                End While
            End Using
        End Using
        GridView.DataSource = TmpDipa
        GridView.DataBind()
        Session("TmpDipa") = TmpDipa

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM JobH WHERE JobNo=@P1 ORDER BY AddendumKe"
                .Parameters.AddWithValue("@P1", TxtJobNo.Text)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    TmpAddendum.Rows.Add(RsFind("JobNo"), RsFind("Bruto"), RsFind("Netto"), RsFind("NoKontrak"), RsFind("TglKontrak"), RsFind("AddendumKe"), _
                                         RsFind("PrdAwal"), RsFind("PrdAkhir"), RsFind("Hari"), RsFind("RemarkAddendum"), 0)
                End While
            End Using
        End Using
        GridAdd.DataSource = TmpAddendum
        GridAdd.DataBind()
        Session("TmpAddendum") = TmpAddendum

        If Left(LblAction.Text, 3) = "SEE" Then
            DisableControls(Form)
        End If

    End Sub

    Private Sub BindStatus(StatusJob As String)
        DDLStatus.Items.Clear()

        If StatusJob = "Proposal" Then
            DDLStatus.Items.Add("Pelaksanaan", "Pelaksanaan")
        ElseIf StatusJob = "Pelaksanaan" Then
            DDLStatus.Items.Add("Pelaksanaan", "Pelaksanaan")
            DDLStatus.Items.Add("Pemeliharaan (Pra FHO)", "Pemeliharaan")
        ElseIf StatusJob = "Pemeliharaan" Then
            DDLStatus.Items.Add("Pemeliharaan (Pra FHO)", "Pemeliharaan")
            DDLStatus.Items.Add("Closed (Pasca FHO)", "Closed")
        ElseIf StatusJob = "Closed" Then
            DDLStatus.Items.Add("Closed (Pasca FHO)", "Closed")
        End If

        DDLStatus.SelectedIndex = 0
    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Session.Remove("Kontrak")
        Session.Remove("TmpDipa")
        Session.Remove("TmpAddendum")
        TmpDipa.Dispose()
        TmpAddendum.Dispose()

        Response.Redirect("FrmKontrak.aspx")
        Exit Sub
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If TxtKontraktor.Text = "" Then
            msgBox1.alert("Kontraktor belum diisi.")
            TabPage.ActiveTabIndex = 0
            TxtKontraktor.Focus()
            Exit Sub
        End If
        If FileUpload1.HasFile And FileUpload1.PostedFile.ContentType.ToLower <> "image/jpeg" Then
            msgBox1.alert("Hanya mendukung image dengan ext. JPG/JPEG.")
            TabPage.ActiveTabIndex = 0
            FileUpload1.Focus()
            Exit Sub
        End If
        If PrdAwal.Date > PrdAkhir.Date Then
            msgBox1.alert("Tanggal mulai kontrak > tanggal selesai kontrak.")
            TabPage.ActiveTabIndex = 1
            PrdAwal.Focus()
            Exit Sub
        End If
        If TxtBruto.Text = String.Empty Or TxtBruto.Text = "0" Then
            msgBox1.alert("Bruto kontrak belum di-isi.")
            TabPage.ActiveTabIndex = 1
            TxtBruto.Focus()
            Exit Sub
        End If
        If TxtNoRek.Text <> "" And DDLBank.Value = "0" Then
            msgBox1.alert("Bank untuk rekening admin keu lapangan belum di-pilih.")
            TabPage.ActiveTabIndex = 3
            DDLBank.Focus()
            Exit Sub
        End If
        If DDLKso.Value = "1" Then
            If DDLManajerial.Value = "JO Partial" Then
                If TxtMember1.Text = "" Or TxtMember2.Text = "" Then
                    msgBox1.alert("Nama untuk Leader atau Member belum di-isi.")
                    TabPage.ActiveTabIndex = 3
                    TxtMember1.Focus()
                    Exit Sub
                End If
                If RdoOwn1.Checked = False And RdoOwn2.Checked = False Then
                    msgBox1.alert("Belum pilih Leader/Member.")
                    TabPage.ActiveTabIndex = 3
                    Exit Sub
                End If
            End If
        End If
        'If TxtNoRekInduk.Text = "" Then
        '    msgBox1.alert("No rekening termin belum di-isi.")
        '    TabPage.ActiveTabIndex = 3
        '    TxtNoRekInduk.Focus()
        '    Exit Sub
        'End If
        If DDLStatus.Value = "Pemeliharaan" Then
            If TxtNoPHO.Text = "" Then
                msgBox1.alert("No PHO belum diisi.")
                TabPage.ActiveTabIndex = 5
                TxtNoPHO.Focus()
                Exit Sub
            End If
            If TglPHO.Text = "" Then
                msgBox1.alert("Tgl PHO belum diisi.")
                TabPage.ActiveTabIndex = 5
                TglPHO.Focus()
                Exit Sub
            End If
        ElseIf DDLStatus.Value = "Closed" Then
            If TxtNoFHO.Text = "" Then
                msgBox1.alert("No FHO belum diisi.")
                TabPage.ActiveTabIndex = 5
                TxtNoFHO.Focus()
                Exit Sub
            End If
            If TglFHO.Date < TglPHO.Date Then
                msgBox1.alert("Tgl PHO > Tgl FHO.")
                TabPage.ActiveTabIndex = 5
                TglFHO.Focus()
                Exit Sub
            End If
            If FHOUpload.HasFile = False Then
                msgBox1.alert("File PDF belum dipilih.")
                TabPage.ActiveTabIndex = 5
                FHOUpload.Focus()
                Exit Sub
            End If
            If FHOUpload.PostedFile.ContentType.ToLower <> "application/pdf" Then
                msgBox1.alert("File yang di-upload bukan PDF.")
                TabPage.ActiveTabIndex = 5
                FHOUpload.Focus()
                Exit Sub
            End If
            'If CheckJaminan() = False Then Exit Sub
        End If

        If DDLStatus.Value = "Pelaksanaan" Then
            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE Job SET " & _
                                    "KSO=@P1,PersenKSO=@P2,PrdAwal=@P3,PrdAkhir=@P4,Bruto=@P5,Netto=@P6,CompanyId=@P7," & _
                                    "NoKontrak=@P8,TglKontrak=@P9,UserEntry=@P10,Minggu=@P11,StatusJob=@P12,Nama=@P13," & _
                                    "Alamat=@P14,Telepon=@P15,NPWP=@P16,NoRek=@P17,AtasNama=@P18,Bank=@P19,NoRekInduk=@P20,AtasNamaInduk=@P21," & _
                                    "BankInduk=@P22,Hari=@P23,TipeManajerial=@P24,Logo=@P25,TimeEntry=@P26,AddendumKe=@P27,Member1=@P28,Member2=@P29, " & _
                                    "Own=@P30,PersenShare1=@P31,PersenShare2=@P32,BrutoShare1=@P33,BrutoShare2=@P34,KetShare1=@P35,KetShare2=@P36," & _
                                    "Kategori=@P37,Deskripsi=@P38,Instansi=@P39 WHERE JobNo=@P40"
                    .Parameters.AddWithValue("@P1", DDLKso.Value)
                    .Parameters.AddWithValue("@P2", If(RdoOwn1.Checked = True, TxtShare1.Value, TxtShare2.Value))
                    .Parameters.AddWithValue("@P3", PrdAwal.Date)
                    .Parameters.AddWithValue("@P4", PrdAkhir.Date)
                    .Parameters.AddWithValue("@P5", TxtBruto.Value)
                    .Parameters.AddWithValue("@P6", TxtNetto.Value)
                    .Parameters.AddWithValue("@P7", TxtKontraktor.Text)
                    .Parameters.AddWithValue("@P8", TxtNoKontrak.Text)
                    .Parameters.AddWithValue("@P9", TglKontrak.Date)
                    .Parameters.AddWithValue("@P10", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P11", TxtMinggu.Text)
                    .Parameters.AddWithValue("@P12", DDLStatus.Value)
                    .Parameters.AddWithValue("@P13", TxtNama1.Text)
                    .Parameters.AddWithValue("@P14", TxtAlamat.Text)
                    .Parameters.AddWithValue("@P15", TxtTelepon.Text)
                    .Parameters.AddWithValue("@P16", TxtNPWP.Text)
                    .Parameters.AddWithValue("@P17", TxtNoRek.Text)
                    .Parameters.AddWithValue("@P18", TxtAN.Text)
                    .Parameters.AddWithValue("@P19", If(DDLBank.Text = String.Empty, "", DDLBank.Value))
                    .Parameters.AddWithValue("@P20", TxtNoRekInduk.Text)
                    .Parameters.AddWithValue("@P21", TxtANInduk.Text)
                    .Parameters.AddWithValue("@P22", If(DDLBankInduk.Text = String.Empty, "", DDLBankInduk.Value))
                    .Parameters.AddWithValue("@P23", TxtHari.Text)
                    .Parameters.AddWithValue("@P24", DDLManajerial.Text)
                    If FileUpload1.HasFile Then
                        Dim fileName As String = Format(Now, "yyyy-MM-dd") & "_" & Format(Now, "hhmmss") & _
                            System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName)
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/") + fileName)
                        .Parameters.AddWithValue("@P25", "~/Images/" + fileName)
                    Else
                        .Parameters.AddWithValue("@P25", Image1.ImageUrl)
                    End If
                    .Parameters.AddWithValue("@P26", Now)
                    .Parameters.AddWithValue("@P27", TxtAddendumKe.Value)
                    .Parameters.AddWithValue("@P28", TxtMember1.Text)
                    .Parameters.AddWithValue("@P29", TxtMember2.Text)
                    If DDLKso.Value = "1" Then
                        .Parameters.AddWithValue("@P30", If(RdoOwn1.Checked = True, "1", "2"))
                    Else
                        .Parameters.AddWithValue("@P30", "")
                    End If
                    .Parameters.AddWithValue("@P31", TxtShare1.Value)
                    .Parameters.AddWithValue("@P32", TxtShare2.Value)
                    .Parameters.AddWithValue("@P33", TxtBruto1.Value)
                    .Parameters.AddWithValue("@P34", TxtBruto2.Value)
                    .Parameters.AddWithValue("@P35", Remark1.Text)
                    .Parameters.AddWithValue("@P36", Remark2.Text)
                    .Parameters.AddWithValue("@P37", DDLKategori.Text)
                    .Parameters.AddWithValue("@P38", TxtDesc.Text)
                    .Parameters.AddWithValue("@P39", TxtInstansi.Text)
                    .Parameters.AddWithValue("@P40", TxtJobNo.Text)
                    .ExecuteNonQuery()
                End With
            End Using

            Using CmdDelete As New Data.SqlClient.SqlCommand
                With CmdDelete
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "DELETE FROM DIPA WHERE JobNo=@P1"
                    .Parameters.AddWithValue("@P1", TxtJobNo.Text)
                    .ExecuteNonQuery()
                End With
            End Using

            TmpDipa = Session("TmpDipa")
            Using CmdInsert As New Data.SqlClient.SqlCommand
                For Each row As DataRow In TmpDipa.Rows
                    CmdInsert.Parameters.Clear()
                    With CmdInsert
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "INSERT INTO DIPA (JobNo,Tahun, Budget,UserEntry,TimeEntry) VALUES " & _
                                            "(@P1,@P2,@P3,@P4,@P5)"
                        .Parameters.AddWithValue("@P1", row("JobNo"))
                        .Parameters.AddWithValue("@P2", row("Tahun"))
                        .Parameters.AddWithValue("@P3", row("Budget"))
                        .Parameters.AddWithValue("@P4", Session("User").ToString.Split("|")(0))
                        .Parameters.AddWithValue("@P5", Now)
                        .ExecuteNonQuery()
                    End With
                Next row
            End Using

            Using CmdDelete As New Data.SqlClient.SqlCommand
                With CmdDelete
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "DELETE FROM JobH WHERE JobNo=@P1"
                    .Parameters.AddWithValue("@P1", TxtJobNo.Text)
                    .ExecuteNonQuery()
                End With
            End Using

            TmpAddendum = Session("TmpAddendum")
            Using CmdInsert As New Data.SqlClient.SqlCommand
                For Each row As DataRow In TmpAddendum.Rows
                    CmdInsert.Parameters.Clear()
                    With CmdInsert
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "INSERT INTO JobH (JobNo,Bruto,Netto,NoKontrak,TglKontrak,AddendumKe,PrdAwal,PrdAkhir,Hari,RemarkAddendum,UserEntry,TimeEntry) " & _
                                       "VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12)"
                        .Parameters.AddWithValue("@P1", row("JobNo"))
                        .Parameters.AddWithValue("@P2", row("Bruto"))
                        .Parameters.AddWithValue("@P3", row("Netto"))
                        .Parameters.AddWithValue("@P4", row("NoKontrak"))
                        .Parameters.AddWithValue("@P5", row("TglKontrak"))
                        .Parameters.AddWithValue("@P6", row("AddendumKe"))
                        .Parameters.AddWithValue("@P7", row("PrdAwal"))
                        .Parameters.AddWithValue("@P8", row("PrdAkhir"))
                        .Parameters.AddWithValue("@P9", row("Hari"))
                        .Parameters.AddWithValue("@P10", row("RemarkAddendum"))
                        .Parameters.AddWithValue("@P11", Session("User").ToString.Split("|")(0))
                        .Parameters.AddWithValue("@P12", Now)
                        .ExecuteNonQuery()
                    End With
                Next row
            End Using

        ElseIf DDLStatus.Value = "Pemeliharaan" Then
            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE Job SET " & _
                                    "StatusJob=@P1,NoPHO=@P2,TglPHO=@P3,UserEntry=@P6,TimeEntry=@P7 WHERE JobNo=@P8"
                    .Parameters.AddWithValue("@P1", DDLStatus.Value)
                    .Parameters.AddWithValue("@P2", TxtNoPHO.Text)
                    .Parameters.AddWithValue("@P3", TglPHO.Date)
                    .Parameters.AddWithValue("@P6", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P7", Now)
                    .Parameters.AddWithValue("@P8", TxtJobNo.Text)
                    .ExecuteNonQuery()
                End With
            End Using

        ElseIf DDLStatus.Value = "Closed" Then
            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE Job SET " & _
                                    "StatusJob=@P1,NoFHO=@P2,TglFHO=@P3,FHOFile=@P4,ClosedBy=@P5,TimeClosed=@P6,UserEntry=@P7,TimeEntry=@P8 " & _
                                    "WHERE JobNo=@P9"
                    .Parameters.AddWithValue("@P1", DDLStatus.Value)
                    .Parameters.AddWithValue("@P2", TxtNoFHO.Text)
                    .Parameters.AddWithValue("@P3", TglFHO.Date)
                    If FHOUpload.HasFile Then
                        Dim fileName As String = "FHO_" & Format(Now, "yyyy-MM-dd") & "_" & Format(Now, "hhmmss") & _
                            System.IO.Path.GetFileName(FHOUpload.PostedFile.FileName)
                        FHOUpload.PostedFile.SaveAs(Server.MapPath("~/PDF/") + fileName)
                        .Parameters.AddWithValue("@P4", "~/PDF/" + fileName)
                    Else
                        .Parameters.AddWithValue("@P4", "")
                    End If
                    .Parameters.AddWithValue("@P5", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P6", Now)
                    .Parameters.AddWithValue("@P7", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P8", Now)
                    .Parameters.AddWithValue("@P9", TxtJobNo.Text)
                    .ExecuteNonQuery()
                End With
            End Using

        End If

        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub

    Private Function CheckJobNo() As Boolean
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo FROM Job WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", TxtJobNo.Text)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    Return False
                Else
                    Return True
                End If
            End Using
        End Using

    End Function

    Protected Sub DDLKso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLKso.SelectedIndexChanged
        'TxtPersenKso.Enabled = False
        'TxtPersenKso.Text = "0"
        'TxtMember1.Text = ""
        'TxtMember2.Text = ""
        'TxtMember1.Enabled = False
        'TxtMember2.Enabled = False
        'RdoOwn1.Enabled = False
        'RdoOwn2.Enabled = False

        'If DDLKso.Value = "1" Then
        '    If DDLManajerial.Value = "JO Integrated" Then
        '        TxtPersenKso.Text = "100"
        '    Else
        '        TxtPersenKso.Enabled = True
        '        TxtMember1.Enabled = True
        '        TxtMember2.Enabled = True
        '        RdoOwn1.Enabled = True
        '        RdoOwn2.Enabled = True
        '    End If

        'End If

    End Sub

    Private Function FindWeekNumber() As Integer
        'Function untuk cari minggu ke berapa untuk parameter input Tglv; convert masing2 tgl ke hari senin
        '1 minggu = Senin s.d. Minggu

        Return DateDiff(DateInterval.Weekday, FindMondayDate(PrdAwal.Date), FindMondayDate(PrdAkhir.Date), Microsoft.VisualBasic.FirstDayOfWeek.Monday) + 1

    End Function

    Private Function FindMondayDate(Tglv As Date) As Date 'Function untuk cari tanggal hari senin untuk parameter input Tglv
        If Tglv.DayOfWeek = DayOfWeek.Sunday Then Tglv = Tglv.AddDays(-1)
        Dim MondayDate As DateTime
        MondayDate = Tglv.AddDays(DayOfWeek.Monday - Tglv.DayOfWeek)

        Return MondayDate

    End Function

    Protected Sub PrdAkhir_DateChanged(sender As Object, e As EventArgs) Handles PrdAkhir.DateChanged
        'TxtMinggu.Text = FindWeekNumber()
        If PrdAwal.Text = String.Empty Then Exit Sub
        If PrdAwal.Date > PrdAkhir.Date Then
            TxtHari.Value = 0
            TxtMinggu.Value = 0
            Exit Sub
        End If

        TxtHari.Text = DateDiff(DateInterval.Day, PrdAwal.Value, PrdAkhir.Value) + 1
        TxtMinggu.Text = If(TxtHari.Text < 7, "0", Format(TxtHari.Text / 7, "N0"))

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
        ContentControl6.Enabled = True
        LnkView.Visible = True
        LnkView.Enabled = True

    End Sub

    Protected Sub TxtBruto_NumberChanged(sender As Object, e As EventArgs) Handles TxtBruto.NumberChanged
        If TxtBruto.Text <> String.Empty Then
            TxtNetto.Text = CDec(TxtBruto.Text) - ((CDec(TxtBruto.Text) / 1.1) * (10 / 100)) - ((CDec(TxtBruto.Text) / 1.1) * (3 / 100))
        Else
            TxtNetto.Text = "0"
        End If

    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As System.EventArgs) Handles BtnAdd.Click
        If BtnAdd.Text = "TAMBAH" Then
            BtnAdd.Text = "SIMPAN"
            BtnCcl.Visible = True
            TxtTahun.Enabled = True
            TxtDIPA.Enabled = True
            GridView.Enabled = False
            TxtTahun.Focus()
        Else
            If TxtTahun.Text = "" Then
                msgBox1.alert("Tahun belum diisi.")
                TxtTahun.Focus()
                Exit Sub
            End If
            If TxtDIPA.Text = "0" Then
                msgBox1.alert("Budget belum diisi.")
                TxtDIPA.Focus()
                Exit Sub
            End If

            TmpDipa = Session("TmpDipa")

            If LblDipa.Text = "NEW" Then
                If TmpDipa.Select("Tahun='" + TxtTahun.Text + "'").Length <> 0 Then
                    msgBox1.alert("Tahun " + TxtTahun.Text + " sudah ada.")
                    TxtTahun.Focus()
                    Exit Sub
                End If

                Dim Counter As Integer
                Dim result As DataRow = TmpDipa.Select("NoUrut > 0", "NoUrut DESC").FirstOrDefault
                If result Is Nothing Then
                    Counter = 1
                Else
                    Counter = result("NoUrut") + 1
                End If
                TmpDipa.Rows.Add(Counter, TxtJobNo.Text, TxtTahun.Text, TxtDIPA.Value)
            Else
                Dim result As DataRow = TmpDipa.Select("NoUrut='" & TxtNo.Text & "'").FirstOrDefault
                If result IsNot Nothing Then
                    result("Tahun") = TxtTahun.Text
                    result("Budget") = TxtDIPA.Value
                End If
            End If

            Session("TmpDipa") = TmpDipa
            GridView.DataSource = TmpDipa
            GridView.DataBind()

            BtnAdd.Text = "TAMBAH"
            BtnCcl.Visible = False
            BtnCcl_Click(BtnCcl, New EventArgs())
        End If
        
    End Sub

    Private Sub BtnCcl_Click(sender As Object, e As System.EventArgs) Handles BtnCcl.Click
        TxtTahun.Enabled = False
        TxtDIPA.Enabled = False
        TxtNo.Text = ""
        TxtTahun.Text = ""
        TxtDIPA.Text = "0"
        BtnAdd.Text = "TAMBAH"
        LblDipa.Text = "NEW"
        BtnCcl.Visible = False
        GridView.Enabled = True
    End Sub

    Private Sub GridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)
            TxtNo.Text = SelectRecord.Cells(0).Text
            TxtTahun.Text = SelectRecord.Cells(1).Text
            TxtDIPA.Text = SelectRecord.Cells(2).Text
            TxtTahun.Enabled = False
            TxtDIPA.Enabled = True
            BtnAdd.Text = "SIMPAN"
            BtnCcl.Visible = True
            GridView.Enabled = False
            LblDipa.Text = "UPD"
            TxtTahun.Focus()
        End If
        If e.CommandName = "BtnDelete" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)

            TmpDipa = Session("TmpDipa")

            Dim result As DataRow = TmpDipa.Select("Tahun='" + SelectRecord.Cells(1).Text + "'").FirstOrDefault
            If result IsNot Nothing Then result.Delete()

            GridView.DataSource = TmpDipa
            GridView.DataBind()

            Session("TmpDipa") = TmpDipa

        End If

    End Sub

    Private Sub BtnAddendum_Click(sender As Object, e As System.EventArgs) Handles BtnAddendum.Click
        TmpAddendum = Session("TmpAddendum")

        'Fungsi field New adalah untuk setelah simpan baru bisa tambah addendum lagi
        If TmpAddendum.Select("New=1").Length > 0 Then Exit Sub

        If TxtRemarkAddendum.Text = "" Then
            msgBox1.alert("Keterangan addendum belum diisi.")
            TxtRemarkAddendum.Focus()
            Exit Sub
        End If

        TmpAddendum.Rows.Add(TxtJobNo.Text, TxtBruto.Value, TxtNetto.Value, TxtNoKontrak.Text, TglKontrak.Date, TxtAddendumKe.Value, _
                                 PrdAwal.Date, PrdAkhir.Date, TxtHari.Text, TxtRemarkAddendum.Text, 1)
        TxtAddendumKe.Value = TxtAddendumKe.Value + 1
        TxtBruto.Text = "0"
        TxtNetto.Text = "0"
        TxtNoKontrak.Text = ""
        TglKontrak.Text = ""
        PrdAwal.Text = ""
        PrdAkhir.Text = ""
        TxtHari.Text = "0"
        TxtMinggu.Text = "0"

        TxtRemarkAddendum.Visible = False
        BtnAddendum.Enabled = False
        'Call EnabledObject(True)

        GridAdd.DataSource = TmpAddendum
        GridAdd.DataBind()

        Session("TmpAddendum") = TmpAddendum

    End Sub

    Private Function CheckJaminan() As Boolean
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT TOP 1 * FROM Jaminan WHERE JobNo=@P1 AND UserKembali IS NULL ORDER BY DrTgl"
                .Parameters.AddWithValue("@P1", TxtJobNo.Text)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    msgBox1.alert("Jaminan " + RsFind("Tipe") + " belum dikembalikan.")
                    TxtNoFHO.Focus()
                    Return False
                End If
            End Using
        End Using

        Return True

    End Function

    Protected Sub View(sender As Object, e As EventArgs)
        Response.ContentType = ContentType
        Response.AppendHeader("Content-Disposition", ("attachment; filename=" + System.IO.Path.GetFileName(LblPath.Text)))
        Response.WriteFile(LblPath.Text)
        Response.End()
    End Sub

    Private Sub DDLManajerial_ValueChanged(sender As Object, e As System.EventArgs) Handles DDLManajerial.ValueChanged
        If DDLManajerial.Value <> "Single" Then
            DDLKso.Value = "1"
        Else
            DDLKso.Value = "0"
        End If

    End Sub

    Private Sub BtnCclAdd_Click(sender As Object, e As System.EventArgs) Handles BtnCclAdd.Click
        BtnCclAdd.Visible = False
        BtnAddendum.Text = "TAMBAH ADDENDUM"
        'Call EnabledObject(False)

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Job WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", LblJobNo.Text)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    TxtNoKontrak.Text = RsFind("NoKontrak").ToString
                    TglKontrak.Value = RsFind("TglKontrak")
                    TxtAddendumKe.Text = RsFind("AddendumKe").ToString
                    PrdAwal.Value = If(IsDBNull(RsFind("PrdAwal")), vbNull, RsFind("PrdAwal"))
                    PrdAkhir.Value = If(IsDBNull(RsFind("PrdAkhir")), vbNull, RsFind("PrdAkhir"))
                    TxtMinggu.Text = RsFind("Minggu").ToString
                    TxtBruto.Text = Format(RsFind("Bruto"), "N0")
                    TxtNetto.Text = Format(RsFind("Netto"), "N0")
                    TxtHari.Text = RsFind("Hari").ToString
                End If
            End Using
        End Using

    End Sub

    Private Sub TxtHari_ValueChanged(sender As Object, e As System.EventArgs) Handles TxtHari.ValueChanged
        If PrdAwal.Text = String.Empty Then Exit Sub
        PrdAkhir.Date = PrdAwal.Date.AddDays(TxtHari.Text)
        TxtMinggu.Text = If(TxtHari.Text < 7, "0", Format(TxtHari.Text / 7, "N0"))
    End Sub

    Private Sub PrdAwal_DateChanged(sender As Object, e As System.EventArgs) Handles PrdAwal.DateChanged
        TxtHari_ValueChanged(TxtHari, New EventArgs())
    End Sub

End Class