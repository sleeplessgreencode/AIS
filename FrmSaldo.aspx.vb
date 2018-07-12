Public Class FrmSaldo
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
        End If

        Call BindGrid()
    End Sub

    Private Sub BindGrid()
        Dim CmdGrid As New Data.SqlClient.SqlCommand
        With CmdGrid
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Saldo WHERE JobNo=@P1 ORDER BY TglSaldo DESC"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
        End With

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

            Session("Saldo") = DDLJob.Value & "|" & SelectRecord.Cells(0).Text & "|" & SelectRecord.Cells(1).Text

            TxtAction.Text = "UPD"
            DDLTipe.Value = SelectRecord.Cells(0).Text
            TglSaldo.Date = SelectRecord.Cells(1).Text
            TxtNominal.Text = SelectRecord.Cells(2).Text
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
            .CommandText = "SELECT JobNo,JobNm FROM Job WHERE StatusJob=@P1"
            .Parameters.AddWithValue("@P1", "Pelaksanaan")
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

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If DDLJob.Value = "0" Then
            LblErr.Text = "Belum pilih Job."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        TxtAction.Text = "NEW"
        TglSaldo.Date = Today
        TxtNominal.Value = "0"
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
        If TglSaldo.Text = "" Then
            LblErr.Text = "Tanggal belum di-input."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtNominal.Text = "" Or CDec(TxtNominal.Text) = 0 Then
            LblErr.Text = "Saldo belum di-input."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        If TxtAction.Text = "NEW" Then
            If CheckUnique() = False Then Exit Sub

            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Saldo(JobNo,Tipe,TglSaldo,Nominal,UserEntry,TimeEntry) VALUES " & _
                               "(@P1,@P2,@P3,@P4,@P5,@P6)"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", DDLTipe.Value)
                .Parameters.AddWithValue("@P3", TglSaldo.Date)
                .Parameters.AddWithValue("@P4", TxtNominal.Text)
                .Parameters.AddWithValue("@P5", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P6", Now)
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
            If TglSaldo.Date <> Session("SALDO").ToString.Split("|")(2) Or
               DDLTipe.Value <> Session("SALDO").ToString.Split("|")(1) Then
                If CheckUnique() = False Then Exit Sub
            End If

            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE Saldo SET TglSaldo=@P1,Nominal=@P2,UserEntry=@P3,TimeEntry=@P4 " & _
                               "WHERE JobNo=@P5 AND Tipe=@P6 AND TglSaldo=@P7"
                .Parameters.AddWithValue("@P1", TglSaldo.Date)
                .Parameters.AddWithValue("@P2", TxtNominal.Text)
                .Parameters.AddWithValue("@P3", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P4", Now)
                .Parameters.AddWithValue("@P5", Session("SALDO").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P6", Session("SALDO").ToString.Split("|")(1))
                .Parameters.AddWithValue("@P7", Session("SALDO").ToString.Split("|")(2))
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

    Private Sub BtnCancel_Click(sender As Object, e As System.EventArgs) Handles BtnCancel.Click
        Session.Remove("SALDO")
        PopEntry.ShowOnPageLoad = False
    End Sub

    Private Function CheckUnique() As Boolean

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Saldo WHERE JobNo=@P1 AND Tipe=@P2 AND TglSaldo=@P3"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", DDLTipe.Value)
            .Parameters.AddWithValue("@P3", TglSaldo.Date)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            RsFind.Close()
            CmdFind.Dispose()
            LblErr.Text = "Saldo " & DDLTipe.Value & " dengan tanggal " & Format(TglSaldo.Date, "dd-MMM-yyyy") & " sudah pernah di-input."
            ErrMsg.ShowOnPageLoad = True
            Return False
        End If
        RsFind.Close()
        CmdFind.Dispose()

        Return True
    End Function
End Class