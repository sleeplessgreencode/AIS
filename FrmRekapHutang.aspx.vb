Public Class FrmRekapHutang
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
            Session.Remove("RekapHutang")
            Call BindJob()
        Else
            CRViewer.ReportSource = Session("RekapHutang")
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
        DDLJob.Items.Add(String.Empty, String.Empty)
        DDLJob.Items.Add("All Job", "All")
        Using CmdIsi As New Data.SqlClient.SqlCommand
            With CmdIsi
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo,JobNm FROM Job WHERE Kategori='Primary'"
            End With
            Using RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
                While RsIsi.Read
                    If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsIsi("JobNo")) >= 0 Then
                        DDLJob.Items.Add(RsIsi("JobNo") & " - " & RsIsi("JobNm"), RsIsi("JobNo"))
                    End If
                End While
            End Using
        End Using

        DDLJob.SelectedIndex = 0

    End Sub

    Protected Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Call BindReport()
    End Sub

    Private Sub BindReport()
        Dim TmpDt1 As New DataTable()
        TmpDt1.Columns.AddRange(New DataColumn() {New DataColumn("JobNo", GetType(String)), _
                                                  New DataColumn("JobNm", GetType(String)), _
                                                  New DataColumn("NettKontrak", GetType(Decimal)), _
                                                  New DataColumn("TipeManajerial", GetType(String)), _
                                                  New DataColumn("PersenShare", GetType(Decimal)), _
                                                  New DataColumn("NominalShare", GetType(Decimal)), _
                                                  New DataColumn("RAP", GetType(Decimal)), _
                                                  New DataColumn("KO", GetType(Decimal)), _
                                                  New DataColumn("Invoice", GetType(Decimal)), _
                                                  New DataColumn("InvPD", GetType(Decimal)), _
                                                  New DataColumn("BLE_KO", GetType(Decimal)), _
                                                  New DataColumn("BLE_PD", GetType(Decimal))})

        Dim TtlKO As Decimal = 0, TtlPayKO As Decimal = 0, TtlInvoice As Decimal = 0, _
            TtlInvPD As Decimal = 0, TtlPayInvPD As Decimal = 0, TtlRAP As Decimal = 0
        Dim PersenKSO As Decimal = 0
        Dim AksesJob As String = String.Empty
        Dim CSV As String = String.Empty

        If DDLJob.Value = "All" Then
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
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT JobNo FROM Job WHERE Kategori='Primary'"
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    While RsFind.Read
                        If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsFind("JobNo").ToString) > -1 Then
                            CSV = If(CSV = "", "'" & RsFind("JobNo") & "'", CSV & "," & "'" & RsFind("JobNo") & "'")
                        End If
                    End While
                End Using
            End Using
        Else
            CSV = "'" & DDLJob.Value & "'"
        End If

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Job WHERE JobNo IN (" & CSV & ") ORDER BY JobNo DESC"
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    Using CmdFind1 As New Data.SqlClient.SqlCommand
                        With CmdFind1
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT ISNULL(SUM(SubTotal-DiscAmount+PPN),0) FROM KoHdr WHERE JobNo=@P1 " & _
                                           "AND ApprovedBy IS NOT NULL AND CanceledBy IS NULL"
                            .Parameters.AddWithValue("@P1", RsFind("JobNo"))
                        End With
                        Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                            If RsFind1.Read Then
                                TtlKO = RsFind1(0)
                            End If
                        End Using
                    End Using
                    Using CmdFind1 As New Data.SqlClient.SqlCommand
                        With CmdFind1
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT ISNULL(SUM(Amount),0) FROM BLE A " & _
                                "JOIN KoHdr B ON A.NoKO=B.NoKO AND B.ApprovedBy IS NOT NULL AND B.CanceledBy IS NULL " & _
                                "WHERE A.JobNo=@P1 AND A.NoKO LIKE 'KO" & RsFind("JobNo") & "%'"
                            .Parameters.AddWithValue("@P1", RsFind("JobNo"))
                        End With
                        Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                            If RsFind1.Read Then
                                TtlPayKO = RsFind1(0)
                            End If
                        End Using
                    End Using
                    Using CmdFind1 As New Data.SqlClient.SqlCommand
                        With CmdFind1
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT ISNULL(SUM(Total),0) FROM Invoice A " & _
                                "JOIN KoHdr B ON A.NoKO=B.NoKO AND B.ApprovedBy IS NOT NULL AND B.CanceledBy IS NULL " & _
                                "WHERE A.JobNo=@P1"
                            .Parameters.AddWithValue("@P1", RsFind("JobNo"))
                        End With
                        Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                            If RsFind1.Read Then
                                TtlInvoice = RsFind1(0)
                            End If
                        End Using
                    End Using
                    Using CmdFind1 As New Data.SqlClient.SqlCommand
                        With CmdFind1
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT ISNULL(SUM(PaymentAmount),0) FROM InvPD A " & _
                                "JOIN KoHdr B ON A.NoKO=B.NoKO AND B.ApprovedBy IS NOT NULL AND B.CanceledBy IS NULL " & _
                                "WHERE A.JobNo=@P1"
                            .Parameters.AddWithValue("@P1", RsFind("JobNo"))
                        End With
                        Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                            If RsFind1.Read Then
                                TtlInvPD += RsFind1(0)
                            End If
                        End Using
                    End Using
                    Using CmdFind1 As New Data.SqlClient.SqlCommand
                        With CmdFind1
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT ISNULL(SUM(PaymentAmount),0) FROM BLE, InvPD, PdHdr, KoHdr WHERE " & _
                                           "(BLE.NoPD=PdHdr.NoPD OR BLE.NoPD=PdHdr.NoRef) AND PdHdr.NoKO LIKE 'KO" & RsFind("JobNo") & "%' " & _
                                           "AND InvPD.NoPD=PdHdr.NoPD AND KoHdr.NoKO=PdHdr.NoKO AND KoHdr.ApprovedBy IS NOT NULL AND KoHdr.CanceledBy IS NULL"
                        End With
                        Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                            If RsFind1.Read Then
                                TtlPayInvPD += RsFind1(0)
                            End If
                        End Using
                    End Using
                    
                    Using CmdFind1 As New Data.SqlClient.SqlCommand
                        With CmdFind1
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT ISNULL(SUM(Vol*HrgSatuan),0) FROM RAP WHERE JobNo=@P1 AND Alokasi='B' AND Tipe='Detail'"
                            .Parameters.AddWithValue("@P1", RsFind("JobNo"))
                        End With
                        Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                            If RsFind1.Read Then
                                TtlRAP = RsFind1(0)
                            End If
                        End Using
                    End Using

                    PersenKSO = If(RsFind("PersenKSO") = 0, 100, RsFind("PersenKSO"))

                    TmpDt1.Rows.Add(RsFind("JobNo"), RsFind("JobNm"), RsFind("Netto") / 1000, RsFind("TipeManajerial"), _
                                   PersenKSO, (RsFind("Netto") * PersenKSO / 100) / 1000, TtlRAP / 1000, TtlKO / 1000, _
                                   TtlInvoice / 1000, TtlInvPD / 1000, TtlPayKO / 1000, TtlPayInvPD / 1000)
                    TtlRAP = 0
                    TtlKO = 0
                    TtlInvoice = 0
                    TtlInvPD = 0
                    TtlPayKO = 0
                    TtlPayInvPD = 0

                End While
            End Using
        End Using

        Rpt.Load(Server.MapPath("~/Report/RptRekapHutang1.rpt"))
        With Rpt
            .SetDataSource(TmpDt1)
            '.SetParameterValue("@Job", DDLJob.Text)
            .SetParameterValue("@PrintInfo", "Printed On " + Format(Now, "dd-MMM-yyyy HH:mm") + " By " + Session("User").ToString.Split("|")(0))
        End With

        Session("RekapHutang") = Rpt

        With CRViewer
            .ReportSource = Rpt
            .Zoom(100)
        End With

        TmpDt1.Dispose()

    End Sub

End Class