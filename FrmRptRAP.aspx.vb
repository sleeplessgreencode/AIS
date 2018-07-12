Public Class FrmRptRAP
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "RAP") = False Then
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
            If Session("Print").ToString.Split("|")(2) = "RAP" Then
                .CommandText = "SELECT A.*, B.JobNm, B.Lokasi " & _
                            "FROM RAP A JOIN Job B ON A.JobNo = B.JobNo " & _
                            "WHERE A.JobNo=@P1 AND A.Alokasi LIKE @P3"
            Else
                .CommandText = "SELECT A.*, B.JobNm, B.Lokasi " & _
                            "FROM RAPH A JOIN Job B ON A.JobNo = B.JobNo " & _
                            "WHERE A.JobNo=@P1 AND A.Versi=@P2 AND A.Alokasi LIKE @P3"
            End If
            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P2", Session("Print").ToString.Split("|")(3))
            .Parameters.AddWithValue("@P3", "%" + Session("Print").ToString.Split("|")(1) + "%")
        End With
        'Dim Ttl As Decimal = 0
        'Dim RsReport As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
        'While RsReport.Read
        '    If RsReport("Tipe") = "Header" Then Continue While
        '    Ttl = Ttl + (RsReport("Vol") * RsReport("HrgSatuan"))
        'End While
        'RsReport.Close()

        Dim DaReport As New Data.SqlClient.SqlDataAdapter
        DaReport.SelectCommand = CmdReport
        Dim DtReport As New Data.DataTable
        DaReport.Fill(DtReport)
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader

        Dim Rpt As New RptRAP
        With Rpt
            .SetDataSource(DtReport)
            If RsFind.Read Then
                .SetParameterValue("@JobNm", RsFind("JobNm"))
                .SetParameterValue("@Alokasi", RsFind("Alokasi"))
                '.SetParameterValue("@Total", Ttl)
            Else
                .SetParameterValue("@JobNm", " ")
                .SetParameterValue("@Alokasi", " ")
                '.SetParameterValue("@Total", "0")
            End If
        End With

        CRViewer.ReportSource = Rpt
        Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, False, "")

        RsFind.Close()
        DtReport.Dispose()
        DaReport.Dispose()
        CmdReport.Dispose()

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Session("Job") = Session("Print").ToString.Split("|")(0) & "|" & Session("Print").ToString.Split("|")(1) & "|" & _
                         Session("Print").ToString.Split("|")(2) & "|" & Session("Print").ToString.Split("|")(3)
        Session.Remove("Print")
        Response.Redirect("FrmRAP.aspx")
    End Sub
End Class