Public Class FrmItemPrice
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User"), "ItemPrice") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Grid.DataBind()
        End If

    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub Grid_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.DataBinding
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.Uraian, A.Uom, A.HrgSatuan, B.NoKO, B.TglKO, C.JobNm, D.VendorNm FROM KoDtl A " & _
                               "LEFT JOIN KoHdr B ON A.NoKO=B.NoKO " & _
                               "LEFT JOIN Job C ON B.JobNo=C.JobNo " & _
                               "LEFT JOIN Vendor D ON B.VendorId=D.VendorId " & _
                               "WHERE B.KategoriId='PO'"
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

End Class