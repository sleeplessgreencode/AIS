Public Class FrmOverrideSaldo
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "OverrideSaldo") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If Request.Params("Confirm") = 1 Then
            Using CmdEdit As New Data.SqlClient.SqlCommand
                With CmdEdit
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE PdHdr SET OverrideSaldo='1',RemarkOverrideSaldo=@P1,UserEntry=@P2,TimeEntry=@P3,Saldo=@P4 WHERE NoPJ=@P5"
                    .Parameters.AddWithValue("@P1", TxtRemark.Text)
                    .Parameters.AddWithValue("@P2", Session("User").ToString.Split("|")(0))
                    .Parameters.AddWithValue("@P3", Now)
                    .Parameters.AddWithValue("@P4", TxtSaldo2.Text)
                    .Parameters.AddWithValue("@P5", TxtNoPJ.Text)
                    .ExecuteNonQuery()
                End With
            End Using
            MsgBox1.alert("Override Done.")
            Call Cls()

        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If Trim(TxtNoPJ.Text) = "" Or Trim(TxtRemark.Text) = "" Then
            MsgBox1.alert("No PJ/Remark belum diisi.")
            TxtNoPJ.Focus()
            Exit Sub
        End If

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

        Dim AksesAlokasi As String = ""
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT AksesAlokasi FROM Login WHERE UserID=@P1"
                .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    AksesAlokasi = RsFind("AksesAlokasi")
                End If
            End Using
        End Using

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM PdHdr WHERE NoPJ=@P1"
                .Parameters.AddWithValue("@P1", TxtNoPJ.Text)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    If String.IsNullOrEmpty(RsFind("RejectBy").ToString) = False Then
                        MsgBox1.alert("No PD " + TxtNoPJ.Text + " sudah dibatalkan oleh " + RsFind("RejectBy") + "\npada " + Format(RsFind("TimeReject"), "dd-MMM-yyyy HH:mm"))
                        TxtNoPJ.Focus()
                        Exit Sub
                    End If
                    If Array.IndexOf(AksesJob.Split(","), RsFind("JobNo")) < 0 Then
                        MsgBox1.alert("Anda tidak punya akses untuk Job No " + RsFind("JobNo") + ".")
                        TxtNoPJ.Focus()
                        Exit Sub
                    End If
                    If Array.IndexOf(AksesAlokasi.Split(","), RsFind("Alokasi")) < 0 Then
                        MsgBox1.alert("Anda tidak punya akses untuk Alokasi " + RsFind("Alokasi") + ".")
                        TxtNoPJ.Focus()
                        Exit Sub
                    End If
                    MsgBox1.confirm("Konfirmasi override saldo untuk " + TxtNoPJ.Text + " ?", "Confirm")
                End If

            End Using
        End Using

    End Sub


    Private Sub TxtNoPJ_ValueChanged(sender As Object, e As System.EventArgs) Handles TxtNoPJ.ValueChanged
        TxtSaldo1.Text = "0"
        TxtSaldo2.Text = "0"
        TxtRemark.Text = ""
        TxtNoPJ.Text = Trim(TxtNoPJ.Text)

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM PdHdr WHERE NoPJ=@P1"
                .Parameters.AddWithValue("@P1", TxtNoPJ.Text)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If Not RsFind.Read Then
                    MsgBox1.alert("No PJ " + TxtNoPJ.Text + " tidak ada.")
                    TxtNoPJ.Focus()
                    Exit Sub
                End If
                TxtSaldo1.Text = Format(RsFind("Saldo"), "N0")

            End Using
        End Using

    End Sub

    Private Sub Cls()
        TxtNoPJ.Text = ""
        TxtSaldo1.Text = "0"
        TxtSaldo2.Text = "0"
        TxtRemark.Text = ""
    End Sub

End Class