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
    // Jump variables
    private bool isOnGround = true;
    // Pick up or drop object variables
    private GameObject collectibleHoldSlot;
    private bool canGrab = false;
    private bool withinRange = false;
    private GameObject currentCollectible;
    

    private void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody>();

        controls.Gameplay.Jump.performed += ctx => Jump();
        
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector3.zero;
        
        collectibleHoldSlot = GameObject.Find("CollectibleHolder");
        controls.Gameplay.HandleObject.performed += ctx => HandleObject();

    }

    void Jump()
    {
        if (isOnGround)
        {
            rb.AddForce(Vector3.up * 10, ForceMode.VelocityChange);
        }
        isOnGround = false;
    }
    
    void HandleObject()
    {

        if (withinRange)
        {
            canGrab = true;
        }
        else
        {
            canGrab = false;
        }
        
        
        if (canGrab && collectibleHoldSlot.transform.childCount == 0)
        {
            Transform collectibleObjTransform = currentCollectible.transform;
            collectibleObjTransform.parent = collectibleHoldSlot.transform;
            collectibleObjTransform.localPosition = Vector3.zero;
            Rigidbody collectibleRB = currentCollectible.GetComponent<Rigidbody>();
            collectibleRB.isKinematic = true;
            canGrab = false;
        }
        else if (collectibleHoldSlot.transform.childCount > 0)
        {
            GameObject child = collectibleHoldSlot.transform.GetChild(0).gameObject;
            child.transform.parent = null;
            Rigidbody collectibleRB = child.GetComponent<Rigidbody>();
            collectibleRB.isKinematic = false;
            collectibleRB.AddForce(child.transform.forward * 20, ForceMode.Impulse);
        }
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

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Collectible")
        {
            withinRange = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Terrain")
        {
            isOnGround = true;
        }
        if (other.gameObject.tag == "Collectible")
        {
            withinRange = true;
            currentCollectible = other.gameObject;
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
