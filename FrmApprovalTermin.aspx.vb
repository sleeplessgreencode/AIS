Public Class FrmApprovalTermin
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "ApprovalTermin") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            TabPage.ActiveTabIndex = 0
            Call BindJob()
            Call BindGrid()
            Call BindGrid1()
        End If

        If Request.Params("AppTerminInduk") = 1 Then
            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    If Session("AppTermin").ToString.Split("|")(1) = "Unapprove" Then
                        .CommandText = "UPDATE TerminInduk SET UserUnApproval=@P1,TimeUnApproval=@P2,UserApproval=NULL,TimeApproval=NULL WHERE LedgerNo=@P3"
                        .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(0))
                        .Parameters.AddWithValue("@P2", Now)
                        .Parameters.AddWithValue("@P3", Session("AppTermin").ToString.Split("|")(0))
                    Else
                        .CommandText = "UPDATE TerminInduk SET UserApproval=@P1,TimeApproval=@P2 WHERE LedgerNo=@P3"
                        .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(0))
                        .Parameters.AddWithValue("@P2", Now)
                        .Parameters.AddWithValue("@P3", Session("AppTermin").ToString.Split("|")(0))
                    End If
                    .ExecuteNonQuery()
                End With
            End Using

            Call BindGrid()
        ElseIf Request.Params("AppTerminMember") = 1 Then
            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    If Session("AppTermin").ToString.Split("|")(1) = "Unapprove" Then
                        .CommandText = "UPDATE TerminMember SET UserUnApproval=@P1,TimeUnApproval=@P2,UserApproval=NULL,TimeApproval=NULL WHERE LedgerNo=@P3"
                        .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(0))
                        .Parameters.AddWithValue("@P2", Now)
                        .Parameters.AddWithValue("@P3", Session("AppTermin").ToString.Split("|")(0))
                    Else
                        .CommandText = "UPDATE TerminMember SET UserApproval=@P1,TimeApproval=@P2 WHERE LedgerNo=@P3"
                        .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(0))
                        .Parameters.AddWithValue("@P2", Now)
                        .Parameters.AddWithValue("@P3", Session("AppTermin").ToString.Split("|")(0))
                    End If
                    .ExecuteNonQuery()
                End With
            End Using

            Call BindGrid1()
        End If

    End Sub

    Private Sub BindGrid()
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Job WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    TxtBruto.Text = Format(RsFind("Bruto"), "N0")
                    TxtFisik.Text = Format(RsFind("Bruto") / 1.1, "N0")
                    TxtPPN.Text = Format((RsFind("Bruto") / 1.1) * 0.1, "N0")
                    TxtPPH.Text = Format((RsFind("Bruto") / 1.1) * 0.03, "N0")
                    TxtNetto.Text = Format(TxtBruto.Value - TxtPPN.Value - TxtPPH.Value, "N0")
                End If
            End Using
        End Using

        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM TerminInduk WHERE JobNo=@P1 ORDER BY TglCair DESC"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With

            Dim TtlKontrak As Decimal = 0
            Dim TtlFisik As Decimal = 0
            Dim TtlPPN As Decimal = 0
            Dim TtlPPH As Decimal = 0
            Dim TtlNetto As Decimal = 0
            Dim TtlMember1 As Decimal = 0
            Dim TtlMember2 As Decimal = 0
            Dim TtlBrutoBOQ As Decimal = 0, TtlUM As Decimal = 0, TtlRetensi As Decimal = 0
            Using RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                While RsGrid.Read
                    TtlKontrak += RsGrid("TerminInduk")
                    TtlBrutoBOQ += RsGrid("BrutoBOQ")
                    TtlUM += RsGrid("UM")
                    TtlRetensi += RsGrid("Retensi")
                    TtlFisik = TtlKontrak / 1.1
                    TtlPPN = TtlFisik * (10 / 100)
                    TtlPPH = TtlFisik * (3 / 100)
                    TtlNetto = TtlKontrak - TtlPPN - TtlPPH
                End While
            End Using

            TtlBrutoBOQ1.Text = Format(TtlBrutoBOQ, "N0")
            TtlUM1.Text = Format(TtlUM, "N0")
            TtlRetensi1.Text = Format(TtlRetensi, "N0")
            TxtBruto1.Text = Format(TtlKontrak, "N0")
            TxtFisik1.Text = Format(TtlFisik, "N0")
            TxtPPN1.Text = Format(TtlPPN, "N0")
            TxtPPH1.Text = Format(TtlPPH, "N0")
            TxtNetto1.Text = Format(TtlNetto, "N0")

            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    GridView.DataSource = DtGrid
                    GridView.DataBind()
                End Using
            End Using
        End Using

        TxtBruto2.Text = Format(TxtBruto.Value - TxtBruto1.Value, "N0")
        TxtFisik2.Text = Format(TxtFisik.Value - TxtFisik1.Value, "N0")
        TxtPPN2.Text = Format(TxtPPN.Value - TxtPPN1.Value, "N0")
        TxtPPH2.Text = Format(TxtPPH.Value - TxtPPH1.Value, "N0")
        TxtNetto2.Text = Format(TxtNetto.Value - TxtNetto1.Value, "N0")

    End Sub

    Private Sub BindGrid1()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM TerminMember WHERE JobNo=@P1 ORDER BY TglCair DESC"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With

            Dim TtlMember1 As Decimal = 0
            Dim TtlMember2 As Decimal = 0
            Dim TtlKSO As Decimal = 0
            Using RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
                While RsGrid.Read
                    TtlMember1 += RsGrid("TerminMember1")
                    TtlMember2 += RsGrid("TerminMember2")
                    TtlKSO += RsGrid("CadanganKSO")
                End While
            End Using

            GridView1.Columns(4).FooterText = Format(TtlMember1, "N0")
            GridView1.Columns(5).FooterText = Format(TtlMember2, "N0")
            GridView1.Columns(6).FooterText = Format(TtlKSO, "N0")

            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    GridView1.DataSource = DtGrid
                    GridView1.DataBind()
                End Using
            End Using
        End Using

    End Sub

    Private Sub GridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
        If e.CommandName = "BtnApproval" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)
            Dim UserApproved As Boolean
            Dim Uraian As String = TryCast(SelectRecord.FindControl("LblUraian"), Label).Text

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM TerminInduk WHERE LedgerNo=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        UserApproved = IsDBNull(RsFind("TimeApproval"))
                    End If
                End Using
            End Using

            Session("AppTermin") = SelectRecord.Cells(1).Text & If(UserApproved, "|Approve", "|Unapprove")
            MsgBox1.confirm("Anda yakin ingin " & If(UserApproved, "Approve", "Unapprove") & " data berikut?" & "\n" & Uraian, "AppTerminInduk")

        ElseIf e.CommandName = "BtnPDF" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM TerminInduk WHERE LedgerNo=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        If String.IsNullOrEmpty(RsFind("PDFFile").ToString) = True Then
                            MsgBox1.alert("Belum ada PDF.")
                            Exit Sub
                        End If
                        Session("ViewPDF") = Mid(RsFind("PDFFile").ToString, 3, Len(RsFind("PDFFile").ToString) - 2)
                        With DialogWindow1
                            .TargetUrl = "FrmViewPDF.aspx"
                            .Open()
                        End With
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
        DDLJob.Items.Add("Pilih salah satu", "0")
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo,JobNm FROM Job WHERE StatusJob=@P1 OR StatusJob=@P2"
                .Parameters.AddWithValue("@P1", "Pelaksanaan")
                .Parameters.AddWithValue("@P2", "Pemeliharaan")
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsFind("JobNo")) >= 0 Then
                        DDLJob.Items.Add(RsFind("JobNo") & " - " & RsFind("JobNm"), RsFind("JobNo"))
                    End If
                End While
            End Using
        End Using

        DDLJob.Value = "0"

    End Sub

    Private Sub GridView_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BtnApprove As LinkButton = TryCast(e.Row.Cells(15).Controls(0), LinkButton)
            Dim UserApproved As Boolean

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM TerminInduk WHERE LedgerNo=@P1"
                    .Parameters.AddWithValue("@P1", e.Row.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        UserApproved = IsDBNull(RsFind("UserApproval"))
                    End If
                End Using
                If UserApproved Then
                    BtnApprove.Text = "APPROVE"
                Else
                    BtnApprove.Text = "UNAPPROVE"
                End If
            End Using
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(3).Text = "Jumlah Penerimaan : "
            e.Row.Cells(3).Font.Bold = True

            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right

            e.Row.Cells.RemoveAt(9)
        End If
    End Sub

    Protected Sub DDLJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindGrid()
        Call BindGrid1()
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "BtnApproval" Then
            Dim SelectRecord As GridViewRow = GridView1.Rows(e.CommandArgument)
            Dim UserApproved As Boolean

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM TerminMember WHERE LedgerNo=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        UserApproved = IsDBNull(RsFind("TimeApproval"))
                    End If
                End Using
            End Using

            Session("AppTermin") = SelectRecord.Cells(1).Text & If(UserApproved, "|Approve", "|Unapprove")
            MsgBox1.confirm("Anda yakin ingin " & If(UserApproved, "Approve", "Unapprove") & " data berikut?" & "\n" & SelectRecord.Cells(4).Text, "AppTerminMember")

        End If

    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BtnApprove As LinkButton = TryCast(e.Row.Cells(10).Controls(0), LinkButton)
            Dim UserApproved As Boolean

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM TerminMember WHERE LedgerNo=@P1"
                    .Parameters.AddWithValue("@P1", e.Row.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        UserApproved = IsDBNull(RsFind("UserApproval"))
                    End If
                End Using
                If UserApproved Then
                    BtnApprove.Text = "APPROVE"
                Else
                    BtnApprove.Text = "UNAPPROVE"
                End If
            End Using
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(3).Text = "Jumlah Penerimaan : "
            e.Row.Cells(3).Font.Bold = True

            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right

            e.Row.Cells.RemoveAt(7)
        End If
    End Sub

End Class