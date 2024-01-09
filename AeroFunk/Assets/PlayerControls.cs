using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _rSpeed;
    Rigidbody _p;

    [SerializeField] GameObject _eng;

    [SerializeField] float _rotationSpeed = 10f;
    [SerializeField] float _cLimit = 1;

    float _eSet = 0.4f;

    void Start()
    {
        _p = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovementInput();

        // Limit the maximum speed
        LimitSpeed();

        // Get mouse delta movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the object based on mouse movement
        RotateObject(mouseX, mouseY);
    }

    void HandleMovementInput()
    {
        // Get input for forward and backward movement
        float moveInput = Input.GetAxis("Vertical");

        // Apply force in the forward direction of the player's local coordinate system
        Vector3 forwardForce = transform.forward * moveInput * _speed;
        _p.AddForce(forwardForce);

        // Part engagement
        if (Input.GetKeyDown(KeyCode.W))
        {
            PartEngaged(_eng);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            PartDisEngaged(_eng);
        }
    }

    void RotateObject(float mouseX, float mouseY)
    {
        // Adjust rotation angles based on mouse movement
        float rotationY = mouseX * _rotationSpeed;

        // Apply the rotation to the object
        transform.Rotate(Vector3.up, rotationY, Space.World);
    }

    void PartEngaged(GameObject _part)
    {
        _part.transform.localPosition = new Vector3(_part.transform.localPosition.x + _eSet, _part.transform.localPosition.y, _part.transform.localPosition.z);
    }

    void PartDisEngaged(GameObject _part)
    {
        _part.transform.localPosition = new Vector3(_part.transform.localPosition.x - _eSet, _part.transform.localPosition.y, _part.transform.localPosition.z);
    }

    void LimitSpeed()
    {
        // Limit the maximum speed
        if (_p.velocity.magnitude > _cLimit)
        {
            _p.velocity = _p.velocity.normalized * _cLimit;
        }
    }
}