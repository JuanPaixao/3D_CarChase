using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDamageSystem : MonoBehaviour
{
    private CarControl _carControl;
    public GameObject smokeDamage;
    [Range(1f, 20f)] public int HP;
    [Range(1f, 3f)] public float cooldown = 1;
    [SerializeField] private float _cooldownToTakeDamage;
    private int _initialHP;
    private UIManager _uiManager;
    public bool fullHP;
    private AudioSource _audioSource;
    public AudioClip hitSound;
    private void Awake()
    {
        _carControl = FindObjectOfType<CarControl>();
        _uiManager = FindObjectOfType<UIManager>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        HP = this._carControl.carComponents.HP;
        _initialHP = HP;
        _cooldownToTakeDamage = cooldown;
        _uiManager.SetMaxHP(HP);
        _uiManager.SetHP(HP);
    }
    private void Update()
    {
        _cooldownToTakeDamage -= Time.deltaTime;

        if (this.HP <= 0)
        {
            FindObjectOfType<CarControl>().enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
        if (this.HP == _initialHP)
        {
            fullHP = true;
        }
        else if (HP < _initialHP)
        {
            fullHP = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (_carControl.ReturnSpeed() > 10f)
        {
            TakeDamage();
        }
        if (other.CompareTag("Cop"))
        {
            TakeDamage();
        }
    }
    public void Heal()
    {
        if (HP < _initialHP)
        {
            HP++;
            _uiManager.SetHP(HP);
            if (HP >= _initialHP / 2)
            {
                smokeDamage.SetActive(false);
            }
            else
            {
                smokeDamage.SetActive(true);
            }
        }
    }
    public void TakeDamage()
    {
        if (_cooldownToTakeDamage < 0f)
        {
            HP--;
            _cooldownToTakeDamage = cooldown;
            if (HP <= _initialHP / 2)
            {
                smokeDamage.SetActive(true);
            }
            else
            {
                smokeDamage.SetActive(false);
            }
            _audioSource.PlayOneShot(hitSound);
            _uiManager.SetHP(HP);
        }
    }
}
