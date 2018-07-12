Public Class FrmRptShareTermin
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If CheckAkses() = False Then
            Response.Redirect("Default.aspx")
            Exit Sub
        End If

        Call BindReport()

    End Sub

    Private Sub BindReport()

        Dim CmdReport As New Data.SqlClient.SqlCommand
        With CmdReport
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM ShareTermin WHERE JobNo=@P1"
            .Parameters.AddWithValue("@P1", Session("Print"))
        End With

        Dim DaReport As New Data.SqlClient.SqlDataAdapter
        DaReport.SelectCommand = CmdReport
        Dim DtReport As New Data.DataTable
        DaReport.Fill(DtReport)

        Dim Rpt As New RptShareTermin
        CRViewer.ReportSource = Rpt

        Rpt.SetDataSource(DtReport)

        Rpt.SetParameterValue("@TtlTermin", CountTtlTermin)

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT A.*, B.CompanyNm FROM Job A " & _
                           "LEFT JOIN Company B ON B.CompanyId = A.CompanyId " & _
                           "WHERE A.JobNo=@P1"
            .Parameters.AddWithValue("@P1", Session("Print"))
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            Rpt.SetParameterValue("@CompanyNm", RsFind("NamaPartner").ToString + " - " + RsFind("CompanyNm").ToString)
            Rpt.SetParameterValue("@JobNm", RsFind("JobNo") + " - " + RsFind("JobNm"))
            Rpt.SetParameterValue("@Lokasi", RsFind("Lokasi"))
            Rpt.SetParameterValue("@BankInduk", RsFind("BankInduk").ToString)
            Rpt.SetParameterValue("@NoRekInduk", RsFind("NoRekInduk").ToString)
            Rpt.SetParameterValue("@HeaderMember1", RsFind("NamaPartner").ToString)
            Rpt.SetParameterValue("@HeaderMember2", RsFind("CompanyNm").ToString)
        End If
        RsFind.Close()
        CmdFind.Dispose()

        DaReport.Dispose()
        DtReport.Dispose()
        CmdReport.Dispose()

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Function CheckAkses() As Boolean
        If Session("User") = "" Then
            Return False
        Else
            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT AksesMenu FROM Login WHERE UserID=@P1"
                .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
            End With
            Dim AksesMenu As String = ""
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                AksesMenu = RsFind("AksesMenu").ToString
            End If
            RsFind.Close()
            CmdFind.Dispose()

            If AksesMenu = "*" Then Return True
            If Array.IndexOf(AksesMenu.Split(","), _
                HttpContext.Current.Request.Url.AbsolutePath.Split("/")(1).Split(".")(0)) < 0 Then
                Return False
            End If
        End If

        Return True
    End Function

    Protected Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Session("Job") = Session("Print")
        Session.Remove("Print")
        Response.Redirect("FrmRekapShareTermin.aspx")
    End Sub

    Private Function CountTtlTermin() As Decimal
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT SUM(TotalTermin) FROM Termin WHERE JobNo=@P1"
            .Parameters.AddWithValue("@P1", Session("Print"))
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            Dim Ttl As Decimal = If(IsDBNull(RsFind(0)), 0, RsFind(0))
            Dim TtlFisik As Decimal = Ttl / 1.1
            Dim TtlPPN As Decimal = TtlFisik * (10 / 100)
            Dim TtlPPH As Decimal = TtlFisik * (3 / 100)
            Dim Netto As Decimal = Ttl - TtlPPN - TtlPPH
            RsFind.Close()
            CmdFind.Dispose()
            Return Netto
        End If
        RsFind.Close()
        CmdFind.Dispose()

        Return 0
    End Function
End Class