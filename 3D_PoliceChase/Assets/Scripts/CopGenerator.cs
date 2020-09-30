using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopGenerator : MonoBehaviour
{
    private CopPool _copPool;
    public float spawnTime, firstSpawn;
    public GameObject[] copSpawnPoints;

    private void Awake()
    {
        _copPool = FindObjectOfType<CopPool>();
        copSpawnPoints = GameObject.FindGameObjectsWithTag("CopSpawn");
    }
    private void Start()
    {
        InvokeRepeating("CreateCop", firstSpawn, spawnTime);
    }
    public void CreateCop()
    {
        if (_copPool.HasCops())
        {
            int randomPos = Random.Range(0, copSpawnPoints.Length);
            GameObject police = _copPool.CreateCop();
            CopAI cop = police.GetComponent<CopAI>();
            police.transform.position = copSpawnPoints[randomPos].transform.position;
            police.SetActive(true);
            if (spawnTime >= 0.5f)
            {
                spawnTime -= 0.05f;
            }
        }
    }
}
