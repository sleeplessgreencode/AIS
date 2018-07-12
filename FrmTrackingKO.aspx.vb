Public Class FrmTrackingKO
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim DtGrid1 As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "TrackingKO") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Call BindJob()
            GridView.DataSource = Nothing
            GridView.DataBind()
        End If

        DtGrid1 = Session("DtGrid1")
        DDLKo.DataSource = DtGrid1
        DDLKo.DataBind()

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
        DDLJob.Items.Add(String.Empty, String.Empty)
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

        DDLJob.SelectedIndex = 0
        Call BindKO()

    End Sub

    Private Sub BindGrid()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM TraceKO WHERE NoKO=@P1"
                .Parameters.AddWithValue("@P1", If(String.IsNullOrEmpty(DDLKo.Text) = True, String.Empty, DDLKo.Value))
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

    Private Sub BtnAdd_Click(sender As Object, e As System.EventArgs) Handles BtnAdd.Click
        If DDLJob.Text = String.Empty Then
            msgBox1.alert("Belum pilih Job.")
            DDLJob.Focus()
            Exit Sub
        End If
        If DDLKo.Text = String.Empty Then
            msgBox1.alert("Belum pilih No KO.")
            DDLKo.Focus()
            Exit Sub
        End If

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM KoHdr WHERE NoKO=@P1"
                .Parameters.AddWithValue("@P1", DDLKo.Value)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    If String.IsNullOrEmpty(RsFind("ApprovedBy").ToString) = True Then
                        msgBox1.alert("No " & DDLKo.Text & " belum di approved.")
                        DDLKo.Focus()
                        Exit Sub
                    End If
                End If
            End Using
        End Using

        LblAction.Text = "NEW"
        TxtKeterangan.Text = ""
        TxtTanggal.Text = ""
        DDLStatus.Value = "0"
        TxtKeterangan.Focus()

        ModalEntry.ShowOnPageLoad = True
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As System.EventArgs) Handles BtnSave.Click
        If TxtKeterangan.Text = "" Then
            msgBox1.alert("Belum isi keterangan.")
            TxtKeterangan.Focus()
            Exit Sub
        End If

        If LblAction.Text = "NEW" Then
            Using CmdInsert As New Data.SqlClient.SqlCommand
                With CmdInsert
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO TraceKO(NoKO,Keterangan,Tanggal,Status,UserEntry,TimeEntry) VALUES " & _
                                   "(@P1,@P2,@P3,@P4,@P5,@P6)"
                    .Parameters.AddWithValue("@P1", DDLKo.Value)
                    .Parameters.AddWithValue("@P2", TxtKeterangan.Text)
                    .Parameters.AddWithValue("@P3", TxtTanggal.Date)
                    .Parameters.AddWithValue("@P4", DDLStatus.Value)
                    .Parameters.AddWithValue("@P5", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P6", Now)
                    .ExecuteNonQuery()
                End With
            End Using
        Else
            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE TraceKO SET Keterangan=@P1,Tanggal=@P2,Status=@P3,UserEntry=@P4,TimeEntry=@P5 WHERE NoKO=@P6 AND Trace#=@P7"
                    .Parameters.AddWithValue("@P1", TxtKeterangan.Text)
                    .Parameters.AddWithValue("@P2", TxtTanggal.Date)
                    .Parameters.AddWithValue("@P3", DDLStatus.Value)
                    .Parameters.AddWithValue("@P4", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P5", Now)
                    .Parameters.AddWithValue("@P6", DDLKo.Value)
                    .Parameters.AddWithValue("@P7", Session("TraceKO"))
                    .ExecuteNonQuery()
                End With
            End Using

        End If

        Call BindGrid()

        BtnCancel_Click(BtnCancel, New EventArgs())
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As System.EventArgs) Handles BtnCancel.Click
        Session.Remove("TraceKO")
        ModalEntry.ShowOnPageLoad = False
    End Sub

    Private Sub GridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)

            Session("TraceKO") = SelectRecord.Cells(1).Text

            LblAction.Text = "UPD"
            TxtKeterangan.Text = TryCast(SelectRecord.FindControl("LblKeterangan"), Label).Text.Replace("<br />", Environment.NewLine)
            TxtTanggal.Date = SelectRecord.Cells(3).Text
            DDLStatus.Text = TryCast(SelectRecord.FindControl("LblStatus"), Label).Text
            TxtKeterangan.Focus()

            ModalEntry.ShowOnPageLoad = True

        End If
    End Sub

    Private Sub DDLJob_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindKO()
    End Sub

    Private Sub DDLKo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLKo.SelectedIndexChanged
        TglKO.Text = ""
        TxtAlamatKirim.Text = ""
        TxtNamaKirim.Text = ""
        TxtTeleponKirim.Text = ""

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM KoHdr WHERE NoKO=@P1"
                .Parameters.AddWithValue("@P1", If(DDLKo.Text = String.Empty, String.Empty, DDLKo.Value))
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    TglKO.Date = RsFind("TglKO")
                    TxtAlamatKirim.Text = RsFind("AlamatKirim")
                    TxtNamaKirim.Text = RsFind("NamaKirim")
                    TxtTeleponKirim.Text = RsFind("TeleponKirim")
                End If
            End Using
        End Using

        Call BindGrid()
    End Sub

    Private Sub BindKO()
        DDLKo.Text = ""
        TglKO.Text = ""
        TxtAlamatKirim.Text = ""
        TxtNamaKirim.Text = ""
        TxtTeleponKirim.Text = ""
        DtGrid1.Clear()
        GridView.DataSource = Nothing
        GridView.DataBind()

        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.NoKO, FORMAT(A.TglKO,'dd-MMM-yyyy') AS TglKO, B.VendorNm, FORMAT((A.SubTotal - A.DiscAmount) + A.PPN,'N0') AS TotalKO " & _
                               "FROM KoHdr A JOIN Vendor B ON A.VendorId=B.VendorId WHERE " & _
                               "JobNo=@P1 AND KategoriId='PO' ORDER BY NoKO DESC"
                .Parameters.AddWithValue("@P1", If(DDLJob.Text = String.Empty, String.Empty, DDLJob.Value))
            End With
            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                DaGrid.Fill(DtGrid1)
                Session("DtGrid1") = DtGrid1
                DDLKo.DataSource = DtGrid1
                DDLKo.DataBind()
            End Using
        End Using

        'Using CmdFind As New Data.SqlClient.SqlCommand
        '    With CmdFind
        '        .Connection = Conn
        '        .CommandType = CommandType.Text
        '        .CommandText = "SELECT * FROM KoHdr WHERE JobNo=@P1 AND KategoriId='PO'"
        '        .Parameters.AddWithValue("@P1", If(DDLJob.Text = String.Empty, String.Empty, DDLJob.Value))
        '    End With
        '    Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        '        While RsFind.Read
        '            DDLKo.Items.Add(RsFind("NoKO"), RsFind("NoKO"))
        '        End While
        '    End Using
        'End Using
    End Sub

End Class