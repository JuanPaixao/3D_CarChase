using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CopAI : MonoBehaviour
{
    public float speed, rotationSpeed;
    [SerializeField] private GameObject _target;
    private Rigidbody _rb;
    public Transform vehicleModel;
    public Transform target;
    public float distance;
    public Vector3 pointTarget;
    public Transform front;
    private ParticleSystem _particleLeft, _particleRight;
    public bool isDrifting;
    public Vector3 lastPos;
    public GameObject explosion;
    public bool closeToPlayer;
    public CopPool copPool;
    private CopAIExplosionTrigger _copExplosionTrigger;
    private AudioSource _driftAudioSource;
    public GameObject repair;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Target").transform;
        _copExplosionTrigger = GetComponentInChildren<CopAIExplosionTrigger>();
        _driftAudioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        Transform[] vehicleComponentes = gameObject.GetComponentsInChildren<Transform>();
        foreach (var item in vehicleComponentes)
        {
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
                //   _driftAudioSource = item.GetComponent<AudioSource>();
            }
        }
    }
    private void Update()
    {
        distance = Vector3.Distance(this.transform.position, target.position);
        closeToPlayer = distance < 12.5f;
        if (Vector3.Angle(_rb.velocity, vehicleModel.forward) > 35f && _rb.velocity.magnitude > speed / 4.5f) //trail/particle according to curve angle and speed
        {
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
            if (!isDrifting)
            {
                _driftAudioSource.volume -= Time.deltaTime * 1.5f;
            }
            if (_driftAudioSource.volume < 0.01f)
            {
                _driftAudioSource.Stop();
            }
        }
        float distanceToObstacle = 0;
        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        RaycastHit hit;
        if (Physics.SphereCast(front.transform.position, 1, transform.forward, out hit, 10))
        {
            distanceToObstacle = hit.distance;
        }
        Vector3 v = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, v.y, 0);
    }

    private void FixedUpdate()
    {
        var lastPos = FindObjectOfType<LastPosition>().lastPosition;
        if (distance > 15)
        {
            pointTarget = transform.position - lastPos;
        }
        else
        {
            pointTarget = transform.position - target.position;
        }
        pointTarget.Normalize();
        float value = Vector3.Cross(pointTarget, transform.forward).y;
        _rb.angularVelocity = rotationSpeed * value * new Vector3(0, 1, 0);
        if (distance < 50 && distance > 2f)
        {
            _rb.AddForce(this.transform.forward * (speed + 15f), ForceMode.Acceleration);
        }
        else
        {
            _rb.AddForce(this.transform.forward * speed / 5, ForceMode.Acceleration);
        }
        _rb.AddForce(Vector3.down * 9.8f, ForceMode.Acceleration);
    }
    public void Explode()
    {
        Debug.Log("exploding");
        Instantiate(explosion, this.transform.position, Quaternion.identity);
        Instantiate(repair, this.transform.position, Quaternion.identity);
        if (distance < 12)
        {
            FindObjectOfType<RipplePostProcessor>().ExplosionRippleEffect();

        }
        Invoke("Deactive", 0.3f);
    }
    public void Deactive()
    {
        FindObjectOfType<GameManager>().copsActive -= 1;
        _copExplosionTrigger.HP = 1;
        _copExplosionTrigger.isExploding = false;
        copPool.ReturnCop(this.gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<CarDamageSystem>().TakeDamage();
        }
    }
}

