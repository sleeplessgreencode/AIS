Public Class FrmRptTraceKO
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim Rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "RptTrackingKO") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Call BindJob()
            Rpt.Close()
            Rpt.Dispose()
            Session.Remove("MyRpt")
        Else
            CRViewer.ReportSource = Session("MyRpt")
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindJob()
        DDLJob.Items.Clear()
        DDLJob.Items.Add("All Job", "All")

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo, JobNm FROM Job"
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                DDLJob.Items.Add(RsFind("JobNo") & " - " & RsFind("JobNm"), RsFind("JobNo"))
            End While
        End Using

        DDLJob.SelectedIndex = 0

    End Sub

    Private Sub BindReport()
        Dim TotalKO As Decimal = 0
        Dim TmpJob As String = If(DDLJob.Value <> "All", "AND A.JobNo=@P1", String.Empty)

        Dim TmpDt1 As New DataTable()
        TmpDt1.Columns.AddRange(New DataColumn() {New DataColumn("NoKO", GetType(String)), _
                                                  New DataColumn("TglKO", GetType(Date)), _
                                                  New DataColumn("VendorNm", GetType(String)), _
                                                  New DataColumn("TotalKO", GetType(Decimal))})
        Dim TmpDt2 As New DataTable()
        TmpDt2.Columns.AddRange(New DataColumn() {New DataColumn("NoKO", GetType(String)), _
                                                  New DataColumn("Keterangan", GetType(String)), _
                                                  New DataColumn("Tanggal", GetType(Date)), _
                                                  New DataColumn("Status", GetType(String))})

        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, B.VendorNm FROM KoHdr A JOIN Vendor B ON A.VendorId = B.VendorId WHERE A.KategoriId='PO' " & _
                               TmpJob & " ORDER BY NoKO"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With
            Using RsReport As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                While RsReport.Read
                    TotalKO = RsReport("SubTotal") - RsReport("DiscAmount") + RsReport("PPN")

                    If DDLStatus.Value <> "All" Then
                        Using CmdFind As New Data.SqlClient.SqlCommand
                            With CmdFind
                                .Connection = Conn
                                .CommandType = CommandType.Text
                                .CommandText = "SELECT TOP 1 * FROM TraceKO WHERE NoKO=@P1 ORDER BY Tanggal DESC"
                                .Parameters.AddWithValue("@P1", RsReport("NoKO"))
                            End With
                            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                                If RsFind.Read Then
                                    If RsFind("Status") = DDLStatus.Value Then
                                        TmpDt1.Rows.Add(RsReport("NoKO"), RsReport("TglKO"), RsReport("VendorNm"), TotalKO)

                                        Using CmdFind1 As New Data.SqlClient.SqlCommand
                                            With CmdFind1
                                                .Connection = Conn
                                                .CommandType = CommandType.Text
                                                .CommandText = "SELECT * FROM TraceKO WHERE NoKO=@P1 "
                                                .Parameters.AddWithValue("@P1", RsReport("NoKO"))
                                            End With
                                            Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                                                While RsFind1.Read
                                                    TmpDt2.Rows.Add(RsFind1("NoKO"), RsFind1("Keterangan"), RsFind1("Tanggal"), If(RsFind1("Status") = "0", "In Process", "Delivered"))
                                                End While
                                            End Using
                                        End Using
                                    End If
                                ElseIf DDLStatus.Value = "0" Then
                                    TmpDt1.Rows.Add(RsReport("NoKO"), RsReport("TglKO"), RsReport("VendorNm"), TotalKO)
                                End If
                            End Using
                        End Using
                    Else
                        TmpDt1.Rows.Add(RsReport("NoKO"), RsReport("TglKO"), RsReport("VendorNm"), TotalKO)

                        Using CmdFind1 As New Data.SqlClient.SqlCommand
                            With CmdFind1
                                .Connection = Conn
                                .CommandType = CommandType.Text
                                .CommandText = "SELECT * FROM TraceKO WHERE NoKO=@P1"
                                .Parameters.AddWithValue("@P1", RsReport("NoKO"))
                            End With
                            Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                                While RsFind1.Read
                                    TmpDt2.Rows.Add(RsFind1("NoKO"), RsFind1("Keterangan"), RsFind1("Tanggal"), If(RsFind1("Status") = "0", "In Process", "Delivered"))
                                End While
                            End Using
                        End Using
                    End If

                End While

            End Using
        End Using

        Rpt.Load(Server.MapPath("~/Report/RptTrackingKO.rpt"))
        With Rpt
            .Database.Tables("KoHdr").SetDataSource(TmpDt1)
            .Database.Tables("TraceKO").SetDataSource(TmpDt2)
            .OpenSubreport("TraceKO").SetDataSource(TmpDt2)
            .SetParameterValue("@Job", DDLJob.Text)
            .SetParameterValue("@Status", DDLStatus.Text)
            .SetParameterValue("@PrintInfo", "Printed On " + Format(Now, "dd-MMM-yyyy HH:mm") + " By " + Session("User").ToString.Split("|")(0))
        End With

        Session("MyRpt") = Rpt

        With CRViewer
            .ReportSource = Rpt
            .Zoom(100)
        End With

        TmpDt1.Dispose()
        TmpDt2.Dispose()

    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As System.EventArgs) Handles BtnPrint.Click
        Call BindReport()
    End Sub

End Class