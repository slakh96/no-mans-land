using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using DefaultNamespace;
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
    // How close the player needs to be before he can deposit the material successfully
    private float DISTANCE_LIMIT = 50;
    // Time to wait before the material is destroyed
    private float DESTROY_DELAY = 1.0f;
    public GameObject goscreen;
    

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
            GameObject spaceship = GameObject.FindGameObjectWithTag("Spaceship");
            // Check if the player was close enough to the spaceship to deposit the material
            if (spaceship != null && 
                Vector3.Distance(child.gameObject.transform.position, spaceship.transform.position) <= DISTANCE_LIMIT)
            {
                SpaceshipManager.AddPartToShip();
                Destroy(child, DESTROY_DELAY);
            }
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
        if (other.collider.tag == "Alien") 
        {
            Destroy(this.gameObject);
            goscreen.SetActive(true);
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
