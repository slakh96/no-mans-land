using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DefaultNamespace;

public class HandleObject : MonoBehaviour
{
    private GameObject collectibleHoldSlot;

    // Start is called before the first frame update
    void Start()
    {
        collectibleHoldSlot = GameObject.Find("CollectibleHolder");
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
            //Get position of child
            GameObject spaceship = GameObject.FindGameObjectWithTag("Spaceship");
            if (spaceship == null)
            {
                return;
            }
            float sxpos = spaceship.transform.position.x;
            float sypos = spaceship.transform.position.y;
            float szpos = spaceship.transform.position.z;
            float xpos = child.gameObject.transform.position.x;
            float ypos = child.gameObject.transform.position.y;
            float zpos = child.gameObject.transform.position.z;
            
            float DISTANCE_LIMIT = 50;
            float TIME_BONUS = 30;
            float DESTROY_DELAY = 1.0f;

            if (Vector3.Distance(child.gameObject.transform.position, spaceship.transform.position) <= DISTANCE_LIMIT)
            {
                SpaceshipTimekeeping.AddBonusTime(TIME_BONUS);
                Debug.Log("Added time bonus");
                Destroy(child, DESTROY_DELAY);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // if (gameObject.GetComponent<Collider>().tag == "Collectible")
        // {
        //     //Debug.Log("COllectible collided with something");
        //     //Debug.Log(other.collider.tag);
        //     Debug.Log(GameObject.FindGameObjectWithTag("Spaceship").transform.position);
        //     Debug.Log(GameObject.FindGameObjectWithTag("Player").transform.position);
        // }
        //Debug.Log(gameObject.GetComponent<Collider>().tag);
        //Debug.Log(other.collider.tag);
        if (other.collider.tag == "Collectible")
        {
            Transform collectibleObjTransform = other.gameObject.transform;
            collectibleObjTransform.parent = collectibleHoldSlot.transform;
            collectibleObjTransform.localPosition = Vector3.zero;
            Rigidbody collectibleRB = other.gameObject.GetComponent<Rigidbody>();
            collectibleRB.isKinematic = true;

        } else if (gameObject.GetComponent<Collider>().tag == "Collectible" && other.collider.tag == "Spaceship")
        {
            Debug.Log("Collectible collided with spaceship");
        }
    }
}
