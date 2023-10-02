using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Game _game;
    private float _sensitivityX = 0.1f;
    private float _sensitivityY = 0.1f;
    private Vector2 _delta;
    private Vector3 _targetAngles;
    private Vector3 _followAngles;
    private bool _isTouch = false;
    private float _smoothSpeed = 10f;
    private Vector3 _previousAngles;
    private int _inversion;

    void Start()
    {
        _sensitivityX = PlayerPrefs.GetFloat("MobileSensitivitySettings");
        _sensitivityY = PlayerPrefs.GetFloat("MobileSensitivitySettings");

        _targetAngles = _camera.transform.localRotation.eulerAngles;
        _followAngles = _targetAngles;
        _previousAngles = _targetAngles;
    }

    private void Awake()
    {
        _inversion = PlayerPrefs.GetInt("Inversion");
    }

    void Update()
    {
        if (_game.IsMobile)
        {
            if (_isTouch)
            {
                if (Input.touchCount >= 1)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Moved)
                    {
                        _delta = touch.deltaPosition;
                        _targetAngles += new Vector3(-_delta.y * -_sensitivityY, _delta.x * -_sensitivityX, 0);
                        _targetAngles.x = Mathf.Clamp(_targetAngles.x, -90f, 90f);
                    }
                }
            }
            _followAngles = Vector3.Lerp(_followAngles, _targetAngles, Time.deltaTime * _smoothSpeed);
            _camera.transform.localRotation = Quaternion.Euler(_followAngles.x, _followAngles.y, 0f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        _isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        _isTouch = false;
        _previousAngles = _followAngles;
    }

    public void ChangeSensitivity(bool isChange)
    {
        if (isChange)
        {
            _sensitivityX = 0.03f;
            _sensitivityY = 0.03f;
        }
        else
        {
            _sensitivityX = PlayerPrefs.GetFloat("MobileSensitivitySettings");
            _sensitivityY = PlayerPrefs.GetFloat("MobileSensitivitySettings");
        }
    }
}