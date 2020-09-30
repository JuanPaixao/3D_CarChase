using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlatform : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
