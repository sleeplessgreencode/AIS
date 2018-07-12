Public Class FrmCashFlow
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim RptCF As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "CashFlow") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            RptCF.Close()
            RptCF.Dispose()
            Session.Remove("RptCashFlow")
            Call BindJob()
            Call BindPeriode()
            DDLBulan.Value = If(Today.Month = 1, "12", (Today.Month - 1).ToString)
            TxtTahun.Value = If(Today.Month = 1, Today.Year - 1, Today.Year)
        Else
            CRViewer.ReportSource = Session("RptCashFlow")
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindJob()
        DDLJob.Items.Clear()
        DDLJob.Items.Add("All Job No", String.Empty)

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo, JobNm FROM Job"
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read                
                DDLJob.Items.Add(RsFind("JobNo") & " - " & RsFind("JobNm"), RsFind("JobNo"))
            End While
        End Using

        DDLJob.SelectedIndex = 0
    End Sub

    Private Sub BindPeriode()
        DDLBulan.Items.Clear()
        DDLBulan.Items.Add("Januari", "1")
        DDLBulan.Items.Add("Februari", "2")
        DDLBulan.Items.Add("Maret", "3")
        DDLBulan.Items.Add("April", "4")
        DDLBulan.Items.Add("Mei", "5")
        DDLBulan.Items.Add("Juni", "6")
        DDLBulan.Items.Add("Juli", "7")
        DDLBulan.Items.Add("Agustus", "8")
        DDLBulan.Items.Add("September", "9")
        DDLBulan.Items.Add("Oktober", "10")
        DDLBulan.Items.Add("November", "11")
        DDLBulan.Items.Add("Desember", "12")
    End Sub

    Private Sub BindReport()
        If TxtTahun.Text = "0" Then
            msgBox1.alert("Tahun belum diisi.")
            TxtTahun.Focus()
            Exit Sub
        End If

        Dim PrdAwal As Date = DateSerial(TxtTahun.Text, DDLBulan.Value, 1)
        Dim PrdAkhir As Date = If(DDLBulan.Value = 12, DateSerial(TxtTahun.Text + 1, 1, 1).AddDays(-1), DateSerial(TxtTahun.Text, DDLBulan.Value + 1, 1).AddDays(-1))

        Dim JobNm As String = String.Empty
        Dim NettKontrak As Decimal = 0, TerminShare As Decimal = 0, CadanganKSO As Decimal = 0, _
            Alokasi_A As Decimal = 0, Alokasi_B As Decimal = 0, Alokasi_C As Decimal = 0, Alokasi_F As Decimal = 0, Alokasi_G As Decimal = 0, _
            Alokasi_H As Decimal = 0, Alokasi_I As Decimal = 0, Alokasi_T As Decimal = 0, Alokasi_D As Decimal = 0, Alokasi_E As Decimal = 0, _
            Alokasi_L As Decimal = 0, HutangProc As Decimal = 0

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                If DDLJob.Value = String.Empty Then
                    .CommandText = "SELECT * FROM Job WHERE StatusJob='Pelaksanaan'"
                Else
                    .CommandText = "SELECT * FROM Job WHERE StatusJob='Pelaksanaan' AND JobNo='" & DDLJob.Value & "'"
                End If
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    JobNm = If(DDLJob.Value = String.Empty, "KONSOLIDASI", RsFind("JobNo") & " - " & UCase(RsFind("JobNm")))

                    'Nett Kontrak Porsi
                    If RsFind("KSO") = "1" Then
                        If RsFind("TipeManajerial") = "JO Partial" Then
                            NettKontrak += RsFind("Netto") - (RsFind("Netto") * (RsFind("PersenKSO") / 100))
                        Else
                            NettKontrak += RsFind("Netto")
                        End If
                    Else
                        NettKontrak += RsFind("Netto")
                    End If

                    'Nett Revenue
                    Using CmdTermin As New Data.SqlClient.SqlCommand
                        With CmdTermin
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            If RsFind("TipeManajerial").ToString = "JO Partial" Then
                                If RsFind("Own").ToString = "1" Then
                                    .CommandText = "SELECT ISNULL(SUM(TerminMember1),0), ISNULL(SUM(CadanganKSO),0) FROM TerminMember WHERE JobNo=@P1 AND TglCair<=@P2"
                                Else
                                    .CommandText = "SELECT ISNULL(SUM(TerminMember2),0), ISNULL(SUM(CadanganKSO),0) FROM TerminMember WHERE JobNo=@P1 AND TglCair<=@P2"
                                End If
                            Else
                                .CommandText = "SELECT ISNULL(SUM(TerminInduk),0) FROM TerminInduk WHERE JobNo=@P1 AND TglCair<=@P2"
                            End If
                            .Parameters.AddWithValue("@P1", RsFind("JobNo"))
                            .Parameters.AddWithValue("@P2", PrdAkhir)
                        End With
                        Using RsTermin As Data.SqlClient.SqlDataReader = CmdTermin.ExecuteReader
                            If RsTermin.Read Then
                                If RsFind("TipeManajerial").ToString = "JO Partial" Then
                                    TerminShare = RsTermin(0)
                                    CadanganKSO = RsTermin(1)
                                Else
                                    TerminShare = RsTermin(0) - (RsTermin(0) / 1.1) * (10 / 100) - (RsTermin(0) / 1.1) * (3 / 100)
                                End If
                            End If
                        End Using
                    End Using

                    'Cost Job Number
                    Using CmdAlokasi As New Data.SqlClient.SqlCommand
                        With CmdAlokasi
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT Alokasi, SUM(Amount) FROM BLE WHERE JobNo=@P1 AND TglBayar<=@P2 GROUP BY Alokasi"
                            .Parameters.AddWithValue("@P1", RsFind("JobNo"))
                            .Parameters.AddWithValue("@P2", PrdAkhir)
                        End With
                        Using RsAlokasi As Data.SqlClient.SqlDataReader = CmdAlokasi.ExecuteReader
                            While RsAlokasi.Read
                                If RsAlokasi(0) = "A" Then Alokasi_A = RsAlokasi(1)
                                If RsAlokasi(0) = "B" Then Alokasi_B = RsAlokasi(1)
                                If RsAlokasi(0) = "C" Then Alokasi_C = RsAlokasi(1)
                                If RsAlokasi(0) = "F" Then Alokasi_F = RsAlokasi(1)
                                If RsAlokasi(0) = "G" Then Alokasi_G = RsAlokasi(1)
                                If RsAlokasi(0) = "H" Then Alokasi_H = RsAlokasi(1)
                                If RsAlokasi(0) = "I" Then Alokasi_I = RsAlokasi(1)
                                If RsAlokasi(0) = "T" Then Alokasi_T = RsAlokasi(1)
                            End While
                        End Using
                    End Using

                    'Hutang Procurement
                    Using CmdDebt As New Data.SqlClient.SqlCommand
                        With CmdDebt
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "WITH TmpQuery AS " & _
                                           "(SELECT JobNo,Subtotal - DiscAmount + PPN AS TotalKO, " & _
                                           "(SELECT ISNULL(SUM(Amount),0) FROM BLE WHERE BLE.NoKo=KoHdr.NoKo AND BLE.TglBayar <= '" & PrdAkhir & "') AS Payment " & _
                                           "FROM KoHdr WHERE CanceledBy IS NULL AND ApprovedBy IS NOT NULL AND JobNo='" & RsFind("JobNo") & "')" & _
                                           "SELECT ISNULL(SUM(TotalKO) - SUM(Payment),0) AS ProcDebt FROM TmpQuery "
                        End With
                        Using RsDebt As Data.SqlClient.SqlDataReader = CmdDebt.ExecuteReader
                            While RsDebt.Read
                                HutangProc += RsDebt("ProcDebt")
                            End While
                        End Using
                    End Using
                End While
            End Using
        End Using

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT Alokasi, SUM(Amount) FROM BLE WHERE TglBayar<=@P1 AND Alokasi IN ('D','E','L') GROUP BY Alokasi"
                .Parameters.AddWithValue("@P1", PrdAkhir)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    If RsFind(0) = "D" Then Alokasi_D = RsFind(1)
                    If RsFind(0) = "E" Then Alokasi_E = RsFind(1)
                    If RsFind(0) = "L" Then Alokasi_L = RsFind(1)
                End While
            End Using
        End Using

        If DDLJob.Value <> String.Empty Then
            Dim TtlNettKontrak As Decimal = 0, NettKontrak1 As Decimal = 0
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM Job WHERE StatusJob='Pelaksanaan'"
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    While RsFind.Read
                        'Nett Kontrak Porsi
                        If RsFind("KSO") = "1" Then
                            If RsFind("TipeManajerial") = "JO Partial" Then
                                If RsFind("JobNo") = DDLJob.Value Then NettKontrak1 = RsFind("Netto") - (RsFind("Netto") * (RsFind("PersenKSO") / 100))
                                TtlNettKontrak += RsFind("Netto") - (RsFind("Netto") * (RsFind("PersenKSO") / 100))
                            Else
                                If RsFind("JobNo") = DDLJob.Value Then NettKontrak1 = RsFind("Netto")
                                TtlNettKontrak += RsFind("Netto")
                            End If
                        Else
                            If RsFind("JobNo") = DDLJob.Value Then NettKontrak1 = RsFind("Netto")
                            TtlNettKontrak += RsFind("Netto")
                        End If
                    End While
                End Using
            End Using
            Alokasi_D = (NettKontrak1 / TtlNettKontrak) * Alokasi_D
            Alokasi_E = (NettKontrak1 / TtlNettKontrak) * Alokasi_E
            Alokasi_L = (NettKontrak1 / TtlNettKontrak) * Alokasi_L
        End If

        RptCF.Load(Server.MapPath("~/Report/RptCashFlow.rpt"))
        With RptCF
            .SetParameterValue("@Periode", DDLBulan.Text & " - " & TxtTahun.Text)
            .SetParameterValue("@PrintInfo", "Printed On " + Format(Now, "dd-MMM-yyyy HH:mm") + " By " + Session("User").ToString.Split("|")(0))
            .SetParameterValue("@JobNm", JobNm)
            .SetParameterValue("@NettKontrak", NettKontrak)
            .SetParameterValue("@TerminShare", TerminShare)
            .SetParameterValue("@CadanganKSO", CadanganKSO)
            .SetParameterValue("@Alokasi_A", Alokasi_A)
            .SetParameterValue("@Alokasi_B", Alokasi_B)
            .SetParameterValue("@Alokasi_C", Alokasi_C)
            .SetParameterValue("@Alokasi_F", Alokasi_F)
            .SetParameterValue("@Alokasi_G", Alokasi_G)
            .SetParameterValue("@Alokasi_H", Alokasi_H)
            .SetParameterValue("@Alokasi_I", Alokasi_I)
            .SetParameterValue("@Alokasi_T", Alokasi_T)
            .SetParameterValue("@Alokasi_D", Alokasi_D)
            .SetParameterValue("@Alokasi_E", Alokasi_E)
            .SetParameterValue("@Alokasi_L", Alokasi_L)
            .SetParameterValue("@HutangProc", HutangProc)

        End With

        Session("RptCashFlow") = RptCF

        With CRViewer
            .ReportSource = RptCF
            .Zoom(100)
        End With

    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As System.EventArgs) Handles BtnPrint.Click
        Call BindReport()
    End Sub

End Class