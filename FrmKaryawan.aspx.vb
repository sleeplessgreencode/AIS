Imports DevExpress.Web
Public Class FrmKaryawan
    Inherits System.Web.UI.Page
    Dim Conn As New SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "QueryPD") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack Then
            If Request.Form("konfirmasiStatus") = "1" Then
                Using CmdUbahStatus As New SqlClient.SqlCommand
                    With CmdUbahStatus
                        .Connection = Conn
                        .CommandType = CommandType.Text
                        .CommandText = "UPDATE Karyawan SET Active=@P1 where NIK=@P2"
                        .Parameters.AddWithValue("@P1", "0")
                        .Parameters.AddWithValue("@P2", Session("NIK"))
                        .ExecuteNonQuery()
                        .Dispose()
                    End With
                End Using
                Request.Form("konfirmasiStatus").Replace("1", "0")
                Session.Remove("NIK")
                msgBox1.alert("Karyawan telah dinon-aktifkan.")
            End If
        End If

        GridMaster.DataBind()
        GridMaster.Columns(2).Visible = False
        GridMaster.Columns(3).Visible = False
        GridMaster.Columns(4).Visible = False
        GridMaster.Columns(9).Visible = False
        GridMaster.Columns(10).Visible = False
        GridMaster.Columns(11).Visible = False
        GridMaster.Columns(12).Visible = False
        GridMaster.Columns(13).Visible = True

        If Session("masterKey") IsNot Nothing Then
            Using CmdGrid As New SqlClient.SqlCommand
                With CmdGrid
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM EmpPendidikan WHERE NIK=@P1"
                    .Parameters.AddWithValue("@P1", Session("masterKey"))
                End With
                Using DaGrid As New SqlClient.SqlDataAdapter
                    DaGrid.SelectCommand = CmdGrid
                    Using DsGrid As New DataSet
                        DaGrid.Fill(DsGrid)
                        With GridPendidikan
                            .DataSource = DsGrid
                        End With
                    End Using
                End Using
            End Using
        End If
    End Sub
    Protected Sub GridMaster_DataBound(ByVal sender As Object, ByVal e As EventArgs)
        If GridMaster.Columns("UpdateColumn") Is Nothing Then
            Dim colUpdate = New DevExpress.Web.GridViewCommandColumn()
            colUpdate.Name = "UpdateColumn"
            Dim btnUpdate = New DevExpress.Web.GridViewCommandColumnCustomButton()
            btnUpdate.ID = "UpdateBaris"
            btnUpdate.Text = "Update"
            colUpdate.CustomButtons.Add(btnUpdate)
            GridMaster.Columns.Add(colUpdate)
        End If
        If GridMaster.Columns("StatusColumn") Is Nothing Then
            Dim colStatus = New DevExpress.Web.GridViewCommandColumn()
            colStatus.Name = "StatusColumn"
            Dim btnStatus = New DevExpress.Web.GridViewCommandColumnCustomButton()
            btnStatus.ID = "StatusBaris"
            btnStatus.Text = "Non Aktif"
            colStatus.CustomButtons.Add(btnStatus)
            GridMaster.Columns.Add(colStatus)
        End If
    End Sub

    Protected Sub GridMaster_CustomButtonCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs)
        If e.ButtonID = "UpdateBaris" Then
            Session("Karyawan") = "UPD"
            Session("NIK") = GridMaster.GetRowValues(e.VisibleIndex, "NIK")
            DevExpress.Web.ASPxWebControl.RedirectOnCallback("FrmEntryKaryawan.aspx")
        ElseIf e.ButtonID = "StatusBaris" Then
            Dim StatusSkrg As String = GridMaster.GetRowValues(e.VisibleIndex, "Active")
            If StatusSkrg = "0" Then
                LblInformasi.Text = "Status karyawan ini sudah tidak aktif"
                PopInformasi.ShowOnPageLoad = True
            Else
                Session("NIK") = GridMaster.GetRowValues(e.VisibleIndex, "NIK")
                LblKonfirmasi.Text = "Anda akan merubah status karyawan " & GridMaster.GetRowValues(e.VisibleIndex, "Nama") & _
                                " menjadi tidak aktif."
                PopKonfirmasi.ShowOnPageLoad = True
            End If
        End If
    End Sub
    'Private Sub GridMaster_EditCommand(ByVal sender As Object, ByVal e As DataGridCommandEventArgs) Handles GridMaster.EditCommand
    '    Session("Karyawan") = "UPD"
    '    Dim NilaiNIK As Object = TryCast(GridMaster.GetRowValues(1, "NIK"), Object())
    '    Session("NIK") = NilaiNIK
    '    Response.Redirect("FrmEntryKaryawan.aspx")
    'End Sub

    Private Sub Grid_DataBinding(sender As Object, e As System.EventArgs) Handles GridMaster.DataBinding
        Using CmdGrid As New SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT " & _
                               "Karyawan.NIK, " & _
                               "Karyawan.Nama, " & _
                               "Karyawan.Alamat, " & _
                               "Karyawan.NoTelp, " & _
                               "Karyawan.Email, " & _
                               "Karyawan.LokasiKerja as [Lokasi Kerja], " & _
                               "Karyawan.Divisi, " & _
                               "Karyawan.Subdivisi as [Sub Divisi], " & _
                               "Karyawan.Jabatan, " & _
                               "Karyawan.Golongan, " & _
                               "Karyawan.Grade, " & _
                               "Karyawan.UraianPekerjaan as [Uraian Pekerjaan], " & _
                               "Karyawan.Foto, " & _
                               "Karyawan.Active " & _
                               "FROM Karyawan"
            End With
            Using DaGrid As New SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DsGrid As New DataSet
                    DaGrid.Fill(DsGrid)
                    With GridMaster
                        .DataSource = DsGrid
                    End With
                End Using
            End Using
        End Using

    End Sub

    Protected Sub BtnTambah_Click(sender As Object, e As EventArgs) Handles BtnTambah.Click
        Session("Karyawan") = "NEW"
        Response.Redirect("FrmEntryKaryawan.aspx")
    End Sub

    Protected Sub GridMinarta_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs)
        'Dim masterKey As Integer = GridMaster.GetRowValues(0, "NIK")
        Dim masterKey As Object = GridMaster.GetRowValues(Convert.ToInt32(e.Parameters), "NIK")
        Session("masterKey") = masterKey
        Using CmdGrid As New SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT " & _
                               "EmpPekerjaanMinarta.NIK, " & _
                               "EmpPekerjaanMinarta.PrdAwal as [Tanggal Dimulai], " & _
                               "EmpPekerjaanMinarta.PrdAkhir as [Tanggal Berakhir], " & _
                               "EmpPekerjaanMinarta.Divisi, " & _
                               "EmpPekerjaanMinarta.Subdivisi, " & _
                               "EmpPekerjaanMinarta.Jabatan, " & _
                               "EmpPekerjaanMinarta.Golongan, " & _
                               "EmpPekerjaanMinarta.Grade as Status, " & _
                               "EmpPekerjaanMinarta.LokasiKerja as [Lokasi Kerja], " & _
                               "EmpPekerjaanMinarta.KPI, " & _
                               "EmpPekerjaanMinarta.Atasan " & _
                               "FROM EmpPekerjaanMinarta " & _
                               "WHERE NIK=@P1"
                .Parameters.AddWithValue("@P1", Session("masterKey"))
            End With
            Using DaGrid As New SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DsGrid As New DataSet
                    DaGrid.Fill(DsGrid)
                    With GridMinarta
                        .DataSource = DsGrid
                        .DataBind()
                        .Columns(0).Visible = False
                    End With
                End Using
            End Using
        End Using
    End Sub
    Protected Sub GridRwytPekerjaan_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs)
        'Dim masterKey As Integer = GridMaster.GetRowValues(0, "NIK")
        Dim masterKey As Object = GridMaster.GetRowValues(Convert.ToInt32(e.Parameters), "NIK")
        Session("masterKey") = masterKey
        Using CmdGrid As New SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT " & _
                               "EmpPekerjaanH.NIK, " & _
                               "EmpPekerjaanH.PrdAwal as [Tanggal Dimulai], " & _
                               "EmpPekerjaanH.PrdAkhir as [Tanggal Berakhir], " & _
                               "EmpPekerjaanH.Perusahaan, " & _
                               "EmpPekerjaanH.Alamat, " & _
                               "EmpPekerjaanH.Industri, " & _
                               "EmpPekerjaanH.Jabatan, " & _
                               "EmpPekerjaanH.UraianPekerjaan as [Uraian Pekerjaan] " & _
                               "FROM EmpPekerjaanH " & _
                               "WHERE NIK=@P1"
                .Parameters.AddWithValue("@P1", Session("masterKey"))
            End With
            Using DaGrid As New SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DsGrid As New DataSet
                    DaGrid.Fill(DsGrid)
                    With GridRwytPekerjaan
                        .DataSource = DsGrid
                        .DataBind()
                        .Columns(0).Visible = False
                    End With
                End Using
            End Using
        End Using
    End Sub

    'Protected Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
    '    'Menyimpan input data karyawan ke dalam database
    'End Sub
    Protected Sub GridPendidikan_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs)
        'Dim masterKey As Integer = GridMaster.GetRowValues(0, "NIK")
        Dim masterKey As Object = GridMaster.GetRowValues(Convert.ToInt32(e.Parameters), "NIK")
        Session("masterKey") = masterKey
        Using CmdGrid As New SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT " & _
                               "EmpPendidikan.NIK, " & _
                               "EmpPendidikan.TgkPendidikan as [Tingkat Pendidikan], " & _
                               "EmpPendidikan.PrdAwal as [Tanggal Dimulai], " & _
                               "EmpPendidikan.PrdAkhir as [Tanggal Berakhir], " & _
                               "EmpPendidikan.Institusi, " & _
                               "EmpPendidikan.Alamat, " & _
                               "EmpPendidikan.Jurusan, " & _
                               "EmpPendidikan.LlsTdkLls as Status, " & _
                               "EmpPendidikan.IPK as Nilai, " & _
                               "EmpPendidikan.NoIjazah as [Nomor Ijazah] " & _
                               "FROM EmpPendidikan " & _
                               "WHERE NIK=@P1"
                .Parameters.AddWithValue("@P1", Session("masterKey"))
            End With
            Using DaGrid As New SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DsGrid As New DataSet
                    DaGrid.Fill(DsGrid)
                    With GridPendidikan
                        .DataSource = DsGrid
                        .DataBind()
                        .Columns(0).Visible = False
                    End With
                End Using
            End Using
        End Using
    End Sub
    Protected Sub GridKetrampilan_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs)
        'Dim masterKey As Integer = GridMaster.GetRowValues(0, "NIK")
        Dim masterKey As Object = GridMaster.GetRowValues(Convert.ToInt32(e.Parameters), "NIK")
        Session("masterKey") = masterKey
        Using CmdGrid As New SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT " & _
                               "EmpKetrampilan.NIK, " & _
                               "EmpKetrampilan.Nama as [Nama Ketrampilan], " & _
                               "EmpKetrampilan.PrdAwal as [Tanggal Dimulai], " & _
                               "EmpKetrampilan.PrdAkhir as [Tanggal Berakhir], " & _
                               "EmpKetrampilan.Sertifikat as [Nama Sertifikat], " & _
                               "EmpKetrampilan.Grade as [Level / Grade], " & _
                               "EmpKetrampilan.NoSertifikat as [No. Sertifikat], " & _
                               "EmpKetrampilan.Institusi as [Diterbitkan Oleh] " & _
                               "FROM EmpKetrampilan " & _
                               "WHERE NIK=@P1"
                .Parameters.AddWithValue("@P1", Session("masterKey"))
            End With
            Using DaGrid As New SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DsGrid As New DataSet
                    DaGrid.Fill(DsGrid)
                    With GridKetrampilan
                        .DataSource = DsGrid
                        .DataBind()
                        .Columns(0).Visible = False
                    End With
                End Using
            End Using
        End Using
    End Sub
    Protected Sub GridIdentitas_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs)
        'Dim masterKey As Integer = GridMaster.GetRowValues(0, "NIK")
        Dim masterKey As Object = GridMaster.GetRowValues(Convert.ToInt32(e.Parameters), "NIK")
        Session("masterKey") = masterKey
        Using CmdGrid As New SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT " & _
                               "EmpID.NIK, " & _
                               "EmpID.JenisID as [Jenis ID], " & _
                               "EmpID.NoID as [No ID], " & _
                               "EmpID.PrdAwal as [Tanggal Mulai Berlaku], " & _
                               "EmpID.PrdAkhir as [Tanggal Akhir Berlaku], " & _
                               "EmpID.DiterbitkanOleh as [Diterbitkan Oleh] " & _
                               "FROM EmpID " & _
                               "WHERE NIK=@P1"
                .Parameters.AddWithValue("@P1", Session("masterKey"))
            End With
            Using DaGrid As New SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DsGrid As New DataSet
                    DaGrid.Fill(DsGrid)
                    With GridIdentitas
                        .DataSource = DsGrid
                        .DataBind()
                        .Columns(0).Visible = False
                    End With
                End Using
            End Using
        End Using
    End Sub
    Protected Sub GridKeluarga_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs)
        'Dim masterKey As Integer = GridMaster.GetRowValues(0, "NIK")
        Dim masterKey As Object = GridMaster.GetRowValues(Convert.ToInt32(e.Parameters), "NIK")
        Session("masterKey") = masterKey
        Using CmdGrid As New SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT " & _
                               "EmpKeluarga.NIK, " & _
                               "EmpKeluarga.Hubungan, " & _
                               "EmpKeluarga.Nama, " & _
                               "EmpKeluarga.Kelamin as [Jenis Kelamin], " & _
                               "EmpKeluarga.TglLahir as [Tanggal Lahir], " & _
                               "EmpKeluarga.Pekerjaan, " & _
                               "EmpKeluarga.Perusahaan " & _
                               "FROM EmpKeluarga " & _
                               "WHERE NIK=@P1"
                .Parameters.AddWithValue("@P1", Session("masterKey"))
            End With
            Using DaGrid As New SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DsGrid As New DataSet
                    DaGrid.Fill(DsGrid)
                    With GridKeluarga
                        .DataSource = DsGrid
                        .DataBind()
                        .Columns(0).Visible = False
                    End With
                End Using
            End Using
        End Using
    End Sub
    Protected Sub GridMinarta_DataBound(ByVal sender As Object, ByVal e As EventArgs)
        Dim grid = TryCast(sender, ASPxGridView)
        For i As Integer = 0 To grid.Columns.Count - 1
            If grid.DataColumns(i).FieldName = "Tanggal Dimulai" _
               Or grid.DataColumns(i).FieldName = "Tanggal Berakhir" Then
                grid.DataColumns(i).PropertiesEdit.DisplayFormatString = "dd-MMM-yyyy"
            End If
        Next
    End Sub
    Protected Sub GridRwytPekerjaan_DataBound(ByVal sender As Object, ByVal e As EventArgs)
        Dim grid = TryCast(sender, ASPxGridView)
        For i As Integer = 0 To grid.Columns.Count - 1
            If grid.DataColumns(i).FieldName = "Tanggal Dimulai" _
               Or grid.DataColumns(i).FieldName = "Tanggal Berakhir" Then
                grid.DataColumns(i).PropertiesEdit.DisplayFormatString = "dd-MMM-yyyy"
            End If
        Next
    End Sub
    Protected Sub GridPendidikan_DataBound(ByVal sender As Object, ByVal e As EventArgs)
        Dim grid = TryCast(sender, ASPxGridView)
        For i As Integer = 0 To grid.Columns.Count - 1
            If grid.DataColumns(i).FieldName = "Tanggal Dimulai" _
               Or grid.DataColumns(i).FieldName = "Tanggal Berakhir" Then
                grid.DataColumns(i).PropertiesEdit.DisplayFormatString = "dd-MMM-yyyy"
            End If
        Next
    End Sub
    Protected Sub GridKetrampilan_DataBound(ByVal sender As Object, ByVal e As EventArgs)
        Dim grid = TryCast(sender, ASPxGridView)
        For i As Integer = 0 To grid.Columns.Count - 1
            If grid.DataColumns(i).FieldName = "Tanggal Dimulai" _
               Or grid.DataColumns(i).FieldName = "Tanggal Berakhir" Then
                grid.DataColumns(i).PropertiesEdit.DisplayFormatString = "dd-MMM-yyyy"
            End If
        Next
    End Sub
    Protected Sub GridIdentitas_DataBound(ByVal sender As Object, ByVal e As EventArgs)
        Dim grid = TryCast(sender, ASPxGridView)
        For i As Integer = 0 To grid.Columns.Count - 1
            If grid.DataColumns(i).FieldName = "Tanggal Mulai Berlaku" _
               Or grid.DataColumns(i).FieldName = "Tanggal Akhir Berlaku" Then
                grid.DataColumns(i).PropertiesEdit.DisplayFormatString = "dd-MMM-yyyy"
            End If
        Next
    End Sub
    Protected Sub GridKeluarga_CustomColumnDisplayText(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs) Handles GridKeluarga.CustomColumnDisplayText
        If e.Column.FieldName = "Jenis Kelamin" Then
            If e.Value = "L" Then
                e.DisplayText = "Pria"
            Else
                e.DisplayText = "Wanita"
            End If
        End If
    End Sub

End Class