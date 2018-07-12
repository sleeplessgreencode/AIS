Public Class FrmInvoice
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim DtGrid As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "Invoice") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        DtGrid = Session("DtGrid")
        DDLKo.DataSource = DtGrid
        DDLKo.DataBind()

        If IsPostBack = False Then
            ViewState("ColumnName") = "InvDate"
            ViewState("SortOrder") = "DESC"

            Call BindJob()
            Call BindGrid()
        End If

        If Request.Params("Confirm") = 1 Then
            Using CmdDelete As New Data.SqlClient.SqlCommand
                With CmdDelete
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "DELETE FROM Invoice WHERE JobNo=@P1 AND NoKO=@P2 AND InvNo=@P3"
                    .Parameters.AddWithValue("@P1", Session("Delete").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P2", Session("Delete").ToString.Split("|")(1))
                    .Parameters.AddWithValue("@P3", Session("Delete").ToString.Split("|")(2))
                    .ExecuteNonQuery()
                End With
            End Using

            Session.Remove("Delete")
            Call BindGrid()

        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindGrid()
        Dim TmpJoin As String = If(DDLField1.Text = String.Empty, String.Empty, "AND " & DDLField1.Value & " LIKE '%" & TxtFind1.Text & "%'")

        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, C.VendorNm FROM Invoice A LEFT JOIN KoHdr B ON A.NoKO=B.NoKO LEFT JOIN Vendor C ON B.VendorId=C.VendorId WHERE " & _
                               "A.JobNo=@P1 " & TmpJoin
                .Parameters.AddWithValue("@P1", If(DDLJob.Text = String.Empty, String.Empty, DDLJob.Value))
                .CommandText = .CommandText & If(ViewState("ColumnName") = "", "", "ORDER BY " & ViewState("ColumnName") & " " & ViewState("SortOrder"))
            End With

            Dim Ttl As Decimal = 0
            Using RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                While RsGrid.Read
                    Ttl += RsGrid("Total")
                End While
                GridView.Columns(5).FooterText = Format(Ttl, "N0")
            End Using

            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    GridView.DataSource = DtGrid
                    GridView.DataBind()
                End Using
            End Using
        End Using

    End Sub

    Private Sub GridView_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView.PageIndexChanging
        GridView.PageIndex = e.NewPageIndex
        GridView.DataBind()
        Call BindGrid()
    End Sub

    Private Sub GridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
       If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)

            DDLKo.Enabled = True
            TxtInvNo.Enabled = True
            TxtTglInv.Enabled = True
            TxtDueDate.Enabled = True
            TxtFP.Enabled = True
            TxtTglFP.Enabled = True
            TxtTotal.Enabled = True
            TxtPPN.Enabled = True
            TxtKeterangan.Enabled = True

            Action.Text = "UPD"
            DDLKo.Value = SelectRecord.Cells(0).Text
            TxtInvNo.Text = SelectRecord.Cells(2).Text
            TxtTglInv.Date = SelectRecord.Cells(3).Text
            TxtDueDate.Date = SelectRecord.Cells(4).Text
            TxtFP.Text = If(SelectRecord.Cells(7).Text <> "&nbsp;", SelectRecord.Cells(7).Text, String.Empty)
            If SelectRecord.Cells(8).Text <> "&nbsp;" Then
                TxtTglFP.Date = SelectRecord.Cells(8).Text
            Else
                TxtTglFP.Text = String.Empty
            End If
            TxtTotal.Text = SelectRecord.Cells(5).Text
            TxtPPN.Text = SelectRecord.Cells(6).Text
            TxtKeterangan.Text = TryCast(SelectRecord.FindControl("LblKeterangan"), Label).Text.Replace("<br />", Environment.NewLine)

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT NoPD FROM InvPD WHERE JobNo=@P1 AND NoKO=@P2 AND InvNo=@P3"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", SelectRecord.Cells(0).Text)
                    .Parameters.AddWithValue("@P3", SelectRecord.Cells(2).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        'msgBox1.alert("Invoice tidak bisa di update. \nSudah digunakan untuk PD" & RsFind(0))
                        'Exit Sub
                        DDLKo.Enabled = False
                        TxtInvNo.Enabled = False
                        TxtTglInv.Enabled = False
                        TxtDueDate.Enabled = False
                        TxtTotal.Enabled = False
                        TxtPPN.Enabled = False
                    End If
                End Using
            End Using

            Session("Invoice") = DDLJob.Value & "|" & SelectRecord.Cells(0).Text & "|" & SelectRecord.Cells(2).Text

            ModalEntry.ShowOnPageLoad = True

        ElseIf e.CommandName = "BtnDelete" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT NoPD FROM InvPD WHERE JobNo=@P1 AND NoKO=@P2 AND InvNo=@P3"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", SelectRecord.Cells(0).Text)
                    .Parameters.AddWithValue("@P3", SelectRecord.Cells(2).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        msgBox1.alert("Invoice tidak bisa di delete. \nSudah digunakan untuk " & RsFind(0))
                        Exit Sub
                    End If
                End Using
            End Using

            Session("Delete") = DDLJob.Value & "|" & SelectRecord.Cells(0).Text & "|" & SelectRecord.Cells(2).Text
            msgBox1.confirm("Confirm delete invoice " & SelectRecord.Cells(2).Text & " ?", "Confirm")

        End If

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

    Protected Sub DDLJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindGrid()
        Call BindKO()
    End Sub

    Private Sub GridView_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then

            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).Text = "Total : "
            e.Row.Cells(4).Font.Bold = True

            e.Row.Cells.RemoveAt(9)
        End If
    End Sub

    Private Sub GridView_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView.Sorting
        If e.SortExpression = ViewState("ColumnName").ToString() Then
            If ViewState("SortOrder").ToString() = "ASC" Then
                ViewState("SortOrder") = "DESC"
            Else
                ViewState("SortOrder") = "ASC"
            End If
        Else
            ViewState("ColumnName") = e.SortExpression
            ViewState("SortOrder") = "ASC"
        End If

        Call BindGrid()
    End Sub

    Private Sub BtnFind_Click(sender As Object, e As System.EventArgs) Handles BtnFind.Click
        Call BindGrid()
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As System.EventArgs) Handles BtnAdd.Click
        If DDLJob.Text = String.Empty Then
            msgBox1.alert("Belum pilih Job.")
            DDLJob.Focus()
            Exit Sub
        End If

        Action.Text = "NEW"
        DDLKo.Text = ""
        TxtInvNo.Text = ""
        TxtTglInv.Text = ""
        TxtDueDate.Text = ""
        TxtFP.Text = ""
        TxtTglFP.Text = ""
        TxtTotal.Text = "0"
        TxtPPN.Text = "0"
        TxtKeterangan.Text = ""
        DDLKO.Focus()

        ModalEntry.ShowOnPageLoad = True
    End Sub

    Private Sub BindKO()
        Session.Remove("DtGrid")

        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.NoKO, FORMAT(A.TglKO,'dd-MMM-yyyy') AS TglKO, B.VendorNm, FORMAT((A.SubTotal - A.DiscAmount) + A.PPN,'N0') AS TotalKO " & _
                               "FROM KoHdr A JOIN Vendor B ON A.VendorId=B.VendorId WHERE " & _
                               "JobNo=@P1 AND ApprovedBy IS NOT NULL ORDER BY NoKO DESC"
                .Parameters.AddWithValue("@P1", If(DDLJob.Text = String.Empty, String.Empty, DDLJob.Value))
            End With
            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid1 As New Data.DataTable
                    DaGrid.Fill(DtGrid1)
                    DDLKo.DataSource = DtGrid1
                    DDLKo.DataBind()
                    Session("DtGrid") = DtGrid1
                End Using
            End Using
        End Using

    End Sub

    Private Sub TxtTotal_ValueChanged(sender As Object, e As System.EventArgs) Handles TxtTotal.ValueChanged
        If TxtTotal.Text = "0" Then
            TxtPPN.Text = "0"
            Exit Sub
        End If

        If TxtFP.Text <> "   .   -  ." Then
            TxtPPN.Text = Format(TxtTotal.Value - (TxtTotal.Value / 1.1), "N0")
        Else
            TxtPPN.Text = "0"
        End If

    End Sub

    Private Sub BtnSave_Click(sender As Object, e As System.EventArgs) Handles BtnSave.Click
        If TxtDueDate.Date < TxtTglInv.Date Then
            msgBox1.alert("Jatuh Tempo < Tgl Invoice.")
            TxtDueDate.Focus()
            Exit Sub
        End If
        If TxtPPN.Text <> "0" Then
            If TxtFP.Text = "   .   -  ." Or TxtTglFP.Text = String.Empty Then
                msgBox1.alert("Nomor/Tgl faktur pajak belum diisi.")
                TxtFP.Focus()
                Exit Sub
            End If
        End If
        If TxtFP.Text <> "   .   -  ." Or TxtTglFP.Text <> String.Empty Then
            If TxtPPN.Text = "0" Then
                msgBox1.alert("PPN belum diisi.")
                TxtFP.Focus()
                Exit Sub
            End If
        End If
        If TxtTotal.Text = "0" Then
            msgBox1.alert("Total belum diisi.")
            TxtFP.Focus()
            Exit Sub
        End If

        If ValidasiTotalInv() = False Then Exit Sub

        If Action.Text = "NEW" Then
            If ValidasiInv() = False Then Exit Sub

            Using CmdInsert As New Data.SqlClient.SqlCommand
                With CmdInsert
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO Invoice (JobNo,NoKO,InvNo,InvDate,DueDate,PPN,FPNo,FPDate," & _
                                    "Total,UserEntry,TimeEntry,Keterangan) VALUES(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12)"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", DDLKo.Value)
                    .Parameters.AddWithValue("@P3", TxtInvNo.Text)
                    .Parameters.AddWithValue("@P4", TxtTglInv.Text)
                    .Parameters.AddWithValue("@P5", TxtDueDate.Text)
                    .Parameters.AddWithValue("@P6", TxtPPN.Text)
                    .Parameters.AddWithValue("@P7", If(TxtFP.Text = "   .   -  .", DBNull.Value, TxtFP.Text))
                    .Parameters.AddWithValue("@P8", If(TxtTglFP.Text = String.Empty, DBNull.Value, TxtTglFP.Text))
                    .Parameters.AddWithValue("@P9", TxtTotal.Text)
                    .Parameters.AddWithValue("@P10", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P11", Now)
                    .Parameters.AddWithValue("@P12", TxtKeterangan.Text)
                    .ExecuteNonQuery()
                End With
            End Using
        Else
            If DDLKo.Value <> Session("Invoice").ToString.Split("|")(1) Or
                TxtInvNo.Text <> Session("Invoice").ToString.Split("|")(2) Then
                If ValidasiInv() = False Then Exit Sub
            End If

            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE Invoice SET NoKO=@P1,InvNo=@P2,InvDate=@P3,DueDate=@P4,PPN=@P5,FPNo=@P6,FPDate=@P7,Total=@P8,UserEntry=@P9," & _
                                    "TimeEntry=@P10,Keterangan=@P11 WHERE JobNo=@P12 AND NoKO=@P13 AND InvNo=@P14"
                    .Parameters.AddWithValue("@P1", DDLKo.Value)
                    .Parameters.AddWithValue("@P2", TxtInvNo.Text)
                    .Parameters.AddWithValue("@P3", TxtTglInv.Text)
                    .Parameters.AddWithValue("@P4", TxtDueDate.Text)
                    .Parameters.AddWithValue("@P5", TxtPPN.Text)
                    .Parameters.AddWithValue("@P6", If(TxtFP.Text = "   .   -  .", DBNull.Value, TxtFP.Text))
                    .Parameters.AddWithValue("@P7", If(TxtTglFP.Text = String.Empty, DBNull.Value, TxtTglFP.Text))
                    .Parameters.AddWithValue("@P8", TxtTotal.Text)
                    .Parameters.AddWithValue("@P9", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P10", Now)
                    .Parameters.AddWithValue("@P11", TxtKeterangan.Text)
                    .Parameters.AddWithValue("@P12", Session("Invoice").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P13", Session("Invoice").ToString.Split("|")(1))
                    .Parameters.AddWithValue("@P14", Session("Invoice").ToString.Split("|")(2))
                    .ExecuteNonQuery()
                End With
            End Using
        End If

        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub

    Private Sub TxtFP_ValueChanged(sender As Object, e As System.EventArgs) Handles TxtFP.ValueChanged
        TxtTotal_ValueChanged(TxtTotal, New EventArgs())
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As System.EventArgs) Handles BtnCancel.Click
        Session.Remove("Invoice")
        Call BindGrid()
        ModalEntry.ShowOnPageLoad = False
    End Sub

    Private Function ValidasiInv() As Boolean
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT InvNo FROM Invoice WHERE JobNo=@P1 AND NoKO=@P2 AND InvNo=@P3"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", DDLKo.Value)
                .Parameters.AddWithValue("@P3", TxtInvNo.Text)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    msgBox1.alert("Sudah ada no. invoice " & TxtInvNo.Text & ".")
                    TxtInvNo.Focus()
                    Return False
                End If
            End Using
        End Using

        Return True
    End Function

    Private Function ValidasiTotalInv() As Boolean
        Dim TmpJoin As String = If(Action.Text = "NEW", String.Empty, "AND InvNo!='" & Session("Invoice").ToString.Split("|")(2) & "'")

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT SUM(Total), " & _
                               "(SELECT SubTotal - DiscAmount + PPN AS TotalKO FROM KoHdr WHERE NoKO=@P2 AND KategoriId!='KONTRAK') AS TotalKO " & _
                               "FROM Invoice WHERE JobNo=@P1 AND NoKO=@P2 " & TmpJoin
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", DDLKo.Value)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    If IsDBNull(RsFind(1)) = False Then
                        If RsFind(0) + TxtTotal.Value > RsFind(1) Then
                            msgBox1.alert("Total invoice melebihi total " & DDLKo.Value)
                            Return False
                        End If
                    End If
                End If
            End Using
        End Using

        Return True
    End Function

End Class