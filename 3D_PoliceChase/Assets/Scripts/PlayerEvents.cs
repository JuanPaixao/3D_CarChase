using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public int score;
    private GameManager _gameManager;
    public CarDamageSystem carDamageSystem;
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MoneyBag"))
        {
            _gameManager.AddScore();
            other.GetComponent<MoneyBag>().DestroyMe();
        }
        if (other.CompareTag("Repair"))
        {
            if (!carDamageSystem.fullHP)
            {
                carDamageSystem.Heal();
                other.GetComponent<Repair>().HealPlayer();
            }
        }
    }
}
