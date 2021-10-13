using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DefaultNamespace;

public class HandleObject : MonoBehaviour
{
    private GameObject collectibleHoldSlot;
    // The spaceship object - null if not in the game
    private GameObject spaceship;
    // How close the player needs to be before he can deposit the material successfully
    private float DISTANCE_LIMIT = 50;
    // Time bonus earned when the player deposits a material
    private float TIME_BONUS = 30;
    // Time to wait before the material is destroyed
    private float DESTROY_DELAY = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        collectibleHoldSlot = GameObject.Find("CollectibleHolder");
        spaceship = GameObject.FindGameObjectWithTag("Spaceship");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && collectibleHoldSlot.transform.childCount > 0)
        {
            GameObject child = collectibleHoldSlot.transform.GetChild(0).gameObject;
            child.transform.parent = null;
            Rigidbody collectibleRB = child.GetComponent<Rigidbody>();
            collectibleRB.isKinematic = false;
            collectibleRB.AddForce(-child.transform.forward * 20, ForceMode.Impulse);
            // Check if the player was close enough to the spaceship to deposit the material
            if (spaceship != null && 
                Vector3.Distance(child.gameObject.transform.position, spaceship.transform.position) <= DISTANCE_LIMIT)
            {
                SpaceshipTimekeeping.AddBonusTime(TIME_BONUS);
                Destroy(child, DESTROY_DELAY);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Collectible")
        {
            Transform collectibleObjTransform = other.gameObject.transform;
            collectibleObjTransform.parent = collectibleHoldSlot.transform;
            collectibleObjTransform.localPosition = Vector3.zero;
            Rigidbody collectibleRB = other.gameObject.GetComponent<Rigidbody>();
            collectibleRB.isKinematic = true;
        }
    }
}
