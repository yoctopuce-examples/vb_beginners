Public Class Form1
  Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Dim errmsg as String = ""
    If YAPI.RegisterHub("usb", errmsg) <> YAPI_SUCCESS Then
      MessageBox.Show("Init error:" + errmsg)
      Application.Exit()
    End If
    label1.Text = "No sensor detected"
    Label2.Text = "N/A"
    Timer1.Enabled = True
  End Sub

  Dim sensor As YSensor = Nothing
  dim hardwaredetect As Integer = 0

  Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    Dim errmsg As String = ""

    If hardwaredetect = 0 Then
      YAPI.UpdateDeviceList(errmsg)
    End If
    hardwaredetect = (hardwaredetect + 1) Mod 20
    YAPI.HandleEvents(errmsg)
    If sensor Is Nothing Then
      sensor = YSensor.FirstSensor()
    End If
    If Not sensor Is Nothing Then
      If sensor.isOnline() Then
        label1.Text = sensor.get_friendlyName()
        label2.Text = Str(sensor.get_currentValue()) + sensor.get_unit()
      Else
        label1.Text = "Sensor is offline"
        label2.Text = "OFFLINE"
        sensor = Nothing
      End If
    End If
  End Sub
End Class