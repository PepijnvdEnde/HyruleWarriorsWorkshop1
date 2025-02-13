using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 200.0f;

    [SerializeField]
    private float _speed = 5f;

    private Vector3 _movement;
    private Rigidbody _rigidbody;
    private float _mouseX, _mouseY;
    private Transform _cameraTransform;
    private float _verticalRotation = 0f;
    private Camera _playerCamera;
    private Camera _otherCamera;
    private bool _onPlayerCamera = true;

    void OnMove(InputValue Input)
    {
        Vector2 InputVector = Input.Get<Vector2>();
        _movement = new Vector3(InputVector.x, 0, InputVector.y);
    }

    void OnLook(InputValue Input)
    {
        Vector2 lookInput = Input.Get<Vector2>();
        _mouseX = lookInput.x;
        _mouseY = lookInput.y;
    }

    void Start()
    {
        _playerCamera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = transform.right * _movement.x + transform.forward * _movement.z;
        _rigidbody.MovePosition(_rigidbody.position + moveDirection * Time.deltaTime * _speed);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * _mouseX * (_rotationSpeed / 1000));

        _verticalRotation -= _mouseY * (_rotationSpeed / 1000);
        _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);
        _cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
    }
    
    void OnJump(InputValue input)
    {
        if (input.isPressed && Mathf.Abs(_rigidbody.linearVelocity.y) < 0.01f)
        {
            _rigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }
}
