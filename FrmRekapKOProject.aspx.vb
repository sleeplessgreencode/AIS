Public Class FrmRekapKOProject
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If CheckAkses() = False Then
            Response.Redirect("Default.aspx")
            Exit Sub
        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Function CheckAkses() As Boolean
        If Session("User") = "" Then
            Return False
        Else
            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT AksesMenu FROM Login WHERE UserID=@P1"
                .Parameters.AddWithValue("@P1", Session("User").ToString.Split("|")(1))
            End With
            Dim AksesMenu As String = ""
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            If RsFind.Read Then
                AksesMenu = RsFind("AksesMenu").ToString
            End If
            RsFind.Close()
            CmdFind.Dispose()

            If AksesMenu = "*" Then Return True
            If Array.IndexOf(AksesMenu.Split(","), _
                HttpContext.Current.Request.Url.AbsolutePath.Split("/")(1).Split(".")(0)) < 0 Then
                Return False
            End If
        End If

        Return True
    End Function

    Protected Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Response.Redirect("FrmRptRekapKOProject.aspx")
    End Sub

    Public Class Job
        Public Property JobNo() As String
        Public Property TotalRAP() As Decimal
        Public Property TotalKO() As Decimal
    End Class

    <Services.WebMethod()> _
    Public Shared Function GetData() As List(Of Job)
        Dim Conn As New Data.SqlClient.SqlConnection
        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        Dim Jobs As New List(Of Job)()

        Dim CmdReport As New Data.SqlClient.SqlCommand
        With CmdReport
            .Connection = Conn
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Job WHERE StatusJob='Pelaksanaan'"
        End With
        Dim TotalRAP As Decimal = 0
        Dim TotalKO As Decimal = 0

        Dim RsLoad As Data.SqlClient.SqlDataReader = CmdReport.ExecuteReader
        While RsLoad.Read
            'Hitung Total RAP
            TotalRAP = 0
            Dim CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT Vol, HrgSatuan FROM RAP WHERE JobNo=@P1 AND Alokasi='B' AND Tipe='Detail'"
                .Parameters.AddWithValue("@P1", RsLoad("JobNo"))
            End With
            Dim RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
            While RsFind.Read
                TotalRAP += (RsFind("Vol") * RsFind("HrgSatuan"))
            End While
            RsFind.Close()
            CmdFind.Dispose()

            'Hitung Total KO
            TotalKO = 0
            Dim CmdFind1 As New Data.SqlClient.SqlCommand
            With CmdFind1
                .Connection = Conn
                .CommandType = CommandType.Text
                '.CommandText = "SELECT A.* FROM KoHdr A JOIN PdHdr B ON A.NoKO=B.NoKO WHERE A.JobNo=@P1 AND B.TglBayar IS NOT NULL"
                .CommandText = "SELECT * FROM KoHdr WHERE JobNo=@P1 AND ApprovedBy IS NOT NULL"
                .Parameters.AddWithValue("@P1", RsLoad("JobNo"))
            End With
            Dim RsFind1 As Data.SqlClient.SqlDataReader = CmdFind1.ExecuteReader
            While RsFind1.Read
                TotalKO += (RsFind1("SubTotal") - RsFind1("DiscAmount") + RsFind1("PPN"))
            End While
            RsFind1.Close()
            CmdFind1.Dispose()

            Jobs.Add(New Job() With { _
                        .JobNo = RsLoad("JobNo"), _
                        .TotalRAP = TotalRAP / 1000000, _
                        .TotalKO = TotalKO / 1000000
                    })
        End While

        RsLoad.Close()
        CmdReport.Dispose()
        Conn.Close()
        Conn.Dispose()

        Return Jobs
    End Function
End Class