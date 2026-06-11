Public Class SaleInvoiceCounter
    Public Property CounterValue As String   ' e.g. "000000-00-000001"

    Public Sub New(initialValue As String)
        Me.CounterValue = initialValue
    End Sub

    ' Increment the counter following your rules
    Public Sub Increment()

        ' Ensure default format if empty or invalid
        If String.IsNullOrEmpty(CounterValue) OrElse Not CounterValue.Contains("-") Then
            CounterValue = "000000-00-000001"
        End If

        Dim parts() As String = CounterValue.Split("-"c)

        ' Validate structure
        If parts.Length <> 3 Then
            Throw New Exception("Invalid counter format. Expected format: 000000-00-000000")
        End If

        Dim left As Integer = Integer.Parse(parts(0))
        Dim middle As Integer = Integer.Parse(parts(1))
        Dim right As Integer = Integer.Parse(parts(2))

        ' --- Increment Right ---
        right += 1

        ' If right exceeds 99999, reset and increment middle
        If right > 999999 Then
            right = 0
            middle += 1

            ' If middle exceeds 9999, reset and increment left
            If middle > 99 Then
                middle = 0
                left += 1
            End If
        End If

        CounterValue = $"{left:000000}-{middle:00}-{right:000000}"
    End Sub
End Class
