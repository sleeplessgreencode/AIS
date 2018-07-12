Public Class FrmQueryTermin
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim CSV As String = String.Empty
    Dim AksesJob As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "QueryTermin") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then           
            Call BindJob()
            TxtTahun.Value = Today.Year
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindAkses()
        AksesJob = String.Empty

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
    End Sub

    Private Sub BindJob()
        Call BindAkses()

        DDLJob.Items.Clear()
        DDLJob.Items.Add(String.Empty, String.Empty)
        DDLJob.Items.Add("All Job", "All")
        Using CmdIsi As New Data.SqlClient.SqlCommand
            With CmdIsi
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo,JobNm FROM Job"
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

    Private Sub BindCSV()
        AksesJob = String.Empty
        CSV = String.Empty

        If DDLJob.Value = "All" Then
            Call BindAkses()

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT JobNo FROM Job"
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

    End Sub

    Private Sub Grid_DataBinding(sender As Object, e As System.EventArgs) Handles Grid.DataBinding
        Dim PrdAwal As Date, PrdAkhir As Date
        Dim Persentase1 As Decimal = 0, Netto1 As Decimal = 0, Persentase2 As Decimal = 0, Netto2 As Decimal = 0
        Dim Bruto As Decimal = 0, Counter As Integer = 0
        Dim TmpDt As New DataTable() 'Untuk tampung detail

        Call BindCSV()

        TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("JobNo", GetType(String)), _
                                                 New DataColumn("JobNm", GetType(String)), _
                                                 New DataColumn("Jenis", GetType(String)), _
                                                 New DataColumn("Bulan", GetType(Integer)), _
                                                 New DataColumn("Persentase1", GetType(Decimal)), _
                                                 New DataColumn("Netto1", GetType(Decimal)), _
                                                 New DataColumn("Persentase2", GetType(Decimal)), _
                                                 New DataColumn("Netto2", GetType(Decimal))})

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo, JobNm, Bruto, Netto FROM Job WHERE JobNo IN (" & CSV & ") ORDER BY JobNo"
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    Bruto = RsFind("Netto")
                    'Bruto = RsFind("Bruto") / 1.1
                    'If RsFind("KSO") = "1" And RsFind("TipeManajerial") = "JO Partial" Then
                    '    Bruto = RsFind("Netto") * (If(RsFind("PersenKSO") = 0, 1, RsFind("PersenKSO") / 100))
                    'Else
                    '    Bruto = RsFind("Netto")
                    'End If

                    Using CmdFind1 As New Data.SqlClient.SqlCommand
                        With CmdFind1
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT Persentase, Bruto, TglRencana FROM RencanaTermin WHERE JobNo=@P1 AND " & _
                                           "TglRencana>=@P2 AND TglRencana<=@P3 AND Jenis='UM'"
                            .Parameters.AddWithValue("@P1", RsFind("JobNo"))
                            .Parameters.AddWithValue("@P2", DateSerial(TxtTahun.Value, 1, 1))
                            .Parameters.AddWithValue("@P3", DateSerial(TxtTahun.Value, 12, 31))
                        End With
                        Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                            If RsFind1.Read Then
                                Persentase1 = RsFind1(0)
                                Netto1 = RsFind1(1)
                                Counter = Month(RsFind1(2))
                            End If
                        End Using
                    End Using
                    Using CmdFind1 As New Data.SqlClient.SqlCommand
                        With CmdFind1
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT ISNULL(SUM(TerminInduk),0) FROM TerminInduk WHERE JobNo=@P1 AND " & _
                                           "TglCair>=@P2 AND TglCair<=@P3 AND Jenis='UM'"
                            .Parameters.AddWithValue("@P1", RsFind("JobNo"))
                            .Parameters.AddWithValue("@P2", DateSerial(TxtTahun.Value, 1, 1))
                            .Parameters.AddWithValue("@P3", DateSerial(TxtTahun.Value, 12, 31))
                        End With
                        Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                            If RsFind1.Read Then
                                Persentase2 = (RsFind1(0) / Bruto) * 100
                                Netto2 = RsFind1(0)
                            End If
                        End Using
                    End Using

                    TmpDt.Rows.Add(RsFind("JobNo"), RsFind("JobNm"), "Uang Muka", Counter, Persentase1, Netto1, Persentase2, Netto2)
                    Persentase1 = 0
                    Netto1 = 0
                    Persentase2 = 0
                    Netto2 = 0
                    Counter = 0

                    For Counter = 1 To 12
                        PrdAwal = DateSerial(TxtTahun.Value, Counter, 1)
                        PrdAkhir = If(Counter = 12, DateSerial(TxtTahun.Value + 1, 1, 1).AddDays(-1), _
                                      DateSerial(TxtTahun.Value, Counter + 1, 1).AddDays(-1))
                        Using CmdFind1 As New Data.SqlClient.SqlCommand
                            With CmdFind1
                                .Connection = Conn
                                .CommandType = CommandType.Text
                                .CommandText = "SELECT Persentase, Netto FROM RencanaTermin WHERE JobNo=@P1 AND " & _
                                               "TglRencana>=@P2 AND TglRencana<=@P3 AND Jenis='Termin'"
                                .Parameters.AddWithValue("@P1", RsFind("JobNo"))
                                .Parameters.AddWithValue("@P2", PrdAwal)
                                .Parameters.AddWithValue("@P3", PrdAkhir)
                            End With
                            Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                                If RsFind1.Read Then
                                    Persentase1 = RsFind1(0)
                                    Netto1 = RsFind1(1)
                                End If
                            End Using
                        End Using

                        Using CmdFind1 As New Data.SqlClient.SqlCommand
                            With CmdFind1
                                .Connection = Conn
                                .CommandType = CommandType.Text
                                .CommandText = "SELECT ISNULL(SUM(TerminInduk),0) FROM TerminInduk WHERE JobNo=@P1 AND " & _
                                               "TglCair>=@P2 AND TglCair<=@P3 AND Jenis='Termin'"
                                .Parameters.AddWithValue("@P1", RsFind("JobNo"))
                                .Parameters.AddWithValue("@P2", PrdAwal)
                                .Parameters.AddWithValue("@P3", PrdAkhir)
                            End With
                            Using RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
                                If RsFind1.Read Then
                                    Persentase2 = (RsFind1(0) / Bruto) * 100
                                    Netto2 = RsFind1(0)
                                End If
                            End Using
                        End Using

                        TmpDt.Rows.Add(RsFind("JobNo"), RsFind("JobNm"), "Termin", Counter, Persentase1, Netto1, Persentase2, Netto2)
                        Persentase1 = 0
                        Netto1 = 0
                        Persentase2 = 0
                        Netto2 = 0
                    Next
                    Counter = 0
                End While
            End Using
        End Using

        Grid.DataSource = TmpDt
    End Sub

    Private Sub BtnPreview_Click(sender As Object, e As System.EventArgs) Handles BtnPreview.Click
        Grid.DataBind()
    End Sub

    Private Sub BtnExport_Click(sender As Object, e As System.EventArgs) Handles BtnExport.Click
        Grid.DataBind()
        ASPxGridViewExporter1.WriteXlsxToResponse(New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})
    End Sub

    Private Sub Grid_SummaryDisplayText(sender As Object, e As DevExpress.Web.ASPxGridViewSummaryDisplayTextEventArgs) Handles Grid.SummaryDisplayText
        If e.IsGroupSummary = True Then
            If e.Item.FieldName = "Netto1" Then e.Text = "Total Netto Rencana: " + Format(e.Value, "N0")
            If e.Item.FieldName = "Netto2" Then e.Text = "Total Netto Realisasi: " + Format(e.Value, "N0")
        End If
    End Sub

    Protected Sub Grid_HeaderFilterFillItems(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewHeaderFilterEventArgs)
        e.Values.Insert(0, DevExpress.Web.FilterValue.CreateShowBlanksValue(e.Column, "Blanks"))
        e.Values.Insert(1, DevExpress.Web.FilterValue.CreateShowNonBlanksValue(e.Column, "Non Blanks"))
    End Sub

    Protected Sub Grid_CustomUnboundColumnData(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewColumnDataEventArgs)
        If e.Column.FieldName = "NmBulan" Then
            If e.GetListSourceFieldValue("Bulan") > 0 Then
                Dim Bulan As Integer = Convert.ToInt16(e.GetListSourceFieldValue("Bulan"))
                e.Value = CharMo(DateSerial(TxtTahun.Value, Bulan, 1))
            End If
        End If
    End Sub

End Class
