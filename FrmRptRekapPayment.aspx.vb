Public Class FrmRptRekapPayment
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "RekapPayment") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Call BindReport()

    End Sub

    Private Sub BindReport()
        Dim TmpDt As New DataTable()
        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("TglBayar", GetType(Date)), _
                                                 New DataColumn("TipeForm", GetType(String)), _
                                                 New DataColumn("Alokasi", GetType(String)), _
                                                 New DataColumn("NoPD", GetType(String)), _
                                                 New DataColumn("JobNo", GetType(String)), _
                                                 New DataColumn("NamaTerima", GetType(String)), _
                                                 New DataColumn("NoRekTerima", GetType(String)), _
                                                 New DataColumn("BankTerima", GetType(String)), _
                                                 New DataColumn("TotalBayar", GetType(Decimal)), _
                                                 New DataColumn("CaraBayar", GetType(String)), _
                                                 New DataColumn("BankKirim", GetType(String)), _
                                                 New DataColumn("JenisTrf", GetType(String)), _
                                                 New DataColumn("NoRekKirim", GetType(String)), _
                                                 New DataColumn("NoKO", GetType(String)), _
                                                 New DataColumn("KeteranganBayar", GetType(String))})

        Dim PrdAwal As Date = Session("Print").ToString.Split("|")(1).Split("#")(0)
        Dim PrdAkhir As Date = Session("Print").ToString.Split("|")(1).Split("#")(1)
        Dim CmdReport As New Data.SqlClient.SqlCommand
        With CmdReport
            .Connection = Conn
            .CommandType = CommandType.Text
            If Trim(Session("Print").ToString.Split("|")(0)) = "Semua Job" Then
                .CommandText = "SELECT A.*, B.JobNm FROM BLE A JOIN Job B ON A.JobNo = B.JobNo WHERE " & _
                               "TglBayar>=@P2 AND TglBayar<=@P3 ORDER BY TglBayar"
            Else
                .CommandText = "SELECT A.*, B.JobNm FROM BLE A JOIN Job B ON A.JobNo = B.JobNo WHERE " & _
                               "A.JobNo=@P1 AND TglBayar>=@P2 AND TglBayar<=@P3 ORDER BY TglBayar"
            End If
            .Parameters.AddWithValue("@P1", Trim(Session("Print").ToString.Split("|")(0).Split("-")(0)))
            .Parameters.AddWithValue("@P2", PrdAwal.Date)
            .Parameters.AddWithValue("@P3", PrdAkhir.Date)
        End With
        Dim RsLoad As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
        While RsLoad.Read
            Dim NamaTerima As String = ""
            Dim BankTerima As String = ""
            Dim NoRekTerima As String = ""

            If RsLoad("NmPenerimaTunai").ToString <> "" Then
                NamaTerima = RsLoad("NmPenerimaTunai").ToString
                BankTerima = "-"
                NoRekTerima = "-"
            Else
                NamaTerima = RsLoad("AtasNama").ToString
                BankTerima = RsLoad("Bank").ToString
                NoRekTerima = RsLoad("NoRek").ToString
            End If
            

            Dim BankKirim As String = ""
            Dim NoRekKirim As String = ""

            Dim CmdFind1 As New Data.SqlClient.SqlCommand
            With CmdFind1
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Rekening WHERE RekId=@P1"
                .Parameters.AddWithValue("@P1", RsLoad("RekId"))
            End With
            Dim RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
            If RsFind1.Read Then
                BankKirim = RsFind1("Bank").ToString
                NoRekKirim = RsFind1("NoRek").ToString
            End If
            RsFind1.Close()
            CmdFind1.Dispose()

            Dim JenisTrf As String = ""
            If RsLoad("CaraBayar") = "CG" Or RsLoad("CaraBayar") = "CEK BG" Then
                JenisTrf = RsLoad("NoCG").ToString
            Else
                JenisTrf = RsLoad("JenisTrf").ToString
            End If
            TmpDt.Rows.Add(RsLoad("TglBayar"), RsLoad("TipeForm"), RsLoad("Alokasi"), RsLoad("NoPD"), RsLoad("JobNo") & "-" & RsLoad("JobNm"), _
                           NamaTerima, NoRekTerima, BankTerima, RsLoad("Amount"), RsLoad("CaraBayar"), BankKirim, JenisTrf, NoRekKirim, _
                           RsLoad("NoKO"), RsLoad("Keterangan").ToString)
        End While

        RsLoad.Close()

        Dim Rpt As New RptRekapPayment
        With Rpt
            .SetDataSource(TmpDt)
            .SetParameterValue("@Job", Session("Print").ToString.Split("|")(0))
            .SetParameterValue("@Periode", Format(PrdAwal.Date, "dd-MMM-yyyy") & " s.d. " & Format(PrdAkhir.Date, "dd-MMM-yyyy"))
        End With

        CRViewer.ReportSource = Rpt
        Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, False, "")

        TmpDt.Dispose()
        CmdReport.Dispose()

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Session("Job") = Trim(Session("Print").ToString.Split("|")(0))
        Session.Remove("Print")
        Response.Redirect("FrmRekapPayment.aspx")
    End Sub

End Class