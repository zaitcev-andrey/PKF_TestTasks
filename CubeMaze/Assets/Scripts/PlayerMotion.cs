using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    private CharacterController controller;
    private float motionSpeed;
    private float gravity = -9.81f;
    private Vector3 velocity = Vector3.zero;
    private bool isGrounded;

    public Transform GroundCheck;
    public float GroundDistance = 0.6f;
    public LayerMask GroundMask;

    public float HorizontalRotationSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        motionSpeed = 7f;
        HorizontalRotationSpeed = 150f;
    }

    void Update()
    {
        // реализуем движение персонажа
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * motionSpeed * Time.deltaTime);

        // реализуем падение персонажа
        // если созданная сфера касается объектов с выбранным слоем, то true
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if(isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // реализуем вращение персонажа
        float yRotation = Input.GetAxis("Mouse X");
        Vector3 rotationOffset = new Vector3(0, yRotation * HorizontalRotationSpeed * Time.deltaTime, 0);
        transform.Rotate(rotationOffset);
    }
}
