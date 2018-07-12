Imports DevExpress.Web

Public Class Site1
    Inherits System.Web.UI.MasterPage
    Dim Conn As New System.Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then Response.Redirect("Default.aspx")

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()
        Call BindMenu()

    End Sub

    Private Sub BindMenu()
        Using CmdFind As New System.Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM CPanel"
            End With
            Using RsFind As System.Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    If String.IsNullOrEmpty(RsFind("CNotification").ToString) = False Then
                        If String.IsNullOrEmpty(RsFind("CTimeOut").ToString) = False Then
                            If RsFind("CTimeOut") > Now Then
                                lblNotification.Visible = True
                                lblNotification.Text = RsFind("CNotification").ToString
                            End If
                        End If
                    End If
                End If
            End Using
        End Using
        
        Dim CmdLogin As New System.Data.SqlClient.SqlCommand
        With CmdLogin
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Login " & _
                            "WHERE UserID='" & Session("User").ToString.Split("|")(1) & "'"
        End With
        Dim RsLogin As System.Data.SqlClient.SqlDataReader = CmdLogin.ExecuteReader
        If RsLogin.Read Then
            lblMaster.Text = "Login sebagai " & UCase(RsLogin("UserName")) & " pada hari ini " & Format(Now, "dd MMMM yyyy")

            On Error Resume Next
            Dim TmpMenuDaftar As Byte = 0
            Dim TmpMenuEntry As Byte = 0
            Dim TmpMenuAcc As Byte = 0
            Dim TmpSubMenuEntry1 As Byte = 0 'SubMenu Entry > Procurement
            Dim TmpSubMenuEntry2 As Byte = 0 'SubMenu Entry > PD/PJ
            Dim TmpMenuRpt As Byte = 0
            Dim TmpSubMenuRpt1 As Byte = 0 'SubMenu Report > Procurement
            Dim TmpSubMenuRpt2 As Byte = 0 'SubMenu Report > PD/PJ
            Dim TmpSubMenuRpt3 As Byte = 0 'SubMenu Report > Accounting
            Dim TmpMenuTools As Byte = 0
            Dim Item As DevExpress.Web.MenuItem

            If RsLogin("AksesMenu").ToString <> "*" Then
                'Menu Daftar

                If RsLogin("Alokasi") = "0" Then
                    TmpMenuDaftar = TmpMenuDaftar + 1
                    Item = Menu1.Items.FindByName("FrmAlokasi.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("Bank") = "0" Then
                    TmpMenuDaftar = TmpMenuDaftar + 1
                    Item = Menu1.Items.FindByName("FrmBank.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("RekPengirim") = "0" Then
                    TmpMenuDaftar = TmpMenuDaftar + 1
                    Item = Menu1.Items.FindByName("FrmRek.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("Vendor") = "0" Then
                    TmpMenuDaftar = TmpMenuDaftar + 1
                    Item = Menu1.Items.FindByName("FrmVendor.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("TipeForm") = "0" Then
                    TmpMenuDaftar = TmpMenuDaftar + 1
                    Item = Menu1.Items.FindByName("FrmTipeForm.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("Kategori") = "0" Then
                    TmpMenuDaftar = TmpMenuDaftar + 1
                    Item = Menu1.Items.FindByName("FrmKategori.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("Proposal") = "0" Then
                    TmpMenuDaftar = TmpMenuDaftar + 1
                    Item = Menu1.Items.FindByName("FrmProposal.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("Job") = "0" Then
                    TmpMenuDaftar = TmpMenuDaftar + 1
                    Item = Menu1.Items.FindByName("FrmKontrak.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("NPWP") = "0" Then
                    TmpMenuDaftar = TmpMenuDaftar + 1
                    Item = Menu1.Items.FindByName("FrmNPWP.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If TmpMenuDaftar = 9 Then
                    Item = Menu1.Items.FindByName("#Daftar")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                '-------------------

                'Menu Entry               
                If RsLogin("RAP") = "0" Then
                    TmpMenuEntry = TmpMenuEntry + 1
                    Item = Menu1.Items.FindByName("FrmRAP.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                'SubMenu Entry > Procurement
                If RsLogin("KO") = "0" Then
                    TmpSubMenuEntry2 = TmpSubMenuEntry2 + 1
                    Item = Menu1.Items.FindByName("FrmKO.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("ApprovalKO") = "0" Then
                    TmpSubMenuEntry2 = TmpSubMenuEntry2 + 1
                    Item = Menu1.Items.FindByName("FrmApprovalKO.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("Invoice") = "0" Then
                    TmpSubMenuEntry2 = TmpSubMenuEntry2 + 1
                    Item = Menu1.Items.FindByName("FrmInvoice.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("ClosingKO") = "0" Then
                    TmpSubMenuEntry2 = TmpSubMenuEntry2 + 1
                    Item = Menu1.Items.FindByName("FrmClosingKO.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("KOAddendum") = "0" Then
                    TmpSubMenuEntry2 = TmpSubMenuEntry2 + 1
                    Item = Menu1.Items.FindByName("FrmKOAddendum.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("TrackingKO") = "0" Then
                    TmpSubMenuEntry2 = TmpSubMenuEntry2 + 1
                    Item = Menu1.Items.FindByName("FrmTrackingKO.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("CancelPO") = "0" Then
                    TmpSubMenuEntry2 = TmpSubMenuEntry2 + 1
                    Item = Menu1.Items.FindByName("FrmCancelPO.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If TmpSubMenuEntry2 = 7 Then
                    TmpMenuEntry = TmpMenuEntry + 1
                    Item = Menu1.Items.FindByName("#Procurement1")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                '---------------------------       

                'SubMenu Entry > Finance
                If RsLogin("PD") = "0" Then
                    TmpSubMenuEntry1 = TmpSubMenuEntry1 + 1
                    Item = Menu1.Items.FindByName("FrmPD.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("ApprovalPD_KK") = "0" Then
                    TmpSubMenuEntry1 = TmpSubMenuEntry1 + 1
                    Item = Menu1.Items.FindByName("FrmApprovalPD_KK.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("ApprovalPD_KT") = "0" Then
                    TmpSubMenuEntry1 = TmpSubMenuEntry1 + 1
                    Item = Menu1.Items.FindByName("FrmApprovalPD_KT.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("ApprovalPD_DP") = "0" Then
                    TmpSubMenuEntry1 = TmpSubMenuEntry1 + 1
                    Item = Menu1.Items.FindByName("FrmApprovalPD_DP.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("ApprovalPD_TBP") = "0" Then
                    TmpSubMenuEntry1 = TmpSubMenuEntry1 + 1
                    Item = Menu1.Items.FindByName("FrmApprovalPD_TBP.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("ApprovalPD_DK") = "0" Then
                    TmpSubMenuEntry1 = TmpSubMenuEntry1 + 1
                    Item = Menu1.Items.FindByName("FrmApprovalPD_DK.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("RejectPD") = "0" Then
                    TmpSubMenuEntry1 = TmpSubMenuEntry1 + 1
                    Item = Menu1.Items.FindByName("FrmRejectPD.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("PJ") = "0" Then
                    TmpSubMenuEntry1 = TmpSubMenuEntry1 + 1
                    Item = Menu1.Items.FindByName("FrmPJ.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("ApprovalPJ") = "0" Then
                    TmpSubMenuEntry1 = TmpSubMenuEntry1 + 1
                    Item = Menu1.Items.FindByName("FrmApprovalPJ.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("PayPD") = "0" Then
                    TmpSubMenuEntry1 = TmpSubMenuEntry1 + 1
                    Item = Menu1.Items.FindByName("FrmPayment.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If TmpSubMenuEntry1 = 10 Then
                    TmpMenuEntry = TmpMenuEntry + 1
                    Item = Menu1.Items.FindByName("#Finance1")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                '---------------------------

                If RsLogin("RencanaTermin") = "0" Then
                    TmpMenuEntry = TmpMenuEntry + 1
                    Item = Menu1.Items.FindByName("FrmRencanaTermin.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("Termin") = "0" Then
                    TmpMenuEntry = TmpMenuEntry + 1
                    Item = Menu1.Items.FindByName("FrmTermin.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("ApprovalTermin") = "0" Then
                    TmpMenuEntry = TmpMenuEntry + 1
                    Item = Menu1.Items.FindByName("FrmApprovalTermin.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("Jaminan") = "0" Then
                    TmpMenuEntry = TmpMenuEntry + 1
                    Item = Menu1.Items.FindByName("FrmJaminan.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("RPPM") = "0" Then
                    TmpMenuEntry = TmpMenuEntry + 1
                    Item = Menu1.Items.FindByName("FrmProgress.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("RPPM1") = "0" Then
                    TmpMenuEntry = TmpMenuEntry + 1
                    Item = Menu1.Items.FindByName("FrmRPPM.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If TmpMenuEntry = 9 Then
                    Item = Menu1.Items.FindByName("#Entry")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                '-------------------

                'Menu Accounting
                If RsLogin("COA") = "0" Then
                    TmpMenuAcc = TmpMenuAcc + 1
                    Item = Menu1.Items.FindByName("FrmCOA.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                If RsLogin("COA") = "0" Then
                    TmpMenuAcc = TmpMenuAcc + 1
                    Item = Menu1.Items.FindByName("FrmCOAX.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                If RsLogin("GLReff") = "0" Then
                    TmpMenuAcc = TmpMenuAcc + 1
                    Item = Menu1.Items.FindByName("FrmGlReff.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                If RsLogin("Identitas") = "0" Then
                    TmpMenuAcc = TmpMenuAcc + 1
                    Item = Menu1.Items.FindByName("FrmIdentitas.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                If RsLogin("JurnalEntry") = "0" Then
                    TmpMenuAcc = TmpMenuAcc + 1
                    Item = Menu1.Items.FindByName("FrmJurnal.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                If RsLogin("JurnalApproval") = "0" Then
                    TmpMenuAcc = TmpMenuAcc + 1
                    Item = Menu1.Items.FindByName("FrmJurnalApproval.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                If RsLogin("JurnalExport") = "0" Then
                    TmpMenuAcc = TmpMenuAcc + 1
                    Item = Menu1.Items.FindByName("FrmExportJurnal.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                If RsLogin("QueryJurnal") = "0" Then
                    TmpMenuAcc = TmpMenuAcc + 1
                    Item = Menu1.Items.FindByName("FrmQueryJurnal.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If TmpMenuAcc = 8 Then
                    Item = Menu1.Items.FindByName("#Acc")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                'Menu Report
                'SubMenu Report > Procurement
                If RsLogin("RekapKO") = "0" Then
                    TmpSubMenuRpt2 = TmpSubMenuRpt2 + 1
                    Item = Menu1.Items.FindByName("FrmRekapKO.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("RptTrackingKO") = "0" Then
                    TmpSubMenuRpt2 = TmpSubMenuRpt2 + 1
                    Item = Menu1.Items.FindByName("FrmRptTraceKO.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("InvoiceReceipt") = "0" Then
                    TmpSubMenuRpt2 = TmpSubMenuRpt2 + 1
                    Item = Menu1.Items.FindByName("FrmInvoiceReceipt.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("ItemPrice") = "0" Then
                    TmpSubMenuRpt2 = TmpSubMenuRpt2 + 1
                    Item = Menu1.Items.FindByName("FrmItemPrice.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("QueryAP") = "0" Then
                    TmpSubMenuRpt2 = TmpSubMenuRpt2 + 1
                    Item = Menu1.Items.FindByName("FrmQueryAP.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("SerapRAPKO") = "0" Then
                    TmpSubMenuRpt2 = TmpSubMenuRpt2 + 1
                    Item = Menu1.Items.FindByName("FrmRAPKO.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("RekapKO") = "0" Then
                    TmpSubMenuRpt2 = TmpSubMenuRpt2 + 1
                    Item = Menu1.Items.FindByName("FrmRekapHutang.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If TmpSubMenuRpt2 = 7 Then
                    TmpMenuRpt = TmpMenuRpt + 1
                    Item = Menu1.Items.FindByName("#Procurement2")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                'SubMenu Report > Finance
                If RsLogin("DPPD") = "0" Then
                    TmpSubMenuRpt1 = TmpSubMenuRpt1 + 1
                    Item = Menu1.Items.FindByName("FrmDPPD.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("RekapPayment") = "0" Then
                    TmpSubMenuRpt1 = TmpSubMenuRpt1 + 1
                    Item = Menu1.Items.FindByName("FrmRekapPayment.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("SerapRAP") = "0" Then
                    TmpSubMenuRpt1 = TmpSubMenuRpt1 + 1
                    Item = Menu1.Items.FindByName("FrmPenyerapanRAP.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("QueryPD") = "0" Then
                    TmpSubMenuRpt1 = TmpSubMenuRpt1 + 1
                    Item = Menu1.Items.FindByName("FrmPenyerapanRAP.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If TmpSubMenuRpt1 = 4 Then
                    TmpMenuRpt = TmpMenuRpt + 1
                    Item = Menu1.Items.FindByName("#Finance2")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("QueryTermin") = "0" Then
                    TmpMenuRpt = TmpMenuRpt + 1
                    Item = Menu1.Items.FindByName("FrmQueryTermin.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("RealisasiTermin") = "0" Then
                    TmpMenuRpt = TmpMenuRpt + 1
                    Item = Menu1.Items.FindByName("FrmRealisasiTermin.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("SRPengeluaran") = "0" Then
                    TmpMenuRpt = TmpMenuRpt + 1
                    Item = Menu1.Items.FindByName("FrmSRPengeluaran.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("CashFlow") = "0" Then
                    TmpMenuRpt = TmpMenuRpt + 1
                    Item = Menu1.Items.FindByName("FrmCashFlow.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("PiutangProgress") = "0" Then
                    TmpMenuRpt = TmpMenuRpt + 1
                    Item = Menu1.Items.FindByName("FrmPiuProgress.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                '---------------------------    

                'SubMenu(Report > Accounting)
                If RsLogin("ResumePK") = "0" Then
                    TmpSubMenuRpt3 = TmpSubMenuRpt3 + 1
                    Item = Menu1.Items.FindByName("FrmResumePK.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("LapKeu") = "0" Then
                    TmpSubMenuRpt3 = TmpSubMenuRpt3 + 1
                    Item = Menu1.Items.FindByName("FrmLK.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("LabaRugi") = "0" Then
                    TmpSubMenuRpt3 = TmpSubMenuRpt3 + 1
                    Item = Menu1.Items.FindByName("FrmLR.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("BukuTambahan") = "0" Then
                    TmpSubMenuRpt3 = TmpSubMenuRpt3 + 1
                    Item = Menu1.Items.FindByName("FrmBukuTambahan.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("BukuBesar") = "0" Then
                    TmpSubMenuRpt3 = TmpSubMenuRpt3 + 1
                    Item = Menu1.Items.FindByName("FrmBukuBesar.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("NeracaMutasi") = "0" Then
                    TmpSubMenuRpt3 = TmpSubMenuRpt3 + 1
                    Item = Menu1.Items.FindByName("FrmNeracaMutasi.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("NotaAkuntansi") = "0" Then
                    TmpSubMenuRpt3 = TmpSubMenuRpt3 + 1
                    Item = Menu1.Items.FindByName("FrmNotaAkuntasi.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If TmpSubMenuRpt3 = 7 Then
                    TmpMenuRpt = TmpMenuRpt + 1
                    Item = Menu1.Items.FindByName("#Accounting2")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If TmpMenuRpt = 8 Then
                    Item = Menu1.Items.FindByName("#Report")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                '-------------------

                'Menu Tools
                If RsLogin("TrackPD") = "0" Then
                    TmpMenuTools = TmpMenuTools + 1
                    Item = Menu1.Items.FindByName("FrmTrackPD.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("SetupAkses") = "0" Then
                    TmpMenuTools = TmpMenuTools + 1
                    Item = Menu1.Items.FindByName("FrmSetupAkses.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("OverrideSaldo") = "0" Then
                    TmpMenuTools = TmpMenuTools + 1
                    Item = Menu1.Items.FindByName("FrmOverrideSaldo.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("ChangePasswd") = "0" Then
                    TmpMenuTools = TmpMenuTools + 1
                    Item = Menu1.Items.FindByName("FrmChangePasswd.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If RsLogin("CPanel") = "0" Then
                    TmpMenuTools = TmpMenuTools + 1
                    Item = Menu1.Items.FindByName("FrmMessage.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If TmpMenuTools = 5 Then
                    Item = Menu1.Items.FindByName("#Tools")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                '-------------------

            End If
        End If
        RsLogin.Close()
        CmdLogin.Dispose()
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    'Private Sub Menu1_ItemClick(source As Object, e As DevExpress.Web.MenuItemEventArgs) Handles Menu1.ItemClick
    '    'Not Parent/Sub Parent Menu

    '    If Left(e.Item.Name, 1) <> "#" Then
    '        Dim User = Session("User")
    '        Session.RemoveAll()
    '        Session("User") = User

    '        Response.Redirect(e.Item.Name)
    '    End If

    'End Sub

    Private Sub Menu1_Load(sender As Object, e As System.EventArgs) Handles Menu1.Load
        Dim menu As ASPxMenu = TryCast(sender, ASPxMenu)
        CorrectItem(menu.RootItem)

    End Sub

    Private Sub CorrectItem(ByVal item As MenuItem)
        If item Is Nothing Then
            Return
        End If
        If item.HasChildren Then
            item.NavigateUrl = Nothing
            For Each subItem As MenuItem In item.Items
                CorrectItem(subItem)
            Next subItem
        End If
    End Sub

End Class