Public Class FrmRPPM
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "RPPM1") = False Then
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

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub BindGrid()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM RPPM WHERE JobNo=@P1 ORDER BY Tgl1 DESC"
                .Parameters.AddWithValue("@P1", If(DDLJob.Text = String.Empty, String.Empty, DDLJob.Value))
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
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridView.Rows(e.CommandArgument)

            Session("RPPM") = DDLJob.Value & "|" & SelectRecord.Cells(0).Text & "|" & SelectRecord.Cells(1).Text

            TxtAction.Text = "UPD"
            TxtTgl1.Date = SelectRecord.Cells(0).Text
            TxtTgl2.Date = SelectRecord.Cells(1).Text
            TxtInduk.Text = SelectRecord.Cells(2).Text
            TxtShare.Text = SelectRecord.Cells(3).Text
            PopEntry.ShowOnPageLoad = True

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
                .CommandText = "SELECT JobNo,JobNm FROM Job WHERE StatusJob=@P1"
                .Parameters.AddWithValue("@P1", "Pelaksanaan")
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

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Session.Remove("RPPM")

        TxtAction.Text = "NEW"
        TxtTgl1.Text = String.Empty
        TxtTgl2.Text = String.Empty
        TxtInduk.Text = "0.000"
        TxtShare.Text = "0.000"
        PopEntry.ShowOnPageLoad = True

    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If TxtAction.Text = "NEW" Then
            If CheckUnique() = False Then Exit Sub

            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO RPPM(JobNo,Tgl1,Tgl2,Induk,Share,UserEntry,TimeEntry) VALUES " & _
                               "(@P1,@P2,@P3,@P4,@P5,@P6,@P7)"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", TxtTgl1.Date)
                .Parameters.AddWithValue("@P3", TxtTgl2.Date)
                .Parameters.AddWithValue("@P4", TxtInduk.Value)
                .Parameters.AddWithValue("@P5", TxtShare.Value)
                .Parameters.AddWithValue("@P6", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P7", Now)
                .ExecuteNonQuery()
            End With
        Else
            If TxtTgl1.Date <> Session("RPPM").ToString.Split("|")(1) Or
                TxtTgl2.Date <> Session("RPPM").ToString.Split("|")(2) Then
                If CheckUnique() = False Then Exit Sub
            End If

            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE RPPM SET Tgl1=@P1,Tgl2=@P2,Induk=@P3,Share=@P4,UserEntry=@P5,TimeEntry=@P6 WHERE JobNo=@P7 AND Tgl1=@P8 AND Tgl2=@P9"
                .Parameters.AddWithValue("@P1", TxtTgl1.Date)
                .Parameters.AddWithValue("@P2", TxtTgl2.Date)
                .Parameters.AddWithValue("@P3", TxtInduk.Value)
                .Parameters.AddWithValue("@P4", TxtShare.Value)
                .Parameters.AddWithValue("@P5", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P6", Now)
                .Parameters.AddWithValue("@P7", Session("RPPM").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P8", Session("RPPM").ToString.Split("|")(1))
                .Parameters.AddWithValue("@P9", Session("RPPM").ToString.Split("|")(2))
                .ExecuteNonQuery()
            End With
        End If

        Call BindGrid()

        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As System.EventArgs) Handles BtnCancel.Click
        Session.Remove("RPPM")
        PopEntry.ShowOnPageLoad = False
    End Sub

    Private Sub DDLJob_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindGrid()
    End Sub

    Private Function CheckUnique() As Boolean
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM RPPM WHERE JobNo=@P1 AND Tgl1=@P2 AND Tgl2=@P3"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
                .Parameters.AddWithValue("@P2", TxtTgl1.Date)
                .Parameters.AddWithValue("@P3", TxtTgl2.Date)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    msgBox1.alert("Sudah ada RPPM untuk periode " & Format(RsFind("Tgl1"), "dd-MMM-yyyy") & "\ns.d. " & Format(RsFind("Tgl2"), "dd-MMM-yyyy"))
                    Return False
                End If
            End Using
        End Using

        Return True

    End Function

End Class