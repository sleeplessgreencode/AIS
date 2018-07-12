Public Class FrmRptBB
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "BukuBesar") = False Then
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
        Dim DebetTahunLalu, KreditTahunLalu As Decimal
        Dim TmpDt As New DataTable

        TmpDt.Clear()
        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("Member", GetType(String)), _
                                                 New DataColumn("Nota", GetType(String)), _
                                                 New DataColumn("LedgerNo", GetType(Integer)), _
                                                 New DataColumn("NoJurnal", GetType(String)),
                                                 New DataColumn("TglJurnal", GetType(Date)), _
                                                 New DataColumn("Bulan", GetType(Integer)), _
                                                 New DataColumn("Tahun", GetType(Integer)), _
                                                 New DataColumn("Site", GetType(String)), _
                                                 New DataColumn("Nama", GetType(String)), _
                                                 New DataColumn("Identitas", GetType(String)), _
                                                 New DataColumn("PC", GetType(String)), _
                                                 New DataColumn("Uraian", GetType(String)), _
                                                 New DataColumn("NoReg", GetType(String)), _
                                                 New DataColumn("AccNo", GetType(String)), _
                                                 New DataColumn("DK", GetType(String)), _
                                                 New DataColumn("AccName", GetType(String)), _
                                                 New DataColumn("Debet", GetType(Decimal)), _
                                                 New DataColumn("Kredit", GetType(Decimal))})

        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, B.Nama, C.AccName FROM JurnalEntry A " + _
                               "LEFT JOIN Identitas B ON A.JobNo=B.JobNo AND A.Identitas=B.Identitas " + _
                               "LEFT JOIN COA C ON A.AccNo=C.AccNo " + _
                               "WHERE A.JobNo=@P1 AND A.Site=@P2 AND A.AccNo=@P3 AND A.TglJurnal<=@P4 ORDER BY TglJurnal"
                .Parameters.AddWithValue("@P1", JobNo)
                .Parameters.AddWithValue("@P2", Site)
                .Parameters.AddWithValue("@P3", AccNo)
                .Parameters.AddWithValue("@P4", PrdAkhir)
            End With
            Using RsReport As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                While RsReport.Read
                    If RsReport("Tahun") < Tahun Then
                        DebetTahunLalu += RsReport("Debet")
                        KreditTahunLalu += RsReport("Kredit")
                    ElseIf RsReport("Tahun") = Tahun And RsReport("Bulan") <= Bulan Then
                        TmpDt.Rows.Add(RsReport("Member"), RsReport("Nota"), RsReport("LedgerNo"), RsReport("NoJurnal"), RsReport("TglJurnal"), _
                                       RsReport("Bulan"), RsReport("Tahun"), RsReport("Site"), RsReport("Nama"), RsReport("Identitas"), _
                                       RsReport("PC"), RsReport("Uraian"), RsReport("NoReg"), RsReport("AccNo"), RsReport("DK"), RsReport("AccName"), _
                                       RsReport("Debet"), RsReport("Kredit"))
                    End If
                End While
            End Using
        End Using

        Using Rpt As New RptBB
            With Rpt
                .SetDataSource(TmpDt)
                .SetParameterValue("@Title", "Buku Besar Tahunan " + CharMo(PrdAwal) + " " + Tahun.ToString)
                .SetParameterValue("@PrintInfo", "Printed On " + Format(Now, "dd-MMM-yyyy HH:mm") + " By " + Session("User").ToString.Split("|")(0))
                .SetParameterValue("@DebetTahunLalu", DebetTahunLalu)
                .SetParameterValue("@KreditTahunLalu", KreditTahunLalu)
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