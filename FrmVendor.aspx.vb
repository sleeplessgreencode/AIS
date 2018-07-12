Public Class FrmVendor
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "Vendor") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            ViewState("ColumnName") = "VendorId"
            ViewState("SortOrder") = "DESC"
            Call BindBank()
            Call BindKategori()
            Call BindGrid()
        End If

    End Sub

    Private Sub BindGrid()
        Using CmdGrid As New Data.SqlClient.SqlCommand
            With CmdGrid
                .Connection = Conn
                .CommandType = CommandType.Text
                If TxtFilter.Text = "" Then
                    .CommandText = "SELECT * FROM Vendor "
                Else
                    .CommandText = "SELECT * FROM Vendor WHERE " + DDLFilter.Value + " LIKE '%" + TxtFilter.Text + "%' "
                End If
                .CommandText = .CommandText + If(ViewState("ColumnName") = "", "", "ORDER BY " + ViewState("ColumnName") + " " + ViewState("SortOrder"))
            End With
            Using DaGrid As New Data.SqlClient.SqlDataAdapter
                DaGrid.SelectCommand = CmdGrid
                Using DtGrid As New Data.DataTable
                    DaGrid.Fill(DtGrid)
                    With GridData
                        .DataSource = DtGrid
                        .DataBind()
                    End With
                End Using
            End Using
        End Using

    End Sub

    Private Sub BindBank()
        DDLBank.Items.Clear()
        Using CmdIsi As New Data.SqlClient.SqlCommand
            With CmdIsi
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT Bank FROM Bank"
            End With
            Using RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
                While RsIsi.Read
                    DDLBank.Items.Add(RsIsi(0), RsIsi(0))
                End While
            End Using
        End Using

    End Sub

    Private Sub BindKategori()
        DDLKategori.Items.Clear()
        DDLKategori.Items.Add(String.Empty, String.Empty)

        Using CmdIsi As New Data.SqlClient.SqlCommand
            With CmdIsi
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT KategoriId FROM Kategori"
            End With
            Using RsIsi As Data.SqlClient.SqlDataReader = CmdIsi.ExecuteReader
                While RsIsi.Read
                    DDLKategori.Items.Add(RsIsi(0), RsIsi(0))
                End While
            End Using
        End Using

    End Sub

    Private Sub BindCleaning()
        TxtNama.Text = ""
        TxtAlamat.Text = ""
        TxtKota.Text = ""
        TxtPropinsi.Text = ""
        TxtCP.Text = ""
        TxtPhone1.Text = ""
        TxtPhone2.Text = ""
        TxtPhone3.Text = ""
        TxtFax.Text = ""
        TxtEmail1.Text = ""
        TxtEmail2.Text = ""
        TxtEmail3.Text = ""
        TxtNPWP.Text = ""
        TxtNoRek.Text = ""
        TxtAN.Text = ""
        DDLBank.Text = String.Empty
        DDLKategori.Text = String.Empty
        TxtUsaha.Text = ""
        CheckPKP.Checked = False

        LblAction.Text = "NEW"

        Session.Remove("Vendor")
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub GridData_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridData.PageIndexChanging
        GridData.PageIndex = e.NewPageIndex
        GridData.DataBind()
        Call BindGrid()
    End Sub

    Private Sub GridData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridData.RowCommand
        If e.CommandName = "BtnUpdate" Then
            Dim SelectRecord As GridViewRow = GridData.Rows(e.CommandArgument)
            Session("Vendor") = SelectRecord.Cells(0).Text

            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM Vendor WHERE VendorID=@P1"
                    .Parameters.AddWithValue("@P1", Session("Vendor"))
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        TxtNama.Text = RsFind("VendorNm")
                        TxtAlamat.Text = RsFind("Alamat").ToString
                        TxtKota.Text = RsFind("Kota").ToString
                        TxtPropinsi.Text = RsFind("Propinsi").ToString
                        TxtCP.Text = RsFind("ContactPerson").ToString
                        TxtPhone1.Text = RsFind("Telepon1").ToString
                        TxtPhone2.Text = RsFind("Telepon2").ToString
                        TxtPhone3.Text = RsFind("Telepon3").ToString
                        TxtFax.Text = RsFind("Fax").ToString
                        TxtEmail1.Text = RsFind("Email1").ToString
                        TxtEmail2.Text = RsFind("Email2").ToString
                        TxtEmail3.Text = RsFind("Email3").ToString
                        TxtNPWP.Text = RsFind("NPWP").ToString
                        TxtNoRek.Text = RsFind("NoRek").ToString
                        TxtAN.Text = RsFind("AtasNama").ToString
                        DDLBank.Value = RsFind("Bank").ToString
                        DDLKategori.Value = RsFind("Kategori")
                        TxtUsaha.Text = RsFind("BidangUsaha").ToString
                        CheckPKP.Checked = If(RsFind("PKP") = "0", False, True)
                    End If
                End Using
            End Using

            TxtNama.Enabled = True
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM KoHdr WHERE VendorId=@P1"
                    .Parameters.AddWithValue("@P1", SelectRecord.Cells(0).Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        TxtNama.Enabled = False
                    End If
                End Using
            End Using

            LblAction.Text = "UPD"

            ModalEntry.ShowOnPageLoad = True
            TxtNama.Focus()
        End If
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If TxtNama.Text = "" Then
            msgBox1.alert("Nama belum dilengkapi.")
            TxtNama.Focus()
            Exit Sub
        End If
        If DDLKategori.Value = "0" Then
            msgBox1.alert("Kategori belum dipilih.")
            DDLKategori.Focus()
            Exit Sub
        End If

        If LblAction.Text = "NEW" Then
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM Vendor WHERE VendorNm=@P1"
                    .Parameters.AddWithValue("@P1", TxtNama.Text)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If RsFind.Read Then
                        msgBox1.alert("Vendor " & TxtNama.Text & " sudah ada.")
                        TxtNama.Focus()
                        Exit Sub
                    End If
                End Using
            End Using

            Dim CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Vendor (VendorID,VendorNm,ContactPerson,Alamat,Telepon1,Telepon2,Telepon3,Fax,Email1,Email2,Email3,NPWP,NoRek,AtasNama,Bank," & _
                               "UserEntry,TimeEntry,BidangUsaha,Kategori,PKP,Kota,Propinsi) " & _
                               "VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19,@P20,@P21,@P22)"
                .Parameters.AddWithValue("@P1", AssignId())
                .Parameters.AddWithValue("@P2", TxtNama.Text)
                .Parameters.AddWithValue("@P3", TxtCP.Text)
                .Parameters.AddWithValue("@P4", TxtAlamat.Text)
                .Parameters.AddWithValue("@P5", TxtPhone1.Text)
                .Parameters.AddWithValue("@P6", TxtPhone2.Text)
                .Parameters.AddWithValue("@P7", TxtPhone3.Text)
                .Parameters.AddWithValue("@P8", TxtFax.Text)
                .Parameters.AddWithValue("@P9", TxtEmail1.Text)
                .Parameters.AddWithValue("@P10", TxtEmail2.Text)
                .Parameters.AddWithValue("@P11", TxtEmail3.Text)
                .Parameters.AddWithValue("@P12", TxtNPWP.Text)
                .Parameters.AddWithValue("@P13", TxtNoRek.Text)
                .Parameters.AddWithValue("@P14", TxtAN.Text)
                .Parameters.AddWithValue("@P15", If(DDLBank.Text = String.Empty, String.Empty, DDLBank.Value))
                .Parameters.AddWithValue("@P16", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P17", Now)
                .Parameters.AddWithValue("@P18", TxtUsaha.Text)
                .Parameters.AddWithValue("@P19", If(DDLKategori.Text = String.Empty, String.Empty, DDLKategori.Value))
                .Parameters.AddWithValue("@P20", If(CheckPKP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P21", TxtKota.Text)
                .Parameters.AddWithValue("@P22", TxtPropinsi.Text)
                .ExecuteNonQuery()
            End With
        Else
            Dim CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE Vendor SET " & _
                               "ContactPerson=@P1,Alamat=@P2,Fax=@P3,Telepon1=@P4,Email1=@P5,NPWP=@P6,NoRek=@P7,AtasNama=@P8,Bank=@P9," + _
                               "UserEntry=@P10,TimeEntry=@P11,VendorNm=@P12,Telepon2=@P13,Telepon3=@P14,Email2=@P15,Email3=@P16," + _
                               "Kategori=@P17,BidangUsaha=@P18,PKP=@P19,Kota=@P20,Propinsi=@P21 WHERE VendorID=@P22"
                .Parameters.AddWithValue("@P1", TxtCP.Text)
                .Parameters.AddWithValue("@P2", TxtAlamat.Text)
                .Parameters.AddWithValue("@P3", TxtFax.Text)
                .Parameters.AddWithValue("@P4", TxtPhone1.Text)
                .Parameters.AddWithValue("@P5", TxtEmail1.Text)
                .Parameters.AddWithValue("@P6", TxtNPWP.Text)
                .Parameters.AddWithValue("@P7", TxtNoRek.Text)
                .Parameters.AddWithValue("@P8", TxtAN.Text)
                .Parameters.AddWithValue("@P9", If(DDLBank.Text = String.Empty, String.Empty, DDLBank.Value))
                .Parameters.AddWithValue("@P10", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P11", Now)
                .Parameters.AddWithValue("@P12", TxtNama.Text)
                .Parameters.AddWithValue("@P13", TxtPhone2.Text)
                .Parameters.AddWithValue("@P14", TxtPhone3.Text)
                .Parameters.AddWithValue("@P15", TxtEmail2.Text)
                .Parameters.AddWithValue("@P16", TxtEmail3.Text)
                .Parameters.AddWithValue("@P17", If(DDLKategori.Text = String.Empty, String.Empty, DDLKategori.Value))
                .Parameters.AddWithValue("@P18", TxtUsaha.Text)
                .Parameters.AddWithValue("@P19", If(CheckPKP.Checked = True, "1", "0"))
                .Parameters.AddWithValue("@P20", TxtKota.Text)
                .Parameters.AddWithValue("@P21", TxtPropinsi.Text)
                .Parameters.AddWithValue("@P22", Session("Vendor"))
                .ExecuteNonQuery()
            End With
        End If

        Call BindGrid()
        ModalEntry.ShowOnPageLoad = False

    End Sub

    Private Function AssignId() As String
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT VendorID FROM Vendor ORDER BY VendorID DESC"
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    AssignId = "VEN" & Format(Right(RsFind(0), 4) + 1, "0000")
                Else
                    AssignId = "VEN0001"
                End If
            End Using
        End Using

    End Function

    Private Sub BtnAdd_Click(sender As Object, e As System.EventArgs) Handles BtnAdd.Click
        Call BindCleaning()
        ModalEntry.ShowOnPageLoad = True
        TxtNama.Focus()

    End Sub

    Private Sub BtnFilter_Click(sender As Object, e As System.EventArgs) Handles BtnFilter.Click
        Call BindGrid()
    End Sub

    Private Sub GridData_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridData.Sorting
        If e.SortExpression = ViewState("ColumnName").ToString() Then
            If ViewState("SortOrder").ToString() = "ASC" Then
                ViewState("SortOrder") = "DESC"
            Else
                ViewState("SortOrder") = "ASC"
            End If
        Else
            ViewState("ColumnName") = e.SortExpression
            ViewState("SortOrder") = "ASC"
        End If

        Call BindGrid()

    End Sub

End Class