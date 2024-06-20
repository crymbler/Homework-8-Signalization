using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _sensitivity;
    [SerializeField] private Transform _player;
    [SerializeField] private float _MaxRotationX;
    [SerializeField] private float _MinRotationX;

    private float _rotationX = 0;

    private void Start()
    {
        if (_MinRotationX > _MaxRotationX)
            _MinRotationX = _MaxRotationX;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, _MinRotationX, _MaxRotationX);

        transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);

        _player.Rotate(Vector3.up * mouseX);
    }
}