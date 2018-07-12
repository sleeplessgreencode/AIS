Public Class FrmBukuBesar
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "BukuBesar") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Call BindJob()
            Call BindPeriode()
            Call BindAccount()
            DDLBulan.Value = Today.Month.ToString
            TxtTahun.Value = Today.Year
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
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                AksesJob = RsFind("AksesJob")
            End If
        End Using

        DDLJob.Items.Clear()
        DDLJob.Items.Add(String.Empty, String.Empty)

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo, JobNm FROM Job"
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsFind("JobNo")) >= 0 Then
                    DDLJob.Items.Add(RsFind("JobNo") & " - " & RsFind("JobNm"), RsFind("JobNo"))
                End If
            End While
        End Using

    End Sub

    Private Sub BindPeriode()
        DDLBulan.Items.Clear()
        DDLBulan.Items.Add("Januari", "1")
        DDLBulan.Items.Add("Februari", "2")
        DDLBulan.Items.Add("Maret", "3")
        DDLBulan.Items.Add("April", "4")
        DDLBulan.Items.Add("Mei", "5")
        DDLBulan.Items.Add("Juni", "6")
        DDLBulan.Items.Add("Juli", "7")
        DDLBulan.Items.Add("Agustus", "8")
        DDLBulan.Items.Add("September", "9")
        DDLBulan.Items.Add("Oktober", "10")
        DDLBulan.Items.Add("November", "11")
        DDLBulan.Items.Add("Desember", "12")
    End Sub

    Private Sub BindAccount()
        DDLAccount.Items.Clear()
        DDLAccount.Items.Add(String.Empty, String.Empty)

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM COA"
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                DDLAccount.Items.Add(RsFind("AccNo") + " - " + RsFind("AccName"), RsFind("AccNo"))
            End While
        End Using

        DDLAccount.SelectedIndex = 0
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As System.EventArgs) Handles BtnPrint.Click
        If TxtTahun.Text = "0" Then
            msgBox1.alert("Tahun belum diisi.")
            TxtTahun.Focus()
            Exit Sub
        End If

        Dim PrdAwal As Date = DateSerial(TxtTahun.Text, DDLBulan.Value, 1)
        Dim PrdAkhir As Date = If(DDLBulan.Value = 12, DateSerial(TxtTahun.Text + 1, 1, 1).AddDays(-1), DateSerial(TxtTahun.Text, DDLBulan.Value + 1, 1).AddDays(-1))

        Session("Data") = DDLJob.Value + "|" + DDLSite.Value + "|" + PrdAwal.Date + "|" + PrdAkhir.Date + "|" + DDLAccount.Value
        Response.Redirect("FrmRptBB.aspx")

    End Sub

    Private Sub DDLJob_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDLJob.SelectedIndexChanged
        DDLSite.Items.Clear()

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT Site FROM GlReff WHERE JobNo=@P1"
                .Parameters.AddWithValue("@P1", DDLJob.Value)
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                DDLSite.Items.Add(RsFind("Site"), RsFind("Site"))
            End While
        End Using
    End Sub

End Class