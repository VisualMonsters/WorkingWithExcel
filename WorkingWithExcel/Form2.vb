Public Class Form2
    Private Sub Form101_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For i As Integer = 0 To Form1.sheetsList.Count - 1
            SheetsListBox.Items.Add(Form1.sheetsList(i))
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Form1.addSheet(SheetsListBox.SelectedItem)
        Me.Close()
    End Sub
End Class