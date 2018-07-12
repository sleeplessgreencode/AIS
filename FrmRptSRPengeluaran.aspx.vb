Public Class FrmRptSRPengeluaran
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "SRPengeluaran") = False Then
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
        Dim TmpDt As New DataTable()
        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("JobNo", GetType(String)), _
                                                 New DataColumn("JobNm", GetType(String)), _
                                                 New DataColumn("NettoKontrak", GetType(Decimal)), _
                                                 New DataColumn("TipeManajerial", GetType(String)), _
                                                 New DataColumn("ShareMinarta", GetType(Decimal)), _
                                                 New DataColumn("RAP", GetType(Decimal)), _
                                                 New DataColumn("MggLalu", GetType(Decimal)), _
                                                 New DataColumn("MggIni", GetType(Decimal)), _
                                                 New DataColumn("YTD", GetType(Decimal))})

        Dim StatusJob As String = Session("Print").ToString.Split("|")(0)
        Dim Alokasi As String = Session("Print").ToString.Split("|")(1)
        Dim DrTgl As Date = Session("Print").ToString.Split("|")(2)
        Dim SpTgl As Date = Session("Print").ToString.Split("|")(3)
        Dim Result As DataRow

        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Job WHERE StatusJob=@P1"
                .Parameters.AddWithValue("@P1", StatusJob)
            End With

            Using RsReport As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                While RsReport.Read
                    TmpDt.Rows.Add(RsReport("JobNo"), RsReport("JobNm"), RsReport("Netto") / 1000, RsReport("TipeManajerial"), _
                                   If(RsReport("TipeManajerial").ToString = "JO Partial", RsReport("PersenKSO"), 100), 0, 0, 0, 0)
                    Result = TmpDt.Select("JobNo='" + RsReport("JobNo") + "'").FirstOrDefault

                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT SUM(Vol*HrgSatuan) FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2 AND Tipe='Header' AND Header='0'"
                            .Parameters.AddWithValue("@P1", RsReport("JobNo"))
                            .Parameters.AddWithValue("@P2", Alokasi)
                        End With

                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            If RsFind.Read Then
                                If Result IsNot Nothing Then
                                    Result("RAP") = If(IsDBNull(RsFind(0)) = True, 0, RsFind(0) / 1000)
                                End If
                            End If
                        End Using
                    End Using

                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT * FROM BLE WHERE JobNo=@P1 AND Alokasi=@P2 AND TglBayar<=@P3 "
                            .Parameters.AddWithValue("@P1", RsReport("JobNo"))
                            .Parameters.AddWithValue("@P2", Alokasi)
                            .Parameters.AddWithValue("@P3", SpTgl)
                        End With

                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            While RsFind.Read
                                If RsFind("TglBayar") < DrTgl Then
                                    If Result IsNot Nothing Then
                                        Result("MggLalu") += RsFind("Amount") / 1000
                                    End If
                                ElseIf RsFind("TglBayar") >= DrTgl And RsFind("TglBayar") <= SpTgl Then
                                    If Result IsNot Nothing Then
                                        Result("MggIni") += RsFind("Amount") / 1000
                                    End If
                                End If
                                If RsFind("TglBayar") >= DateSerial(Year(SpTgl), 1, 1) Then
                                    If Result IsNot Nothing Then
                                        Result("YTD") += RsFind("Amount") / 1000
                                    End If
                                End If
                            End While
                        End Using
                    End Using
                End While
            End Using

            Using Rpt As New RptSRPengeluaran
                With Rpt
                    .SetDataSource(TmpDt)
                    .SetParameterValue("@Title", "Summary Rekap Pengeluaran " + If(StatusJob = "Pelaksanaan", "(On Going)", _
                                        If(StatusJob = "Pemeliharaan", "(Pemeliharaan - Pra FHO)", "(Closed - Pasca FHO)")))
                    .SetParameterValue("@PrintInfo", "Printed On " + Format(Now, "dd-MMM-yyyy HH:mm") + " By " + Session("User").ToString.Split("|")(0))
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT * FROM Alokasi WHERE Alokasi=@P1"
                            .Parameters.AddWithValue("@P1", Alokasi)
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            If RsFind.Read Then
                                .SetParameterValue("@Alokasi", RsFind("Alokasi") + " - " + RsFind("Keterangan"))
                            End If
                        End Using
                    End Using
                    .SetParameterValue("@Periode", Format(DrTgl, "dd-MMM-yyyy") + " s.d. " + Format(SpTgl, "dd-MMM-yyyy"))

                End With

                CRViewer.ReportSource = Rpt
                Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, False, "")

            End Using

        End Using

    End Sub

End Class