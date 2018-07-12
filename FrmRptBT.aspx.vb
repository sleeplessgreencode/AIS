Public Class FrmRptBT
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "BukuTambahan") = False Then
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
        Dim JobNo As String = Session("Data").ToString.Split("|")(0)
        Dim Site As String = Session("Data").ToString.Split("|")(1)
        Dim PrdAwal As Date = Session("Data").ToString.Split("|")(2)
        Dim PrdAkhir As Date = Session("Data").ToString.Split("|")(3)
        Dim AccNo As String = Session("Data").ToString.Split("|")(4)
        Dim Tahun As Integer = PrdAwal.Year
        Dim Bulan As Integer = PrdAwal.Month
        Dim DebetTahunKSO As Decimal, KreditTahunKSO As Decimal, DebetBulanKSO As Decimal, KreditBulanKSO As Decimal
        Dim DebetTahunMember1 As Decimal, KreditTahunMember1 As Decimal, DebetBulanMember1 As Decimal, KreditBulanMember1 As Decimal
        Dim DebetTahunMember2 As Decimal, KreditTahunMember2 As Decimal, DebetBulanMember2 As Decimal, KreditBulanMember2 As Decimal
        Dim DebetBulanIni As Decimal, KreditBulanIni As Decimal
        Dim SaldoKSO As Decimal, SaldoMember1 As Decimal, SaldoMember2 As Decimal
        Dim TmpDt As New DataTable

        TmpDt.Clear()
        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("Identitas", GetType(String)), _
                                                 New DataColumn("Nama", GetType(String)), _
                                                 New DataColumn("SaldoLalu", GetType(Decimal)), _
                                                 New DataColumn("Debet", GetType(Decimal)), _
                                                 New DataColumn("Kredit", GetType(Decimal)), _
                                                 New DataColumn("SaldoKini", GetType(Decimal))})

        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Identitas WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", JobNo)
            End With
            Using RsReport As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                While RsReport.Read
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT * FROM JurnalEntry " + _
                                           "WHERE JobNo=@P1 AND AccNo=@P2 AND TglJurnal<=@P3 AND Identitas=@P4"
                            .Parameters.AddWithValue("@P1", JobNo)
                            .Parameters.AddWithValue("@P2", AccNo)
                            .Parameters.AddWithValue("@P3", PrdAkhir)
                            .Parameters.AddWithValue("@P4", RsReport("Identitas"))
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            While RsFind.Read
                                If RsFind("Site") = "KSO" Then
                                    If RsFind("Tahun") < Tahun Then
                                        DebetTahunKSO += RsFind("Debet")
                                        KreditTahunKSO += RsFind("Kredit")
                                    ElseIf RsFind("Tahun") = Tahun And RsFind("Bulan") < Bulan Then
                                        DebetBulanKSO += RsFind("Debet")
                                        KreditBulanKSO += RsFind("Kredit")
                                    End If
                                ElseIf RsFind("Site") = "Member1" Then
                                    If RsFind("Tahun") < Tahun Then
                                        DebetTahunMember1 += RsFind("Debet")
                                        KreditTahunMember1 += RsFind("Kredit")
                                    ElseIf RsFind("Tahun") = Tahun And RsFind("Bulan") < Bulan Then
                                        DebetBulanMember1 += RsFind("Debet")
                                        KreditBulanMember1 += RsFind("Kredit")
                                    End If
                                ElseIf RsFind("Site") = "Member2" Then
                                    If RsFind("Tahun") < Tahun Then
                                        DebetTahunMember2 += RsFind("Debet")
                                        KreditTahunMember2 += RsFind("Kredit")
                                    ElseIf RsFind("Tahun") = Tahun And RsFind("Bulan") < Bulan Then
                                        DebetBulanMember2 += RsFind("Debet")
                                        KreditBulanMember2 += RsFind("Kredit")
                                    End If
                                End If

                                If RsFind("Tahun") = Tahun And RsFind("Bulan") = Bulan Then
                                    DebetBulanIni += RsFind("Debet")
                                    KreditBulanIni += RsFind("Kredit")
                                End If

                            End While
                        End Using
                    End Using

                    SaldoKSO = DebetTahunKSO - KreditTahunKSO + DebetBulanKSO - KreditBulanKSO
                    SaldoMember1 = DebetTahunMember1 - KreditTahunMember1 + DebetBulanMember1 - KreditBulanMember1
                    SaldoMember2 = DebetTahunMember2 - KreditTahunMember2 + DebetBulanMember2 - KreditBulanMember2

                    If Site = "KSO" Then
                        TmpDt.Rows.Add(RsReport("Identitas"), RsReport("Nama"), SaldoKSO, DebetBulanIni, KreditBulanIni)
                    ElseIf Site = "Member1" Then
                        TmpDt.Rows.Add(RsReport("Identitas"), RsReport("Nama"), SaldoMember1, DebetBulanIni, KreditBulanIni)
                    ElseIf Site = "Member2" Then
                        TmpDt.Rows.Add(RsReport("Identitas"), RsReport("Nama"), SaldoMember2, DebetBulanIni, KreditBulanIni)
                    Else
                        TmpDt.Rows.Add(RsReport("Identitas"), RsReport("Nama"), SaldoKSO + SaldoMember1 + SaldoMember2, DebetBulanIni, KreditBulanIni)
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
                    DebetBulanIni = 0
                    KreditBulanIni = 0
                    
                End While
            End Using
        End Using

        Using Rpt As New RptBT
            With Rpt
                .SetDataSource(TmpDt)
                .SetParameterValue("@Title", "Buku Tambahan Tahunan " + CharMo(PrdAwal) + " " + Tahun.ToString)
                .SetParameterValue("@PrintInfo", "Printed On " + Format(Now, "dd-MMM-yyyy HH:mm") + " By " + Session("User").ToString.Split("|")(0))
                Using CmdFind As New Data.SqlClient.SqlCommand
                    With CmdFind
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT * FROM Job WHERE JobNo=@P1"
                        .Parameters.AddWithValue("@P1", JobNo)
                    End With
                    Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                        If RsFind.Read Then
                            .SetParameterValue("@Kontraktor", RsFind("CompanyId"))
                            .SetParameterValue("@Deskripsi", RsFind("Deskripsi"))
                            If String.IsNullOrEmpty(RsFind("Own").ToString) = False Then
                                .SetParameterValue("@Site", "Site - " + If(RsFind("Own") = "1", RsFind("Member1"), RsFind("Member2")))
                            Else
                                .SetParameterValue("@Site", "")
                            End If
                        End If
                    End Using
                End Using
                Using CmdFind As New Data.SqlClient.SqlCommand
                    With CmdFind
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT * FROM COA WHERE AccNo=@P1"
                        .Parameters.AddWithValue("@P1", AccNo)
                    End With
                    Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                        If RsFind.Read Then
                            .SetParameterValue("@Account", AccNo + " - " + RsFind("AccName"))
                        End If
                    End Using
                End Using
            End With

            CRViewer.ReportSource = Rpt
            Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, False, "")

        End Using

    End Sub

End Class