using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelected : MonoBehaviour
{
    public string carSelected;
    public static CarSelected carSelectedObject;

    private void Awake()
    {
        carSelected = PlayerPrefs.GetString("carSelected", "Derby");
        DontDestroyOnLoad(this);

        if (carSelectedObject == null)
        {
            carSelectedObject = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateCar(string car)
    {
        carSelected = car;
        PlayerPrefs.SetString("carSelected", carSelected);
        Debug.Log(carSelected);
    }
}
