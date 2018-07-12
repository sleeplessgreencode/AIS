Public Class FrmRptNA
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "NotaAkuntansi") = False Then
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
        Dim JobNo As String = Session("Data").ToString.Split("|")(0)
        Dim Site As String = Session("Data").ToString.Split("|")(1)
        Dim NoJurnal As String = Session("Data").ToString.Split("|")(2)
        Dim TmpDt As New DataTable
        Dim Kasir As String = ""

        TmpDt.Clear()
        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("Identitas", GetType(String)), _
                                                 New DataColumn("Uraian", GetType(String)), _
                                                 New DataColumn("NoReg", GetType(String)), _
                                                 New DataColumn("AccNo", GetType(String)), _
                                                 New DataColumn("Debet", GetType(Decimal)), _
                                                 New DataColumn("Kredit", GetType(Decimal))})

        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM JurnalEntry " + _
                               "WHERE JobNo=@P1 AND Site=@P2 AND NoJurnal=@P3 AND PC='C' AND ApprovedBy IS NOT NULL"
                .Parameters.AddWithValue("@P1", JobNo)
                .Parameters.AddWithValue("@P2", Site)
                .Parameters.AddWithValue("@P3", NoJurnal)
            End With
            Using RsReport As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                While RsReport.Read
                    TmpDt.Rows.Add(RsReport("Identitas"), RsReport("Uraian"), RsReport("NoReg"), RsReport("AccNo"), RsReport("Debet"), RsReport("Kredit"))
                End While
            End Using
        End Using

        Using Rpt As New RptNA
            With Rpt
                .SetDataSource(TmpDt)
                .SetParameterValue("@Title", "Nota Akuntansi")
                .SetParameterValue("@PrintInfo", "Printed On " + Format(Now, "dd-MMM-yyyy HH:mm") + " By " + Session("User").ToString.Split("|")(0))
                .SetParameterValue("@NoJurnal", "( " & NoJurnal & " )")
                .SetParameterValue("@UserInfo", Session("User").ToString.Split("|")(0))
                Using CmdFind As New Data.SqlClient.SqlCommand
                    With CmdFind
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT * FROM Job WHERE JobNo=@P1"
                        .Parameters.AddWithValue("@P1", JobNo)
                    End With
                    Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                        If RsFind.Read Then
                            .SetParameterValue("@Kontraktor", RsFind("CompanyId"))
                            .SetParameterValue("@Deskripsi", RsFind("Deskripsi"))
                            If String.IsNullOrEmpty(RsFind("Own").ToString) = False Then
                                .SetParameterValue("@Site", "Site - " + If(RsFind("Own") = "1", RsFind("Member1"), RsFind("Member2")))
                            Else
                                .SetParameterValue("@Site", "")
                            End If
                        End If
                    End Using
                End Using
                Using CmdFind As New Data.SqlClient.SqlCommand
                    With CmdFind
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT * FROM JurnalEntry WHERE JobNo=@P1 AND Site=@P2 AND NoJurnal=@P3"
                        .Parameters.AddWithValue("@P1", JobNo)
                        .Parameters.AddWithValue("@P2", Site)
                        .Parameters.AddWithValue("@P3", NoJurnal)
                    End With
                    Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        .SetParameterValue("@TglJurnal", "Tgl Pembukuan : " + Format(RsFind("TglJurnal"), "dd-MMM-yyyy"))
                    End If
                End Using
                Using CmdFind As New Data.SqlClient.SqlCommand
                    With CmdFind
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT * FROM GlReff WHERE JobNo=@P1 AND Site=@P2"
                        .Parameters.AddWithValue("@P1", JobNo)
                        .Parameters.AddWithValue("@P2", Site)
                    End With
                    Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        Kasir = RsFind("Kasir").ToString
                    End If
                End Using
                If NoJurnal.Split("/")(3) = "KM" Or NoJurnal.Split("/")(3) = "BM" Then
                    .SetParameterValue("@SignInfo", "Diterima oleh")
                    .SetParameterValue("@Kasir", Kasir)
                ElseIf NoJurnal.Split("/")(3) = "KK" Or NoJurnal.Split("/")(3) = "BK" Then
                    .SetParameterValue("@SignInfo", "Dibayar oleh")
                    .SetParameterValue("@Kasir", Kasir)
                Else
                    .SetParameterValue("@SignInfo", "")
                    .SetParameterValue("@Kasir", "")
                End If
            End With

            CRViewer.ReportSource = Rpt
            Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, False, "")

        End Using

    End Sub

End Class