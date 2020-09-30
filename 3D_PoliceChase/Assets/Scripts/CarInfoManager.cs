using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarInfoManager : MonoBehaviour
{
    public Slider sliderHP, sliderSpeed;
    public Car carInfo;
    private void Start()
    {
        sliderHP.value = carInfo.HP;
        sliderSpeed.value = carInfo.acceleration;
    }
}
