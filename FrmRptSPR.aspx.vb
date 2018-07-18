Public Class FrmRptSPR
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "KO") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Call BindReport()

    End Sub
    Private Sub BindReport()
        'Hapus semua file berawalan KO*.PNG di Folder TMP, meninggalkan selalu 1 file PNG untuk digunakan sebagai QRCode Image di crystal report.
        Dim path As String = Server.MapPath("~/TMP/")
        If System.IO.Directory.Exists(path) Then
            'Delete all files from the Directory
            For Each filepath As String In System.IO.Directory.GetFiles(path, "PR*.PNG")
                If System.IO.File.Exists(filepath) Then System.IO.File.Delete(filepath)
            Next
        End If

        Dim ImageFile As String = ""
        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.NoSPR AS NoSPR, A.*, B.*, C.JobNm, C.CompanyId, C.Logo " & _
                                "FROM (PRHdr A " & _
                                "LEFT JOIN PRDtl B ON A.NoSPR = B.NoSPR) " & _
                                "LEFT JOIN Job C ON C.JobNo = A.JobNo " & _
                                "WHERE A.NoSPR=@P1 ORDER BY B.NoUrut"
                .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
            End With
            Using DaReport As New Data.SqlClient.SqlDataAdapter
                DaReport.SelectCommand = CmdReport
                Using DtReport As New Data.DataTable
                    DaReport.Fill(DtReport)

                    'Dim Jumlah As Decimal = 0
                    'Using CmdFind1 As New Data.SqlClient.SqlCommand
                    '    With CmdFind1
                    '        .Connection = Conn
                    '        .CommandType = CommandType.Text
                    '        .CommandText = "SELECT Vol,HrgSatuan FROM PRDtl WHERE NoSPR=@P1"
                    '        .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
                    '    End With
                    '    Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                    '        While RsFind1.Read
                    '            Jumlah += RsFind1("Vol") * RsFind1("HrgSatuan")
                    '        End While
                    '    End Using
                    'End Using

                    Using Rpt As New RptSPR
                        With Rpt
                            .SetDataSource(DtReport)
                            '.SetParameterValue("@MaterialApproval", Chr(168).ToString)
                            '.SetParameterValue("@RAP", Chr(168).ToString)
                            '.SetParameterValue("@K3", Chr(168).ToString)
                            '.SetParameterValue("@OptInvoice", Chr(168).ToString)
                            '.SetParameterValue("@OptSJ", Chr(168).ToString)
                            '.SetParameterValue("@OptCopy", Chr(168).ToString)
                            '.SetParameterValue("@OptFP", Chr(168).ToString)
                            '.SetParameterValue("@OptBAP", Chr(168).ToString)
                            '.SetParameterValue("@OptBAPP", Chr(168).ToString)

                            Using RsLoad As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                                If RsLoad.Read Then
                                    '.SetParameterValue("@NoSPR", RsLoad("NoSPR"))
                                    '.SetParameterValue("@TglSPR", RsLoad("TglSPR"))
                                    '.SetParameterValue("@JobNo", RsLoad("JobNo") & " - " & RsLoad("JobNm"))
                                    '.SetParameterValue("@ImageCompany", If(String.IsNullOrEmpty(RsLoad("Logo").ToString) = True, _
                                    '                       Server.MapPath("~/Images/NoLogo.jpg"), Server.MapPath(RsLoad("Logo").ToString)))
                                    'ImageFile = GenerateQRImage(RsLoad("NoKO"), RsLoad("QRCode").ToString)
                                    .SetParameterValue("@Kepada", RsLoad("Kepada"))
                                    .SetParameterValue("@UtkPekerjaan", RsLoad("UtkPekerjaan"))
                                    .SetParameterValue("@Barcode", "*" & RsLoad("NoSPR") & "*")
                                    .SetParameterValue("@PrintInfo", "Printed On " & Format(Now, "dd-MMM-yyyy HH:mm") & " By " & Session("User").ToString.Split("|")(0))
                                    '.SetParameterValue("@Terbilang", "Terbilang : " + If(Jumlah = 0, "Nol Rupiah,", Trim(Terbilang(Jumlah)) + " Rupiah."))
                                End If
                            End Using
                            .ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, False, "")
                        End With

                        CRViewer.ReportSource = Rpt
                    End Using
                End Using
            End Using
        End Using

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Session("Job") = Trim(Session("Print").ToString.Split("|")(1))
        Session.Remove("Print")
        Response.Redirect("FrmSPR.aspx")
    End Sub

    Private Function GenerateQRImage(NoSPR As String, code As String)
        Dim Image As String = ""
        Dim qrGenerator As New QRCoder.QRCodeGenerator()
        Dim qrCode As QRCoder.QRCodeGenerator.QRCode = qrGenerator.CreateQrCode(code, QRCoder.QRCodeGenerator.ECCLevel.Q)
        Using bitMap As System.Drawing.Bitmap = qrCode.GetGraphic(20)
            Image = Server.MapPath("~/TMP/") + NoSPR + ".png"
            bitMap.Save(Image, System.Drawing.Imaging.ImageFormat.Png)
        End Using
        Return Image

    End Function
End Class