Public Class FrmEntryRAP
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "RAP") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            LblAction.Text = Session("RAP").ToString.Split("|")(0)
            LblJobNo.Text = Trim(Session("RAP").ToString.Split("|")(2).Split("-")(0))
            LblJobNm.Text = Trim(Session("RAP").ToString.Split("|")(2).Split("-")(1))
            LblKdRAP.Text = Session("RAP").ToString.Split("|")(1)
            LblSource.Text = Session("RAP").ToString.Split("|")(3)
            LblVersi.Text = Session("RAP").ToString.Split("|")(4)
            LblAlokasi.Text = Session("RAP").ToString.Split("|")(5)

            Call BindAlokasi()
            Call BindHeader()
            Call BindData()
        End If

    End Sub

    Private Sub BindAlokasi()
        DDLAlokasi.Value = LblAlokasi.Text

        If DDLAlokasi.Value = "B" Then
            LblRAB.Visible = True
            LblRAB1.Visible = True
            TxtHrgRAB.Visible = True
        Else
            LblRAB.Visible = False
            LblRAB1.Visible = False
            TxtHrgRAB.Visible = False
        End If

    End Sub

    Private Sub BindHeader()
        DDLHeader.Items.Clear()
        DDLHeader.Items.Add("Top Header", "0")

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            If LblSource.Text = "RAP" Then
                .CommandText = "SELECT KdRAP,Uraian FROM RAP WHERE JobNo=@P1 AND Alokasi=@P3 AND Tipe='Header'"
            Else
                .CommandText = "SELECT KdRAP,Uraian FROM RAPH WHERE JobNo=@P1 AND Versi=@P2 AND Alokasi=@P3 AND Tipe='Header'"
            End If
            .Parameters.AddWithValue("@P1", LblJobNo.Text)
            .Parameters.AddWithValue("@P2", LblVersi.Text)
            .Parameters.AddWithValue("@P3", DDLAlokasi.Value)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        While RsFind.Read
            DDLHeader.Items.Add(RsFind("KdRAP") & " - " & RsFind("Uraian"), RsFind("KdRAP"))
        End While
        RsFind.Close()
        CmdFind.Dispose()

        DDLHeader.Value = "0"
    End Sub

    Private Sub BindData()
        LblJudul.Text = "RAP - " & LblJobNm.Text
        DDLTipe.Value = "0"
        DDLTipe.Focus()

        If LblAction.Text <> "NEW" And LblAction.Text <> "INS" Then
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    If LblSource.Text = "RAP" Then
                        .CommandText = "SELECT * FROM RAP WHERE JobNo=@P1 AND KdRAP=@P2 AND Alokasi=@P4"
                    Else
                        .CommandText = "SELECT * FROM RAPH WHERE JobNo=@P1 AND KdRAP=@P2 AND Versi=@P3 AND Alokasi=@P4"
                    End If
                    .Parameters.AddWithValue("@P1", LblJobNo.Text)
                    .Parameters.AddWithValue("@P2", LblKdRAP.Text)
                    .Parameters.AddWithValue("@P3", LblVersi.Text)
                    .Parameters.AddWithValue("@P4", LblAlokasi.Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        DDLTipe.Value = RsFind("Tipe")
                        'If RsFind("Tipe") = "Detail" Then DDLHeader.Value = RsFind("Header")
                        DDLHeader.Value = RsFind("Header")
                        DDLAlokasi.Value = RsFind("Alokasi")
                        TxtKdRAP.Text = RsFind("KdRAP")
                        TxtUraian.Text = RsFind("Uraian")
                        TxtUom.Text = RsFind("Uom").ToString
                        TxtVol.Text = RsFind("Vol")
                        TxtHrgSatuan.Text = Format(RsFind("HrgSatuan"), "N0")
                        TxtHrgRAB.Text = Format(RsFind("HrgRAB"), "N0")
                    End If
                End Using
            End Using

            DDLTipe.Enabled = False
            DDLAlokasi.Enabled = False
            DDLHeader.Enabled = False
            TxtKdRAP.Enabled = True
            'Disable KdRAP jika Header sudah ada detail
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT KdRAP FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2 AND Header=@P3"
                    .Parameters.AddWithValue("@P1", LblJobNo.Text)
                    .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                    .Parameters.AddWithValue("@P3", TxtKdRAP.Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        TxtKdRAP.Enabled = False
                    End If
                End Using
            End Using
            'Disable KdRAP jika sudah dipakai PD
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM PdHdr A JOIN PdDtl B ON A.NoPD=B.NoPD WHERE A.JobNo=@P1 AND A.Alokasi=@P2 AND A.RejectBy IS NULL AND B.KdRAP=@P3"
                    .Parameters.AddWithValue("@P1", LblJobNo.Text)
                    .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                    .Parameters.AddWithValue("@P3", TxtKdRAP.Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        TxtKdRAP.Enabled = False
                    End If
                End Using
            End Using
            'Disable KdRAP jika sudah dipakai KO
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM KoHdr A JOIN KoDtl B ON A.NoKO=B.NoKO WHERE A.JobNo=@P1 AND B.Alokasi=@P2 AND B.KdRAP=@P3"
                    .Parameters.AddWithValue("@P1", LblJobNo.Text)
                    .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                    .Parameters.AddWithValue("@P3", TxtKdRAP.Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        TxtKdRAP.Enabled = False
                    End If
                End Using
            End Using

            DDLTipe_SelectedIndexChanged(DDLTipe, New EventArgs())
            TxtUraian.Focus()
        End If

        If Left(LblAction.Text, 3) = "SEE" Then
            DisableControls(Form)
        End If
    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Session("Job") = LblJobNo.Text & "|" & DDLAlokasi.Value & "|" & LblSource.Text & "|" & LblVersi.Text
        Session.Remove("RAP")

        Response.Redirect("FrmRAP.aspx")
        Exit Sub

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As System.EventArgs) Handles BtnSave.Click
        If DDLTipe.Value = "0" Then
            LblErr.Text = "Belum pilih Tipe."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub        
        End If
        'If DDLAlokasi.Value = "0" Then
        '    LblErr.Text = "Belum pilih Alokasi."
        '    ErrMsg.ShowOnPageLoad = True
        '    Exit Sub
        'End If
        If DDLTipe.Value = "Detail" Then
            If DDLHeader.Value = "0" Then
                LblErr.Text = "Belum pilih Header."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            'If TxtVol.Text = "" Or CDec(TxtVol.Text) = 0 Then
            '    LblErr.Text = "Volume belum di-isi."
            '    ErrMsg.ShowOnPageLoad = True
            '    Exit Sub
            'End If
            'If CDec(TxtHrgSatuan.Text) = 0 Then
            '    LblErr.Text = "Harga satuan belum di-isi."
            '    ErrMsg.ShowOnPageLoad = True
            '    Exit Sub
            'End If
        End If

        If LblAction.Text = "NEW" Or LblAction.Text = "INS" Then

            If validasi(TxtKdRAP.Text) = False Then Exit Sub
            Dim hasil As Integer = 1 'Default value untuk NoUrut

            If DDLHeader.Value <> "0" And LblAction.Text = "NEW" Then
                If DDLTipe.Value = "Detail" Then
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT TOP 1 KdRAP FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2 AND Tipe='Detail' AND Header=@P3 ORDER BY NoUrut DESC"
                            .Parameters.AddWithValue("@P1", LblJobNo.Text)
                            .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                            .Parameters.AddWithValue("@P3", DDLHeader.Value)
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            If RsFind.Read Then
                                'Jika header punya detail maka ambil header terakhir
                                LblKdRAP.Text = RsFind("KdRAP")
                                LblAction.Text = "INS1"
                            Else
                                'Jika header tidak punya detail maka ambil header terakhir
                                Using CmdFindX As New Data.SqlClient.SqlCommand
                                    With CmdFindX
                                        .Connection = Conn
                                        .CommandType = CommandType.Text
                                        .CommandText = "SELECT KdRAP FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2 AND KdRAP=@P3"
                                        .Parameters.AddWithValue("@P1", LblJobNo.Text)
                                        .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                                        .Parameters.AddWithValue("@P3", DDLHeader.Value)
                                    End With
                                    Using RsFindX As Data.SqlClient.SqlDataReader = CmdFindX.ExecuteReader
                                        If RsFindX.Read Then
                                            LblKdRAP.Text = RsFindX("KdRAP")
                                            LblAction.Text = "INS1"
                                        End If
                                    End Using
                                End Using
                            End If
                        End Using
                    End Using
                Else
                    Using CmdFind As New Data.SqlClient.SqlCommand
                        With CmdFind
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT TOP 1 KdRAP FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2 AND Header=@P3 ORDER BY NoUrut DESC"
                            .Parameters.AddWithValue("@P1", LblJobNo.Text)
                            .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                            .Parameters.AddWithValue("@P3", DDLHeader.Value)
                        End With
                        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                            If RsFind.Read Then
                                'Jika header punya detail maka ambil header terakhir
                                LblKdRAP.Text = RsFind("KdRAP")
                                LblAction.Text = "INS1"
                            Else
                                'Jika header tidak punya detail maka ambil kdrap 
                                Using CmdFindX As New Data.SqlClient.SqlCommand
                                    With CmdFindX
                                        .Connection = Conn
                                        .CommandType = CommandType.Text
                                        .CommandText = "SELECT KdRAP FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2 AND KdRAP=@P3"
                                        .Parameters.AddWithValue("@P1", LblJobNo.Text)
                                        .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                                        .Parameters.AddWithValue("@P3", DDLHeader.Value)
                                    End With
                                    Using RsFindX As Data.SqlClient.SqlDataReader = CmdFindX.ExecuteReader
                                        If RsFindX.Read Then
                                            LblKdRAP.Text = RsFindX("KdRAP")
                                            LblAction.Text = "INS1"
                                        End If
                                    End Using
                                End Using
                            End If
                        End Using
                    End Using
                End If
            End If

            If LblAction.Text = "NEW" Then
                Dim CmdID As New Data.SqlClient.SqlCommand
                With CmdID
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT TOP 1 NoUrut FROM RAP WHERE JobNo=@P1 AND Alokasi=@P2 ORDER BY NoUrut DESC"
                    .Parameters.AddWithValue("@P1", LblJobNo.Text)
                    .Parameters.AddWithValue("@P2", DDLAlokasi.Value)
                End With
                Dim RsID As Data.SqlClient.SqlDataReader = CmdID.ExecuteReader
                If RsID.Read Then hasil = RsID(0) + 1
            Else
                Dim CmdID1 As New Data.SqlClient.SqlCommand
                With CmdID1
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM RAP WHERE JobNo=@P1 AND KdRAP=@P2 AND Alokasi=@P3"
                    .Parameters.AddWithValue("@P1", LblJobNo.Text)
                    .Parameters.AddWithValue("@P2", LblKdRAP.Text)
                    .Parameters.AddWithValue("@P3", DDLAlokasi.Value)
                End With
                Dim RsID1 As Data.SqlClient.SqlDataReader = CmdID1.ExecuteReader
                If RsID1.Read Then
                    Dim CmdID2 As New Data.SqlClient.SqlCommand
                    With CmdID2
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        If LblAction.Text = "INS" Then
                            .CommandText = "SELECT * FROM  RAP WHERE JobNo=@P1 AND Alokasi=@P2 AND NoUrut>=@P3 ORDER BY NoUrut"
                        ElseIf LblAction.Text = "INS1" Then
                            .CommandText = "SELECT * FROM  RAP WHERE JobNo=@P1 AND Alokasi=@P2 AND NoUrut>@P3 ORDER BY NoUrut"
                        End If
                        .Parameters.AddWithValue("@P1", RsID1("JobNo"))
                        .Parameters.AddWithValue("@P2", RsID1("Alokasi"))
                        .Parameters.AddWithValue("@P3", RsID1("NoUrut"))
                    End With
                    Dim RsID2 As Data.SqlClient.SqlDataReader = CmdID2.ExecuteReader
                    While RsID2.Read
                        Dim CmdUpdate As New Data.SqlClient.SqlCommand
                        With CmdUpdate
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "UPDATE RAP SET NoUrut=@P1 WHERE JobNo=@P2 AND Alokasi=@P3 AND KdRAP=@P4"
                            .Parameters.AddWithValue("@P1", RsID2("NoUrut") + 1)
                            .Parameters.AddWithValue("@P2", RsID2("JobNo"))
                            .Parameters.AddWithValue("@P3", RsID2("Alokasi"))
                            .Parameters.AddWithValue("@P4", RsID2("KdRAP"))
                            .ExecuteNonQuery()
                            .Dispose()
                        End With
                    End While
                    hasil = If(LblAction.Text = "INS1", RsID1("NoUrut") + 1, RsID1("NoUrut"))
                    RsID1.Close()
                    RsID2.Close()
                    CmdID1.Dispose()
                    CmdID2.Dispose()
                End If
            End If

            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO RAP (JobNo,KdRAP,Versi,NoUrut,Uraian,Tipe,Header,Uom,Vol,HrgSatuan,Alokasi," & _
                                    "UserEntry,TimeEntry,HrgRAB) VALUES " & _
                                    "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14)"
                .Parameters.AddWithValue("@P1", LblJobNo.Text)
                .Parameters.AddWithValue("@P2", Trim(TxtKdRAP.Text))
                .Parameters.AddWithValue("@P3", LblVersi.Text)
                .Parameters.AddWithValue("@P4", hasil)
                .Parameters.AddWithValue("@P5", TxtUraian.Text)
                .Parameters.AddWithValue("@P6", DDLTipe.Value)
                '.Parameters.AddWithValue("@P7", If(DDLTipe.Value = "Detail", DDLHeader.Value, ""))
                .Parameters.AddWithValue("@P7", DDLHeader.Value)
                .Parameters.AddWithValue("@P8", TxtUom.Text)
                .Parameters.AddWithValue("@P9", If(DDLTipe.Value = "Header", "1", TxtVol.Text))
                .Parameters.AddWithValue("@P10", TxtHrgSatuan.Text)
                .Parameters.AddWithValue("@P11", DDLAlokasi.Value)
                .Parameters.AddWithValue("@P12", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P13", Now)
                .Parameters.AddWithValue("@P14", TxtHrgRAB.Value)
                .ExecuteNonQuery()
                .Dispose()
            End With
        Else
            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                If DDLTipe.Value = "Detail" Then
                    .CommandText = "UPDATE RAP SET Uraian=@P1,Uom=@P2,Vol=@P3,HrgSatuan=@P4," & _
                                   "UserEntry=@P5,TimeEntry=@P6,HrgRAB=@P7,KdRAP=@P11 WHERE JobNo=@P8 AND KdRAP=@P9 AND Alokasi=@P10"
                Else
                    .CommandText = "UPDATE RAP SET Uraian=@P1," & _
                                   "UserEntry=@P5,TimeEntry=@P6,KdRAP=@P11 WHERE JobNo=@P8 AND KdRAP=@P9 AND Alokasi=@P10"
                End If
                .Parameters.AddWithValue("@P1", TxtUraian.Text)
                .Parameters.AddWithValue("@P2", TxtUom.Text)
                .Parameters.AddWithValue("@P3", TxtVol.Text)
                .Parameters.AddWithValue("@P4", TxtHrgSatuan.Text)
                .Parameters.AddWithValue("@P5", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P6", Now)
                .Parameters.AddWithValue("@P7", TxtHrgRAB.Value)
                .Parameters.AddWithValue("@P8", LblJobNo.Text)
                .Parameters.AddWithValue("@P9", LblKdRAP.Text)
                .Parameters.AddWithValue("@P10", DDLAlokasi.Value)
                .Parameters.AddWithValue("@P11", TxtKdRAP.Text)
                .ExecuteNonQuery()
                .Dispose()
            End With
        End If

        If DDLTipe.Value = "Detail" Then
            Call HitRecursive(DDLHeader.Value)
        End If

        BtnCancel_Click(BtnCancel, New EventArgs())
    End Sub

    Private Sub HitRecursive(ByVal KdRAP As String)

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT SUM(HrgSatuan*Vol), SUM(HrgRAB*Vol) FROM RAP WHERE JobNo=@P1 AND Header=@P2 AND Alokasi=@P3"
            .Parameters.AddWithValue("@P1", LblJobNo.Text)
            .Parameters.AddWithValue("@P2", KdRAP)
            .Parameters.AddWithValue("@P3", DDLAlokasi.Value)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        Dim Totalv As Decimal = 0
        Dim TotalRAB As Decimal = 0
        If RsFind.Read Then
            Totalv = RsFind(0)
            TotalRAB = RsFind(1)
        End If
        RsFind.Close()
        CmdFind.Dispose()

        Dim CmdEdit As New Data.SqlClient.SqlCommand
        With CmdEdit
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "UPDATE RAP SET HrgSatuan=@P1,HrgRAB=@P2 WHERE JobNo=@P3 AND KdRAP=@P4 AND Alokasi=@P5"
            .Parameters.AddWithValue("@P1", Totalv)
            .Parameters.AddWithValue("@P2", TotalRAB)
            .Parameters.AddWithValue("@P3", LblJobNo.Text)
            .Parameters.AddWithValue("@P4", KdRAP)
            .Parameters.AddWithValue("@P5", DDLAlokasi.Value)
            .ExecuteNonQuery()
            .Dispose()
        End With

        Dim Header As String = "0" '0 adalah top header, selain itu adalah sub header
        Dim CmdFind1 As New Data.SqlClient.SqlCommand
        With CmdFind1
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TOP 1 Header FROM RAP WHERE JobNo=@P1 AND KdRAP=@P2 AND Alokasi=@P3"
            .Parameters.AddWithValue("@P1", LblJobNo.Text)
            .Parameters.AddWithValue("@P2", KdRAP)
            .Parameters.AddWithValue("@P3", DDLAlokasi.Value)
        End With
        Dim RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
        If RsFind1.Read Then
            Header = RsFind1("Header")
        End If
        RsFind1.Close()
        CmdFind1.Dispose()

        If Header = "0" Then
            Exit Sub
        Else
            Call HitRecursive(Header)
        End If

    End Sub

    Protected Sub DDLTipe_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLTipe.SelectedIndexChanged
        If DDLTipe.Value = "Detail" Then
            TxtUom.Enabled = True
            TxtVol.Enabled = True
            TxtHrgSatuan.Enabled = True
            TxtHrgRAB.Enabled = True
            'DDLHeader.Visible = True
            'LblHeader.Visible = True
            'LblDot.Visible = True
        ElseIf DDLTipe.Value = "Header" Then
            'DDLHeader.Visible = False
            'LblHeader.Visible = False
            'LblDot.Visible = False
            TxtUom.Enabled = False
            TxtUom.Text = ""
            TxtVol.Enabled = False
            TxtVol.Value = 0
            TxtHrgSatuan.Enabled = False
            TxtHrgSatuan.Value = 0
            TxtHrgRAB.Enabled = False
            TxtHrgRAB.Value = 0
        End If
    End Sub

    Private Function validasi(ByVal KdRAPv As String) As Boolean
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM RAP WHERE JobNo=@P1 AND KdRAP=@P2 AND Alokasi=@P3"
            .Parameters.AddWithValue("@P1", LblJobNo.Text)
            .Parameters.AddWithValue("@P2", KdRAPv)
            .Parameters.AddWithValue("@P3", DDLAlokasi.Value)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            RsFind.Close()
            CmdFind.Dispose()
            LblErr.Text = "Kode RAP " & KdRAPv & " sudah ada."
            ErrMsg.ShowOnPageLoad = True
            Return False        
        End If

        RsFind.Close()
        CmdFind.Dispose()
        Return True

    End Function

    Private Sub DisableControls(control As System.Web.UI.Control)

        For Each c As System.Web.UI.Control In control.Controls
            If c.GetType.ToString = "System.Web.UI.WebControls.GridView" Then Continue For
            If c.GetType.ToString = "DevExpress.Web.ASPxPopupControl" Then Continue For

            ' Get the Enabled property by reflection.
            Dim type As Type = c.GetType
            Dim prop As Reflection.PropertyInfo = type.GetProperty("Enabled")

            ' Set it to False to disable the control.
            If Not prop Is Nothing Then
                prop.SetValue(c, False, Nothing)
            End If

            ' Recurse into child controls.
            If c.Controls.Count > 0 Then
                Me.DisableControls(c)
            End If
        Next

        BtnSave.Visible = False
        BtnCancel.Text = "OK"
        BtnCancel.Enabled = True

    End Sub

End Class