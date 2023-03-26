using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAndCameraMotion : MonoBehaviour
{
    public float VerticalRotationSpeed;
    private float xRotation = 0f;

    private void Start()
    {
        VerticalRotationSpeed = 150f;
    }

    private void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * VerticalRotationSpeed * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
