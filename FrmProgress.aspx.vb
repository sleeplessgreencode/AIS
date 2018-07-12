Public Class FrmProgress
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "RPPM") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Call BindJob()
            Call BindPeriode()
            Call BindGrid()
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindGrid()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Progress WHERE JobNo=@P1 ORDER BY Tahun DESC, Bulan DESC"
                .Parameters.AddWithValue("@P1", If(String.IsNullOrEmpty(DDLJob.Text) = True, String.Empty, DDLJob.Value))
            End With

            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    GridView.DataSource = DtGrid
                    GridView.DataBind()
                End Using
            End Using
        End Using

    End Sub

    Private Sub GridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM Progress WHERE JobNo=@P1 AND Tahun=@P2 AND Bulan=@P3"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", SelectRecord.Cells(0).Text)
                    .Parameters.AddWithValue("@P3", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        TxtTahun.Text = RsFind("Tahun").ToString
                        DDLBulan.Value = RsFind("Bulan").ToString
                        TxtRencanaK.Text = RsFind("RencanaK").ToString
                        TxtRealisasiK.Text = RsFind("RealisasiK").ToString
                        TxtRealisasiKeuK.Text = RsFind("RealisasiKeuK").ToString
                        TxtRencanaTB.Text = RsFind("RencanaTB").ToString
                        TxtRealisasiTB.Text = RsFind("RealisasiTB").ToString
                        TxtRealisasiKeuTB.Text = RsFind("RealisasiKeuTB").ToString
                    End If
                End Using
            End Using

            Session("RPPM") = SelectRecord.Cells(0).Text & "|" & SelectRecord.Cells(1).Text
            LblAction.Text = "UPD"
            ModalEntry.ShowOnPageLoad = True

        End If
    End Sub

    Private Sub BindJob()
        Dim AksesJob As String = String.Empty

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
        DDLJob.Items.Add(String.Empty, String.Empty)

        Using CmdIsi As New Data.SqlClient.SqlCommand
            With CmdIsi
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo,JobNm FROM Job"
            End With
            Using RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
                While RsIsi.Read
                    If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsIsi("JobNo")) >= 0 Then
                        DDLJob.Items.Add(RsIsi("JobNo") & " - " & RsIsi("JobNm"), RsIsi("JobNo"))
                    End If
                End While
            End Using
        End Using

        DDLJob.SelectedIndex = 0

    End Sub

    Private Sub BindPeriode()
        DDLBulan.Items.Clear()
        DDLBulan.Items.Add("Januari", "1")
        DDLBulan.Items.Add("Februari", "2")
        DDLBulan.Items.Add("Maret", "3")
        DDLBulan.Items.Add("April", "4")
        DDLBulan.Items.Add("Mei", "5")
        DDLBulan.Items.Add("Juni", "6")
        DDLBulan.Items.Add("Juli", "7")
        DDLBulan.Items.Add("Agustus", "8")
        DDLBulan.Items.Add("September", "9")
        DDLBulan.Items.Add("Oktober", "10")
        DDLBulan.Items.Add("November", "11")
        DDLBulan.Items.Add("Desember", "12")
    End Sub

    Private Sub GridView_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(2).Text = CharMo(DateSerial(e.Row.Cells(0).Text, e.Row.Cells(1).Text, 1))
        End If

    End Sub

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If DDLJob.Value = String.Empty Then
            msgBox1.alert("Belum pilih Job.")
            DDLJob.Focus()
            Exit Sub
        End If

        LblAction.Text = "NEW"
        TxtTahun.Text = Year(Today)
        DDLBulan.Value = Month(Today).ToString
        TxtRencanaK.Text = "0"
        TxtRealisasiK.Text = "0"
        TxtRealisasiKeuK.Text = "0"
        TxtRencanaTB.Text = "0"
        TxtRealisasiTB.Text = "0"
        TxtRealisasiKeuTB.Text = "0"

        Dim TtlTermin As Decimal = 0, TtlDIPA As Decimal = 0

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT ISNULL(SUM(TerminInduk),0) FROM TerminInduk WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    TtlTermin = RsFind(0)
                End If
            End Using
        End Using

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT ISNULL(SUM(Budget),0) FROM DIPA WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    TtlDIPA = RsFind(0)
                End If
            End Using
        End Using
        TxtRealisasiKeuK.Text = If(TtlDIPA = 0, "0.000", Format((TtlTermin / TtlDIPA) * 100, "N3"))

        TtlTermin = 0
        TtlDIPA = 0

        Dim Tahun As Integer = TxtTahun.Text

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT ISNULL(SUM(TerminInduk),0) FROM TerminInduk WHERE JobNo=@P1 AND TglCair>=@P2 AND TglCair<=@P3"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", DateSerial(Tahun, 1, 1))
                .Parameters.AddWithValue("@P3", DateSerial(Tahun, 12, 1))
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    TtlTermin = RsFind(0)
                End If
            End Using
        End Using

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT ISNULL(SUM(Budget),0) FROM DIPA WHERE JobNo=@P1 AND Tahun=@P2"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", Tahun)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    TtlDIPA = RsFind(0)
                End If
            End Using
        End Using
        TxtRealisasiKeuTB.Text = If(TtlDIPA = 0, "0.000", Format((TtlTermin / TtlDIPA) * 100, "N3"))

        ModalEntry.ShowOnPageLoad = True

    End Sub

    'Private Sub GetTotal()

    '    Dim CmdFind As New Data.SqlClient.SqlCommand
    '    With CmdFind
    '        .Connection = Conn
    '        .CommandType = CommandType.Text
    '        .CommandText = "SELECT FORMAT(SUM(Bobot),'N2') AS TtlBobot FROM RPPM WHERE JobNo=@P1"
    '        .Parameters.AddWithValue("@P1", DDLJob.Value)
    '    End With
    '    Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
    '    If RsFind.Read Then
    '        GridView.Columns(1).FooterText = RsFind(0).ToString
    '    Else
    '        GridView.Columns(1).FooterText = "0"
    '    End If
    '    RsFind.Close()
    '    CmdFind.Dispose()

    'End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If TxtTahun.Text = "0" Then
            msgBox1.alert("Tahun belum di-input.")
            TxtTahun.Focus()
            Exit Sub
        End If

        If LblAction.Text = "NEW" Then
            If CheckUnique() = False Then Exit Sub

            Using CmdInsert As New Data.SqlClient.SqlCommand
                With CmdInsert
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO Progress(JobNo,Tahun,Bulan,RencanaK,RealisasiK,RealisasiKeuK,RencanaTB,RealisasiTB,RealisasiKeuTB," & _
                                   "UserEntry,TimeEntry) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11)"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", TxtTahun.Text)
                    .Parameters.AddWithValue("@P3", DDLBulan.Value)
                    .Parameters.AddWithValue("@P4", TxtRencanaK.Text)
                    .Parameters.AddWithValue("@P5", TxtRealisasiK.Text)
                    .Parameters.AddWithValue("@P6", TxtRealisasiKeuK.Text)
                    .Parameters.AddWithValue("@P7", TxtRencanaTB.Text)
                    .Parameters.AddWithValue("@P8", TxtRealisasiTB.Text)
                    .Parameters.AddWithValue("@P9", TxtRealisasiKeuTB.Text)
                    .Parameters.AddWithValue("@P10", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P11", Now)
                    .ExecuteNonQuery()
                End With
            End Using
        Else
            If TxtTahun.Text <> Session("RPPM").ToString.Split("|")(0) Or
               DDLBulan.Value <> Session("RPPM").ToString.Split("|")(1) Then
                If CheckUnique() = False Then Exit Sub
            End If

            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE Progress SET Tahun=@P1,Bulan=@P2,RencanaK=@P3,RealisasiK=@P4,RealisasiKeuK=@P5,RencanaTB=@P6,RealisasiTB=@P7," & _
                                   "RealisasiKeuTB=@P8,UserEntry=@P9,TimeEntry=@P10 WHERE JobNo=@P11 AND Tahun=@P12 AND Bulan=@P13"
                    .Parameters.AddWithValue("@P1", TxtTahun.Text)
                    .Parameters.AddWithValue("@P2", DDLBulan.Value)
                    .Parameters.AddWithValue("@P3", TxtRencanaK.Text)
                    .Parameters.AddWithValue("@P4", TxtRealisasiK.Text)
                    .Parameters.AddWithValue("@P5", TxtRealisasiKeuK.Text)
                    .Parameters.AddWithValue("@P6", TxtRencanaTB.Text)
                    .Parameters.AddWithValue("@P7", TxtRealisasiTB.Text)
                    .Parameters.AddWithValue("@P8", TxtRealisasiKeuTB.Text)
                    .Parameters.AddWithValue("@P9", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P10", Now)
                    .Parameters.AddWithValue("@P11", DDLJob.Value)
                    .Parameters.AddWithValue("@P12", Session("RPPM").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P13", Session("RPPM").ToString.Split("|")(1))
                    .ExecuteNonQuery()
                End With
            End Using
        End If

        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As System.EventArgs) Handles BtnCancel.Click
        Session.Remove("RPPM")
        ModalEntry.ShowOnPageLoad = False
        Call BindGrid()
    End Sub

    Private Function CheckUnique() As Boolean
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Progress WHERE JobNo=@P1 AND Tahun=@P2 AND Bulan=@P3"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", TxtTahun.Text)
                .Parameters.AddWithValue("@P3", DDLBulan.Value)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    msgBox1.alert("Progress untuk JobNo " & DDLJob.Value & "\nTahun " & TxtTahun.Text & "\nBulan " & DDLBulan.Text & " sudah ada.")
                    CheckUnique = False
                    Exit Function
                End If
            End Using
        End Using

        CheckUnique = True

    End Function

    Private Sub DDLJob_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindGrid()
    End Sub

End Class