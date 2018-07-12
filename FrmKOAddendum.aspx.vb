Public Class FrmKOAddendum
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "KOAddendum") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            Call BindJob()
            If Session("Job") <> "" Then DDLJob.Value = Session("Job").ToString.Split("|")(0)
            Call BindKO()
            If Session("Job") <> "" Then DDLKo.Value = Session("Job").ToString.Split("|")(1)
            Session.Remove("Job")
        End If

        Call BindGrid()

    End Sub

    Private Sub BindKO()
        DDLKo.Items.Clear()
        DDLKo.Items.Add("Pilih salah satu", "0")

        Dim CmdIsi As New Data.SqlClient.SqlCommand
        With CmdIsi
            .Connection = Conn
            .CommandType = CommandType.Text
            '.CommandText = "SELECT NoKO FROM KoHdr WHERE JobNo=@P1 AND KategoriId=@P2 AND ClosedBy IS NULL"
            .CommandText = "SELECT NoKO FROM KoHdr WHERE JobNo=@P1 AND ClosedBy IS NULL ORDER BY NoKO"
            .Parameters.AddWithValue("@P1", DDLJob.Value)
            '.Parameters.AddWithValue("@P2", "SUBKON")
        End With
        Dim RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
        While RsIsi.Read
            DDLKo.Items.Add(RsIsi("NoKO"), RsIsi("NoKO"))
        End While
        RsIsi.Close()
        CmdIsi.Dispose()

        DDLKo.Value = "0"
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
            If AksesJob = "*" Or Array.IndexOf(AksesJob.Split(","), RsIsi("JobNo")) >= 0 Then
                DDLJob.Items.Add(RsIsi("JobNo") & " - " & RsIsi("JobNm"), RsIsi("JobNo"))
            End If
        End While
        RsIsi.Close()
        CmdIsi.Dispose()

        DDLJob.Value = "0"
    End Sub

    Private Sub BindGrid()

        Dim CmdGrid1 As New Data.SqlClient.SqlCommand
        With CmdGrid1
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT A.NoKO,A.TglKO,A.KategoriId,A.AddendumKe,B.VendorNm,A.SubTotal,A.ApprovedBy,A.TimeApproved FROM " & _
                           "KoHdr A LEFT OUTER JOIN Vendor B " & _
                           "ON A.VendorId = B.VendorId WHERE A.NoKO=@P1"
            .Parameters.AddWithValue("@P1", DDLKo.Value)
        End With

        'LblAdd.Text = "0"

        'Dim RsFind As Data.SqlClient.SqlDataReader = CmdGrid1.ExecuteReader
        'If RsFind.Read Then
        '    LblAdd.Text = RsFind("AddendumKe")
        'End If
        'RsFind.Close()

        Dim DaGrid1 As New Data.SqlClient.SqlDataAdapter
        DaGrid1.SelectCommand = CmdGrid1
        Dim DtGrid1 As New Data.DataTable
        DaGrid1.Fill(DtGrid1)
        With GridData1
            .DataSource = DtGrid1
            .DataBind()
        End With
        DtGrid1.Dispose()
        DaGrid1.Dispose()
        CmdGrid1.Dispose()

        Dim CmdGrid As New Data.SqlClient.SqlCommand
        With CmdGrid
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT A.NoKO,A.TglKO,A.KategoriId,A.AddendumKe,B.VendorNm,A.SubTotal,A.ApprovedBy,A.TimeApproved FROM " & _
                           "KoHdrH A LEFT OUTER JOIN Vendor B " & _
                           "ON A.VendorId = B.VendorId WHERE A.NoKO=@P1"
            .Parameters.AddWithValue("@P1", DDLKo.Value)
        End With
        Dim DaGrid As New Data.SqlClient.SqlDataAdapter
        DaGrid.SelectCommand = CmdGrid
        Dim DtGrid As New Data.DataTable
        DaGrid.Fill(DtGrid)
        With GridData
            .DataSource = DtGrid
            .DataBind()
        End With
        DtGrid.Dispose()
        DaGrid.Dispose()
        CmdGrid.Dispose()

    End Sub

    Private Sub GridData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridData.PageIndexChanging
        GridData.PageIndex = e.NewPageIndex
        GridData.DataBind()
        Call BindGrid()
    End Sub

    Private Sub GridData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)

            Dim TglKO As Date = Date.Parse(SelectRecord.Cells(1).Text)

            Session("KO") = "SEE_KOADDENDUMH|" & DDLJob.Text & "|" & SelectRecord.Cells(0).Text & "|" & TglKO
            
            If SelectRecord.Cells(3).Text = "KONTRAK" Then
                Response.Redirect("FrmEntryKO.aspx")
            Else
                Session("KO") = Session("KO") & "|"
                Response.Redirect("FrmEntryPO.aspx")
            End If

            'Response.Redirect("FrmEntryKO.aspx")
            Exit Sub
        End If
    End Sub

    Private Sub GridData1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData1.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridData1.Rows(e.CommandArgument)

            Session("KO") = "SEE_KOADDENDUM|" & DDLJob.Text & "|" & SelectRecord.Cells(0).Text & "|"

            If SelectRecord.Cells(3).Text = "KONTRAK" Then
                Response.Redirect("FrmEntryKO.aspx")
            Else
                Session("KO") = Session("KO") & "|"
                Response.Redirect("FrmEntryPO.aspx")
            End If

            'Response.Redirect("FrmEntryKO.aspx")
            Exit Sub
        End If
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If DDLJob.Value = "0" Then
            LblErr.Text = "Belum pilih Job."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If DDLKo.Value = "0" Then
            LblErr.Text = "Belum pilih No. KO."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        LblAdd.Text = "0"

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT NoKO,TglKO,ApprovedBy,AddendumKe FROM KoHdr WHERE NoKO=@P1"
            .Parameters.AddWithValue("@P1", DDLKo.Value)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            If IsDBNull(RsFind("ApprovedBy")) Then
                LblErr.Text = "No " & RsFind("NoKO") & " belum di-Approved."
                RsFind.Close()
                CmdFind.Dispose()
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            LblAdd.Text = RsFind("AddendumKe") & "|" & RsFind("TglKO")
        End If
        RsFind.Close()
        CmdFind.Dispose()

        Dim CmdFind1 As New Data.SqlClient.SqlCommand
        With CmdFind1
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM KoDtl WHERE NoKO=@P1"
            .Parameters.AddWithValue("@P1", DDLKo.Value)
        End With
        Dim RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
        While RsFind1.Read
            Dim CmdInsert1 As New Data.SqlClient.SqlCommand
            With CmdInsert1
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO KoDtlH (NoKO,TglKO,NoUrut,KdRAP,Uraian,Vol,Uom,HrgSatuan,UserEntry,TimeEntry) VALUES " & _
                               "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10)"
                .Parameters.AddWithValue("@P1", RsFind1("NoKO"))
                .Parameters.AddWithValue("@P2", LblAdd.Text.Split("|")(1))
                .Parameters.AddWithValue("@P3", RsFind1("NoUrut"))
                .Parameters.AddWithValue("@P4", RsFind1("KdRAP"))
                .Parameters.AddWithValue("@P5", RsFind1("Uraian"))
                .Parameters.AddWithValue("@P6", RsFind1("Vol"))
                .Parameters.AddWithValue("@P7", RsFind1("Uom"))
                .Parameters.AddWithValue("@P8", RsFind1("HrgSatuan"))
                .Parameters.AddWithValue("@P9", RsFind1("UserEntry"))
                .Parameters.AddWithValue("@P10", RsFind1("TimeEntry"))
                .ExecuteNonQuery()
                .Dispose()
            End With
        End While
        RsFind1.Close()
        CmdFind1.Dispose()

        Dim CmdInsert As New Data.SqlClient.SqlCommand
        With CmdInsert
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO KoHdrH SELECT * FROM KoHdr WHERE NoKO=@P1"
            .Parameters.AddWithValue("@P1", DDLKo.Value)
            .ExecuteNonQuery()
            .Dispose()
        End With

        Dim CmdEdit As New Data.SqlClient.SqlCommand
        With CmdEdit
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "UPDATE KoHdr SET " & _
                           "TglKO=@P1,ApprovedBy=@P2,TimeApproved=@P3,AddendumKe=@P4,UserEntry=@P5,TimeEntry=@P6 WHERE NoKO=@P7"
            .Parameters.AddWithValue("@P1", Today)
            .Parameters.AddWithValue("@P2", DBNull.Value)
            .Parameters.AddWithValue("@P3", DBNull.Value)
            .Parameters.AddWithValue("@P4", CInt(LblAdd.Text.Split("|")(0)) + 1)
            .Parameters.AddWithValue("@P5", Session("User").ToString.Split("|")(0))
            .Parameters.AddWithValue("@P6", Now)
            .Parameters.AddWithValue("@P7", DDLKo.Value)
            .ExecuteNonQuery()
            .Dispose()
        End With

        Call BindGrid()
    End Sub

    Protected Sub DDLJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLJob.SelectedIndexChanged
        Call BindKO()
    End Sub

    Private Function CheckNoKO() As Boolean
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT NoKO,TglKO FROM KoHdr WHERE NoKO=@P1"
            .Parameters.AddWithValue("@P1", DDLKo.Value)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            Dim CmdFind1 As New Data.SqlClient.SqlCommand
            With CmdFind1
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT NoKO,TglKO FROM KoHdrH WHERE NoKO=@P1 AND TglKO=@P2"
                .Parameters.AddWithValue("@P1", RsFind("NoKO"))
                .Parameters.AddWithValue("@P2", RsFind("TglKO"))
            End With
            Dim RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
            If RsFind1.Read Then
                LblErr.Text = "Duplicate data found." & "<br />" & _
                              "No " & RsFind1("NoKO") & "<br />" & "Tgl Kontrak/Addendum: " & Format(RsFind1("TglKO"), "dd-MMM-yyyy")
                RsFind.Close()
                CmdFind.Dispose()
                RsFind1.Close()
                CmdFind1.Dispose()
                ErrMsg.ShowOnPageLoad = True
                Return False
            End If
        End If
        RsFind.Close()
        CmdFind.Dispose()

        Return True

    End Function
End Class