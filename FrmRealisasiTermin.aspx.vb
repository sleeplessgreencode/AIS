Public Class FrmRealisasiTermin
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim Rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "RealisasiTermin") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Rpt.Close()
            Rpt.Dispose()
            Session.Remove("RptRealisasiTermin")
            PrdAwal.Date = DateSerial(Year(Today), Month(Today), 1)
            PrdAkhir.Date = DateSerial(Year(Today), Month(Today), Day(Today))
        Else
            CRViewer.ReportSource = Session("RptRealisasiTermin")
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        If Year(PrdAwal.Date) <> Year(PrdAkhir.Date) Then
            msgBox1.alert("Tahun periode harus sama.")
            PrdAkhir.Focus()
            Exit Sub
        End If

        Call BindReport()
    End Sub

    Private Sub BindReport()
        Dim DIPAInduk As Decimal = 0, DIPAMember As Decimal = 0, TerminInduk As Decimal = 0, TerminMember As Decimal = 0
        Dim DIPAIndukAll As Decimal = 0, DIPAMemberAll As Decimal = 0, TerminIndukAll As Decimal = 0, TerminMemberAll As Decimal = 0
        Dim PersenKSO As Decimal = 0

        Dim TmpDt As New DataTable()
        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("JobNo", GetType(String)), _
                                                 New DataColumn("JobNm", GetType(String)), _
                                                 New DataColumn("Kategori", GetType(String)), _
                                                 New DataColumn("NettKontrak", GetType(Decimal)), _
                                                 New DataColumn("TipeManajerial", GetType(String)), _
                                                 New DataColumn("PersenShare", GetType(Decimal)), _
                                                 New DataColumn("NominalShare", GetType(Decimal)), _
                                                 New DataColumn("DIPAInduk", GetType(Decimal)), _
                                                 New DataColumn("DIPAMember", GetType(Decimal)), _
                                                 New DataColumn("RealisasiInduk", GetType(Decimal)), _
                                                 New DataColumn("RealisasiMember", GetType(Decimal)), _
                                                 New DataColumn("DIPAIndukAll", GetType(Decimal)), _
                                                 New DataColumn("DIPAMemberAll", GetType(Decimal)), _
                                                 New DataColumn("RealisasiIndukAll", GetType(Decimal)), _
                                                 New DataColumn("RealisasiMemberAll", GetType(Decimal))})

        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Job WHERE StatusJob='Pelaksanaan' AND LEN(JobNo)='4'"
            End With
            Using RsReport As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                While RsReport.Read
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT ISNULL(SUM(Budget),0) FROM DIPA WHERE JobNo=@P1 AND Tahun=@P2"
                            .Parameters.AddWithValue("@P1", RsReport("JobNo"))
                            .Parameters.AddWithValue("@P2", Year(PrdAkhir.Date))
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            If RsFind.Read Then
                                DIPAInduk = (RsFind(0) / 1.1) * 0.97
                                DIPAMember = ((RsFind(0) / 1.1) * 0.97) * If(RsReport("PersenKSO") = "0", "1", RsReport("PersenKSO") / 100)
                            End If
                        End Using
                    End Using
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT ISNULL(SUM(TerminInduk),0) FROM TerminInduk WHERE JobNo=@P1 AND TglCair>=@P2 AND TglCair<=@P3"
                            .Parameters.AddWithValue("@P1", RsReport("JobNo"))
                            .Parameters.AddWithValue("@P2", PrdAwal.Date)
                            .Parameters.AddWithValue("@P3", PrdAkhir.Date)
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            If RsFind.Read Then
                                TerminInduk = (RsFind(0) / 1.1) * 0.97
                            End If
                        End Using
                    End Using
                    If RsReport("KSO") = "1" And RsReport("TipeManajerial") = "JO Partial" Then
                        Using CmdFind As New Data.SqlClient.SqlCommand
                            With CmdFind
                                .Connection = Conn
                                .CommandType = CommandType.Text
                                If RsReport("Own") = "1" Then
                                    .CommandText = "SELECT ISNULL(SUM(TerminMember1 + CadanganKSO),0) FROM TerminMember WHERE JobNo=@P1 AND TglCair>=@P2 AND TglCair<=@P3"
                                ElseIf RsReport("Own") = "2" Then
                                    .CommandText = "SELECT ISNULL(SUM(TerminMember2 + CadanganKSO),0) FROM TerminMember WHERE JobNo=@P1 AND TglCair>=@P2 AND TglCair<=@P3"
                                End If
                                .Parameters.AddWithValue("@P1", RsReport("JobNo"))
                                .Parameters.AddWithValue("@P2", PrdAwal.Date)
                                .Parameters.AddWithValue("@P3", PrdAkhir.Date)
                            End With
                            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                                If RsFind.Read Then
                                    TerminMember = RsFind(0)
                                End If
                            End Using
                        End Using
                    Else
                        TerminMember = TerminInduk
                    End If
                    'All DIPA & Realisasi
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT ISNULL(SUM(Budget),0) FROM DIPA WHERE JobNo=@P1"
                            .Parameters.AddWithValue("@P1", RsReport("JobNo"))
                            .Parameters.AddWithValue("@P2", Year(PrdAkhir.Date))
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            If RsFind.Read Then
                                DIPAIndukAll = (RsFind(0) / 1.1) * 0.97
                                DIPAMemberAll = ((RsFind(0) / 1.1) * 0.97) * If(RsReport("PersenKSO") = "0", "1", RsReport("PersenKSO") / 100)
                            End If
                        End Using
                    End Using
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT ISNULL(SUM(TerminInduk),0) FROM TerminInduk WHERE JobNo=@P1"
                            .Parameters.AddWithValue("@P1", RsReport("JobNo"))
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            If RsFind.Read Then
                                TerminIndukAll = (RsFind(0) / 1.1) * 0.97
                            End If
                        End Using
                    End Using
                    If RsReport("KSO") = "1" And RsReport("TipeManajerial") = "JO Partial" Then
                        Using CmdFind As New Data.SqlClient.SqlCommand
                            With CmdFind
                                .Connection = Conn
                                .CommandType = CommandType.Text
                                If RsReport("Own") = "1" Then
                                    .CommandText = "SELECT ISNULL(SUM(TerminMember1 + CadanganKSO),0) FROM TerminMember WHERE JobNo=@P1"
                                ElseIf RsReport("Own") = "2" Then
                                    .CommandText = "SELECT ISNULL(SUM(TerminMember2 + CadanganKSO),0) FROM TerminMember WHERE JobNo=@P1"
                                End If
                                .Parameters.AddWithValue("@P1", RsReport("JobNo"))
                            End With
                            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                                If RsFind.Read Then
                                    TerminMemberAll = RsFind(0)
                                End If
                            End Using
                        End Using
                    Else
                        TerminMemberAll = TerminIndukAll
                    End If
                    PersenKSO = If(RsReport("PersenKSO") = 0, 100, RsReport("PersenKSO"))
                    TmpDt.Rows.Add(RsReport("JobNo"), RsReport("JobNm"), RsReport("Kategori"), RsReport("Netto") / 1000, RsReport("TipeManajerial"), _
                                   PersenKSO, (RsReport("Netto") * PersenKSO / 100) / 1000, DIPAInduk / 1000, DIPAMember / 1000, _
                                   TerminInduk / 1000, TerminMember / 1000, DIPAIndukAll / 1000, DIPAMemberAll / 1000, TerminIndukAll / 1000, _
                                   TerminMemberAll / 1000)

                    DIPAInduk = 0
                    DIPAMember = 0
                    TerminInduk = 0
                    TerminMember = 0
                    DIPAIndukAll = 0
                    DIPAMemberAll = 0
                    TerminIndukAll = 0
                    TerminMemberAll = 0
                    PersenKSO = 0

                End While
            End Using
        End Using

        Rpt.Load(Server.MapPath("~/Report/RptTermin.rpt"))
        With Rpt
            .SetDataSource(TmpDt)
            .SetParameterValue("@Periode", Format(PrdAwal.Date, "dd-MMM-yyyy") & " s.d. " & Format(PrdAkhir.Date, "dd-MMM-yyyy"))
            .SetParameterValue("@PrintInfo", "Printed On " & Format(Now, "dd-MMM-yyyy HH:mm") & " By " & Session("User").ToString.Split("|")(0))
            .SetParameterValue("@ColumnDIPA1", "Netto DIPA Tahun " & Year(PrdAkhir.Date))
            .SetParameterValue("@ColumnDIPA2", "Netto Realisasi Termin " & Year(PrdAkhir.Date))
            .SetParameterValue("@ColumnDIPA3", "Sisa Netto Termin " & Year(PrdAkhir.Date))
        End With

        Session("RptRealisasiTermin") = Rpt

        With CRViewer
            .ReportSource = Rpt
            .Zoom(100)
        End With

        TmpDt.Dispose()

    End Sub

End Class