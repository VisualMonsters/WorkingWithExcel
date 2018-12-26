<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ChooseFile = New System.Windows.Forms.Button()
        Me.fileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SaveExcelFile = New System.Windows.Forms.Button()
        Me.FormatData = New System.Windows.Forms.Button()
        Me.ExcelView = New System.Windows.Forms.DataGridView()
        Me.SaveExcelBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.SavingProgress = New System.Windows.Forms.ProgressBar()
        Me.SaveExcelDialog = New System.Windows.Forms.SaveFileDialog()
        CType(Me.ExcelView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ChooseFile
        '
        Me.ChooseFile.Location = New System.Drawing.Point(16, 16)
        Me.ChooseFile.Margin = New System.Windows.Forms.Padding(4)
        Me.ChooseFile.Name = "ChooseFile"
        Me.ChooseFile.Size = New System.Drawing.Size(324, 28)
        Me.ChooseFile.TabIndex = 4
        Me.ChooseFile.Text = "Wybierz plik"
        Me.ChooseFile.UseVisualStyleBackColor = True
        '
        'fileDialog
        '
        Me.fileDialog.FileName = "OpenFileDialog1"
        '
        'SaveExcelFile
        '
        Me.SaveExcelFile.Location = New System.Drawing.Point(680, 16)
        Me.SaveExcelFile.Margin = New System.Windows.Forms.Padding(4)
        Me.SaveExcelFile.Name = "SaveExcelFile"
        Me.SaveExcelFile.Size = New System.Drawing.Size(268, 28)
        Me.SaveExcelFile.TabIndex = 7
        Me.SaveExcelFile.Text = "Zapisz plik Excel"
        Me.SaveExcelFile.UseVisualStyleBackColor = True
        '
        'FormatData
        '
        Me.FormatData.Location = New System.Drawing.Point(348, 16)
        Me.FormatData.Margin = New System.Windows.Forms.Padding(4)
        Me.FormatData.Name = "FormatData"
        Me.FormatData.Size = New System.Drawing.Size(324, 28)
        Me.FormatData.TabIndex = 6
        Me.FormatData.Text = "Formatuj dane"
        Me.FormatData.UseVisualStyleBackColor = True
        '
        'ExcelView
        '
        Me.ExcelView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExcelView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ExcelView.Location = New System.Drawing.Point(16, 64)
        Me.ExcelView.Margin = New System.Windows.Forms.Padding(4)
        Me.ExcelView.Name = "ExcelView"
        Me.ExcelView.Size = New System.Drawing.Size(932, 504)
        Me.ExcelView.TabIndex = 5
        '
        'SaveExcelBackgroundWorker
        '
        '
        'SavingProgress
        '
        Me.SavingProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SavingProgress.Location = New System.Drawing.Point(16, 581)
        Me.SavingProgress.Name = "SavingProgress"
        Me.SavingProgress.Size = New System.Drawing.Size(932, 23)
        Me.SavingProgress.TabIndex = 8
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(965, 616)
        Me.Controls.Add(Me.SavingProgress)
        Me.Controls.Add(Me.ChooseFile)
        Me.Controls.Add(Me.SaveExcelFile)
        Me.Controls.Add(Me.FormatData)
        Me.Controls.Add(Me.ExcelView)
        Me.DoubleBuffered = True
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.ExcelView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ChooseFile As Button
    Friend WithEvents fileDialog As OpenFileDialog
    Friend WithEvents SaveExcelFile As Button
    Friend WithEvents FormatData As Button
    Friend WithEvents ExcelView As DataGridView
    Friend WithEvents SaveExcelBackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents SavingProgress As ProgressBar
    Friend WithEvents SaveExcelDialog As SaveFileDialog
End Class
