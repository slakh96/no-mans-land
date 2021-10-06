using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIsKinematic : MonoBehaviour
{
	public GameObject cube;
	public float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (time <= 0f)
        {
        	Rigidbody cubeRigidbody = GetComponent<Rigidbody>();
        	cubeRigidbody.isKinematic = false;
        }
        time -= Time.deltaTime;
    }
}
