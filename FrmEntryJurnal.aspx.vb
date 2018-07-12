Public Class FrmEntryJurnal
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim TmpDt As New DataTable() 'Untuk tampung detail

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User"), "JurnalEntry") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Action.Text = Session("Data").ToString.Split("|")(0)
            JobNo.Text = Session("Data").ToString.Split("|")(1)
            TxtSite.Text = Session("Data").ToString.Split("|")(2)
            TxtNoJurnal.Text = Session("Data").ToString.Split("|")(3)
            Source.Text = Session("Data").ToString.Split("|")(4)
            Tgl1.Text = Session("Data").ToString.Split("|")(5)
            Tgl2.Text = Session("Data").ToString.Split("|")(6)

            Call BindJob()
            Call BindMember()
            Call BindIdentitas()
            Call BindAccount()
            Call BindGrid()

        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindJob()
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo, JobNm FROM Job WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", JobNo.Text)
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                TxtJobNo.Text = RsFind("JobNo") + " - " + RsFind("JobNm")
            End If
        End Using

    End Sub

    Private Sub BindIdentitas()
        DDLIdentitas.Items.Clear()
        DDLIdentitas.Items.Add(String.Empty, String.Empty)

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Identitas WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", JobNo.Text)
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                DDLIdentitas.Items.Add(RsFind("Identitas") + " - " + RsFind("Nama"), RsFind("Identitas"))
            End While
        End Using

        DDLIdentitas.SelectedIndex = 0
    End Sub

    Private Sub BindAccount()
        DDLAccount.Items.Clear()
        DDLAccount.Items.Add(String.Empty, String.Empty)

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM COA"
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                DDLAccount.Items.Add(RsFind("AccNo") + " - " + RsFind("AccName"), RsFind("AccNo"))
            End While
        End Using

        DDLAccount.SelectedIndex = 0
    End Sub

    Private Sub BindGrid()
        Dim Counter As Integer = 0

        TxtTglJurnal.Date = Format(Now, "dd-MMM-yyyy")

        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("NoUrut", GetType(Integer)), _
                                                 New DataColumn("AccNo", GetType(String)), _
                                                 New DataColumn("AccName", GetType(String)), _
                                                 New DataColumn("Uraian", GetType(String)), _
                                                 New DataColumn("Debet", GetType(Decimal)), _
                                                 New DataColumn("Kredit", GetType(Decimal))})


        If Action.Text <> "NEW" Then
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT A.*, B.AccName FROM JurnalEntry A LEFT JOIN COA B ON A.AccNo=B.AccNo WHERE A.JobNo=@P1 AND NoJurnal=@P2"
                    .Parameters.AddWithValue("@P1", JobNo.Text)
                    .Parameters.AddWithValue("@P2", TxtNoJurnal.Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    While RsFind.Read
                        If RsFind("PC") = "P" Then
                            TxtTglJurnal.Date = RsFind("TglJurnal")
                            TxtSite.Text = RsFind("Site")
                            TxtMember.Text = RsFind("Member")
                            DDLNota.Value = RsFind("Nota")
                            DDLIdentitas.Value = RsFind("Identitas")
                            TxtNoReg.Text = RsFind("NoReg").ToString
                            LedgerNo.Text = RsFind("LedgerNo")
                        Else
                            Counter += 1
                            TmpDt.Rows.Add(Counter, RsFind("AccNo"), RsFind("AccName"), RsFind("Uraian"), RsFind("Debet"), RsFind("Kredit"))
                        End If
                    End While
                End Using
            End Using

            'TxtTglJurnal.Enabled = False
            'DDLNota.Enabled = False

        End If

        Session("TmpDt") = TmpDt

        Call HitBalance()
        GridView.DataSource = TmpDt
        GridView.DataBind()

        If Left(Action.Text, 3) = "SEE" Then Call DisableControls(Form)

    End Sub

    Private Sub BindMember()
        TxtMember.Text = ""

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM GlReff WHERE JobNo=@P1 AND Site=@P2"
                .Parameters.AddWithValue("@P1", JobNo.Text)
                .Parameters.AddWithValue("@P2", TxtSite.Text)
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                TxtMember.Text = RsFind("Member")
            End If
        End Using
    End Sub

    Private Sub BtnCancel1_Click(sender As Object, e As System.EventArgs) Handles BtnCancel1.Click
        Session("Job") = JobNo.Text & "|" & TxtSite.Text & "|" & Tgl1.Text & "|" & Tgl2.Text
        Session.Remove("TmpDt")
        TmpDt.Dispose()
        Session.Remove("Data")

        Response.Redirect(Source.Text)
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As System.EventArgs) Handles BtnAdd.Click
        Action1.Text = "NEW"
        DDLAccount.SelectedIndex = 0
        TxtUraian.Text = ""
        TxtDebet.Text = "0"
        TxtKredit.Text = "0"
        DDLAccount.Enabled = True

        DDLAccount.Focus()
        ModalEntry.ShowOnPageLoad = True
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As System.EventArgs) Handles BtnSave.Click
        If TxtDebet.Text > "0" And TxtKredit.Text > "0" Then
            msgBox1.alert("Debet & Kredit > 0")
            TxtDebet.Focus()
            Exit Sub
        End If
        If TxtDebet.Text = "0" And TxtKredit.Text = "0" Then
            msgBox1.alert("Debet & Kredit = 0")
            TxtDebet.Focus()
            Exit Sub
        End If

        Dim result As DataRow
        Dim counter As Integer = 0

        TmpDt = Session("TmpDt")

        If Action1.Text = "NEW" Then
            result = TmpDt.Select("NoUrut > 0", "NoUrut DESC").FirstOrDefault

            If result Is Nothing Then
                Counter = 1
            Else
                Counter = result("NoUrut") + 1
            End If

            TmpDt.Rows.Add(counter, DDLAccount.Value, Trim(DDLAccount.Text.Split("-")(1)), TxtUraian.Text, TxtDebet.Text, TxtKredit.Text)

        Else
            result = TmpDt.Select("NoUrut='" & TxtNo.Text & "'").FirstOrDefault
            If result IsNot Nothing Then
                result("AccNo") = DDLAccount.Value
                result("AccName") = Trim(DDLAccount.Text.Split("-")(1))
                result("Uraian") = TxtUraian.Text
                result("Debet") = TxtDebet.Text
                result("Kredit") = TxtKredit.Text
            End If
        End If

        Session("TmpDt") = TmpDt

        Call HitBalance()
        GridView.DataSource = TmpDt
        GridView.DataBind()

        BtnAdd.Focus()
        ModalEntry.ShowOnPageLoad = False

    End Sub

    Private Sub GridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)

            Action1.Text = "UPD"
            TxtNo.Text = SelectRecord.Cells(0).Text
            DDLAccount.Value = SelectRecord.Cells(1).Text
            TxtUraian.Text = TryCast(SelectRecord.FindControl("LblUraian"), Label).Text.Replace("<br />", Environment.NewLine)
            TxtDebet.Text = SelectRecord.Cells(4).Text
            TxtKredit.Text = SelectRecord.Cells(5).Text

            'DDLAccount.Enabled = False
            DDLAccount.Focus()
            TxtUraian.Focus()
            ModalEntry.ShowOnPageLoad = True

        ElseIf e.CommandName = "BtnDelete" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)

            TmpDt = Session("TmpDt")
            'dt.DefaultView.Sort = "regNumber DESC" 'Sort lbh dahulu baru delete, jika tidak ada akan salah delete krn sort.
            TmpDt.Rows(e.CommandArgument).Delete()
            TmpDt.AcceptChanges()

            Session("TmpDt") = TmpDt

            Call HitBalance()
            GridView.DataSource = TmpDt
            GridView.DataBind()

        End If

    End Sub

    Private Sub GridView_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).Font.Bold = True
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(3).Text = "Balance"
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub

    Private Sub HitBalance()
        Dim Debet As Decimal = 0
        Dim Kredit As Decimal = 0

        TmpDt = Session("TmpDt")

        For Each row As DataRow In TmpDt.Rows
            Debet += row("Debet")
            Kredit += row("Kredit")
        Next

        GridView.Columns(4).FooterText = Format(Debet, "N0")
        GridView.Columns(5).FooterText = Format(Kredit, "N0")
    End Sub

    Private Sub BtnSave1_Click(sender As Object, e As System.EventArgs) Handles BtnSave1.Click
        Dim NewNoJurnal As Boolean = False

        TmpDt = Session("TmpDt")

        'If TmpDt.Rows.Count = 0 Then
        '    msgBox1.alert("Belum ada inputan untuk uraian jurnal.")
        '    Exit Sub
        'End If

        If GridView.Columns(4).FooterText <> GridView.Columns(5).FooterText Then
            msgBox1.alert("Balance Debet <> Kredit.")
            Exit Sub
        End If

        If Action.Text = "NEW" Then
            Call AssignNo()
            If TxtNoJurnal.Text = "" Then
                msgBox1.alert("Failed to generate No. Jurnal.")
                Exit Sub
            End If

            Using CmdInsert As New Data.SqlClient.SqlCommand
                With CmdInsert
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO JurnalEntry " & _
                                    "(JobNo,NoJurnal,TglJurnal,PC,Site,Member,Nota,Identitas,NoReg," + _
                                    "DebetBalance,KreditBalance,UserEntry,TimeEntry,Bulan,Tahun,LedgerNo) " & _
                                    "VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16)"
                    .Parameters.AddWithValue("@P1", JobNo.Text)
                    .Parameters.AddWithValue("@P2", TxtNoJurnal.Text)
                    .Parameters.AddWithValue("@P3", TxtTglJurnal.Text)
                    .Parameters.AddWithValue("@P4", "P")
                    .Parameters.AddWithValue("@P5", TxtSite.Text)
                    .Parameters.AddWithValue("@P6", TxtMember.Text)
                    .Parameters.AddWithValue("@P7", DDLNota.Value)
                    .Parameters.AddWithValue("@P8", DDLIdentitas.Value)
                    .Parameters.AddWithValue("@P9", TxtNoReg.Text)
                    .Parameters.AddWithValue("@P10", GridView.Columns(4).FooterText)
                    .Parameters.AddWithValue("@P11", GridView.Columns(5).FooterText)
                    .Parameters.AddWithValue("@P12", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P13", Now)
                    .Parameters.AddWithValue("@P14", Format(TxtTglJurnal.Date, "MM"))
                    .Parameters.AddWithValue("@P15", Format(TxtTglJurnal.Date, "yyyy"))
                    .Parameters.AddWithValue("@P16", LedgerNo.Text)
                    .ExecuteNonQuery()
                End With
            End Using

        Else
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT TglJurnal FROM JurnalEntry WHERE JobNo=@P1 AND NoJurnal=@P2"
                    .Parameters.AddWithValue("@P1", JobNo.Text)
                    .Parameters.AddWithValue("@P2", TxtNoJurnal.Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        If Format(RsFind("TglJurnal"), "MM") <> Format(TxtTglJurnal.Date, "MM") Then
                            NewNoJurnal = True
                            TxtNoJurnal1.Text = TxtNoJurnal.Text
                            TxtNoJurnal.Text = ""
                            Call AssignNo()
                            If TxtNoJurnal.Text = "" Then
                                msgBox1.alert("Failed to generate No. Jurnal.")
                                Exit Sub
                            End If
                        End If
                    End If
                End Using
            End Using

            Dim NoJurnal As String() = TxtNoJurnal.Text.Split("/")
            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE JurnalEntry SET Identitas=@P1,NoReg=@P2,DebetBalance=@P3,KreditBalance=@P4,NoJurnal=@P5,Nota=@P6,UserEntry=@P7,TimeEntry=@P8," & _
                                   "TglJurnal=@P9,LedgerNo=@P10,Bulan=@P11,Tahun=@P12 WHERE JobNo=@P13 AND NoJurnal=@P14 AND PC='P'"
                    .Parameters.AddWithValue("@P1", DDLIdentitas.Value)
                    .Parameters.AddWithValue("@P2", TxtNoReg.Text)
                    .Parameters.AddWithValue("@P3", GridView.Columns(4).FooterText)
                    .Parameters.AddWithValue("@P4", GridView.Columns(5).FooterText)
                    .Parameters.AddWithValue("@P5", NoJurnal(0) + "/" + NoJurnal(1) + "/" + NoJurnal(2) + "/" + DDLNota.Value + "/" + NoJurnal(4))
                    .Parameters.AddWithValue("@P6", DDLNota.Value)
                    .Parameters.AddWithValue("@P7", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P8", Now)
                    .Parameters.AddWithValue("@P9", TxtTglJurnal.Text)
                    .Parameters.AddWithValue("@P10", LedgerNo.Text)
                    .Parameters.AddWithValue("@P11", Format(TxtTglJurnal.Date, "MM"))
                    .Parameters.AddWithValue("@P12", Format(TxtTglJurnal.Date, "yyyy"))
                    .Parameters.AddWithValue("@P13", JobNo.Text)
                    .Parameters.AddWithValue("@P14", If(NewNoJurnal = True, TxtNoJurnal1.Text, TxtNoJurnal.Text))
                    .ExecuteNonQuery()
                End With
            End Using

            Using CmdDelete As New Data.SqlClient.SqlCommand
                With CmdDelete
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "DELETE FROM JurnalEntry WHERE JobNo=@P1 AND NoJurnal=@P2 AND PC='C'"
                    .Parameters.AddWithValue("@P1", JobNo.Text)
                    .Parameters.AddWithValue("@P2", If(NewNoJurnal = True, TxtNoJurnal1.Text, TxtNoJurnal.Text))
                    .ExecuteNonQuery()
                End With
            End Using

            TxtNoJurnal.Text = NoJurnal(0) + "/" + NoJurnal(1) + "/" + NoJurnal(2) + "/" + DDLNota.Value + "/" + NoJurnal(4)
        End If

        For Each row As DataRow In TmpDt.Rows
            Using CmdInsert As New Data.SqlClient.SqlCommand
                With CmdInsert
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO JurnalEntry " & _
                                    "(JobNo,NoJurnal,TglJurnal,PC,Site,Member,Nota,Identitas,NoReg,AccNo,Uraian,DK,Debet,Kredit," + _
                                    "UserEntry,TimeEntry,Bulan,Tahun,LedgerNo) " & _
                                    "VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19)"
                    .Parameters.AddWithValue("@P1", JobNo.Text)
                    .Parameters.AddWithValue("@P2", TxtNoJurnal.Text)
                    .Parameters.AddWithValue("@P3", TxtTglJurnal.Text)
                    .Parameters.AddWithValue("@P4", "C")
                    .Parameters.AddWithValue("@P5", TxtSite.Text)
                    .Parameters.AddWithValue("@P6", TxtMember.Text)
                    .Parameters.AddWithValue("@P7", DDLNota.Value)
                    .Parameters.AddWithValue("@P8", DDLIdentitas.Value)
                    .Parameters.AddWithValue("@P9", TxtNoReg.Text)
                    .Parameters.AddWithValue("@P10", row("AccNo"))
                    .Parameters.AddWithValue("@P11", row("Uraian"))
                    .Parameters.AddWithValue("@P12", If(row("Debet") > 0, "D", "K"))
                    .Parameters.AddWithValue("@P13", row("Debet"))
                    .Parameters.AddWithValue("@P14", row("Kredit"))
                    .Parameters.AddWithValue("@P15", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P16", Now)
                    .Parameters.AddWithValue("@P17", Format(TxtTglJurnal.Date, "MM"))
                    .Parameters.AddWithValue("@P18", Format(TxtTglJurnal.Date, "yyyy"))
                    .Parameters.AddWithValue("@P19", LedgerNo.Text)
                    .ExecuteNonQuery()
                End With
            End Using
        Next

        BtnCancel1_Click(BtnCancel1, New EventArgs())

    End Sub

    Private Sub AssignNo()
        Dim Nmr As Integer = 0

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT TOP 1 NoJurnal FROM JurnalEntry WHERE JobNo=@P1 AND Member=@P2 AND Bulan=@P3 AND Tahun=@P4 AND PC='P' ORDER BY NoJurnal DESC"
                .Parameters.AddWithValue("@P1", JobNo.Text)
                .Parameters.AddWithValue("@P2", TxtMember.Text)
                .Parameters.AddWithValue("@P3", Format(TxtTglJurnal.Date, "MM"))
                .Parameters.AddWithValue("@P4", Format(TxtTglJurnal.Date, "yyyy"))
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    Nmr = RsFind("NoJurnal").ToString.Split("/")(1) + 1
                Else
                    Nmr = 1
                End If

                LedgerNo.Text = Nmr
                TxtNoJurnal.Text = TxtMember.Text + "/" + Format(Nmr, "000") + "/" + Format(TxtTglJurnal.Date, "MM") + "/" + DDLNota.Value + "/" + Format(TxtTglJurnal.Date, "yy")
            End Using
        End Using

    End Sub

    Private Sub DisableControls(control As System.Web.UI.Control)
        For Each c As System.Web.UI.Control In control.Controls
            If c.GetType.ToString = "DevExpress.Web.ASPxMenu" Then Continue For

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

        BtnSave1.Visible = False
        BtnCancel1.Text = "OK"
        BtnCancel1.Enabled = True

    End Sub

End Class