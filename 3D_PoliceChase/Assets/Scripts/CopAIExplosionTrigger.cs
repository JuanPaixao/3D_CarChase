using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopAIExplosionTrigger : MonoBehaviour
{
    private Rigidbody _rb;
    private CopAI _copAI;
    public int HP;
    public bool isExploding;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody>();
        _copAI = GetComponentInParent<CopAI>();
    }
    private void Update()
    {
        // Debug.Log(_rb.velocity.magnitude);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Rigidbody playerRB = other.GetComponent<Rigidbody>();
            Debug.Log(playerRB.velocity.magnitude);
            if (playerRB.velocity.magnitude >= 14.9f)
            {
                DamageCop();
                //  FindObjectOfType<CarDamageSystem>().Heal();
                // FindObjectOfType<CarDamageSystem>().Heal();

            }
        }
        else if (_rb.velocity.magnitude >= 16f)
        {
            DamageCop();
        }
        if (HP <= 0 && !isExploding)
        {
            _copAI.Explode();
            isExploding = true;
        }
    }
    public void DamageCop()
    {
        if (_copAI.closeToPlayer)
        {
            HP--;
        }
    }
}