Public Class FrmRptNeracaMutasi
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "NeracaMutasi") = False Then
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
        Dim Tahun As Integer = PrdAwal.Year
        Dim Bulan As Integer = PrdAwal.Month
        Dim DebetTahunLalu As Decimal, KreditTahunLalu As Decimal, DebetBulanIni As Decimal, KreditBulanIni As Decimal, DebetBulanLalu As Decimal, KreditBulanLalu As Decimal
        Dim TmpDt As New DataTable

        TmpDt.Clear()
        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("AccNo", GetType(String)), _
                                                 New DataColumn("AccName", GetType(String)), _
                                                 New DataColumn("DebetTahunLalu", GetType(Decimal)), _
                                                 New DataColumn("KreditTahunLalu", GetType(Decimal)), _
                                                 New DataColumn("DebetBulanIni", GetType(Decimal)), _
                                                 New DataColumn("KreditBulanIni", GetType(Decimal)), _
                                                 New DataColumn("DebetBulanLalu", GetType(Decimal)), _
                                                 New DataColumn("KreditBulanLalu", GetType(Decimal))})

        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM COA"
            End With
            Using RsReport As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                While RsReport.Read
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT * FROM JurnalEntry WHERE JobNo=@P1 AND Site=@P2 AND AccNo=@P3 AND TglJurnal<=@P4"
                            .Parameters.AddWithValue("@P1", JobNo)
                            .Parameters.AddWithValue("@P2", Site)
                            .Parameters.AddWithValue("@P3", RsReport("AccNo"))
                            .Parameters.AddWithValue("@P4", PrdAkhir)
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            While RsFind.Read
                                If RsFind("Tahun") < Tahun Then
                                    DebetTahunLalu += RsFind("Debet")
                                    KreditTahunLalu += RsFind("Kredit")
                                ElseIf RsFind("Tahun") = Tahun And RsFind("Bulan") = Bulan Then
                                    DebetBulanIni += RsFind("Debet")
                                    KreditBulanIni += RsFind("Kredit")
                                ElseIf RsFind("Tahun") = Tahun And RsFind("Bulan") < Bulan Then
                                    DebetBulanLalu += RsFind("Debet")
                                    KreditBulanLalu += RsFind("Kredit")
                                End If
                            End While
                        End Using
                    End Using

                    TmpDt.Rows.Add(RsReport("AccNo"), RsReport("AccName"), DebetTahunLalu, KreditTahunLalu, DebetBulanIni, KreditBulanIni, DebetBulanLalu, KreditBulanLalu)

                    DebetTahunLalu = 0
                    KreditTahunLalu = 0
                    DebetBulanIni = 0
                    KreditBulanIni = 0
                    DebetBulanLalu = 0
                    KreditBulanLalu = 0

                End While

            End Using
        End Using

        Using Rpt As New RptNeracaMutasi
            With Rpt
                .SetDataSource(TmpDt)
                .SetParameterValue("@Title", "Neraca Mutasi - " + CharMo(PrdAwal) + " " + Tahun.ToString)
                .SetParameterValue("@PrintInfo", "Printed On " + Format(Now, "dd-MMM-yyyy HH:mm") + " By " + Session("User").ToString.Split("|")(0))
                .SetParameterValue("@UserName", Session("User").ToString.Split("|")(0))
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
            End With

            CRViewer.ReportSource = Rpt
            Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, False, "")

        End Using

    End Sub

End Class