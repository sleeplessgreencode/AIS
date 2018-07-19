Public Class FrmEntrySetup
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim TmpDt As New DataTable() 'Untuk Tampung Akses Job
    Dim TmpDt1 As New DataTable() 'Untuk Tampung Akses Alokasi

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "SetupAkses") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            TabPage.ActiveTabIndex = 0
            Call BindGrid()
        End If

    End Sub

    Private Sub BindGrid()
        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("IsChecked", GetType(String)), _
                                New DataColumn("JobNo", GetType(String)), _
                                New DataColumn("JobNm", GetType(String))})

        TmpDt1.Columns.AddRange(New DataColumn() {New DataColumn("IsChecked", GetType(String)), _
                                New DataColumn("Alokasi", GetType(String)), _
                                New DataColumn("Keterangan", GetType(String))})

        If Session("Setup") <> "NEW" Then
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM Login WHERE UserID=@P1"
                    .Parameters.AddWithValue("@P1", Session("Setup"))
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        TxtUserID.Text = RsFind("UserID")
                        TxtUserName.Text = RsFind("UserName")

                        'Menu Daftar
                        CbAlokasi.Checked = If(RsFind("Alokasi") = "0", False, True)
                        CbBank.Checked = If(RsFind("Bank") = "0", False, True)
                        CbRekening.Checked = If(RsFind("RekPengirim") = "0", False, True)
                        CbVendor.Checked = If(RsFind("Vendor") = "0", False, True)
                        CbTipeForm.Checked = If(RsFind("TipeForm") = "0", False, True)
                        CbProposal.Checked = If(RsFind("Proposal") = "0", False, True)
                        CbJob.Checked = If(RsFind("Job") = "0", False, True)
                        CbKategori.Checked = If(RsFind("Kategori") = "0", False, True)
                        CbNPWP.Checked = If(RsFind("NPWP") = "0", False, True)

                        'Menu Entry
                        CbRAP.Checked = If(RsFind("RAP") = "0", False, True)
                        CbPD.Checked = If(RsFind("PD") = "0", False, True)
                        CbApprovalPD_KK.Checked = If(RsFind("ApprovalPD_KK") = "0", False, True)
                        CbApprovalPD_KT.Checked = If(RsFind("ApprovalPD_KT") = "0", False, True)
                        CbApprovalPD_DP.Checked = If(RsFind("ApprovalPD_DP") = "0", False, True)
                        CbApprovalPD_TBP.Checked = If(RsFind("ApprovalPD_TBP") = "0", False, True)
                        CbApprovalPD_DK.Checked = If(RsFind("ApprovalPD_DK") = "0", False, True)
                        CbRejectPD.Checked = If(RsFind("RejectPD") = "0", False, True)
                        CbPJ.Checked = If(RsFind("PJ") = "0", False, True)
                        CbApprovalPJ.Checked = If(RsFind("ApprovalPJ") = "0", False, True)
                        CbPaymentPD.Checked = If(RsFind("PayPD") = "0", False, True)
                        CbEntrySPR.Checked = If(RsFind("EntrySPR") = "0", False, True)
                        CbApprovalSPR.Checked = If(RsFind("ApprovalSPR") = "0", False, True)
                        CbKO.Checked = If(RsFind("KO") = "0", False, True)
                        CbApprovalKO.Checked = If(RsFind("ApprovalKO") = "0", False, True)
                        CbInvoice.Checked = If(RsFind("Invoice") = "0", False, True)
                        CbClosingKO.Checked = If(RsFind("ClosingKO") = "0", False, True)
                        CbKOAddendum.Checked = If(RsFind("KOAddendum") = "0", False, True)
                        CbTrackingKO.Checked = If(RsFind("TrackingKO") = "0", False, True)
                        CbCancelPO.Checked = If(RsFind("CancelPO") = "0", False, True)
                        CbRPPM.Checked = If(RsFind("RPPM") = "0", False, True)
                        CbRPPM1.Checked = If(RsFind("RPPM1") = "0", False, True)
                        CbTermin.Checked = If(RsFind("Termin") = "0", False, True)
                        CbRencanaTermin.Checked = If(RsFind("RencanaTermin") = "0", False, True)
                        CbApprovalTermin.Checked = If(RsFind("ApprovalTermin") = "0", False, True)

                        'Menu Accounting
                        CbCOA.Checked = If(RsFind("COA") = "0", False, True)
                        CbGlReff.Checked = If(RsFind("GlReff") = "0", False, True)
                        CbIdentitas.Checked = If(RsFind("Identitas") = "0", False, True)
                        CbJurnalEntry.Checked = If(RsFind("JurnalEntry") = "0", False, True)
                        CbJurnalApproval.Checked = If(RsFind("JurnalApproval") = "0", False, True)
                        CbExportJurnal.Checked = If(RsFind("JurnalExport") = "0", False, True)
                        CbQueryJurnal.Checked = If(RsFind("QueryJurnal") = "0", False, True)

                        'Menu Report
                        CbDPPD.Checked = If(RsFind("DPPD") = "0", False, True)
                        CbRekapKO.Checked = If(RsFind("RekapKO") = "0", False, True)
                        CbTrackKO.Checked = If(RsFind("RptTrackingKO") = "0", False, True)
                        CbInvReceipt.Checked = If(RsFind("InvoiceReceipt") = "0", False, True)
                        CbItemPrice.Checked = If(RsFind("ItemPrice") = "0", False, True)
                        CbRekapPayment.Checked = If(RsFind("RekapPayment") = "0", False, True)
                        CbSerapRAP.Checked = If(RsFind("SerapRAP") = "0", False, True)
                        CbRealisasiTermin.Checked = If(RsFind("RealisasiTermin") = "0", False, True)
                        CbSRPengeluaran.Checked = If(RsFind("SRPengeluaran") = "0", False, True)
                        CbResumePK.Checked = If(RsFind("ResumePK") = "0", False, True)
                        CbLapKeu.Checked = If(RsFind("LapKeu") = "0", False, True)
                        CbLR.Checked = If(RsFind("LabaRugi") = "0", False, True)
                        CbNeracaMutasi.Checked = If(RsFind("NeracaMutasi") = "0", False, True)
                        CbNotaAkuntansi.Checked = If(RsFind("NotaAkuntansi") = "0", False, True)
                        CbBukuBesar.Checked = If(RsFind("BukuBesar") = "0", False, True)
                        CbBukuTambahan.Checked = If(RsFind("BukuTambahan") = "0", False, True)
                        CbCashFlow.Checked = If(RsFind("CashFlow") = "0", False, True)
                        CbQueryAP.Checked = If(RsFind("QueryAP") = "0", False, True)
                        CbQueryPD.Checked = If(RsFind("QueryPD") = "0", False, True)
                        CbRAPKO.Checked = If(RsFind("SerapRAPKO") = "0", False, True)
                        CbQueryTermin.Checked = If(RsFind("QueryTermin") = "0", False, True)
                        CbPiuProgress.Checked = If(RsFind("PiutangProgress") = "0", False, True)

                        'Menu Tools
                        CbTrackPD.Checked = If(RsFind("TrackPD") = "0", False, True)
                        CbSetup.Checked = If(RsFind("SetupAkses") = "0", False, True)
                        CbOverrideSaldo.Checked = If(RsFind("OverrideSaldo") = "0", False, True)
                        CbPasswd.Checked = If(RsFind("ChangePasswd") = "0", False, True)
                        CbMessage.Checked = If(RsFind("CPanel") = "0", False, True)

                        'Akses Job
                        Dim IsChecked As String = "0"
                        Using CmdGrid As New Data.SqlClient.SqlCommand
                            With CmdGrid
                                .Connection = Conn
                                .CommandType = CommandType.Text
                                .CommandText = "SELECT JobNo,JobNm FROM Job"
                            End With
                            Using RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                                While RsGrid.Read
                                    If RsFind("AksesJob") = "*" Then
                                        IsChecked = 1
                                    Else
                                        IsChecked = If(Array.IndexOf(RsFind("AksesJob").ToString.Split(","), RsGrid("JobNo")) >= 0, "1", "0")
                                    End If

                                    TmpDt.Rows.Add(IsChecked, RsGrid("JobNo"), RsGrid("JobNm"))
                                End While
                            End Using
                        End Using

                        'Akses Alokasi
                        IsChecked = "0"
                        Using CmdGrid As New Data.SqlClient.SqlCommand
                            With CmdGrid
                                .Connection = Conn
                                .CommandType = CommandType.Text
                                .CommandText = "SELECT * FROM Alokasi"
                            End With
                            Using RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                                While RsGrid.Read
                                    If IsDBNull(RsFind("AksesAlokasi")) = True Then Continue While

                                    IsChecked = If(Array.IndexOf(RsFind("AksesAlokasi").ToString.Split(","), RsGrid("Alokasi")) >= 0, "1", "0")

                                    TmpDt1.Rows.Add(IsChecked, RsGrid("Alokasi"), RsGrid("Keterangan"))
                                End While
                            End Using
                        End Using

                    End If
                End Using
            End Using

            TxtUserID.Enabled = False
            TxtAction.Text = "UPD"
        Else
            Using CmdGrid As New Data.SqlClient.SqlCommand
                With CmdGrid
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT JobNo,JobNm FROM Job"
                End With
                Using RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                    While RsGrid.Read
                        TmpDt.Rows.Add("0", RsGrid("JobNo"), RsGrid("JobNm"))
                    End While
                End Using
            End Using

            Using CmdGrid As New Data.SqlClient.SqlCommand
                With CmdGrid
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM Alokasi"
                End With
                Using RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                    While RsGrid.Read
                        TmpDt1.Rows.Add("0", RsGrid("Alokasi"), RsGrid("Keterangan"))
                    End While
                End Using
            End Using

        End If

        GridData.DataSource = TmpDt
        GridData.DataBind()
        GridView.DataSource = TmpDt1
        GridView.DataBind()

        Session("TmpDt") = TmpDt
        Session("TmpDt1") = TmpDt1

    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Session.Remove("Setup")
        Session.Remove("TmpDt")
        Session.Remove("TmpDt1")
        TmpDt.Dispose()
        TmpDt1.Dispose()

        Response.Redirect("FrmSetupAkses.aspx")
        Exit Sub
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If TxtAction.Text = "NEW" Then
            If String.IsNullOrEmpty(TxtPassword.Text) = True Then
                msgBox1.alert("Password belum diisi.")
                TxtPassword.Focus()
                Exit Sub
            End If
        End If
        If TxtPassword.Text <> TxtConfirmPw.Text Then
            msgBox1.alert("Password dan Confirm Password tidak sama.")
            TxtPassword.Focus()
            Exit Sub
        End If
        If TxtAction.Text = "NEW" Then
            If CheckUserId() = False Then Exit Sub

            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Login (UserID,UserName,Password,Alokasi,Bank,RekPengirim,Vendor,TipeForm,Kategori,Proposal," & _
                               "Job,RAP,KO,ApprovalKO,ClosingKO,KOAddendum,PD,ApprovalPD_KK,ApprovalPD_KT,ApprovalPD_DP,ApprovalPD_TBP," & _
                               "ApprovalPD_DK,RejectPD,PJ,ApprovalPJ,PayPD,Termin,RencanaTermin,RekapKO,DPPD,RekapPayment,RealisasiTermin," & _
                               "TrackPD,SetupAkses,ChangePasswd,AksesJob,UserEntry,TimeEntry,AksesAlokasi,RPPM,OverrideSaldo,SerapRAP,QueryTermin," & _
                               "SRPengeluaran,COA,GlReff,Identitas,JurnalEntry,JurnalApproval,ResumePK,JurnalExport,LapKeu,LabaRugi,BukuTambahan," & _
                               "BukuBesar,NeracaMutasi,NotaAkuntansi,TrackingKO,RptTrackingKO,CPanel,Invoice,InvoiceReceipt,ItemPrice,CancelPO,NPWP," & _
                               "ApprovalTermin,CashFlow,QueryAP,QueryPD,QueryJurnal,SerapRAPKO,RPPM1,PiutangProgress, EntrySPR, ApprovalSPR) VALUES " & _
                               "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21," & _
                               "@P22,@P23,@P24,@P25,@P26,@P27,@P28,@P29,@P30,@P31,@P32,@P33,@P34,@P35,@P36,@P37,@P38,@P39,@P40,@P41,@P42,@P43,@P44,@P45," & _
                               "@P46,@P47,@P48,@P49,@P50,@P51,@P52,@P53,@P54,@P55,@P56,@P57,@P58,@P59,@P60,@P61,@P62,@P63,@P64,@P65,@P66,@P67,@P68,@P69,@P70," & _
                               "@P71,@P72,@P73,@P74,@P75,@P76)"
                .Parameters.AddWithValue("@P1", TxtUserID.Text)
                .Parameters.AddWithValue("@P2", TxtUserName.Text)
                .Parameters.AddWithValue("@P3", Encrypt(TxtPassword.Text))
                .Parameters.AddWithValue("@P4", If(CbAlokasi.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P5", If(CbBank.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P6", If(CbRekening.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P7", If(CbVendor.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P8", If(CbTipeForm.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P9", If(CbKategori.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P10", If(CbProposal.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P11", If(CbJob.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P13", If(CbRAP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P14", If(CbKO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P15", If(CbApprovalKO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P16", If(CbClosingKO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P17", If(CbKOAddendum.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P18", If(CbPD.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P19", If(CbApprovalPD_KK.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P20", If(CbApprovalPD_KT.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P21", If(CbApprovalPD_DP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P22", If(CbApprovalPD_TBP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P23", If(CbApprovalPD_DK.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P24", If(CbRejectPD.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P25", If(CbPJ.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P26", If(CbApprovalPJ.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P27", If(CbPaymentPD.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P28", If(CbTermin.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P29", If(CbRencanaTermin.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P30", If(CbRekapKO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P31", If(CbDPPD.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P32", If(CbRekapPayment.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P33", If(CbRealisasiTermin.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P34", If(CbTrackPD.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P35", If(CbSetup.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P36", If(CbPasswd.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P37", GetAksesJob)
                .Parameters.AddWithValue("@P38", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P39", Now)
                .Parameters.AddWithValue("@P40", GetAksesAlokasi)
                .Parameters.AddWithValue("@P41", If(CbRPPM.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P42", If(CbOverrideSaldo.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P43", If(CbSerapRAP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P44", If(CbQueryTermin.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P45", If(CbSRPengeluaran.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P46", If(CbCOA.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P47", If(CbGlReff.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P48", If(CbIdentitas.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P49", If(CbJurnalEntry.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P50", If(CbJurnalApproval.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P51", If(CbResumePK.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P52", If(CbExportJurnal.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P53", If(CbLapKeu.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P54", If(CbLR.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P55", If(CbBukuTambahan.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P56", If(CbBukuBesar.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P57", If(CbNeracaMutasi.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P58", If(CbNotaAkuntansi.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P59", If(CbTrackingKO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P60", If(CbTrackKO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P61", If(CbMessage.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P62", If(CbInvoice.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P63", If(CbInvReceipt.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P64", If(CbItemPrice.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P65", If(CbCancelPO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P66", If(CbNPWP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P67", If(CbApprovalTermin.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P68", If(CbCashFlow.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P69", If(CbQueryAP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P70", If(CbQueryPD.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P71", If(CbQueryJurnal.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P72", If(CbRAPKO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P73", If(CbRPPM1.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P74", If(CbPiuProgress.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P75", If(CbEntrySPR.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P76", If(CbApprovalSPR.Checked = True, "1", "0"))
                .ExecuteNonQuery()
            End With
        Else
            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                If String.IsNullOrEmpty(TxtPassword.Text) = False Then
                    .CommandText = "UPDATE Login SET Password=@P1,Alokasi=@P2,Bank=@P3,RekPengirim=@P4,Vendor=@P5,TipeForm=@P6,Kategori=@P7,Proposal=@P8," & _
                                   "Job=@P9,RAP=@P11,KO=@P12,ApprovalKO=@P13,ClosingKO=@P14,KOAddendum=@P15,PD=@P16,ApprovalPD_KK=@P17," & _
                                   "ApprovalPD_KT=@P18,ApprovalPD_DP=@P19,ApprovalPD_TBP=@P20,ApprovalPD_DK=@P21,RejectPD=@P22,PJ=@P23,ApprovalPJ=@P24," & _
                                   "PayPD=@P25,Termin=@P26,RencanaTermin=@P27,RekapKO=@P28,DPPD=@P29,RekapPayment=@P30,RealisasiTermin=@P31," & _
                                   "TrackPD=@P32,SetupAkses=@P33,ChangePasswd=@P34,AksesJob=@P35,UserEntry=@P36,TimeEntry=@P37,AksesAlokasi=@P38," & _
                                   "RPPM=@P39,UserName=@P40,OverrideSaldo=@P41,SerapRAP=@P42,QueryTermin=@P43,SRPengeluaran=@P44,COA=@P45,GlReff=@P46," & _
                                   "Identitas=@P47,JurnalEntry=@P48,JurnalApproval=@P49,ResumePK=@P50,JurnalExport=@P51,LapKeu=@P52,LabaRugi=@P53," & _
                                   "BukuTambahan=@P54,BukuBesar=@P55,NeracaMutasi=@P56,NotaAkuntansi=@P57,TrackingKO=@P58,RptTrackingKO=@P59,CPanel=@P60," & _
                                   "Invoice=@P61,InvoiceReceipt=@P62,ItemPrice=@P63,CancelPO=@P64,NPWP=@P65,ApprovalTermin=@P66,CashFlow=@P67," & _
                                   "QueryAP=@P68,QueryPD=@P69,QueryJurnal=@P70,SerapRAPKO=@P71,RPPM1=@P72,PiutangProgress=@P73,EntrySPR=@P74," & _
                                   "ApprovalSPR=@P75 WHERE UserID=@P76"
                Else
                    .CommandText = "UPDATE Login SET Alokasi=@P2,Bank=@P3,RekPengirim=@P4,Vendor=@P5,TipeForm=@P6,Kategori=@P7,Proposal=@P8," & _
                                   "Job=@P9,RAP=@P11,KO=@P12,ApprovalKO=@P13,ClosingKO=@P14,KOAddendum=@P15,PD=@P16,ApprovalPD_KK=@P17," & _
                                   "ApprovalPD_KT=@P18,ApprovalPD_DP=@P19,ApprovalPD_TBP=@P20,ApprovalPD_DK=@P21,RejectPD=@P22,PJ=@P23,ApprovalPJ=@P24," & _
                                   "PayPD=@P25,Termin=@P26,RencanaTermin=@P27,RekapKO=@P28,DPPD=@P29,RekapPayment=@P30,RealisasiTermin=@P31," & _
                                   "TrackPD=@P32,SetupAkses=@P33,ChangePasswd=@P34,AksesJob=@P35,UserEntry=@P36,TimeEntry=@P37,AksesAlokasi=@P38," & _
                                   "RPPM=@P39,UserName=@P40,OverrideSaldo=@P41,SerapRAP=@P42,QueryTermin=@P43,SRPengeluaran=@P44,COA=@P45,GlReff=@P46," & _
                                   "Identitas=@P47,JurnalEntry=@P48,JurnalApproval=@P49,ResumePK=@P50,JurnalExport=@P51,LapKeu=@P52,LabaRugi=@P53," & _
                                   "BukuTambahan=@P54,BukuBesar=@P55,NeracaMutasi=@P56,NotaAkuntansi=@P57,TrackingKO=@P58,RptTrackingKO=@P59,CPanel=@P60," & _
                                   "Invoice=@P61,InvoiceReceipt=@P62,ItemPrice=@P63,CancelPO=@P64,NPWP=@P65,ApprovalTermin=@P66,CashFlow=@P67," & _
                                   "QueryAP=@P68,QueryPD=@P69,QueryJurnal=@P70,SerapRAPKO=@P71,RPPM1=@P72,PiutangProgress=@P73,EntrySPR=@P74," & _
                                   "ApprovalSPR=@P75 WHERE UserID=@P76"
                End If
                .Parameters.AddWithValue("@P1", Encrypt(TxtPassword.Text))
                .Parameters.AddWithValue("@P2", If(CbAlokasi.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P3", If(CbBank.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P4", If(CbRekening.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P5", If(CbVendor.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P6", If(CbTipeForm.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P7", If(CbKategori.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P8", If(CbProposal.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P9", If(CbJob.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P11", If(CbRAP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P12", If(CbKO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P13", If(CbApprovalKO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P14", If(CbClosingKO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P15", If(CbKOAddendum.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P16", If(CbPD.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P17", If(CbApprovalPD_KK.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P18", If(CbApprovalPD_KT.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P19", If(CbApprovalPD_DP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P20", If(CbApprovalPD_TBP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P21", If(CbApprovalPD_DK.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P22", If(CbRejectPD.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P23", If(CbPJ.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P24", If(CbApprovalPJ.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P25", If(CbPaymentPD.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P26", If(CbTermin.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P27", If(CbRencanaTermin.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P28", If(CbRekapKO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P29", If(CbDPPD.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P30", If(CbRekapPayment.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P31", If(CbRealisasiTermin.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P32", If(CbTrackPD.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P33", If(CbSetup.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P34", If(CbPasswd.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P35", GetAksesJob)
                .Parameters.AddWithValue("@P36", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P37", Now)
                .Parameters.AddWithValue("@P38", GetAksesAlokasi)
                .Parameters.AddWithValue("@P39", If(CbRPPM.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P40", TxtUserName.Text)
                .Parameters.AddWithValue("@P41", If(CbOverrideSaldo.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P42", If(CbSerapRAP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P43", If(CbQueryTermin.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P44", If(CbSRPengeluaran.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P45", If(CbCOA.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P46", If(CbGlReff.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P47", If(CbIdentitas.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P48", If(CbJurnalEntry.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P49", If(CbJurnalApproval.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P50", If(CbResumePK.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P51", If(CbExportJurnal.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P52", If(CbLapKeu.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P53", If(CbLR.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P54", If(CbBukuTambahan.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P55", If(CbBukuBesar.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P56", If(CbNeracaMutasi.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P57", If(CbNotaAkuntansi.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P58", If(CbTrackingKO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P59", If(CbTrackKO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P60", If(CbMessage.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P61", If(CbInvoice.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P62", If(CbInvReceipt.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P63", If(CbItemPrice.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P64", If(CbCancelPO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P65", If(CbNPWP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P66", If(CbApprovalTermin.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P67", If(CbCashFlow.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P68", If(CbQueryAP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P69", If(CbQueryPD.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P70", If(CbQueryJurnal.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P71", If(CbRAPKO.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P72", If(CbRPPM1.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P73", If(CbPiuProgress.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P74", If(CbEntrySPR.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P75", If(CbApprovalSPR.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P76", Session("Setup"))
                .ExecuteNonQuery()
                .Dispose()
            End With
        End If

        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub

    Protected Function GetValue(ByVal IsChecked As String) As Boolean
        If IsChecked = "1" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function Encrypt(clearText As String) As String

        Dim EncryptionKey As String = "sKlpxvB8hR43zYt3CQRwO1K94Q39k5m7"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create()
            Dim pdb As New System.Security.Cryptography.Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
             &H65, &H64, &H76, &H65, &H64, &H65, _
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New System.IO.MemoryStream()
                Using cs As New System.Security.Cryptography.CryptoStream(ms, encryptor.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText

    End Function

    Private Function Decrypt(cipherText As String) As String

        Dim EncryptionKey As String = "sKlpxvB8hR43zYt3CQRwO1K94Q39k5m7"
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using encryptor As System.Security.Cryptography.Aes = System.Security.Cryptography.Aes.Create()
            Dim pdb As New System.Security.Cryptography.Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, _
             &H65, &H64, &H76, &H65, &H64, &H65, _
             &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New System.IO.MemoryStream()
                Using cs As New System.Security.Cryptography.CryptoStream(ms, encryptor.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return cipherText

    End Function

    Private Function GetAksesJob() As String
        TmpDt = Session("TmpDt")

        Dim AksesJob As String = ""
        For Each row As GridViewRow In GridData.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CbSelect"), CheckBox)
                If chkRow.Checked Then
                    AksesJob = If(AksesJob = "", row.Cells(1).Text, AksesJob + "," + row.Cells(1).Text)
                End If
            End If
        Next

        Session("TmpDt") = TmpDt

        GetAksesJob = AksesJob

    End Function

    Private Function GetAksesAlokasi() As String
        TmpDt1 = Session("TmpDt1")

        Dim AksesAlokasi As String = ""
        For Each row As GridViewRow In GridView.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CbSelect"), CheckBox)
                If chkRow.Checked Then
                    AksesAlokasi = If(AksesAlokasi = "", row.Cells(1).Text, AksesAlokasi + "," + row.Cells(1).Text)
                End If
            End If
        Next

        Session("TmpDt1") = TmpDt1

        GetAksesAlokasi = AksesAlokasi

    End Function

    Private Function CheckUserId() As Boolean

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Login WHERE UserID=@P1"
                .Parameters.AddWithValue("@P1", Trim(TxtUserID.Text))
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    msgBox1.alert("User ID " & Trim(TxtUserID.Text) & " sudah ada.")
                    TxtUserID.Focus()
                    Return False
                End If
            End Using
        End Using

        Return True

    End Function

    Private Sub BtnAllMenu_Click(sender As Object, e As System.EventArgs) Handles BtnAllMenu.Click
        'Daftar
        CbAlokasi.Checked = True
        CbBank.Checked = True
        CbRekening.Checked = True
        CbVendor.Checked = True
        CbTipeForm.Checked = True
        CbProposal.Checked = True
        CbJob.Checked = True
        CbKategori.Checked = True
        CbNPWP.Checked = True

        'Entry
        CbRAP.Checked = True
        CbPD.Checked = True
        CbApprovalPD_KK.Checked = True
        CbApprovalPD_KT.Checked = True
        CbApprovalPD_DP.Checked = True
        CbApprovalPD_TBP.Checked = True
        CbApprovalPD_DK.Checked = True
        CbRejectPD.Checked = True
        CbPJ.Checked = True
        CbApprovalPJ.Checked = True
        CbPaymentPD.Checked = True
        CbEntrySPR.Checked = True
        CbApprovalSPR.Checked = True
        CbKO.Checked = True
        CbApprovalKO.Checked = True
        CbInvoice.Checked = True
        CbTermin.Checked = True
        CbClosingKO.Checked = True
        CbKOAddendum.Checked = True
        CbTrackingKO.Checked = True
        CbCancelPO.Checked = True
        CbRencanaTermin.Checked = True
        CbRPPM.Checked = True
        CbRPPM1.Checked = True
        CbApprovalTermin.Checked = True

        'Accounting
        CbCOA.Checked = True
        CbGlReff.Checked = True
        CbIdentitas.Checked = True
        CbJurnalEntry.Checked = True
        CbJurnalApproval.Checked = True
        CbExportJurnal.Checked = True
        CbQueryJurnal.Checked = True

        'Report
        CbDPPD.Checked = True
        CbRekapKO.Checked = True
        CbTrackKO.Checked = True
        CbInvReceipt.Checked = True
        CbItemPrice.Checked = True
        CbRekapPayment.Checked = True
        CbSerapRAP.Checked = True
        CbRealisasiTermin.Checked = True
        CbSRPengeluaran.Checked = True
        CbResumePK.Checked = True
        CbLapKeu.Checked = True
        CbLR.Checked = True
        CbBukuTambahan.Checked = True
        CbBukuBesar.Checked = True
        CbNeracaMutasi.Checked = True
        CbNotaAkuntansi.Checked = True
        CbCashFlow.Checked = True
        CbQueryAP.Checked = True
        CbQueryPD.Checked = True
        CbRAPKO.Checked = True
        CbQueryTermin.Checked = True
        CbPiuProgress.Checked = True

        'Tools
        CbTrackPD.Checked = True
        CbSetup.Checked = True
        CbOverrideSaldo.Checked = True
        CbPasswd.Checked = True
        CbMessage.Checked = True

    End Sub

    Private Sub BtnAllJob_Click(sender As Object, e As System.EventArgs) Handles BtnAllJob.Click
        TmpDt = Session("TmpDt")

        Dim result As DataRow
        For Each row As DataRow In TmpDt.Rows

            result = TmpDt.Select("JobNo='" & row("JobNo") & "'").FirstOrDefault
            If result IsNot Nothing Then
                result("IsChecked") = "1"
            End If

        Next

        GridData.DataSource = TmpDt
        GridData.DataBind()

        Session("TmpDt") = TmpDt

    End Sub

    Private Sub BtnAllAlokasi_Click(sender As Object, e As System.EventArgs) Handles BtnAllAlokasi.Click
        TmpDt1 = Session("TmpDt1")

        Dim result As DataRow
        For Each row As DataRow In TmpDt1.Rows

            result = TmpDt1.Select("Alokasi='" & row("Alokasi") & "'").FirstOrDefault
            If result IsNot Nothing Then
                result("IsChecked") = "1"
            End If

        Next

        GridView.DataSource = TmpDt1
        GridView.DataBind()

        Session("TmpDt1") = TmpDt1
    End Sub

End Class