Public Class FrmRptLK
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "LapKeu") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Call BindReport()
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindReport()
        Dim TmpAktiva As New DataTable()
        Dim TmpPasiva As New DataTable()

        TmpAktiva.Clear()
        TmpPasiva.Clear()

        TmpAktiva.Columns.AddRange(New DataColumn() {New DataColumn("AccNo", GetType(String)), _
                                                     New DataColumn("AccName", GetType(String)), _
                                                     New DataColumn("Amount", GetType(Decimal))})
        TmpPasiva.Columns.AddRange(New DataColumn() {New DataColumn("AccNo", GetType(String)), _
                                                     New DataColumn("AccName", GetType(String)), _
                                                     New DataColumn("Amount", GetType(Decimal))})

        Dim JobNo As String = Session("Data").ToString.Split("|")(0)
        Dim Site As String = Session("Data").ToString.Split("|")(1)
        Dim PrdAwal As Date = Session("Data").ToString.Split("|")(2)
        Dim PrdAkhir As Date = Session("Data").ToString.Split("|")(3)
        Dim Tahun As Integer = PrdAwal.Year
        Dim Bulan As Integer = PrdAwal.Month
        Dim DebetTahunKSO As Decimal, KreditTahunKSO As Decimal, DebetBulanKSO As Decimal, KreditBulanKSO As Decimal
        Dim DebetTahunMember1 As Decimal, KreditTahunMember1 As Decimal, DebetBulanMember1 As Decimal, KreditBulanMember1 As Decimal
        Dim DebetTahunMember2 As Decimal, KreditTahunMember2 As Decimal, DebetBulanMember2 As Decimal, KreditBulanMember2 As Decimal
        Dim SaldoKSO As Decimal, SaldoMember1 As Decimal, SaldoMember2 As Decimal
        Dim LBTBKso As Decimal, LBTBMember1 As Decimal, LBTBMember2 As Decimal 'Laba Bersih Tahun Berjalan
        Dim TtlAktiva As Decimal, TtlPasiva As Decimal

        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM COA WHERE LEFT(Accno,1) < '4'"
            End With
            Using RsLoad As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                While RsLoad.Read
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT * FROM JurnalEntry WHERE JobNo=@P1 AND AccNo=@P2"
                            .Parameters.AddWithValue("@P1", JobNo)
                            .Parameters.AddWithValue("@P2", RsLoad("AccNo"))
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
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
                        End Using
                    End Using

                    If Left(RsLoad("AccNo"), 1) = "2" Or Left(RsLoad("AccNo"), 1) = "3" Then
                        SaldoKSO = KreditTahunKSO - DebetTahunKSO + KreditBulanKSO - DebetBulanKSO
                        SaldoMember1 = KreditTahunMember1 - DebetTahunMember1 + KreditBulanMember1 - DebetBulanMember1
                        If RsLoad("AccNo") <> "3100.002" Then
                            SaldoMember2 = KreditTahunMember2 - DebetTahunMember2 + KreditBulanMember2 - DebetBulanMember2
                        End If
                    Else
                        SaldoKSO = DebetTahunKSO - KreditTahunKSO + DebetBulanKSO - KreditBulanKSO
                        SaldoMember1 = DebetTahunMember1 - KreditTahunMember1 + DebetBulanMember1 - KreditBulanMember1
                        SaldoMember2 = DebetTahunMember2 - KreditTahunMember2 + DebetBulanMember2 - KreditBulanMember2
                    End If

                    If Left(RsLoad("AccNo"), 1) = "1" Then
                        LBTBKso += SaldoKSO
                        LBTBMember1 += SaldoMember1
                        LBTBMember2 += SaldoMember2
                    ElseIf Left(RsLoad("AccNo"), 1) = "2" Or Left(RsLoad("AccNo"), 1) = "3" Then
                        LBTBKso -= SaldoKSO
                        LBTBMember1 -= SaldoMember1
                        LBTBMember2 -= SaldoMember2
                    End If

                    If RsLoad("AccNo") = "3100.002" Then
                        If Site = "KSO" Then
                            TmpPasiva.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), LBTBKso)
                            TtlPasiva += LBTBKso
                        ElseIf Site = "Member1" Then
                            TmpPasiva.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), LBTBMember1)
                            TtlPasiva += LBTBMember1
                        ElseIf Site = "Member2" Then
                            TmpPasiva.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), LBTBMember2)
                            TtlPasiva += LBTBMember2
                        Else
                            TmpPasiva.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), LBTBKso + LBTBMember1 + LBTBMember2)
                            TtlPasiva += LBTBKso + LBTBMember1 + LBTBMember2
                        End If
                    Else
                        If Left(RsLoad("AccNo"), 1) = "2" Or Left(RsLoad("AccNo"), 1) = "3" Then
                            If Site = "KSO" Then
                                TmpPasiva.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), SaldoKSO)
                                TtlPasiva += SaldoKSO
                            ElseIf Site = "Member1" Then
                                TmpPasiva.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), SaldoMember1)
                                TtlPasiva += SaldoMember1
                            ElseIf Site = "Member2" Then
                                TmpPasiva.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), SaldoMember2)
                                TtlPasiva += SaldoMember2
                            Else
                                TmpPasiva.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), SaldoKSO + SaldoMember1 + SaldoMember2)
                                TtlPasiva += SaldoKSO + SaldoMember1 + SaldoMember2
                            End If
                        Else
                            If Site = "KSO" Then
                                TmpAktiva.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), SaldoKSO)
                                TtlAktiva += SaldoKSO
                            ElseIf Site = "Member1" Then
                                TmpAktiva.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), SaldoMember1)
                                TtlAktiva += SaldoMember1
                            ElseIf Site = "Member2" Then
                                TmpAktiva.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), SaldoMember2)
                                TtlAktiva += SaldoMember2
                            Else
                                TmpAktiva.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), SaldoKSO + SaldoMember1 + SaldoMember2)
                                TtlAktiva += SaldoKSO + SaldoMember1 + SaldoMember2
                            End If
                        End If
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

            End Using
        End Using

        For Each row As DataRow In TmpAktiva.Select("Amount=0")
            row.Delete()
        Next
        For Each row As DataRow In TmpPasiva.Select("Amount=0")
            row.Delete()
        Next
        TmpAktiva.AcceptChanges()
        TmpPasiva.AcceptChanges()

        Using Rpt As New RptLK
            With Rpt
                .Database.Tables("Aktiva").SetDataSource(TmpAktiva)
                .Database.Tables("Pasiva").SetDataSource(TmpPasiva)
                .OpenSubreport("Aktiva").SetDataSource(TmpAktiva)
                .OpenSubreport("Pasiva").SetDataSource(TmpPasiva)
                .SetParameterValue("@Title", "Laporan Posisi Keuangan (Neraca) " + CharMo(PrdAwal) + " " + Tahun.ToString)
                .SetParameterValue("@PrintInfo", "Printed On " + Format(Now, "dd-MMM-yyyy HH:mm") + " By " + Session("User").ToString.Split("|")(0))
                .SetParameterValue("@UserName", Session("User").ToString.Split("|")(0))
                .SetParameterValue("@TtlAktiva", TtlAktiva)
                .SetParameterValue("@TtlPasiva", TtlPasiva)
                Using CmdFind As New Data.SqlClient.SqlCommand
                    With CmdFind
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT A.*, B.Logo AS GlLogo FROM Job A LEFT JOIN GlReff B ON A.JobNo=B.JobNo AND B.Site=@P2 WHERE A.JobNo=@P1"
                        .Parameters.AddWithValue("@P1", JobNo)
                        .Parameters.AddWithValue("@P2", Site)
                    End With
                    Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                        If RsFind.Read Then
                            .SetParameterValue("@Kontraktor", RsFind("CompanyId"))
                            .SetParameterValue("@Deskripsi", RsFind("Deskripsi"))
                            If String.IsNullOrEmpty(RsFind("Own").ToString) = False Then
                                .SetParameterValue("@Site", If(RsFind("Own") = "1", RsFind("Member1"), RsFind("Member2")))
                            Else
                                .SetParameterValue("@Site", "")
                            End If
                            .SetParameterValue("@Image", If(String.IsNullOrEmpty(RsFind("GlLogo").ToString) = True, Server.MapPath("~/Images/NoLogo.jpg"), _
                                                            Server.MapPath(RsFind("GlLogo").ToString)))
                        End If
                    End Using
                End Using
            End With

            CRViewer.ReportSource = Rpt
            Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, False, "")

        End Using

    End Sub

End Class