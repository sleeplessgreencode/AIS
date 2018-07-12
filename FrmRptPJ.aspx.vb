Public Class FrmRptPJ
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "PJ") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Call BindReport()

    End Sub

    Private Sub BindReport()
        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.NoPD AS NoPD, A.*, B.*, C.JobNm " & _
                                "FROM (PdHdr A " & _
                                "LEFT JOIN PdDtl B ON A.NoPD = B.NoPD) " & _
                                "LEFT JOIN Job C ON C.JobNo = A.JobNo " & _
                                "WHERE A.NoPD=@P1"
                .Parameters.AddWithValue("@P1", Session("Print").ToString.Split("|")(0))
            End With
            Using DaReport As New Data.SqlClient.SqlDataAdapter
                DaReport.SelectCommand = CmdReport
                Using DtReport As New Data.DataTable
                    DaReport.Fill(DtReport)

                    Using Rpt As New RptPJ
                        With Rpt
                            .SetDataSource(DtReport)
                            .SetParameterValue("@vNoPJ", "-")
                            .SetParameterValue("@vSaldo", 0)
                            .SetParameterValue("@OptInvoice", Chr(168).ToString)
                            .SetParameterValue("@OptSJ", Chr(168).ToString)
                            .SetParameterValue("@OptCopy", Chr(168).ToString)
                            .SetParameterValue("@OptFP", Chr(168).ToString)
                            .SetParameterValue("@OptBAP", Chr(168).ToString)
                            .SetParameterValue("@OptBAPP", Chr(168).ToString)
                            .SetParameterValue("@Tunai", Chr(168).ToString)
                            .SetParameterValue("@Transfer", Chr(168).ToString)
                            .SetParameterValue("@CG", Chr(168).ToString)
                            .SetParameterValue("@NoRekKirim", "-")
                            .SetParameterValue("@AtasNmKirim", "-")
                            .SetParameterValue("@BankKirim", "-")
                            .SetParameterValue("@NoRekTerima", "-")
                            .SetParameterValue("@AtasNmTerima", "-")
                            .SetParameterValue("@BankTerima", "-")
                            .SetParameterValue("@NoCG", "-")
                            .SetParameterValue("@TglCG", "-")
                            .SetParameterValue("@JKT", Chr(168).ToString)
                            .SetParameterValue("@MKS", Chr(168).ToString)
                            .SetParameterValue("@LAP", Chr(168).ToString)
                            .SetParameterValue("@NmPenerimaTunai", "-")
                            .SetParameterValue("@OptKliring", Chr(168).ToString)
                            .SetParameterValue("@OptOnline", Chr(168).ToString)
                            .SetParameterValue("@OptBNI", Chr(168).ToString)
                            .SetParameterValue("@OptRtgs", Chr(168).ToString)
                            .SetParameterValue("@VendorID", "-")
                            .SetParameterValue("@TglBayar", "-")
                            .SetParameterValue("@NoRef", "-")
                            Using RsLoad As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                                If RsLoad.Read Then
                                    .SetParameterValue("@NoPD", RsLoad("NoPD"))
                                    .SetParameterValue("@Barcode", "*" & RsLoad("NoPD") & "*")
                                    .SetParameterValue("@NoRef", If(String.IsNullOrEmpty(RsLoad("NoRef").ToString) = True, "-", RsLoad("NoRef")))
                                    .SetParameterValue("@NoPJ", RsLoad("NoPJ"))

                                    If Array.IndexOf(RsLoad("BuktiPendukung").Split(","), "INV") >= 0 Then .SetParameterValue("@OptInvoice", Chr(254).ToString)
                                    If Array.IndexOf(RsLoad("BuktiPendukung").Split(","), "SJ") >= 0 Then .SetParameterValue("@OptSJ", Chr(254).ToString)
                                    If Array.IndexOf(RsLoad("BuktiPendukung").Split(","), "PO") >= 0 Then .SetParameterValue("@OptCopy", Chr(254).ToString)
                                    If Array.IndexOf(RsLoad("BuktiPendukung").Split(","), "FP") >= 0 Then .SetParameterValue("@OptFP", Chr(254).ToString)
                                    If Array.IndexOf(RsLoad("BuktiPendukung").Split(","), "BAP") >= 0 Then .SetParameterValue("@OptBAP", Chr(254).ToString)
                                    If Array.IndexOf(RsLoad("BuktiPendukung").Split(","), "BAOP") >= 0 Then .SetParameterValue("@OptBAPP", Chr(254).ToString)
                                    .SetParameterValue("@Nama", RsLoad("Nama"))
                                    .SetParameterValue("@Alamat", RsLoad("Alamat").ToString)
                                    .SetParameterValue("@Telepon", RsLoad("Telepon").ToString)
                                    .SetParameterValue("@NPWP", If(String.IsNullOrEmpty(RsLoad("NPWP").ToString) = True, "-", RsLoad("NPWP").ToString))
                                    .SetParameterValue("@NoRekTerima", RsLoad("NoRek"))
                                    .SetParameterValue("@AtasNmTerima", RsLoad("AtasNama"))
                                    .SetParameterValue("@BankTerima", RsLoad("Bank"))

                                    Using CmdFind As New Data.SqlClient.SqlCommand
                                        With CmdFind
                                            .Connection = Conn
                                            .CommandType = CommandType.Text
                                            .CommandText = "SELECT TOP 1 * FROM BLE WHERE NoPD=@P1 OR NoPD=@P2 ORDER BY TimeEntry DESC"
                                            .Parameters.AddWithValue("@P1", RsLoad("NoPD"))
                                            .Parameters.AddWithValue("@P2", RsLoad("NoRef"))
                                        End With
                                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                                            If RsFind.Read Then
                                                .SetParameterValue("@TglBayar", If(IsDBNull(RsFind("TglBayar")) = True, "-", Format(RsFind("TglBayar"), "dd-MMM-yyyy")))
                                                If RsFind("CaraBayar").ToString = "CASH" Then
                                                    .SetParameterValue("@Tunai", Chr(254).ToString)
                                                    .SetParameterValue("@JKT", If(RsFind("SumberKas") = "JKT", Chr(254).ToString, Chr(168).ToString))
                                                    .SetParameterValue("@MKS", If(RsFind("SumberKas") = "MKS", Chr(254).ToString, Chr(168).ToString))
                                                    .SetParameterValue("@LAP", If(RsFind("SumberKas") = "LAP", Chr(254).ToString, Chr(168).ToString))
                                                    .SetParameterValue("@NmPenerimaTunai", RsFind("NmPenerimaTunai"))
                                                    If String.IsNullOrEmpty(RsFind("NmPenerimaTunai")) = False Then
                                                        .SetParameterValue("@NoRekTerima", "-")
                                                        .SetParameterValue("@AtasNmTerima", "-")
                                                        .SetParameterValue("@BankTerima", "-")
                                                    End If
                                                Else
                                                    Using CmdFindx As New Data.SqlClient.SqlCommand
                                                        With CmdFindx
                                                            .Connection = Conn
                                                            .CommandType = CommandType.Text
                                                            .CommandText = "SELECT * FROM Rekening WHERE RekId=@P1"
                                                            .Parameters.AddWithValue("@P1", RsFind("RekId"))
                                                        End With
                                                        Using RsFindx As Data.SqlClient.SqlDataReader = CmdFindx.ExecuteReader
                                                            If RsFindx.Read Then
                                                                .SetParameterValue("@NoRekKirim", RsFindx("NoRek"))
                                                                .SetParameterValue("@AtasNmKirim", RsFindx("AtasNama"))
                                                                .SetParameterValue("@BankKirim", RsFindx("Bank"))
                                                            End If
                                                        End Using
                                                    End Using

                                                    If RsFind("CaraBayar").ToString = "TRF" Then
                                                        .SetParameterValue("@Transfer", Chr(254).ToString)
                                                        .SetParameterValue("@OptKliring", If(RsFind("JenisTrf") = "Kliring", Chr(254).ToString, Chr(168).ToString))
                                                        .SetParameterValue("@OptOnline", If(RsFind("JenisTrf") = "Online", Chr(254).ToString, Chr(168).ToString))
                                                        .SetParameterValue("@OptBNI", If(RsFind("JenisTrf") = "Sesama BNI", Chr(254).ToString, Chr(168).ToString))
                                                        .SetParameterValue("@OptRtgs", If(RsFind("JenisTrf") = "RTGS", Chr(254).ToString, Chr(168).ToString))
                                                    ElseIf RsFind("CaraBayar").ToString = "CG" Then
                                                        .SetParameterValue("@CG", Chr(254).ToString)
                                                        .SetParameterValue("@NoCG", RsFind("NoCG"))
                                                        .SetParameterValue("@TglCG", Format(RsFind("TglBayar"), "dd-MMM-yyyy"))
                                                    End If
                                                End If
                                            End If
                                        End Using
                                    End Using

                                    Using CmdFind1 As New Data.SqlClient.SqlCommand
                                        With CmdFind1
                                            .Connection = Conn
                                            .CommandType = CommandType.Text
                                            .CommandText = "SELECT NoPJ,Saldo FROM PdHdr WHERE NoPD<@P1 AND JobNo=@P2 AND TipeForm=@P3 AND KSO='" & _
                                                           RsLoad("KSO").ToString & "' ORDER BY NoPd DESC"
                                            .Parameters.AddWithValue("@P1", RsLoad("NoPD"))
                                            .Parameters.AddWithValue("@P2", RsLoad("JobNo"))
                                            .Parameters.AddWithValue("@P3", RsLoad("TipeForm"))
                                        End With
                                        Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                                            If RsFind1.Read Then
                                                .SetParameterValue("@vNoPJ", RsFind1("NoPJ").ToString)
                                                .SetParameterValue("@vSaldo", RsFind1("Saldo"))
                                            End If
                                        End Using
                                    End Using

                                End If
                            End Using

                            .SetParameterValue("@PrintInfo", "Printed On " + Format(Now, "dd-MMM-yyyy HH:mm") + " By " + Session("User").ToString.Split("|")(0))
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
        Response.Redirect("FrmPJ.aspx")
    End Sub
End Class