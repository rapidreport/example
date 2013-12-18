Public Class Index
    Inherits System.Web.UI.Page

    Protected Sub BtnExample1_Click(sender As Object, e As EventArgs) Handles BtnExample1.Click
        Example1.Run(Server, Response)
    End Sub
End Class