Public Class FrmRptTermin
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "Termin") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Call BindReport()

    End Sub

    Private Sub BindReport()        
        Dim CmdReport As New Data.SqlClient.SqlCommand
        With CmdReport
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Termin WHERE JobNo=@P1"
            .Parameters.AddWithValue("@P1", Session("Print"))
        End With

        Dim DaReport As New Data.SqlClient.SqlDataAdapter
        DaReport.SelectCommand = CmdReport
        Dim DtReport As New Data.DataTable
        DaReport.Fill(DtReport)

        Dim Rpt As New RptTermin
        Rpt.SetDataSource(DtReport)

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT A.*, B.Bruto AS BrutoAwal FROM Job A " & _
                           "LEFT JOIN JobH B ON B.JobNo = A.JobNo AND B.AddendumKe=0 " & _
                           "WHERE A.JobNo=@P1"
            .Parameters.AddWithValue("@P1", Session("Print"))
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            Rpt.SetParameterValue("@CompanyNm", RsFind("CompanyId").ToString)
            Rpt.SetParameterValue("@JobNm", RsFind("JobNo") + " - " + RsFind("JobNm"))
            Rpt.SetParameterValue("@Lokasi", RsFind("Lokasi"))
            Rpt.SetParameterValue("@BankInduk", RsFind("BankInduk").ToString)
            Rpt.SetParameterValue("@NoRekInduk", RsFind("NoRekInduk").ToString)
            Rpt.SetParameterValue("@NilaiKontrak", If(IsDBNull(RsFind("BrutoAwal")), RsFind("Bruto"), RsFind("BrutoAwal")))
            Rpt.SetParameterValue("@NilaiAddendum", If(IsDBNull(RsFind("BrutoAwal")), 0, RsFind("Bruto") - RsFind("BrutoAwal")))
        End If
        RsFind.Close()
        CmdFind.Dispose()

        CRViewer.ReportSource = Rpt
        Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, False, "")

        DaReport.Dispose()
        DtReport.Dispose()
        CmdReport.Dispose()

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

End Class