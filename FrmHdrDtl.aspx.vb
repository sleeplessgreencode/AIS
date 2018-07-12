Public Class FrmHdrDtl
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

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub Grid_DataBinding(sender As Object, e As System.EventArgs) Handles Grid.DataBinding
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM KoHdr, Job, Vendor WHERE KoHdr.JobNo=Job.JobNo AND Vendor.VendorId=KoHdr.VendorId AND ApprovedBy IS NOT NULL AND CanceledBy IS NULL"
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
        Grid.DataBind()
    End Sub

    'Private Sub Grid_FocusedRowChanged(sender As Object, e As System.EventArgs) Handles Grid.FocusedRowChanged
    '    Session("NoKO") = Grid.GetRowValues(Grid.FocusedRowIndex, "NoKO")
    '    If Session("NoKO") = String.Empty Then Exit Sub
    '    InvoiceGrid.DataBind()
    'End Sub

    'Protected Sub InvoiceGrid_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs)
    '    Dim Key As String = Grid.GetRowValues(Grid.FocusedRowIndex, "NoKO")

    '    If String.IsNullOrEmpty(Key) = False Then
    '        Session("NoKO") = Key
    '        InvoiceGrid.DataBind()
    '    End If

    'End Sub

    Private Sub InvoiceGrid_CustomCallback1(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles InvoiceGrid.CustomCallback
        Dim Key As String = Grid.GetRowValues(Grid.FocusedRowIndex, "NoKO")

        If String.IsNullOrEmpty(Key) = False Then
            Session("NoKO") = Key
            InvoiceGrid.DataBind()
        End If
    End Sub

    Private Sub InvoiceGrid_DataBinding(sender As Object, e As System.EventArgs) Handles InvoiceGrid.DataBinding
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
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
    End Sub

End Class