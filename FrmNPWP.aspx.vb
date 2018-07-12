Public Class FrmNPWP
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "NPWP") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Call BindGrid()
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindGrid()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo,JobNm,NPWPName,NPWPAddress,NPWPCompany FROM Job WHERE StatusJob <> 'Proposal'"
            End With
            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DsGrid As New Data.DataSet
                    DaGrid.Fill(DsGrid)
                    With GridView
                        .DataSource = DsGrid
                        .DataBind()
                    End With
                End Using
            End Using
        End Using
    End Sub

    Private Sub GridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)

            Action.Text = "UPD"
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT JobNo,JobNm,NPWPName,NPWPAddress,NPWPCompany FROM Job WHERE JobNo=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(0).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        TxtJobNo.Text = RsFind("JobNo")
                        TxtJobNm.Text = RsFind("JobNm")
                        TxtNPWPName.Text = RsFind("NPWPName").ToString
                        TxtNPWP.Text = RsFind("NPWPCompany").ToString
                        TxtAlamat.Text = RsFind("NPWPAddress").ToString

                    End If
                End Using
            End Using

            Session("NPWP") = SelectRecord.Cells(0).Text

            ModalEntry.ShowOnPageLoad = True
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As System.EventArgs) Handles BtnSave.Click
        If TxtAlamat.Text = String.Empty Then
            msgBox1.alert("Alamat belum diisi.")
            TxtAlamat.Focus()
            Exit Sub
        End If

        Using CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE Job SET NPWPName=@P1,NPWPCompany=@P2,NPWPAddress=@P3,UserEntry=@P4,TimeEntry=@P5 WHERE JobNo=@P6"
                .Parameters.AddWithValue("@P1", TxtNPWPName.Text)
                .Parameters.AddWithValue("@P2", TxtNPWP.Text)
                .Parameters.AddWithValue("@P3", TxtAlamat.Text)
                .Parameters.AddWithValue("@P4", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P5", Now)
                .Parameters.AddWithValue("@P6", Session("NPWP"))
                .ExecuteNonQuery()
            End With
        End Using

        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As System.EventArgs) Handles BtnCancel.Click
        Session.Remove("NPWP")
        Call BindGrid()
        ModalEntry.ShowOnPageLoad = False
    End Sub

End Class