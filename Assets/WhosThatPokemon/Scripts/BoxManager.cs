using UnityEngine;

[RequireComponent( typeof( SerialPortHandler ) )]
public class BoxManager : MonoBehaviour
{
    [Header("How long should the box open for")]
    [Tooltip( "Time in seconds" )]
    [Range( 0, 60f )]
    public float openDuration = 10f;

    private SerialPortHandler _sph;
    [Header( "Debug views: don't touch" )]
    [SerializeField]
    private bool _isOpen = false;
    [SerializeField]
    private float _countdown;


    void Start()
    {
        _sph = GetComponent<SerialPortHandler>();
        _countdown = openDuration;
    }

    void Update()
    {
        if(_isOpen)
        {
            if(_countdown > 0)
            {
                _countdown -= Time.deltaTime;
            } else
            {
                CloseBox();
            }
        }
    }

    public void OpenBox()
    {
        if(!_isOpen)
        {
            _sph.WriteString( "1" );
            _isOpen = true;
        }
        _countdown = openDuration;
    }

    public void CloseBox()
    {
        if(_isOpen)
        {
            _sph.WriteString( "0" );
            _isOpen = false;
        }
    }

}
