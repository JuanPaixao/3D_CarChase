using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    public GameObject particleExplosionMoney;
    private float _rotValue;
    private void Update()
    {
        _rotValue = Time.deltaTime * 130;
        transform.Rotate(new Vector3(this.transform.rotation.x, this.transform.rotation.y + _rotValue, this.transform.rotation.z));
    }

    public void DestroyMe()
    {
        Instantiate(particleExplosionMoney, this.transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
