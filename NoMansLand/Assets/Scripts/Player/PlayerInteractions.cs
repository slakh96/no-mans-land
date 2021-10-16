using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
public class PlayerInteractions : MonoBehaviour
{
    private PlayerControls controls;
    private Vector3 move;
    private Rigidbody rb;
    private bool isOnGround = true;

    private void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody>();

        controls.Gameplay.Grow.performed += ctx => Grow();
        
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector3.zero;

    }

    void Grow()
    {
        if (isOnGround)
        {
            rb.AddForce(Vector3.up * 10, ForceMode.VelocityChange);
        }
        isOnGround = false;
    }

    private void Update()
    {
        Vector3 m = new Vector3(move.x, 0, move.y) * Time.deltaTime * 50f;
        transform.Translate(m, Space.World);
        
        Vector3 currentPosition = transform.position;
        // Vector3 newPosition = new Vector3(move.x, 0, move.y) * 50f;
        
        Vector3 positionToLookAt = currentPosition + m;
        
        transform.LookAt(positionToLookAt);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Terrain")
        {
            isOnGround = true;
        }
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
