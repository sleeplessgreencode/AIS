Public Class FrmQueryAP
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "QueryAP") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Call BindJob()
            Call BindVendor()
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindJob()
        DDLJob.Items.Clear()
        DDLJob.Items.Add("All", "All")

        Using CmdIsi As New Data.SqlClient.SqlCommand
            With CmdIsi
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo,JobNm FROM Job"
            End With
            Using RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
                While RsIsi.Read
                    DDLJob.Items.Add(RsIsi("JobNo") & " - " & RsIsi("JobNm"), RsIsi("JobNo"))
                End While
            End Using
        End Using

        DDLJob.SelectedIndex = 0
    End Sub

    Private Sub BindVendor()
        DDLVendor.Items.Clear()
        DDLVendor.Items.Add("All", "All")

        Using CmdIsi As New Data.SqlClient.SqlCommand
            With CmdIsi
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT VendorId,VendorNm FROM Vendor ORDER BY VendorNm"
            End With
            Using RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
                While RsIsi.Read
                    DDLVendor.Items.Add(RsIsi("VendorNm"), RsIsi("VendorId"))
                End While
            End Using
        End Using

        DDLVendor.SelectedIndex = 0
    End Sub

    Private Sub Grid_DataBinding(sender As Object, e As System.EventArgs) Handles Grid.DataBinding
        Dim TmpJoin As String = String.Empty

        If DDLJob.Value <> "All" Then
            TmpJoin = "WHERE JobNo='" & DDLJob.Value & "'"
        End If
        If DDLVendor.Value <> "All" Then
            TmpJoin = TmpJoin & If(TmpJoin = String.Empty, "WHERE ", " AND ")
            TmpJoin = TmpJoin & "VendorId='" & DDLVendor.Value & "'"
        End If
        If DDLStatus.Value <> "All" Then
            TmpJoin = TmpJoin & If(TmpJoin = String.Empty, "WHERE ", " AND ")
            If DDLStatus.Value = "Outstanding KO" Then
                TmpJoin = TmpJoin & "RemainingKO > 0"
            ElseIf DDLStatus.Value = "Outstanding Invoice" Then
                TmpJoin = TmpJoin & "RemainingInv > 0"
            ElseIf DDLStatus.Value = "Settle KO" Then
                TmpJoin = TmpJoin & "RemainingKO <= 0"
            ElseIf DDLStatus.Value = "Settle Invoice" Then
                TmpJoin = TmpJoin & "RemainingInv <= 0"
            End If
        End If

        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "WITH TmpQuery AS (" & _
                               "SELECT A.Jobno AS JobNo, D.JobNm, A.NoKO, A.TglKO, A.KategoriId, A.VendorId, B.VendorNm, A.SubTotal-A.DiscAmount+A.PPN AS TotalKO, " & _
                               "ISNULL((SELECT SUM(Amount) FROM BLE WHERE NoKO=A.NoKO),0) AS PaymentKO, " & _
                               "A.SubTotal-A.DiscAmount+A.PPN - ISNULL((SELECT SUM(AMOUNT) FROM BLE WHERE NoKO=A.NoKO),0) AS RemainingKO, " & _
                               "ISNULL((SELECT SUM(Total) FROM Invoice WHERE NoKO=A.NoKO),0) AS TotalInv, " & _
                               "ISNULL((SELECT SUM(PaymentAmount) FROM BLE, InvPD, PdHdr WHERE " & _
                               "(BLE.NoPD=PdHdr.NoPD OR BLE.NoPD=PdHdr.NoRef) AND PdHdr.NoKO=A.NoKO AND InvPD.NoPD=PdHdr.NoPD),0) AS PaymentInv, " & _
                               "ISNULL(ISNULL((SELECT SUM(Total) FROM Invoice WHERE NoKO=A.NoKO),0) - ISNULL((SELECT SUM(PaymentAmount) FROM BLE, InvPD, PdHdr WHERE " & _
                               "(BLE.NoPD=PdHdr.NoPD OR BLE.NoPD=PdHdr.NoRef) AND PdHdr.NoKO=A.NoKO AND InvPD.NoPD=PdHdr.NoPD),0),0) AS RemainingInv " & _
                               "FROM KoHdr A " & _
                               "LEFT JOIN Vendor B ON B.VendorId=A.VendorId " & _
                               "LEFT JOIN Job D  ON D.JobNo=A.JobNo " & _
                               "WHERE A.ApprovedBy IS NOT NULL AND A.CanceledBy IS NULL) " & _
                               "SELECT * FROM TmpQuery " & TmpJoin & " ORDER BY NoKO"
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

    Private Sub BtnPreview_Click(sender As Object, e As System.EventArgs) Handles BtnPreview.Click
        Grid.DataBind
    End Sub

    Private Sub BtnExport_Click(sender As Object, e As System.EventArgs) Handles BtnExport.Click
        Grid.DataBind()
        ASPxGridViewExporter1.WriteXlsxToResponse(New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})
    End Sub

    Private Sub Grid_SummaryDisplayText(sender As Object, e As DevExpress.Web.ASPxGridViewSummaryDisplayTextEventArgs) Handles Grid.SummaryDisplayText
        If e.IsGroupSummary = True Then
            If e.Item.FieldName = "RemainingKO" Then e.Text = "Sisa Hutang KO: " + Format(e.Value, "N0")
            If e.Item.FieldName = "RemainingInv" Then e.Text = "Sisa Hutang Invoice: " + Format(e.Value, "N0")
        End If
    End Sub

    Protected Sub InvoiceGrid_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("NoKO") = (TryCast(sender, DevExpress.Web.ASPxGridView)).GetMasterRowKeyValue().ToString.Split("|")(0)

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("ConnStr").ConnectionString
        Using con As New Data.SqlClient.SqlConnection(strConnString)
            Using CmdGrid As New Data.SqlClient.SqlCommand
                With CmdGrid
                    .Connection = con
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT *, " & _
                                   "ISNULL((SELECT SUM(PaymentAmount) FROM BLE, InvPD, PdHdr WHERE " & _
                                   "(BLE.NoPD=PdHdr.NoPD OR BLE.NoPD=PdHdr.NoRef) AND PdHdr.NoKO=Invoice.NoKO AND InvPD.NoPD=PdHdr.NoPD AND InvPD.InvNo=Invoice.InvNo),0) AS PaymentAmount " & _
                                   "FROM Invoice WHERE NoKO=@P1"
                    .Parameters.AddWithValue("@P1", Session("NoKO"))
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
        Session("NoKO") = (TryCast(sender, DevExpress.Web.ASPxGridView)).GetMasterRowKeyValue().ToString.Split("|")(0)
        Session("InvNo") = (TryCast(sender, DevExpress.Web.ASPxGridView)).GetMasterRowKeyValue().ToString.Split("|")(1)

        Dim strConnString As String = ConfigurationManager.ConnectionStrings("ConnStr").ConnectionString
        Using con As New Data.SqlClient.SqlConnection(strConnString)
            Using CmdGrid As New Data.SqlClient.SqlCommand
                With CmdGrid
                    .Connection = con
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT A.InvNo, A.NoPD, " & _
                                   "ISNULL((SELECT SUM(PaymentAmount) FROM BLE, InvPD, PdHdr WHERE " & _
                                   "(BLE.NoPD=PdHdr.NoPD OR BLE.NoPD=PdHdr.NoRef) AND PdHdr.NoKO=InvPD.NoKO AND InvPD.NoPD=PdHdr.NoPD AND " & _
                                   "InvPD.NoKO=A.NoKO AND InvPD.InvNo=A.InvNo AND InvPD.NoPD=A.NoPD),0) AS BLEAmount " & _
                                   "FROM InvPD A WHERE NoKO=@P1 AND InvNo=@P2"
                    .Parameters.AddWithValue("@P1", Session("NoKO"))
                    .Parameters.AddWithValue("@P2", Session("InvNo"))
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

End Class
