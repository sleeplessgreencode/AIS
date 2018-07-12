Public Class FrmRptRekapPPD
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If CheckAkses() = False Then
            Response.Redirect("Default.aspx")
            Exit Sub
        End If

        Call BindReport()

    End Sub

    Private Sub BindReport()
        Dim TmpDt1 As New DataTable()
        TmpDt1.Columns.AddRange(New DataColumn() {New DataColumn("Alokasi", GetType(String)), _
                                                  New DataColumn("TotalRAP", GetType(Decimal)), _
                                                  New DataColumn("PJLastWeek", GetType(Decimal)), _
                                                  New DataColumn("PJCurrentWeek", GetType(Decimal))})

        Dim PrdAwal As Date = Session("Print").ToString.Split("|")(1)
        Dim PrdAkhir As Date = Session("Print").ToString.Split("|")(2)
        Dim Rpt As New RptRekapPPD

        'Create Tmp Data Alokasi
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT Alokasi FROM Alokasi"
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        While RsFind.Read
            TmpDt1.Rows.Add(RsFind(0), 0, 0, 0)
        End While
        RsFind.Close()
        CmdFind.Dispose()

        'Hitung Total RAP per Alokasi
        Dim CmdFind1 As New Data.SqlClient.SqlCommand
        With CmdFind1
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT Alokasi, SUM(Vol*HrgSatuan) FROM RAP WHERE JobNo=@P1 AND Tipe='Detail' GROUP BY Alokasi"
            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
        End With
        Dim RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
        While RsFind1.Read
            Dim result As DataRow = TmpDt1.Select("Alokasi='" & RsFind1(0) & "'").FirstOrDefault
            If result IsNot Nothing Then
                result("TotalRAP") = RsFind1(1)
            End If
        End While
        RsFind1.Close()
        CmdFind1.Dispose()

        'Hitung Total PJ s.d. Minggu lalu per Alokasi
        Dim CmdFind2 As New Data.SqlClient.SqlCommand
        With CmdFind2
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT Alokasi, SUM(TotalPJ) FROM PdHdr WHERE JobNo=@P1 AND TglPJ<=@P2 AND PjApprovedByKK IS NOT NULL GROUP BY Alokasi"
            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P2", FindMondayDate(PrdAwal).AddDays(-1))
        End With
        Dim RsFind2 As Data.SqlClient.SqlDataReader = CmdFind2.ExecuteReader
        While RsFind2.Read
            Dim result As DataRow = TmpDt1.Select("Alokasi='" & RsFind2(0) & "'").FirstOrDefault
            If result IsNot Nothing Then
                result("PJLastWeek") = RsFind2(1)
            End If
        End While
        RsFind2.Close()
        CmdFind2.Dispose()

        'Hitung Total PJ Minggu ini per Alokasi
        Dim CmdFind3 As New Data.SqlClient.SqlCommand
        With CmdFind3
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT Alokasi, SUM(TotalPJ) FROM PdHdr WHERE JobNo=@P1 AND TglPJ BETWEEN @P2 AND @P3 AND PjApprovedByKK IS NOT NULL GROUP BY Alokasi"
            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P2", PrdAwal)
            .Parameters.AddWithValue("@P3", PrdAkhir)
        End With
        Dim RsFind3 As Data.SqlClient.SqlDataReader = CmdFind3.ExecuteReader
        While RsFind3.Read
            Dim result As DataRow = TmpDt1.Select("Alokasi='" & RsFind3(0) & "'").FirstOrDefault
            If result IsNot Nothing Then
                result("PJCurrentWeek") = RsFind3(1)
            End If
        End While
        RsFind3.Close()
        CmdFind3.Dispose()

        'Hitung Total Progress Fisik
        Dim RPPMLastWeek As Decimal = 0
        Dim RPPMCurrentWeek As Decimal = 0
        Dim LastWeek As Integer = FindWeekNumber(PrdAwal) - 1
        Dim CurrentWeek As Integer = FindWeekNumber(PrdAwal)

        Dim CmdFind4 As New Data.SqlClient.SqlCommand
        With CmdFind4
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT Minggu, Bobot FROM RPPM WHERE JobNo=@P1 AND Minggu<=@P2"
            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P2", CurrentWeek)
        End With
        Dim RsFind4 As Data.SqlClient.SqlDataReader = CmdFind4.ExecuteReader
        While RsFind4.Read
            If RsFind4("Minggu") <= LastWeek Then RPPMLastWeek += RsFind4("Bobot")
            If RsFind4("Minggu") = CurrentWeek Then RPPMCurrentWeek += RsFind4("Bobot")
        End While
        RsFind4.Close()
        CmdFind4.Dispose()

        'Hitung Total Kontrak/PO
        Dim KOLastWeek As Decimal = 0
        Dim KOCurrentWeek As Decimal = 0
        Dim CmdFind5 As New Data.SqlClient.SqlCommand
        With CmdFind5
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TglKO, SubTotal, DiscAmount, PPN FROM KoHdr WHERE JobNo=@P1 AND TglKO<=@P2 AND ApprovedBy IS NOT NULL"
            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P2", PrdAkhir)
        End With
        Dim RsFind5 As Data.SqlClient.SqlDataReader = CmdFind5.ExecuteReader
        While RsFind5.Read
            If RsFind5("TglKO") <= FindMondayDate(PrdAwal).AddDays(-1) Then KOLastWeek = KOLastWeek + (RsFind5("SubTotal") - RsFind5("DiscAmount") + RsFind5("PPN"))
            If RsFind5("TglKO") >= PrdAwal And RsFind5("TglKO") <= PrdAkhir Then KOCurrentWeek = KOCurrentWeek + (RsFind5("SubTotal") - RsFind5("DiscAmount") + RsFind5("PPN"))
        End While
        RsFind5.Close()
        CmdFind5.Dispose()

        Dim PersenKOLastWeek As Decimal = 0
        Dim PersenKOCurrentWeek As Decimal = 0
        Dim result1 As DataRow = TmpDt1.Select("Alokasi='B'").FirstOrDefault
        If result1 IsNot Nothing Then
            If result1("TotalRAP") > 0 Then
                PersenKOLastWeek = (KOLastWeek / result1("TotalRAP")) * 100
                PersenKOCurrentWeek = (KOCurrentWeek / result1("TotalRAP")) * 100
            End If
        End If

        'Hitung Total Termin Induk (Nett)
        Dim TerminIndukLastWeek As Decimal = 0
        Dim TerminIndukCurrentWeek As Decimal = 0
        Dim CmdFind6 As New Data.SqlClient.SqlCommand
        With CmdFind6
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TglTermin, TotalTermin FROM Termin WHERE JobNo=@P1 AND TglTermin<=@P2"
            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P2", PrdAkhir)
        End With
        Dim RsFind6 As Data.SqlClient.SqlDataReader = CmdFind6.ExecuteReader
        While RsFind6.Read
            If RsFind6("TglTermin") <= FindMondayDate(PrdAwal).AddDays(-1) Then _
                TerminIndukLastWeek = TerminIndukLastWeek + (RsFind6("TotalTermin") - (RsFind6("TotalTermin") / 1.1) * (10 / 100) - (RsFind6("TotalTermin") / 1.1) * (3 / 100))
            If RsFind6("TglTermin") >= PrdAwal And RsFind6("TglTermin") <= PrdAkhir Then _
                TerminIndukCurrentWeek = TerminIndukCurrentWeek + (RsFind6("TotalTermin") - (RsFind6("TotalTermin") / 1.1) * (10 / 100) - (RsFind6("TotalTermin") / 1.1) * (3 / 100))
        End While
        RsFind6.Close()
        CmdFind6.Dispose()

        'Hitung Total Termin Member
        Dim TerminMemberLastWeek As Decimal = 0
        Dim TerminMemberCurrentWeek As Decimal = 0
        Dim CmdFind7 As New Data.SqlClient.SqlCommand
        With CmdFind7
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TglShare, ShareMember2 FROM ShareTermin WHERE JobNo=@P1 AND TglShare<=@P2"
            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P2", PrdAkhir)
        End With
        Dim RsFind7 As Data.SqlClient.SqlDataReader = CmdFind7.ExecuteReader
        While RsFind7.Read
            If RsFind7("TglShare") <= FindMondayDate(PrdAwal).AddDays(-1) Then _
                TerminMemberLastWeek += RsFind7("ShareMember2")
            If RsFind7("TglShare") >= PrdAwal And RsFind7("TglShare") <= PrdAkhir Then _
                TerminMemberCurrentWeek += RsFind7("ShareMember2")
        End While
        RsFind7.Close()
        CmdFind7.Dispose()

        'Saldo Terkini Induk
        Dim TglSaldoInduk As Date = Nothing
        Dim NominalInduk As Decimal = 0
        Dim CmdFind8 As New Data.SqlClient.SqlCommand
        With CmdFind8
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TOP 1 TglSaldo, Nominal FROM Saldo WHERE JobNo=@P1 AND TglSaldo<=@P2 AND Tipe='Induk' ORDER BY TglSaldo DESC"
            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P2", PrdAkhir)
        End With
        Dim RsFind8 As Data.SqlClient.SqlDataReader = CmdFind8.ExecuteReader
        If RsFind8.Read Then
            TglSaldoInduk = RsFind8("TglSaldo")
            NominalInduk = RsFind8("Nominal")
        End If
        RsFind8.Close()
        CmdFind8.Dispose()

        'Saldo Terkini Member
        Dim TglSaldoMember As Date = Nothing
        Dim NominalMember As Decimal = 0
        Dim CmdFind9 As New Data.SqlClient.SqlCommand
        With CmdFind9
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TOP 1 TglSaldo, Nominal FROM Saldo WHERE JobNo=@P1 AND TglSaldo<=@P2 AND Tipe='Member' ORDER BY TglSaldo DESC"
            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P2", PrdAkhir)
        End With
        Dim RsFind9 As Data.SqlClient.SqlDataReader = CmdFind9.ExecuteReader
        If RsFind9.Read Then
            TglSaldoMember = RsFind9("TglSaldo")
            NominalMember = RsFind9("Nominal")
        End If
        RsFind9.Close()
        CmdFind9.Dispose()

        With Rpt
            .SetDataSource(TmpDt1)
            .SetParameterValue("@RPPMLastWeek", RPPMLastWeek)
            .SetParameterValue("@RPPMCurrentWeek", RPPMCurrentWeek)
            .SetParameterValue("@KOLastWeek", KOLastWeek)
            .SetParameterValue("@KOCurrentWeek", KOCurrentWeek)
            .SetParameterValue("@TerminIndukLastWeek", TerminIndukLastWeek)
            .SetParameterValue("@TerminIndukCurrentWeek", TerminIndukCurrentWeek)
            .SetParameterValue("@TerminMemberLastWeek", TerminMemberLastWeek)
            .SetParameterValue("@TerminMemberCurrentWeek", TerminMemberCurrentWeek)
            .SetParameterValue("@PersenKOLastWeek", PersenKOLastWeek)
            .SetParameterValue("@PersenKOCurrentWeek", PersenKOCurrentWeek)
            .SetParameterValue("@TglSaldoInduk", TglSaldoInduk)
            .SetParameterValue("@TglSaldoMember", TglSaldoMember)
            .SetParameterValue("@NominalInduk", NominalInduk)
            .SetParameterValue("@NominalMember", NominalMember)
        End With

        Dim CmdFindx As New Data.SqlClient.SqlCommand
        With CmdFindx
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Job WHERE JobNo=@P1"
            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
        End With
        Dim RsFindx As Data.SqlClient.SqlDataReader = CmdFindx.ExecuteReader
        If RsFindx.Read Then
            Rpt.SetParameterValue("@JobNm", RsFindx("JobNo") + " - " + RsFindx("JobNm"))
            Rpt.SetParameterValue("@Periode", Format(PrdAwal.Date, "dd-MMM-yyyy") & " s.d. " & Format(PrdAkhir.Date, "dd-MMM-yyyy"))
            Rpt.SetParameterValue("@PrdAwal", RsFindx("PrdAwal"))
            Rpt.SetParameterValue("@PrdAkhir", RsFindx("PrdAkhir"))
            Rpt.SetParameterValue("@Minggu", RsFindx("Minggu").ToString + " Minggu")
            Rpt.SetParameterValue("@Netto", RsFindx("Netto"))
            Rpt.SetParameterValue("@PersenKSO", If(RsFindx("KSO") = "0", 100, RsFindx("PersenKSO")))
            Rpt.SetParameterValue("@NoRekInduk", RsFindx("BankInduk").ToString + " - " + RsFindx("NoRekInduk").ToString)
            Rpt.SetParameterValue("@NoRekMember", RsFindx("BankMember").ToString + " - " + RsFindx("NoRekMember").ToString)            
        End If
        RsFindx.Close()
        CmdFindx.Dispose()

        CRViewer.ReportSource = Rpt

        Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, False, "")

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Function CheckAkses() As Boolean
        If Session("User") = "" Then
            Return False
        Else
            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT AksesMenu FROM Login WHERE UserID=@P1"
                .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
            End With
            Dim AksesMenu As String = ""
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                AksesMenu = RsFind("AksesMenu").ToString
            End If
            RsFind.Close()
            CmdFind.Dispose()

            If AksesMenu = "*" Then Return True
            If Array.IndexOf(AksesMenu.Split(","), _
                HttpContext.Current.Request.Url.AbsolutePath.Split("/")(1).Split(".")(0)) < 0 Then
                Return False
            End If
        End If

        Return True
    End Function

    Protected Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Session("Job") = Session("Print").ToString.Split("|")(0)
        Session.Remove("Print")
        Response.Redirect("FrmRekapPPD.aspx")
    End Sub

    Private Function FindMondayDate(Tglv As Date) As Date 'Function untuk cari tanggal hari senin untuk parameter input Tglv
        If Tglv.DayOfWeek = DayOfWeek.Sunday Then Tglv = Tglv.AddDays(-1)
        Dim MondayDate As DateTime
        MondayDate = Tglv.AddDays(DayOfWeek.Monday - Tglv.DayOfWeek)

        Return MondayDate

    End Function

    Private Function FindWeekNumber(Tglv As Date) As Integer
        'Function untuk cari minggu ke berapa untuk parameter input Tglv; convert masing2 tgl ke hari senin
        '1 minggu = Senin s.d. Minggu    

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT PrdAwal FROM Job WHERE JobNo=@P1"
            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            If IsDBNull(RsFind("PrdAwal")) Then
                Return 0
            Else
                Return DateDiff(DateInterval.Weekday, FindMondayDate(RsFind("PrdAwal")), FindMondayDate(Tglv), Microsoft.VisualBasic.FirstDayOfWeek.Monday) + 1
            End If
        Else
            Return 0
        End If
        RsFind.Close()
        CmdFind.Dispose()

    End Function
End Class