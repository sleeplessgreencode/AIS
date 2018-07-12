Public Class FrmEntryPJ
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim TmpDt As New DataTable()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "PJ") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        LblAction.Text = Session("PJ").ToString.Split("|")(0)
        LblJobNo.Text = Session("PJ").ToString.Split("|")(1)
        LblNoPD.Text = Session("PJ").ToString.Split("|")(2)
        LblAlokasi.Text = Session("PJ").ToString.Split("|")(3)

        If IsPostBack = False Then
            Call BindGrid()
        End If

    End Sub

    Private Sub BindRAP()

        Dim CmdBind As New Data.SqlClient.SqlCommand
        With CmdBind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT KdRAP,Uraian FROM RAP WHERE JobNo=@P1 AND KdRAP=@P2 ORDER BY NoUrut ASC"
            .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
            .Parameters.AddWithValue("@P2", TxtRAP.Text)
        End With
        Dim RsBind As Data.SqlClient.SqlDataReader = CmdBind.ExecuteReader
        If RsBind.Read Then
            TxtRAP.Text = TxtRAP.Text & " - " & RsBind("Uraian")
        End If
        RsBind.Close()
        CmdBind.Dispose()

    End Sub

    Private Sub BindUraian(ByVal NoUrutv As String, ByVal Uraianv As String, ByVal Volv As String, ByVal HrgSatuanv As String)

        Dim CmdBind As New Data.SqlClient.SqlCommand
        With CmdBind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM PdDtl WHERE NoPD=@P1 AND NoUrut=@P2"
            .Parameters.AddWithValue("@P1", TxtNoPD.Text)
            .Parameters.AddWithValue("@P2", CInt(NoUrutv))
        End With
        Dim RsBind As Data.SqlClient.SqlDataReader = CmdBind.ExecuteReader
        If RsBind.Read Then
            TxtUraian.Text = RsBind("Uraian").ToString
            TxtVol.Text = RsBind("Vol").ToString
            TxtUom.Text = RsBind("Uom").ToString
            TxtHrgSatuan.Text = RsBind("HrgSatuan").ToString
        End If
        RsBind.Close()
        CmdBind.Dispose()

        TxtUraian1.Text = If(String.IsNullOrEmpty(Uraianv), TxtUraian.Text, Uraianv)
        TxtVol1.Text = If(String.IsNullOrEmpty(Volv) = True Or CDec(Volv) = 0, TxtVol.Text, Volv)
        TxtUom1.Text = TxtUom.Text
        TxtHrgSatuan1.Text = If(String.IsNullOrEmpty(HrgSatuanv) = True Or CDec(HrgSatuanv) = 0, TxtHrgSatuan.Text, HrgSatuanv)
    End Sub

    Private Function CekSaldo(ByVal NoPD As String, ByVal JobNo As String, ByVal TipeForm As String, ByVal KSO As Integer) As Decimal
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT TOP 1 Saldo FROM PdHdr WHERE NoPD<@P1 AND JobNo=@P2 AND TipeForm=@P3 AND KSO=@P4 AND RejectBy IS NULL ORDER BY NoPD DESC"
                .Parameters.AddWithValue("@P1", NoPD)
                .Parameters.AddWithValue("@P2", JobNo)
                .Parameters.AddWithValue("@P3", TipeForm)
                .Parameters.AddWithValue("@P4", KSO)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    Return RsFind("Saldo")
                End If
            End Using
        End Using

        Return 0
    End Function

    Private Sub BindGrid()
        TglPJ.Date = Format(Now, "dd-MMM-yyyy")
        Dim Saldo As Decimal = 0

        TxtJob.Text = LblJobNo.Text

        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("NoUrut", GetType(Integer)), _
                                                 New DataColumn("KdRAP", GetType(String)), _
                                                 New DataColumn("Uraian", GetType(String)), _
                                                 New DataColumn("Vol", GetType(Decimal)), _
                                                 New DataColumn("Uom", GetType(String)), _
                                                 New DataColumn("HrgSatuan", GetType(Decimal)), _
                                                 New DataColumn("NewRecord", GetType(String))})

        GridPD.Columns(5).FooterText = "Total Pertanggungjawaban"
        GridPD.Columns(5).FooterStyle.Font.Bold = True
        GridPD.Columns(5).FooterStyle.HorizontalAlign = HorizontalAlign.Center
        GridPD.Columns(6).FooterStyle.HorizontalAlign = HorizontalAlign.Right

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
            TxtAlokasi.Text = RsFind("Alokasi")
            If RsFind("Alokasi") = "C" Then
                BtnAdd.Enabled = True
            Else
                BtnAdd.Enabled = False
            End If
            Call BindAlokasi()
            PrdAwal.Date = RsFind("PrdAwal")
            PrdAkhir.Date = RsFind("PrdAkhir")
            TxtMinggu.Text = RsFind("Minggu")
            TglPD.Date = RsFind("TglPD")
            TxtForm.Text = RsFind("TipeForm")
            Call BindForm()
            TxtDesc.Text = RsFind("Deskripsi")
            AssignCB(RsFind("BuktiPendukung").ToString)
            TxtKo.Text = RsFind("NoKO").ToString
            Call BindKO()
            TxtNoTagihan.Text = RsFind("NoTagihan").ToString
            TxtNoPJ.Text = RsFind("NoPJ").ToString
            TglPJ.Value = If(IsDBNull(RsFind("TglPJ")), vbNull, RsFind("TglPJ"))
            'Cek Saldo seblumnya. jika lebih besar, maka ambil nilai saldo. dipentung dulu krn tipeform 01C masih overlapping. Edited on 25-May-2018
            'Saldo = CekSaldo(RsFind("NoPD"), RsFind("JobNo"), RsFind("TipeForm"), RsFind("KSO"))
            'TxtDana.Text = If(Saldo > RsFind("TotalPD"), Format(Saldo, "N0"), Format(RsFind("TotalPD"), "N0"))
            TxtDana.Text = Format(RsFind("TotalPD"), "N0")
            If RsFind("OverrideSaldo") = "1" Then
                LblOverride.Text = "1"
                TxtSaldo.Text = Format(RsFind("Saldo"), "N0")
                TxtEditSaldo.Text = RsFind("RemarkOverrideSaldo").ToString
                TxtEditSaldo.Visible = True
            End If

            TxtNama.Text = RsFind("Nama").ToString
            TxtAlamat.Text = RsFind("Alamat").ToString
            TxtTelepon.Text = RsFind("Telepon").ToString
            TxtNPWP.Text = RsFind("NPWP").ToString
            TxtNoRek.Text = RsFind("NoRek").ToString
            TxtAN.Text = RsFind("AtasNama").ToString
            TxtBank.Text = RsFind("Bank").ToString

            RsFind.Close()
            CmdFind.Dispose()

            Dim CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM PdDtl WHERE NoPD=@P1 ORDER BY NoUrut"
                .Parameters.AddWithValue("@P1", LblNoPD.Text)
            End With
            Dim RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
            While RsGrid.Read
                TmpDt.Rows.Add(RsGrid("NoUrut"), RsGrid("KdRAP"), RsGrid("PjUraian"), RsGrid("PjVol"), RsGrid("Uom"), RsGrid("PjHrgSatuan"), "No")
            End While
            RsGrid.Close()

        End If

        GridPD.DataSource = TmpDt
        Session("TmpDt") = TmpDt

        If TmpDt.Rows.Count > 0 Then
            Call GetTotal()
            If LblOverride.Text = "" Then
                TxtSaldo.Text = Format(CDec(TxtDana.Text) - CDec(GridPD.Columns(6).FooterText), "N0")
            End If

        End If

        GridPD.DataBind()

        If Left(LblAction.Text, 3) = "SEE" Then
            DisableControls(Form)
        End If

    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Session("Job") = LblJobNo.Text & "|" & LblAlokasi.Text

        Session.Remove("TmpDt")
        Session.Remove("PJ")
        TmpDt.Dispose()

        If LblAction.Text = "UPD_APPROVALPJ_KK" Or LblAction.Text = "SEE_APPROVALPJ_KK" Then
            Response.Redirect("FrmApprovalPJ.aspx")
        Else
            Response.Redirect("FrmPJ.aspx")
        End If

        Exit Sub
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As System.EventArgs) Handles BtnSave.Click
        If TglPJ.Text = "" Then
            LblErr.Text = "Tgl permintaan belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TglPJ.Date < TglPD.Date Then
            LblErr.Text = "Tgl PJ lebih kecil dari Tgl Permintaan."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If SaveCB() = "" Then
            LblErr.Text = "Belum pilih bukti pendukung."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        TmpDt = Session("TmpDt")
        For Each row As DataRow In TmpDt.Rows
            If row("Uraian").ToString = String.Empty Then
                LblErr.Text = "No. " & row("NoUrut").ToString & " belum isi PJ."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
        Next

        TxtNoPJ.Text = AssignNoPJ()
        If TxtNoPJ.Text = "" Then
            LblErr.Text = "Error while generate No PJ."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        Dim CmdEdit As New Data.SqlClient.SqlCommand
        With CmdEdit
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "UPDATE PdHdr SET NoPJ=@P1,TglPJ=@P2,TotalPJ=@P3,BuktiPendukung=@P4,Saldo=@P5,PjUserEntry=@P6,PjTimeEntry=@P7 " & _
                           "WHERE NoPD=@P8"
            .Parameters.AddWithValue("@P1", TxtNoPJ.Text)
            .Parameters.AddWithValue("@P2", TglPJ.Date)
            .Parameters.AddWithValue("@P3", GridPD.Columns(6).FooterText)
            .Parameters.AddWithValue("@P4", SaveCB)
            .Parameters.AddWithValue("@P5", TxtSaldo.Text)
            .Parameters.AddWithValue("@P6", Session("User").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P7", Now)
            .Parameters.AddWithValue("@P8", TxtNoPD.Text)
            .ExecuteNonQuery()
            .Dispose()
        End With

        Dim CmdEdit2 As New Data.SqlClient.SqlCommand
        For Each row As DataRow In TmpDt.Rows
            If row("NewRecord") = "Yes" Then
                Using CmdInsert2 As New Data.SqlClient.SqlCommand
                    With CmdInsert2
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "INSERT INTO PdDtl (NoPD,NoUrut,KdRAP,NoPJ,PjUraian,PjVol,Uom,PjHrgSatuan,PjUserEntry,PjTimeEntry) VALUES " & _
                                    "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10)"
                        .Parameters.AddWithValue("@P1", TxtNoPD.Text)
                        .Parameters.AddWithValue("@P2", row("NoUrut"))
                        .Parameters.AddWithValue("@P3", row("KdRAP"))
                        .Parameters.AddWithValue("@P4", TxtNoPJ.Text)
                        .Parameters.AddWithValue("@P5", row("Uraian"))
                        .Parameters.AddWithValue("@P6", row("Vol"))
                        .Parameters.AddWithValue("@P7", row("Uom"))
                        .Parameters.AddWithValue("@P8", row("HrgSatuan"))
                        .Parameters.AddWithValue("@P9", Session("User").ToString.Split("|")(0))
                        .Parameters.AddWithValue("@P10", Now)
                        .ExecuteNonQuery()
                    End With
                End Using
            Else
                CmdEdit2.Parameters.Clear()
                With CmdEdit2
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE PdDtl SET NoPJ=@P1,PjUraian=@P2,PjVol=@P3,PjHrgSatuan=@P4,PjUserEntry=@P5,PjTimeEntry=@P6 " & _
                                   "WHERE NoPD=@P7 AND NoUrut=@P8"
                    .Parameters.AddWithValue("@P1", TxtNoPJ.Text)
                    .Parameters.AddWithValue("@P2", row("Uraian"))
                    .Parameters.AddWithValue("@P3", row("Vol"))
                    .Parameters.AddWithValue("@P4", row("HrgSatuan"))
                    .Parameters.AddWithValue("@P5", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P6", Now)
                    .Parameters.AddWithValue("@P7", TxtNoPD.Text)
                    .Parameters.AddWithValue("@P8", row("NoUrut"))
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If
        Next row

        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub


    Private Sub GetTotal()
        Dim ttl As Decimal

        TmpDt = Session("TmpDt")

        For Each row As DataRow In TmpDt.Rows
            ttl += row("Vol") * row("HrgSatuan")
        Next row

        GridPD.Columns(6).FooterText = Format(ttl, "N0")

    End Sub

    Private Sub GridPD_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridPD.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridPD.Rows(e.CommandArgument)

            TxtAction.Text = "UPD"
            TxtNo.Text = SelectRecord.Cells(0).Text
            TxtNo1.Text = TxtNo.Text
            TxtRAP.Text = SelectRecord.Cells(1).Text
            Call BindRAP()
            TxtRAP1.Text = TxtRAP.Text
            Call BindUraian(SelectRecord.Cells(0).Text, SelectRecord.Cells(2).Text, SelectRecord.Cells(3).Text, SelectRecord.Cells(5).Text)

            If Left(LblAction.Text, 3) = "SEE" Then
                TxtUraian1.Enabled = False
                TxtVol1.Enabled = False
                TxtHrgSatuan1.Enabled = False
                BtnSave1.Visible = False
                BtnCancel1.Text = "OK"
            End If
            PopEntry.ShowOnPageLoad = True
        
        End If
    End Sub

    Private Function AssignNoPJ() As String
        
        AssignNoPJ = "PJ" & Mid(TxtNoPD.Text, 3, Len(TxtNoPD.Text) - 2)

    End Function

    Protected Sub BtnSave1_Click(sender As Object, e As EventArgs) Handles BtnSave1.Click
        If TxtUraian1.Text = "" Then
            LblErr.Text = "Uraian belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        'If TxtVol1.Text = "" Or TxtVol1.Text = "0" Then
        '    LblErr.Text = "Volume belum di-isi."
        '    ErrMsg.ShowOnPageLoad = True
        '    Exit Sub
        'End If
        'If TxtHrgSatuan1.Text = "" Or TxtHrgSatuan1.Text = "0" Then
        '    LblErr.Text = "Harga satuan belum di-isi."
        '    ErrMsg.ShowOnPageLoad = True
        '    Exit Sub
        'End If

        TmpDt = Session("TmpDt")

        If TxtAction.Text = "UPD" Then
            Dim result As DataRow = TmpDt.Select("NoUrut='" & TxtNo.Text & "'").FirstOrDefault
            If result IsNot Nothing Then
                result("Uraian") = TxtUraian1.Text
                result("Vol") = TxtVol1.Text
                result("HrgSatuan") = TxtHrgSatuan1.Text
            End If
        End If

        GridPD.DataSource = TmpDt
        Call GetTotal()
        TxtSaldo.Text = Format(CDec(TxtDana.Text) - CDec(GridPD.Columns(6).FooterText), "N0")
        GridPD.DataBind()

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
            If c.GetType.ToString = "System.Web.UI.WebControls.GridView" Then Continue For
            If c.GetType.ToString = "DevExpress.Web.ASPxPopupControl" Then Continue For

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

    Private Sub BindKO()

        If TxtKo.Text = Trim(LblJobNo.Text.Split("-")(0)) & "_INT" Then Exit Sub

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT VendorID FROM KoHdr WHERE NoKO=@P1"
            .Parameters.AddWithValue("@P1", TxtKo.Text)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            TxtVendor.Text = RsFind("VendorID")
        End If
        RsFind.Close()
        CmdFind.Dispose()

    End Sub

    Private Sub BindForm()
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TipeForm,Keterangan FROM TipeForm WHERE TipeForm=@P1"
            .Parameters.AddWithValue("@P1", TxtForm.Text)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        While RsFind.Read
            TxtForm.Text = RsFind("TipeForm") & " - " & RsFind("Keterangan")
        End While
        RsFind.Close()
        CmdFind.Dispose()
    End Sub

    Private Sub BindAlokasi()
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT Alokasi,Keterangan FROM Alokasi WHERE Alokasi=@P1"
            .Parameters.AddWithValue("@P1", TxtAlokasi.Text)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        While RsFind.Read
            TxtAlokasi.Text = RsFind("Alokasi") & " - " & RsFind("Keterangan")
        End While
        RsFind.Close()
        CmdFind.Dispose()

    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As System.EventArgs) Handles BtnAdd.Click
        TxtAction1.Text = "NEW"
        TxtNo2.Text = ""
        TxtUraian2.Text = ""
        TxtVol2.Text = "1"
        TxtUom2.Text = "Ls"
        TxtHrgSatuan2.Text = "0"
        PopEntry1.ShowOnPageLoad = True
    End Sub

    Private Sub BtnSave2_Click(sender As Object, e As System.EventArgs) Handles BtnSave2.Click
        If TxtUraian2.Text = "" Then
            LblErr.Text = "Uraian belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtHrgSatuan2.Text = "" Or TxtHrgSatuan2.Text = "0" Then
            LblErr.Text = "Harga satuan belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        TmpDt = Session("TmpDt")

        If TxtAction1.Text = "NEW" Then
            Dim Counter As Integer
            Dim result1 As DataRow = TmpDt.Select("NoUrut > 0", "NoUrut DESC").FirstOrDefault
            If result1 Is Nothing Then
                Counter = 1
            Else
                Counter = result1("NoUrut") + 1
            End If
            TmpDt.Rows.Add(Counter, DDLRap1.Value, TxtUraian2.Text, TxtVol2.Text, UCase(TxtUom2.Text), TxtHrgSatuan2.Text, "Yes")
        Else
            Dim result1 As DataRow = TmpDt.Select("NoUrut='" & TxtNo2.Text & "'").FirstOrDefault
            If result1 IsNot Nothing Then
                result1("KdRAP") = DDLRap1.Value
                result1("Uraian") = TxtUraian2.Text
                result1("Uom") = UCase(TxtUom2.Text)
                result1("Vol") = TxtVol2.Text
                result1("HrgSatuan") = TxtHrgSatuan2.Text
            End If
        End If

        Session("TmpDt") = TmpDt
        GridPD.DataSource = TmpDt
        Call GetTotal()
        GridPD.DataBind()

        PopEntry1.ShowOnPageLoad = False
    End Sub

    Private Sub BtnFillUraian_Click(sender As Object, e As System.EventArgs) Handles BtnFillUraian.Click
        TmpDt = Session("TmpDt")

        For Each row As DataRow In TmpDt.Rows
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM PdDtl WHERE NoPD=@P1 AND NoUrut=@P2"
                    .Parameters.AddWithValue("@P1", TxtNoPD.Text)
                    .Parameters.AddWithValue("@P2", row("NoUrut"))
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        row("Uraian") = RsFind("Uraian").ToString
                        row("Vol") = RsFind("Vol").ToString
                        row("HrgSatuan") = RsFind("HrgSatuan").ToString
                    End If
                End Using
            End Using
        Next

        Session("TmpDt") = TmpDt
        GridPD.DataSource = TmpDt
        Call GetTotal()
        TxtSaldo.Text = Format(CDec(TxtDana.Text) - CDec(GridPD.Columns(6).FooterText), "N0")
        GridPD.DataBind()
    End Sub

End Class