using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public enum View { Full, HalfTop, HalfBottom, HalfLeft, HalfRight, QuarterTopLeft, QuarterTopRight, QuarterBottomLeft, QuarterBottomRight };
    [Header("Components")]
    public Transform camPos;
    [Header("Settings")]
    public View view = View.Full;
    [Range(1, 20)] public float followSpeed = 16;
    [Range(1, 20)] public float rotationSpeed = 12;
    public bool followRotation = true;
    private Vector3 _cameraPosOffset;
    private Vector3 _cameraRotOffset;
    public Camera vehicleCamera;
    private void Awake()
    {
        _cameraPosOffset = camPos.localPosition;
        _cameraRotOffset = camPos.localEulerAngles;
        vehicleCamera = camPos.GetChild(0).GetComponent<Camera>();
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        vehicleCamera.rect = new Rect(0, 0, 1, 1);
    }
    private void FixedUpdate()
    {
        camPos.position = Vector3.Lerp(camPos.position, transform.position + _cameraPosOffset, Time.fixedDeltaTime * followSpeed);
        if (followRotation)
        {
            camPos.rotation = Quaternion.Lerp(camPos.rotation, Quaternion.Euler(transform.eulerAngles + _cameraRotOffset), Time.fixedDeltaTime * rotationSpeed);
        }
    }
}
