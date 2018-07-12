Public Class FrmMessage
    Inherits System.Web.UI.Page
    Dim Conn As New Data.SqlClient.SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Response.Redirect("Default.aspx")
            Exit Sub
        Else
            If CheckAkses1(Session("User").ToString.Split("|")(1), "CPanel") = False Then
                Response.Redirect("Default.aspx")
                Exit Sub
            End If
        End If

        Conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConnStr").ToString
        Conn.Open()

        If IsPostBack = False Then
            TxtDate.Date = Today
            TxtTime.DateTime = Now.AddHours(1)

            Dim TmpDt As New DataTable()
            TmpDt.Columns.AddRange(New DataColumn() {New DataColumn("NmSession", GetType(String)), _
                                                     New DataColumn("Value", GetType(String))})

            Dim SessionVariableName As String
            Dim SessionVariableValue As String

            For Each SessionVariable As String In Session.Keys
                ' Find Session variable name
                SessionVariableName = SessionVariable
                ' Find Session variable value
                SessionVariableValue = Session(SessionVariableName).ToString()

                ' Do something with this data
                TmpDt.Rows.Add(SessionVariableValue, SessionVariableName)

            Next

            GridView1.DataSource = TmpDt
            GridView1.DataBind()

        End If

    End Sub

    Private Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
        Conn.Close()
        Conn.Dispose()
    End Sub

    Protected Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If TxtDate.Date < Today Then
            msgBox1.alert("Date < Today.")
            TxtDate.Focus()
            Exit Sub
        End If
        Dim CTime As String = TxtDate.Date + " " + Format(TxtTime.Value, "HH:mm")

        Using CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE CPanel SET CNotification=@P1, CTimeOut=@P2"
                .Parameters.AddWithValue("@P1", TxtMessage.Text)
                .Parameters.AddWithValue("@P2", CTime)
                .ExecuteNonQuery()
            End With
        End Using
        Response.Redirect(Request.RawUrl)

    End Sub

    Private Sub BtnClear_Click(sender As Object, e As System.EventArgs) Handles BtnClear.Click
        Using CmdEdit As New Data.SqlClient.SqlCommand
            With CmdEdit
                .Connection = Conn
                .CommandType = CommandType.Text
                .CommandText = "UPDATE CPanel SET CNotification=NULL, CTimeOut=NULL"
                .ExecuteNonQuery()
            End With
        End Using
        Response.Redirect(Request.RawUrl)

    End Sub

End Class