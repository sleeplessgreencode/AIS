Public Class FrmEntrySPR
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

        LblAction.Text = Session("SPR").ToString.Split("|")(0)
        LblJobNo.Text = Session("SPR").ToString.Split("|")(1)
        LblNoSPR.Text = Session("SPR").ToString.Split("|")(2)
        LblTglSPR.Text = Session("SPR").ToString.Split("|")(3)

        If IsPostBack = False Then
            Call BindAlokasi()
            Call BindRAP()
            Call BindGridDataMaterial()
        End If
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
    Private Sub BindGridDataMaterial()
        Dim Ttl As Decimal = 0
        TglSPR.Date = Format(Now, "dd-MMM-yyyy")

        TxtJobNo.Text = LblJobNo.Text

        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("NoUrut", GetType(Integer)), _
                                                 New DataColumn("KdRAP", GetType(String)), _
                                                 New DataColumn("Uraian", GetType(String)), _
                                                 New DataColumn("Vol", GetType(Decimal)), _
                                                 New DataColumn("Uom", GetType(String)), _
                                                 New DataColumn("Alokasi", GetType(String))})

        If LblAction.Text <> "NEW" Then
            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM PRHdr WHERE NoSPR=@P1"
                .Parameters.AddWithValue("@P1", LblNoSPR.Text)
                '.CommandText = "SELECT * FROM KoHdr WHERE NoKO=@P1"
                '.Parameters.AddWithValue("@P1", LblNoKO.Text)
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                TxtNoSPR.Text = RsFind("NoSPR")
                TglSPR.Text = If(RsFind("TglSPR") Is DBNull.Value, String.Empty, RsFind("TglSPR"))
                LblTglSPR1.Text = If(RsFind("TglSPR") Is DBNull.Value, String.Empty, RsFind("TglSPR"))
                TxtKepada.Text = If(String.IsNullOrEmpty(RsFind("Kepada").ToString), String.Empty, RsFind("Kepada").ToString)
                TglPenggunaan.Text = If(RsFind("TglPenggunaan") Is DBNull.Value, String.Empty, RsFind("TglPenggunaan"))
                TxtUtkPekerjaan.Text = If(String.IsNullOrEmpty(RsFind("UtkPekerjaan").ToString), String.Empty, RsFind("UtkPekerjaan"))

                Dim CmdGrid As New Data.SqlClient.SqlCommand
                With CmdGrid
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM PRDtl WHERE NoSPR=@P1 ORDER BY NoUrut"
                    .Parameters.AddWithValue("@P1", LblNoSPR.Text)
                End With
                Dim RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                While RsGrid.Read
                    TmpDt.Rows.Add(RsGrid("NoUrut"), RsGrid("KdRAP"), RsGrid("Uraian"), RsGrid("Vol"), RsGrid("Uom"), RsGrid("Alokasi"))
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

        GridDataMaterial.DataSource = TmpDt
        Session("TmpDt") = TmpDt

        GridDataMaterial.DataBind()

        If Left(LblAction.Text, 3) = "SEE" Then
            DisableControls(Form)
        End If

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

        BtnSaveSPR.Visible = False
        BtnCancelSPR.Text = "OK"
        BtnCancelSPR.Enabled = True

    End Sub
    Private Sub BtnAddMaterial_Click(sender As Object, e As System.EventArgs) Handles BtnAddMaterial.Click
        TxtAction.Text = "NEW"
        TxtNo.Text = ""
        Call BindRAP()
        DDLRap.Value = "0"
        TxtUraian.Text = ""
        TxtVol.Text = "0"
        TxtUom.Text = ""
        PopEntry.ShowOnPageLoad = True
    End Sub
    Protected Sub BtnSaveMaterial_Click(sender As Object, e As EventArgs) Handles BtnSaveMaterial.Click
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
            TmpDt.Rows.Add(Counter, DDLRap.Value, TxtUraian.Text, TxtVol.Text, UCase(TxtUom.Text), DDLAlokasi.Value)
        Else
            Dim result As DataRow = TmpDt.Select("NoUrut='" & TxtNo.Text & "'").FirstOrDefault
            If result IsNot Nothing Then
                result("KdRAP") = DDLRap.Value
                result("Uraian") = Trim(TxtUraian.Text)
                result("Uom") = UCase(TxtUom.Text)
                result("Vol") = TxtVol.Text
                result("Alokasi") = DDLAlokasi.Value
            End If
        End If

        GridDataMaterial.DataSource = TmpDt

        GridDataMaterial.DataBind()

        Session("TmpDt") = TmpDt
        PopEntry.ShowOnPageLoad = False

    End Sub
    Private Sub GridDataMaterial_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDataMaterial.RowCommand
        If e.CommandName = "BtnUpdMaterial" Then
            Dim SelectRecord As GridViewRow = GridDataMaterial.Rows(e.CommandArgument)

            TxtAction.Text = "UPD"
            TxtNo.Text = SelectRecord.Cells(0).Text
            Call BindRAP()
            DDLRap.Value = SelectRecord.Cells(1).Text
            TxtUraian.Text = TryCast(SelectRecord.FindControl("LblUraian"), Label).Text.Replace("<br />", Environment.NewLine)
            TxtVol.Text = SelectRecord.Cells(3).Text
            TxtUom.Text = SelectRecord.Cells(4).Text
            DDLAlokasi.Value = SelectRecord.Cells(5).Text
            PopEntry.ShowOnPageLoad = True

        ElseIf e.CommandName = "BtnDelMaterial" Then
            Dim SelectRecord As GridViewRow = GridDataMaterial.Rows(e.CommandArgument)

            TmpDt = Session("TmpDT")
            TmpDt.Rows(e.CommandArgument).Delete()
            TmpDt.AcceptChanges()

            GridDataMaterial.DataSource = TmpDt
            GridDataMaterial.DataBind()
            Session("TmpDt") = TmpDt
        End If
    End Sub
    Protected Sub BtnSaveSPR_Click(sender As Object, e As System.EventArgs) Handles BtnSaveSPR.Click
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
            TxtNoSPR.Text = AssignNoSPR()
            If TxtNoSPR.Text = "" Then
                LblErr.Text = "Error while generate No SPR."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If

            Dim CmdInsert1 As New Data.SqlClient.SqlCommand
            With CmdInsert1
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO PRHdr (NoSPR, TglSPR, JobNo, Kepada, UtkPekerjaan, TglPenggunaan, " & _
                               "UserEntry, TimeEntry) VALUES (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8)"
                .Parameters.AddWithValue("@P1", TxtNoSPR.Text)
                .Parameters.AddWithValue("@P2", TglSPR.Date)
                .Parameters.AddWithValue("@P3", Trim(LblJobNo.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P4", TxtKepada.Text)
                .Parameters.AddWithValue("@P5", TxtUtkPekerjaan.Text)
                .Parameters.AddWithValue("@P6", TglPenggunaan.Date)
                .Parameters.AddWithValue("@P7", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P8", Now)
                .ExecuteNonQuery()
                .Dispose()
            End With
        Else
            'Check Database if NoSPR already approved, cannot be edited, else free to edit except NoSPR do it on select buttoncommand of GridData
            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE PRHdr SET TglSPR=@P1, JobNo=@P2, Kepada=@P3, UtkPekerjaan=@P4, " & _
                               "TglPenggunaan=@P5, UserEntry=@P6, TimeEntry=@P7 WHERE NoSPR=@P8"
                .Parameters.AddWithValue("@P1", TglSPR.Date)
                .Parameters.AddWithValue("@P2", Trim(LblJobNo.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P3", TxtKepada.Text)
                .Parameters.AddWithValue("@P4", TxtUtkPekerjaan.Text)
                .Parameters.AddWithValue("@P5", TglPenggunaan.Date)
                .Parameters.AddWithValue("@P6", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P7", Now)
                .Parameters.AddWithValue("@P8", TxtNoSPR.Text)
                .ExecuteNonQuery()
                .Dispose()
            End With

            Dim CmdDel As New Data.SqlClient.SqlCommand
            With CmdDel
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "DELETE PRDtl WHERE NoSPR=@P1"
                .Parameters.AddWithValue("@P1", TxtNoSPR.Text)
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
                .CommandText = "INSERT INTO PRDtl (NoSPR,NoUrut,KdRAP,Uraian,Vol,Uom,Alokasi,UserEntry,TimeEntry) VALUES " & _
                                    "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)"
                .Parameters.AddWithValue("@P1", TxtNoSPR.Text)
                .Parameters.AddWithValue("@P2", Counter)
                .Parameters.AddWithValue("@P3", row("KdRAP"))
                .Parameters.AddWithValue("@P4", row("Uraian"))
                .Parameters.AddWithValue("@P5", row("Vol"))
                .Parameters.AddWithValue("@P6", row("Uom"))
                .Parameters.AddWithValue("@P7", row("Alokasi"))
                .Parameters.AddWithValue("@P8", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P9", Now)
                .ExecuteNonQuery()
                .Dispose()
            End With
        Next row

        BtnCancelSPR_Click(BtnCancelSPR, New EventArgs())

    End Sub
    Protected Sub BtnCancelSPR_Click(sender As Object, e As System.EventArgs) Handles BtnCancelSPR.Click
        Session("Job") = Trim(LblJobNo.Text.Split("-")(0))

        Session.Remove("TmpDt")
        Session.Remove("SPR")
        TmpDt.Dispose()

        If LblAction.Text = "SEE_APPROVALSPR" Then
            Response.Redirect("FrmApprovalSPR.aspx")
        Else
            Response.Redirect("FrmSPR.aspx")
        End If

        Exit Sub
    End Sub
    Private Function AssignNoSPR() As String
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT NoSPR FROM PRHdr Where JobNo=@P1 ORDER BY NoSPR DESC"
            .Parameters.AddWithValue("@P1", Trim(LblJobNo.Text.Split("-")(0)))
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            AssignNoSPR = "PR" & Trim(LblJobNo.Text.Split("-")(0)) & Format(Right(RsFind("NoSPR"), 5) + 1, "00000")
        Else
            AssignNoSPR = "PR" & Trim(LblJobNo.Text.Split("-")(0)) & "00001"
        End If
    End Function
    Private Sub DDLAlokasi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLAlokasi.SelectedIndexChanged
        Call BindRAP()
    End Sub
End Class