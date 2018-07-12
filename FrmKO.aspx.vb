Public Class FrmKO
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "KO") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Call BindJob()
            Call BindGrid()
        End If

        If Session("Job") <> "" Then
            DDLJob.Value = Session("Job")
            Session.Remove("Job")
            Call BindGrid()
        End If

    End Sub

    Private Sub BindGrid()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, B.VendorNm FROM KoHdr A JOIN Vendor B ON A.VendorId=B.VendorId WHERE " & _
                               "JobNo=@P1 " + GetFilter1() + GetFilter2() + " ORDER BY NoKO DESC"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With

            Dim Ttl As Decimal = 0
            Using RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                While RsGrid.Read
                    Ttl += (RsGrid("SubTotal") - RsGrid("DiscAmount")) + RsGrid("PPN")
                End While
                GridData.Columns(6).FooterText = Format(Ttl, "N0")
            End Using

            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    GridData.DataSource = DtGrid
                    GridData.DataBind()
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
        Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)
        Dim Approved As Boolean = False, Canceled As Boolean = False

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT ApprovedBy, CanceledBy FROM KoHdr WHERE NoKO=@P1"
                .Parameters.AddWithValue("@P1", SelectRecord.Cells(1).Text)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    If String.IsNullOrEmpty(RsFind("ApprovedBy").ToString) = False Then Approved = True
                    If String.IsNullOrEmpty(RsFind("CanceledBy").ToString) = False Then Canceled = True
                End If
            End Using
        End Using

        If Canceled = True Then
            LblErr.Text = SelectRecord.Cells(1).Text & " sudah dibatalkan."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        If e.CommandName = "BtnUpdate" Then
            If Approved = False Then
                Session("KO") = "UPD|" & DDLJob.Text & "|" & SelectRecord.Cells(1).Text & "|"
            Else
                Session("KO") = "SEE_KO|" & DDLJob.Text & "|" & SelectRecord.Cells(1).Text & "|"
            End If

            If SelectRecord.Cells(4).Text = "KONTRAK" Then
                Response.Redirect("FrmEntryKO.aspx")
            Else
                Session("KO") = Session("KO") & "|MIX"
                Response.Redirect("FrmEntryPO.aspx")
            End If

        ElseIf e.CommandName = "BtnPrint" Then
            If Approved = False Then
                LblErr.Text = SelectRecord.Cells(1).Text & " belum di-Approved."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If

            Session("Print") = SelectRecord.Cells(1).Text & "|" & DDLJob.Text
            Dim Url As String = If(SelectRecord.Cells(4).Text = "KONTRAK", "FrmRptSummaryKontrak.aspx", "FrmRptPO.aspx")

            With DialogWindow1
                .TargetUrl = Url
                .Open()
            End With

        ElseIf e.CommandName = "BtnPDF" Then
            LblPath.Text = ""
            LblKO.Text = ""
            LnkView.Visible = False

            If Approved = False Then
                LblErr.Text = SelectRecord.Cells(1).Text & " belum di-Approved."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM KoHdr WHERE NoKO=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        If String.IsNullOrEmpty(RsFind("PDFFile").ToString) = False Then
                            LblPath.Text = RsFind("PDFFile").ToString
                            LnkView.Visible = True
                        End If

                        LblKO.Text = SelectRecord.Cells(1).Text
                        ModalPDF.ShowOnPageLoad = True

                    End If
                End Using
            End Using

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

    Private Sub GridData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridData.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).Text = "Total : "
            e.Row.Cells(5).Font.Bold = True
        End If

    End Sub

    Protected Sub BtnPo_Click(sender As Object, e As EventArgs) Handles BtnPo.Click
        If DDLJob.Value = "0" Then
            LblErr.Text = "Belum pilih Job."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        Session("KO") = "NEW|" & DDLJob.Text & "|||"

        Response.Redirect("FrmEntryPO.aspx")
        Exit Sub
    End Sub

    Protected Sub BtnKontrak_Click(sender As Object, e As EventArgs) Handles BtnKontrak.Click
        If DDLJob.Value = "0" Then
            LblErr.Text = "Belum pilih Job."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        Session("KO") = "NEW|" & DDLJob.Text & "||"

        Response.Redirect("FrmEntryKO.aspx")
        Exit Sub
    End Sub

    Private Function GetFilter1() As String
        If DDLField1.Value <> "0" Then
            Return "AND " + DDLField1.Value + GetFilterFunction1()
        End If

        Return ""
    End Function

    Private Function GetFilterFunction1() As String
        If DDLFilterBy1.Value = "0" Then
            If DDLField1.Value <> "TglKO" Then
                Return "='" + TxtFind1.Text + "'"
            Else
                Return "='" + TglFilter1.Date + "'"
            End If
        ElseIf DDLFilterBy1.Value = "1" Then
            If DDLField1.Value <> "TglKO" Then
                Return " LIKE '%" + TxtFind1.Text + "%'"
            Else
                Return ""
            End If
        ElseIf DDLFilterBy1.Value = "2" Then
            If DDLField1.Value <> "TglKO" Then
                Return ">=" + TxtFind1.Text
            Else
                Return ">='" + TglFilter1.Date + "'"
            End If
        ElseIf DDLFilterBy1.Value = "3" Then
            If DDLField1.Value <> "TglKO" Then
                Return "<=" + TxtFind1.Text
            Else
                Return "<='" + TglFilter1.Date + "'"
            End If
        End If

        Return ""
    End Function

    Protected Sub DDLField1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLField1.SelectedIndexChanged
        TxtFind1.Text = ""
        TglFilter1.Text = ""
        DDLFilterBy1.Items.Clear()
        TxtFind1.Visible = True
        TglFilter1.Visible = False
        If DDLField1.Value = "TglKO" Then
            TxtFind1.Visible = False
            TglFilter1.Visible = True
            DDLFilterBy1.Items.Add("Equals", "0")
            DDLFilterBy1.Items.Add("Is Greather Than Or Equal To", "2")
            DDLFilterBy1.Items.Add("Is Less Than Or Equal To", "3")
        Else
            DDLFilterBy1.Items.Add("Equals", "0")
            DDLFilterBy1.Items.Add("Contains", "1")
        End If
        DDLFilterBy1.Value = "0"
    End Sub

    Protected Sub DDLField2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLField2.SelectedIndexChanged
        TxtFind2.Text = ""
        TglFilter2.Text = ""
        DDLFilterBy2.Items.Clear()
        TxtFind2.Visible = True
        TglFilter2.Visible = False
        If DDLField2.Value = "TglKO" Then
            TxtFind2.Visible = False
            TglFilter2.Visible = True
            DDLFilterBy2.Items.Add("Equals", "0")
            DDLFilterBy2.Items.Add("Is Greather Than Or Equal To", "2")
            DDLFilterBy2.Items.Add("Is Less Than Or Equal To", "3")
        Else
            DDLFilterBy2.Items.Add("Equals", "0")
            DDLFilterBy2.Items.Add("Contains", "1")
        End If
        DDLFilterBy2.Value = "0"
    End Sub

    Private Function GetFilter2() As String
        If DDLField2.Value <> "0" Then
            Return " AND " + DDLField2.Value + " " + GetFilterFunction2()
        End If

        Return ""
    End Function

    Private Function GetFilterFunction2() As String
        If DDLFilterBy2.Value = "0" Then
            If DDLField2.Value <> "TglKO" Then
                Return "='" + TxtFind2.Text + "'"
            Else
                Return "='" + TglFilter2.Date + "'"
            End If
        ElseIf DDLFilterBy2.Value = "1" Then
            If DDLField2.Value <> "TglKO" Then
                Return "LIKE '%" + TxtFind2.Text + "%'"
            Else
                Return ""
            End If
        ElseIf DDLFilterBy2.Value = "2" Then
            If DDLField2.Value <> "TglKO" Then
                Return ">=" + TxtFind2.Text
            Else
                Return ">='" + TglFilter2.Date + "'"
            End If
        ElseIf DDLFilterBy2.Value = "3" Then
            If DDLField2.Value <> "TglKO" Then
                Return "<=" + TxtFind2.Text
            Else
                Return "<='" + TglFilter2.Date + "'"
            End If
        End If

        Return ""
    End Function

    Private Sub BtnFind_Click(sender As Object, e As System.EventArgs) Handles BtnFind.Click
        Call BindGrid()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As System.EventArgs) Handles BtnSave.Click
        If Not PDFUpload.HasFile = True Then
            ModalPDF.ShowOnPageLoad = False
            Exit Sub
        End If

        If PDFUpload.HasFile And PDFUpload.PostedFile.ContentType.ToLower <> "application/pdf" Then
            MsgBox1.alert("File yang di-upload bukan PDF.")
            PDFUpload.Focus()
            Exit Sub
        End If

        Using CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE KoHdr SET " & _
                                "PDFFile=@P1,UserEntry=@P2,TimeEntry=@P3 " & _
                                "WHERE NoKO=@P4"
                If System.IO.File.Exists(Server.MapPath("~/PDF/KO/") + System.IO.Path.GetFileName(LblPath.Text)) Then
                    System.IO.File.Delete(Server.MapPath("~/PDF/KO/") + System.IO.Path.GetFileName(LblPath.Text))
                End If
                Dim fileName As String = LblKO.Text & "_" & Format(Now, "yyyy-MM-dd") & "_" & Format(Now, "hhmmss") & "_" & _
                    System.IO.Path.GetFileName(PDFUpload.PostedFile.FileName)
                PDFUpload.PostedFile.SaveAs(Server.MapPath("~/PDF/KO/") + fileName)
                .Parameters.AddWithValue("@P1", "~/PDF/KO/" + fileName)
                .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P3", Now)
                .Parameters.AddWithValue("@P4", LblKO.Text)
                .ExecuteNonQuery()
            End With
        End Using

        ModalPDF.ShowOnPageLoad = False

    End Sub

    Protected Sub View(sender As Object, e As EventArgs)
        'Response.ContentType = ContentType
        'Response.AppendHeader("Content-Disposition", ("attachment; filename=" & System.IO.Path.GetFileName(LblPath.Text)))
        'Response.WriteFile(LblPath.Text)
        'Response.End()

        Session("ViewPDF") = Mid(LblPath.Text, 3, Len(LblPath.Text) - 2)

        With DialogWindow1
            .TargetUrl = "FrmViewPDF.aspx"
            .Open()
        End With
    End Sub

    Private Sub BtnClearPDF_Click(sender As Object, e As System.EventArgs) Handles BtnClearPDF.Click
        If System.IO.File.Exists(Server.MapPath("~/PDF/KO/") & System.IO.Path.GetFileName(LblPath.Text)) Then
            System.IO.File.Delete(Server.MapPath("~/PDF/KO/") & System.IO.Path.GetFileName(LblPath.Text))
        End If

        Using CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE KoHdr SET " & _
                                "PDFFile=@P1,UserEntry=@P2,TimeEntry=@P3 " & _
                                "WHERE NoKO=@P4"
                .Parameters.AddWithValue("@P1", DBNull.Value)
                .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P3", Now)
                .Parameters.AddWithValue("@P4", LblKO.Text)
                .ExecuteNonQuery()
            End With
        End Using

        ModalPDF.ShowOnPageLoad = False
    End Sub

    Private Sub BtnMix_Click(sender As Object, e As System.EventArgs) Handles BtnMix.Click
        If DDLJob.Value = "0" Then
            LblErr.Text = "Belum pilih Job."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        Session("KO") = "NEW|" & DDLJob.Text & "|||MIX"

        Response.Redirect("FrmEntryPO.aspx")
        Exit Sub
    End Sub

End Class