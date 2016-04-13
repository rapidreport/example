Public Class FmMenu

    Private Sub BtnExample1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExample1.Click
        Example1.Run()
    End Sub

    Private Sub BtnOpenOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOpenOutput.Click
        System.Diagnostics.Process.Start("output\")
    End Sub

End Class
