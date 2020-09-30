using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad dontDestroy;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (dontDestroy == null)
        {
            dontDestroy = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
