using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private Game _game;

    private float _sensitivity = 1f;
    private float _smoothing = 0.1f;
    private Vector2 _velocity;
    private Vector2 _frameVelocity;

    private void Reset()
    {
        _character = GetComponentInParent<PlayerController>().transform;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _sensitivity = PlayerPrefs.GetFloat("DecktopSensitivitySettings");
    }

    private void Update()
    {
        if (_game.IsMobile == false)
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * _sensitivity);
            _frameVelocity = Vector2.Lerp(_frameVelocity, rawFrameVelocity, 1 / _smoothing);
            _velocity += _frameVelocity;
            _velocity.y = Mathf.Clamp(_velocity.y, -90, 90);

            transform.localRotation = Quaternion.AngleAxis(-_velocity.y, Vector3.right);
            _character.localRotation = Quaternion.AngleAxis(_velocity.x, Vector3.up);
        }
    }

    public void ChangeSensivity(bool isChange)
    {
        if (isChange)
        {
            _sensitivity = 0.01f;
            _smoothing = 0.5f;
        }
        else if (isChange == false)
        {
            _sensitivity = PlayerPrefs.GetFloat("DecktopSensitivitySettings");
            _smoothing = 0.1f;
        }
    }
}