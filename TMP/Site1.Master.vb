Public Class Site1
    Inherits System.Web.UI.MasterPage
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then Response.Redirect("Default.aspx")

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()
        Call BindMenu()

    End Sub

    Private Sub BindMenu()
        Dim CmdLogin As New Data.SqlClient.SqlCommand
        With CmdLogin
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Login " & _
                            "WHERE UserID='" & Session("User").ToString.Split("|")(1) & "'"
        End With
        Dim RsLogin As Data.SqlClient.SqlDataReader = CmdLogin.ExecuteReader
        If RsLogin.Read Then
            lblMaster.Text = "Login sebagai " & UCase(RsLogin("UserName")) & "<br /> pada hari ini " & Format(Now, "dd MMMM yyyy")

            On Error Resume Next
            Dim TmpMenuDaftar As Byte = 0
            Dim TmpMenuEntry As Byte = 0
            Dim TmpSubMenuEntry1 As Byte = 0 'SubMenu Entry > Procurement
            Dim TmpSubMenuEntry2 As Byte = 0 'SubMenu Entry > PD/PJ
            Dim TmpMenuRpt As Byte = 0
            Dim TmpSubMenuRpt1 As Byte = 0 'SubMenu Report > Procurement
            Dim TmpSubMenuRpt2 As Byte = 0 'SubMenu Report > PD/PJ
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

                If TmpMenuDaftar = 8 Then
                    Item = Menu1.Items.FindByName("#Daftar")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                '-------------------

                'Menu Entry               
                If RsLogin("RAB") = "0" Then
                    TmpMenuEntry = TmpMenuEntry + 1
                    Item = Menu1.Items.FindByName("FrmRAB.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

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

                If TmpSubMenuEntry2 = 4 Then
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
                    Item = Menu1.Items.FindByName("FrmPayPD.aspx")
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

                If RsLogin("Termin") = "0" Then
                    TmpMenuEntry = TmpMenuEntry + 1
                    Item = Menu1.Items.FindByName("FrmTermin.aspx")
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
                    Item = Menu1.Items.FindByName("FrmRPPM1.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If TmpMenuEntry = 7 Then
                    Item = Menu1.Items.FindByName("#Entry")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                '-------------------

                'Menu Report
                'SubMenu Report > Procurement
                If RsLogin("RekapKO") = "0" Then
                    TmpSubMenuRpt2 = TmpSubMenuRpt2 + 1
                    Item = Menu1.Items.FindByName("FrmRekapKO.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If TmpSubMenuRpt2 = 1 Then
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

                If RsLogin("RealisasiTermin") = "0" Then
                    TmpSubMenuRpt1 = TmpSubMenuRpt1 + 1
                    Item = Menu1.Items.FindByName("FrmRealisasiTermin.aspx")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If

                If TmpSubMenuRpt1 = 3 Then
                    TmpMenuRpt = TmpMenuRpt + 1
                    Item = Menu1.Items.FindByName("#Finance2")
                    If Item IsNot Nothing Then
                        Item.Visible = False
                    End If
                End If
                '---------------------------    

                If TmpMenuRpt = 2 Then
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

                If TmpMenuTools = 4 Then
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

    Private Sub Menu1_ItemClick(source As Object, e As DevExpress.Web.MenuItemEventArgs) Handles Menu1.ItemClick
        'Not Parent/Sub Parent Menu

        If Left(e.Item.Name, 1) <> "#" Then
            Dim User = Session("User")
            Session.RemoveAll()
            Session("User") = User

            Response.Redirect(e.Item.Name)
        End If

    End Sub
End Class