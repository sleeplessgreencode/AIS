Public Class FrmEntryHRD
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            TabPage.ActiveTabIndex = 0
            Call BindGrid()
        End If
        txtNama.Focus()
    End Sub

    Private Sub BindGrid()
        Call isiBank()
        DDLBank.SelectedValue = ""

        If Session("Project") <> "NEW" Then

            Dim CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM mKaryawan WHERE nip='" & Session("HRD") & "'"
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdGrid.ExecuteReader
            If RsFind.Read Then
                txtNIP.Text = Session("HRD")
                txtNama.Text = RsFind("nmKaryawan")
                If Not IsDBNull(RsFind("ktp")) Then txtKTP.Text = RsFind("ktp")
                If Not IsDBNull(RsFind("alamat")) Then txtAlamat.Text = RsFind("alamat")
                If Not IsDBNull(RsFind("kelurahan")) Then txtKelurahan.Text = RsFind("kelurahan")
                If Not IsDBNull(RsFind("kecamatan")) Then txtKecamatan.Text = RsFind("kecamatan")
                If Not IsDBNull(RsFind("kota")) Then txtKota.Text = RsFind("kota")
                If Not IsDBNull(RsFind("kodepos")) Then txtKodePos.Text = RsFind("kodepos")
                If Not IsDBNull(RsFind("kelamin")) Then DDLKelamin.SelectedValue = RsFind("kelamin")
                If Not IsDBNull(RsFind("agama")) Then DDLAgama.SelectedValue = RsFind("agama")
                If Not IsDBNull(RsFind("tmpLahir")) Then txtTmpLahir.Text = RsFind("tmpLahir")
                If Not IsDBNull(RsFind("tglLahir")) AndAlso RsFind("tglLahir") <> "1900-01-01" Then tglLahir.Text = RsFind("tglLahir")
                If Not IsDBNull(RsFind("stsNikah")) Then DDLStatus.SelectedValue = Left(RsFind("stsNikah"), 1)
                If Not IsDBNull(RsFind("stsNikah")) AndAlso Len(RsFind("stsNikah")) > 1 Then jmlAnak.Text = Right(RsFind("stsNikah"), 1)
                If Not IsDBNull(RsFind("wn")) Then txtWN.Text = RsFind("wn")
                If Not IsDBNull(RsFind("suku")) Then txtSuku.Text = RsFind("suku")
                If Not IsDBNull(RsFind("hp")) Then txtHP.Text = RsFind("hp")
                If Not IsDBNull(RsFind("telepon")) Then txtTelp.Text = RsFind("telepon")
                If Not IsDBNull(RsFind("pendidikan")) Then txtPendidikan.Text = RsFind("pendidikan")
                If Not IsDBNull(RsFind("jurusan")) Then txtJurusan.Text = RsFind("jurusan")
                If Not IsDBNull(RsFind("email")) Then txtEmail.Text = RsFind("email")
                If Not IsDBNull(RsFind("kk")) Then txtKK.Text = RsFind("kk")
                If Not IsDBNull(RsFind("tglMasuk")) AndAlso RsFind("tglMasuk") <> "1900-01-01" Then tglMasuk.Text = RsFind("tglMasuk")
                If Not IsDBNull(RsFind("tglKeluar")) AndAlso RsFind("tglKeluar") <> "1900-01-01" Then tglKeluar.Text = RsFind("tglKeluar")
                If Not IsDBNull(RsFind("kontrakAwal")) AndAlso RsFind("kontrakAwal") <> "1900-01-01" Then tglAwalKontrak.Text = RsFind("kontrakAwal")
                If Not IsDBNull(RsFind("kontrakAkhir")) AndAlso RsFind("kontrakAkhir") <> "1900-01-01" Then tglAkhirKontrak.Text = RsFind("kontrakAkhir")
                If Not IsDBNull(RsFind("klasifikasi")) Then DDLKlasifikasi.SelectedValue = RsFind("klasifikasi")
                If Not IsDBNull(RsFind("lokasi")) Then txtLokasi.Text = RsFind("lokasi")
                If Not IsDBNull(RsFind("stsKaryawan")) Then DDLStsKerja.SelectedValue = RsFind("stsKaryawan")
                If Not IsDBNull(RsFind("ptkp")) Then DDLStsPajak.SelectedValue = RsFind("ptkp")
                If Not IsDBNull(RsFind("npwp")) Then txtNPWP.Text = RsFind("npwp")
                If Not IsDBNull(RsFind("bpjs")) Then txtBPJS.Text = RsFind("bpjs")
                If Not IsDBNull(RsFind("noKPA")) Then txtNoKPA.Text = RsFind("noKPA")
                If Not IsDBNull(RsFind("tglKPA")) AndAlso RsFind("tglKPA") <> "1900-01-01" Then tglKPA.Text = RsFind("tglKPA")
                If Not IsDBNull(RsFind("jkk")) Then txtJKK.Text = RsFind("jkk")
                If Not IsDBNull(RsFind("jkm")) Then txtJKM.Text = RsFind("jkm")
                If Not IsDBNull(RsFind("jht")) Then txtJHT.Text = RsFind("jht")
                If Not IsDBNull(RsFind("jp")) Then txtJP.Text = RsFind("jp")
                If Not IsDBNull(RsFind("stsPerjanjian")) Then DDLPKWTT.SelectedValue = RsFind("stsPerjanjian")
                If Not IsDBNull(RsFind("terdaftar")) Then DDLTerdaftar.SelectedValue = RsFind("terdaftar")
                If Not IsDBNull(RsFind("bank")) Then DDLBank.SelectedValue = RsFind("bank")
                If Not IsDBNull(RsFind("cabang")) Then txtCabang.Text = RsFind("cabang")
                If Not IsDBNull(RsFind("noRek")) Then txtRek.Text = RsFind("noRek")
                If Not IsDBNull(RsFind("atasNm")) Then txtAN.Text = RsFind("atasNm")
                If Not IsDBNull(RsFind("alasanKeluar")) Then txtKeluar.Text = RsFind("alasanKeluar")
            End If
            RsFind.Close()
            CmdGrid.Dispose()

        End If

    End Sub

    Protected Sub bCancel_Click(sender As Object, e As EventArgs) Handles bCancel.Click
        Session.Remove("HRD")
        Response.Redirect("FrmHRD.aspx")
        Exit Sub
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub bSave_Click(sender As Object, e As EventArgs) Handles bSave.Click
        If txtNama.Text = "" Then
            TabPage.ActiveTabIndex = 0
            lblErr.Text = "Nama karyawan belum di-isi."
            errMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If tglMasuk.Date > tglKeluar.Date And tglKeluar.Text <> "" Then
            TabPage.ActiveTabIndex = 1
            tglMasuk.Focus()
            lblErr.Text = "Tanggal masuk & keluar tidak sesuai."
            errMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If Session("HRD") = "NEW" Then
            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO mKaryawan (nip,nmKaryawan,ktp,alamat,kelurahan,kecamatan,kota,kodepos,kelamin,agama," & _
                               "tmpLahir,tglLahir,stsNikah,wn,suku,hp,telepon,pendidikan,jurusan,email,kk,tglMasuk," & _
                               "kontrakAwal,kontrakAkhir,klasifikasi,lokasi,stsKaryawan,ptkp,npwp,bpjs,noKPA,tglKPA,jkk,jkm,jht,jp," & _
                               "stsPerjanjian,terdaftar,bank,cabang,noRek,atasNm,userEntry,timeOfEntry) VALUES " & _
                                "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21," & _
                                "@P22,@P23,@P24,@P25,@P26,@P27,@P28,@P29,@P30,@P31,@P32,@P33,@P34,@P35,@P36,@P37,@P38,@P39,@P40,@P41," & _
                                "@P42,@P43,@P44)"
                .Parameters.AddWithValue("@P1", assignNIP())
                .Parameters.AddWithValue("@P2", txtNama.Text)
                .Parameters.AddWithValue("@P3", txtKTP.Text)
                .Parameters.AddWithValue("@P4", txtAlamat.Text)
                .Parameters.AddWithValue("@P5", txtKelurahan.Text)
                .Parameters.AddWithValue("@P6", txtKecamatan.Text)
                .Parameters.AddWithValue("@P7", txtKota.Text)
                .Parameters.AddWithValue("@P8", txtKodePos.Text)
                .Parameters.AddWithValue("@P9", DDLKelamin.SelectedValue)
                .Parameters.AddWithValue("@P10", DDLAgama.SelectedValue)
                .Parameters.AddWithValue("@P11", txtTmpLahir.Text)
                .Parameters.AddWithValue("@P12", tglLahir.Text)
                .Parameters.AddWithValue("@P13", DDLStatus.SelectedValue & jmlAnak.Text)
                .Parameters.AddWithValue("@P14", txtWN.Text)
                .Parameters.AddWithValue("@P15", txtSuku.Text)
                .Parameters.AddWithValue("@P16", txtHP.Text)
                .Parameters.AddWithValue("@P17", txtTelp.Text)
                .Parameters.AddWithValue("@P18", txtPendidikan.Text)
                .Parameters.AddWithValue("@P19", txtJurusan.Text)
                .Parameters.AddWithValue("@P20", txtEmail.Text)
                .Parameters.AddWithValue("@P21", txtKK.Text)
                .Parameters.AddWithValue("@P22", tglMasuk.Text)
                .Parameters.AddWithValue("@P23", tglAwalKontrak.Text)
                .Parameters.AddWithValue("@P24", tglAkhirKontrak.Text)
                .Parameters.AddWithValue("@P25", DDLKlasifikasi.SelectedValue)
                .Parameters.AddWithValue("@P26", txtLokasi.Text)
                .Parameters.AddWithValue("@P27", DDLStsKerja.SelectedValue)
                .Parameters.AddWithValue("@P28", DDLStsPajak.SelectedValue)
                .Parameters.AddWithValue("@P29", txtNPWP.Text)
                .Parameters.AddWithValue("@P30", txtBPJS.Text)
                .Parameters.AddWithValue("@P31", txtNoKPA.Text)
                .Parameters.AddWithValue("@P32", tglKPA.Text)
                .Parameters.AddWithValue("@P33", txtJKK.Text)
                .Parameters.AddWithValue("@P34", txtJKM.Text)
                .Parameters.AddWithValue("@P35", txtJHT.Text)
                .Parameters.AddWithValue("@P36", txtJP.Text)
                .Parameters.AddWithValue("@P37", DDLPKWTT.SelectedValue)
                .Parameters.AddWithValue("@P38", DDLTerdaftar.SelectedValue)
                .Parameters.AddWithValue("@P39", DDLBank.SelectedValue)
                .Parameters.AddWithValue("@P40", txtCabang.Text)
                .Parameters.AddWithValue("@P41", txtRek.Text)
                .Parameters.AddWithValue("@P42", txtAN.Text)
                .Parameters.AddWithValue("@P43", Session("User"))
                .Parameters.AddWithValue("@P44", Now)
                Try
                    .ExecuteNonQuery()
                Catch
                    lblErr.Text = Err.Description
                    errMsg.ShowOnPageLoad = True
                    Exit Sub
                End Try
                .Dispose()
            End With
        Else
            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE mKaryawan SET nmKaryawan=@P1,ktp=@P2,alamat=@P3,kelurahan=@P4,kecamatan=@P5,kota=@P6," & _
                               "kodepos=@P7,kelamin=@P8,agama=@P9,tmpLahir=@P10,tglLahir=@P11,stsNikah=@P12,wn=@P13,suku=@P14," & _
                               "hp=@P15,telepon=@P16,pendidikan=@P17,jurusan=@P18,email=@P19,kk=@P20,tglMasuk=@P21," & _
                               "kontrakAwal=@P22,kontrakAkhir=@P23,klasifikasi=@P24,lokasi=@P25,stsKaryawan=@P26,ptkp=@P27," & _
                               "npwp=@P28,bpjs=@P29,noKPA=@P30,tglKPA=@P31,jkk=@P32,jkm=@P33,jht=@P34,jp=@P35,stsPerjanjian=@P36," & _
                               "terdaftar=@P37,bank=@P38,cabang=@P39,noRek=@P40,atasNm=@P41,userEntry=@P42,timeOfEntry = GETDATE() " & _
                               "WHERE nip=@P43"
                .Parameters.AddWithValue("@P1", txtNama.Text)
                .Parameters.AddWithValue("@P2", txtKTP.Text)
                .Parameters.AddWithValue("@P3", txtAlamat.Text)
                .Parameters.AddWithValue("@P4", txtKelurahan.Text)
                .Parameters.AddWithValue("@P5", txtKecamatan.Text)
                .Parameters.AddWithValue("@P6", txtKota.Text)
                .Parameters.AddWithValue("@P7", txtKodePos.Text)
                .Parameters.AddWithValue("@P8", DDLKelamin.SelectedValue)
                .Parameters.AddWithValue("@P9", DDLAgama.SelectedValue)
                .Parameters.AddWithValue("@P10", txtTmpLahir.Text)
                .Parameters.AddWithValue("@P11", tglLahir.Text)
                .Parameters.AddWithValue("@P12", DDLStatus.SelectedValue & jmlAnak.Text)
                .Parameters.AddWithValue("@P13", txtWN.Text)
                .Parameters.AddWithValue("@P14", txtSuku.Text)
                .Parameters.AddWithValue("@P15", txtHP.Text)
                .Parameters.AddWithValue("@P16", txtTelp.Text)
                .Parameters.AddWithValue("@P17", txtPendidikan.Text)
                .Parameters.AddWithValue("@P18", txtJurusan.Text)
                .Parameters.AddWithValue("@P19", txtEmail.Text)
                .Parameters.AddWithValue("@P20", txtKK.Text)
                .Parameters.AddWithValue("@P21", tglMasuk.Text)
                .Parameters.AddWithValue("@P22", tglAwalKontrak.Text)
                .Parameters.AddWithValue("@P23", tglAkhirKontrak.Text)
                .Parameters.AddWithValue("@P24", DDLKlasifikasi.SelectedValue)
                .Parameters.AddWithValue("@P25", txtLokasi.Text)
                .Parameters.AddWithValue("@P26", DDLStsKerja.SelectedValue)
                .Parameters.AddWithValue("@P27", DDLStsPajak.SelectedValue)
                .Parameters.AddWithValue("@P28", txtNPWP.Text)
                .Parameters.AddWithValue("@P29", txtBPJS.Text)
                .Parameters.AddWithValue("@P30", txtNoKPA.Text)
                .Parameters.AddWithValue("@P31", tglKPA.Text)
                .Parameters.AddWithValue("@P32", txtJKK.Text)
                .Parameters.AddWithValue("@P33", txtJKM.Text)
                .Parameters.AddWithValue("@P34", txtJHT.Text)
                .Parameters.AddWithValue("@P35", txtJP.Text)
                .Parameters.AddWithValue("@P36", DDLPKWTT.SelectedValue)
                .Parameters.AddWithValue("@P37", DDLTerdaftar.SelectedValue)
                .Parameters.AddWithValue("@P38", DDLBank.SelectedValue)
                .Parameters.AddWithValue("@P39", txtCabang.Text)
                .Parameters.AddWithValue("@P40", txtRek.Text)
                .Parameters.AddWithValue("@P41", txtAN.Text)
                .Parameters.AddWithValue("@P42", Session("User"))
                .Parameters.AddWithValue("@P43", Session("HRD"))
                Try
                    .ExecuteNonQuery()
                Catch
                    lblErr.Text = Err.Description
                    errMsg.ShowOnPageLoad = True
                    Exit Sub
                End Try
                .Dispose()
            End With
        End If

        Session.Remove("HRD")
        Response.Redirect("FrmHRD.aspx")
    End Sub

    Private Function assignNIP() As String
        Dim CmdID As New Data.SqlClient.SqlCommand
        With CmdID
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT nip FROM mKaryawan ORDER BY nip DESC"
        End With
        Dim RsID As Data.SqlClient.SqlDataReader = CmdID.ExecuteReader
        If RsID.Read Then
            If Left(RsID("nip"), 2) = Format(Now, "yy") Then
                Return Format(Now, "yy") & Format(CInt(Right(RsID("nip"), 4)) + 1, "0000")
            Else
                Return Format(Now, "yy") & "0001"
            End If
        Else
            Return Format(Now, "yy") & "0001"
        End If
        RsID.Close()
        CmdID.Dispose()
    End Function

    Private Sub isiBank()
        DDLBank.Items.Clear()
        Dim CmdIsi As New Data.SqlClient.SqlCommand
        With CmdIsi
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT bank FROM Bank"
        End With
        Dim RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
        While RsIsi.Read
            DDLBank.Items.Add(New ListItem(RsIsi(0), RsIsi(0)))
        End While
        RsIsi.Close()
        CmdIsi.Dispose()

    End Sub

End Class