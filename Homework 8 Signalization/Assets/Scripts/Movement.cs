using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis(nameof(Orientation.Horizontal));
        float vertical = Input.GetAxis(nameof(Orientation.Vertical));

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (direction != Vector3.zero)
        {
            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            _characterController.Move(move * _speed * Time.deltaTime);
        }
    }
}