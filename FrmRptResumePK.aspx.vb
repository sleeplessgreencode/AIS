Public Class FrmRptResumePK
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
        Dim PrdAwal As Date = DateAdd("m", 0, DateSerial(Session("Print").ToString.Split("|")(2), Session("Print").ToString.Split("|")(1), 1))
        Dim PrdAkhir As Date = DateAdd("m", 1, DateSerial(Session("Print").ToString.Split("|")(2), Session("Print").ToString.Split("|")(1), 0))
        Dim Tahun As Integer = PrdAwal.Year
        Dim Bulan As Integer = PrdAwal.Month
        Dim DebetTahunKSO, KreditTahunKSO, DebetBulanKSO, KreditBulanKSO As Decimal
        Dim DebetTahunMember1, KreditTahunMember1, DebetBulanMember1, KreditBulanMember1 As Decimal
        Dim DebetTahunMember2, KreditTahunMember2, DebetBulanMember2, KreditBulanMember2 As Decimal
        Dim SaldoKSO, SaldoMember1, SaldoMember2 As Decimal
        Dim LBTBKso, LBTBMember1, LBTBMember2 As Decimal 'Laba Bersih Tahun Berjalan
        Dim IRLKso, IRLMember1, IRLMember2 As Decimal 'Summary Akun 4
        Dim IIRLKso, IIRLMember1, IIRLMember2 As Decimal 'Summary Akun 5,6,7

        Dim TmpDt1 As New DataTable()
        TmpDt1.Columns.AddRange(New DataColumn() {New DataColumn("AccNo", GetType(String)), _
                                                  New DataColumn("AccName", GetType(String)), _
                                                  New DataColumn("SaldoKSO", GetType(Decimal)), _
                                                  New DataColumn("SaldoMember1", GetType(Decimal)), _
                                                  New DataColumn("SaldoMember2", GetType(Decimal))})
        Dim CmdReport As New Data.SqlClient.SqlCommand
        With CmdReport
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM COA WHERE JobNo=@P1"
            .Parameters.AddWithValue("@P1", Trim(Session("Print").ToString.Split("|")(0)))
        End With
        Dim RsLoad As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
        While RsLoad.Read
            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM MutasiAcc WHERE JobNo=@P1 AND AccNo=@P3"
                .Parameters.AddWithValue("@P1", Trim(Session("Print").ToString.Split("|")(0)))
                .Parameters.AddWithValue("@P3", RsLoad("AccNo"))
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                If RsFind("Site") = "KSO" Then
                    If RsFind("Tahun") < Tahun Then
                        DebetTahunKSO += RsFind("Debet")
                        KreditTahunKSO += RsFind("Kredit")
                    ElseIf RsFind("Tahun") = Tahun And RsFind("Bulan") <= Bulan Then
                        DebetBulanKSO += RsFind("Debet")
                        KreditBulanKSO += RsFind("Kredit")
                    End If

                ElseIf RsFind("Site") = "Member1" Then
                    If RsFind("Tahun") < Tahun Then
                        DebetTahunMember1 += RsFind("Debet")
                        KreditTahunMember1 += RsFind("Kredit")
                    ElseIf RsFind("Tahun") = Tahun And RsFind("Bulan") <= Bulan Then
                        DebetBulanMember1 += RsFind("Debet")
                        KreditBulanMember1 += RsFind("Kredit")
                    End If

                ElseIf RsFind("Site") = "Member2" Then
                    If RsFind("Tahun") < Tahun Then
                        DebetTahunMember2 += RsFind("Debet")
                        KreditTahunMember2 += RsFind("Kredit")
                    ElseIf RsFind("Tahun") = Tahun And RsFind("Bulan") <= Bulan Then
                        DebetBulanMember2 += RsFind("Debet")
                        KreditBulanMember2 += RsFind("Kredit")
                    End If

                End If
            End While
            RsFind.Close()
            CmdFind.Dispose()

            SaldoKSO = DebetTahunKSO - KreditTahunKSO + DebetBulanKSO - KreditBulanKSO
            SaldoMember1 = DebetTahunMember1 - KreditTahunMember1 + DebetBulanMember1 - KreditBulanMember1
            SaldoMember2 = DebetTahunMember2 - KreditTahunMember2 + DebetBulanMember2 - KreditBulanMember2
            If Left(RsLoad("AccNo"), 1) = "1" Then
                LBTBKso += SaldoKSO
                LBTBMember1 += SaldoMember1
                LBTBMember2 += SaldoMember2
            ElseIf Left(RsLoad("AccNo"), 1) = "2" Then
                LBTBKso -= SaldoKSO
                LBTBMember1 -= SaldoMember1
                LBTBMember2 -= SaldoMember2
            ElseIf Left(RsLoad("AccNo"), 1) = "4" Then
                IRLKso += SaldoKSO
                IRLMember1 += SaldoMember1
                IRLMember2 += SaldoMember2
            ElseIf Left(RsLoad("AccNo"), 1) = "5" Or Left(RsLoad("AccNo"), 1) = "6" Or Left(RsLoad("AccNo"), 1) = "7" Then
                IIRLKso += SaldoKSO
                IIRLMember1 += SaldoMember1
                IIRLMember2 += SaldoMember2
            End If

            If RsLoad("AccNo") = "3100.002" Then
                TmpDt1.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), LBTBKso, LBTBMember1, LBTBMember2)
            ElseIf RsLoad("AccNo") = "8001.001" Then
                TmpDt1.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), IRLKso - IIRLKso, IRLMember1 - IIRLMember1, IRLMember2 - IIRLMember2)
            Else
                TmpDt1.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), SaldoKSO, SaldoMember1, SaldoMember2)
            End If

            DebetTahunKSO = 0
            KreditTahunKSO = 0
            DebetBulanKSO = 0
            KreditBulanKSO = 0
            SaldoKSO = 0
            DebetTahunMember1 = 0
            KreditTahunMember1 = 0
            DebetBulanMember1 = 0
            KreditBulanMember1 = 0
            SaldoMember1 = 0
            DebetTahunMember2 = 0
            KreditTahunMember2 = 0
            DebetBulanMember2 = 0
            KreditBulanMember2 = 0
            SaldoMember2 = 0

        End While

        RsLoad.Close()

        Dim NmBulan As String() = {"Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "Nopember", "Desember"}
        Dim BulanIni As String = NmBulan(PrdAwal.Month - 1)

        Dim Rpt As New RptResumePK
        With Rpt
            .SetDataSource(TmpDt1)
            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.KSO, A.Deskripsi, A.NamaPartner, B.CompanyNm FROM Job A LEFT JOIN Company B ON A.CompanyId=B.CompanyId " & _
                               "WHERE A.JobNo=@P1"
                .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                .SetParameterValue("@CompanyNm", RsFind("CompanyNm") + If(RsFind("KSO") = "1", " - " + RsFind("NamaPartner") + ", KSO", ""))
                .SetParameterValue("@JobDeskripsi", RsFind("Deskripsi"))
            End If
            RsFind.Close()
            CmdFind.Dispose()
            .SetParameterValue("@Periode", "Periode : " + BulanIni + " " + Format(PrdAkhir.Date, "yyyy"))
        End With

        CRViewer.ReportSource = Rpt
        Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, False, "")

        TmpDt1.Dispose()
        CmdReport.Dispose()

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
        Session("Report") = Trim(Session("Print").ToString.Split("|")(0))
        Session.Remove("Print")
        Response.Redirect("FrmResumePK.aspx")
    End Sub

End Class