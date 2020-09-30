using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPosition : MonoBehaviour
{
    public float timer;
    public Vector3 currentPositon, lastPosition;
    void Start()
    {
        lastPosition = currentPositon;
        timer = 0;
    }

    void Update()
    {
        currentPositon = this.transform.position;
        timer-=Time.deltaTime;
        if (timer <= 0)
        {
            lastPosition = currentPositon;
            timer = 1;
        }
    }
}
