using System.IO.Ports;
using UnityEngine;

public class SerialPortHandler : MonoBehaviour
{
    [SerializeField]
    public enum OS { windows, osx }
    public OS OperatingSystem;
    [Tooltip("Servo uses 9600")]
    public int baudRate = 9600;
    [Header("Port number: No prefix text")]
    [Tooltip("Just the number, no COM or dev/usbmodem")]
    public int SerialPortNumber;
    private SerialPort _port;
    private string _formattedSerialPortName;

    private void Start()
    {
        InitPort();
    }

    private void OnApplicationQuit()
    {
        if(_port.IsOpen)
        {
            _port.Close();
        }
    }

    public void WriteString( string message )
    {
        if(_port.IsOpen)
        {
            _port.Write( message );
        } else
        {
            Debug.LogWarning( "Port is not open. It may still be loading." );
        }
    }

    private void InitPort()
    {
        switch(OperatingSystem)
        {
            case OS.windows:
                if(SerialPortNumber > 9)
                {
                    _formattedSerialPortName = "\\\\.\\COM" + SerialPortNumber.ToString();
                } else if(SerialPortNumber > 0)
                {
                    _formattedSerialPortName = "COM" + SerialPortNumber.ToString();
                } else
                {
                    Debug.LogError( "Please fill in serial port number." );
                }
                break;
            case OS.osx:
                if(SerialPortNumber > 0)
                {
                    _formattedSerialPortName = "/dev/tty.usbmodem" + SerialPortNumber.ToString();
                } else
                {
                    Debug.LogError( "Please fill in serial port number." );
                }
                break;
            default:

                break;
        }

        _port = new SerialPort( _formattedSerialPortName, baudRate );
        _port.Open();
    }
}
