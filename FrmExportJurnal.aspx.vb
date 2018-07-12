Public Class FrmExportJurnal
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "JurnalExport") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            TxtTgl2.Date = Today
            TxtTgl1.Date = Today.AddDays(-60)
            Call BindJob()

            If Session("Job") <> String.Empty Then
                DDLJob.Value = Session("Job").ToString.Split("|")(0)
                Call BindSite()
                DDLSite.Value = Session("Job").ToString.Split("|")(1)
                Session.Remove("Job")
            End If

            Call BindGrid()

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

    Private Sub BindSite()
        DDLSite.Items.Clear()
        DDLSite.Items.Add(String.Empty, String.Empty)

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM GlReff WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                DDLSite.Items.Add(RsFind("Site"), RsFind("Site"))
            End While
        End Using

        DDLSite.SelectedIndex = 0

    End Sub

    Private Sub BindGrid()
        Grid.DataBind()
    End Sub

    Private Sub TxtTgl1_ValueChanged(sender As Object, e As System.EventArgs) Handles TxtTgl1.ValueChanged
        Call BindGrid()
    End Sub

    Private Sub TxtTgl2_ValueChanged(sender As Object, e As System.EventArgs) Handles TxtTgl2.ValueChanged
        Call BindGrid()
    End Sub

    Private Sub DDLJob_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindSite()
        Call BindGrid()
    End Sub

    Private Sub DDLSite_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLSite.SelectedIndexChanged
        Call BindGrid()
    End Sub

    Protected Sub Grid_CustomUnboundColumnData(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewColumnDataEventArgs)
        If e.Column.FieldName = "Deviasi" Then
            Dim DebetBalance As Decimal = CDec(e.GetListSourceFieldValue("DebetBalance"))
            Dim KreditBalance As Decimal = CDec(e.GetListSourceFieldValue("KreditBalance"))
            e.Value = (DebetBalance - KreditBalance)
        End If
    End Sub

    Private Sub Grid_DataBinding(sender As Object, e As System.EventArgs) Handles Grid.DataBinding
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, B.Nama, C.AccName FROM JurnalEntry A " + _
                               "LEFT JOIN Identitas B ON A.JobNo=B.JobNo AND A.Identitas=B.Identitas " + _
                               "LEFT JOIN COA C ON A.AccNo=C.AccNo " + _
                               "WHERE A.JobNo=@P1 AND A.Site=@P2 AND A.TglJurnal>=@P3 AND A.TglJurnal<=@P4 ORDER BY Bulan DESC, Tahun DESC, NoJurnal DESC, PC DESC"
                .Parameters.AddWithValue("@P1", If(DDLJob.Text = String.Empty, String.Empty, DDLJob.Value))
                .Parameters.AddWithValue("@P2", If(DDLSite.Text = String.Empty, String.Empty, DDLSite.Value))
                .Parameters.AddWithValue("@P3", TxtTgl1.Date)
                .Parameters.AddWithValue("@P4", TxtTgl2.Date)
            End With
            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DsGrid As New Data.DataSet
                    DaGrid.Fill(DsGrid)
                    With Grid
                        .DataSource = DsGrid
                    End With
                End Using
            End Using
        End Using
    End Sub

    Private Sub BtnExport_Click(sender As Object, e As System.EventArgs) Handles BtnExport.Click
        Grid.DataBind()
        ASPxGridViewExporter1.WriteXlsxToResponse(New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})
    End Sub

End Class