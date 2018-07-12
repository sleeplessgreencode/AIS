Public Class FrmRptPenyerapanRAP
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim TmpDt As New DataTable()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "SerapRAP") = False Then
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
        Dim TtlTerserap As Decimal = 0

        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("KdRAP", GetType(String)), _
                                                 New DataColumn("Tipe", GetType(String)), _
                                                 New DataColumn("Header", GetType(String)), _
                                                 New DataColumn("Uraian", GetType(String)), _
                                                 New DataColumn("Uom", GetType(String)), _
                                                 New DataColumn("Vol", GetType(Decimal)), _
                                                 New DataColumn("HrgSatuan", GetType(Decimal)), _
                                                 New DataColumn("TotalTerserap", GetType(Decimal))})

        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2"
                .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P2", Session("Print").ToString.Split("|")(1))
            End With

            Using RsLoad As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                While RsLoad.Read
                    If RsLoad("Tipe") = "Header" Then
                        TmpDt.Rows.Add(RsLoad("KdRAP"), RsLoad("Tipe"), RsLoad("Header"), RsLoad("Uraian"), RsLoad("Uom"), RsLoad("Vol"), RsLoad("HrgSatuan"), 0)
                    Else
                        TtlTerserap = 0
                        Using CmdFind As New Data.SqlClient.SqlCommand
                            With CmdFind
                                .Connection = Conn
                                .CommandType = CommandType.Text
                                .CommandText = "SELECT A.* FROM PdDtl A JOIN PdHdr B ON A.NoPD=B.NoPD WHERE " + _
                                               "B.JobNo=@P1 AND B.Alokasi=@P2 AND B.RejectBy IS NULL AND A.KdRAP=@P3"
                                .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
                                .Parameters.AddWithValue("@P2", Session("Print").ToString.Split("|")(1))
                                .Parameters.AddWithValue("@P3", RsLoad("KdRAP"))
                            End With
                            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                                While RsFind.Read
                                    TtlTerserap += RsFind("PjVol") * RsFind("PjHrgSatuan")
                                End While

                            End Using
                        End Using
                        TmpDt.Rows.Add(RsLoad("KdRAP"), RsLoad("Tipe"), RsLoad("Header"), RsLoad("Uraian"), RsLoad("Uom"), RsLoad("Vol"), RsLoad("HrgSatuan"), TtlTerserap)
                        Session("TmpDt") = TmpDt
                        Call HitRecursive(RsLoad("Header"), RsLoad("HrgSatuan") * RsLoad("Vol"), TtlTerserap)

                    End If

                End While
            End Using

            Using Rpt As New RptSerapRAP
                With Rpt
                    .SetDataSource(TmpDt)
                    .SetParameterValue("@Title", "Penyerapan RAP")
                    .SetParameterValue("@PrintInfo", "Printed On " + Format(Now, "dd-MMM-yyyy HH:mm") + " By " + Session("User").ToString.Split("|")(0))
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT * FROM JOB WHERE JobNo=@P1"
                            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            If RsFind.Read Then
                                .SetParameterValue("@Job", RsFind("JobNo") + " - " + RsFind("JobNm"))
                            End If
                        End Using
                    End Using
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT * FROM Alokasi WHERE Alokasi=@P1"
                            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(1))
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            If RsFind.Read Then
                                .SetParameterValue("@Alokasi", RsFind("Alokasi") + " - " + RsFind("Keterangan"))
                            End If
                        End Using
                    End Using

                End With

                CRViewer.ReportSource = Rpt
                Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, False, "")

            End Using

        End Using

    End Sub

    Private Sub HitRecursive(ByVal KdRAP As String, ByVal HrgSatuan As Decimal, ByVal TotalTerserap As Decimal)
        TmpDt = Session("TmpDt")

        Dim Result As DataRow
        Result = TmpDt.Select("KdRAP='" + KdRAP + "'").FirstOrDefault
        If Result IsNot Nothing Then
            Result("TotalTerserap") += TotalTerserap
            Result("HrgSatuan") += HrgSatuan

            Session("TmpDt") = TmpDt

            If Result("Header") = "0" Then
                Exit Sub
            Else
                HitRecursive(Result("Header"), Result("HrgSatuan"), Result("TotalTerserap"))
            End If

        End If

    End Sub

End Class