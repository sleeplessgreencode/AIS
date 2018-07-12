Public Class FrmTermin
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "Termin") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            TabPage.ActiveTabIndex = 0
            Call BindJob()
            Call BindGrid()
            Call BindGrid1()
        End If

        If Request.Params("TerminInduk") = 1 Then
            Using CmdDelete As New Data.SqlClient.SqlCommand
                With CmdDelete
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "DELETE FROM TerminInduk WHERE LedgerNo=@P1"
                    .Parameters.AddWithValue("@P1", Session("Termin"))
                    .ExecuteNonQuery()
                End With
            End Using

            Call BindGrid()
        ElseIf Request.Params("TerminMember") = 1 Then
            Using CmdDelete As New Data.SqlClient.SqlCommand
                With CmdDelete
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "DELETE FROM TerminMember WHERE LedgerNo=@P1"
                    .Parameters.AddWithValue("@P1", Session("Termin"))
                    .ExecuteNonQuery()
                End With
            End Using

            Call BindGrid1()
        End If

    End Sub

    Private Sub BindGrid()
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Job WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    TxtBruto.Text = Format(RsFind("Bruto"), "N0")
                    TxtFisik.Text = Format(RsFind("Bruto") / 1.1, "N0")
                    TxtPPN.Text = Format((RsFind("Bruto") / 1.1) * 0.1, "N0")
                    TxtPPH.Text = Format((RsFind("Bruto") / 1.1) * 0.03, "N0")
                    TxtNetto.Text = Format(TxtBruto.Value - TxtPPN.Value - TxtPPH.Value, "N0")
                End If
            End Using
        End Using

        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM TerminInduk WHERE JobNo=@P1 ORDER BY LedgerNo DESC"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With

            Dim TtlKontrak As Decimal = 0
            Dim TtlFisik As Decimal = 0
            Dim TtlPPN As Decimal = 0
            Dim TtlPPH As Decimal = 0
            Dim TtlNetto As Decimal = 0
            Dim TtlMember1 As Decimal = 0
            Dim TtlMember2 As Decimal = 0
            Dim TtlBrutoBOQ As Decimal = 0, TtlUM As Decimal = 0, TtlRetensi As Decimal = 0
            Using RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                While RsGrid.Read
                    TtlKontrak += RsGrid("TerminInduk")
                    TtlBrutoBOQ += RsGrid("BrutoBOQ")
                    TtlUM += RsGrid("UM")
                    TtlRetensi += RsGrid("Retensi")
                    TtlFisik = TtlKontrak / 1.1
                    TtlPPN = TtlFisik * (10 / 100)
                    TtlPPH = TtlFisik * (3 / 100)
                    TtlNetto = TtlKontrak - TtlPPN - TtlPPH
                End While
            End Using

            TtlBrutoBOQ1.Text = Format(TtlBrutoBOQ, "N0")
            TtlUM1.Text = Format(TtlUM, "N0")
            TtlRetensi1.Text = Format(TtlRetensi, "N0")
            TxtBruto1.Text = Format(TtlKontrak, "N0")
            TxtFisik1.Text = Format(TtlFisik, "N0")
            TxtPPN1.Text = Format(TtlPPN, "N0")
            TxtPPH1.Text = Format(TtlPPH, "N0")
            TxtNetto1.Text = Format(TtlNetto, "N0")

            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    GridView.DataSource = DtGrid
                    GridView.DataBind()
                End Using
            End Using
        End Using

        TxtBruto2.Text = Format(TxtBruto.Value - TxtBruto1.Value, "N0")
        TxtFisik2.Text = Format(TxtFisik.Value - TxtFisik1.Value, "N0")
        TxtPPN2.Text = Format(TxtPPN.Value - TxtPPN1.Value, "N0")
        TxtPPH2.Text = Format(TxtPPH.Value - TxtPPH1.Value, "N0")
        TxtNetto2.Text = Format(TxtNetto.Value - TxtNetto1.Value, "N0")

    End Sub

    Private Sub BindGrid1()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM TerminMember WHERE JobNo=@P1 ORDER BY LedgerNo DESC"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With

            Dim TtlMember1 As Decimal = 0
            Dim TtlMember2 As Decimal = 0
            Dim TtlKSO As Decimal = 0
            Using RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                While RsGrid.Read
                    TtlMember1 += RsGrid("TerminMember1")
                    TtlMember2 += RsGrid("TerminMember2")
                    TtlKSO += RsGrid("CadanganKSO")
                End While
            End Using

            GridView1.Columns(4).FooterText = Format(TtlMember1, "N0")
            GridView1.Columns(5).FooterText = Format(TtlMember2, "N0")
            GridView1.Columns(6).FooterText = Format(TtlKSO, "N0")

            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    GridView1.DataSource = DtGrid
                    GridView1.DataBind()
                End Using
            End Using
        End Using

        Call AssignNama()

    End Sub

    Private Sub GridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
        Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)

        If e.CommandName = "BtnUpdate" Then
            If ValidasiTermin(SelectRecord.Cells(1).Text, "TerminInduk") = False Then
                MsgBox1.alert("Termin sudah diapproved.")
                Exit Sub
            End If

            Session("Termin") = SelectRecord.Cells(1).Text

            LblAction.Text = "UPD"
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM TerminInduk WHERE LedgerNo=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        DDLJenis.Value = RsFind("Jenis")
                        TxtTglCair.Date = RsFind("TglCair")
                        TxtNoBAP.Text = RsFind("NoBAP").ToString
                        TxtUraian.Text = RsFind("Uraian").ToString
                        TxtBrutoBOQ.Text = RsFind("BrutoBOQ").ToString
                        TxtUM.Text = RsFind("UM").ToString
                        TxtRetensi.Text = RsFind("Retensi").ToString
                        TxtTerminInduk.Text = RsFind("TerminInduk")
                    End If
                End Using
            End Using

            PopEntry.ShowOnPageLoad = True
        ElseIf e.CommandName = "BtnPDF" Then
            LblPath.Text = ""
            LblUraian.Text = ""
            LnkView.Visible = False

            If ValidasiTermin(SelectRecord.Cells(1).Text, "TerminInduk") = True Then
                MsgBox1.alert("Termin belum diapprove.")
                Exit Sub
            End If

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM TerminInduk WHERE LedgerNo=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        If String.IsNullOrEmpty(RsFind("PDFFile").ToString) = False Then
                            LblPath.Text = RsFind("PDFFile").ToString
                            LnkView.Visible = True
                        End If
                        Session("Termin") = SelectRecord.Cells(1).Text
                        LblUraian.Text = RsFind("Uraian").ToString
                        ModalPDF.ShowOnPageLoad = True

                    End If
                End Using
            End Using
        ElseIf e.CommandName = "BtnDelete" Then
            If ValidasiTermin(SelectRecord.Cells(1).Text, "TerminInduk") = False Then
                MsgBox1.alert("Termin sudah diapproved.")
                Exit Sub
            End If

            Session("Termin") = SelectRecord.Cells(1).Text
            MsgBox1.confirm("Konfirmasi DELETE untuk termin " & SelectRecord.Cells(4).Text & " ?", "TerminInduk")
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindJob()
        Dim AksesJob As String = ""
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT AksesJob FROM Login WHERE UserID=@P1"
                .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    AksesJob = RsFind("AksesJob")
                End If
            End Using
        End Using

        DDLJob.Items.Clear()
        DDLJob.Items.Add("Pilih salah satu", "0")
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo,JobNm FROM Job WHERE StatusJob=@P1 OR StatusJob=@P2"
                .Parameters.AddWithValue("@P1", "Pelaksanaan")
                .Parameters.AddWithValue("@P2", "Pemeliharaan")
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsFind("JobNo")) >= 0 Then
                        DDLJob.Items.Add(RsFind("JobNo") & " - " & RsFind("JobNm"), RsFind("JobNo"))
                    End If
                End While
            End Using
        End Using

        DDLJob.Value = "0"

    End Sub

    Private Sub GridView_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(3).Text = "Jumlah Penerimaan : "
            e.Row.Cells(3).Font.Bold = True

            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right

            e.Row.Cells.RemoveAt(9)
        End If
    End Sub

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If DDLJob.Value = "0" Then
            MsgBox1.alert("Belum pilih Job.")
            Exit Sub
        End If

        LblAction.Text = "NEW"
        TxtTglCair.Date = Today
        TxtNoBAP.Text = ""
        TxtUraian.Text = ""
        TxtBrutoBOQ.Text = "0"
        TxtUM.Text = "0"
        TxtRetensi.Text = "0"
        TxtTerminInduk.Text = "0"
        DDLJenis.Text = String.Empty

        PopEntry.ShowOnPageLoad = True
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If LblAction.Text = "NEW" Then
            Using CmdInsert As New Data.SqlClient.SqlCommand
                With CmdInsert
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO TerminInduk(JobNo,TglCair,NoBAP,Uraian,BrutoBOQ,UM,Retensi,TerminInduk,UserEntry,TimeEntry,Jenis) VALUES " & _
                                   "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11)"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", TxtTglCair.Date)
                    .Parameters.AddWithValue("@P3", TxtNoBAP.Text)
                    .Parameters.AddWithValue("@P4", TxtUraian.Text)
                    .Parameters.AddWithValue("@P5", TxtBrutoBOQ.Value)
                    .Parameters.AddWithValue("@P6", TxtUM.Value)
                    .Parameters.AddWithValue("@P7", TxtRetensi.Value)
                    .Parameters.AddWithValue("@P8", TxtTerminInduk.Value)
                    .Parameters.AddWithValue("@P9", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P10", Now)
                    .Parameters.AddWithValue("@P11", DDLJenis.Value)
                    .ExecuteNonQuery()
                End With
            End Using
        Else
            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE TerminInduk SET TglCair=@P1,NoBAP=@P2,Uraian=@P3,BrutoBOQ=@P4,UM=@P5,Retensi=@P6,TerminInduk=@P7," + _
                                   "UserEntry=@P8,TimeEntry=@P9,Jenis=@P10 WHERE LedgerNo=@P11"
                    .Parameters.AddWithValue("@P1", TxtTglCair.Date)
                    .Parameters.AddWithValue("@P2", TxtNoBAP.Text)
                    .Parameters.AddWithValue("@P3", TxtUraian.Text)
                    .Parameters.AddWithValue("@P4", TxtBrutoBOQ.Value)
                    .Parameters.AddWithValue("@P5", TxtUM.Value)
                    .Parameters.AddWithValue("@P6", TxtRetensi.Value)
                    .Parameters.AddWithValue("@P7", TxtTerminInduk.Value)
                    .Parameters.AddWithValue("@P8", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P9", Now)
                    .Parameters.AddWithValue("@P10", DDLJenis.Value)
                    .Parameters.AddWithValue("@P11", Session("Termin"))
                    .ExecuteNonQuery()
                End With
            End Using

        End If

        Call BindGrid()
        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As System.EventArgs) Handles BtnCancel.Click
        Session.Remove("Termin")
        PopEntry.ShowOnPageLoad = False
    End Sub

    Protected Sub DDLJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindGrid()
        Call BindGrid1()
    End Sub

    Private Sub AssignNama()
        GridView1.HeaderRow.Cells(5).Text = "Member1" & " (Rp)"
        GridView1.HeaderRow.Cells(6).Text = "Member2" & " (Rp)"

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT Member1,Member2,Own FROM Job WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    GridView1.HeaderRow.Cells(5).Text = RsFind("Member1").ToString & " (Rp)"
                    GridView1.HeaderRow.Cells(6).Text = RsFind("Member2").ToString & " (Rp)"
                    'TxtMember1.Caption = RsFind("Member1").ToString
                    'TxtMember2.Caption = RsFind("Member2").ToString
                    'TxtCadanganKSO.Caption = "Cadangan KSO "
                    'If String.IsNullOrEmpty(RsFind("Own").ToString) = False Then
                    ' TxtCadanganKSO.Caption = TxtCadanganKSO.Caption & If(RsFind("Own") = "1", TxtMember1.Caption, TxtMember2.Caption)
                    'End If
                End If
            End Using
        End Using

    End Sub

    Private Sub BtnAdd1_Click(sender As Object, e As System.EventArgs) Handles BtnAdd1.Click
        If DDLJob.Value = "0" Then
            MsgBox1.alert("Belum pilih Job.")
            Exit Sub
        End If

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT KSO FROM Job WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    If RsFind("KSO").ToString = "0" Then
                        MsgBox1.alert("Tidak bisa tambah termin member untuk job dengan tipe manajemen single.")
                        Exit Sub
                    End If
                End If
            End Using
        End Using

        LblAction.Text = "NEW"
        TxtTglCair1.Text = ""
        TxtNoBAP1.Text = ""
        TxtUraian1.Text = ""
        TxtMember1.Text = "0"
        TxtMember2.Text = "0"
        TxtCadanganKSO.Text = "0"
        TxtMember1.Caption = GridView1.HeaderRow.Cells(5).Text
        TxtMember2.Caption = GridView1.HeaderRow.Cells(6).Text

        PopEntry1.ShowOnPageLoad = True
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim SelectRecord As GridViewRow = GridView1.Rows(e.CommandArgument)

        If e.CommandName = "BtnUpdate" Then
            If ValidasiTermin(SelectRecord.Cells(1).Text, "TerminMember") = False Then
                MsgBox1.alert("Termin sudah diapproved.")
                Exit Sub
            End If
            Session("Termin") = SelectRecord.Cells(1).Text

            LblAction.Text = "UPD"
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM TerminMember WHERE LedgerNo=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        TxtTglCair1.Date = RsFind("TglCair")
                        TxtNoBAP1.Text = RsFind("NoBAP").ToString
                        TxtUraian1.Text = RsFind("Uraian").ToString
                        TxtMember1.Text = RsFind("TerminMember1")
                        TxtMember2.Text = RsFind("TerminMember2")
                        TxtCadanganKSO.Text = RsFind("CadanganKSO")
                        TxtMember1.Caption = GridView1.HeaderRow.Cells(5).Text
                        TxtMember2.Caption = GridView1.HeaderRow.Cells(6).Text
                    End If
                End Using
            End Using

            PopEntry1.ShowOnPageLoad = True
        ElseIf e.CommandName = "BtnDelete" Then
            If ValidasiTermin(SelectRecord.Cells(1).Text, "TerminMember") = False Then
                MsgBox1.alert("Termin sudah diapproved.")
                Exit Sub
            End If

            Session("Termin") = SelectRecord.Cells(1).Text
            MsgBox1.confirm("Konfirmasi DELETE untuk termin " & SelectRecord.Cells(4).Text & " ?", "TerminMember")
        End If
    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(3).Text = "Jumlah Penerimaan : "
            e.Row.Cells(3).Font.Bold = True

            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right

            e.Row.Cells.RemoveAt(7)
        End If
    End Sub

    Private Sub BtnSave1_Click(sender As Object, e As System.EventArgs) Handles BtnSave1.Click
        If LblAction.Text = "NEW" Then
            Using CmdInsert As New Data.SqlClient.SqlCommand
                With CmdInsert
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO TerminMember(JobNo,TglCair,NoBAP,Uraian,TerminMember1,TerminMember2,CadanganKSO,UserEntry,TimeEntry) VALUES " & _
                                   "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", TxtTglCair1.Date)
                    .Parameters.AddWithValue("@P3", TxtNoBAP1.Text)
                    .Parameters.AddWithValue("@P4", TxtUraian1.Text)
                    .Parameters.AddWithValue("@P5", TxtMember1.Value)
                    .Parameters.AddWithValue("@P6", TxtMember2.Value)
                    .Parameters.AddWithValue("@P7", TxtCadanganKSO.Value)
                    .Parameters.AddWithValue("@P8", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P9", Now)
                    .ExecuteNonQuery()
                End With
            End Using
        Else
            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE TerminMember SET TglCair=@P1,NoBAP=@P2,Uraian=@P3,TerminMember1=@P4," + _
                                   "TerminMember2=@P5,CadanganKSO=@P6,UserEntry=@P7,TimeEntry=@P8 WHERE LedgerNo=@P9"
                    .Parameters.AddWithValue("@P1", TxtTglCair1.Date)
                    .Parameters.AddWithValue("@P2", TxtNoBAP1.Text)
                    .Parameters.AddWithValue("@P3", TxtUraian1.Text)
                    .Parameters.AddWithValue("@P4", TxtMember1.Value)
                    .Parameters.AddWithValue("@P5", TxtMember2.Value)
                    .Parameters.AddWithValue("@P6", TxtCadanganKSO.Value)
                    .Parameters.AddWithValue("@P7", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P8", Now)
                    .Parameters.AddWithValue("@P9", Session("Termin"))
                    .ExecuteNonQuery()
                End With
            End Using
        End If

        Call BindGrid1()
        BtnCancel1_Click(BtnCancel1, New EventArgs())
    End Sub

    Private Sub BtnCancel1_Click(sender As Object, e As System.EventArgs) Handles BtnCancel1.Click
        Session.Remove("Termin")
        PopEntry1.ShowOnPageLoad = False
    End Sub

    Private Sub TxtBrutoBOQ_NumberChanged(sender As Object, e As System.EventArgs) Handles TxtBrutoBOQ.NumberChanged
        Call HitungNettBOQ()
        TxtUM.Focus()
    End Sub

    Private Sub HitungNettBOQ()
        TxtTerminInduk.Text = TxtBrutoBOQ.Value - TxtUM.Value - TxtRetensi.Value
    End Sub

    Private Sub TxtUM_NumberChanged(sender As Object, e As System.EventArgs) Handles TxtUM.NumberChanged
        Call HitungNettBOQ()
        TxtRetensi.Focus()
    End Sub

    Private Sub TxtRetensi_NumberChanged(sender As Object, e As System.EventArgs) Handles TxtRetensi.NumberChanged
        Call HitungNettBOQ()
        TxtTerminInduk.Focus()
    End Sub

    Protected Sub View(sender As Object, e As EventArgs)
        Session("ViewPDF") = Mid(LblPath.Text, 3, Len(LblPath.Text) - 2)

        With DialogWindow1            
            .TargetUrl = "FrmViewPDF.aspx"
            .Open()
        End With

    End Sub

    Private Function ValidasiTermin(ByVal LedgerNo As Integer, Type As String) As Boolean
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM " & Type & " WHERE LedgerNo=@P1"
                .Parameters.AddWithValue("@P1", LedgerNo)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    If IsDBNull(RsFind("TimeApproval")) = False Then
                        Return False
                    End If
                End If
            End Using
        End Using

        Return True
    End Function

    Private Sub BtnSavePDF_Click(sender As Object, e As System.EventArgs) Handles BtnSavePDF.Click
        If Not PDFUpload.HasFile = True Then
            ModalPDF.ShowOnPageLoad = False
            Exit Sub
        End If

        If PDFUpload.HasFile And PDFUpload.PostedFile.ContentType.ToLower <> "application/pdf" Then
            MsgBox1.alert("File yang di-upload bukan PDF.")
            PDFUpload.Focus()
            Exit Sub
        End If

        Using CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE TerminInduk SET " & _
                                "PDFFile=@P1,UserEntry=@P2,TimeEntry=@P3 " & _
                                "WHERE LedgerNo=@P4"
                If System.IO.File.Exists(Server.MapPath("~/PDF/TERMIN/") + System.IO.Path.GetFileName(LblPath.Text)) Then
                    System.IO.File.Delete(Server.MapPath("~/PDF/TERMIN/") + System.IO.Path.GetFileName(LblPath.Text))
                End If
                Dim fileName As String = Format(Now, "yyyy-MM-dd") & "_" & Format(Now, "hhmmss") & "_" & _
                    System.IO.Path.GetFileName(PDFUpload.PostedFile.FileName)
                PDFUpload.PostedFile.SaveAs(Server.MapPath("~/PDF/TERMIN/") + fileName)
                .Parameters.AddWithValue("@P1", "~/PDF/TERMIN/" + fileName)
                .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P3", Now)
                .Parameters.AddWithValue("@P4", Session("Termin"))
                .ExecuteNonQuery()
            End With
        End Using

        ModalPDF.ShowOnPageLoad = False
    End Sub

    Private Sub BtnClearPDF_Click(sender As Object, e As System.EventArgs) Handles BtnClearPDF.Click
        If System.IO.File.Exists(Server.MapPath("~/PDF/TERMIN/") & System.IO.Path.GetFileName(LblPath.Text)) Then
            System.IO.File.Delete(Server.MapPath("~/PDF/TERMIN/") & System.IO.Path.GetFileName(LblPath.Text))
        End If

        Using CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE TerminInduk SET " & _
                                "PDFFile=@P1,UserEntry=@P2,TimeEntry=@P3 " & _
                                "WHERE LedgerNo=@P4"
                .Parameters.AddWithValue("@P1", DBNull.Value)
                .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P3", Now)
                .Parameters.AddWithValue("@P4", Session("Termin"))
                .ExecuteNonQuery()
            End With
        End Using

        ModalPDF.ShowOnPageLoad = False
    End Sub

End Class