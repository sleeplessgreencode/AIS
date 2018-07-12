Public Class FrmRptSummaryKontrak
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
            For Each filepath As String In System.IO.Directory.GetFiles(path, "KO*.PNG")
                If System.IO.File.Exists(filepath) Then System.IO.File.Delete(filepath)
            Next
        End If

        Dim ImageFile As String = ""
        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.NoKO AS NoKO, A.*, B.*, C.JobNm " & _
                                "FROM (KoHdr A " & _
                                "LEFT JOIN KoDtl B ON A.NoKO = B.NoKO) " & _
                                "LEFT JOIN Job C ON C.JobNo = A.JobNo " & _
                                "WHERE A.NoKO=@P1 ORDER BY B.NoUrut"
                .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
            End With
            Using DaReport As New Data.SqlClient.SqlDataAdapter
                DaReport.SelectCommand = CmdReport
                Using DtReport As New Data.DataTable
                    DaReport.Fill(DtReport)

                    Dim Jumlah As Decimal = 0
                    Using CmdFind1 As New Data.SqlClient.SqlCommand
                        With CmdFind1
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT Vol,HrgSatuan FROM KoDtl WHERE NoKO=@P1"
                            .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
                        End With
                        Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                            While RsFind1.Read
                                Jumlah += RsFind1("Vol") * RsFind1("HrgSatuan")
                            End While
                        End Using
                    End Using

                    Using Rpt As New RptSummaryKontrak
                        With Rpt
                            .SetDataSource(DtReport)
                            .SetParameterValue("@K3", Chr(168).ToString)
                            .SetParameterValue("@OptInvoice", Chr(168).ToString)
                            .SetParameterValue("@OptSJ", Chr(168).ToString)
                            .SetParameterValue("@OptCopy", Chr(168).ToString)
                            .SetParameterValue("@OptFP", Chr(168).ToString)
                            .SetParameterValue("@OptBAP", Chr(168).ToString)
                            .SetParameterValue("@OptBAPP", Chr(168).ToString)

                            Using RsLoad As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                                If RsLoad.Read Then
                                    .SetParameterValue("@NoKO", If(RsLoad("AddendumKe") > 0, RsLoad("NoKO") + " ADD " + RsLoad("AddendumKe").ToString, RsLoad("NoKO")))
                                    .SetParameterValue("@TglKO", RsLoad("TglKO"))
                                    .SetParameterValue("@JobNo", RsLoad("JobNo") & " - " & RsLoad("JobNm"))
                                    .SetParameterValue("@K3", If(String.IsNullOrEmpty(RsLoad("K3").ToString) = False, Chr(254).ToString, Chr(168).ToString))
                                    If Array.IndexOf(RsLoad("SyaratPembayaran").Split(","), "INV") >= 0 Then .SetParameterValue("@OptInvoice", Chr(254).ToString)
                                    If Array.IndexOf(RsLoad("SyaratPembayaran").Split(","), "SJ") >= 0 Then .SetParameterValue("@OptSJ", Chr(254).ToString)
                                    If Array.IndexOf(RsLoad("SyaratPembayaran").Split(","), "PO") >= 0 Then .SetParameterValue("@OptCopy", Chr(254).ToString)
                                    If Array.IndexOf(RsLoad("SyaratPembayaran").Split(","), "FP") >= 0 Then .SetParameterValue("@OptFP", Chr(254).ToString)
                                    If Array.IndexOf(RsLoad("SyaratPembayaran").Split(","), "BAP") >= 0 Then .SetParameterValue("@OptBAP", Chr(254).ToString)
                                    If Array.IndexOf(RsLoad("SyaratPembayaran").Split(","), "BAOP") >= 0 Then .SetParameterValue("@OptBAPP", Chr(254).ToString)
                                    .SetParameterValue("@SyaratTeknis", RsLoad("SyaratTeknis").ToString)
                                    .SetParameterValue("@JadwalPengiriman", RsLoad("JadwalPengiriman").ToString)
                                    .SetParameterValue("@JadwalPembayaran", RsLoad("JadwalPembayaran").ToString)
                                    .SetParameterValue("@Sanksi", RsLoad("Sanksi").ToString)
                                    .SetParameterValue("@Keterangan", RsLoad("Keterangan").ToString)
                                    .SetParameterValue("@VendorID", RsLoad("VendorId"))
                                    ImageFile = GenerateQRImage(RsLoad("NoKO"), RsLoad("QRCode").ToString)
                                    .SetParameterValue("@QRCode", ImageFile)

                                    Using CmdFind As New Data.SqlClient.SqlCommand
                                        With CmdFind
                                            .Connection = Conn
                                            .CommandType = CommandType.Text
                                            .CommandText = "SELECT * FROM Vendor WHERE VendorId=@P1"
                                            .Parameters.AddWithValue("@P1", RsLoad("VendorId"))
                                        End With
                                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                                            If RsFind.Read Then
                                                .SetParameterValue("@KategoriId", If(String.IsNullOrEmpty(RsFind("Kategori").ToString) = False, RsFind("Kategori").ToString, "-"))
                                                .SetParameterValue("@SubKategoriId", If(String.IsNullOrEmpty(RsFind("BidangUsaha").ToString) = False, RsFind("BidangUsaha").ToString, "-"))
                                                .SetParameterValue("@VendorNm", RsFind("VendorNm"))
                                                .SetParameterValue("@Alamat", RsFind("Alamat").ToString)
                                                .SetParameterValue("@Telepon", RsFind("Telepon1").ToString)
                                                .SetParameterValue("@NPWP", If(RsFind("NPWP").ToString = "", "-", RsFind("NPWP").ToString))
                                            End If
                                        End Using
                                    End Using

                                    .SetParameterValue("@Terbilang", "Terbilang : " + If(Jumlah = 0, "Nol Rupiah,", Trim(Terbilang(Jumlah)) + " Rupiah."))
                                End If
                            End Using

                        End With

                        CRViewer.ReportSource = Rpt
                        Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, False, "")

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
        Response.Redirect("FrmKO.aspx")
    End Sub

    Private Function GenerateQRImage(NoKO As String, code As String) As String
        Dim Image As String = ""
        Dim qrGenerator As New QRCoder.QRCodeGenerator()
        Dim qrCode As QRCoder.QRCodeGenerator.QRCode = qrGenerator.CreateQrCode(code, QRCoder.QRCodeGenerator.ECCLevel.Q)
        Using bitMap As System.Drawing.Bitmap = qrCode.GetGraphic(20)
            Image = Server.MapPath("~/TMP/") + NoKO + ".png"
            bitMap.Save(Image, System.Drawing.Imaging.ImageFormat.Png)
        End Using
        Return Image

    End Function

End Class