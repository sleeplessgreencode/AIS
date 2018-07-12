Imports System.IO
Public Class FrmEntryKaryawan
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection
    Dim TmpDtKeluarga As New DataTable()
    Dim TmpDtPendidikan As New DataTable()
    Dim TmpDtKetrampilan As New DataTable()
    Dim TmpDtRwytPekerjaan As New DataTable()
    Dim TmpDtRwytPekerjaanMinarta As New DataTable()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "KO") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        LblAction.Text = Session("Karyawan")
        Dim UpdateNIK As String = Session("NIK")

        If IsPostBack = False Then
            Call BindGridKeluarga()
            Call BindGridPendidikan()
            Call BindGridKetrampilan()
            Call BindGridRwytPekerjaan()
            Call BindGridRwytPekerjaanMinarta()
            Call BindData()

            If Session("Karyawan") = "UPD" Then
                PasFotoDefault.ImageUrl = Session("PasFoto")
            ElseIf Session("Karyawan") = "NEW" And PasFoto.HasFile Then
                Dim FileName As String = TxtNIK.Text & "PasFoto.jpg"
                Session("PasFoto") = Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileName)
            Else
                Session("PasFoto") = "/Images/PasFotoDefault.jpg"
            End If

            PasFotoDefault.ImageUrl = Session("PasFoto")
        End If
    End Sub
    Private Sub BindData()
        Dim UpdateNIK As String = Session("NIK")

        If LblAction.Text = "UPD" Then
            TxtNIK.Enabled = False
            Dim CmdDataHdr As New Data.SqlClient.SqlCommand
            With CmdDataHdr
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Karyawan where NIK=@P1"
                .Parameters.AddWithValue("@P1", UpdateNIK)
            End With
            Dim DataHdr As Data.SqlClient.SqlDataReader = CmdDataHdr.ExecuteReader
            If DataHdr.Read Then
                TxtNIK.Text = DataHdr("NIK")
                TxtNama.Text = DataHdr("Nama")
                RblJenisKelamin.Value = If(DataHdr("Kelamin") = "L", "L", "P")
                TxtTempatLahir.Text = DataHdr("TmpLahir").ToString
                TxtTanggalLahir.Text = DataHdr("TglLahir").ToString
                TxtWN.Text = DataHdr("WN")
                RblStsNikah.Value = DataHdr("StsNikah")
                RblAgama.Value = DataHdr("Agama")
                TxtDivisi.Text = DataHdr("Divisi")
                TxtSubdivisi.Text = DataHdr("Subdivisi")
                TxtJabatan.Text = DataHdr("Jabatan")
                TxtGolongan.Text = DataHdr("Golongan")
                TxtGrade.Text = DataHdr("Grade")
                TxtPrdAwal.Date = DataHdr("PrdAwal")
                TxtUraian.Text = DataHdr("UraianPekerjaan")
                TxtAlamat.Text = DataHdr("Alamat")
                TxtProvinsi.Text = DataHdr("Provinsi")
                TxtKota.Text = DataHdr("Kota")
                TxtAlamatSurat.Text = DataHdr("AlamatSurat")
                TxtEmail.Text = DataHdr("Email")
                TxtNoTelp.Text = DataHdr("NoTelp")
                TxtLokasiKerja.Text = DataHdr("LokasiKerja")
                Session("PasFoto") = DataHdr("Foto")
            End If
            CmdDataHdr.Dispose()
            DataHdr.Close()

            Dim CmdDataID As New Data.SqlClient.SqlCommand
            With CmdDataID
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM EmpID where NIK=@P1"
                .Parameters.AddWithValue("@P1", UpdateNIK)
            End With
            Dim DataID As Data.SqlClient.SqlDataReader = CmdDataID.ExecuteReader
            If DataID.Read Then
                If DataID("JenisID") = "KTP" Then
                    TxtNoKTP.Text = DataID("NoID")
                    TxtPrdAwalKTP.Date = DataID("PrdAwal")
                    TxtPrdAkhirKTP.Date = DataID("PrdAkhir")
                    TxtDiterbitkanOlehKTP.Text = DataID("DiterbitkanOleh")
                ElseIf DataID("JenisID") = "Passport" Then
                    TxtNoPassport.Text = DataID("NoID")
                    TxtPrdAwalPassport.Date = DataID("PrdAwal")
                    TxtPrdAkhirPassport.Date = DataID("PrdAkhir")
                    TxtDiterbitkanOlehPassport.Text = DataID("DiterbitkanOleh")
                ElseIf DataID("JenisID") = "NPWP" Then
                    TxtNoNPWP.Text = DataID("NoID")
                    TxtPrdAwalNPWP.Date = DataID("PrdAwal")
                    TxtPrdAkhirNPWP.Date = DataID("PrdAkhir")
                    TxtDiterbitkanOlehNPWP.Text = DataID("DiterbitkanOleh")
                ElseIf DataID("JenisID") = "KK" Then
                    TxtNoKK.Text = DataID("NoID")
                    TxtPrdAwalKK.Date = DataID("PrdAwal")
                    TxtPrdAkhirKK.Date = DataID("PrdAkhir")
                    TxtDiterbitkanOlehKK.Text = DataID("DiterbitkanOleh")
                ElseIf DataID("JenisID") = "SIM A" Then
                    TxtNoSIMA.Text = DataID("NoID")
                    TxtPrdAwalSIMA.Date = DataID("PrdAwal")
                    TxtPrdAkhirSIMA.Date = DataID("PrdAkhir")
                    TxtDiterbitkanOlehSIMA.Text = DataID("DiterbitkanOleh")
                ElseIf DataID("JenisID") = "SIM B" Then
                    TxtNoSIMB.Text = DataID("NoID")
                    TxtPrdAwalSIMB.Date = DataID("PrdAwal")
                    TxtPrdAkhirSIMB.Date = DataID("PrdAkhir")
                    TxtDiterbitkanOlehSIMB.Text = DataID("DiterbitkanOleh")
                ElseIf DataID("JenisID") = "SIM C" Then
                    TxtNoSIMC.Text = DataID("NoID")
                    TxtPrdAwalSIMC.Date = DataID("PrdAwal")
                    TxtPrdAkhirSIMC.Date = DataID("PrdAkhir")
                    TxtDiterbitkanOlehSIMC.Text = DataID("DiterbitkanOleh")
                End If
            End If
            CmdDataID.Dispose()
            DataID.Close()
        End If
    End Sub
    Private Sub BindGridKeluarga()
        Dim UpdateNIK As String = Session("NIK")
        TmpDtKeluarga.Columns.AddRange(New DataColumn() {New DataColumn("NoUrutKeluarga", GetType(Integer)), _
                                                         New DataColumn("HubKeluarga", GetType(String)), _
                                                         New DataColumn("NamaKeluarga", GetType(String)), _
                                                         New DataColumn("JenisKelaminKeluarga", GetType(String)), _
                                                         New DataColumn("TglLahirKeluarga", GetType(Date)), _
                                                         New DataColumn("PekerjaanKeluarga", GetType(String)), _
                                                         New DataColumn("PerusahaanKeluarga", GetType(String))})

        If Session("Karyawan") = "UPD" Then
            Dim CmdDataKeluarga As New Data.SqlClient.SqlCommand
            With CmdDataKeluarga
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM EmpKeluarga WHERE NIK=@P1 ORDER BY NoUrutKeluarga"
                .Parameters.AddWithValue("@P1", UpdateNIK)
            End With
            Dim DataKeluarga As Data.SqlClient.SqlDataReader = CmdDataKeluarga.ExecuteReader
            While DataKeluarga.Read
                TmpDtKeluarga.Rows.Add(DataKeluarga("NoUrutKeluarga"), DataKeluarga("Hubungan"), DataKeluarga("Nama"), DataKeluarga("Kelamin"), DataKeluarga("TglLahir"), DataKeluarga("Pekerjaan"), DataKeluarga("Perusahaan"))
            End While
            DataKeluarga.Close()
            CmdDataKeluarga.Dispose()
        End If

        GridDataKeluarga.DataSource = TmpDtKeluarga
        Session("TmpDtKeluarga") = TmpDtKeluarga

        GridDataKeluarga.DataBind()
    End Sub
    Private Sub BindGridPendidikan()
        Dim UpdateNIK As String = Session("NIK")
        TmpDtPendidikan.Columns.AddRange(New DataColumn() {New DataColumn("NoUrutPendidikan", GetType(Integer)), _
                                                           New DataColumn("TgkPendidikan", GetType(String)), _
                                                           New DataColumn("PrdAwalPendidikan", GetType(Date)), _
                                                           New DataColumn("PrdAkhirPendidikan", GetType(Date)), _
                                                           New DataColumn("InstitusiPendidikan", GetType(String)), _
                                                           New DataColumn("AlamatInstitusiPendidikan", GetType(String)), _
                                                           New DataColumn("JurusanPendidikan", GetType(String)), _
                                                           New DataColumn("LlsTdkLlsPendidikan", GetType(String)), _
                                                           New DataColumn("NilaiPendidikan", GetType(String)), _
                                                           New DataColumn("NoIjazahPendidikan", GetType(String))})
        If Session("Karyawan") = "UPD" Then
            Dim CmdDataPendidikan As New Data.SqlClient.SqlCommand
            With CmdDataPendidikan
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM EmpPendidikan WHERE NIK=@P1 ORDER BY NoUrutPendidikan"
                .Parameters.AddWithValue("@P1", UpdateNIK)
            End With
            Dim DataPendidikan As Data.SqlClient.SqlDataReader = CmdDataPendidikan.ExecuteReader
            While DataPendidikan.Read
                TmpDtPendidikan.Rows.Add(DataPendidikan("NoUrutPendidikan"), DataPendidikan("TgkPendidikan"), _
                                         DataPendidikan("PrdAwal"), DataPendidikan("PrdAkhir"), _
                                         DataPendidikan("Institusi"), DataPendidikan("Alamat"), _
                                         DataPendidikan("Jurusan"), DataPendidikan("LlsTdkLls"), _
                                         DataPendidikan("IPK"), DataPendidikan("NoIjazah"))
            End While
            DataPendidikan.Close()
            CmdDataPendidikan.Dispose()
        End If
        GridDataPendidikan.DataSource = TmpDtPendidikan
        Session("TmpDtPendidikan") = TmpDtPendidikan

        GridDataPendidikan.DataBind()
    End Sub
    Private Sub BindGridKetrampilan()
        Dim UpdateNIK As String = Session("NIK")
        TmpDtKetrampilan.Columns.AddRange(New DataColumn() {New DataColumn("NoUrutKetrampilan", GetType(Integer)), _
                                                            New DataColumn("NamaKetrampilan", GetType(String)), _
                                                            New DataColumn("PrdAwalKetrampilan", GetType(Date)), _
                                                            New DataColumn("PrdAkhirKetrampilan", GetType(Date)), _
                                                            New DataColumn("NamaSertifikatKetrampilan", GetType(String)), _
                                                            New DataColumn("GradeKetrampilan", GetType(String)), _
                                                            New DataColumn("NoSertifikatKetrampilan", GetType(String)), _
                                                            New DataColumn("InstitusiKetrampilan", GetType(String))})
        If Session("Karyawan") = "UPD" Then
            Dim CmdDataKetrampilan As New Data.SqlClient.SqlCommand
            With CmdDataKetrampilan
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM EmpKetrampilan WHERE NIK=@P1 ORDER BY NoUrutKetrampilan"
                .Parameters.AddWithValue("@P1", UpdateNIK)
            End With
            Dim DataKetrampilan As Data.SqlClient.SqlDataReader = CmdDataKetrampilan.ExecuteReader
            While DataKetrampilan.Read
                TmpDtKetrampilan.Rows.Add(DataKetrampilan("NoUrutKetrampilan"), DataKetrampilan("Nama"), _
                                          DataKetrampilan("PrdAwal"), DataKetrampilan("PrdAkhir"), _
                                          DataKetrampilan("Sertifikat"), DataKetrampilan("Grade"), _
                                          DataKetrampilan("NoSertifikat"), DataKetrampilan("Institusi"))
            End While
            DataKetrampilan.Close()
            CmdDataKetrampilan.Dispose()
        End If
        GridDataKetrampilan.DataSource = TmpDtKetrampilan
        Session("TmpDtKetrampilan") = TmpDtKetrampilan

        GridDataKetrampilan.DataBind()
    End Sub
    Private Sub BindGridRwytPekerjaan()
        Dim UpdateNIK As String = Session("NIK")
        TmpDtRwytPekerjaan.Columns.AddRange(New DataColumn() {New DataColumn("NoUrutRwytPekerjaan", GetType(Integer)), _
                                                              New DataColumn("PrdAwalRwytPekerjaan", GetType(Date)), _
                                                              New DataColumn("PrdAkhirRwytPekerjaan", GetType(Date)), _
                                                              New DataColumn("PerusahaanRwytPekerjaan", GetType(String)), _
                                                              New DataColumn("AlamatRwytPekerjaan", GetType(String)), _
                                                              New DataColumn("IndustriRwytPekerjaan", GetType(String)), _
                                                              New DataColumn("JabatanRwytPekerjaan", GetType(String)), _
                                                              New DataColumn("LokasiKerjaRwytPekerjaan", GetType(String)), _
                                                              New DataColumn("GajiRwytPekerjaan", GetType(String)), _
                                                              New DataColumn("TunjanganRwytPekerjaan", GetType(String)), _
                                                              New DataColumn("UraianRwytPekerjaan", GetType(String))})
        If Session("Karyawan") = "UPD" Then
            Dim CmdDataRwytPekerjaan As New Data.SqlClient.SqlCommand
            With CmdDataRwytPekerjaan
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM EmpPekerjaanH WHERE NIK=@P1 ORDER BY NoUrutRwytPekerjaan"
                .Parameters.AddWithValue("@P1", UpdateNIK)
            End With
            Dim DataRwytPekerjaan As Data.SqlClient.SqlDataReader = CmdDataRwytPekerjaan.ExecuteReader
            While DataRwytPekerjaan.Read
                TmpDtRwytPekerjaan.Rows.Add(DataRwytPekerjaan("NoUrutRwytPekerjaan"), DataRwytPekerjaan("PrdAwal"), _
                                            DataRwytPekerjaan("PrdAkhir"), DataRwytPekerjaan("Perusahaan"), _
                                            DataRwytPekerjaan("Alamat"), DataRwytPekerjaan("Industri"), _
                                            DataRwytPekerjaan("Jabatan"), DataRwytPekerjaan("LokasiKerja"), _
                                            DataRwytPekerjaan("GajiPokok"), DataRwytPekerjaan("Tunjangan"), _
                                            DataRwytPekerjaan("UraianPekerjaan"))
            End While
            DataRwytPekerjaan.Close()
            CmdDataRwytPekerjaan.Dispose()
        End If
        GridDataRwytPekerjaan.DataSource = TmpDtRwytPekerjaan
        Session("TmpDtRwytPekerjaan") = TmpDtRwytPekerjaan

        GridDataRwytPekerjaan.DataBind()
    End Sub
    Private Sub BindGridRwytPekerjaanMinarta()
        Dim UpdateNIK As String = Session("NIK")
        TmpDtRwytPekerjaanMinarta.Columns.AddRange(New DataColumn() {New DataColumn("NoUrutRwytPekerjaanMinarta", GetType(Integer)), _
                                                                     New DataColumn("PrdAwalRwytPekerjaanMinarta", GetType(Date)), _
                                                                     New DataColumn("PrdAkhirRwytPekerjaanMinarta", GetType(Date)), _
                                                                     New DataColumn("DivisiRwytPekerjaanMinarta", GetType(String)), _
                                                                     New DataColumn("SubdivisiRwytPekerjaanMinarta", GetType(String)), _
                                                                     New DataColumn("LokasiKerjaRwytPekerjaanMinarta", GetType(String)), _
                                                                     New DataColumn("JabatanRwytPekerjaanMinarta", GetType(String)), _
                                                                     New DataColumn("GolonganRwytPekerjaanMinarta", GetType(String)), _
                                                                     New DataColumn("GradeRwytPekerjaanMinarta", GetType(String)), _
                                                                     New DataColumn("GajiRwytPekerjaanMinarta", GetType(String)), _
                                                                     New DataColumn("TunjanganRwytPekerjaanMinarta", GetType(String)), _
                                                                     New DataColumn("KPIRwytPekerjaanMinarta", GetType(String)), _
                                                                     New DataColumn("TesKesehatanRwytPekerjaanMinarta", GetType(String)), _
                                                                     New DataColumn("HasilKesehatanRwytPekerjaanMinarta", GetType(String)), _
                                                                     New DataColumn("TesPsikologiRwytPekerjaanMinarta", GetType(String)), _
                                                                     New DataColumn("HasilPsikologiRwytPekerjaanMinarta", GetType(String)), _
                                                                     New DataColumn("AtasanRwytPekerjaanMinarta", GetType(String)), _
                                                                     New DataColumn("UraianRwytPekerjaanMinarta", GetType(String))})
        If Session("Karyawan") = "UPD" Then
            Dim CmdDataRwytPekerjaanMinarta As New Data.SqlClient.SqlCommand
            With CmdDataRwytPekerjaanMinarta
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM EmpPekerjaanMinarta WHERE NIK=@P1 ORDER BY NoUrutRwytPekerjaanMinarta"
                .Parameters.AddWithValue("@P1", UpdateNIK)
            End With
            Dim DataRwytPekerjaanMinarta As Data.SqlClient.SqlDataReader = CmdDataRwytPekerjaanMinarta.ExecuteReader
            While DataRwytPekerjaanMinarta.Read
                TmpDtRwytPekerjaanMinarta.Rows.Add(DataRwytPekerjaanMinarta("NoUrutRwytPekerjaanMinarta"), _
                                                   DataRwytPekerjaanMinarta("PrdAwal"), _
                                                   DataRwytPekerjaanMinarta("PrdAkhir"), _
                                                   DataRwytPekerjaanMinarta("Divisi"), _
                                                   DataRwytPekerjaanMinarta("Subdivisi"), _
                                                   DataRwytPekerjaanMinarta("LokasiKerja"), _
                                                   DataRwytPekerjaanMinarta("Jabatan"), _
                                                   DataRwytPekerjaanMinarta("Golongan"), _
                                                   DataRwytPekerjaanMinarta("Grade"), _
                                                   DataRwytPekerjaanMinarta("GajiPokok"), _
                                                   DataRwytPekerjaanMinarta("Tunjangan"), _
                                                   DataRwytPekerjaanMinarta("KPI"), _
                                                   DataRwytPekerjaanMinarta("TesKesehatan"), _
                                                   DataRwytPekerjaanMinarta("HslKesehatan"), _
                                                   DataRwytPekerjaanMinarta("TesPsikologi"), _
                                                   DataRwytPekerjaanMinarta("HslPsikologi"), _
                                                   DataRwytPekerjaanMinarta("Atasan"), _
                                                   DataRwytPekerjaanMinarta("UraianPekerjaan"))
            End While
            DataRwytPekerjaanMinarta.Close()
            CmdDataRwytPekerjaanMinarta.Dispose()
        End If
        GridDataRwytPekerjaanMinarta.DataSource = TmpDtRwytPekerjaanMinarta
        Session("TmpDtRwytPekerjaanMinarta") = TmpDtRwytPekerjaanMinarta

        GridDataRwytPekerjaanMinarta.DataBind()
    End Sub

    Protected Sub RblStsNikah_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RblStsNikah.SelectedIndexChanged
        If RblStsNikah.SelectedItem.Value IsNot Nothing Then
            If RblStsNikah.SelectedItem.Value = "Menikah" Then
                TxtTglNikah.Visible = True
            Else
                TxtTglNikah.Visible = False
            End If
        End If
    End Sub

    Protected Sub BtnPopUpKeluarga_Click(sender As Object, e As EventArgs) Handles BtnPopUpKeluarga.Click
        TxtActKeluarga.Text = "NEW"
        TxtNoUrutKeluarga.Text = ""
        TxtHubKeluarga.Text = ""
        TxtNamaKeluarga.Text = ""
        TxtJenisKelaminKeluarga.Text = ""
        TxtTglLahirKeluarga.Text = ""
        TxtPekerjaanKeluarga.Text = ""
        TxtPerusahaanKeluarga.Text = ""
        PopEntKeluarga.ShowOnPageLoad = True
    End Sub
    Protected Sub BtnPopUpPendidikan_Click(sender As Object, e As EventArgs) Handles BtnPopUpPendidikan.Click
        TxtActPendidikan.Text = "NEW"
        TxtNoUrutPendidikan.Text = ""
        TxtTgkPendidikan.Text = ""
        TxtPrdAwalPendidikan.Text = ""
        TxtPrdAkhirPendidikan.Text = ""
        TxtInstitusiPendidikan.Text = ""
        TxtAlamatInstitusiPendidikan.Text = ""
        TxtJurusanPendidikan.Text = ""
        TxtLlsTdkLlsPendidikan.Text = ""
        TxtNilaiPendidikan.Text = ""
        TxtNoIjazahPendidikan.Text = ""
        PopEntPendidikan.ShowOnPageLoad = True
    End Sub
    Protected Sub BtnPopUpKetrampilan_Click(sender As Object, e As EventArgs) Handles BtnPopUpKetrampilan.Click
        TxtActKetrampilan.Text = "NEW"
        TxtNoUrutKetrampilan.Text = ""
        TxtNamaKetrampilan.Text = ""
        TxtPrdAwalKetrampilan.Text = ""
        TxtPrdAkhirKetrampilan.Text = ""
        TxtNamaSertifikatKetrampilan.Text = ""
        TxtGradeKetrampilan.Text = ""
        TxtNoSertifikatKetrampilan.Text = ""
        TxtInstitusiKetrampilan.Text = ""
        PopEntKetrampilan.ShowOnPageLoad = True
    End Sub
    Protected Sub BtnPopUpRwytPekerjaan_Click(sender As Object, e As EventArgs) Handles BtnPopUpRwytPekerjaan.Click
        TxtActRwytPekerjaan.Text = "NEW"
        TxtNoUrutRwytPekerjaan.Text = ""
        TxtPrdAwalRwytPekerjaan.Text = ""
        TxtPrdAkhirRwytPekerjaan.Text = ""
        TxtPerusahaanRwytPekerjaan.Text = ""
        TxtAlamatRwytPekerjaan.Text = ""
        TxtIndustriRwytPekerjaan.Text = ""
        TxtJabatanRwytPekerjaan.Text = ""
        TxtLokasiKerjaRwytPekerjaan.Text = ""
        TxtGajiRwytPekerjaan.Text = ""
        TxtTunjanganRwytPekerjaan.Text = ""
        TxtUraianRwytPekerjaan.Text = ""
        PopEntRwytPekerjaan.ShowOnPageLoad = True
    End Sub
    Protected Sub BtnPopUpRwytPekerjaanMinarta_Click(sender As Object, e As EventArgs) Handles BtnPopUpRwytPekerjaanMinarta.Click
        TxtActRwytPekerjaanMinarta.Text = "NEW"
        TxtNoUrutRwytPekerjaanMinarta.Text = ""
        TxtPrdAwalRwytPekerjaanMinarta.Text = ""
        TxtPrdAkhirRwytPekerjaanMinarta.Text = ""
        TxtDivisiRwytPekerjaanMinarta.Text = ""
        TxtSubdivisiRwytPekerjaanMinarta.Text = ""
        TxtLokasiKerjaRwytPekerjaanMinarta.Text = ""
        TxtJabatanRwytPekerjaanMinarta.Text = ""
        TxtGolonganRwytPekerjaanMinarta.Text = ""
        TxtGradeRwytPekerjaanMinarta.Text = ""
        TxtGajiRwytPekerjaanMinarta.Text = ""
        TxtTunjanganRwytPekerjaanMinarta.Text = ""
        TxtKPIRwytPekerjaanMinarta.Text = ""
        TxtTesKesehatanRwytPekerjaanMinarta.Text = ""
        TxtHasilKesehatanRwytPekerjaanMinarta.Text = ""
        TxtTesPsikologiRwytPekerjaanMinarta.Text = ""
        TxtHasilPsikologiRwytPekerjaanMinarta.Text = ""
        TxtAtasanRwytPekerjaanMinarta.Text = ""
        TxtUraianRwytPekerjaanMinarta.Text = ""
        PopEntRwytPekerjaanMinarta.ShowOnPageLoad = True
    End Sub

    Protected Sub BtnSaveKeluarga_Click(sender As Object, e As EventArgs) Handles BtnSaveKeluarga.Click
        If TxtHubKeluarga.Value = "" Then
            LblErr.Text = "Hubungan Keluarga belum di-pilih."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtNamaKeluarga.Text = "" Then
            LblErr.Text = "Nama Keluarga belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtJenisKelaminKeluarga.Value = "" Then
            LblErr.Text = "Jenis Kelamin belum di-pilih."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtTglLahirKeluarga.Text = "" Then
            LblErr.Text = "Tanggal Lahir belum di-pilih."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtPekerjaanKeluarga.Text = "" Then
            LblErr.Text = "Pekerjaan belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        TmpDtKeluarga = Session("TmpDtKeluarga")

        If TxtActKeluarga.Text = "NEW" Then
            Dim Counter As Integer
            Dim Result As DataRow = TmpDtKeluarga.Select("NoUrutKeluarga > 0", "NoUrutKeluarga DESC").FirstOrDefault
            If Result Is Nothing Then
                Counter = 1
            Else
                Counter = Result("NoUrutKeluarga") + 1
            End If
            TmpDtKeluarga.Rows.Add(Counter, TxtHubKeluarga.Value, TxtNamaKeluarga.Text, TxtJenisKelaminKeluarga.Value, TxtTglLahirKeluarga.Text, TxtPekerjaanKeluarga.Text, TxtPerusahaanKeluarga.Text)
        Else
            Dim Result As DataRow = TmpDtKeluarga.Select("NoUrutKeluarga='" & TxtNoUrutKeluarga.Text & "'").FirstOrDefault
            If Result Is Nothing Then
                Result("HubKeluarga") = TxtHubKeluarga.Value
                Result("NamaKeluarga") = TxtNamaKeluarga.Text
                Result("JenisKelaminKeluarga") = TxtJenisKelaminKeluarga.Value
                Result("TglLahirKeluarga") = TxtTglLahirKeluarga.Text
                Result("PekerjaanKeluarga") = TxtPekerjaanKeluarga.Text
                Result("PerusahaanKeluarga") = TxtPerusahaanKeluarga.Text
            End If
        End If

        GridDataKeluarga.DataSource = TmpDtKeluarga
        GridDataKeluarga.DataBind()

        Session("TmpDtKeluarga") = TmpDtKeluarga
        PopEntKeluarga.ShowOnPageLoad = False
    End Sub
    Protected Sub BtnSavePendidikan_Click(sender As Object, e As EventArgs) Handles BtnSavePendidikan.Click
        If TxtTgkPendidikan.Value = "" Then
            LblErr.Text = "Tingkat Pendidikan belum di-pilih."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtPrdAwalPendidikan.Text = "" Then
            LblErr.Text = "Tanggal Dimulai belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtInstitusiPendidikan.Text = "" Then
            LblErr.Text = "Nama Institusi belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtAlamatInstitusiPendidikan.Text = "" Then
            LblErr.Text = "Alamat Institusi belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        TmpDtPendidikan = Session("TmpDtPendidikan")

        If TxtActPendidikan.Text = "NEW" Then
            Dim Counter As Integer
            Dim Result As DataRow = TmpDtPendidikan.Select("NoUrutPendidikan > 0", "NoUrutPendidikan DESC").FirstOrDefault
            If Result Is Nothing Then
                Counter = 1
            Else
                Counter = Result("NoUrutPendidikan") + 1
            End If
            TmpDtPendidikan.Rows.Add(Counter, TxtTgkPendidikan.Value, TxtPrdAwalPendidikan.Text, TxtPrdAkhirPendidikan.Text, TxtInstitusiPendidikan.Text, TxtAlamatInstitusiPendidikan.Text, TxtJurusanPendidikan.Text, TxtLlsTdkLlsPendidikan.Value, TxtNilaiPendidikan.Text, TxtNoIjazahPendidikan.Text)
        Else
            Dim Result As DataRow = TmpDtPendidikan.Select("NoUrutPendidikan='" & TxtNoUrutPendidikan.Text & "'").FirstOrDefault
            If Result Is Nothing Then
                Result("TgkPendidikan") = TxtTgkPendidikan.Value
                Result("PrdAwalPendidikan") = TxtPrdAwalPendidikan.Text
                Result("PrdAkhirPendidikan") = TxtPrdAkhirPendidikan.Text
                Result("InstitusiPendidikan") = TxtInstitusiPendidikan.Text
                Result("AlamatInstitusiPendidikan") = TxtAlamatInstitusiPendidikan.Text
                Result("JurusanPendidikan") = TxtJurusanPendidikan.Text
                Result("LlsTdkLlsPendidikan") = TxtLlsTdkLlsPendidikan.Value
                Result("NilaiPendidikan") = TxtNilaiPendidikan.Text
                Result("NoIjazahPendidikan") = TxtNoIjazahPendidikan.Text
            End If
        End If

        GridDataPendidikan.DataSource = TmpDtPendidikan
        GridDataPendidikan.DataBind()

        Session("TmpDtPendidikan") = TmpDtPendidikan
        PopEntPendidikan.ShowOnPageLoad = False
    End Sub
    Protected Sub BtnSaveKetrampilan_Click(sender As Object, e As EventArgs) Handles BtnSaveKetrampilan.Click
        If TxtNamaKetrampilan.Text = "" Then
            LblErr.Text = "Nama Ketramplan belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtPrdAwalKetrampilan.Text = "" Then
            LblErr.Text = "Tanggal Dimulai belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtNamaSertifikatKetrampilan.Text = "" Then
            LblErr.Text = "Nama Sertifikat belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtGradeKetrampilan.Text = "" Then
            LblErr.Text = "Level/Grade Ketrampilan belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtInstitusiKetrampilan.Text = "" Then
            LblErr.Text = "Nama Institusi belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        TmpDtKetrampilan = Session("TmpDtKetrampilan")

        If TxtActKetrampilan.Text = "NEW" Then
            Dim Counter As Integer
            Dim Result As DataRow = TmpDtKetrampilan.Select("NoUrutKetrampilan > 0", "NoUrutKetrampilan DESC").FirstOrDefault
            If Result Is Nothing Then
                Counter = 1
            Else
                Counter = Result("NoUrutKetrampilan") + 1
            End If
            TmpDtKetrampilan.Rows.Add(Counter, TxtNamaKetrampilan.Text, TxtPrdAwalKetrampilan.Text, TxtPrdAkhirKetrampilan.Text, TxtNamaSertifikatKetrampilan.Text, TxtGradeKetrampilan.Text, TxtNoSertifikatKetrampilan.Text, TxtInstitusiKetrampilan.Text)
        Else
            Dim Result As DataRow = TmpDtPendidikan.Select("NoUrutKetrampilan='" & TxtNoUrutKetrampilan.Text & "'").FirstOrDefault
            If Result Is Nothing Then
                Result("NamaKetrampilan") = TxtNamaKetrampilan.Text
                Result("PrdAwalKetrampilan") = TxtPrdAwalKetrampilan.Text
                Result("PrdAkhirKetrampilan") = TxtPrdAkhirKetrampilan.Text
                Result("NamaSertifikatKetrampilan") = TxtNamaSertifikatKetrampilan.Text
                Result("GradeKetrampilan") = TxtGradeKetrampilan.Text
                Result("NoSertifikatKetrampilan") = TxtNoSertifikatKetrampilan.Text
                Result("InstitusiKetrampilan") = TxtInstitusiKetrampilan.Text
            End If
        End If

        GridDataKetrampilan.DataSource = TmpDtKetrampilan
        GridDataKetrampilan.DataBind()

        Session("TmpDtKetrampilan") = TmpDtKetrampilan
        PopEntKetrampilan.ShowOnPageLoad = False
    End Sub
    Protected Sub BtnSaveRwytPekerjaan_Click(sender As Object, e As EventArgs) Handles BtnSaveRwytPekerjaan.Click
        If TxtPrdAwalRwytPekerjaan.Text = "" Then
            LblErr.Text = "Tanggal Dimulai belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtPrdAkhirRwytPekerjaan.Text = "" Then
            LblErr.Text = "Tanggal Berakhir belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtPerusahaanRwytPekerjaan.Text = "" Then
            LblErr.Text = "Nama Perusahaan belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtAlamatRwytPekerjaan.Text = "" Then
            LblErr.Text = "Alamat Perusahaan belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtIndustriRwytPekerjaan.Text = "" Then
            LblErr.Text = "Bidang Industri belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtJabatanRwytPekerjaan.Text = "" Then
            LblErr.Text = "Jabatan belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtLokasiKerjaRwytPekerjaan.Text = "" Then
            LblErr.Text = "Lokasi Kerja belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtGajiRwytPekerjaan.Text = "" Then
            LblErr.Text = "Gaji Pokok belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtUraianRwytPekerjaan.Text = "" Then
            LblErr.Text = "Uraian Pekerjaan belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        TmpDtRwytPekerjaan = Session("TmpDtRwytPekerjaan")

        If TxtActRwytPekerjaan.Text = "NEW" Then
            Dim Counter As Integer
            Dim Result As DataRow = TmpDtRwytPekerjaan.Select("NoUrutRwytPekerjaan > 0", "NoUrutRwytPekerjaan DESC").FirstOrDefault
            If Result Is Nothing Then
                Counter = 1
            Else
                Counter = Result("NoUrutRwytPekerjaan") + 1
            End If
            TmpDtRwytPekerjaan.Rows.Add(Counter, TxtPrdAwalRwytPekerjaan.Text, TxtPrdAkhirRwytPekerjaan.Text, TxtPerusahaanRwytPekerjaan.Text, TxtAlamatRwytPekerjaan.Text, TxtIndustriRwytPekerjaan.Text, TxtJabatanRwytPekerjaan.Text, TxtLokasiKerjaRwytPekerjaan.Text, TxtGajiRwytPekerjaan.Text, TxtTunjanganRwytPekerjaan.Text, TxtUraianRwytPekerjaan.Text)
        Else
            Dim Result As DataRow = TmpDtRwytPekerjaan.Select("NoUrutRwytPekerjaan='" & TxtNoUrutRwytPekerjaan.Text & "'").FirstOrDefault
            If Result Is Nothing Then
                Result("PrdAwalRwytPekerjaan") = TxtPrdAwalRwytPekerjaan.Text
                Result("PrdAkhirRwytPekerjaan") = TxtPrdAkhirRwytPekerjaan.Text
                Result("PerusahaanRwytPekerjaan") = TxtPerusahaanRwytPekerjaan.Text
                Result("AlamatRwytPekerjaan") = TxtAlamatRwytPekerjaan.Text
                Result("IndustriRwytPekerjaan") = TxtIndustriRwytPekerjaan.Text
                Result("JabatanRwytPekerjaan") = TxtJabatanRwytPekerjaan.Text
                Result("LokasiKerjaRwytPekerjaan") = TxtLokasiKerjaRwytPekerjaan.Text
                Result("GajiRwytPekerjaan") = TxtGajiRwytPekerjaan.Text
                Result("TunjanganRwytPekerjaan") = TxtTunjanganRwytPekerjaan.Text
                Result("UraianRwytPekerjaan") = TxtUraianRwytPekerjaan.Text
            End If
        End If

        GridDataRwytPekerjaan.DataSource = TmpDtRwytPekerjaan
        GridDataRwytPekerjaan.DataBind()

        Session("TmpDtRwytPekerjaan") = TmpDtRwytPekerjaan
        PopEntRwytPekerjaan.ShowOnPageLoad = False
    End Sub
    Protected Sub BtnSaveRwytPekerjaanMinarta_Click(sender As Object, e As EventArgs) Handles BtnSaveRwytPekerjaanMinarta.Click
        If TxtPrdAwalRwytPekerjaanMinarta.Text = "" Then
            LblErr.Text = "Tanggal Dimulai belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtPrdAkhirRwytPekerjaanMinarta.Text = "" Then
            LblErr.Text = "Tanggal Berakhir belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtDivisiRwytPekerjaanMinarta.Text = "" Then
            LblErr.Text = "Divisi belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtSubdivisiRwytPekerjaanMinarta.Text = "" Then
            LblErr.Text = "Subdivisi belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtLokasiKerjaRwytPekerjaanMinarta.Text = "" Then
            LblErr.Text = "Lokasi Kerja belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtJabatanRwytPekerjaanMinarta.Text = "" Then
            LblErr.Text = "Jabatan belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtGolonganRwytPekerjaanMinarta.Text = "" Then
            LblErr.Text = "Golongan Kerja belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtGradeRwytPekerjaanMinarta.Text = "" Then
            LblErr.Text = "Grade belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtGajiRwytPekerjaanMinarta.Text = "" Then
            LblErr.Text = "Gaji Pokok belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtAtasanRwytPekerjaanMinarta.Text = "" Then
            LblErr.Text = "Atasan belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If
        If TxtUraianRwytPekerjaanMinarta.Text = "" Then
            LblErr.Text = "Uraian Pekerjaan belum di-isi."
            ErrMsg.ShowOnPageLoad = True
            Exit Sub
        End If

        TmpDtRwytPekerjaanMinarta = Session("TmpDtRwytPekerjaanMinarta")

        If TxtActRwytPekerjaanMinarta.Text = "NEW" Then
            Dim Counter As Integer
            Dim Result As DataRow = TmpDtRwytPekerjaanMinarta.Select("NoUrutRwytPekerjaanMinarta > 0", "NoUrutRwytPekerjaanMinarta DESC").FirstOrDefault
            If Result Is Nothing Then
                Counter = 1
            Else
                Counter = Result("NoUrutRwytPekerjaanMinarta") + 1
            End If
            TmpDtRwytPekerjaanMinarta.Rows.Add(Counter, TxtPrdAwalRwytPekerjaanMinarta.Text, TxtPrdAkhirRwytPekerjaanMinarta.Text, TxtDivisiRwytPekerjaanMinarta.Text, TxtSubdivisiRwytPekerjaanMinarta.Text, TxtLokasiKerjaRwytPekerjaanMinarta.Text, TxtJabatanRwytPekerjaanMinarta.Text, TxtGolonganRwytPekerjaanMinarta.Text, TxtGradeRwytPekerjaanMinarta.Text, TxtGajiRwytPekerjaanMinarta.Text, TxtTunjanganRwytPekerjaanMinarta.Text, TxtKPIRwytPekerjaanMinarta.Text, TxtTesKesehatanRwytPekerjaanMinarta.Text, TxtHasilKesehatanRwytPekerjaanMinarta.Text, TxtTesPsikologiRwytPekerjaanMinarta.Text, TxtHasilPsikologiRwytPekerjaanMinarta.Text, TxtAtasanRwytPekerjaanMinarta.Text, TxtUraianRwytPekerjaanMinarta.Text)
        Else
            Dim Result As DataRow = TmpDtRwytPekerjaanMinarta.Select("NoUrutRwytPekerjaanMinarta='" & TxtNoUrutRwytPekerjaanMinarta.Text & "'").FirstOrDefault
            If Result Is Nothing Then
                Result("PrdAwalRwytPekerjaanMinarta") = TxtPrdAwalRwytPekerjaanMinarta.Text
                Result("PrdAkhirRwytPekerjaanMinarta") = TxtPrdAkhirRwytPekerjaanMinarta.Text
                Result("DivisiRwytPekerjaanMinarta") = TxtDivisiRwytPekerjaanMinarta.Text
                Result("SubdivisiRwytPekerjaanMinarta") = TxtSubdivisiRwytPekerjaanMinarta.Text
                Result("LokasiKerjaRwytPekerjaanMinarta") = TxtLokasiKerjaRwytPekerjaanMinarta.Text
                Result("JabatanRwytPekerjaanMinarta") = TxtJabatanRwytPekerjaanMinarta.Text
                Result("GolonganRwytPekerjaanMinarta") = TxtGolonganRwytPekerjaanMinarta.Text
                Result("GradeRwytPekerjaanMinarta") = TxtGradeRwytPekerjaanMinarta.Text
                Result("GajiRwytPekerjaanMinarta") = TxtGajiRwytPekerjaanMinarta.Text
                Result("TunjanganRwytPekerjaanMinarta") = TxtTunjanganRwytPekerjaanMinarta.Text
                Result("KPIRwytPekerjaanMinarta") = TxtKPIRwytPekerjaanMinarta.Text
                Result("TesKesehatanRwytPekerjaanMinarta") = TxtTesKesehatanRwytPekerjaanMinarta.Text
                Result("HasilKesehatanRwytPekerjaanMinarta") = TxtHasilKesehatanRwytPekerjaanMinarta.Text
                Result("TesPsikologiRwytPekerjaanMinarta") = TxtTesPsikologiRwytPekerjaanMinarta.Text
                Result("HasilPsikologiRwytPekerjaanMinarta") = TxtTesPsikologiRwytPekerjaanMinarta.Text
                Result("AtasanRwytPekerjaanMinarta") = TxtAtasanRwytPekerjaanMinarta.Text
                Result("UraianRwytPekerjaanMinarta") = TxtUraianRwytPekerjaanMinarta.Text
            End If
        End If

        GridDataRwytPekerjaanMinarta.DataSource = TmpDtRwytPekerjaanMinarta
        GridDataRwytPekerjaanMinarta.DataBind()

        Session("TmpDtRwytPekerjaanMinarta") = TmpDtRwytPekerjaanMinarta
        PopEntRwytPekerjaanMinarta.ShowOnPageLoad = False
    End Sub

    Protected Sub BtnSaveDataEntry_Click(sender As Object, e As System.EventArgs) Handles BtnSaveDataEntry.Click
        TmpDtKeluarga = Session("TmpDtKeluarga")
        TmpDtPendidikan = Session("TmpDtPendidikan")
        TmpDtKetrampilan = Session("TmpDtKetrampilan")
        TmpDtRwytPekerjaan = Session("TmpDtRwytPekerjaan")
        TmpDtRwytPekerjaanMinarta = Session("TmpDtRwytPekerjaanMinarta")
        Dim UpdateNIK As String = Session("NIK")

        If LblAction.Text = "NEW" Then
            Dim CmdInsertHdr As New Data.SqlClient.SqlCommand
            If PasFoto.HasFile And PasFoto.PostedFile.ContentType.ToLower <> "image/jpeg" Then
                LblErr.Text = "Pas Foto hanya mendukung file dengan ext. JPG/JPEG."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            If FileKTP.HasFile And (FileKTP.PostedFile.ContentType.ToLower <> "image/jpeg" Or _
                                    FileKTP.PostedFile.ContentType.ToLower <> "application/pdf") Then
                LblErr.Text = "KTP hanya mendukung file dengan ext. JPG/JPEG/PDF."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            If FileKK.HasFile And (FileKK.PostedFile.ContentType.ToLower <> "image/jpeg" Or _
                                   FileKK.PostedFile.ContentType.ToLower <> "application/pdf") Then
                LblErr.Text = "Kartu Keluarga hanya mendukung file dengan ext. JPG/JPEG/PDF."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            If FileNPWP.HasFile And (FileNPWP.PostedFile.ContentType.ToLower <> "image/jpeg" Or _
                                     FileNPWP.PostedFile.ContentType.ToLower <> "application/pdf") Then
                LblErr.Text = "NPWP hanya mendukung file dengan ext. JPG/JPEG/PDF."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            If FileIjazah.HasFile And (FileIjazah.PostedFile.ContentType.ToLower <> "image/jpeg" Or _
                                       FileIjazah.PostedFile.ContentType.ToLower <> "application/pdf") Then
                LblErr.Text = "Ijazah hanya mendukung file dengan ext. JPG/JPEG/PDF."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            If FileTranskripNilai.HasFile And (FileTranskripNilai.PostedFile.ContentType.ToLower <> "image/jpeg" Or _
                                               FileTranskripNilai.PostedFile.ContentType.ToLower <> "application/pdf") Then
                LblErr.Text = "Transkrip Nilai hanya mendukung file dengan ext. JPG/JPEG/PDF."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            With CmdInsertHdr
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Karyawan (NIK, Nama, Kelamin, TmpLahir, TglLahir, WN, StsNikah, TglNikah, " & _
                               "Agama, Alamat, Provinsi, Kota, AlamatSurat, Email, NoTelp, LokasiKerja, Divisi, " & _
                               "Subdivisi, Jabatan, Golongan, Grade, PrdAwal, UraianPekerjaan, Foto, ScanKTP, " & _
                               "ScanKK, ScanNPWP, ScanIjazah, ScanTranskripNilai, ScanSertifikat) VALUES (@P1, " & _
                               "@P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9, @P10, @P11, @P12, @P13, @P14, @P15, @P16, " & _
                               "@P17, @P18, @P19, @P20, @P21, @P22, @P23, @P24, @P25, @P26, @P27, @P28, @P29, @P30)"
                .Parameters.AddWithValue("@P1", TxtNIK.Text)
                .Parameters.AddWithValue("@P2", TxtNama.Text)
                .Parameters.AddWithValue("@P3", RblJenisKelamin.Value)
                .Parameters.AddWithValue("@P4", TxtTempatLahir.Text)
                .Parameters.AddWithValue("@P5", TxtTanggalLahir.Date)
                .Parameters.AddWithValue("@P6", TxtWN.Text)
                .Parameters.AddWithValue("@P7", RblStsNikah.Value)
                .Parameters.AddWithValue("@P8", If(RblStsNikah.Value = "Menikah", TxtTglNikah.Date, DBNull.Value))
                .Parameters.AddWithValue("@P9", RblAgama.Value)
                .Parameters.AddWithValue("@P10", TxtAlamat.Text)
                .Parameters.AddWithValue("@P11", TxtProvinsi.Text)
                .Parameters.AddWithValue("@P12", TxtKota.Text)
                .Parameters.AddWithValue("@P13", TxtAlamatSurat.Text)
                .Parameters.AddWithValue("@P14", TxtEmail.Text)
                .Parameters.AddWithValue("@P15", TxtNoTelp.Text)
                .Parameters.AddWithValue("@P16", TxtLokasiKerja.Text)
                .Parameters.AddWithValue("@P17", TxtDivisi.Text)
                .Parameters.AddWithValue("@P18", TxtSubdivisi.Text)
                .Parameters.AddWithValue("@P19", TxtJabatan.Text)
                .Parameters.AddWithValue("@P20", TxtGolongan.Text)
                .Parameters.AddWithValue("@P21", TxtGrade.Text)
                .Parameters.AddWithValue("@P22", TxtPrdAwal.Date)
                .Parameters.AddWithValue("@P23", TxtUraian.Text)
                Dim FolderPath As String = Server.MapPath("/PDF/Employee/" + TxtNIK.Text)
                If Not Directory.Exists(FolderPath) Then
                    Directory.CreateDirectory(FolderPath)
                End If
                If PasFoto.HasFile And PasFoto.PostedFile.ContentLength < 2000000 Then
                    Dim FileName As String = TxtNIK.Text & "PasFoto.jpg"
                    PasFoto.PostedFile.SaveAs(Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileName))
                    .Parameters.AddWithValue("@P24", "/PDF/Employee/" + TxtNIK.Text + "/" + FileName)
                ElseIf PasFoto.HasFile And PasFoto.PostedFile.ContentLength >= 2000000 Then
                    LblErr.Text = "Ukuran file foto maximum 2MB"
                    ErrMsg.ShowOnPageLoad = True
                    Exit Sub
                Else
                    .Parameters.AddWithValue("@P24", Session("PasFoto"))
                End If
                If FileKTP.HasFile Then
                    Dim FileNameKTP As String = TxtNIK.Text & "KTP." & _
                                                If(FileKTP.PostedFile.ContentType.ToLower = "jpg", "jpg", "pdf")
                    FileKTP.PostedFile.SaveAs(Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileNameKTP))
                    .Parameters.AddWithValue("@P25", "/PDF/Employee/" + TxtNIK.Text + "/" + FileNameKTP)
                Else
                    .Parameters.AddWithValue("@P25", DBNull.Value)
                End If
                If FileKK.HasFile Then
                    Dim FileNameKK As String = TxtNIK.Text & "KK." & _
                                               If(FileKK.PostedFile.ContentType.ToLower = "jpg", "jpg", "pdf")
                    FileKK.PostedFile.SaveAs(Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileNameKK))
                    .Parameters.AddWithValue("@P26", "/PDF/Employee/" + TxtNIK.Text + "/" + FileNameKK)
                Else
                    .Parameters.AddWithValue("@P26", DBNull.Value)
                End If
                If FileNPWP.HasFile Then
                    Dim FileNameNPWP As String = TxtNIK.Text & "NPWP." & _
                                                 If(FileNPWP.PostedFile.ContentType.ToLower = "jpg", "jpg", "pdf")
                    FileNPWP.PostedFile.SaveAs(Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileNameNPWP))
                    .Parameters.AddWithValue("@P27", "/PDF/Employee/" + TxtNIK.Text + "/" + FileNameNPWP)
                Else
                    .Parameters.AddWithValue("@P27", DBNull.Value)
                End If
                If FileIjazah.HasFile Then
                    Dim FileNameIjazah As String = TxtNIK.Text & "Ijazah." & _
                                                   If(FileIjazah.PostedFile.ContentType.ToLower = "jpg", "jpg", "pdf")
                    FileIjazah.PostedFile.SaveAs(Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileNameIjazah))
                    .Parameters.AddWithValue("@P28", "/PDF/Employee/" + TxtNIK.Text + "/" + FileNameIjazah)
                Else
                    .Parameters.AddWithValue("@P28", DBNull.Value)
                End If
                If FileTranskripNilai.HasFile Then
                    Dim FileNameTranskripNilai As String = TxtNIK.Text & "TranskripNilai." & _
                                                           If(FileTranskripNilai.PostedFile.ContentType.ToLower = _
                                                              "jpg", "jpg", "pdf")
                    FileTranskripNilai.PostedFile.SaveAs(Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileNameTranskripNilai))
                    .Parameters.AddWithValue("@P29", "/PDF/Employee/" + TxtNIK.Text + "/" + FileNameTranskripNilai)
                Else
                    .Parameters.AddWithValue("@P29", DBNull.Value)
                End If
                If FileSertifikat.HasFile Then
                    Dim FileFileSertifikat As HttpFileCollection = Request.Files
                    Dim FilePathSertifikatDiServer As String
                    Dim Ctr As Integer = 0
                    For i As Integer = 0 To FileFileSertifikat.Count - 1
                        If FileFileSertifikat.Count - i <= 2 Then
                            Ctr += 1
                            Dim FileSertifikatSatuan As HttpPostedFile = FileFileSertifikat(i)
                            Dim FileNameSertifikat As String = TxtNIK.Text & "Sertifikat[" & Ctr & "].jpg"
                            FileSertifikatSatuan.SaveAs(Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileNameSertifikat))
                            If Ctr > 1 Then
                                FilePathSertifikatDiServer = FilePathSertifikatDiServer + "|/PDF/Employee/" + TxtNIK.Text + "/" + FileNameSertifikat
                            Else
                                FilePathSertifikatDiServer = "/PDF/Employee/" + TxtNIK.Text + "/" + FileNameSertifikat
                            End If
                        End If
                    Next
                    Ctr = 0
                    .Parameters.AddWithValue("@P30", FilePathSertifikatDiServer)
                Else
                    .Parameters.AddWithValue("@P30", DBNull.Value)
                End If
                .ExecuteNonQuery()
                .Dispose()
            End With

            If TxtNoKTP.Text <> "" Then
                Dim CmdInsertKTP As New Data.SqlClient.SqlCommand
                With CmdInsertKTP
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpID (NIK, JenisID, NoID, PrdAwal, PrdAkhir, DiterbitkanOleh) " & _
                                   "VALUES (@P1, @P2, @P3, @P4, @P5, @P6)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", "KTP")
                    .Parameters.AddWithValue("@P3", TxtNoKTP.Text)
                    .Parameters.AddWithValue("@P4", TxtPrdAwalKTP.Date)
                    .Parameters.AddWithValue("@P5", TxtPrdAkhirKTP.Date)
                    .Parameters.AddWithValue("@P6", TxtDiterbitkanOlehKTP.Text)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If

            If TxtNoPassport.Text <> "" Then
                Dim CmdInsertPassport As New Data.SqlClient.SqlCommand
                With CmdInsertPassport
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpID (NIK, JenisID, NoID, PrdAwal, PrdAkhir, DiterbitkanOleh) " & _
                                   "VALUES (@P1, @P2, @P3, @P4, @P5, @P6)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", "Passport")
                    .Parameters.AddWithValue("@P3", TxtNoPassport.Text)
                    .Parameters.AddWithValue("@P4", TxtPrdAwalPassport.Date)
                    .Parameters.AddWithValue("@P5", TxtPrdAkhirPassport.Date)
                    .Parameters.AddWithValue("@P6", TxtDiterbitkanOlehPassport.Text)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If

            If TxtNoNPWP.Text <> "" Then
                Dim CmdInsertNPWP As New Data.SqlClient.SqlCommand
                With CmdInsertNPWP
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpID (NIK, JenisID, NoID, PrdAwal, PrdAkhir, DiterbitkanOleh) " & _
                                   "VALUES (@P1, @P2, @P3, @P4, @P5, @P6)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", "NPWP")
                    .Parameters.AddWithValue("@P3", TxtNoNPWP.Text)
                    .Parameters.AddWithValue("@P4", TxtPrdAwalNPWP.Date)
                    .Parameters.AddWithValue("@P5", TxtPrdAkhirNPWP.Date)
                    .Parameters.AddWithValue("@P6", TxtDiterbitkanOlehNPWP.Text)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If

            If TxtNoNPWP.Text <> "" Then
                Dim CmdInsertNPWP As New Data.SqlClient.SqlCommand
                With CmdInsertNPWP
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpID (NIK, JenisID, NoID, PrdAwal, PrdAkhir, DiterbitkanOleh) " & _
                                   "VALUES (@P1, @P2, @P3, @P4, @P5, @P6)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", "KK")
                    .Parameters.AddWithValue("@P3", TxtNoKK.Text)
                    .Parameters.AddWithValue("@P4", TxtPrdAwalKK.Date)
                    .Parameters.AddWithValue("@P5", TxtPrdAkhirKK.Date)
                    .Parameters.AddWithValue("@P6", TxtDiterbitkanOlehKK.Text)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If

            If TxtNoSIMA.Text <> "" Then
                Dim CmdInsertSIMA As New Data.SqlClient.SqlCommand
                With CmdInsertSIMA
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpID (NIK, JenisID, NoID, PrdAwal, PrdAkhir, DiterbitkanOleh) " & _
                                   "VALUES (@P1, @P2, @P3, @P4, @P5, @P6)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", "SIM A")
                    .Parameters.AddWithValue("@P3", TxtNoSIMA.Text)
                    .Parameters.AddWithValue("@P4", TxtPrdAwalSIMA.Date)
                    .Parameters.AddWithValue("@P5", TxtPrdAkhirSIMA.Date)
                    .Parameters.AddWithValue("@P6", TxtDiterbitkanOlehSIMA.Text)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If

            If TxtNoSIMB.Text <> "" Then
                Dim CmdInsertSIMB As New Data.SqlClient.SqlCommand
                With CmdInsertSIMB
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpID (NIK, JenisID, NoID, PrdAwal, PrdAkhir, DiterbitkanOleh) " & _
                                   "VALUES (@P1, @P2, @P3, @P4, @P5, @P6)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", "SIM B")
                    .Parameters.AddWithValue("@P3", TxtNoSIMB.Text)
                    .Parameters.AddWithValue("@P4", TxtPrdAwalSIMB.Date)
                    .Parameters.AddWithValue("@P5", TxtPrdAkhirSIMB.Date)
                    .Parameters.AddWithValue("@P6", TxtDiterbitkanOlehSIMB.Text)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If

            If TxtNoSIMC.Text <> "" Then
                Dim CmdInsertSIMC As New Data.SqlClient.SqlCommand
                With CmdInsertSIMC
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpID (NIK, JenisID, NoID, PrdAwal, PrdAkhir, DiterbitkanOleh) " & _
                                   "VALUES (@P1, @P2, @P3, @P4, @P5, @P6)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", "SIM C")
                    .Parameters.AddWithValue("@P3", TxtNoSIMC.Text)
                    .Parameters.AddWithValue("@P4", TxtPrdAwalSIMC.Date)
                    .Parameters.AddWithValue("@P5", TxtPrdAkhirSIMC.Date)
                    .Parameters.AddWithValue("@P6", TxtDiterbitkanOlehSIMC.Text)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If

            Dim Counter As Integer = 0

            Dim CmdInsertKeluarga As New Data.SqlClient.SqlCommand
            For Each row As DataRow In TmpDtKeluarga.Rows
                Counter += 1
                CmdInsertKeluarga.Parameters.Clear()
                With CmdInsertKeluarga
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpKeluarga (NIK, NoUrutKeluarga, Hubungan, Nama, " & _
                                   "Kelamin, TglLahir, Pekerjaan, Perusahaan) VALUES (@P1, @P2, @P3, @P4, " & _
                                   "@P5, @P6, @P7, @P8)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", Counter)
                    .Parameters.AddWithValue("@P3", row("HubKeluarga"))
                    .Parameters.AddWithValue("@P4", row("NamaKeluarga"))
                    .Parameters.AddWithValue("@P5", row("JenisKelaminKeluarga"))
                    .Parameters.AddWithValue("@P6", row("TglLahirKeluarga"))
                    .Parameters.AddWithValue("@P7", row("PekerjaanKeluarga"))
                    .Parameters.AddWithValue("@P8", row("PerusahaanKeluarga"))
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            Next row
            Counter = 0

            Dim CmdInsertPendidikan As New Data.SqlClient.SqlCommand
            For Each row As DataRow In TmpDtPendidikan.Rows
                Counter += 1
                CmdInsertPendidikan.Parameters.Clear()
                With CmdInsertPendidikan
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpPendidikan (NIK, NoUrutPendidikan, TgkPendidikan, PrdAwal, PrdAkhir, Institusi, " & _
                                   "Alamat, Jurusan, LlsTdkLls, IPK, NoIjazah) VALUES (@P1, @P2, @P3, @P4, " & _
                                   "@P5, @P6, @P7, @P8, @P9, @P10, @P11)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", Counter)
                    .Parameters.AddWithValue("@P3", row("TgkPendidikan"))
                    .Parameters.AddWithValue("@P4", row("PrdAwalPendidikan"))
                    .Parameters.AddWithValue("@P5", row("PrdAkhirPendidikan"))
                    .Parameters.AddWithValue("@P6", row("InstitusiPendidikan"))
                    .Parameters.AddWithValue("@P7", row("AlamatInstitusiPendidikan"))
                    .Parameters.AddWithValue("@P8", row("JurusanPendidikan"))
                    .Parameters.AddWithValue("@P9", row("LlsTdkLlsPendidikan"))
                    .Parameters.AddWithValue("@P10", row("NilaiPendidikan"))
                    .Parameters.AddWithValue("@P11", row("NoIjazahPendidikan"))
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            Next row
            Counter = 0

            Dim CmdInsertKetrampilan As New Data.SqlClient.SqlCommand
            For Each row As DataRow In TmpDtKetrampilan.Rows
                Counter += 1
                CmdInsertKetrampilan.Parameters.Clear()
                With CmdInsertKetrampilan
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpKetrampilan (NIK, NoUrutKetrampilan, Nama, PrdAwal, PrdAkhir, " & _
                                   "Sertifikat, Grade, NoSertifikat, Institusi) VALUES (@P1, @P2, @P3, " & _
                                   "@P4, @P5, @P6, @P7, @P8, @P9)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", Counter)
                    .Parameters.AddWithValue("@P3", row("NamaKetrampilan"))
                    .Parameters.AddWithValue("@P4", row("PrdAwalKetrampilan"))
                    .Parameters.AddWithValue("@P5", row("PrdAkhirKetrampilan"))
                    .Parameters.AddWithValue("@P6", row("NamaSertifikatKetrampilan"))
                    .Parameters.AddWithValue("@P7", row("GradeKetrampilan"))
                    .Parameters.AddWithValue("@P8", row("NoSertifikatKetrampilan"))
                    .Parameters.AddWithValue("@P9", row("InstitusiKetrampilan"))
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            Next row
            Counter = 0

            Dim CmdInsertRwytPekerjaan As New Data.SqlClient.SqlCommand
            For Each row As DataRow In TmpDtRwytPekerjaan.Rows
                Counter += 1
                CmdInsertRwytPekerjaan.Parameters.Clear()
                With CmdInsertRwytPekerjaan
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpPekerjaanH (NIK, NoUrutRwytPekerjaan, PrdAwal, PrdAkhir, " & _
                                   "Perusahaan, Alamat, Industri, Jabatan, GajiPokok, Tunjangan, " & _
                                   "UraianPekerjaan) VALUES (@P1, @P2, @P3, @P4, @P5, @P6, @P7, " & _
                                   "@P8, @P9, @P10, @P11)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", Counter)
                    .Parameters.AddWithValue("@P3", row("PrdAwalRwytPekerjaan"))
                    .Parameters.AddWithValue("@P4", row("PrdAkhirRwytPekerjaan"))
                    .Parameters.AddWithValue("@P5", row("PerusahaanRwytPekerjaan"))
                    .Parameters.AddWithValue("@P6", row("AlamatRwytPekerjaan"))
                    .Parameters.AddWithValue("@P7", row("IndustriRwytPekerjaan"))
                    .Parameters.AddWithValue("@P8", row("JabatanRwytPekerjaan"))
                    .Parameters.AddWithValue("@P9", row("GajiRwytPekerjaan"))
                    .Parameters.AddWithValue("@P10", row("TunjanganRwytPekerjaan"))
                    .Parameters.AddWithValue("@P11", row("UraianRwytPekerjaan"))
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            Next row
            Counter = 0

            Dim CmdInsertRwytPekerjaanMinarta As New Data.SqlClient.SqlCommand
            For Each row As DataRow In TmpDtRwytPekerjaanMinarta.Rows
                Counter += 1
                CmdInsertRwytPekerjaanMinarta.Parameters.Clear()
                With CmdInsertRwytPekerjaanMinarta
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpPekerjaanMinarta (NIK, NoUrutRwytPekerjaanMinarta, " & _
                                   "PrdAwal, PrdAkhir, Divisi, Subdivisi, LokasiKerja, Jabatan, " & _
                                   "Golongan, Grade, GajiPokok, Tunjangan, KPI, TesKesehatan, HslKesehatan, " & _
                                   "TesPsikologi, HslPsikologi, Atasan, UraianPekerjaan) VALUES " & _
                                   "(@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9, @P10, @P11, @P12, " & _
                                   "@P13, @P14, @P15, @P16, @P17, @P18, @P19)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", Counter)
                    .Parameters.AddWithValue("@P3", row("PrdAwalRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P4", row("PrdAkhirRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P5", row("DivisiRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P6", row("SubdivisiRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P7", row("LokasiKerjaRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P8", row("JabatanRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P9", row("GolonganRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P10", row("GradeRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P11", row("GajiRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P12", row("TunjanganRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P13", row("KPIRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P14", row("TesKesehatanRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P15", row("HasilKesehatanRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P16", row("TesPsikologiRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P17", row("HasilPsikologiRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P18", row("AtasanRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P19", row("UraianRwytPekerjaanMinarta"))
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            Next row
            Counter = 0

            LblErr.Text = "Data berhasil dimasukkan"
            ErrMsg.ShowOnPageLoad = True
        Else
            'MENYIMPAN FILEPATH DOKUMEN
            Dim CmdDokumenReader As New Data.SqlClient.SqlCommand
            With CmdDokumenReader
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT ScanKTP, ScanKK, ScanNPWP, ScanIjazah, ScanTranskripNilai, ScanSertifikat " & _
                               "FROM Karyawan where NIK=@P1"
                .Parameters.AddWithValue("@P1", UpdateNIK)
            End With
            Dim DokumenReader As Data.SqlClient.SqlDataReader = CmdDokumenReader.ExecuteReader
            If DokumenReader.Read Then
                Session("KTP") = DokumenReader("ScanKTP")
                Session("KK") = DokumenReader("ScanKK")
                Session("NPWP") = DokumenReader("ScanNPWP")
                Session("Ijazah") = DokumenReader("ScanIjazah")
                Session("TranskripNilai") = DokumenReader("ScanTranskripNilai")
                Session("Sertifikat") = DokumenReader("ScanSertifikat")
            End If
            CmdDokumenReader.Dispose()
            DokumenReader.Close()

            Dim CmdDeleteHdr As New Data.SqlClient.SqlCommand
            If PasFoto.HasFile And PasFoto.PostedFile.ContentType.ToLower <> "image/jpeg" Then
                LblErr.Text = "Hanya mendukung image dengan ext. JPG/JPEG."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            With CmdDeleteHdr
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "DELETE Karyawan WHERE NIK=@P1"
                .Parameters.AddWithValue("@P1", UpdateNIK)
                .ExecuteNonQuery()
                .Dispose()
            End With

            Dim CmdDeleteID As New Data.SqlClient.SqlCommand
            With CmdDeleteID
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "DELETE EmpID WHERE NIK=@P1"
                .Parameters.AddWithValue("@P1", UpdateNIK)
                .ExecuteNonQuery()
                .Dispose()
            End With

            Dim CmdDeleteKeluarga As New Data.SqlClient.SqlCommand
            With CmdDeleteKeluarga
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "DELETE EmpKeluarga WHERE NIK=@P1"
                .Parameters.AddWithValue("@P1", UpdateNIK)
                .ExecuteNonQuery()
                .Dispose()
            End With

            Dim CmdDeletePendidikan As New Data.SqlClient.SqlCommand
            With CmdDeletePendidikan
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "DELETE EmpPendidikan WHERE NIK=@P1"
                .Parameters.AddWithValue("@P1", UpdateNIK)
                .ExecuteNonQuery()
                .Dispose()
            End With

            Dim CmdDeleteKetrampilan As New Data.SqlClient.SqlCommand
            With CmdDeleteKetrampilan
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "DELETE EmpKetrampilan WHERE NIK=@P1"
                .Parameters.AddWithValue("@P1", UpdateNIK)
                .ExecuteNonQuery()
                .Dispose()
            End With

            Dim CmdDeleteRwytPekerjaan As New Data.SqlClient.SqlCommand
            With CmdDeleteRwytPekerjaan
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "DELETE EmpPekerjaanH WHERE NIK=@P1"
                .Parameters.AddWithValue("@P1", UpdateNIK)
                .ExecuteNonQuery()
                .Dispose()
            End With

            Dim CmdDeleteRwytPekerjaanMinarta As New Data.SqlClient.SqlCommand
            With CmdDeleteRwytPekerjaanMinarta
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "DELETE EmpPekerjaanMinarta WHERE NIK=@P1"
                .Parameters.AddWithValue("@P1", UpdateNIK)
                .ExecuteNonQuery()
                .Dispose()
            End With

            Dim CmdInsertHdr As New Data.SqlClient.SqlCommand
            If PasFoto.HasFile And PasFoto.PostedFile.ContentType.ToLower <> "image/jpeg" Then
                LblErr.Text = "Pas Foto hanya mendukung file dengan ext. JPG/JPEG."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            If FileKTP.HasFile And (FileKTP.PostedFile.ContentType.ToLower <> "image/jpeg" Or _
                                    FileKTP.PostedFile.ContentType.ToLower <> "application/pdf") Then
                LblErr.Text = "KTP hanya mendukung file dengan ext. JPG/JPEG/PDF."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            If FileKK.HasFile And (FileKK.PostedFile.ContentType.ToLower <> "image/jpeg" Or _
                                   FileKK.PostedFile.ContentType.ToLower <> "application/pdf") Then
                LblErr.Text = "Kartu Keluarga hanya mendukung file dengan ext. JPG/JPEG/PDF."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            If FileNPWP.HasFile And (FileNPWP.PostedFile.ContentType.ToLower <> "image/jpeg" Or _
                                     FileNPWP.PostedFile.ContentType.ToLower <> "application/pdf") Then
                LblErr.Text = "NPWP hanya mendukung file dengan ext. JPG/JPEG/PDF."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            If FileIjazah.HasFile And (FileIjazah.PostedFile.ContentType.ToLower <> "image/jpeg" Or _
                                       FileIjazah.PostedFile.ContentType.ToLower <> "application/pdf") Then
                LblErr.Text = "Ijazah hanya mendukung file dengan ext. JPG/JPEG/PDF."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            If FileTranskripNilai.HasFile And (FileTranskripNilai.PostedFile.ContentType.ToLower <> "image/jpeg" Or _
                                               FileTranskripNilai.PostedFile.ContentType.ToLower <> "application/pdf") Then
                LblErr.Text = "Transkrip Nilai hanya mendukung file dengan ext. JPG/JPEG/PDF."
                ErrMsg.ShowOnPageLoad = True
                Exit Sub
            End If
            With CmdInsertHdr
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Karyawan (NIK, Nama, Kelamin, TmpLahir, TglLahir, WN, StsNikah, TglNikah, " & _
                               "Agama, Alamat, Provinsi, Kota, AlamatSurat, Email, NoTelp, LokasiKerja, Divisi, " & _
                               "Subdivisi, Jabatan, Golongan, Grade, PrdAwal, UraianPekerjaan, Foto, ScanKTP, " & _
                               "ScanKK, ScanNPWP, ScanIjazah, ScanTranskripNilai, ScanSertifikat) VALUES (@P1, " & _
                               "@P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9, @P10, @P11, @P12, @P13, @P14, @P15, @P16, " & _
                               "@P17, @P18, @P19, @P20, @P21, @P22, @P23, @P24, @P25, @P26, @P27, @P28, @P29, @P30)"
                .Parameters.AddWithValue("@P1", TxtNIK.Text)
                .Parameters.AddWithValue("@P2", TxtNama.Text)
                .Parameters.AddWithValue("@P3", RblJenisKelamin.Value)
                .Parameters.AddWithValue("@P4", TxtTempatLahir.Text)
                .Parameters.AddWithValue("@P5", TxtTanggalLahir.Date)
                .Parameters.AddWithValue("@P6", TxtWN.Text)
                .Parameters.AddWithValue("@P7", If(RblStsNikah.Value = "Lajang", "Lajang", If(RblStsNikah.Value = "Janda/Duda", "Janda/Duda", "Menikah")))
                .Parameters.AddWithValue("@P8", If(RblStsNikah.Value = "Menikah", TxtTglNikah.Date, DBNull.Value))
                .Parameters.AddWithValue("@P9", RblAgama.Value)
                .Parameters.AddWithValue("@P10", TxtAlamat.Text)
                .Parameters.AddWithValue("@P11", TxtProvinsi.Text)
                .Parameters.AddWithValue("@P12", TxtKota.Text)
                .Parameters.AddWithValue("@P13", TxtAlamatSurat.Text)
                .Parameters.AddWithValue("@P14", TxtEmail.Text)
                .Parameters.AddWithValue("@P15", TxtNoTelp.Text)
                .Parameters.AddWithValue("@P16", TxtLokasiKerja.Text)
                .Parameters.AddWithValue("@P17", TxtDivisi.Text)
                .Parameters.AddWithValue("@P18", TxtSubdivisi.Text)
                .Parameters.AddWithValue("@P19", TxtJabatan.Text)
                .Parameters.AddWithValue("@P20", TxtGolongan.Text)
                .Parameters.AddWithValue("@P21", TxtGrade.Text)
                .Parameters.AddWithValue("@P22", TxtPrdAwal.Date)
                .Parameters.AddWithValue("@P23", TxtUraian.Text)
                Dim FolderPath As String = Server.MapPath("/PDF/Employee/" + TxtNIK.Text)
                If Not Directory.Exists(FolderPath) Then
                    Directory.CreateDirectory(FolderPath)
                End If
                If PasFoto.HasFile And PasFoto.PostedFile.ContentLength < 2000000 Then
                    Dim FileName As String = TxtNIK.Text & "PasFoto.jpg"
                    PasFoto.PostedFile.SaveAs(Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileName))
                    .Parameters.AddWithValue("@P24", "/PDF/Employee/" + TxtNIK.Text + "/" + FileName)
                ElseIf PasFoto.HasFile And PasFoto.PostedFile.ContentLength >= 2000000 Then
                    LblErr.Text = "Ukuran file foto maximum 2MB"
                    ErrMsg.ShowOnPageLoad = True
                    Exit Sub
                Else
                    .Parameters.AddWithValue("@P24", Session("PasFoto"))
                End If
                If FileKTP.HasFile Then
                    Dim FileNameKTP As String = TxtNIK.Text & "KTP." & _
                                                If(FileKTP.PostedFile.ContentType.ToLower = "jpg", "jpg", "pdf")
                    FileKTP.PostedFile.SaveAs(Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileNameKTP))
                    .Parameters.AddWithValue("@P25", "/PDF/Employee/" + TxtNIK.Text + "/" + FileNameKTP)
                Else
                    .Parameters.AddWithValue("@P25", Session("KTP"))
                End If
                If FileKK.HasFile Then
                    Dim FileNameKK As String = TxtNIK.Text & "KK." & _
                                               If(FileKK.PostedFile.ContentType.ToLower = "jpg", "jpg", "pdf")
                    FileKK.PostedFile.SaveAs(Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileNameKK))
                    .Parameters.AddWithValue("@P26", "/PDF/Employee/" + TxtNIK.Text + "/" + FileNameKK)
                Else
                    .Parameters.AddWithValue("@P26", Session("KK"))
                End If
                If FileNPWP.HasFile Then
                    Dim FileNameNPWP As String = TxtNIK.Text & "NPWP." & _
                                                 If(FileNPWP.PostedFile.ContentType.ToLower = "jpg", "jpg", "pdf")
                    FileNPWP.PostedFile.SaveAs(Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileNameNPWP))
                    .Parameters.AddWithValue("@P27", "/PDF/Employee/" + TxtNIK.Text + "/" + FileNameNPWP)
                Else
                    .Parameters.AddWithValue("@P27", Session("NPWP"))
                End If
                If FileIjazah.HasFile Then
                    Dim FileNameIjazah As String = TxtNIK.Text & "Ijazah." & _
                                                   If(FileIjazah.PostedFile.ContentType.ToLower = "jpg", "jpg", "pdf")
                    FileIjazah.PostedFile.SaveAs(Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileNameIjazah))
                    .Parameters.AddWithValue("@P28", "/PDF/Employee/" + TxtNIK.Text + "/" + FileNameIjazah)
                Else
                    .Parameters.AddWithValue("@P28", Session("Ijazah"))
                End If
                If FileTranskripNilai.HasFile Then
                    Dim FileNameTranskripNilai As String = TxtNIK.Text & "Transkrip." & _
                                                           If(FileTranskripNilai.PostedFile.ContentType.ToLower = "jpg", "jpg", "pdf")
                    FileTranskripNilai.PostedFile.SaveAs(Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileNameTranskripNilai))
                    .Parameters.AddWithValue("@P29", "/PDF/Employee/" + TxtNIK.Text + "/" + FileNameTranskripNilai)
                Else
                    .Parameters.AddWithValue("@P29", Session("TranskripNilai"))
                End If
                If FileSertifikat.HasFile Then
                    Dim FileFileSertifikat As HttpFileCollection = Request.Files
                    Dim FilePathSertifikatDiServer As String
                    Dim Ctr As Integer = 0
                    For Each key As String In FileFileSertifikat.Keys
                        Ctr += 1
                        Dim FileSertifikatSatuan As HttpPostedFile = FileFileSertifikat(key)
                        Dim FileNameSertifikat As String = TxtNIK.Text & "Sertifikat[" & Ctr & "].jpg"
                        FileSertifikatSatuan.SaveAs(Server.MapPath("/PDF/Employee/" + TxtNIK.Text + "/" + FileNameSertifikat))
                        If Ctr > 1 Then
                            FilePathSertifikatDiServer = FilePathSertifikatDiServer + "|/PDF/Employee/" + TxtNIK.Text + "/" + FileNameSertifikat
                        Else
                            FilePathSertifikatDiServer = "/PDF/Employee/" + TxtNIK.Text + "/" + FileNameSertifikat
                        End If
                    Next
                    Ctr = 0
                    .Parameters.AddWithValue("@P30", FilePathSertifikatDiServer)
                Else
                    .Parameters.AddWithValue("@P30", Session("Sertifikat"))
                End If
                .ExecuteNonQuery()
                .Dispose()
            End With

            If TxtNoKTP.Text <> "" Then
                Dim CmdInsertKTP As New Data.SqlClient.SqlCommand
                With CmdInsertKTP
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpID (NIK, JenisID, NoID, PrdAwal, PrdAkhir, DiterbitkanOleh) " & _
                                   "VALUES (@P1, @P2, @P3, @P4, @P5, @P6)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", "KTP")
                    .Parameters.AddWithValue("@P3", TxtNoKTP.Text)
                    .Parameters.AddWithValue("@P4", TxtPrdAwalKTP.Date)
                    .Parameters.AddWithValue("@P5", TxtPrdAkhirKTP.Date)
                    .Parameters.AddWithValue("@P6", TxtDiterbitkanOlehKTP.Text)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If

            If TxtNoPassport.Text <> "" Then
                Dim CmdInsertPassport As New Data.SqlClient.SqlCommand
                With CmdInsertPassport
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpID (NIK, JenisID, NoID, PrdAwal, PrdAkhir, DiterbitkanOleh) " & _
                                   "VALUES (@P1, @P2, @P3, @P4, @P5, @P6)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", "Passport")
                    .Parameters.AddWithValue("@P3", TxtNoPassport.Text)
                    .Parameters.AddWithValue("@P4", TxtPrdAwalPassport.Date)
                    .Parameters.AddWithValue("@P5", TxtPrdAkhirPassport.Date)
                    .Parameters.AddWithValue("@P6", TxtDiterbitkanOlehPassport.Text)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If

            If TxtNoNPWP.Text <> "" Then
                Dim CmdInsertNPWP As New Data.SqlClient.SqlCommand
                With CmdInsertNPWP
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpID (NIK, JenisID, NoID, PrdAwal, PrdAkhir, DiterbitkanOleh) " & _
                                   "VALUES (@P1, @P2, @P3, @P4, @P5, @P6)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", "NPWP")
                    .Parameters.AddWithValue("@P3", TxtNoNPWP.Text)
                    .Parameters.AddWithValue("@P4", TxtPrdAwalNPWP.Date)
                    .Parameters.AddWithValue("@P5", TxtPrdAkhirNPWP.Date)
                    .Parameters.AddWithValue("@P6", TxtDiterbitkanOlehNPWP.Text)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If

            If TxtNoNPWP.Text <> "" Then
                Dim CmdInsertNPWP As New Data.SqlClient.SqlCommand
                With CmdInsertNPWP
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpID (NIK, JenisID, NoID, PrdAwal, PrdAkhir, DiterbitkanOleh) " & _
                                   "VALUES (@P1, @P2, @P3, @P4, @P5, @P6)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", "KK")
                    .Parameters.AddWithValue("@P3", TxtNoKK.Text)
                    .Parameters.AddWithValue("@P4", TxtPrdAwalKK.Date)
                    .Parameters.AddWithValue("@P5", TxtPrdAkhirKK.Date)
                    .Parameters.AddWithValue("@P6", TxtDiterbitkanOlehKK.Text)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If

            If TxtNoSIMA.Text <> "" Then
                Dim CmdInsertSIMA As New Data.SqlClient.SqlCommand
                With CmdInsertSIMA
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpID (NIK, JenisID, NoID, PrdAwal, PrdAkhir, DiterbitkanOleh) " & _
                                   "VALUES (@P1, @P2, @P3, @P4, @P5, @P6)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", "SIM A")
                    .Parameters.AddWithValue("@P3", TxtNoSIMA.Text)
                    .Parameters.AddWithValue("@P4", TxtPrdAwalSIMA.Date)
                    .Parameters.AddWithValue("@P5", TxtPrdAkhirSIMA.Date)
                    .Parameters.AddWithValue("@P6", TxtDiterbitkanOlehSIMA.Text)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If

            If TxtNoSIMB.Text <> "" Then
                Dim CmdInsertSIMB As New Data.SqlClient.SqlCommand
                With CmdInsertSIMB
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpID (NIK, JenisID, NoID, PrdAwal, PrdAkhir, DiterbitkanOleh) " & _
                                   "VALUES (@P1, @P2, @P3, @P4, @P5, @P6)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", "SIM B")
                    .Parameters.AddWithValue("@P3", TxtNoSIMB.Text)
                    .Parameters.AddWithValue("@P4", TxtPrdAwalSIMB.Date)
                    .Parameters.AddWithValue("@P5", TxtPrdAkhirSIMB.Date)
                    .Parameters.AddWithValue("@P6", TxtDiterbitkanOlehSIMB.Text)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If

            If TxtNoSIMC.Text <> "" Then
                Dim CmdInsertSIMC As New Data.SqlClient.SqlCommand
                With CmdInsertSIMC
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpID (NIK, JenisID, NoID, PrdAwal, PrdAkhir, DiterbitkanOleh) " & _
                                   "VALUES (@P1, @P2, @P3, @P4, @P5, @P6)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", "SIM C")
                    .Parameters.AddWithValue("@P3", TxtNoSIMC.Text)
                    .Parameters.AddWithValue("@P4", TxtPrdAwalSIMC.Date)
                    .Parameters.AddWithValue("@P5", TxtPrdAkhirSIMC.Date)
                    .Parameters.AddWithValue("@P6", TxtDiterbitkanOlehSIMC.Text)
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            End If

            Dim Counter As Integer = 0

            Dim CmdInsertKeluarga As New Data.SqlClient.SqlCommand
            For Each row As DataRow In TmpDtKeluarga.Rows
                Counter += 1
                CmdInsertKeluarga.Parameters.Clear()
                With CmdInsertKeluarga
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpKeluarga (NIK, NoUrutKeluarga, Hubungan, Nama, " & _
                                   "Kelamin, TglLahir, Pekerjaan, Perusahaan) VALUES (@P1, @P2, @P3, @P4, " & _
                                   "@P5, @P6, @P7, @P8)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", Counter)
                    .Parameters.AddWithValue("@P3", row("HubKeluarga"))
                    .Parameters.AddWithValue("@P4", row("NamaKeluarga"))
                    .Parameters.AddWithValue("@P5", row("JenisKelaminKeluarga"))
                    .Parameters.AddWithValue("@P6", row("TglLahirKeluarga"))
                    .Parameters.AddWithValue("@P7", row("PekerjaanKeluarga"))
                    .Parameters.AddWithValue("@P8", row("PerusahaanKeluarga"))
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            Next row
            Counter = 0

            Dim CmdInsertPendidikan As New Data.SqlClient.SqlCommand
            For Each row As DataRow In TmpDtPendidikan.Rows
                Counter += 1
                CmdInsertPendidikan.Parameters.Clear()
                With CmdInsertPendidikan
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpPendidikan (NIK, NoUrutPendidikan, TgkPendidikan, PrdAwal, PrdAkhir, Institusi, " & _
                                   "Alamat, Jurusan, LlsTdkLls, IPK, NoIjazah) VALUES (@P1, @P2, @P3, @P4, " & _
                                   "@P5, @P6, @P7, @P8, @P9, @P10, @P11)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", Counter)
                    .Parameters.AddWithValue("@P3", row("TgkPendidikan"))
                    .Parameters.AddWithValue("@P4", row("PrdAwalPendidikan"))
                    .Parameters.AddWithValue("@P5", row("PrdAkhirPendidikan"))
                    .Parameters.AddWithValue("@P6", row("InstitusiPendidikan"))
                    .Parameters.AddWithValue("@P7", row("AlamatInstitusiPendidikan"))
                    .Parameters.AddWithValue("@P8", row("JurusanPendidikan"))
                    .Parameters.AddWithValue("@P9", row("LlsTdkLlsPendidikan"))
                    .Parameters.AddWithValue("@P10", row("NilaiPendidikan"))
                    .Parameters.AddWithValue("@P11", row("NoIjazahPendidikan"))
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            Next row
            Counter = 0

            Dim CmdInsertKetrampilan As New Data.SqlClient.SqlCommand
            For Each row As DataRow In TmpDtKetrampilan.Rows
                Counter += 1
                CmdInsertKetrampilan.Parameters.Clear()
                With CmdInsertKetrampilan
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpKetrampilan (NIK, NoUrutKetrampilan, Nama, PrdAwal, PrdAkhir, " & _
                                   "Sertifikat, Grade, NoSertifikat, Institusi) VALUES (@P1, @P2, @P3, " & _
                                   "@P4, @P5, @P6, @P7, @P8, @P9)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", Counter)
                    .Parameters.AddWithValue("@P3", row("NamaKetrampilan"))
                    .Parameters.AddWithValue("@P4", row("PrdAwalKetrampilan"))
                    .Parameters.AddWithValue("@P5", row("PrdAkhirKetrampilan"))
                    .Parameters.AddWithValue("@P6", row("NamaSertifikatKetrampilan"))
                    .Parameters.AddWithValue("@P7", row("GradeKetrampilan"))
                    .Parameters.AddWithValue("@P8", row("NoSertifikatKetrampilan"))
                    .Parameters.AddWithValue("@P9", row("InstitusiKetrampilan"))
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            Next row
            Counter = 0

            Dim CmdInsertRwytPekerjaan As New Data.SqlClient.SqlCommand
            For Each row As DataRow In TmpDtRwytPekerjaan.Rows
                Counter += 1
                CmdInsertRwytPekerjaan.Parameters.Clear()
                With CmdInsertRwytPekerjaan
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpPekerjaanH (NIK, NoUrutRwytPekerjaan, PrdAwal, PrdAkhir, " & _
                                   "Perusahaan, Alamat, Industri, Jabatan, GajiPokok, Tunjangan, " & _
                                   "UraianPekerjaan) VALUES (@P1, @P2, @P3, @P4, @P5, @P6, @P7, " & _
                                   "@P8, @P9, @P10, @P11)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", Counter)
                    .Parameters.AddWithValue("@P3", row("PrdAwalRwytPekerjaan"))
                    .Parameters.AddWithValue("@P4", row("PrdAkhirRwytPekerjaan"))
                    .Parameters.AddWithValue("@P5", row("PerusahaanRwytPekerjaan"))
                    .Parameters.AddWithValue("@P6", row("AlamatRwytPekerjaan"))
                    .Parameters.AddWithValue("@P7", row("IndustriRwytPekerjaan"))
                    .Parameters.AddWithValue("@P8", row("JabatanRwytPekerjaan"))
                    .Parameters.AddWithValue("@P9", row("GajiRwytPekerjaan"))
                    .Parameters.AddWithValue("@P10", row("TunjanganRwytPekerjaan"))
                    .Parameters.AddWithValue("@P11", row("UraianRwytPekerjaan"))
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            Next row
            Counter = 0

            Dim CmdInsertRwytPekerjaanMinarta As New Data.SqlClient.SqlCommand
            For Each row As DataRow In TmpDtRwytPekerjaanMinarta.Rows
                Counter += 1
                CmdInsertRwytPekerjaanMinarta.Parameters.Clear()
                With CmdInsertRwytPekerjaanMinarta
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO EmpPekerjaanMinarta (NIK, NoUrutRwytPekerjaanMinarta, " & _
                                   "PrdAwal, PrdAkhir, Divisi, Subdivisi, LokasiKerja, Jabatan, " & _
                                   "Golongan, Grade, GajiPokok, Tunjangan, KPI, TesKesehatan, HslKesehatan, " & _
                                   "TesPsikologi, HslPsikologi, Atasan, UraianPekerjaan) VALUES " & _
                                   "(@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9, @P10, @P11, @P12, " & _
                                   "@P13, @P14, @P15, @P16, @P17, @P18, @P19)"
                    .Parameters.AddWithValue("@P1", TxtNIK.Text)
                    .Parameters.AddWithValue("@P2", Counter)
                    .Parameters.AddWithValue("@P3", row("PrdAwalRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P4", row("PrdAkhirRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P5", row("DivisiRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P6", row("SubdivisiRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P7", row("LokasiKerjaRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P8", row("JabatanRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P9", row("GolonganRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P10", row("GradeRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P11", row("GajiRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P12", row("TunjanganRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P13", row("KPIRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P14", row("TesKesehatanRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P15", row("HasilKesehatanRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P16", row("TesPsikologiRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P17", row("HasilPsikologiRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P18", row("AtasanRwytPekerjaanMinarta"))
                    .Parameters.AddWithValue("@P19", row("UraianRwytPekerjaanMinarta"))
                    .ExecuteNonQuery()
                    .Dispose()
                End With
            Next row
            Counter = 0
        End If
        BtnCancelDataEntry_Click(BtnCancelDataEntry, New EventArgs())
    End Sub
    Private Sub GridDataKeluarga_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDataKeluarga.RowCommand
        If e.CommandName = "BtnUpdKeluarga" Then
            Dim SelectRecord As GridViewRow = GridDataKeluarga.Rows(e.CommandArgument)

            TxtActKeluarga.Text = "UPD"
            TxtNoUrutKeluarga.Text = SelectRecord.Cells(0).Text
            TxtHubKeluarga.Value = SelectRecord.Cells(1).Text
            TxtNamaKeluarga.Text = SelectRecord.Cells(2).Text
            TxtJenisKelaminKeluarga.Value = SelectRecord.Cells(3).Text
            TxtTglLahirKeluarga.Text = SelectRecord.Cells(4).Text
            TxtPekerjaanKeluarga.Text = If(SelectRecord.Cells(5).Text = "&nbsp;", String.Empty, SelectRecord.Cells(5).Text)
            TxtPerusahaanKeluarga.Text = If(SelectRecord.Cells(6).Text = "&nbsp;", String.Empty, SelectRecord.Cells(6).Text)
            PopEntKeluarga.ShowOnPageLoad = True
        ElseIf e.CommandName = "BtnDelKeluarga" Then
            Dim SelectRecord As GridViewRow = GridDataKeluarga.Rows(e.CommandArgument)

            TmpDtKeluarga = Session("TmpDtKeluarga")
            TmpDtKeluarga.Rows(e.CommandArgument).Delete()
            TmpDtKeluarga.AcceptChanges()

            GridDataKeluarga.DataSource = TmpDtKeluarga
            GridDataKeluarga.DataBind()
            Session("TmpDtKeluarga") = TmpDtKeluarga
        End If
    End Sub
    Private Sub GridDataPendidikan_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDataPendidikan.RowCommand
        If e.CommandName = "BtnUpdPendidikan" Then
            Dim SelectRecord As GridViewRow = GridDataPendidikan.Rows(e.CommandArgument)

            TxtActPendidikan.Text = "UPD"
            TxtNoUrutPendidikan.Text = SelectRecord.Cells(0).Text
            TxtTgkPendidikan.Value = SelectRecord.Cells(1).Text
            TxtPrdAwalPendidikan.Text = SelectRecord.Cells(2).Text
            TxtPrdAkhirPendidikan.Value = SelectRecord.Cells(3).Text
            TxtInstitusiPendidikan.Text = If(SelectRecord.Cells(4).Text = "&nbsp;", String.Empty, SelectRecord.Cells(4).Text)
            TxtAlamatInstitusiPendidikan.Text = If(SelectRecord.Cells(5).Text = "&nbsp;", String.Empty, SelectRecord.Cells(5).Text)
            TxtJurusanPendidikan.Text = If(SelectRecord.Cells(6).Text = "&nbsp;", String.Empty, SelectRecord.Cells(6).Text)
            TxtLlsTdkLlsPendidikan.Value = SelectRecord.Cells(7).Text
            TxtNilaiPendidikan.Text = If(SelectRecord.Cells(8).Text = "&nbsp;", String.Empty, SelectRecord.Cells(8).Text)
            TxtNoIjazahPendidikan.Text = If(SelectRecord.Cells(9).Text = "&nbsp;", String.Empty, SelectRecord.Cells(9).Text)
            PopEntPendidikan.ShowOnPageLoad = True
        ElseIf e.CommandName = "BtnDelPendidikan" Then
            Dim SelectRecord As GridViewRow = GridDataPendidikan.Rows(e.CommandArgument)

            TmpDtPendidikan = Session("TmpDtPendidikan")
            TmpDtPendidikan.Rows(e.CommandArgument).Delete()
            TmpDtPendidikan.AcceptChanges()

            GridDataPendidikan.DataSource = TmpDtPendidikan
            GridDataPendidikan.DataBind()
            Session("TmpDtPendidikan") = TmpDtPendidikan
        End If
    End Sub
    Private Sub GridDataKetrampilan_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDataKetrampilan.RowCommand
        If e.CommandName = "BtnUpdKetrampilan" Then
            Dim SelectRecord As GridViewRow = GridDataKetrampilan.Rows(e.CommandArgument)

            TxtActKetrampilan.Text = "UPD"
            TxtNoUrutKetrampilan.Text = SelectRecord.Cells(0).Text
            TxtNamaKetrampilan.Value = SelectRecord.Cells(1).Text
            TxtPrdAwalKetrampilan.Text = SelectRecord.Cells(2).Text
            TxtPrdAkhirKetrampilan.Value = SelectRecord.Cells(3).Text
            TxtNamaSertifikatKetrampilan.Text = SelectRecord.Cells(4).Text
            TxtGradeKetrampilan.Text = If(SelectRecord.Cells(5).Text = "&nbsp;", String.Empty, SelectRecord.Cells(5).Text)
            TxtNoSertifikatKetrampilan.Text = If(SelectRecord.Cells(6).Text = "&nbsp;", String.Empty, SelectRecord.Cells(6).Text)
            TxtInstitusiKetrampilan.Text = If(SelectRecord.Cells(7).Text = "&nbsp;", String.Empty, SelectRecord.Cells(7).Text)
            PopEntKetrampilan.ShowOnPageLoad = True
        ElseIf e.CommandName = "BtnDelKetrampilan" Then
            Dim SelectRecord As GridViewRow = GridDataKetrampilan.Rows(e.CommandArgument)

            TmpDtKetrampilan = Session("TmpDtKetrampilan")
            TmpDtKetrampilan.Rows(e.CommandArgument).Delete()
            TmpDtKetrampilan.AcceptChanges()

            GridDataKetrampilan.DataSource = TmpDtKetrampilan
            GridDataKetrampilan.DataBind()
            Session("TmpDtKetrampilan") = TmpDtKetrampilan
        End If
    End Sub
    Private Sub GridDataRwytPekerjaan_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDataRwytPekerjaan.RowCommand
        If e.CommandName = "BtnUpdRwytPekerjaan" Then
            Dim SelectRecord As GridViewRow = GridDataRwytPekerjaan.Rows(e.CommandArgument)

            TxtActRwytPekerjaan.Text = "UPD"
            TxtNoUrutRwytPekerjaan.Text = SelectRecord.Cells(0).Text
            TxtPrdAwalRwytPekerjaan.Value = SelectRecord.Cells(1).Text
            TxtPrdAkhirRwytPekerjaan.Text = SelectRecord.Cells(2).Text
            TxtPerusahaanRwytPekerjaan.Value = If(SelectRecord.Cells(3).Text = "&nbsp;", String.Empty, SelectRecord.Cells(3).Text)
            TxtAlamatRwytPekerjaan.Text = If(SelectRecord.Cells(4).Text = "&nbsp;", String.Empty, SelectRecord.Cells(4).Text)
            TxtIndustriRwytPekerjaan.Text = If(SelectRecord.Cells(5).Text = "&nbsp;", String.Empty, SelectRecord.Cells(5).Text)
            TxtJabatanRwytPekerjaan.Text = If(SelectRecord.Cells(6).Text = "&nbsp;", String.Empty, SelectRecord.Cells(6).Text)
            TxtLokasiKerjaRwytPekerjaan.Text = If(SelectRecord.Cells(7).Text = "&nbsp;", String.Empty, SelectRecord.Cells(7).Text)
            TxtGajiRwytPekerjaan.Text = If(SelectRecord.Cells(8).Text = "&nbsp;", String.Empty, SelectRecord.Cells(8).Text)
            TxtTunjanganRwytPekerjaan.Text = If(SelectRecord.Cells(9).Text = "&nbsp;", String.Empty, SelectRecord.Cells(9).Text)
            TxtUraianRwytPekerjaan.Text = If(SelectRecord.Cells(10).Text = "&nbsp;", String.Empty, SelectRecord.Cells(10).Text)
            PopEntRwytPekerjaan.ShowOnPageLoad = True
        ElseIf e.CommandName = "BtnDelRwytPekerjaan" Then
            Dim SelectRecord As GridViewRow = GridDataRwytPekerjaan.Rows(e.CommandArgument)

            TmpDtRwytPekerjaan = Session("TmpDtRwytPekerjaan")
            TmpDtRwytPekerjaan.Rows(e.CommandArgument).Delete()
            TmpDtRwytPekerjaan.AcceptChanges()

            GridDataRwytPekerjaan.DataSource = TmpDtRwytPekerjaan
            GridDataRwytPekerjaan.DataBind()
            Session("TmpDtRwytPekerjaan") = TmpDtRwytPekerjaan
        End If
    End Sub
    Private Sub GridDataRwytPekerjaanMinarta_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridDataRwytPekerjaanMinarta.RowCommand
        If e.CommandName = "BtnUpdRwytPekerjaanMinarta" Then
            Dim SelectRecord As GridViewRow = GridDataRwytPekerjaanMinarta.Rows(e.CommandArgument)

            TxtActRwytPekerjaanMinarta.Text = "UPD"
            TxtNoUrutRwytPekerjaanMinarta.Text = SelectRecord.Cells(0).Text
            TxtPrdAwalRwytPekerjaanMinarta.Value = SelectRecord.Cells(1).Text
            TxtPrdAkhirRwytPekerjaanMinarta.Text = SelectRecord.Cells(2).Text
            TxtDivisiRwytPekerjaanMinarta.Value = If(SelectRecord.Cells(3).Text = "&nbsp;", String.Empty, SelectRecord.Cells(3).Text)
            TxtSubdivisiRwytPekerjaanMinarta.Text = If(SelectRecord.Cells(4).Text = "&nbsp;", String.Empty, SelectRecord.Cells(4).Text)
            TxtLokasiKerjaRwytPekerjaanMinarta.Text = If(SelectRecord.Cells(5).Text = "&nbsp;", String.Empty, SelectRecord.Cells(5).Text)
            TxtJabatanRwytPekerjaanMinarta.Text = If(SelectRecord.Cells(6).Text = "&nbsp;", String.Empty, SelectRecord.Cells(6).Text)
            TxtGolonganRwytPekerjaanMinarta.Text = If(SelectRecord.Cells(7).Text = "&nbsp;", String.Empty, SelectRecord.Cells(7).Text)
            TxtGradeRwytPekerjaanMinarta.Text = If(SelectRecord.Cells(8).Text = "&nbsp;", String.Empty, SelectRecord.Cells(8).Text)
            TxtGajiRwytPekerjaanMinarta.Text = If(SelectRecord.Cells(9).Text = "&nbsp;", String.Empty, SelectRecord.Cells(9).Text)
            TxtTunjanganRwytPekerjaanMinarta.Text = If(SelectRecord.Cells(10).Text = "&nbsp;", String.Empty, SelectRecord.Cells(10).Text)
            TxtKPIRwytPekerjaanMinarta.Text = If(SelectRecord.Cells(11).Text = "&nbsp;", String.Empty, SelectRecord.Cells(11).Text)
            TxtTesKesehatanRwytPekerjaanMinarta.Text = SelectRecord.Cells(12).Text
            TxtHasilKesehatanRwytPekerjaanMinarta.Text = If(SelectRecord.Cells(13).Text = "&nbsp;", String.Empty, SelectRecord.Cells(13).Text)
            TxtTesPsikologiRwytPekerjaanMinarta.Text = SelectRecord.Cells(14).Text
            TxtHasilPsikologiRwytPekerjaanMinarta.Text = If(SelectRecord.Cells(15).Text = "&nbsp;", String.Empty, SelectRecord.Cells(15).Text)
            TxtAtasanRwytPekerjaanMinarta.Text = If(SelectRecord.Cells(16).Text = "&nbsp;", String.Empty, SelectRecord.Cells(16).Text)
            TxtUraianRwytPekerjaanMinarta.Text = If(SelectRecord.Cells(17).Text = "&nbsp;", String.Empty, SelectRecord.Cells(17).Text)
            PopEntRwytPekerjaanMinarta.ShowOnPageLoad = True
        ElseIf e.CommandName = "BtnDelRwytPekerjaanMinarta" Then
            Dim SelectRecord As GridViewRow = GridDataRwytPekerjaanMinarta.Rows(e.CommandArgument)

            TmpDtRwytPekerjaanMinarta = Session("TmpDtRwytPekerjaanMinarta")
            TmpDtRwytPekerjaanMinarta.Rows(e.CommandArgument).Delete()
            TmpDtRwytPekerjaanMinarta.AcceptChanges()

            GridDataRwytPekerjaanMinarta.DataSource = TmpDtRwytPekerjaanMinarta
            GridDataRwytPekerjaanMinarta.DataBind()
            Session("TmpDtRwytPekerjaanMinarta") = TmpDtRwytPekerjaanMinarta
        End If
    End Sub

    Protected Sub BtnCancelDataEntry_Click(sender As Object, e As EventArgs) Handles BtnCancelDataEntry.Click
        Session.Remove("Karyawan")
        Session.Remove("NIK")
        Session.Remove("Nama")
        Session.Remove("PasFoto")
        Session.Remove("KTP")
        Session.Remove("KK")
        Session.Remove("NPWP")
        Session.Remove("Ijazah")
        Session.Remove("TranskripNilai")
        Session.Remove("Sertifikat")
        Session.Remove("TmpDtKeluarga")
        Session.Remove("TmpDtPendidikan")
        Session.Remove("TmpDtKetrampilan")
        Session.Remove("TmpDtRwytPekerjaan")
        Session.Remove("TmpDtRwytPekerjaanMinarta")
        Response.Redirect("FrmKaryawan.aspx")
    End Sub
End Class