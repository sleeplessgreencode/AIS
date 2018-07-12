Public Class FrmRptDPPD
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim Rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "DPPD") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Call BindReport()

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Rpt.Close()
        Rpt.Dispose()
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindReport()
        Dim TmpDt As New DataTable()
        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("NoPD", GetType(String)), _
                                                 New DataColumn("NoRef", GetType(String)), _
                                                 New DataColumn("TglPD", GetType(Date)), _
                                                 New DataColumn("Deskripsi", GetType(String)), _
                                                 New DataColumn("PrdAwal", GetType(Date)), _
                                                 New DataColumn("PrdAkhir", GetType(Date)), _
                                                 New DataColumn("Alokasi", GetType(String)), _
                                                 New DataColumn("NoKO", GetType(String)), _
                                                 New DataColumn("NoTagihan", GetType(String)), _
                                                 New DataColumn("VendorId", GetType(String)), _
                                                 New DataColumn("Nama", GetType(String)), _
                                                 New DataColumn("TotalPD", GetType(Decimal)), _
                                                 New DataColumn("TglBayar", GetType(Date)), _
                                                 New DataColumn("CaraBayar", GetType(String)), _
                                                 New DataColumn("NoPJ", GetType(String)), _
                                                 New DataColumn("TglPJ", GetType(Date)), _
                                                 New DataColumn("TotalPJ", GetType(Decimal)), _
                                                 New DataColumn("TipeForm", GetType(String)), _
                                                 New DataColumn("RekPengirim", GetType(String)), _
                                                 New DataColumn("RekPenerima", GetType(String))})
        Dim TmpDt1 As New DataTable()
        TmpDt1.Columns.AddRange(New DataColumn() {New DataColumn("NoPD", GetType(String)), _
                                                  New DataColumn("Uraian", GetType(String)), _
                                                  New DataColumn("KdRAP", GetType(String)), _
                                                  New DataColumn("Vol", GetType(Decimal)), _
                                                  New DataColumn("Uom", GetType(String)), _
                                                  New DataColumn("HrgSatuan", GetType(Decimal)), _
                                                  New DataColumn("PjUraian", GetType(String)), _
                                                  New DataColumn("PjVol", GetType(Decimal)), _
                                                  New DataColumn("PjUom", GetType(String)), _
                                                  New DataColumn("PjHrgSatuan", GetType(Decimal))})

        Dim PrdAwal As Date = If(Session("Print").ToString.Split("|")(2) = "All", "1900-1-1", Session("Print").ToString.Split("|")(2).Split("#")(0))
        Dim PrdAkhir As Date = If(Session("Print").ToString.Split("|")(2) = "All", "2999-1-1", Session("Print").ToString.Split("|")(2).Split("#")(1))

        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.JobNo AS JobNo, A.*, B.JobNm, " & _
                               "(SELECT TOP 1 RekId FROM BLE WHERE NoPD=A.NoPD OR NoPD=A.NoRef) AS 'RekId', " & _
                               "(SELECT TOP 1 TglBayar FROM BLE WHERE NoPD=A.NoPD OR NoPD=A.NoRef) AS 'TglBayar', " & _
                               "(SELECT TOP 1 CaraBayar FROM BLE WHERE NoPD=A.NoPD OR NoPD=A.NoRef) AS 'CaraBayar' " & _
                               "FROM PdHdr A " & _
                               "LEFT JOIN Job B ON A.JobNo=B.JobNo WHERE " & _
                               "A.JobNo=@P1 AND A.Alokasi=@P2 AND TglPD>=@P3 AND TglPD<=@P4 ORDER BY NoPD DESC"
                .Parameters.AddWithValue("@P1", Trim(Session("Print").ToString.Split("|")(0).Split("-")(0)))
                .Parameters.AddWithValue("@P2", Trim(Session("Print").ToString.Split("|")(1)))
                .Parameters.AddWithValue("@P3", PrdAwal.Date)
                .Parameters.AddWithValue("@P4", PrdAkhir.Date)
            End With

            Dim VendorId As String = ""
            Dim VendorNm As String = ""
            Dim RekPenerima As String = ""
            Dim CaraBayar As String = ""
            Using RsLoad As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                While RsLoad.Read
                    VendorNm = RsLoad("Nama").ToString
                    RekPenerima = RsLoad("Bank") & "/" & RsLoad("AtasNama") & "/" & RsLoad("NoRek")

                    Dim RekPengirim As String = ""
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT * FROM Rekening WHERE RekId=@P1"
                            .Parameters.AddWithValue("@P1", RsLoad("RekId"))
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            If RsFind.Read Then
                                RekPengirim = RsFind("Bank") & "/" & RsFind("AtasNama") & "/" & RsFind("NoRek")
                            End If
                        End Using
                    End Using

                    TmpDt.Rows.Add(RsLoad("NoPD"), RsLoad("NoRef"), RsLoad("TglPD"), RsLoad("Deskripsi"), RsLoad("PrdAwal"), RsLoad("PrdAkhir"), _
                                   RsLoad("Alokasi"), RsLoad("NoKO"), RsLoad("NoTagihan"), VendorId, VendorNm, RsLoad("TotalPD"), RsLoad("TglBayar"), _
                                   RsLoad("CaraBayar"), RsLoad("NoPJ"), RsLoad("TglPJ"), RsLoad("TotalPJ"), RsLoad("TipeForm"), RekPengirim, RekPenerima)

                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT * FROM PdDtl WHERE NoPD=@P1"
                            .Parameters.AddWithValue("@P1", RsLoad("NoPD"))
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            While RsFind.Read                                
                                TmpDt1.Rows.Add(RsFind("NoPD"), RsFind("Uraian"), RsFind("KdRAP"), RsFind("Vol"), RsFind("Uom"), RsFind("HrgSatuan"), _
                                                RsFind("PjUraian"), RsFind("PjVol"), RsFind("Uom"), RsFind("PjHrgSatuan"))
                            End While
                        End Using
                    End Using

                End While
            End Using

            Rpt.Load(Server.MapPath("~/Report/RptDPPD.rpt"))
            With Rpt
                .Database.Tables("PdHdr").SetDataSource(TmpDt)
                .Database.Tables("PdDtl").SetDataSource(TmpDt1)
                .SetParameterValue("@PrintInfo", "Printed On " + Format(Now, "dd-MMM-yyyy HH:mm") + " By " + Session("User").ToString.Split("|")(0))
                .SetParameterValue("@Job", Session("Print").ToString.Split("|")(0))
                .SetParameterValue("@Alokasi", Session("Print").ToString.Split("|")(1))
                If Session("Print").ToString.Split("|")(2) = "All" Then
                    .SetParameterValue("@Periode", "Semua Periode")
                Else
                    .SetParameterValue("@Periode", Format(PrdAwal.Date, "dd-MMM-yyyy") & " s.d. " & Format(PrdAkhir.Date, "dd-MMM-yyyy"))
                End If
                Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, False, "")
            End With

            With CRViewer
                .ReportSource = Rpt
                .Zoom(100)
            End With
            CRViewer.ReportSource = Rpt

        End Using
    End Sub

End Class