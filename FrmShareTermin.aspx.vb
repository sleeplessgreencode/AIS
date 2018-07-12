Public Class FrmShareTermin
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If CheckAkses() = False Then
            Response.Redirect("Default.aspx")
            Exit Sub
        End If

        If IsPostBack = False Then
            Call BindJob()
            Call BindGrid()
        End If

    End Sub

    Private Sub BindGrid()
        Dim CmdGrid As New Data.SqlClient.SqlCommand
        With CmdGrid
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM ShareTermin WHERE JobNo=@P1 ORDER BY TglShare DESC"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
        End With

        Dim TtlMember1 As Decimal = 0
        Dim TtlKSO As Decimal = 0
        Dim TtlMember2 As Decimal = 0
        Dim RsGrid As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
        While RsGrid.Read
            TtlMember1 += RsGrid("ShareMember1")
            TtlKSO += RsGrid("ShareKSO")
            TtlMember2 += RsGrid("ShareMember2")
        End While
        RsGrid.Close()
        GridData.Columns(3).FooterText = Format(TtlMember1, "N0")
        GridData.Columns(4).FooterText = Format(TtlKSO, "N0")
        GridData.Columns(5).FooterText = Format(TtlMember2, "N0")

        Dim DaGrid As New Data.SqlClient.SqlDataAdapter
        DaGrid.SelectCommand = CmdGrid
        Dim DtGrid As New Data.DataTable
        DaGrid.Fill(DtGrid)
        GridData.DataSource = DtGrid
        GridData.DataBind()

        DaGrid.Dispose()
        DtGrid.Dispose()
        CmdGrid.Dispose()

    End Sub

    Private Sub GridData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            Session("Share") = DDLJob.Value & "|" & SelectRecord.Cells(1).Text

            TxtAction.Text = "UPD"
            TglShare.Date = SelectRecord.Cells(1).Text
            TxtKeterangan.Text = SelectRecord.Cells(2).Text
            TxtTermin1.Text = SelectRecord.Cells(3).Text
            TxtTerminKSO.Text = SelectRecord.Cells(4).Text
            TxtTermin2.Text = SelectRecord.Cells(5).Text

            Call FindMember()

            PopEntry.ShowOnPageLoad = True

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
            .CommandText = "SELECT JobNo,JobNm FROM Job WHERE (StatusJob=@P1 OR StatusJob=@P2) AND KSO=@P3"
            .Parameters.AddWithValue("@P1", "Pelaksanaan")
            .Parameters.AddWithValue("@P2", "Pemeliharaan")
            .Parameters.AddWithValue("@P3", "1")
        End With
        Dim RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
        While RsIsi.Read
            If AksesJob = "*" Then
                DDLJob.Items.Add(RsIsi("JobNo") & " - " & RsIsi("JobNm"), RsIsi("JobNo"))
            Else
                If Array.IndexOf(AksesJob.Split(","), RsIsi("JobNo")) >= 0 Then
                    DDLJob.Items.Add(RsIsi("JobNo") & " - " & RsIsi("JobNm"), RsIsi("JobNo"))
                End If
            End If
        End While
        RsIsi.Close()
        CmdIsi.Dispose()

        DDLJob.Value = "0"
    End Sub

    Private Sub GridData_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridData.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            If DDLJob.Value = "0" Then Exit Sub
            e.Row.Cells(3).Text = FindMember.Split("|")(0)
            e.Row.Cells(5).Text = FindMember.Split("|")(1)
        ElseIf e.Row.RowType = DataControlRowType.Footer Then

            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(2).Text = "Jumlah : "
            e.Row.Cells(2).Font.Bold = True

            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right

        End If

    End Sub

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If DDLJob.Value = "0" Then
            LblErr.Text = "Belum pilih Job."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        TxtAction.Text = "NEW"
        TglShare.Date = Today
        TxtKeterangan.Text = ""
        TxtTermin1.Text = "0"
        TxtTermin2.Text = "0"
        TxtTerminKSO.Text = "0"

        Call FindMember()

        PopEntry.ShowOnPageLoad = True

    End Sub

    Private Function CheckAkses() As Boolean
        If Session("User") = "" Then
            Return False
        Else
            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT AksesMenu FROM Login WHERE UserID=@P1"
                .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
            End With
            Dim AksesMenu As String = ""
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                AksesMenu = RsFind("AksesMenu").ToString
            End If
            RsFind.Close()
            CmdFind.Dispose()

            If AksesMenu = "*" Then Return True
            If Array.IndexOf(AksesMenu.Split(","), _
                HttpContext.Current.Request.Url.AbsolutePath.Split("/")(1).Split(".")(0)) < 0 Then
                Return False
            End If
        End If

        Return True
    End Function

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If TglShare.Text = "" Then
            LblErr.Text = "Tgl belum di-input."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        If TxtAction.Text = "NEW" Then
            If CheckUnique() = False Then Exit Sub

            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO ShareTermin(JobNo,TglShare,Keterangan,ShareMember1,ShareKSO,ShareMember2,UserEntry,TimeEntry) " & _
                               "VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", TglShare.Date)
                .Parameters.AddWithValue("@P3", TxtKeterangan.Text)
                .Parameters.AddWithValue("@P4", TxtTermin1.Value)
                .Parameters.AddWithValue("@P5", TxtTerminKSO.Value)
                .Parameters.AddWithValue("@P6", TxtTermin2.Value)
                .Parameters.AddWithValue("@P7", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P8", Now)
                Try
                    .ExecuteNonQuery()
                Catch
                    .Dispose()
                    LblErr.Text = Err.Description
                    ErrMsg.ShowOnPageLoad = True
                End Try
                .Dispose()
            End With
        Else
            If TglShare.Date <> Session("Share").ToString.Split("|")(1) Then
                If CheckUnique() = False Then Exit Sub
            End If

            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE ShareTermin SET TglShare=@P1,Keterangan=@P2,ShareMember1=@P3,ShareKSO=@P4,ShareMember2=@P5," & _
                               "UserEntry=@P6,TimeEntry=@P7 WHERE JobNo=@P8 AND TglShare=@P9"
                .Parameters.AddWithValue("@P1", TglShare.Date)
                .Parameters.AddWithValue("@P2", TxtKeterangan.Text)
                .Parameters.AddWithValue("@P3", TxtTermin1.Value)
                .Parameters.AddWithValue("@P4", TxtTerminKSO.Value)
                .Parameters.AddWithValue("@P5", TxtTermin2.Value)
                .Parameters.AddWithValue("@P6", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P7", Now)
                .Parameters.AddWithValue("@P8", Session("Share").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P9", Session("Share").ToString.Split("|")(1))
                Try
                    .ExecuteNonQuery()
                Catch
                    .Dispose()
                    LblErr.Text = Err.Description
                    ErrMsg.ShowOnPageLoad = True
                End Try
                .Dispose()
            End With
        End If

        Call BindGrid()

        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub

    Private Function CheckUnique() As Boolean
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM ShareTermin WHERE JobNo=@P1 AND TglShare=@P2"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", TglShare.Date)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            RsFind.Close()
            CmdFind.Dispose()
            LblErr.Text = "Tanggal " & Format(TglShare.Date, "dd-MMM-yyyy") & " sudah pernah di-input."
            ErrMsg.ShowOnPageLoad = True
            Return False
        End If
        RsFind.Close()
        CmdFind.Dispose()

        Return True
    End Function

    Private Sub BtnCancel_Click(sender As Object, e As System.EventArgs) Handles BtnCancel.Click
        Session.Remove("Share")
        PopEntry.ShowOnPageLoad = False
    End Sub

    Protected Sub DDLJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindGrid()
    End Sub

    Private Function FindMember() As String
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT A.NamaPartner, B.CompanyNm FROM Job A JOIN Company B ON A.CompanyId=B.CompanyId WHERE A.JobNo=@P1"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            LblMember1.Text = RsFind("NamaPartner").ToString
            LblMember2.Text = RsFind("CompanyNm").ToString
            RsFind.Close()
            CmdFind.Dispose()
            Return LblMember1.Text + "|" + LblMember2.Text
        End If
        RsFind.Close()
        CmdFind.Dispose()

        Return ""

    End Function
End Class
