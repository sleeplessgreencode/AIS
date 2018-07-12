Public Class FrmRencanaTermin
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "RencanaTermin") = False Then
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

        If Request.Params("DelRencana") = 1 Then
            Using CmdDelete As New Data.SqlClient.SqlCommand
                With CmdDelete
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "DELETE FROM RencanaTermin WHERE LedgerNo=@P1"
                    .Parameters.AddWithValue("@P1", Session("RencanaTermin"))
                    .ExecuteNonQuery()
                End With
            End Using

            Call BindGrid()
        End If

    End Sub

    Private Sub BindGrid()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM RencanaTermin WHERE JobNo=@P1 ORDER BY LedgerNo DESC"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With

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

    Private Sub GridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
        Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)

        If e.CommandName = "BtnUpdate" Then
            Session("RencanaTermin") = SelectRecord.Cells(1).Text

            LblAction.Text = "UPD"
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM RencanaTermin WHERE LedgerNo=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(1).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        DDLJenis.Value = RsFind("Jenis")
                        TxtTglRencana.Date = RsFind("TglRencana")
                        LblJenis.Text = RsFind("Jenis")
                        LblTglRencana.Text = RsFind("TglRencana")
                        TxtUraian.Text = RsFind("Uraian").ToString
                        TxtPersentase.Value = RsFind("Persentase").ToString
                        TxtA.Value = RsFind("Bruto")
                        TxtB.Value = RsFind("BrutoRealisasiLalu")
                        TxtC.Value = TxtA.Value - TxtB.Value
                        TxtD.Value = RsFind("PersentaseUM")
                        TxtUM.Value = TxtC.Value * (TxtD.Value / 100)
                        If DDLJenis.Value = "UM" Then
                            TxtE.Value = 0
                        ElseIf DDLJenis.Value = "Termin" Then
                            TxtE.Value = TxtC.Value * 0.05
                        End If
                        TxtF.Value = TxtUM.Value + TxtE.Value
                        TxtG.Value = TxtC.Value - TxtF.Value
                        TxtH.Value = TxtG.Value * 0.1
                        TxtI.Value = TxtG.Value + TxtH.Value
                        TxtJ.Value = TxtG.Value * 0.03
                        TxtK.Value = TxtI.Value - TxtJ.Value
                        TxtL.Value = TxtK.Value - TxtH.Value

                    End If
                End Using
            End Using

            PopEntry.ShowOnPageLoad = True
        Else
            Session("RencanaTermin") = SelectRecord.Cells(1).Text
            MsgBox1.confirm("Konfirmasi DELETE untuk rencana termin " & SelectRecord.Cells(3).Text & " ?", "DelRencana")
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

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If DDLJob.Value = "0" Then
            MsgBox1.alert("Belum pilih Job.")
            Exit Sub
        End If

        LblAction.Text = "NEW"
        DDLJenis.Value = String.Empty
        TxtTglRencana.Date = Today
        TxtUraian.Text = ""
        TxtPersentase.Text = "0"
        TxtA.Text = "0"
        TxtB.Text = "0"
        TxtC.Text = "0"
        TxtD.Text = "0"
        TxtUM.Text = "0"
        TxtE.Text = "0"
        TxtF.Text = "0"
        TxtG.Text = "0"
        TxtH.Text = "0"
        TxtI.Text = "0"
        TxtJ.Text = "0"
        TxtK.Text = "0"
        TxtL.Text = "0"

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM RencanaTermin WHERE JobNo=@P1 AND Jenis='UM'"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    TxtD.Value = RsFind("Persentase")
                End If
            End Using
        End Using

        PopEntry.ShowOnPageLoad = True
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If LblAction.Text = "NEW" Then
            Session("RencanaTermin") = 0
            If Validasi() = False Then
                MsgBox1.alert("Sudah ada rencana termin dengan jenis yg sama di bulan yg sama.")
                Exit Sub
            End If
            Using CmdInsert As New Data.SqlClient.SqlCommand
                With CmdInsert
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO RencanaTermin(JobNo,Jenis,TglRencana,Uraian,Persentase,Bruto,BrutoRealisasiLalu,PersentaseUM," & _
                                   "Netto,UserEntry,TimeEntry) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11)"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", DDLJenis.Value)
                    .Parameters.AddWithValue("@P3", TxtTglRencana.Date)
                    .Parameters.AddWithValue("@P4", TxtUraian.Text)
                    .Parameters.AddWithValue("@P5", TxtPersentase.Value)
                    .Parameters.AddWithValue("@P6", TxtA.Value)
                    .Parameters.AddWithValue("@P7", TxtB.Value)
                    .Parameters.AddWithValue("@P8", TxtD.Value)
                    .Parameters.AddWithValue("@P9", TxtL.Value)
                    .Parameters.AddWithValue("@P10", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P11", Now)
                    .ExecuteNonQuery()
                End With
            End Using
        Else
            If TxtTglRencana.Date <> LblTglRencana.Text Or
                DDLJenis.Value <> LblJenis.Text Then
                If Validasi() = False Then
                    MsgBox1.alert("Sudah ada rencana termin dengan jenis yg sama di bulan yg sama.")
                    Exit Sub
                End If
            End If
            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE RencanaTermin SET Jenis=@P1,TglRencana=@P2,Uraian=@P3,Persentase=@P4,Bruto=@P5,BrutoRealisasiLalu=@P6,PersentaseUM=@P7," + _
                                   "Netto=@P8,UserEntry=@P9,TimeEntry=@P10 WHERE LedgerNo=@P11"
                    .Parameters.AddWithValue("@P1", DDLJenis.Value)
                    .Parameters.AddWithValue("@P2", TxtTglRencana.Date)
                    .Parameters.AddWithValue("@P3", TxtUraian.Text)
                    .Parameters.AddWithValue("@P4", TxtPersentase.Value)
                    .Parameters.AddWithValue("@P5", TxtA.Value)
                    .Parameters.AddWithValue("@P6", TxtB.Value)
                    .Parameters.AddWithValue("@P7", TxtD.Value)
                    .Parameters.AddWithValue("@P8", TxtL.Value)
                    .Parameters.AddWithValue("@P9", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P10", Now)
                    .Parameters.AddWithValue("@P11", Session("RencanaTermin"))
                    .ExecuteNonQuery()
                End With
            End Using

        End If

        Call BindGrid()
        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As System.EventArgs) Handles BtnCancel.Click
        Session.Remove("RencanaTermin")
        PopEntry.ShowOnPageLoad = False
    End Sub

    Protected Sub DDLJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindGrid()
    End Sub

    Private Sub TxtPersentase_ValueChanged(sender As Object, e As System.EventArgs) Handles TxtPersentase.ValueChanged
        Call HitungNett()
    End Sub

    Private Sub TxtD_ValueChanged(sender As Object, e As System.EventArgs) Handles TxtD.ValueChanged
        Call HitungNett()
    End Sub

    Private Sub HitungNett()
        Dim NettKontrak As Decimal = 0

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Job WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    NettKontrak = RsFind("Bruto") / 1.1
                End If

            End Using
        End Using

        TxtA.Value = NettKontrak * (TxtPersentase.Value / 100)

        If DDLJenis.Value = "UM" Then
            TxtB.Value = 0

        ElseIf DDLJenis.Value = "Termin" Then
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT ISNULL(SUM(BrutoBOQ),0) FROM TerminInduk WHERE JobNo=@P1 AND TglCair<=@P2 AND Jenis<>'UM'"
                    .Parameters.AddWithValue("@P1", DDLJob.Value)
                    .Parameters.AddWithValue("@P2", TxtTglRencana.Date)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        TxtB.Value = RsFind(0)
                    End If
                End Using
            End Using

        End If

        TxtC.Value = TxtA.Value - TxtB.Value
        If DDLJenis.Value = "UM" Then
            TxtUM.Value = TxtC.Value * (TxtD.Value / 100)
            TxtE.Value = 0
        ElseIf DDLJenis.Value = "Termin" Then
            TxtUM.Value = TxtC.Value * (TxtD.Value / 100)
            TxtE.Value = TxtC.Value * 0.05
        End If

        TxtF.Value = TxtUM.Value + TxtE.Value
        TxtG.Value = TxtC.Value - TxtF.Value
        TxtH.Value = TxtG.Value * 0.1
        TxtI.Value = TxtG.Value + TxtH.Value
        TxtJ.Value = TxtG.Value * 0.03
        TxtK.Value = TxtI.Value - TxtJ.Value
        TxtL.Value = TxtK.Value - TxtH.Value

    End Sub

    Private Sub TxtTglRencana_ValueChanged(sender As Object, e As System.EventArgs) Handles TxtTglRencana.ValueChanged
        Call HitungNett()
    End Sub

    Private Function Validasi() As Boolean
        Dim PrdAwal As Date = DateSerial(Year(TxtTglRencana.Date), Month(TxtTglRencana.Date), 1)
        Dim PrdAkhir As Date = If(Month(TxtTglRencana.Date) = 12, DateSerial(Year(TxtTglRencana.Date) + 1, 1, 1).AddDays(-1), _
                                  DateSerial(Year(TxtTglRencana.Date), Month(TxtTglRencana.Date) + 1, 1).AddDays(-1))

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM RencanaTermin WHERE JobNo=@P1 AND Jenis=@P2 AND TglRencana>=@P3 AND TglRencana<=@P4 AND LedgerNo<>@P5"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", DDLJenis.Value)
                .Parameters.AddWithValue("@P3", PrdAwal)
                .Parameters.AddWithValue("@P4", PrdAkhir)
                .Parameters.AddWithValue("@P5", Session("RencanaTermin"))
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    Return False
                End If
            End Using
        End Using
        Return True

    End Function

End Class
