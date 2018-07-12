Public Class FrmEntryPD
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim TmpDt As New DataTable() 'Untuk uraian PD
    Dim TmpInv As New DataTable() 'Untuk list invoice

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
        LblRKD.Text = Session("PD").ToString.Split("|")(4)
        LblAlokasi.Text = Session("PD").ToString.Split("|")(5)

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
        Dim TotalTerbayar As Decimal = 0
        DDLKo.Items.Clear()
        DDLKo.Items.Add("Pilih salah satu", "0")
        DDLKo.Items.Add(Trim(LblJobNo.Text.Split("-")(0)) & "_INT", Trim(LblJobNo.Text.Split("-")(0)) & "_INT")

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM KoHdr WHERE JobNo=@P1 AND KategoriId=@P2 AND ApprovedBy IS NOT NULL AND CanceledBy IS NULL"
            .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
            .Parameters.AddWithValue("@P2", If(DDLForm.Value = "04B", "KONTRAK", "PO"))
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        While RsFind.Read
            If Left(LblAction.Text, 3) = "SEE" Then
                DDLKo.Items.Add(RsFind("NoKO"), RsFind("NoKO"))
            Else
                'Jika status kontrak sudah tutup
                If IsDBNull(RsFind("ClosedBy")) = False Then Continue While
                TotalTerbayar = 0
                Using CmdFind1 As New Data.SqlClient.SqlCommand
                    With CmdFind1
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT SUM(Amount) FROM BLE WHERE JobNo=@P1 AND NoKO=@P2"
                        .Parameters.AddWithValue("@P1", RsFind("JobNo"))
                        .Parameters.AddWithValue("@P2", RsFind("NoKO"))
                    End With
                    Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                        If RsFind1.Read Then
                            TotalTerbayar = If(IsDBNull(RsFind1(0)) = True, 0, RsFind1(0))
                        End If
                    End Using
                End Using
                If DDLForm.Value = "05B" Then
                    If RsFind("SubTotal") - RsFind("DiscAmount") + RsFind("PPN") > TotalTerbayar Then
                        DDLKo.Items.Add(RsFind("NoKO"), RsFind("NoKO"))
                    End If
                Else
                    DDLKo.Items.Add(RsFind("NoKO"), RsFind("NoKO"))
                End If
            End If
        End While
        RsFind.Close()
        CmdFind.Dispose()
        DDLKo.Enabled = True

        TabPage.TabPages(1).Visible = True

        If DDLForm.Value <> "04B" And DDLForm.Value <> "05B" And DDLForm.Value <> "01L" Then
            DDLKo.SelectedIndex = 1
        ElseIf DDLForm.Value = "04B" Or DDLForm.Value = "05B" Or DDLForm.Value = "01L" Then
            DDLKo.SelectedIndex = 0
        End If

    End Sub

    Private Sub BindAlokasi()
        'Dim AksesAlokasi As String = ""
        'Using CmdFind As New Data.SqlClient.SqlCommand
        '    With CmdFind
        '        .Connection = Conn
        '        .CommandType = CommandType.Text
        '        .CommandText = "SELECT AksesAlokasi FROM Login WHERE UserID=@P1"
        '        .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
        '    End With
        '    Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        '    If RsFind.Read Then
        '        AksesAlokasi = RsFind("AksesAlokasi")
        '    End If
        'End Using

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
                'If Array.IndexOf(AksesAlokasi.Split(","), RsFind("Alokasi")) >= 0 Then
                DDLAlokasi.Items.Add(RsFind("Alokasi") & " - " & RsFind("Keterangan"), RsFind("Alokasi"))
                'End If
            End While
        End Using

        DDLAlokasi.Value = "0"

    End Sub

    Private Sub BindAlokasi1()
        DDLAlokasi1.Items.Clear()
        DDLAlokasi1.Items.Add("Pilih salah satu", "0")

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT Alokasi,Keterangan FROM Alokasi WHERE Alokasi='B' OR Alokasi='C' OR Alokasi='L'"
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                DDLAlokasi1.Items.Add(RsFind("Alokasi") & " - " & RsFind("Keterangan"), RsFind("Alokasi"))
            End While
        End Using

        DDLAlokasi1.Value = "0"

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
            If DDLKo.Value <> Trim(LblJobNo.Text.Split("-")(0)) & "_INT" Then
                Using CmdFind1 As New Data.SqlClient.SqlCommand
                    With CmdFind1
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT * FROM KoDtl WHERE NoKO=@P1 AND NoUrut=@P2"
                        .Parameters.AddWithValue("@P1", DDLKo.Text)
                        .Parameters.AddWithValue("@P2", TxtNo.Text)
                    End With
                    Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                        If RsFind1.Read Then
                            .Parameters.AddWithValue("@P2", If(String.IsNullOrEmpty(RsFind1("Alokasi").ToString) = True, DDLAlokasi1.Value, RsFind1("Alokasi")))
                        Else
                            .Parameters.AddWithValue("@P2", DDLAlokasi1.Value)
                        End If
                    End Using
                End Using
            Else
                .Parameters.AddWithValue("@P2", DDLAlokasi1.Value)
            End If
        End With
        Dim RsBind As Data.SqlClient.SqlDataReader = CmdBind.ExecuteReader
        While RsBind.Read
            DDLRap.Items.Add(RsBind("KdRAP") & " - " & RsBind("Uraian"), RsBind("Tipe") & "|" & RsBind("KdRAP"))
        End While
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
        TmpInv.Columns.AddRange(New DataColumn() {New DataColumn("NoKO", GetType(String)), _
                                                 New DataColumn("VendorNm", GetType(String)), _
                                                 New DataColumn("InvNo", GetType(String)), _
                                                 New DataColumn("InvDate", GetType(Date)), _
                                                 New DataColumn("DueDate", GetType(Date)), _
                                                 New DataColumn("Total", GetType(Decimal)), _
                                                 New DataColumn("PPN", GetType(Decimal)), _
                                                 New DataColumn("FPNo", GetType(String)),
                                                 New DataColumn("FPDate", GetType(Date)), _
                                                 New DataColumn("PaymentAmount", GetType(Decimal)), _
                                                 New DataColumn("TotalPayment", GetType(Decimal)), _
                                                 New DataColumn("OriginalPayment", GetType(Decimal)), _
                                                 New DataColumn("IsChecked", GetType(String))})
        Session("TmpDt") = TmpDt
        Session("TmpInv") = TmpInv

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
                TxtDesc.Text = RsFind("Deskripsi")
                AssignCB(RsFind("BuktiPendukung").ToString)
                TxtNoTagihan.Text = RsFind("NoTagihan").ToString
                Call BindKO()
                DDLKo.Value = RsFind("NoKO")
                TxtNama.Text = RsFind("Nama").ToString
                TxtAlamat.Text = RsFind("Alamat").ToString
                TxtTelepon.Text = RsFind("Telepon").ToString
                TxtNPWP.Text = RsFind("NPWP").ToString
                TxtNoRek.Text = RsFind("NoRek").ToString
                TxtAN.Text = RsFind("AtasNama").ToString
                DDLBank.SelectedItem = DDLBank.Items.FindByValue(RsFind("Bank").ToString)

                TxtNoTagihan.Enabled = True
                TxtNama.Enabled = True
                TxtAlamat.Enabled = True
                TxtTelepon.Enabled = True
                TxtNPWP.Enabled = True

                TxtNoRek.Enabled = True
                TxtAN.Enabled = True
                DDLBank.Enabled = True

                If DDLKo.Value = Trim(LblJobNo.Text.Split("-")(0)) & "_INT" Then 'Ambil value dari PdHdr
                    'do nothing
                Else
                    Dim TotalInvoice As Decimal = 0
                    Dim TotalBayar As Decimal = 0

                    Using CmdFind1 As New Data.SqlClient.SqlCommand
                        With CmdFind1
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT *, " & _
                                           "(SELECT SUM(PaymentAmount) FROM InvPD WHERE NoKO=Invoice.NoKO AND InvNo=Invoice.InvNo) AS 'TotalPayment' " & _
                                           "FROM Invoice WHERE JobNo=@P1 AND NoKO=@P2"
                            .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
                            .Parameters.AddWithValue("@P2", DDLKo.Value)
                        End With
                        Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                            While RsFind1.Read
                                Using CmdFindInv As New Data.SqlClient.SqlCommand
                                    With CmdFindInv
                                        .Connection = Conn
                                        .CommandType = CommandType.Text
                                        .CommandText = "SELECT * FROM InvPD WHERE JobNo=@P1 AND NoPD=@P2 AND NoKO=@P3 AND InvNo=@P4"
                                        .Parameters.AddWithValue("@P1", RsFind1("JobNo"))
                                        .Parameters.AddWithValue("@P2", TxtNoPD.Text)
                                        .Parameters.AddWithValue("@P3", RsFind1("NoKO"))
                                        .Parameters.AddWithValue("@P4", RsFind1("InvNo"))
                                    End With
                                    Using RsFindInv As Data.SqlClient.SqlDataReader = CmdFindInv.ExecuteReader
                                        If RsFindInv.Read Then
                                            TotalBayar += RsFindInv("PaymentAmount")
                                            TmpInv.Rows.Add(RsFind1("NoKO"), TxtNama.Text, RsFind1("InvNo"), RsFind1("InvDate"), RsFind1("DueDate"), Format(RsFind1("Total"), "N0"), _
                                                Format(RsFind1("PPN"), "N0"), RsFind1("FPNo"), RsFind1("FPDate"), Format(RsFindInv("PaymentAmount"), "N0"), _
                                                If(IsDBNull(RsFind1("TotalPayment")) = True, 0, Format(RsFind1("TotalPayment"), "N0")), _
                                                Format(RsFindInv("PaymentAmount"), "N0"), "1")
                                        Else
                                            If RsFind1("Total") <= If(IsDBNull(RsFind1("TotalPayment")) = True, 0, RsFind1("TotalPayment")) Then Continue While
                                            TotalInvoice += RsFind1("Total")
                                            TmpInv.Rows.Add(RsFind1("NoKO"), TxtNama.Text, RsFind1("InvNo"), RsFind1("InvDate"), RsFind1("DueDate"), Format(RsFind1("Total"), "N0"), _
                                                Format(RsFind1("PPN"), "N0"), RsFind1("FPNo"), RsFind1("FPDate"), _
                                                If(IsDBNull(RsFind1("TotalPayment")) = True, Format(RsFind1("Total"), "N0"), Format(RsFind1("Total") - RsFind1("TotalPayment"), "N0")), _
                                                If(IsDBNull(RsFind1("TotalPayment")) = True, 0, Format(RsFind1("TotalPayment"), "N0")), 0, "0")
                                        End If
                                    End Using
                                End Using
                            End While
                        End Using
                    End Using

                    Session("TmpInv") = TmpInv
                    TxtTotalInvoice.Text = Format(TotalInvoice, "N0")
                    TxtTotalCentang.Text = Format(TotalBayar, "N0")
                    GridView.DataSource = TmpInv
                    GridView.DataBind()
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

                'DDLForm.Enabled = False
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

        Else
            DDLAlokasi.Value = LblAlokasi.Text
            DDLAlokasi_SelectedIndexChanged(DDLAlokasi, New EventArgs())
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
        Session("Job") = LblJobNo.Text & "|" & DDLAlokasi.Value

        Session.Remove("TmpDt")
        TmpDt.Dispose()
        Session.Remove("TmpInv")
        TmpInv.Dispose()
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
        If DDLKo.Value = "0" Then
            LblErr.Text = "Belum pilih No Kontrak/PO."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If SaveCB() = "" And DDLForm.Value = "05C" Then
            LblErr.Text = "Belum pilih bukti pendukung."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        Dim TmpPrice As Decimal = 0

        If Left(DDLKo.Text, 2) = "KO" Then
            If SaveCB() = "" Then
                LblErr.Text = "Belum pilih bukti pendukung."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If

            Dim JmlTick As Integer = 0
            For Each row As GridViewRow In GridView.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CheckBox1"), CheckBox)
                    If chkRow.Checked = False Then Continue For
                    TmpPrice = (CType(row.FindControl("TxtPayment"), DevExpress.Web.ASPxTextBox).Text)
                    If TmpPrice + CDec(row.Cells(12).Text) - CDec(row.Cells(13).Text) > CDec(row.Cells(7).Text) Then
                        MsgBox1.alert("Total bayar invoice " & row.Cells(4).Text & " melebihi total invoice.")
                        Exit Sub
                    End If
                    JmlTick += 1
                End If
            Next row

            If DDLForm.Value = "04B" Then
                If JmlTick > 0 Then
                    If TxtTotalCentang.Text <> TxtTotal.Text Then
                        MsgBox1.alert("Total Invoice <> Total Permintaan.")
                        Exit Sub
                    End If
                End If
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
                    End While
                End Using
            End Using

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT ISNULL(SUM(Amount),0) FROM BLE WHERE NoKO=@P1"
                    .Parameters.AddWithValue("@P1", DDLKo.Value)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        TotalTerbayar += RsFind(0)
                    End If
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
                        If String.IsNullOrEmpty(RsFind("ApprovedBy").ToString) = True Then
                            MsgBox1.alert(RsFind("NoKO") & " belum di-Approve.")
                            Exit Sub
                        End If

                        TotalKO = RsFind("SubTotal") - RsFind("DiscAmount") + RsFind("PPN")

                        If TotalKO < TotalPD And DDLForm.Value <> "04B" Then
                            LblErr.Text = "Total Akumulasi PD melebihi nilai Kontrak/PO." & "<br />" & _
                                            "Nilai Kontrak/PO " & RsFind("NoKO") & " : " & Format(TotalKO, "N0") & "<br />" & _
                                            "Total Akumulasi PD termasuk current : " & Format(TotalPD, "N0") & "<br />" & _
                                            "Nilai Kontrak/PO yang sudah dibayar : " & Format(TotalTerbayar, "N0")
                            ErrMsg.ShowOnPageLoad = True
                            Exit Sub
                        End If

                        If RsFind("KategoriId") = "PO" Then
                            If TxtTotalCentang.Text <> TxtTotal.Text Then
                                MsgBox1.alert("Total Invoice <> Total Permintaan.")
                                Exit Sub
                            End If
                        End If
                    End If
                End Using
            End Using

        End If
        If DDLBank.Value = "0" Or DDLBank.Text = "" Then
            LblErr.Text = "Bank rekening penerima belum di-pilih."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        'If TxtEditSaldo.Visible = True And Trim(TxtEditSaldo.Text) = "" Then
        '    LblErr.Text = "Remark Edit Saldo belum diisi."
        '    ErrMsg.ShowOnPageLoad = True
        '    Exit Sub
        'End If

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
                    .Parameters.AddWithValue("@P15", TxtNama.Text)
                    .Parameters.AddWithValue("@P16", TxtAlamat.Text)
                    .Parameters.AddWithValue("@P17", TxtTelepon.Text)
                    .Parameters.AddWithValue("@P18", TxtNPWP.Text)
                    .Parameters.AddWithValue("@P19", TxtNoRek.Text)
                    .Parameters.AddWithValue("@P20", DDLBank.Text)
                    .Parameters.AddWithValue("@P21", TxtAN.Text)
                    .Parameters.AddWithValue("@P22", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P23", Now)
                    .Parameters.AddWithValue("@P24", If(LblRKD.Text = "RKD", 1, 0))
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
                                   "AtasNama=@P16,UserEntry=@P17,TimeEntry=@P18,NoRef=@P19,TipeForm=@P20 WHERE NoPD=@P21"
                    .Parameters.AddWithValue("@P1", TglPD.Date)
                    .Parameters.AddWithValue("@P2", TxtDesc.Text)
                    .Parameters.AddWithValue("@P3", PrdAwal.Date)
                    .Parameters.AddWithValue("@P4", PrdAkhir.Date)
                    .Parameters.AddWithValue("@P5", TxtMinggu.Text)
                    .Parameters.AddWithValue("@P6", TxtSubTotal.Text)
                    .Parameters.AddWithValue("@P7", SaveCB)
                    .Parameters.AddWithValue("@P8", DDLKo.Value)
                    .Parameters.AddWithValue("@P9", TxtNoTagihan.Text.ToString)
                    .Parameters.AddWithValue("@P10", TxtNama.Text)
                    .Parameters.AddWithValue("@P11", TxtAlamat.Text)
                    .Parameters.AddWithValue("@P12", TxtTelepon.Text)
                    .Parameters.AddWithValue("@P13", TxtNPWP.Text)
                    .Parameters.AddWithValue("@P14", TxtNoRek.Text)
                    .Parameters.AddWithValue("@P15", DDLBank.Text)
                    .Parameters.AddWithValue("@P16", TxtAN.Text)
                    .Parameters.AddWithValue("@P17", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P18", Now)
                    .Parameters.AddWithValue("@P19", TxtNoRef.Text)
                    .Parameters.AddWithValue("@P20", DDLForm.Value)
                    .Parameters.AddWithValue("@P21", TxtNoPD.Text)
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

            Using CmdDel As New Data.SqlClient.SqlCommand
                With CmdDel
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "DELETE InvPD WHERE NoPD=@P1"
                    .Parameters.AddWithValue("@P1", TxtNoPD.Text)
                    .ExecuteNonQuery()
                End With
            End Using

        End If

        Dim Counter As Integer = 0
        Using CmdInsert As New Data.SqlClient.SqlCommand
            For Each row As DataRow In TmpDt.Rows
                Counter += 1
                CmdInsert.Parameters.Clear()
                With CmdInsert
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO PdDtl (NoPD,NoUrut,KdRAP,Uraian,Vol,Uom,HrgSatuan,UserEntry,TimeEntry) VALUES " & _
                                        "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)"
                    .Parameters.AddWithValue("@P1", TxtNoPD.Text)
                    .Parameters.AddWithValue("@P2", Counter)
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

        Using CmdInsert As New Data.SqlClient.SqlCommand
            For Each row As GridViewRow In GridView.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CheckBox1"), CheckBox)
                    If chkRow.Checked = False Then Continue For
                    TmpPrice = (CType(row.FindControl("TxtPayment"), DevExpress.Web.ASPxTextBox).Text)
                    CmdInsert.Parameters.Clear()
                    With CmdInsert
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "INSERT INTO InvPD (JobNo,NoKO,InvNo,NoPD,PaymentAmount,UserEntry,TimeEntry) VALUES " & _
                                            "(@P1,@P2,@P3,@P4,@P5,@P6,@P7)"
                        .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
                        .Parameters.AddWithValue("@P2", DDLKo.Value)
                        .Parameters.AddWithValue("@P3", row.Cells(4).Text)
                        .Parameters.AddWithValue("@P4", TxtNoPD.Text)
                        .Parameters.AddWithValue("@P5", TmpPrice)
                        .Parameters.AddWithValue("@P6", Session("User").ToString.Split("|")(0))
                        .Parameters.AddWithValue("@P7", Now)
                        .ExecuteNonQuery()
                    End With
                End If
            Next row
        End Using

        'Override saldo untuk PD sebelumnya.
        'If Trim(TxtEditSaldo.Text) <> "" Then
        '    Using CmdFind As New Data.SqlClient.SqlCommand
        '        With CmdFind
        '            .Connection = Conn
        '            .CommandType = CommandType.Text
        '            If LblAction.Text = "NEW" Then
        '                .CommandText = "SELECT TOP 1 * FROM PdHdr WHERE JobNo=@P2 AND TipeForm=@P3 AND KSO=0 AND RejectBy IS NULL ORDER BY NoPD DESC"
        '            Else
        '                .CommandText = "SELECT TOP 1 * FROM PdHdr WHERE NoPD<@P1 AND JobNo=@P2 AND TipeForm=@P3 AND KSO=0 AND RejectBy IS NULL ORDER BY NoPD DESC"
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
                If LblRKD.Text = "RKD" Then
                    .CommandText = "SELECT CounterKSO FROM Counter WHERE JobNo=@P1 AND Alokasi=@P2"
                ElseIf LblRKD.Text = "MIX" Then
                    .CommandText = "SELECT CounterPDMIX FROM Counter WHERE JobNo=@P1 AND Alokasi=@P2"
                Else
                    .CommandText = "SELECT CounterPD FROM Counter WHERE JobNo=@P1 AND Alokasi=@P2"
                End If
                .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    Using CmdEdit As New Data.SqlClient.SqlCommand
                        With CmdEdit
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            If LblRKD.Text = "RKD" Then
                                .CommandText = "UPDATE Counter SET CounterKSO=@P1,UserEntry=@P2,TimeEntry=@P3 WHERE JobNo=@P4 AND Alokasi=@P5"
                                .Parameters.AddWithValue("@P1", If(IsDBNull(RsFind("CounterKSO")) = True, 1, RsFind("CounterKSO") + 1))
                            ElseIf LblRKD.Text = "MIX" Then
                                .CommandText = "UPDATE Counter SET CounterPDMIX=@P1,UserEntry=@P2,TimeEntry=@P3 WHERE JobNo=@P4 AND Alokasi=@P5"
                                .Parameters.AddWithValue("@P1", If(IsDBNull(RsFind("CounterPDMIX")) = True, 1, RsFind("CounterPDMIX") + 1))
                            Else
                                .CommandText = "UPDATE Counter SET CounterPD=@P1,UserEntry=@P2,TimeEntry=@P3 WHERE JobNo=@P4 AND Alokasi=@P5"
                                .Parameters.AddWithValue("@P1", If(IsDBNull(RsFind("CounterPD")) = True, 1, RsFind("CounterPD") + 1))
                            End If
                            .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                            .Parameters.AddWithValue("@P3", Now)
                            .Parameters.AddWithValue("@P4", Trim(LblJobNo.Text.Split("-")(0)))
                            .Parameters.AddWithValue("@P5", DDLAlokasi.Value)
                            .ExecuteNonQuery()
                        End With
                    End Using

                    If LblRKD.Text = "RKD" Then
                        AssignNoPD = "PDRKD" & Trim(LblJobNo.Text.Split("-")(0)) & DDLAlokasi.Value & Format(If(IsDBNull(RsFind("CounterKSO")) = True, 1, RsFind("CounterKSO") + 1), "00000")
                    ElseIf LblRKD.Text = "MIX" Then
                        AssignNoPD = "PDMIX" & Trim(LblJobNo.Text.Split("-")(0)) & DDLAlokasi.Value & Format(If(IsDBNull(RsFind("CounterPDMIX")) = True, 1, RsFind("CounterPDMIX") + 1), "00000")
                    Else
                        AssignNoPD = "PD" & Trim(LblJobNo.Text.Split("-")(0)) & DDLAlokasi.Value & Format(If(IsDBNull(RsFind("CounterPD")) = True, 1, RsFind("CounterPD") + 1), "00000")
                    End If

                Else
                    Using CmdInsert As New Data.SqlClient.SqlCommand
                        With CmdInsert
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            If LblRKD.Text = "RKD" Then
                                .CommandText = "INSERT INTO Counter(JobNo,Alokasi,CounterKSO,UserEntry,TimeEntry) VALUES (@P1,@P2,@P3,@P4,@P5)"
                            ElseIf LblRKD.Text = "MIX" Then
                                .CommandText = "INSERT INTO Counter(JobNo,Alokasi,CounterPDMIX,UserEntry,TimeEntry) VALUES (@P1,@P2,@P3,@P4,@P5)"
                            Else
                                .CommandText = "INSERT INTO Counter(JobNo,Alokasi,CounterPD,UserEntry,TimeEntry) VALUES (@P1,@P2,@P3,@P4,@P5)"
                            End If
                            .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
                            .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                            .Parameters.AddWithValue("@P3", 1)
                            .Parameters.AddWithValue("@P4", Session("User").ToString.Split("|")(0))
                            .Parameters.AddWithValue("@P5", Now)
                            .ExecuteNonQuery()
                        End With
                    End Using
                    If LblRKD.Text = "RKD" Then
                        AssignNoPD = "PDRKD" & Trim(LblJobNo.Text.Split("-")(0)) & DDLAlokasi.Value & "00001"
                    ElseIf LblRKD.Text = "MIX" Then
                        AssignNoPD = "PDMIX" & Trim(LblJobNo.Text.Split("-")(0)) & DDLAlokasi.Value & "00001"
                    Else
                        AssignNoPD = "PD" & Trim(LblJobNo.Text.Split("-")(0)) & DDLAlokasi.Value & "00001"
                    End If

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
            'Diminta oleh pak Leo saat meeting tgl 12 Des 2017
            If (DDLForm.Value <> "04B" And DDLForm.Value <> "05B") And
                DDLRap.Value.ToString.Split("|")(0) = "Header" Then
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
        'If TxtHrgSatuan.Text = "" Then
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
        'DDLForm.Enabled = False
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
        TabPage.TabPages(1).Visible = False
        TxtNoTagihan.Enabled = True
        TxtNoTagihan.Text = ""

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

        DDLKo_SelectedIndexChanged(DDLKo, New EventArgs())
    End Sub

    Protected Sub DDLAlokasi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLAlokasi.SelectedIndexChanged
        DDLAlokasi1.Items.Clear()

        If DDLAlokasi.Value = "L" Then
            Call BindAlokasi1()
        Else
            DDLAlokasi1.Items.Add(DDLAlokasi.Text, DDLAlokasi.Value)
        End If
        DDLAlokasi1.SelectedIndex = 0

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
                    If LblRKD.Text = "RKD" Then
                        .CommandText = "SELECT TOP 1 * FROM PdHdr WHERE JobNo=@P2 AND TipeForm=@P3 AND KSO=1 AND RejectBy IS NULL ORDER BY NoPD DESC"
                    Else
                        .CommandText = "SELECT TOP 1 * FROM PdHdr WHERE JobNo=@P2 AND TipeForm=@P3 AND KSO=0 AND RejectBy IS NULL ORDER BY NoPD DESC"
                    End If
                Else
                    If LblRKD.Text = "RKD" Then
                        .CommandText = "SELECT TOP 1 * FROM PdHdr WHERE NoPD<@P1 AND JobNo=@P2 AND TipeForm=@P3 AND KSO=1 AND RejectBy IS NULL ORDER BY NoPD DESC"
                    Else
                        .CommandText = "SELECT TOP 1 * FROM PdHdr WHERE NoPD<@P1 AND JobNo=@P2 AND TipeForm=@P3 AND KSO=0 AND RejectBy IS NULL ORDER BY NoPD DESC"
                    End If
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
        TxtNama.Enabled = True
        TxtAlamat.Enabled = True
        TxtTelepon.Enabled = True
        TxtNPWP.Enabled = True

        TxtNoRek.Enabled = True
        TxtAN.Enabled = True
        DDLBank.Enabled = True

        TxtNama.Text = ""
        TxtAlamat.Text = ""
        TxtTelepon.Text = ""
        TxtNPWP.Text = ""

        TxtNoRek.Text = ""
        TxtAN.Text = ""
        DDLBank.Value = "0"

        TmpDt = Session("TmpDt")
        TmpDt.Rows.Clear()
        TmpInv = Session("TmpInv")
        TmpInv.Rows.Clear()

        TxtNoTagihan.Text = ""
        TxtTotalInvoice.Text = "0"
        TxtTotalCentang.Text = "0"

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

        ElseIf DDLKo.Value <> "0" Then
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT B.* FROM KoHdr A LEFT JOIN Vendor B ON A.VendorId=B.VendorId WHERE NoKO=@P1"
                    .Parameters.AddWithValue("@P1", DDLKo.Value)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        TxtNama.Text = RsFind("VendorNm")
                        TxtAlamat.Text = RsFind("Alamat").ToString
                        TxtTelepon.Text = RsFind("Telepon1").ToString
                        TxtNPWP.Text = RsFind("NPWP").ToString
                        TxtNoRek.Text = RsFind("NoRek").ToString
                        TxtAN.Text = RsFind("AtasNama").ToString
                        DDLBank.Value = RsFind("Bank").ToString

                        'If DDLForm.Value = "04B" Then
                        '    TxtNama.Enabled = True
                        '    TxtAlamat.Enabled = True
                        '    TxtTelepon.Enabled = True
                        '    TxtNPWP.Enabled = True
                        '    TxtNoRek.Enabled = True
                        '    TxtAN.Enabled = True
                        '    DDLBank.Enabled = True
                        'End If
                    End If
                End Using
            End Using

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM KoDtl WHERE NoKO=@P1"
                    .Parameters.AddWithValue("@P1", DDLKo.Value)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    While RsFind.Read
                        TmpDt.Rows.Add(RsFind("NoUrut"), AssignTipe(RsFind("KdRAP").ToString), RsFind("KdRAP"), RsFind("Uraian"), RsFind("Vol"), RsFind("Uom"), RsFind("HrgSatuan"))
                    End While
                End Using
            End Using

            Dim TotalInvoice As Decimal = 0
            Dim TotalBayar As Decimal = 0

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT *, " & _
                                    "(SELECT SUM(PaymentAmount) FROM InvPD WHERE NoKO=Invoice.NoKO AND InvNo=Invoice.InvNo) AS 'TotalPayment' " & _
                                    "FROM Invoice WHERE JobNo=@P1 AND NoKO=@P2"
                    .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
                    .Parameters.AddWithValue("@P2", DDLKo.Value)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    While RsFind.Read
                        TotalBayar = RsFind("Total")
                        Using CmdFind1 As New Data.SqlClient.SqlCommand
                            With CmdFind1
                                .Connection = Conn
                                .CommandType = CommandType.Text
                                .CommandText = "SELECT SUM(PaymentAmount) FROM InvPD WHERE JobNo=@P1 AND NoKO=@P2 AND InvNo=@P3"
                                .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
                                .Parameters.AddWithValue("@P2", DDLKo.Value)
                                .Parameters.AddWithValue("@P3", RsFind("InvNo"))
                            End With
                            Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                                If RsFind1.Read Then
                                    If IsDBNull(RsFind1(0)) = False Then
                                        If RsFind1(0) >= RsFind("Total") Then Continue While
                                        TotalBayar -= RsFind1(0)
                                    End If
                                End If
                            End Using
                        End Using

                        TmpInv.Rows.Add(RsFind("NoKO"), TxtNama.Text, RsFind("InvNo"), RsFind("InvDate"), RsFind("DueDate"), Format(RsFind("Total"), "N0"), _
                                        Format(RsFind("PPN"), "N0"), RsFind("FPNo"), RsFind("FPDate"), Format(TotalBayar, "N0"), _
                                        If(IsDBNull(RsFind("TotalPayment")) = True, 0, Format(RsFind("TotalPayment"), "N0")), 0, "0")
                        TotalInvoice += RsFind("Total")
                    End While
                End Using
            End Using

            TxtTotalInvoice.Text = Format(TotalInvoice, "N0")
            TxtTotalCentang.Text = "0"

        End If

        Session("TmpInv") = TmpInv
        GridView.DataSource = TmpInv
        GridView.DataBind()

        Session("TmpDt") = TmpDt
        GridPD.DataSource = TmpDt
        Call GetSubTotal()
        GridPD.DataBind()

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

    Protected Sub chk1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Call HitungTotalCentang()
    End Sub

    Protected Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Call HitungTotalCentang()
    End Sub

    Protected Sub TxtPay_Changed(ByVal sender As Object, ByVal e As EventArgs)
        Call HitungTotalCentang()
    End Sub

    Private Sub HitungTotalCentang()
        TxtNoTagihan.Text = ""
        Dim TotalCentang As Decimal = 0, NoTagihan As String = String.Empty
        For Each row As GridViewRow In GridView.Rows
            Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CheckBox1"), CheckBox)
            If chkRow.Checked = True Then
                NoTagihan = If(NoTagihan = String.Empty, row.Cells(4).Text, NoTagihan & ";" & row.Cells(4).Text)
                Dim TxtPay As DevExpress.Web.ASPxTextBox = TryCast(row.FindControl("TxtPayment"), DevExpress.Web.ASPxTextBox)
                TotalCentang += CDec(TxtPay.Text)
            End If
        Next
        TxtTotalCentang.Text = Format(TotalCentang, "N0")
        TxtNoTagihan.Text = If(Len(NoTagihan) > 100, Mid(NoTagihan, 1, 100), NoTagihan)

    End Sub

    Protected Function GetValue(ByVal IsChecked As String) As Boolean
        If IsChecked = "1" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub DDLAlokasi1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLAlokasi1.SelectedIndexChanged
        Call BindRAP()
    End Sub

End Class