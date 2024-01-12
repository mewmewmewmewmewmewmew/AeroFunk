using FMODUnity;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _rSpeed;
    [SerializeField] float _sideBurst=5f;
    [SerializeField] float _sideCool;
    [SerializeField] float _agFloat=1.3f;
    [SerializeField] float _finOffset = 0.64f;


    Rigidbody _p;

    [SerializeField] GameObject _eng;
    [SerializeField] GameObject _finL;
    [SerializeField] GameObject _finR;


    float _rotationSpeed = 5f;
    [SerializeField] float _cLimit = 1;
    [SerializeField] float _sideLimit = 1;

    [SerializeField] GameObject badFmodUse;
    [SerializeField] ParticleSystem _myPartis;

    //better Fmod use
    FMOD.Studio.PARAMETER_ID _parameterID;
    [SerializeField] StudioEventEmitter _emitter;
    float _eSet = 0.4f;
    float _flSet = 0.4f;
    float _frSet = 0.4f;

    Vector3 forwardForce;

    public TextMeshProUGUI _textSpeed;




    void Start()
    {
        _p = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        HandleMovementInput();

        // Limit the maximum speed
        LimitSpeed();

        // Get mouse delta movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float joyStickX = Input.GetAxis("HorizontalR");


        // Rotate the object based on mouse movement
        RotateObject(mouseX, mouseY);


    }

    bool alignedToGround = false;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            PartEngaged(_eng);
            Debug.Log("pressed");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            PartDisEngaged(_eng);
            Debug.Log("released");
        }
        if (Input.GetKeyDown(KeyCode.A))//replace with general input later
        {
            SideBurst(1);
        }
        if (Input.GetKeyDown(KeyCode.D))//replace with general input later
        {
            SideBurst(-1);
        }

    }

    void HandleMovementInput()
    {
        // Get input for forward and backward movement
        _rotationSpeed = 200 + _p.velocity.magnitude/4;


    }
    void RotateObject(float mouseX, float mouseY)
    {
        // Adjust rotation angles based on mouse movement

        float rotationY = mouseX * _rotationSpeed;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            //try to make code where it's always just aiming to reach that certain range, instead of snapping to a certain position
            if(hit.distance > _agFloat && hit.distance < _agFloat+1f)
            {
                Vector3 newPos = transform.position;
                newPos.y = (hit.point + Vector3.up * _agFloat).y;
                transform.position = Vector3.Slerp(transform.position, newPos, .1f);
            }

            if(hit.distance > _agFloat + 1f)
            {
                _p.AddForce(0f,(-200f) * Time.deltaTime,0f);
            }


            var slopeRotation = Quaternion.FromToRotation(transform.up, hit.normal);
            transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation * transform.rotation, 10f * Time.deltaTime);
        }


        // Apply the rotation to the object
        transform.Rotate(transform.up, rotationY * Time.deltaTime, Space.World);

        if (rotationY < 0.2f && rotationY > -0.2f)
        {
            _flSet = -.2f;
            _frSet = -.2f;
        }
        if (rotationY > .2f)
        {
            _flSet = -.2f;
            _frSet = 0f;
        }
        if (rotationY < -.2f)
        {
            _flSet = 0f;
            _frSet = -.2f;
        }
        _finL.transform.localPosition = Vector3.Slerp(_finL.transform.localPosition, new Vector3(_flSet, 0, _finOffset), .1f);
        _finR.transform.localPosition = Vector3.Slerp(_finR.transform.localPosition, new Vector3(_frSet, 0, -_finOffset), .1f);

    }

    void SideBurst(int dir)
    {
        _emitter.Play();
        Vector3 forceToAdd = transform.right * -dir * _sideBurst;
        _p.AddForce(forceToAdd);

    }


    void PartEngaged(GameObject _part)
    {
        _part.transform.localPosition = new Vector3(_eSet, _part.transform.localPosition.y, _part.transform.localPosition.z);
        badFmodUse.SetActive(true);

    }

    void PartDisEngaged(GameObject _part)
    {
        _part.transform.localPosition = new Vector3(0f, _part.transform.localPosition.y, _part.transform.localPosition.z);
        badFmodUse.SetActive(false);

    }

    void LimitSpeed()
    {
        float moveInput = Input.GetAxis("Vertical");

        // Apply force in the forward direction of the player's local coordinate system
        // Limit the maximum speed
        if (_p.velocity.magnitude > _cLimit)
        {
            //_p.velocity = _p.velocity.normalized * _cLimit; Smoother attempt below
            //_p.velocity = Vector3.Lerp(_p.velocity.normalized, _p.velocity.normalized* _cLimit, 100f);
            //_p.velocity *= .9999f; one more attempt
            //_p.drag=_cLimit;
            float forceMultiplier = _p.velocity.x * _cLimit - (_p.velocity.x / _cLimit);
            forwardForce.x = forceMultiplier;
        }

        else
        {
            forwardForce = transform.forward * moveInput * _speed;
            //_p.drag=1;
        }
        _p.AddForce(forwardForce * Time.deltaTime);
        _textSpeed.text = Mathf.RoundToInt(Mathf.Abs(_p.velocity.magnitude*10)).ToString() + " /// M P H /// ";



    }


}