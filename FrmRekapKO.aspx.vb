Public Class FrmRekapKO
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim Rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "RekapKO") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Rpt.Close()
            Rpt.Dispose()
            Session.Remove("RekapKO")
            Call BindJob()
            PrdAwal.Date = DateSerial(Year(Today), Month(Today), 1)
            PrdAkhir.Date = DateAdd(DateInterval.Month, 1, DateSerial(Year(Today), Month(Today), 0))
        Else
            CRViewer.ReportSource = Session("RekapKO")
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindJob()
        Dim AksesJob As String = ""
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT AksesJob FROM Login WHERE UserID=@P1"
                .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    AksesJob = RsFind("AksesJob")
                End If
            End Using
        End Using

        DDLJob.Items.Clear()
        DDLJob.Text = ""
        Using CmdIsi As New Data.SqlClient.SqlCommand
            With CmdIsi
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo,JobNm FROM Job"
            End With
            Using RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
                While RsIsi.Read
                    If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsIsi("JobNo")) >= 0 Then
                        DDLJob.Items.Add(RsIsi("JobNo") & " - " & RsIsi("JobNm"), RsIsi("JobNo"))
                    End If
                End While
            End Using
        End Using

    End Sub

    Protected Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Call BindReport()
    End Sub

    Private Sub BindReport()
        Dim TmpDt1 As New DataTable()
        TmpDt1.Columns.AddRange(New DataColumn() {New DataColumn("NoKO", GetType(String)), _
                                                 New DataColumn("TglKO", GetType(Date)), _
                                                 New DataColumn("VendorNm", GetType(String)), _
                                                 New DataColumn("KategoriId", GetType(String)), _
                                                 New DataColumn("TotalKO", GetType(Decimal)), _
                                                 New DataColumn("TotalKOH", GetType(Decimal)), _
                                                 New DataColumn("TotalTerbayar", GetType(Decimal))})
        Dim TmpDt2 As New DataTable()
        TmpDt2.Columns.AddRange(New DataColumn() {New DataColumn("NoPJ", GetType(String)), _
                                                 New DataColumn("TglPJ", GetType(Date)), _
                                                 New DataColumn("TotalPJ", GetType(Decimal)), _
                                                 New DataColumn("NoKO", GetType(String)), _
                                                 New DataColumn("Keterangan", GetType(String))})

        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, B.VendorNm FROM KoHdr A JOIN Vendor B ON A.VendorId = B.VendorId WHERE " & _
                               "A.JobNo=@P1 AND TglKO>=@P2 AND TglKO<=@P3 AND ApprovedBy IS NOT NULL AND CanceledBy IS NULL ORDER BY NoKO"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", If(CbAll.Checked = True, "1900-1-1", PrdAwal.Date))
                .Parameters.AddWithValue("@P3", If(CbAll.Checked = True, "2999-1-1", PrdAkhir.Date))
            End With
            Dim TotalKO As Decimal = 0 'Total KoHdr
            Dim TotalKOH As Decimal = 0 'Total KoHdrH
            Dim TotalTerbayar As Decimal = 0
            Using RsLoad As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                While RsLoad.Read
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT * FROM BLE WHERE NoKO=@P1 ORDER BY NoKO"
                            .Parameters.AddWithValue("@P1", RsLoad("NoKO"))
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            While RsFind.Read
                                TmpDt2.Rows.Add(RsFind("NoPD"), RsFind("TglBayar"), RsFind("Amount"), RsFind("NoKO"), RsFind("Keterangan"))
                                TotalTerbayar += RsFind("Amount")
                            End While
                        End Using
                    End Using

                    TotalKOH = 0
                    'Dim CmdFind1 As New Data.SqlClient.SqlCommand
                    'With CmdFind1
                    '    .Connection = Conn
                    '    .CommandType = CommandType.Text
                    '    .CommandText = "SELECT * FROM KoHdrH WHERE NoKO=@P1 AND AddendumKe=0"
                    '    .Parameters.AddWithValue("@P1", RsLoad("NoKO"))
                    'End With
                    'Dim RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                    'If RsFind1.Read Then                
                    '    TotalKOH = RsFind1("SubTotal") - RsFind1("DiscAmount") + RsFind1("PPN")
                    'End If
                    'RsFind1.Close()
                    'CmdFind1.Dispose()

                    TotalKO = RsLoad("SubTotal") - RsLoad("DiscAmount") + RsLoad("PPN")
                    TmpDt1.Rows.Add(RsLoad("NoKO"), RsLoad("TglKO"), RsLoad("VendorNm"), RsLoad("KategoriId"), TotalKO, TotalKOH, TotalTerbayar)
                    TotalTerbayar = 0
                End While
            End Using

            Rpt.Load(Server.MapPath("~/Report/RptRekapKO.rpt"))
            With Rpt
                .Database.Tables("KoHdr").SetDataSource(TmpDt1)
                .Database.Tables("PdHdr").SetDataSource(TmpDt2)
                .SetParameterValue("@Job", DDLJob.Text)
                .SetParameterValue("@PrintInfo", "Printed On " + Format(Now, "dd-MMM-yyyy HH:mm") + " By " + Session("User").ToString.Split("|")(0))
                If CbAll.Checked = True Then
                    .SetParameterValue("@Periode", "Semua Periode")
                Else
                    .SetParameterValue("@Periode", Format(PrdAwal.Date, "dd-MMM-yyyy") & " s.d. " & Format(PrdAkhir.Date, "dd-MMM-yyyy"))
                End If
            End With

            Session("RekapKO") = Rpt

            With CRViewer
                .ReportSource = Rpt
                .Zoom(100)
            End With
        End Using

        TmpDt1.Dispose()
        TmpDt2.Dispose()

    End Sub

End Class