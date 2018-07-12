Public Class FrmRptRekapRAP
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
        Dim TmpDt1 As New DataTable()
        TmpDt1.Columns.AddRange(New DataColumn() {New DataColumn("KdRAP", GetType(String)), _
                                                  New DataColumn("Tipe", GetType(String)), _
                                                  New DataColumn("Uraian", GetType(String)), _
                                                  New DataColumn("Uom", GetType(String)), _
                                                  New DataColumn("Vol1", GetType(Decimal)), _
                                                  New DataColumn("Vol2", GetType(Decimal)), _
                                                  New DataColumn("HrgSatuan1", GetType(Decimal)), _
                                                  New DataColumn("HrgSatuan2", GetType(Decimal))})

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT JobNo, JobNm FROM Job WHERE JobNo=@P1"
            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
        End With
        Dim LblJob As String = ""
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            LblJob = RsFind("JobNo") + " - " + RsFind("JobNm")
        End If
        RsFind.Close()
        CmdFind.Dispose()

        Dim CmdFind1 As New Data.SqlClient.SqlCommand
        With CmdFind1
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT A.Tipe, A.Versi, A.KdRAP, A.Uraian, A.Uom, A.Vol AS 'Vol2', A.HrgSatuan AS 'HrgSatuan2', B.Vol AS 'Vol1', B.HrgSatuan AS 'HrgSatuan1' " & _
                           "FROM RAP A LEFT JOIN RAPH B " & _
                           "ON A.JobNo=B.JobNo AND A.KdRAP=B.KdRAP AND B.Versi='0.0' WHERE A.JobNo=@P1 AND A.Alokasi=@P2"
            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P2", Session("Print").ToString.Split("|")(1))
        End With
        Dim LblVersi As String = ""
        Dim RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
        While RsFind1.Read
            If RsFind1("Tipe") = "Header" Then
                TmpDt1.Rows.Add(RsFind1("KdRAP"), RsFind1("Tipe"), RsFind1("Uraian"), "", 0, 0, 0, 0)
            Else
                TmpDt1.Rows.Add(RsFind1("KdRAP"), RsFind1("Tipe"), RsFind1("Uraian"), RsFind1("Uom"), If(IsDBNull(RsFind1("Vol1")), 0, RsFind1("Vol1")), RsFind1("Vol2"), If(IsDBNull(RsFind1("HrgSatuan1")), 0, RsFind1("HrgSatuan1")), RsFind1("HrgSatuan2"))
            End If
            LblVersi = RsFind1("Versi")
        End While
        RsFind1.Close()
        CmdFind1.Dispose()

        Dim Rpt As New RptRekapRAP
        Rpt.SetDataSource(TmpDt1)
        Rpt.SetParameterValue("@JobNo", LblJob)
        Rpt.SetParameterValue("@Alokasi", Session("Print").ToString.Split("|")(1))
        Rpt.SetParameterValue("@Versi", LblVersi)
        CRViewer.ReportSource = Rpt
        TmpDt1.Dispose()

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
        Response.Redirect("FrmRekapRAP.aspx")
    End Sub

End Class