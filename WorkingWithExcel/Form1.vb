Public Class Form1
    'rodzaj połączenia
    Dim connection As System.Data.OleDb.OleDbConnection
    'lista nazw arkuszy
    Public sheetsList As New List(Of String)
    'ścieżka do pliku z którego pobierzemy dane
    Dim filePath As String
    'lista checkboxów w nagłówkach
    Dim ColumnsHeaderCheckbox As New ColumnsHeaderCheckbox
    'miejsce zapisu nowego pliku excel
    Dim savePath As String

    Private Sub ChooseFile_Click(sender As Object, e As EventArgs) Handles ChooseFile.Click
        'określamy rodzaj pliku, zmniejszy to ilość wyświetlanych elementów
        'wyfiltruje tylko pliki excel
        fileDialog.Filter = "csv files|;*.xls;*.xlsx"
        fileDialog.Title = "Select a Excel file"
        fileDialog.FileName = ""

        Try
            With fileDialog
                If .ShowDialog() = DialogResult.OK Then
                    'przekazujeny ścieżke do pliku, przyda on się w momencie listowania arkuszy
                    filePath = .FileName
                    'definiujemy połączenie
                    connection = New System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                                                       filePath + ";Extended Properties=Excel 12.0;")
                    'otwieramy połączenie
                    connection.Open()

                    Dim dtXlsSchema As DataTable = Nothing
                    dtXlsSchema = connection.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables,
                      New Object() {Nothing, Nothing, Nothing, "TABLE"})
                    'ładujemy nazwy arkuszy do listy
                    sheetsList.Clear()
                    For i = 0 To dtXlsSchema.Rows.Count - 1
                        sheetsList.Add(dtXlsSchema.Rows(i).Item("Table_Name").ToString)
                    Next
                    'zamykamy połączenie
                    connection.Close()
                    'otwieramy formę odpowiedzialną za wybór arkusza
                    Form2.Show()
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Public Sub addSheet(ByVal sheetName As String)
        Try
            Dim dataSet As System.Data.DataSet
            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter

            connection = New System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                                               filePath + ";Extended Properties=Excel 12.0;")
            connection.Open()

            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [" + sheetName + "]", connection)
            dataSet = New System.Data.DataSet
            MyCommand.Fill(dataSet)
            ExcelView.DataSource = dataSet.Tables(0)
            connection.Close()

            ColumnsHeaderCheckbox.addCheckBoxesToColumnHeaders(ExcelView)

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    'element odpowiedzialny za malowanie checkBoxów w nagłówkach kolumn
    Private Sub ExcelView_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) _
                                       Handles ExcelView.CellPainting

        ColumnsHeaderCheckbox.checkBoxControll(ExcelView)
    End Sub

    Private Sub FormatData_Click(sender As Object, e As EventArgs) Handles FormatData.Click
        'sprawdzamy które checkboxy są wybrane
        For i As Integer = ColumnsHeaderCheckbox.listaCheckBoxow.Count - 1 To 0 Step -1
            If ColumnsHeaderCheckbox.listaCheckBoxow(i).Checked = False Then
                ExcelView.Columns.RemoveAt(i)
            End If
        Next
        'listujemy wiersze
        For l As Integer = ExcelView.Rows.Count - 2 To 0 Step -1
            'zamieniamy dane w każdej komórkce na String i tworzymy jeden wieli tekst
            Dim test As String = ""
            For j As Integer = 0 To ExcelView.ColumnCount - 1
                test += ExcelView.Rows(l).Cells(j).Value.ToString
            Next
            'jeśli tekstu nie ma bo wszystkie komórki były puste
            If test.Trim.Length = 0 Then
                'to usuwamy wiersz
                ExcelView.Rows.Remove(ExcelView.Rows(l))
            End If
        Next
        'ukrywamy checkBoxy
        For k As Integer = 0 To ColumnsHeaderCheckbox.listaCheckBoxow.Count - 1
            ColumnsHeaderCheckbox.listaCheckBoxow(k).Visible = False
        Next
        ColumnsHeaderCheckbox.listaCheckBoxow.Clear()
    End Sub



    Private Sub SaveExcelFile_Click(sender As Object, e As EventArgs) Handles SaveExcelFile.Click

        SaveExcelDialog.Filter = "Excel excel|*.xlsx"
        SaveExcelDialog.Title = "Save an Excel File"
        'określamy miejsce zapisu nowego pliku
        Try
            With SaveExcelDialog
                If .ShowDialog() = DialogResult.OK Then
                    savePath = .FileName
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
        'blokujemy formę aby urzytkownik nic nie klikał podczas zapisu
        blockForm()
        'ustawiamy maksimum paska procesu
        SavingProgress.Maximum = (ExcelView.RowCount - 1) * ExcelView.ColumnCount
        'ustawiamy i odpalamy nasz BackgroundWorker
        SaveExcelBackgroundWorker.WorkerReportsProgress = True
        SaveExcelBackgroundWorker.WorkerSupportsCancellation = True
        SaveExcelBackgroundWorker.RunWorkerAsync()         '
    End Sub

    Private Sub blockForm()
        ChooseFile.Enabled = False
        FormatData.Enabled = False
        SaveExcelFile.Enabled = False
        ExcelView.Enabled = False
    End Sub

    Private Sub SaveExcelBackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) _
                                                Handles SaveExcelBackgroundWorker.DoWork

        Dim xlApp As Microsoft.Office.Interop.Excel.Application
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
        Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value

        xlApp = New Microsoft.Office.Interop.Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(misValue)

        'określamy nazwę arkusza 
        xlWorkSheet = xlWorkBook.Sheets("sheet1")
        'pobieramy nagłówki kolumn je również zapiszemy w Excelu
        For k As Integer = 1 To ExcelView.Columns.Count
            xlWorkSheet.Cells(1, k) = ExcelView.Columns(k - 1).HeaderText
        Next

        'pobieramy każdą komórkę i wstawiamy ją do xlWorkSheet
        For i = 0 To ExcelView.RowCount - 2
            For j = 0 To ExcelView.ColumnCount - 1
                xlWorkSheet.Cells(i + 2, j + 1) = ExcelView(j, i).Value.ToString()
                'dodajemy jeden do naszego paska procesu
                SavingProgress.Invoke(Sub()
                                          SavingProgress.Value += 1
                                      End Sub)
            Next
        Next
        'zapisujemy, zamykamy
        xlWorkSheet.SaveAs(savePath)
        xlWorkBook.Close()
        xlApp.Quit()

        'i czyścimy
        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)

    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    'Kiedy nasz BackgroundWorker zakończy proces, uruchomiona zostanie metoda:
    Private Sub SaveExcelBackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) _
                                                             Handles SaveExcelBackgroundWorker.RunWorkerCompleted
        MsgBox("You can find the excel: " + savePath)
        SavingProgress.Value = 0
        unBlockForm()
    End Sub

    Private Sub unBlockForm()
        ChooseFile.Enabled = True
        FormatData.Enabled = True
        SaveExcelFile.Enabled = True
        ExcelView.Enabled = True
    End Sub

End Class
