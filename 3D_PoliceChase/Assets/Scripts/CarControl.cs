using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    [Header("Components")]
    public Transform vehicleModel;
    public Rigidbody refSphere;
    [Header("Controls")]

    public KeyCode accelerate;
    public KeyCode brake;
    public KeyCode steerLeft;
    public KeyCode steerRight;
    [Header("Parameters")]
    [SerializeField] float _speed, _speedTarget;
    [SerializeField] float _rotate, _rotateTarget;
    [Range(5.0f, 40.0f)] public float acceleration = 30f;
    [Range(1.0f, 15.0f)] public float accelerationMultiplier = 12f;
    [Range(0.0f, 40.0f)] public float brakeForce = 30;
    [Range(20.0f, 160.0f)] public float steering = 80f;
    [Range(50.0f, 80.0f)] public float jumpForce = 60f;
    [Range(0.0f, 20.0f)] public float gravity = 10f;
    [Range(0.0f, 1.0f)] public float drift = 1f;

    public bool steerInAir;
    [SerializeField] private bool nearGround, onGround;
    public Transform frontLeftWheel, frontRightWheel, backLeftWheel, backRightWheel, body;
    private int _direction;
    private TrailRenderer _trailLeft, _trailRight;
    private int _control;
    private ParticleSystem _particleLeft, _particleRight;
    private AudioSource _engineAudioSource, _driftAudioSource;
    [SerializeField] private float _pitchSound = 0.4f;
    public int gear;
    public float[] gearValues;
    public bool isDrifting;
    public Car carComponents;
    //Mobile
    public bool steeringLeft, steeringRight;
    public bool movingForward;
    private void Awake()
    {
        _engineAudioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        GetCarStats();
        Transform[] vehicleComponentes = gameObject.GetComponentsInChildren<Transform>();
        foreach (var item in vehicleComponentes)
        {
            if (item.name == "FrontLeftWheel")
            {
                frontLeftWheel = item;
            }
            if (item.name == "FrontRightWheel")
            {
                frontRightWheel = item;
            }
            if (item.name == "BackLeftWheel")
            {
                backLeftWheel = item;
            }
            if (item.name == "BackRightWheel")
            {
                backRightWheel = item;
            }
            if (item.name == "Body")
            {
                body = item;
            }
            if (item.name == "TrailLeft")
            {
                _trailLeft = item.GetComponent<TrailRenderer>();
            }
            if (item.name == "TrailRight")
            {
                _trailRight = item.GetComponent<TrailRenderer>();
            }
            if (item.name == "ParticleLeft")
            {
                _particleLeft = item.GetComponent<ParticleSystem>();
            }
            if (item.name == "ParticleRight")
            {
                _particleRight = item.GetComponent<ParticleSystem>();
            }
            if (item.name == "Container")
            {
                _driftAudioSource = item.GetComponent<AudioSource>();
            }
        }
    }
    private void Update()
    {
        Acceleration();
        Steering();
        CarSound();

        if (Vector3.Angle(refSphere.velocity, vehicleModel.forward) > 40f && onGround && refSphere.velocity.magnitude > acceleration / 4f) //trail/particle according to curve angle and speed
        {
            _trailLeft.emitting = _trailRight.emitting = true;
            _particleRight.Play();
            _particleLeft.Play();
            isDrifting = true;
            if (isDrifting)
            {
                _driftAudioSource.volume = 1f;
            }
            if (!_driftAudioSource.isPlaying)
            {
                _driftAudioSource.Play();
            }
        }
        else
        {
            isDrifting = false;
            _trailLeft.emitting = _trailRight.emitting = false;
            if (!isDrifting)
            {
                _driftAudioSource.volume -= Time.deltaTime * 1.5f;
            }
            if (_driftAudioSource.volume < 0.01f)
            {
                _driftAudioSource.Stop();
            }
        }
    }
    private void FixedUpdate()
    {
        GroundCheck();
        Movement();
        if (_speed == 0 && refSphere.velocity.magnitude < 4f) //smooth "brake"
        {
            refSphere.velocity = Vector3.Lerp(refSphere.velocity, Vector3.zero, Time.deltaTime * 2.0f);
        }
    }
    public void GetCarStats()
    {
        acceleration = carComponents.acceleration;
        accelerationMultiplier = carComponents.accelerationMultiplier;
        brakeForce = carComponents.brakeForce;
        steering = carComponents.steering;
        jumpForce = carComponents.jumpForce;
        gravity = carComponents.gravity;
        drift = carComponents.drift;
        MeshFilter bodyModel = this.body.GetComponent<MeshFilter>();
        bodyModel.mesh = carComponents.GetMeshColor();
    }
    public void DigitalAccelerateButtonOn()
    {
        movingForward = true;
    }
    public void DigitalAccelerateButtonOff()
    {
        movingForward = false;
    }
    public void SteerLeft(bool steer)
    {
        steeringLeft = steer;
    }
    public void SteerRight(bool steer)
    {
        steeringRight = steer;
    }
    public void Acceleration()
    {
        _speedTarget = Mathf.SmoothStep(_speedTarget, _speed, Time.deltaTime */*offset*/ accelerationMultiplier); //acceleration
        _speed = 0f;

        if (Input.GetKey(accelerate) || movingForward) //control forward and backward movement
        {
            _speed = acceleration;
            if (_speedTarget > 0)
            {
                _control = 1;
            }
        }
        if (Input.GetKey(brake)) //brake
        {
            _speed = -brakeForce;
            if (_speedTarget < 0)
            {
                _speed = -acceleration / 2;
                _control = -1;
            }
        }
    }
    public void Steering()
    {
        _rotateTarget = Mathf.Lerp(_rotateTarget, _rotate, Time.deltaTime * 4f); //rotate on steering, to left or right
        _rotate = 0f;
        if (Input.GetKey(steerLeft) || steeringLeft)
        {
            ControlSteer(-1);
        }
        if (Input.GetKey(steerRight) || steeringRight)
        {
            ControlSteer(1);
        }

        VehicleBodyControl(_control); //reverse wheels when reversing

    }
    public void ControlSteer(int direction) //steer left or right (-1,1)
    {
        this._direction = direction;
        if (nearGround || steerInAir)
        {
            _rotate = steering * direction; // rotation according to my direction (-1/1) and steering (aka turning speed)
        }
        if (Input.GetKey(brake)) //reverse in case of reversing
        {
            if (_speedTarget < 0.1)
            {
                _rotate = steering * -direction;
            }
        }
    }
    public void GroundCheck()
    {
        RaycastHit hitOnGround;
        RaycastHit hitNearGround;

        onGround = Physics.Raycast(transform.position, Vector3.down, out hitOnGround, 0.5f);
        nearGround = Physics.Raycast(transform.position, Vector3.down, out hitNearGround, 1.5f);

        vehicleModel.up = Vector3.Lerp(vehicleModel.up, hitNearGround.normal, Time.deltaTime * 8.0f); //setting my vehicle normals up alligned with the ground
        vehicleModel.Rotate(0, transform.eulerAngles.y, 0); //rotate my vehicle according to my main Y
    }
    public void Movement()
    {
        if (nearGround)
        {
            refSphere.AddForce(vehicleModel.forward * _speedTarget, ForceMode.Acceleration); //add force on my vehicle body forward direction
        }
        else
        {
            refSphere.AddForce(vehicleModel.forward * (_speedTarget / 10), ForceMode.Acceleration);
            refSphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
        this.transform.position = refSphere.transform.position + new Vector3(0, -0.7f, 0); //set this position to sphere position + offset
        //Drag on ground
        Vector3 localVelocity = transform.InverseTransformVector(refSphere.velocity);
        localVelocity.x *= 0.9f + (drift / 10);

        if (nearGround)
        {
            refSphere.velocity = transform.TransformVector(localVelocity);
        }
    }
    public void VehicleBodyControl(int control) //body animation control
    {
        frontRightWheel.transform.localRotation = frontLeftWheel.transform.localRotation = Quaternion.Euler(refSphere.velocity.magnitude * 100, control * _rotateTarget / 2.5f, this.transform.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, transform.eulerAngles.y + _rotateTarget, 0)), Time.deltaTime * 2.0f);
        if (refSphere.velocity.magnitude > 0.05)
        {
            body.localRotation = Quaternion.Slerp(body.localRotation, Quaternion.Euler(new Vector3(_speedTarget / 4, 0, _rotateTarget / 3.25f)), Time.deltaTime * 4.0f);
        }
        backLeftWheel.transform.Rotate(new Vector3(refSphere.velocity.magnitude * 100 * Time.deltaTime, 0, 0));
        backRightWheel.transform.Rotate(new Vector3(refSphere.velocity.magnitude * 100 * Time.deltaTime, 0, 0));
    }
    public void CarSound()
    {
        _pitchSound = refSphere.velocity.magnitude / 1.65f / gearValues[gear];

        if (_pitchSound < 0.275f && gear == 1)
        {
            _pitchSound = 0.275f;
        }

        if (_pitchSound > 0.825f)
        {
            UpGear();
        }

        if (gear == 2)
        {
            if (_pitchSound < 0.4f)
            {
                DownGear();
            }
        }
        if (gear == 3)
        {
            if (_pitchSound < 0.5f)
            {
                DownGear();
            }
        }
        if (gear == 4)
        {
            if (_pitchSound < 0.6f)
            {
                DownGear();
            }
        }
        if (gear == 5)
        {
            if (_pitchSound < 0.7f)
            {
                DownGear();
            }
        }
        _engineAudioSource.pitch = _pitchSound;
    }

    public void UpGear()
    {
        if (gear < gearValues.Length - 1)
        {
            gear++;
        }
    }
    public void DownGear()
    {
        if (gear > 0)
        {
            gear--;
        }
    }
    public float ReturnSpeed()
    {
        return this.refSphere.velocity.magnitude;
    }
}
