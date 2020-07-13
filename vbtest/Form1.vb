
Public Class Form1
    Private main As Xze.TestBench.Mainboard
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Await main.WriteReg(Xze.TestBench.Mainboard.WritableRegs.SW5V, 1) = True Then
            MessageBox.Show("Write success")
        Else
            MessageBox.Show("Write fail")
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        main = New Xze.TestBench.Mainboard(New IO.Ports.SerialPort("COM12"))
    End Sub
End Class
