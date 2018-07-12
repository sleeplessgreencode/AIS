Public Class FrmCancelPO
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "CancelPO") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Call BindJob()
        End If

        If Session("Job") <> "" Then
            DDLJob.Value = Session("Job")
            Session.Remove("Job")
        End If

        Call BindGrid()

    End Sub

    Private Sub BindGrid()
        Dim CmdGrid As New Data.SqlClient.SqlCommand
        With CmdGrid
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT A.*, B.VendorNm, " & _
                           "(SELECT ISNULL(SUM(Amount),0) FROM BLE WHERE NoKO=A.NoKO) AS PaymentAmount " & _
                           "FROM KoHdr A JOIN Vendor B ON A.VendorId=B.VendorId WHERE JobNo=@P1 " & _
                           "AND NoKO LIKE @P2 ORDER BY NoKO DESC"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", "%" + TxtFind.Text + "%")
            .Parameters.AddWithValue("@P3", "PO")
        End With

        Dim DaGrid As New Data.SqlClient.SqlDataAdapter
        DaGrid.SelectCommand = CmdGrid
        Dim DtGrid As New Data.DataTable
        DaGrid.Fill(DtGrid)
        GridData.DataSource = DtGrid
        'Call GetTotal()
        GridData.DataBind()

        DaGrid.Dispose()
        DtGrid.Dispose()
        CmdGrid.Dispose()

    End Sub

    Private Sub GridData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridData.PageIndexChanging
        GridData.PageIndex = e.NewPageIndex
        GridData.DataBind()
    End Sub

    Private Sub GridData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            Session("KO") = "SEE_CANCELKO|" & DDLJob.Text & "|" & SelectRecord.Cells(1).Text & "|"

            If SelectRecord.Cells(4).Text = "KONTRAK" Then
                Response.Redirect("FrmEntryKO.aspx")
            Else
                Session("KO") = Session("KO") & "|"
                Response.Redirect("FrmEntryPO.aspx")
            End If

        ElseIf e.CommandName = "BtnCancel" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM KoHdr WHERE NoKO=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        If String.IsNullOrEmpty(RsFind("CanceledBy").ToString) = False Then
                            LblErr.Text = SelectRecord.Cells(1).Text & " sudah di-batalkan."
                            ErrMsg.ShowOnPageLoad = True
                            Exit Sub
                        End If
                        'If String.IsNullOrEmpty(RsFind("ApprovedBy").ToString) = False Then
                        '    LblErr.Text = SelectRecord.Cells(1).Text & " sudah di-approved."
                        '    ErrMsg.ShowOnPageLoad = True
                        '    Exit Sub
                        'End If
                        If String.IsNullOrEmpty(RsFind("ClosedBy").ToString) = False Then
                            LblErr.Text = SelectRecord.Cells(1).Text & " sudah di-tutup."
                            ErrMsg.ShowOnPageLoad = True
                            Exit Sub
                        End If
                    End If
                End Using
            End Using

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT TOP 1 NoKO FROM BLE WHERE NoKO=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        LblErr.Text = "Sudah ada pembayaran untuk " & SelectRecord.Cells(1).Text & "."
                        ErrMsg.ShowOnPageLoad = True
                        Exit Sub
                    End If
                End Using
            End Using

            Session("KO") = SelectRecord.Cells(1).Text

            LblDel.Text = "Anda yakin ingin batalkan PO berikut?" & "<br />" & _
                           SelectRecord.Cells(1).Text & " - " & SelectRecord.Cells(3).Text
            DelMsg.ShowOnPageLoad = True
        End If
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindJob()
        Dim AksesJob As String = ""
        Dim CmdFind As New Data.SqlClient.SqlCommand
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
        RsFind.Close()
        CmdFind.Dispose()

        DDLJob.Items.Clear()
        DDLJob.Items.Add("Pilih salah satu", "0")
        Dim CmdIsi As New Data.SqlClient.SqlCommand
        With CmdIsi
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT JobNo,JobNm FROM Job"
        End With
        Dim RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
        While RsIsi.Read
            If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsIsi("JobNo")) >= 0 Then
                DDLJob.Items.Add(RsIsi("JobNo") & " - " & RsIsi("JobNm"), RsIsi("JobNo"))
            End If
        End While
        RsIsi.Close()
        CmdIsi.Dispose()

        DDLJob.Value = "0"
    End Sub

    Protected Sub DDLJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindGrid()
    End Sub

    Protected Sub BtnDel_Click(sender As Object, e As EventArgs) Handles BtnDel.Click
        Dim CmdEdit As New Data.SqlClient.SqlCommand
        With CmdEdit
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "UPDATE KoHdr SET CanceledBy=@P1,TimeCancel=@P2,CancelReason=@P3 WHERE NoKO=@P4"
            .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P2", Now)
            .Parameters.AddWithValue("@P3", CancelMemo.Text)
            .Parameters.AddWithValue("@P4", Session("KO"))
            .ExecuteNonQuery()
            .Dispose()
        End With
        Session.Remove("KO")

        Call BindGrid()
    End Sub

End Class