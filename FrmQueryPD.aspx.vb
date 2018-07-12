Public Class FrmQueryPD
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "QueryPD") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Call BindJob()
            Call BindAlokasi()
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

    Private Sub BindAlokasi()
        Dim AksesAlokasi As String = ""
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT AksesAlokasi FROM Login WHERE UserID=@P1"
                .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                AksesAlokasi = RsFind("AksesAlokasi")
            End If
        End Using

        DDLAlokasi.Items.Clear()
        DDLAlokasi.Items.Add(String.Empty, String.Empty)
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT Alokasi,Keterangan FROM Alokasi"
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                If Array.IndexOf(AksesAlokasi.Split(","), RsFind("Alokasi")) >= 0 Then
                    DDLAlokasi.Items.Add(RsFind("Alokasi") & " - " & RsFind("Keterangan"), RsFind("Alokasi"))
                End If
            End While
        End Using

        DDLAlokasi.SelectedIndex = "0"
    End Sub

    Private Sub Grid_DataBinding(sender As Object, e As System.EventArgs) Handles Grid.DataBinding
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, B.SubTotal, B.DiscAmount, B.PPN, " & _
                               "(SELECT TOP 1 RekId FROM BLE WHERE NoPD=A.NoPD OR NoPD=A.NoRef) AS 'RekId', " & _
                               "(SELECT TOP 1 TglBayar FROM BLE WHERE NoPD=A.NoPD OR NoPD=A.NoRef) AS 'TglBayar', " & _
                               "(SELECT TOP 1 JenisTrf FROM BLE WHERE NoPD=A.NoPD OR NoPD=A.NoRef) AS 'JenisTrf', " & _
                               "(SELECT ISNULL(SUM(Amount),0) FROM BLE WHERE NoPD=A.NoPD OR NoPD=A.NoRef) AS 'BLEAmount' " & _
                               "FROM PdHdr A LEFT JOIN KoHdr B ON A.NoKO=B.NoKO WHERE A.JobNo=@P1 AND A.Alokasi=@P2 ORDER BY A.NoPD DESC"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
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

    Protected Sub Grid_CustomUnboundColumnData(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewColumnDataEventArgs)
        If e.Column.FieldName = "TotalKO" Then
            If e.GetListSourceFieldValue("NoKO") <> String.Empty And Mid(e.GetListSourceFieldValue("NoKO"), 1, 2) = "KO" Then
                Dim SubTotal As Decimal = Convert.ToDecimal(e.GetListSourceFieldValue("SubTotal"))
                Dim DiscAmount As Decimal = Convert.ToDecimal(e.GetListSourceFieldValue("DiscAmount"))
                Dim PPN As Decimal = Convert.ToDecimal(e.GetListSourceFieldValue("PPN"))
                e.Value = (SubTotal - DiscAmount + PPN)
            End If
        End If
    End Sub

    Private Sub BtnPreview_Click(sender As Object, e As System.EventArgs) Handles BtnPreview.Click
        Grid.DataBind()
    End Sub

    Private Sub BtnExport_Click(sender As Object, e As System.EventArgs) Handles BtnExport.Click
        Grid.DataBind()
        ASPxGridViewExporter1.WriteXlsxToResponse(New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})
    End Sub

    Protected Sub detailGrid_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("NoPD") = (TryCast(sender, DevExpress.Web.ASPxGridView)).GetMasterRowKeyValue()

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("ConnStr").ConnectionString
        Using con As New Data.SqlClient.SqlConnection(strConnString)
            Using CmdGrid As New Data.SqlClient.SqlCommand
                With CmdGrid
                    .Connection = con
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM PdDtl WHERE NoPD=@P1"
                    .Parameters.AddWithValue("@P1", Session("NoPD"))
                End With
                Using DaGrid As New Data.SqlClient.SqlDataAdapter
                    DaGrid.SelectCommand = CmdGrid
                    Using DsGrid As New Data.DataSet
                        DaGrid.Fill(DsGrid)
                        Dim detailGrid As DevExpress.Web.ASPxGridView = TryCast(sender, DevExpress.Web.ASPxGridView)
                        With detailGrid
                            .DataSource = DsGrid
                        End With
                    End Using
                End Using
            End Using
        End Using

    End Sub

    Protected Sub detailGrid_CustomUnboundColumnData(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewColumnDataEventArgs)
        If e.Column.FieldName = "SubTotalPD" Then
            Dim Price As Decimal = Convert.ToDecimal(e.GetListSourceFieldValue("HrgSatuan"))
            Dim Qty As Decimal = Convert.ToDecimal(e.GetListSourceFieldValue("Vol"))
            e.Value = (Price * Qty)
        End If
        If e.Column.FieldName = "SubTotalPJ" Then
            Dim Price As Decimal = Convert.ToDecimal(e.GetListSourceFieldValue("PjHrgSatuan"))
            Dim Qty As Decimal = Convert.ToDecimal(e.GetListSourceFieldValue("PjVol"))
            e.Value = (Price * Qty)
        End If
    End Sub

    Protected Sub InvoiceGrid_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("NoPD") = (TryCast(sender, DevExpress.Web.ASPxGridView)).GetMasterRowKeyValue()

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("ConnStr").ConnectionString
        Using con As New Data.SqlClient.SqlConnection(strConnString)
            Using CmdGrid As New Data.SqlClient.SqlCommand
                With CmdGrid
                    .Connection = con
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM InvPD A LEFT JOIN Invoice B ON A.NoKO=B.NoKO AND A.InvNo=B.InvNo WHERE A.NoPD=@P1"
                    .Parameters.AddWithValue("@P1", Session("NoPD"))
                End With
                Using DaGrid As New Data.SqlClient.SqlDataAdapter
                    DaGrid.SelectCommand = CmdGrid
                    Using DsGrid As New Data.DataSet
                        DaGrid.Fill(DsGrid)
                        Dim InvoiceGrid As DevExpress.Web.ASPxGridView = TryCast(sender, DevExpress.Web.ASPxGridView)
                        With InvoiceGrid
                            .DataSource = DsGrid
                        End With
                    End Using
                End Using
            End Using
        End Using

    End Sub

    Protected Sub BLEGrid_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("NoPD") = (TryCast(sender, DevExpress.Web.ASPxGridView)).GetMasterRowKeyValue()

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("ConnStr").ConnectionString
        Using con As New Data.SqlClient.SqlConnection(strConnString)
            Using CmdGrid As New Data.SqlClient.SqlCommand
                With CmdGrid
                    .Connection = con
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT BLE.* FROM PdHdr, BLE WHERE (PdHdr.NoPD=BLE.NoPD OR PdHdr.NoRef=BLE.NoPD) AND PdHdr.NoPD=@P1"
                    .Parameters.AddWithValue("@P1", Session("NoPD"))
                End With
                Using DaGrid As New Data.SqlClient.SqlDataAdapter
                    DaGrid.SelectCommand = CmdGrid
                    Using DsGrid As New Data.DataSet
                        DaGrid.Fill(DsGrid)
                        Dim BLEGrid As DevExpress.Web.ASPxGridView = TryCast(sender, DevExpress.Web.ASPxGridView)
                        With BLEGrid
                            .DataSource = DsGrid
                        End With
                    End Using
                End Using
            End Using
        End Using

    End Sub

    Protected Sub grid_HeaderFilterFillItems(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewHeaderFilterEventArgs)
        e.Values.Insert(0, DevExpress.Web.FilterValue.CreateShowBlanksValue(e.Column, "Blanks"))
        e.Values.Insert(1, DevExpress.Web.FilterValue.CreateShowNonBlanksValue(e.Column, "Non Blanks"))
    End Sub

End Class