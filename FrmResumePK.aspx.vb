Public Class FrmResumePK
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim TmpDt As New DataTable()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "ResumePK") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Call BindJob()
            Call BindPeriode()
            DDLBulan.Value = Today.Month.ToString
            TxtTahun.Value = Today.Year

            TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("AccNo", GetType(String)), _
                                                     New DataColumn("AccName", GetType(String)), _
                                                     New DataColumn("SaldoKSO", GetType(Decimal)), _
                                                     New DataColumn("SaldoMember1", GetType(Decimal)), _
                                                     New DataColumn("SaldoMember2", GetType(Decimal))})

            Session("TmpDt") = TmpDt
            GridView.DataSource = TmpDt
            GridView.DataBind()

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
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                AksesJob = RsFind("AksesJob")
            End If
        End Using

        DDLJob.Items.Clear()
        DDLJob.Items.Add(String.Empty, String.Empty)
        DDLJob.Items.Add("Konsolidasi MN", "All_MN")
        DDLJob.Items.Add("Konsolidasi All", "All")

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo, JobNm FROM Job"
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsFind("JobNo")) >= 0 Then
                    DDLJob.Items.Add(RsFind("JobNo") & " - " & RsFind("JobNm"), RsFind("JobNo"))
                End If
            End While
        End Using

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

    Private Sub BtnProcess_Click(sender As Object, e As System.EventArgs) Handles BtnProcess.Click
        If DDLJob.Value = String.Empty Then
            msgBox1.alert("Belum pilih Job.")
            DDLJob.Focus()
            Exit Sub
        End If
        If TxtTahun.Text = "0" Then
            msgBox1.alert("Tahun belum diisi.")
            TxtTahun.Focus()
            Exit Sub
        End If

        Dim PrdAwal As Date = DateSerial(TxtTahun.Text, DDLBulan.Value, 1)
        Dim PrdAkhir As Date = If(DDLBulan.Value = 12, DateSerial(TxtTahun.Text + 1, 1, 1).AddDays(-1), DateSerial(TxtTahun.Text, DDLBulan.Value + 1, 1).AddDays(-1))
        Dim Tahun As Integer = PrdAwal.Year
        Dim Bulan As Integer = PrdAwal.Month
        Dim DebetTahunKSO As Decimal, KreditTahunKSO As Decimal, DebetBulanKSO As Decimal, KreditBulanKSO As Decimal
        Dim DebetTahunMember1 As Decimal, KreditTahunMember1 As Decimal, DebetBulanMember1 As Decimal, KreditBulanMember1 As Decimal
        Dim DebetTahunMember2 As Decimal, KreditTahunMember2 As Decimal, DebetBulanMember2 As Decimal, KreditBulanMember2 As Decimal
        Dim SaldoKSO As Decimal, SaldoMember1 As Decimal, SaldoMember2 As Decimal
        Dim LBTBKso As Decimal, LBTBMember1 As Decimal, LBTBMember2 As Decimal 'Laba Bersih Tahun Berjalan
        Dim IRLKso As Decimal, IRLMember1 As Decimal, IRLMember2 As Decimal 'Summary Akun 4
        Dim IIRLKso As Decimal, IIRLMember1 As Decimal, IIRLMember2 As Decimal 'Summary Akun 5,6,7

        TmpDt = Session("TmpDt")
        TmpDt.Rows.Clear()

        Using CmdReport As New Data.SqlClient.SqlCommand
            With CmdReport
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM COA"
            End With
            Using RsLoad As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
                While RsLoad.Read
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            If DDLJob.Value = "All_MN" Then
                                .CommandText = "SELECT * FROM JurnalEntry WHERE Site='Member2' AND Member='MN' AND AccNo=@P2"
                            ElseIf DDLJob.Value = "All" Then
                                .CommandText = "SELECT * FROM JurnalEntry WHERE AccNo=@P2"
                            Else
                                .CommandText = "SELECT * FROM JurnalEntry WHERE JobNo=@P1 AND AccNo=@P2"
                            End If
                            .Parameters.AddWithValue("@P1", DDLJob.Value)
                            .Parameters.AddWithValue("@P2", RsLoad("AccNo"))
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            While RsFind.Read
                                If RsFind("Site") = "KSO" Then
                                    If DDLView.Value = "0" Then
                                        If RsFind("Tahun") = Tahun And RsFind("Bulan") = Bulan Then
                                            DebetBulanKSO += RsFind("Debet")
                                            KreditBulanKSO += RsFind("Kredit")
                                        End If
                                    Else
                                        If RsFind("Tahun") < Tahun Then
                                            DebetTahunKSO += RsFind("Debet")
                                            KreditTahunKSO += RsFind("Kredit")
                                        ElseIf RsFind("Tahun") = Tahun And RsFind("Bulan") <= Bulan Then
                                            DebetBulanKSO += RsFind("Debet")
                                            KreditBulanKSO += RsFind("Kredit")
                                        End If
                                    End If
                                ElseIf RsFind("Site") = "Member1" Then
                                    If DDLView.Value = "0" Then
                                        If RsFind("Tahun") = Tahun And RsFind("Bulan") = Bulan Then
                                            DebetBulanMember1 += RsFind("Debet")
                                            KreditBulanMember1 += RsFind("Kredit")
                                        End If
                                    Else
                                        If RsFind("Tahun") < Tahun Then
                                            DebetTahunMember1 += RsFind("Debet")
                                            KreditTahunMember1 += RsFind("Kredit")
                                        ElseIf RsFind("Tahun") = Tahun And RsFind("Bulan") <= Bulan Then
                                            DebetBulanMember1 += RsFind("Debet")
                                            KreditBulanMember1 += RsFind("Kredit")
                                        End If
                                    End If
                                ElseIf RsFind("Site") = "Member2" Then
                                    If DDLView.Value = "0" Then
                                        If RsFind("Tahun") = Tahun And RsFind("Bulan") = Bulan Then
                                            DebetBulanMember2 += RsFind("Debet")
                                            KreditBulanMember2 += RsFind("Kredit")
                                        End If
                                    Else
                                        If RsFind("Tahun") < Tahun Then
                                            DebetTahunMember2 += RsFind("Debet")
                                            KreditTahunMember2 += RsFind("Kredit")
                                        ElseIf RsFind("Tahun") = Tahun And RsFind("Bulan") <= Bulan Then
                                            DebetBulanMember2 += RsFind("Debet")
                                            KreditBulanMember2 += RsFind("Kredit")
                                        End If
                                    End If
                                End If
                            End While
                        End Using
                    End Using

                    If Left(RsLoad("AccNo"), 1) = "2" Or Left(RsLoad("AccNo"), 1) = "3" Or Left(RsLoad("AccNo"), 1) = "4" Then
                        If RsLoad("AccNo") <> "3100.002" Then
                            SaldoKSO = KreditTahunKSO - DebetTahunKSO + KreditBulanKSO - DebetBulanKSO
                            SaldoMember1 = KreditTahunMember1 - DebetTahunMember1 + KreditBulanMember1 - DebetBulanMember1
                            SaldoMember2 = KreditTahunMember2 - DebetTahunMember2 + KreditBulanMember2 - DebetBulanMember2
                        End If
                    Else
                        SaldoKSO = DebetTahunKSO - KreditTahunKSO + DebetBulanKSO - KreditBulanKSO
                        SaldoMember1 = DebetTahunMember1 - KreditTahunMember1 + DebetBulanMember1 - KreditBulanMember1
                        SaldoMember2 = DebetTahunMember2 - KreditTahunMember2 + DebetBulanMember2 - KreditBulanMember2
                    End If

                    If Left(RsLoad("AccNo"), 1) = "1" Then
                        LBTBKso += SaldoKSO
                        LBTBMember1 += SaldoMember1
                        LBTBMember2 += SaldoMember2
                    ElseIf Left(RsLoad("AccNo"), 1) = "2" Or Left(RsLoad("AccNo"), 1) = "3" Then
                        LBTBKso -= SaldoKSO
                        LBTBMember1 -= SaldoMember1
                        LBTBMember2 -= SaldoMember2
                    ElseIf Left(RsLoad("AccNo"), 1) = "4" Then
                        IRLKso += SaldoKSO
                        IRLMember1 += SaldoMember1
                        IRLMember2 += SaldoMember2
                    ElseIf Left(RsLoad("AccNo"), 1) = "5" Or Left(RsLoad("AccNo"), 1) = "6" Or Left(RsLoad("AccNo"), 1) = "7" Then
                        IIRLKso += SaldoKSO
                        IIRLMember1 += SaldoMember1
                        IIRLMember2 += SaldoMember2
                    End If

                    If RsLoad("AccNo") = "3100.002" Then
                        TmpDt.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), 0, 0, 0)
                    ElseIf RsLoad("AccNo") = "8001.001" Then
                        TmpDt.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), IRLKso - IIRLKso, IRLMember1 - IIRLMember1, IRLMember2 - IIRLMember2)
                        Dim result As DataRow = TmpDt.Select("AccNo='3100.002'").FirstOrDefault
                        If result IsNot Nothing Then
                            result("SaldoKSO") = IRLKso - IIRLKso
                            result("SaldoMember1") = IRLMember1 - IIRLMember1
                            result("SaldoMember2") = IRLMember2 - IIRLMember2
                        End If
                    Else
                        If RsLoad("AccNo") <> "3100.002" Then
                            TmpDt.Rows.Add(RsLoad("AccNo"), RsLoad("AccName"), SaldoKSO, SaldoMember1, SaldoMember2)
                        End If

                    End If

                    DebetTahunKSO = 0
                    KreditTahunKSO = 0
                    DebetBulanKSO = 0
                    KreditBulanKSO = 0
                    SaldoKSO = 0
                    DebetTahunMember1 = 0
                    KreditTahunMember1 = 0
                    DebetBulanMember1 = 0
                    KreditBulanMember1 = 0
                    SaldoMember1 = 0
                    DebetTahunMember2 = 0
                    KreditTahunMember2 = 0
                    DebetBulanMember2 = 0
                    KreditBulanMember2 = 0
                    SaldoMember2 = 0

                End While

            End Using
        End Using

        GridView.DataSource = TmpDt
        GridView.DataBind()

    End Sub

End Class