Public Class FrmKontrak
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "Job") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Call BindGrid()
    End Sub

    Private Sub BindGrid()
        Dim AksesJob As String = ""
        Dim CSV As String = ""
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

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo FROM Job"
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsFind("JobNo").ToString) > -1 Then
                        CSV = If(CSV = "", "'" + RsFind("JobNo") + "'", CSV + "," + "'" + RsFind("JobNo") + "'")
                    End If
                End While
            End Using
        End Using

        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                If TxtFind.Text = "" Then
                    .CommandText = "SELECT * FROM Job " + _
                                   "WHERE JobNo IN (" + CSV + ") ORDER BY JobNo DESC"
                Else
                    .CommandText = "SELECT * FROM Job " + _
                                   "WHERE JobNo IN (" + CSV + ") AND JobNm LIKE @P1 ORDER BY JobNo DESC"
                End If
                .Parameters.AddWithValue("@P1", "%" + TxtFind.Text + "%")
            End With

            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DsGrid As New Data.DataSet
                    DaGrid.Fill(DsGrid)
                    With GridData
                        .DataSource = DsGrid
                        .DataBind()
                    End With
                End Using
            End Using
        End Using

    End Sub

    Private Sub GridData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridData.PageIndexChanging
        GridData.PageIndex = e.NewPageIndex
        GridData.DataBind()
        Call BindGrid()
    End Sub

    Private Sub GridData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            If SelectRecord.Cells(4).Text = "Closed" Then
                Session("Kontrak") = "SEE|" & SelectRecord.Cells(0).Text
            Else
                Session("Kontrak") = "UPD|" & SelectRecord.Cells(0).Text
            End If

            Response.Redirect("FrmEntryKontrak.aspx")
            Exit Sub
        End If
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

End Class