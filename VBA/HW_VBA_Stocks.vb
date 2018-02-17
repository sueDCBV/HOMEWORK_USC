Sub Stocks()

'Variables for loop
Dim thisCell As String
Dim nextCell As String

'Variables for values to calculate and summarize
Dim Ticker As String
Dim TotalStockVolume As Double
Dim CountStock As Double
Dim OpenValue As Double
Dim CloseValue As Double

'variables for iteration
Dim i As Double
Dim lRow, lRowGreat As Double

'Executes macro in each worksheet
Application.ScreenUpdating = False
For Each sh In Worksheets
    sh.Activate

    'Counting the last row filled
    lRow = Cells(Rows.Count, 1).End(xlUp).Row

    TotalStockVolume = 0
    CountStock = 0
    OpenValue = 0
    CloseValue = 0

    'Row counter for first table summary
    Dim Summary_Table_Row As Double
    Summary_Table_Row = 2
  
    'Headers of first table summary
    Range("I1").Value = "Ticker"
    Range("J1").Value = "Yearly Change"
    Range("K1").Value = "Percent Change"
    Range("L1").Value = "Total Volume"

    'Loop to calculate values per ticker
    For i = 2 To lRow
        thisCell = Cells(i, 1).Value
        nextCell = Cells(i + 1, 1).Value
  
        ' Check if we are still within the same ticker
        If nextCell <> thisCell Then
            Ticker = thisCell
        
            ' Calculate values per row
            TotalStockVolume = TotalStockVolume + Cells(i, 7).Value
            CountStock = CountStock + 1
            OpenValue = OpenValue + Cells(i, 3).Value
            CloseValue = CloseValue + Cells(i, 6).Value
         
            ' Print Ticker
            Range("I" & Summary_Table_Row).Value = Ticker

            ' Print Yearly Change
            Range("J" & Summary_Table_Row).Value = (CloseValue / CountStock) - (OpenValue / CountStock)

            'Set background color green or red
            If ((CloseValue / CountStock) - (OpenValue / CountStock)) >= 0 Then
                Range("J" & Summary_Table_Row).Interior.ColorIndex = 4
            Else
                Range("J" & Summary_Table_Row).Interior.ColorIndex = 3
            End If


            ' Print Percentage Change and validate division with zero
            If (OpenValue / CountStock) > 0 Then
                Range("K" & Summary_Table_Row).Value = FormatPercent(((CloseValue / CountStock) - (OpenValue / CountStock)) / (OpenValue / CountStock), 2)
            Else
                Range("K" & Summary_Table_Row).Value = FormatPercent(0, 2)

            End If
        
            ' Print Total Volume
            Range("L" & Summary_Table_Row).Value = TotalStockVolume
     
        
            ' Add one to the summary table row
            Summary_Table_Row = Summary_Table_Row + 1
      
            ' Reset values
            TotalStockVolume = 0
            CountStock = 0
            OpenValue = 0
            CloseValue = 0
            
        ' If the cell immediately following a row is the same ticker...
        Else
            ' Calculate values per row
            TotalStockVolume = TotalStockVolume + Cells(i, 7).Value
            CountStock = CountStock + 1
            OpenValue = OpenValue + Cells(i, 3).Value
            CloseValue = CloseValue + Cells(i, 6).Value
        
        End If

    Next i

    ''''''''''''''''''''''''''''''''
    ''Second part: Greatest values''
    ''''''''''''''''''''''''''''''''
    Dim rng, rng2 As Range
    Dim dblMin, dblMax, dblMaxVol As Double
    Dim intMatchMin, intMatchMax, intMatchMaxVol As Integer
    
    'Counting the last row filled (second table)
    lRowGreat = Cells(Rows.Count, 9).End(xlUp).Row
 
    'Set range from which to determine smallest and largest value
    Set rng = Range("K1:K" & lRowGreat)
    Set rng2 = Range("L1:L" & lRowGreat)

    'Returns the smallest, largest value in each range
    dblMin = Application.WorksheetFunction.Min(rng)
    dblMax = Application.WorksheetFunction.Max(rng)
    dblMaxVol = Application.WorksheetFunction.Max(rng2)

    'Returns position of each values
    intMatchMin = Application.WorksheetFunction.Match(dblMin, rng, 0)
    intMatchMax = Application.WorksheetFunction.Match(dblMax, rng, 0)
    intMatchMaxVol = Application.WorksheetFunction.Match(dblMaxVol, rng2, 0)

    'Print headers
    Range("P1").Value = "Ticker"
    Range("Q1").Value = "Value"

    ' Print values in each row
    Range("O2").Value = "Greatest % Increase"
    Range("P2").Value = Range("I" & intMatchMax).Value
    Range("Q2").Value = FormatPercent(dblMax, 2)

    Range("O3").Value = "Greatest % Decrease"
    Range("P3").Value = Range("I" & intMatchMin).Value
    Range("Q3").Value = FormatPercent(dblMin, 2)
    
    Range("O4").Value = "Greatest Total Volume"
    Range("P4").Value = Range("I" & intMatchMaxVol).Value
    Range("Q4").Value = dblMaxVol

Next sh
Application.ScreenUpdating = True

End Sub
