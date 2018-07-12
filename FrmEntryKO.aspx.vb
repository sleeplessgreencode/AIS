Public Class FrmEntryKO
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

        If IsPostBack = False Then
            Call BindVendor()
            'Call BindKategori()
            'Call BindSubKategori()
            Call BindGrid()
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
            DDLVendor.Items.Add(RsFind("VendorNm"), RsFind("VendorId"))
        End While
        RsFind.Close()
        CmdFind.Dispose()
        DDLVendor.Value = "0"

        DDLVendor_SelectedIndexChanged(DDLVendor, New EventArgs())

    End Sub

    'Private Sub BindKategori()
    '    DDLKategori.Items.Clear()
    '    DDLKategori.Items.Add("SUBKON", "SUBKON")
    '    'Dim CmdIsi As New Data.SqlClient.SqlCommand
    '    'With CmdIsi
    '    '    .Connection = Conn
    '    '    .CommandType = CommandType.Text
    '    '    .CommandText = "SELECT KategoriId FROM Kategori"
    '    'End With
    '    'Dim RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
    '    'While RsIsi.Read
    '    '    DDLKategori.Items.Add(RsIsi(0), RsIsi(0))
    '    'End While
    '    'RsIsi.Close()
    '    'CmdIsi.Dispose()

    '    DDLKategori.Value = "SUBKON"
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
            .Parameters.AddWithValue("@P2", "B")
        End With
        Dim RsBind As Data.SqlClient.SqlDataReader = CmdBind.ExecuteReader
        While RsBind.Read
            'If RsBind("Tipe") = "Header" Then
            'DDLRap.Items.Add(RsBind("KdRAP") & " - " & RsBind("Uraian"), RsBind("Tipe"))
            'Else
            DDLRap.Items.Add(RsBind("KdRAP") & " - " & RsBind("Uraian"), RsBind("KdRAP"))
            'End If
        End While
        RsBind.Close()
        CmdBind.Dispose()

    End Sub

    Private Sub BindGrid()
        TglKO.Date = Format(Now, "dd-MMM-yyyy")

        TxtJob.Text = LblJobNo.Text

        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("NoUrut", GetType(Integer)), _
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
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                TxtNoKO.Text = RsFind("NoKO")
                TglKO.Date = RsFind("TglKO")
                DDLVendor.Value = RsFind("VendorId")
                DDLVendor_SelectedIndexChanged(DDLVendor, New EventArgs())
                'DDLKategori.Value = RsFind("KategoriId")
                'DDLSubKategori.Value = RsFind("SubKategoriId")
                AssignCB(RsFind("SyaratPembayaran"))
                CheckBoxK3.Checked = If(RsFind("K3").ToString = "1", True, False)
                TxtSyaratTeknis.Text = RsFind("SyaratTeknis").ToString
                TxtJadwalPengiriman.Text = RsFind("JadwalPengiriman").ToString
                TxtJadwalPembayaran.Text = RsFind("JadwalPembayaran").ToString
                TxtSanksi.Text = RsFind("Sanksi").ToString
                TxtKeterangan.Text = RsFind("Keterangan").ToString

                Dim CmdGrid As New Data.SqlClient.SqlCommand
                With CmdGrid
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    If LblAction.Text = "SEE_KOADDENDUMH" Then
                        .CommandText = "SELECT * FROM KoDtlH WHERE NoKO=@P1 AND TglKO=@P2 ORDER BY NoUrut"
                    Else
                        .CommandText = "SELECT * FROM KoDtl WHERE NoKO=@P1 ORDER BY NoUrut"
                    End If
                    .Parameters.AddWithValue("@P1", LblNoKO.Text)
                    .Parameters.AddWithValue("@P2", LblTglKO.Text)
                End With
                Dim RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                While RsGrid.Read
                    TmpDt.Rows.Add(RsGrid("NoUrut"), RsGrid("KdRAP"), RsGrid("Uraian"), RsGrid("Vol"), RsGrid("Uom"), RsGrid("HrgSatuan"))
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

        If TmpDt.Rows.Count > 0 Then Call GetTotal()

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
        ElseIf LblAction.Text = "SEE_CLOSINGKO" Then
            Response.Redirect("FrmClosingKO.aspx")
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
        If SaveCB() = "" Then
            LblErr.Text = "Belum pilih syarat pembayaran."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        TmpDt = Session("TmpDt")
        If TmpDt.Rows.Count = 0 Then
            LblErr.Text = "Belum input rincian pekerjaan."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        'If LblTglKO1.Text <> TglKO.Date Then
        '    Using CmdFind As New Data.SqlClient.SqlCommand
        '        With CmdFind
        '            .Connection = Conn
        '            .CommandType = CommandType.Text
        '            .CommandText = "SELECT TOP 1 * FROM KoHdr WHERE JobNo=@P1 AND NoKO<>@P2 AND TglKO>@P3 AND KategoriId='KONTRAK'"
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
                .CommandText = "INSERT INTO KoHdr (NoKO,TglKO,JobNo,VendorId,SubTotal," & _
                               "K3,SyaratTeknis,JadwalPengiriman,JadwalPembayaran,SyaratPembayaran,Sanksi,Keterangan,UserEntry," & _
                               "TimeEntry,KategoriId,QRCode) VALUES " & _
                               "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16)"
                .Parameters.AddWithValue("@P1", TxtNoKO.Text)
                .Parameters.AddWithValue("@P2", TglKO.Date)
                .Parameters.AddWithValue("@P3", Trim(LblJobNo.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P4", DDLVendor.Value)
                .Parameters.AddWithValue("@P5", GridData.Columns(6).FooterText)
                .Parameters.AddWithValue("@P6", If(CheckBoxK3.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P7", Trim(TxtSyaratTeknis.Text))
                .Parameters.AddWithValue("@P8", Trim(TxtJadwalPengiriman.Text))
                .Parameters.AddWithValue("@P9", Trim(TxtJadwalPembayaran.Text))
                .Parameters.AddWithValue("@P10", SaveCB)
                .Parameters.AddWithValue("@P11", Trim(TxtSanksi.Text))
                .Parameters.AddWithValue("@P12", Trim(TxtKeterangan.Text))
                .Parameters.AddWithValue("@P13", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P14", Now)
                .Parameters.AddWithValue("@P15", "KONTRAK")
                .Parameters.AddWithValue("@P16", GetQRCode)
                .ExecuteNonQuery()
                .Dispose()
            End With

        Else
            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE KoHdr SET SubTotal=@P1,K3=@P2,SyaratTeknis=@P3,JadwalPengiriman=@P4," & _
                               "JadwalPembayaran=@P5,SyaratPembayaran=@P6,Sanksi=@P7,Keterangan=@P8,UserEntry=@P9,TimeEntry=@P10," & _
                               "TglKO=@P11,VendorId=@P12 WHERE NoKO=@P13"
                .Parameters.AddWithValue("@P1", GridData.Columns(6).FooterText)
                .Parameters.AddWithValue("@P2", If(CheckBoxK3.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P3", Trim(TxtSyaratTeknis.Text))
                .Parameters.AddWithValue("@P4", Trim(TxtJadwalPengiriman.Text))
                .Parameters.AddWithValue("@P5", Trim(TxtJadwalPembayaran.Text))
                .Parameters.AddWithValue("@P6", SaveCB)
                .Parameters.AddWithValue("@P7", Trim(TxtSanksi.Text))
                .Parameters.AddWithValue("@P8", Trim(TxtKeterangan.Text))
                .Parameters.AddWithValue("@P9", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P10", Now)
                .Parameters.AddWithValue("@P11", TglKO.Date)
                .Parameters.AddWithValue("@P12", DDLVendor.Value)
                .Parameters.AddWithValue("@P13", TxtNoKO.Text)
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
            CmdInsert2.Parameters.Clear()
            Counter += 1
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
                .Parameters.AddWithValue("@P10", "B")
                .ExecuteNonQuery()
                .Dispose()
            End With
        Next row

        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub


    Private Sub GetTotal()
        Dim ttl As Decimal

        TmpDt = Session("TmpDt")

        For Each row As DataRow In TmpDt.Rows
            ttl += row("Vol") * row("HrgSatuan")
        Next row

        GridData.Columns(6).FooterText = Format(ttl, "N0")

    End Sub

    Private Sub GridData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            TxtAction.Text = "UPD"
            TxtNo.Text = SelectRecord.Cells(0).Text
            Call BindRAP()
            DDLRap.Value = SelectRecord.Cells(1).Text
            TxtUraian.Text = TryCast(SelectRecord.FindControl("LblUraian"), Label).Text.Replace("<br />", Environment.NewLine)
            TxtVol.Text = SelectRecord.Cells(3).Text
            TxtUom.Text = SelectRecord.Cells(4).Text
            TxtHrgSatuan.Text = SelectRecord.Cells(5).Text
            PopEntry.ShowOnPageLoad = True

        ElseIf e.CommandName = "BtnDelete" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            TmpDt = Session("TmpDt")
            'dt.DefaultView.Sort = "regNumber DESC" 'Sort lbh dahulu baru delete, jika tidak ada akan salah delete krn sort.
            TmpDt.Rows(e.CommandArgument).Delete()
            TmpDt.AcceptChanges()

            GridData.DataSource = TmpDt
            Call GetTotal()
            GridData.DataBind()
            Session("TmpDt") = TmpDt
        End If
    End Sub

    Private Function AssignNoKO() As String
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT CounterKO FROM Counter WHERE JobNo=@P1 AND Alokasi=@P2"
            .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
            .Parameters.AddWithValue("@P2", "B")
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE Counter SET CounterKO=@P1,UserEntry=@P2,TimeEntry=@P3 WHERE JobNo=@P4 AND Alokasi=@P5"
                .Parameters.AddWithValue("@P1", If(String.IsNullOrEmpty(RsFind("CounterKO").ToString) = True, 1, RsFind("CounterKO") + 1))
                .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P3", Now)
                .Parameters.AddWithValue("@P4", Trim(LblJobNo.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P5", "B")
                .ExecuteNonQuery()
                .Dispose()
            End With
            AssignNoKO = "KO" & Trim(LblJobNo.Text.Split("-")(0)) & Format(If(String.IsNullOrEmpty(RsFind("CounterKO").ToString) = True, 1, RsFind("CounterKO") + 1), "000")
        Else
            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Counter(JobNo,Alokasi,CounterKO,UserEntry,TimeEntry) VALUES " & _
                               "(@P1,@P2,@P3,@P4,@P5)"
                .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P2", "B")
                .Parameters.AddWithValue("@P3", 1)
                .Parameters.AddWithValue("@P4", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P5", Now)
                .ExecuteNonQuery()
                .Dispose()
            End With
            AssignNoKO = "KO" & Trim(LblJobNo.Text.Split("-")(0)) & "001"
        End If
        RsFind.Close()
        CmdFind.Dispose()

    End Function

    Protected Sub BtnSave1_Click(sender As Object, e As EventArgs) Handles BtnSave1.Click

        If DDLRap.Value = "0" Then
            LblErr.Text = "Belum ada RAP yang di-pilih"
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
            'Else
            '    If DDLRap.Value = "Header" Then
            '        LblErr.Text = "Tidak bisa pilih RAP dengan Tipe Header."
            '        ErrMsg.ShowOnPageLoad = True
            '        Call BindRAP()
            '        Exit Sub
            '    End If
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
            TmpDt.Rows.Add(Counter, DDLRap.Value, TxtUraian.Text, TxtVol.Text, UCase(TxtUom.Text), TxtHrgSatuan.Text)
        Else
            Dim result As DataRow = TmpDt.Select("NoUrut='" & TxtNo.Text & "'").FirstOrDefault
            If result IsNot Nothing Then
                result("KdRAP") = DDLRap.Value
                result("Uraian") = Trim(TxtUraian.Text)
                result("Uom") = UCase(TxtUom.Text)
                result("Vol") = TxtVol.Text
                result("HrgSatuan") = TxtHrgSatuan.Text
            End If
        End If

        GridData.DataSource = TmpDt
        Call GetTotal()
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

    Protected Sub DDLVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLVendor.SelectedIndexChanged
        TxtNama.Text = ""
        TxtAlamat.Text = ""
        TxtTelepon.Text = ""
        TxtNPWP.Text = ""

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
            TxtKategori.Text = RsBind("Kategori").ToString
            TxtUsaha.Text = RsBind("BidangUsaha").ToString

        End If
        RsBind.Close()
        CmdBind.Dispose()
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

End Class