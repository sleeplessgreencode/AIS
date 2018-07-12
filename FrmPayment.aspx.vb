Public Class FrmPayment
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "PayPD") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            TabPage.ActiveTabIndex = 0
            Grid.DataBind()
            Grid1.DataBind()
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub Grid_CustomButtonCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs) Handles Grid.CustomButtonCallback
        Dim key = Grid.GetRowValues(e.VisibleIndex, Grid.KeyFieldName)

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT ApprovedByDK, RejectBy FROM PdHdr WHERE NoPD=@P1"
                .Parameters.AddWithValue("@P1", key)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    If String.IsNullOrEmpty(RsFind("ApprovedByDK").ToString) = True Then
                        msgBox1.alert("PD belum diapproved oleh Direksi Keuangan.")
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(RsFind("RejectBy").ToString) = False Then
                        msgBox1.alert("PD sudah direject.")
                        Exit Sub
                    End If
                Else
                    msgBox1.alert("PD Not Found.")
                    Exit Sub
                End If
            End Using
        End Using

        Session("Payment") = "UPD|" + key + "|FrmPayment.aspx"
        Response.Redirect("FrmEntryPayment.aspx")

    End Sub

    Private Sub Grid1_CustomButtonCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs) Handles Grid1.CustomButtonCallback
        Dim key = Grid1.GetRowValues(e.VisibleIndex, Grid1.KeyFieldName)

        Session("Payment") = "UPD|" + key.ToString + "|FrmPayment.aspx"
        Response.Redirect("FrmEntryPayment.aspx")

    End Sub

    Private Sub Grid_DataBinding(sender As Object, e As System.EventArgs) Handles Grid.DataBinding
        Dim CSV As String = AssignJobCSV()

        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "WITH TmpStr AS ( " & _
                                "SELECT A.JobNo, B.JobNm, A.NoPD, A.TglPD, A.Alokasi, A.TipeForm, A.NoKO, A.Deskripsi, A.TotalPD, A.ApprovedByDK, A.TimeApprovedDK, " & _
                                "(SELECT ISNULL(COUNT(LedgerNo),0) FROM BLE WHERE (NoPD=A.NoPD OR NoPD=A.NoRef)) AS CountBLE " & _
                                "FROM PdHdr A, Job B " & _
                                "WHERE A.JobNo=B.JobNo AND A.ApprovedByDK IS NOT NULL AND A.RejectBy IS NULL AND A.JobNo IN (" & CSV & ") ) " & _
                                "SELECT * FROM TmpStr WHERE TmpStr.CountBLE = 0 ORDER BY TimeApprovedDK DESC, TotalPD DESC"
            End With
            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DsGrid As New Data.DataSet
                    DaGrid.Fill(DsGrid)
                    With Grid
                        .DataSource = DsGrid
                    End With
                End Using
            End Using
        End Using
    End Sub

    Private Sub Grid1_DataBinding(sender As Object, e As System.EventArgs) Handles Grid1.DataBinding
        Dim CSV As String = AssignJobCSV()

        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, B.JobNm FROM BLE A JOIN Job B ON A.JobNo=B.JobNo WHERE A.JobNo IN (" & CSV & ") ORDER BY A.TimeEntry DESC"
            End With
            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DsGrid As New Data.DataSet
                    DaGrid.Fill(DsGrid)
                    With Grid1
                        .DataSource = DsGrid
                    End With
                End Using
            End Using
        End Using
    End Sub

    Private Function AssignJobCSV() As String
        Dim AksesJob As String = String.Empty
        Dim CSV As String = String.Empty

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

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT JobNo FROM Job"
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsFind("JobNo").ToString) > -1 Then
                        CSV = If(CSV = "", "'" + RsFind("JobNo") + "'", CSV + "," + "'" + RsFind("JobNo") + "'")
                    End If
                End While
            End Using
        End Using

        Return CSV

    End Function

End Class