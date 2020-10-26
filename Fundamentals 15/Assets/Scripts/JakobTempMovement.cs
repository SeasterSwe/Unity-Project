﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JakobTempMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    private float walkSpeed = 5;
    private float runSpeed = 8;
    float currentSpeed;
    float speedSmoothVelocity;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(x, y, 0);

        if (input.sqrMagnitude > 1)
            input = input.normalized;

        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * input.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, 0.3f);
        transform.Translate(input * currentSpeed * Time.deltaTime, Space.World);

        rb.velocity = Vector3.zero;
        LookAtMouse();
    }
    void LookAtMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }
}