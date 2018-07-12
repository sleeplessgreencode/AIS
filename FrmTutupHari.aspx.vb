Public Class FrmTutupHari
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

    Private Sub BtnTutup_Click(sender As Object, e As System.EventArgs) Handles BtnTutup.Click
        If DDLJob.Value = "0" Then
            LblErr.Text = "Belum pilih Job."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        Dim PrdAwal As Date = DateAdd("m", 0, DateSerial(Year(Today), Month(Today), 1))
        'Dim PrdAwal As Date = "2016-1-1"
        Dim PrdAkhir As Date = Today

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT B.AccNo, A.Site, Sum(B.Debet), Sum(B.Kredit), A.GlPeriode " & _
                           "FROM GlHdr A JOIN GlDtl B ON A.NoInt = B.NoInt " & _
                           "WHERE A.JobNo=@P1 AND A.TglNota>=@P2 AND A.TglNota<=@P3 " & _
                           "GROUP By B.AccNo, A.Site, A.GlPeriode"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            .Parameters.AddWithValue("@P2", PrdAwal.Date)
            .Parameters.AddWithValue("@P3", PrdAkhir.Date)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        While RsFind.Read
            Dim CmdFind1 As New Data.SqlClient.SqlCommand
            With CmdFind1
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM MutasiAcc WHERE JobNo=@P1 AND Site=@P2 AND AccNo=@P3 AND Tahun=@P4 AND Bulan=@P5"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", RsFind("Site"))
                .Parameters.AddWithValue("@P3", RsFind("AccNo"))
                .Parameters.AddWithValue("@P4", Mid(RsFind("GlPeriode"), 1, 4))
                .Parameters.AddWithValue("@P5", Mid(RsFind("GlPeriode"), 5, 2))
            End With
            Dim RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
            If Not RsFind1.Read Then
                Dim CmdInsert As New Data.SqlClient.SqlCommand
                With CmdInsert
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO MutasiAcc (JobNo,Site,AccNo,Tahun,Bulan,Debet,Kredit,UserEntry,TimeEntry) VALUES " & _
                                   "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", RsFind("Site"))
                    .Parameters.AddWithValue("@P3", RsFind("AccNo"))
                    .Parameters.AddWithValue("@P4", Mid(RsFind("GlPeriode"), 1, 4))
                    .Parameters.AddWithValue("@P5", Mid(RsFind("GlPeriode"), 5, 2))
                    .Parameters.AddWithValue("@P6", RsFind(2))
                    .Parameters.AddWithValue("@P7", RsFind(3))
                    .Parameters.AddWithValue("@P8", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P9", Now)
                    Try
                        .ExecuteNonQuery()
                    Catch
                        .Dispose()
                        LblErr.Text = Err.Description
                        ErrMsg.ShowOnPageLoad = True
                        Exit Sub
                    End Try
                    .Dispose()
                End With
            Else
                Dim CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE MutasiAcc SET Debet=@P1, Kredit=@P2, UserEntry=@P3, TimeEntry=@P4 WHERE " & _
                                   "JobNo=@P5 AND Site=@P6 AND AccNo=@P7 AND Tahun=@P8 AND Bulan=@P9"
                    .Parameters.AddWithValue("@P1", RsFind(2))
                    .Parameters.AddWithValue("@P2", RsFind(3))
                    .Parameters.AddWithValue("@P3", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P4", Now)
                    .Parameters.AddWithValue("@P5", DDLJob.Value)
                    .Parameters.AddWithValue("@P6", RsFind("Site"))
                    .Parameters.AddWithValue("@P7", RsFind("AccNo"))
                    .Parameters.AddWithValue("@P8", Mid(RsFind("GlPeriode"), 1, 4))
                    .Parameters.AddWithValue("@P9", Mid(RsFind("GlPeriode"), 5, 2))
                    Try
                        .ExecuteNonQuery()
                    Catch
                        .Dispose()
                        LblErr.Text = Err.Description
                        ErrMsg.ShowOnPageLoad = True
                        Exit Sub
                    End Try
                    .Dispose()
                End With

            End If
            RsFind1.Close()
            CmdFind1.Dispose()
        End While
        RsFind.Close()
        CmdFind.Dispose()

        LblErr.Text = "Proses tutup hari selesai."
        ErrMsg.ShowOnPageLoad = True
        Exit Sub
    End Sub
End Class