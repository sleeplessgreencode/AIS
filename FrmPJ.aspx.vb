﻿Public Class FrmPJ
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "PJ") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Call BindJob()
            Call BindAlokasi()
            Call BindGrid()
        End If

        If Session("Job") <> "" Then
            DDLJob.Text = Session("Job").ToString.Split("|")(0)
            DDLAlokasi.Value = Session("Job").ToString.Split("|")(1)
            Session.Remove("Job")
            Call BindGrid()
        End If

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
        DDLAlokasi.Items.Add("Pilih salah satu", "0")
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

        DDLAlokasi.Value = "0"
    End Sub

    Private Sub BindGrid()
        Dim CmdGrid As New Data.SqlClient.SqlCommand
        With CmdGrid
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM PdHdr WHERE JobNo=@P1 AND Alokasi=@P2 " + GetFilter1() + GetFilter2() + " AND RejectBy IS NULL ORDER BY TglPD DESC, NoPD DESC"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
        End With

        Dim Ttl As Decimal = 0
        Dim RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
        While RsGrid.Read
            Ttl += If(IsDBNull(RsGrid("TotalPJ")) = True, 0, RsGrid("TotalPJ"))
        End While
        GridPD.Columns(6).FooterText = Format(Ttl, "N0")
        RsGrid.Close()

        Dim DaGrid As New Data.SqlClient.SqlDataAdapter
        DaGrid.SelectCommand = CmdGrid
        Dim DtGrid As New Data.DataTable
        DaGrid.Fill(DtGrid)
        GridPD.DataSource = DtGrid
        GridPD.DataBind()

        DaGrid.Dispose()
        DtGrid.Dispose()
        CmdGrid.Dispose()

    End Sub

    Private Sub GridPD_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridPD.PageIndexChanging
        GridPD.PageIndex = e.NewPageIndex
        GridPD.DataBind()
        Call BindGrid()
    End Sub

    Private Sub GridPD_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridPD.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridPD.Rows(e.CommandArgument)

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT NoPD, NoRef," & _
                                   "(SELECT ISNULL(COUNT(LedgerNo),0) FROM BLE WHERE (NoPD=PdHdr.NoPD OR NoPD=PdHdr.NoRef)) AS CountBLE " & _
                                   "FROM PdHdr WHERE NoPD=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(0).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If Not RsFind.Read Then
                        LblErr.Text = SelectRecord.Cells(0).Text & " Not Found."
                        ErrMsg.ShowOnPageLoad = True
                        Exit Sub
                    Else
                        If RsFind("CountBLE") = 0 Then
                            LblErr.Text = "Belum ada pembayaran untuk No " & SelectRecord.Cells(0).Text
                            ErrMsg.ShowOnPageLoad = True
                            Exit Sub
                        End If
                    End If
                End Using
            End Using

            If SelectRecord.Cells(7).Text = "&nbsp;" Then
                Session("PJ") = "UPD|" & DDLJob.Text & "|" & SelectRecord.Cells(0).Text & "|" & DDLAlokasi.Value

            Else
                Session("PJ") = "SEE|" & DDLJob.Text & "|" & SelectRecord.Cells(0).Text & "|" & DDLAlokasi.Value
            End If
            Response.Redirect("FrmEntryPJ.aspx")

            Exit Sub
        ElseIf e.CommandName = "BtnApprove" Then
            Dim SelectRecord As GridViewRow = GridPD.Rows(e.CommandArgument)

            If SelectRecord.Cells(4).Text = "&nbsp;" Then
                LblErr.Text = SelectRecord.Cells(0).Text & " belum entry uraian PJ."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            If SelectRecord.Cells(7).Text <> "&nbsp;" Then
                Using CmdFind As New Data.SqlClient.SqlCommand
                    With CmdFind
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT * FROM PdHdr WHERE NoPD=@P1"
                        .Parameters.AddWithValue("@P1", SelectRecord.Cells(0).Text)
                    End With
                    Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                        If RsFind.Read Then
                            If String.IsNullOrEmpty(RsFind("RejectBy").ToString) = False Then
                                LblErr.Text = SelectRecord.Cells(0).Text & " sudah di-reject oleh " & RsFind("RejectBy")
                                ErrMsg.ShowOnPageLoad = True
                                Exit Sub
                            End If
                            If String.IsNullOrEmpty(RsFind("PjApprovedByKK").ToString) = False Then
                                LblErr.Text = SelectRecord.Cells(4).Text & " sudah di-Approved oleh Koord. Keuangan."
                                ErrMsg.ShowOnPageLoad = True
                                Exit Sub
                            End If
                        End If
                    End Using
                End Using
                'LblErr.Text = SelectRecord.Cells(0).Text & " sudah di-Approved."
                'ErrMsg.ShowOnPageLoad = True
                'Exit Sub
            End If
            Session("Approve") = SelectRecord.Cells(0).Text & If(SelectRecord.Cells(7).Text = "&nbsp;", "|Approve", "|UnApprove")

            LblDel.Text = "Anda yakin ingin " & If(SelectRecord.Cells(7).Text = "&nbsp;", "Approve", "UnApprove") & " data berikut?" & "<br />" & _
                           SelectRecord.Cells(4).Text & " - " & SelectRecord.Cells(2).Text
            DelMsg.ShowOnPageLoad = True
        ElseIf e.CommandName = "BtnPrint" Then
            Dim SelectRecord As GridViewRow = GridPD.Rows(e.CommandArgument)

            If SelectRecord.Cells(7).Text = "&nbsp;" Then
                LblErr.Text = SelectRecord.Cells(4).Text & " belum di-Approved."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            Session("Print") = SelectRecord.Cells(0).Text & "|" & DDLJob.Text
            Response.Redirect("FrmRptPJ.aspx")
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

    Private Sub GridPD_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridPD.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BtnApprove As LinkButton = TryCast(e.Row.Cells(10).Controls(0), LinkButton)

            If e.Row.Cells(7).Text = "&nbsp;" Then
                BtnApprove.Text = "APPROVE"
            Else
                BtnApprove.Text = "UNAPPROVE"
            End If

            Dim link As LinkButton = TryCast(e.Row.Cells(11).Controls(0), LinkButton)

            If link IsNot Nothing Then
                If e.Row.Cells(7).Text <> "&nbsp;" Then link.Attributes.Add("onclick", "OpenNewTab();")
            End If

        End If
        If e.Row.RowType = DataControlRowType.Footer Then

            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).Text = "Total : "
            e.Row.Cells(5).Font.Bold = True

        End If

    End Sub

    Protected Sub BtnDel_Click(sender As Object, e As EventArgs) Handles BtnDel.Click
        Dim NoPD As String = Session("Approve").ToString.Split("|")(0)
        Dim Action As String = Session("Approve").ToString.Split("|")(1)
        Dim CmdEdit As New Data.SqlClient.SqlCommand
        With CmdEdit
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "UPDATE PdHdr SET PjApprovedByAK=@P1,PjTimeApprovedAK=@P2 WHERE NoPD=@P3"
            .Parameters.AddWithValue("@P1", If(Action = "UnApprove", DBNull.Value, Session("User").ToString.Split("|")(0)))
            .Parameters.AddWithValue("@P2", If(Action = "UnApprove", DBNull.Value, Now))
            .Parameters.AddWithValue("@P3", NoPD)
            .ExecuteNonQuery()
            .Dispose()
        End With
        Session.Remove("Approve")

        Call BindGrid()
    End Sub

    Private Function GetFilter1() As String
        If DDLField1.Value <> "0" Then
            Return "AND " + DDLField1.Value + GetFilterFunction1()
        End If

        Return ""
    End Function

    Private Function GetFilterFunction1() As String
        If DDLFilterBy1.Value = "0" Then
            If DDLField1.Value <> "TglPD" And DDLField1.Value <> "TglPJ" Then
                Return "='" + TxtFind1.Text + "'"
            Else
                Return "='" + TglFilter1.Date + "'"
            End If
        ElseIf DDLFilterBy1.Value = "1" Then
            If DDLField1.Value <> "TglPD" And DDLField1.Value <> "TglPJ" Then
                Return " LIKE '%" + TxtFind1.Text + "%'"
            Else
                Return ""
            End If
        ElseIf DDLFilterBy1.Value = "2" Then
            If DDLField1.Value <> "TglPD" And DDLField1.Value <> "TglPJ" Then
                Return ">=" + TxtFind1.Text
            Else
                Return ">='" + TglFilter1.Date + "'"
            End If
        ElseIf DDLFilterBy1.Value = "3" Then
            If DDLField1.Value <> "TglPD" And DDLField1.Value <> "TglPJ" Then
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
        If DDLField1.Value = "TglPD" Or DDLField1.Value = "TglPJ" Then
            TxtFind1.Visible = False
            TglFilter1.Visible = True
            DDLFilterBy1.Items.Add("Equals", "0")
            DDLFilterBy1.Items.Add("Is Greather Than Or Equal To", "2")
            DDLFilterBy1.Items.Add("Is Less Than Or Equal To", "3")
        ElseIf DDLField1.Value = "TotalPD" Or DDLField1.Value = "TotalPJ" Then
            TxtFind1.Text = "0"
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
        If DDLField1.Value = "TglPD" Or DDLField1.Value = "TglPJ" Then
            TxtFind2.Visible = False
            TglFilter2.Visible = True
            DDLFilterBy2.Items.Add("Equals", "0")
            DDLFilterBy2.Items.Add("Is Greather Than Or Equal To", "2")
            DDLFilterBy2.Items.Add("Is Less Than Or Equal To", "3")
        ElseIf DDLField1.Value = "TotalPD" Or DDLField1.Value = "TotalPJ" Then
            TxtFind2.Text = "0"
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
            If DDLField1.Value <> "TglPD" And DDLField1.Value <> "TglPJ" Then
                Return "='" + TxtFind2.Text + "'"
            Else
                Return "='" + TglFilter2.Date + "'"
            End If
        ElseIf DDLFilterBy2.Value = "1" Then
            If DDLField1.Value <> "TglPD" And DDLField1.Value <> "TglPJ" Then
                Return "LIKE '%" + TxtFind2.Text + "%'"
            Else
                Return ""
            End If
        ElseIf DDLFilterBy2.Value = "2" Then
            If DDLField1.Value <> "TglPD" And DDLField1.Value <> "TglPJ" Then
                Return ">=" + TxtFind2.Text
            Else
                Return ">='" + TglFilter2.Date + "'"
            End If
        ElseIf DDLFilterBy2.Value = "3" Then
            If DDLField1.Value <> "TglPD" And DDLField1.Value <> "TglPJ" Then
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

    Private Sub DDLAlokasi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLAlokasi.SelectedIndexChanged
        Call BindGrid()
    End Sub

End Class