using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            Destroy(gameObject);
        } 
    }
}
