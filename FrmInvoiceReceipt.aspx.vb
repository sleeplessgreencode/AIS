Public Class FrmInvoiceReceipt
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim Rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "InvoiceReceipt") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Rpt.Close()
            Rpt.Dispose()
            Session.Remove("InvReceipt")
            Call BindJob()
            Call BindVendor()
        Else
            CRViewer.ReportSource = Session("InvReceipt")
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindReport()
        Dim TmpQuery As String = String.Empty
        TmpQuery = If(DDLJob.Value <> "All", " AND B.JobNo='" & DDLJob.Value & "'", String.Empty)
        TmpQuery = TmpQuery & If(DDLVendor.Value <> "All", " AND D.VendorId='" & DDLVendor.Value & "'", String.Empty)

        Dim TmpDt As New DataTable()
        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("NoKO", GetType(String)), _
                                                 New DataColumn("InvNo", GetType(String)), _
                                                 New DataColumn("NoPD", GetType(String)), _
                                                 New DataColumn("BLEAmount", GetType(Decimal))})

        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, B.CompanyId, B.JobNm, D.VendorNm FROM Invoice A " & _
                               "LEFT JOIN Job B ON A.JobNo=B.JobNo " & _
                               "LEFT JOIN KoHdr C ON A.NoKO=C.NoKO " & _
                               "LEFT JOIN Vendor D ON C.VendorId=D.VendorId " & _
                               "WHERE A.TimeEntry>=@P1 AND A.TimeEntry<=@P2 " & TmpQuery & " ORDER BY " & DDLSort.Value
                .Parameters.AddWithValue("@P1", PrdAwal.Date & " 00:00")
                .Parameters.AddWithValue("@P2", PrdAkhir.Date & " 23:59")
            End With
            Using RsReport As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                While RsReport.Read
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT A.NoKO, A.InvNo, A.NoPD, " & _
                                           "ISNULL((SELECT SUM(PaymentAmount) FROM BLE, InvPD, PdHdr WHERE " & _
                                           "(BLE.NoPD=PdHdr.NoPD OR BLE.NoPD=PdHdr.NoRef) AND PdHdr.NoKO =InvPD.NoKO AND InvPD.NoPD=PdHdr.NoPD AND " & _
                                           "InvPD.NoKO=A.NoKO AND InvPD.InvNo=A.InvNo AND InvPD.NoPD=A.NoPD),0) AS BLEAmount " & _
                                           "FROM InvPD A WHERE NoKO=@P1 AND InvNo=@P2"
                            .Parameters.AddWithValue("@P1", RsReport("NoKO"))
                            .Parameters.AddWithValue("@P2", RsReport("InvNo"))
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            While RsFind.Read
                                TmpDt.Rows.Add(RsFind("NoKO"), RsFind("InvNo"), RsFind("NoPD"), RsFind("BLEAmount"))
                            End While
                        End Using
                    End Using
                End While
            End Using
            Using DaRpt As New Data.SqlClient.SqlDataAdapter
                DaRpt.SelectCommand = CmdReport
                Using DtRpt As New Data.DataTable
                    DaRpt.Fill(DtRpt)

                    Rpt.Load(Server.MapPath("~/Report/RptInvReceipt.rpt"))
                    With Rpt
                        .SetDataSource(DtRpt)
                        .Database.Tables("InvPD").SetDataSource(TmpDt)
                        .SetParameterValue("@Tgl", Format(PrdAwal.Date, "dd-MMM-yyyy") & " s.d. " & Format(PrdAkhir.Date, "dd-MMM-yyyy"))
                        .SetParameterValue("@PrintInfo", "Printed On " + Format(Now, "dd-MMM-yyyy HH:mm") + " By " + Session("User").ToString.Split("|")(0))
                    End With

                    Session("InvReceipt") = Rpt

                    With CRViewer
                        .ReportSource = Rpt
                        .Zoom(100)
                    End With
                End Using
            End Using
        End Using

        TmpDt.Dispose()

    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As System.EventArgs) Handles BtnPrint.Click
        Call BindReport()
    End Sub

    Private Sub BindJob()
        DDLJob.Items.Clear()
        DDLJob.Items.Add("All Job", "All")

        Using CmdIsi As New Data.SqlClient.SqlCommand
            With CmdIsi
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo,JobNm FROM Job"
            End With
            Using RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
                While RsIsi.Read
                    DDLJob.Items.Add(RsIsi("JobNo") & " - " & RsIsi("JobNm"), RsIsi("JobNo"))
                End While
            End Using
        End Using

        DDLJob.SelectedIndex = 0

    End Sub

    Private Sub BindVendor()
        DDLVendor.Items.Clear()
        DDLVendor.Items.Add("All Vendor", "All")

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT VendorId,VendorNm FROM Vendor"
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    DDLVendor.Items.Add(RsFind("VendorNm") & " - " & RsFind("VendorId"), RsFind("VendorId"))
                End While
            End Using
        End Using

        DDLVendor.SelectedIndex = 0

    End Sub

End Class