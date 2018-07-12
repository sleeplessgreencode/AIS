Public Class FrmEntryPayment
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

        LblAction.Text = Session("Payment").ToString.Split("|")(0)
        LblNoPD.Text = Session("Payment").ToString.Split("|")(1)

        If IsPostBack = False Then
            Call BindRekening()
            Call BindGrid()
        End If

    End Sub

    Private Sub BindRekening()
        DDLRek.Items.Clear()
        DDLRek.Items.Add("Pilih salah satu", "0")

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Rekening"
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        While RsFind.Read
            DDLRek.Items.Add(RsFind("NoRek") & " - " & RsFind("AtasNama") & " - " & RsFind("Bank"), RsFind("RekId"))
        End While

        RsFind.Close()
        CmdFind.Dispose()
    End Sub

    Private Sub BindForm()
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TipeForm,Keterangan FROM TipeForm WHERE TipeForm=@P1"
            .Parameters.AddWithValue("@P1", TxtForm.Text)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        While RsFind.Read
            TxtForm.Text = RsFind("TipeForm") & " - " & RsFind("Keterangan")
        End While
        RsFind.Close()
        CmdFind.Dispose()
    End Sub

    Private Sub BindKO()

        If TxtKo.Text = Trim(TxtJob.Text.Split("-")(0)) & "_INT" Then Exit Sub

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT VendorID FROM KoHdr WHERE NoKO=@P1"
            .Parameters.AddWithValue("@P1", TxtKo.Text)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            TxtVendor.Text = RsFind("VendorID")
        End If
        RsFind.Close()
        CmdFind.Dispose()

    End Sub

    Private Sub BindAlokasi()
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT Alokasi,Keterangan FROM Alokasi WHERE Alokasi=@P1"
            .Parameters.AddWithValue("@P1", TxtAlokasi.Text)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        While RsFind.Read
            TxtAlokasi.Text = RsFind("Alokasi") & " - " & RsFind("Keterangan")
        End While
        RsFind.Close()
        CmdFind.Dispose()

    End Sub

    Private Sub BindGrid()
        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT A.*, B.JobNm FROM PdHdr A JOIN Job B ON A.JobNo=B.JobNo WHERE NoPD=@P1"
                .Parameters.AddWithValue("@P1", LblNoPD.Text)
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    TxtNoPD.Text = RsFind("NoPD")
                    TxtJob.Text = RsFind("JobNo") + " - " + RsFind("JobNm")
                    TxtNoRef.Text = RsFind("NoRef").ToString
                    TxtAlokasi.Text = RsFind("Alokasi")
                    Call BindAlokasi()
                    TglPD.Date = RsFind("TglPD")
                    TxtForm.Text = RsFind("TipeForm")
                    Call BindForm()
                    TxtDesc.Text = RsFind("Deskripsi")
                    TxtKo.Text = RsFind("NoKO")
                    Call BindKO()
                    TxtNoTagihan.Text = RsFind("NoTagihan").ToString
                    TxtSubTotal.Text = Format(RsFind("TotalPD"), "N0")
                    LblKSO.Text = RsFind("KSO")

                    TxtNama.Text = RsFind("Nama").ToString
                    TxtAlamat.Text = RsFind("Alamat").ToString
                    TxtTelepon.Text = RsFind("Telepon").ToString
                    TxtNPWP.Text = RsFind("NPWP").ToString
                    TxtNoRek.Text = RsFind("NoRek").ToString
                    TxtAN.Text = RsFind("AtasNama").ToString
                    TxtBank.Text = RsFind("Bank").ToString

                    Call GetSaldo()

                End If
            End Using
        End Using

        Call GetTotal()
        TxtTotal1.Text = TxtTotal.Text

        If Left(LblAction.Text, 3) = "SEE" Then
            DisableControls(Form)
        End If

    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Dim Source As String = Session("Payment").ToString.Split("|")(2)

        Session.Remove("Payment")
        Response.Redirect(Source)

        Exit Sub
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As System.EventArgs) Handles BtnSave.Click
        If TglBayar.Date < TglPD.Date Then
            msgBox1.alert("Tgl pembayaran < Tgl Permintaan.")
            TglBayar.Focus()
            Exit Sub
        End If
        If DDLVia.Value = "0" Then
            msgBox1.alert("Pembayaran via belum dipilih.")
            DDLVia.Focus()
            Exit Sub
        End If
        If DDLVia.Value = "TRF" Then
            If DDLRek.Value = "0" Then
                msgBox1.alert("Rekening pengirim belum dipilih.")
                DDLRek.Focus()
                Exit Sub
            End If
            If DDLTransfer.Value = "0" Then
                msgBox1.alert("Jenis transfer belum dipilih.")
                DDLTransfer.Focus()
                Exit Sub
            End If
        End If
        If DDLVia.Value = "CG" Then
            If TxtCG.Text = "" Then
                msgBox1.alert("No. Cheque/Giro dan Tanggal belum dilengkapi.")
                TxtCG.Focus()
                Exit Sub
            End If
        End If
        If DDLVia.Value = "CASH" Then
            If DDLKas.Value = "0" Then
                msgBox1.alert("Sumber kas belum dipilih.")
                DDLKas.Focus()
                Exit Sub
            End If
            If TxtPenerima.Text = "" Then
                msgBox1.alert("Nama penerima belum diisi.")
                TxtPenerima.Focus()
                Exit Sub
            End If
        End If

        Using CmdInsert As New Data.SqlClient.SqlCommand
            With CmdInsert
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO BLE (TglBayar,NoPD,JobNo,Keterangan,Alokasi,TipeForm,NoKO,RekId,CaraBayar,JenisTrf,SumberKas," & _
                               "NoCG,NoRek,AtasNama,Bank,Amount,UserEntry,TimeEntry,NmPenerimaTunai) VALUES(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8," & _
                               "@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19)"
                .Parameters.AddWithValue("@P1", TglBayar.Date)
                .Parameters.AddWithValue("@P2", TxtNoPD.Text)
                .Parameters.AddWithValue("@P3", Trim(TxtJob.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P4", TxtKeterangan.Text)
                .Parameters.AddWithValue("@P5", Trim(TxtAlokasi.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P6", Trim(TxtForm.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P7", TxtKo.Text)
                .Parameters.AddWithValue("@P8", DDLRek.Value)
                .Parameters.AddWithValue("@P9", DDLVia.Value)
                .Parameters.AddWithValue("@P10", If(DDLVia.Value = "TRF", DDLTransfer.Value, DBNull.Value))
                .Parameters.AddWithValue("@P11", If(DDLVia.Value = "CASH", DDLKas.Value, DBNull.Value))
                .Parameters.AddWithValue("@P12", If(DDLVia.Value = "CG", TxtCG.Text, DBNull.Value))
                .Parameters.AddWithValue("@P13", TxtNoRek.Text)
                .Parameters.AddWithValue("@P14", TxtAN.Text)
                .Parameters.AddWithValue("@P15", TxtBank.Text)
                .Parameters.AddWithValue("@P16", TxtTotal.Text)
                .Parameters.AddWithValue("@P17", Session("User").ToString.Split("|")(0))
                .Parameters.AddWithValue("@P18", Now)
                .Parameters.AddWithValue("@P19", If(DDLVia.Value = "CASH", TxtPenerima.Text, DBNull.Value))
                .ExecuteNonQuery()
            End With
        End Using

        'If (Trim(TxtJob.Text.Split("-")(0)) = "1704" Or
        '    Trim(TxtJob.Text.Split("-")(0)) = "1734" Or
        '    Trim(TxtJob.Text.Split("-")(0)) = "1625") And
        '   (Trim(TxtForm.Text.Split("-")(0)) = "01C" Or
        '    Trim(TxtForm.Text.Split("-")(0)) = "02C" Or
        '    Trim(TxtForm.Text.Split("-")(0)) = "05C" Or
        '    Trim(TxtForm.Text.Split("-")(0)) = "06C" Or
        '    Trim(TxtForm.Text.Split("-")(0)) = "04B" Or
        '    Trim(TxtForm.Text.Split("-")(0)) = "05B") And
        '    TglBayar.Date > "03/31/2018" Then

        '    Using CmdFind As New Data.SqlClient.SqlCommand
        '        With CmdFind
        '            .Connection = Conn
        '            .CommandType = CommandType.Text
        '            .CommandText = "SELECT TOP 1 NoJurnal FROM JurnalEntry WHERE NoReg=@P1"
        '            .Parameters.AddWithValue("@P1", TxtNoPD.Text)
        '        End With
        '        Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        '            If Not RsFind.Read Then
        '                Dim LedgerNo As Integer = AssignLedgerNo()

        '                Using CmdInsert As New Data.SqlClient.SqlCommand
        '                    With CmdInsert
        '                        .Connection = Conn
        '                        .CommandType = CommandType.Text
        '                        .CommandText = "INSERT INTO JurnalEntry " & _
        '                                       "(JobNo,NoJurnal,TglJurnal,PC,Site,Member,Nota,Identitas,NoReg," & _
        '                                       "DebetBalance,KreditBalance,UserEntry,TimeEntry,Bulan,Tahun,LedgerNo) " & _
        '                                       "VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16)"
        '                        .Parameters.AddWithValue("@P1", Trim(TxtJob.Text.Split("-")(0)))
        '                        .Parameters.AddWithValue("@P2", "MN/" & Format(LedgerNo, "000") & "/" & Format(TglBayar.Date, "MM") & "/MM/" & Format(TglBayar.Date, "yy"))
        '                        .Parameters.AddWithValue("@P3", TglBayar.Date)
        '                        .Parameters.AddWithValue("@P4", "P")
        '                        .Parameters.AddWithValue("@P5", "Member2")
        '                        .Parameters.AddWithValue("@P6", "MN")
        '                        .Parameters.AddWithValue("@P7", "MM")
        '                        .Parameters.AddWithValue("@P8", "1006")
        '                        .Parameters.AddWithValue("@P9", TxtNoPD.Text)
        '                        .Parameters.AddWithValue("@P10", TxtTotal.Text)
        '                        .Parameters.AddWithValue("@P11", TxtTotal.Text)
        '                        .Parameters.AddWithValue("@P12", Session("User").ToString.Split("|")(0))
        '                        .Parameters.AddWithValue("@P13", Now)
        '                        .Parameters.AddWithValue("@P14", Format(TglBayar.Date, "MM"))
        '                        .Parameters.AddWithValue("@P15", Format(TglBayar.Date, "yyyy"))
        '                        .Parameters.AddWithValue("@P16", LedgerNo)
        '                        .ExecuteNonQuery()
        '                    End With
        '                End Using

        '                Using CmdInsert As New Data.SqlClient.SqlCommand
        '                    With CmdInsert
        '                        .Connection = Conn
        '                        .CommandType = CommandType.Text
        '                        .CommandText = "INSERT INTO JurnalEntry " & _
        '                                        "(JobNo,NoJurnal,TglJurnal,PC,Site,Member,Nota,Identitas,NoReg,AccNo,Uraian,DK,Debet,Kredit," + _
        '                                        "UserEntry,TimeEntry,Bulan,Tahun,LedgerNo) " & _
        '                                        "VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19)"
        '                        .Parameters.AddWithValue("@P1", Trim(TxtJob.Text.Split("-")(0)))
        '                        .Parameters.AddWithValue("@P2", "MN/" & Format(LedgerNo, "000") & "/" & Format(TglBayar.Date, "MM") & "/MM/" & Format(TglBayar.Date, "yy"))
        '                        .Parameters.AddWithValue("@P3", TglBayar.Date)
        '                        .Parameters.AddWithValue("@P4", "C")
        '                        .Parameters.AddWithValue("@P5", "Member2")
        '                        .Parameters.AddWithValue("@P6", "MN")
        '                        .Parameters.AddWithValue("@P7", "MM")
        '                        .Parameters.AddWithValue("@P8", "1006")
        '                        .Parameters.AddWithValue("@P9", TxtNoPD.Text)
        '                        If Trim(TxtForm.Text.Split("-")(0)) = "01C" Or Trim(TxtForm.Text.Split("-")(0)) = "02C" Or
        '                           Trim(TxtForm.Text.Split("-")(0)) = "05C" Or Trim(TxtForm.Text.Split("-")(0)) = "06C" Then
        '                            .Parameters.AddWithValue("@P10", "1004.001")
        '                        ElseIf Trim(TxtForm.Text.Split("-")(0)) = "04B" Or Trim(TxtForm.Text.Split("-")(0)) = "05B" Then
        '                            .Parameters.AddWithValue("@P10", "1006.004")
        '                        End If
        '                        .Parameters.AddWithValue("@P11", TxtDesc.Text)
        '                        .Parameters.AddWithValue("@P12", "D")
        '                        .Parameters.AddWithValue("@P13", TxtTotal.Text)
        '                        .Parameters.AddWithValue("@P14", 0)
        '                        .Parameters.AddWithValue("@P15", Session("User").ToString.Split("|")(0))
        '                        .Parameters.AddWithValue("@P16", Now)
        '                        .Parameters.AddWithValue("@P17", Format(TglBayar.Date, "MM"))
        '                        .Parameters.AddWithValue("@P18", Format(TglBayar.Date, "yyyy"))
        '                        .Parameters.AddWithValue("@P19", LedgerNo)
        '                        .ExecuteNonQuery()
        '                    End With
        '                End Using

        '                Using CmdInsert As New Data.SqlClient.SqlCommand
        '                    With CmdInsert
        '                        .Connection = Conn
        '                        .CommandType = CommandType.Text
        '                        .CommandText = "INSERT INTO JurnalEntry " & _
        '                                        "(JobNo,NoJurnal,TglJurnal,PC,Site,Member,Nota,Identitas,NoReg,AccNo,Uraian,DK,Debet,Kredit," + _
        '                                        "UserEntry,TimeEntry,Bulan,Tahun,LedgerNo) " & _
        '                                        "VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18,@P19)"
        '                        .Parameters.AddWithValue("@P1", Trim(TxtJob.Text.Split("-")(0)))
        '                        .Parameters.AddWithValue("@P2", "MN/" & Format(LedgerNo, "000") & "/" & Format(TglBayar.Date, "MM") & "/MM/" & Format(TglBayar.Date, "yy"))
        '                        .Parameters.AddWithValue("@P3", TglBayar.Date)
        '                        .Parameters.AddWithValue("@P4", "C")
        '                        .Parameters.AddWithValue("@P5", "Member2")
        '                        .Parameters.AddWithValue("@P6", "MN")
        '                        .Parameters.AddWithValue("@P7", "MM")
        '                        .Parameters.AddWithValue("@P8", "1006")
        '                        .Parameters.AddWithValue("@P9", TxtNoPD.Text)
        '                        .Parameters.AddWithValue("@P10", "2003.004")
        '                        .Parameters.AddWithValue("@P11", TxtDesc.Text)
        '                        .Parameters.AddWithValue("@P12", "K")
        '                        .Parameters.AddWithValue("@P13", 0)
        '                        .Parameters.AddWithValue("@P14", TxtTotal.Text)
        '                        .Parameters.AddWithValue("@P15", Session("User").ToString.Split("|")(0))
        '                        .Parameters.AddWithValue("@P16", Now)
        '                        .Parameters.AddWithValue("@P17", Format(TglBayar.Date, "MM"))
        '                        .Parameters.AddWithValue("@P18", Format(TglBayar.Date, "yyyy"))
        '                        .Parameters.AddWithValue("@P19", LedgerNo)
        '                        .ExecuteNonQuery()
        '                    End With
        '                End Using
        '            End If
        '        End Using
        '    End Using

        'End If

        BtnCancel_Click(BtnCancel, New EventArgs())

    End Sub

    Private Sub GetTotal()
        TxtTotal.Text = Format(CDec(TxtSubTotal.Text) - CDec(TxtSaldo.Text), "N0")
    End Sub

    Private Sub DisableControls(control As System.Web.UI.Control)

        For Each c As System.Web.UI.Control In control.Controls

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

    Private Sub GetSaldo()
        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT TOP 1 NoPJ,Saldo FROM PdHdr WHERE NoPD<@P1 AND JobNo=@P2 AND TipeForm=@P3 AND KSO=@P4 AND " + _
                           "RejectBy IS NULL ORDER BY NoPD DESC"
            .Parameters.AddWithValue("@P1", LblNoPD.Text)
            .Parameters.AddWithValue("@P2", Trim(TxtJob.Text.Split("-")(0)))
            .Parameters.AddWithValue("@P3", Trim(TxtForm.Text.Split("-")(0)))
            .Parameters.AddWithValue("@P4", LblKSO.Text)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            TxtNoPJ.Text = RsFind("NoPJ").ToString
            TxtSaldo.Text = Format(RsFind("Saldo"), "N0")
        End If
    End Sub

    Protected Sub DDLRek_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLRek.SelectedIndexChanged
        TxtBankKirim.Text = ""
        TxtNoRekKirim.Text = ""
        TxtANKirim.Text = ""
        If DDLRek.Value = "0" Then Exit Sub

        Dim CmdFind As New Data.SqlClient.SqlCommand
        With CmdFind
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Rekening WHERE RekId=@P1"
            .Parameters.AddWithValue("@P1", DDLRek.Value)
        End With
        Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
        If RsFind.Read Then
            TxtBankKirim.Text = RsFind("Bank")
            TxtNoRekKirim.Text = RsFind("NoRek")
            TxtANKirim.Text = RsFind("AtasNama")
        End If
    End Sub

    Protected Sub DDLVia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLVia.SelectedIndexChanged
        DDLKas.Enabled = False
        DDLKas.Value = "0"
        TxtPenerima.Enabled = False
        TxtPenerima.Text = ""
        DDLTransfer.Enabled = False
        DDLTransfer.Value = "0"
        DDLRek.Enabled = False
        DDLRek.Value = "0"
        TxtCG.Enabled = False
        TxtCG.Text = ""
        TxtANKirim.Text = ""
        TxtBankKirim.Text = ""
        TxtNoRekKirim.Text = ""

        If DDLVia.Value = "CASH" Then
            DDLKas.Enabled = True
            TxtPenerima.Enabled = True
        ElseIf DDLVia.Value = "TRF" Then
            DDLTransfer.Enabled = True
            DDLRek.Enabled = True
        ElseIf DDLVia.Value = "CG" Then
            DDLRek.Enabled = True
            TxtCG.Enabled = True
        End If
    End Sub

    Private Function AssignLedgerNo()
        Dim Nmr As Integer = 0

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT TOP 1 LedgerNo FROM JurnalEntry WHERE JobNo=@P1 AND Member=@P2 AND Bulan=@P3 AND Tahun=@P4 AND PC='P' ORDER BY LedgerNo DESC"
                .Parameters.AddWithValue("@P1", Trim(TxtJob.Text.Split("-")(0)))
                .Parameters.AddWithValue("@P2", "MN")
                .Parameters.AddWithValue("@P3", Month(TglBayar.Date))
                .Parameters.AddWithValue("@P4", Year(TglBayar.Date))
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                If RsFind.Read Then
                    Nmr = RsFind("LedgerNo") + 1
                Else
                    Nmr = 1
                End If

                Return Nmr                
            End Using
        End Using

    End Function

End Class