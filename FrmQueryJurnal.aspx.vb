Public Class FrmQueryJurnal
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "QueryJurnal") = False Then
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
                .CommandText = "SELECT A.*, B.Nama, C.AccName, D.JobNm FROM JurnalEntry A " & _
                               "LEFT JOIN Identitas B ON A.JobNo=B.JobNo AND A.Identitas=B.Identitas " & _
                               "LEFT JOIN COA C ON A.AccNo=C.AccNo " & _
                               "LEFT JOIN Job D ON A.JobNo=D.JobNo ORDER BY A.NoJurnal, A.PC DESC"
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

    Protected Sub grid_HeaderFilterFillItems(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewHeaderFilterEventArgs)
        e.Values.Insert(0, DevExpress.Web.FilterValue.CreateShowBlanksValue(e.Column, "Blanks"))
        e.Values.Insert(1, DevExpress.Web.FilterValue.CreateShowNonBlanksValue(e.Column, "Non Blanks"))
    End Sub

    Private Sub Grid_SummaryDisplayText(sender As Object, e As DevExpress.Web.ASPxGridViewSummaryDisplayTextEventArgs) Handles Grid.SummaryDisplayText
        If e.IsGroupSummary = True Then
            If e.Item.FieldName = "Debet" Then e.Text = "Total Debet: " + Format(e.Value, "N0")
            If e.Item.FieldName = "Kredit" Then e.Text = "Total Kredit: " + Format(e.Value, "N0")
            If e.Item.FieldName = "DebetBalance" Then e.Text = "Total Debet Balance: " + Format(e.Value, "N0")
            If e.Item.FieldName = "KreditBalance" Then e.Text = "Total Kredit Balance: " + Format(e.Value, "N0")
        End If
    End Sub

End Class