using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarInfo", menuName = "ScriptableObjects/Car", order = 1)]
public class Car : ScriptableObject
{
    [SerializeField] float _speed, _speedTarget;
    [SerializeField] float _rotate, _rotateTarget;
    [Range(5.0f, 40.0f)] public float acceleration = 30f;
    [Range(1.0f, 15.0f)] public float accelerationMultiplier = 12f;
    [Range(0.0f, 40.0f)] public float brakeForce = 30;
    [Range(20.0f, 160.0f)] public float steering = 80f;
    [Range(50.0f, 80.0f)] public float jumpForce = 60f;
    [Range(0.0f, 20.0f)] public float gravity = 10f;
    [Range(0.0f, 1.0f)] public float drift = 1f;
    [Range(0, 20)] public int HP = 10;
    public int price;
    public Mesh[] model;
    public Mesh GetMeshColor()
    {
        int randomColor = Random.Range(0, model.Length);
        return model[randomColor];
    }
}
