Public Class FrmMain
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Label1.Text = "Welcome " & Session("User") & "<br /> " & DateTime.Now.ToLongDateString() & " " & DateTime.Now.ToLongTimeString()
    End Sub

    'Protected Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
    'Label1.Text = "Welcome " & Session("User") & "<br /> " & DateTime.Now.ToLongDateString() & " " & DateTime.Now.ToLongTimeString()
    'End Sub
End Class