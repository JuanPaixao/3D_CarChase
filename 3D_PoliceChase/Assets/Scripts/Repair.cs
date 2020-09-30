using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : MonoBehaviour
{
    public GameObject repairExplosion;
    private float _rotValue;
    private void Update()
    {
        _rotValue = Time.deltaTime * 110;
        transform.Rotate(new Vector3(this.transform.rotation.x, this.transform.rotation.y + _rotValue, this.transform.rotation.z));
    }
    public void HealPlayer()
    {
        Instantiate(repairExplosion, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
