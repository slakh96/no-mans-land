using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
