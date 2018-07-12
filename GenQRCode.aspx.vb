Public Class GenQRCode
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()
    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As System.EventArgs) Handles btnGenerate.Click
        Dim Total As Integer

        Using CmdFind As New Data.SqlClient.SqlCommand
            With CmdFind
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM KoHdr WHERE QRCode IS NOT NULL OR QRCode<>''"
            End With
            Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                While RsFind.Read
                    Total += 1
                    Using CmdEdit As New Data.SqlClient.SqlCommand
                        With CmdEdit
                            .Connection = Conn
                            .CommandType = CommandType.Text
                            .CommandText = "UPDATE KoHdr SET QRCode=@P1 WHERE NoKO=@P2"
                            .Parameters.AddWithValue("@P1", GetQRCode)
                            .Parameters.AddWithValue("@P2", RsFind("NoKO"))
                            .ExecuteNonQuery()
                        End With
                    End Using
                End While
            End Using
        End Using

        MsgBox1.alert(Total.ToString + " Records Updated.")
    End Sub

    Private Function GetQRCode() As String
        Dim Ulang As Boolean = True

        Dim RandomValue As String = String.Empty

        While Ulang = True
            RandomValue = RandomX()
            Using CmdFind As New Data.SqlClient.SqlCommand
                With CmdFind
                    .Connection = Conn
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT QRCode FROM KOHdr WHERE QRCode=@P1"
                    .Parameters.AddWithValue("@P1", RandomValue)
                End With
                Using RsFind As Data.SqlClient.SqlDataReader = CmdFind.ExecuteReader
                    If Not RsFind.Read Then
                        Return RandomValue
                        Ulang = False
                    End If

                End Using
            End Using

        End While

        Return RandomValue

    End Function
End Class