Public Class ColumnsHeaderCheckbox

    Public listaCheckBoxow As New List(Of CheckBox)

    Public Sub checkBoxControll(ByVal DGV As DataGridView)
        'dodajemy dynamiczną zmiane położenia naszego checkboxa

        'wychwytuje zmniejszony element po lewej stronie
        Dim pierwszyElement As Boolean = True
        'pętla ustawiająca lokalizację checkboxów
        For i As Integer = 0 To listaCheckBoxow.Count - 1
            'nasz nagłówek
            Dim rect As Rectangle = DGV.GetCellDisplayRectangle(DGV.Columns(i).Index, -1, True)
            'lokalizacja checkboxa
            Dim Pt As New Point
            Pt.Y = 3 'ustawienie z góry
            'pętla sprawdza czy nagłówki są widoczne w całości (zmniejszone o wielkość checkboxu)
            If rect.Width >= DGV.Columns(i).Width - 20 Then
                If rect.Location.X > 20 Then
                    listaCheckBoxow(i).Visible = True
                    listaCheckBoxow(i).BackColor = Color.Red
                    If pierwszyElement = True Then
                        Pt.X = rect.Location.X + rect.Width - 20
                    Else
                        Pt.X = rect.Location.X + DGV.Columns(i).Width - 20
                    End If
                    pierwszyElement = False
                Else
                    listaCheckBoxow(i).Visible = False
                End If
            Else
                If rect.Location.X > 20 Then
                    If pierwszyElement = True Then
                        If rect.Location.X > 0 Then
                            'określa czy pierwszy nagłówek jest większy od wielkości Checkboxa 
                            '(czy się zmieści)
                            If rect.Width > 20 Then
                                listaCheckBoxow(i).Visible = True
                                listaCheckBoxow(i).BackColor = Color.Yellow
                                Pt.X = rect.Location.X + rect.Width - 20
                                pierwszyElement = False
                            Else
                                listaCheckBoxow(i).Visible = False
                            End If
                        End If
                    Else
                        listaCheckBoxow(i).Visible = False
                    End If
                Else
                    listaCheckBoxow(i).Visible = False
                End If
            End If
            'ustawia lokalizację obiektów
            listaCheckBoxow(i).Location = Pt
        Next
    End Sub

    Public Sub addCheckBoxesToColumnHeaders(ByVal DGV As DataGridView)
        For i As Integer = 0 To DGV.ColumnCount - 1
            'dla każdego nagłówka kolumny
            'jeśli nagłówek będzie niewidoczny wtedy rect.x będzie <0
            'a rect.Width=0
            Dim rect As Rectangle = DGV.GetCellDisplayRectangle(DGV.Columns(i).Index, -1, True)
            rect.Y = 3
            rect.X = rect.Location.X + rect.Width - 20
            'tworzymy nowy checkbox i nadajemu mu parametry startowe
            Dim NaglowekKolumny = New CheckBox()
            NaglowekKolumny.BackColor = Color.White
            NaglowekKolumny.Name = DGV.Columns(i).Name.ToString
            NaglowekKolumny.CheckAlign = ContentAlignment.MiddleCenter
            NaglowekKolumny.Size = New Size(18, 18)
            NaglowekKolumny.Checked = False
            'jeśli nie dodamy tej pętli, check boxy pojawią się w dziwnych miejscach
            'wyświetlimy checkboxy tylko dla widocznych kolumn
            If rect.Width = DGV.Columns(i).Width Then
                NaglowekKolumny.Location = rect.Location
            Else
                NaglowekKolumny.Visible = False
            End If
            'dodajemy wszystkie checkboxy do datagridview
            'będziemy zmieniać tylko ich położenie i opcje Visible
            DGV.Controls.Add(NaglowekKolumny)
            'dodajemy mu jeszcze jakąś fonkcjonalność, aby nie było, że nic nie robią
            listaCheckBoxow.Add(NaglowekKolumny)
        Next
    End Sub

End Class
