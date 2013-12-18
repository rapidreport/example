Public Class FmMenu

    Private Sub BtnExample1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExample1.Click
        Example1.Run()
    End Sub

    Private Sub BtnExample1Csv_Click(sender As System.Object, e As System.EventArgs) Handles BtnExample1Csv.Click
        Example1Csv.Run()
    End Sub

    Private Sub BtnExample2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExample2.Click
        Example2.Run()
    End Sub

    Private Sub BtnExample2Huge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExample2Huge.Click
        Example2Huge.Run()
    End Sub

    Private Sub BtnExampleDataProvider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExampleDataProvider.Click
        ExampleDataProvider.Run()
    End Sub

    Private Sub BtnExample3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExampleLocate.Click
        ExampleLocate.Run()
    End Sub

    Private Sub BtnExampleRegion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExampleRegion.Click
        ExampleRegion.Run()
    End Sub

    Private Sub BtnExamplePage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExamplePage.Click
        ExamplePage.Run()
    End Sub

    Private Sub BtnExampleRender_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExampleRender.Click
        ExampleRender.Run()
    End Sub

    Private Sub BtnExampleSubPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExampleSubPage.Click
        ExampleSubPage.Run()
    End Sub

    Private Sub BtnExampleImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExampleImage.Click
        ExampleImage.Run()
    End Sub

    Private Sub BtnExtention_Click(sender As System.Object, e As System.EventArgs) Handles BtnExtention.Click
        ExampleExtention.Run()
    End Sub

    Private Sub BtnFeature_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFeature.Click
        Feature.Run()
    End Sub

    Private Sub BtnOpenOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOpenOutput.Click
        System.Diagnostics.Process.Start("output\")
    End Sub

End Class
