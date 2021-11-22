using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
public class CharacterControllerMovement : MonoBehaviour
{
    private CharacterController controller;
    public Transform cam;
    private PlayerControls controls;
    
    // Animator
    private Animator animator;

    // Move
    private Vector3 move;
    private float speed = 70f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    
    // Speed constants
    private float sprintSpeed = 110f;
    private float normalSpeed = 70f;
    public bool decreaseHealth;
    
    // Jump
    private Vector3 playerVelocity;
    private float jumpHeight = 4f;
    private float gravity = -39.81F;
    private float jumpScale = 0.01f;
    
    // Handle object
    private GameObject collectibleHoldSlot;
    private bool canGrab = false;
    private bool withinRange = false;
    private GameObject currentCollectible;
    // Health item
    public bool replenishHealth;

    // Spaceship interactions
    // How close the player needs to be before he can deposit the material successfully
    private float DISTANCE_LIMIT = 50;
    // Time to wait before the material is destroyed
    private float DEPOSITED_ITEM_DESTROY_DELAY = 0f;
    
    // Game Over Screen
    public GameObject goscreen;
    public GameObject healthbars;
    public GameObject compass;
    
    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();
        controller = GetComponent<CharacterController>();

        animator = GetComponent<Animator>();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector3.zero;
        
        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Sprint.performed += ctx => Sprint(true);
        controls.Gameplay.Sprint.canceled += ctx => Sprint(false);
        
        collectibleHoldSlot = GameObject.Find("CollectibleHolder");
        controls.Gameplay.HandleObject.performed += ctx => HandleObject();
    }
    
    void Jump()
    {
        if (controller.isGrounded)
        {
            animator.SetBool("isJumping", true);
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
    
    void Sprint(bool isSprint)
    {
        if (isSprint)
        {
            speed = sprintSpeed;
            jumpScale = 0.1f;
            jumpHeight = 2f;
            decreaseHealth = true;
        }
        else
        {
            speed = normalSpeed;
            jumpScale = 0.01f;
            jumpHeight = 6f;
            decreaseHealth = false;
        }
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
            collectibleObjTransform.position = collectibleHoldSlot.transform.position;
            collectibleObjTransform.localPosition = Vector3.zero;
            collectibleObjTransform.localRotation = Quaternion.identity;
            Rigidbody collectibleRB = currentCollectible.GetComponent<Rigidbody>();
            collectibleRB.isKinematic = true;
            // Randomize later
            FindObjectOfType<AudioManager>().Play("Pickup1");
            canGrab = false;
        }
        else if (collectibleHoldSlot.transform.childCount > 0)
        {
            GameObject child = collectibleHoldSlot.transform.GetChild(0).gameObject;
            child.transform.parent = null;
            Rigidbody collectibleRB = child.GetComponent<Rigidbody>();
            collectibleRB.isKinematic = false;
            collectibleRB.AddForce(child.transform.forward * 10, ForceMode.Impulse);
            
            FindObjectOfType<AudioManager>().Play("Drop1");
            
            GameObject spaceship = GameObject.FindGameObjectWithTag("Spaceship");
            // Check if the player was close enough to the spaceship to deposit the material
            if (spaceship != null && 
                Vector3.Distance(child.gameObject.transform.position, spaceship.transform.position) <= DISTANCE_LIMIT)
            {
                SpaceshipManager.AddPartToShip();
                FindObjectOfType<AudioManager>().Play("Deposit1");
                Destroy(child, DEPOSITED_ITEM_DESTROY_DELAY);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Collectible" || other.gameObject.tag == "HealthItem")
        {
            withinRange = false;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectible")
        {
            withinRange = true;
            currentCollectible = other.gameObject;
        }
        if (other.gameObject.tag == "HealthItem")
        {
            replenishHealth = true;
            FindObjectOfType<AudioManager>().Play("HealthPickup1");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Alien") 
        {
            FindObjectOfType<AudioManager>().Play("KilledByAlien1");
            Destroy(this.gameObject);
            goscreen.SetActive(true);
            healthbars.SetActive(false);
            compass.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 m = new Vector3(move.x, 0, move.y).normalized;

        if (m.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(m.x, m.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if (playerVelocity.y < 0)
            {
                animator.SetBool("isJumping", false);
            }
            moveDir.y += (playerVelocity.y * jumpScale) ;
            
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
            animator.SetBool("isJumping", false);
        }
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

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