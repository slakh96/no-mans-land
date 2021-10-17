using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace; 

public class DisableIsKinematic : MonoBehaviour
{
	private float elapsedTime;
	
    // Start is called before the first frame update
    void Start()
    {
	    elapsedTime = 0;
	    GameObject spaceship = GameObject.Find("spaceShip 1 2 1");
	    Debug.Log("Begin ======================================");
	    handleGameObjects(spaceship);
	    Debug.Log("End=========================================");
    }

    void handleGameObjects(GameObject spaceshipPartObj)
    {
	    if (spaceshipPartObj.transform.childCount == 0)
	    {
		   // Debug.Log(spaceshipPart.name);
		   SpaceshipPart s = SpaceshipTimekeeping.GetSpaceshipPart(spaceshipPartObj.name);
		   if (s == null)
		   {
			   Debug.Log("ERROR spaceship part not found in dictionary: " + spaceshipPartObj.name);
			   return;
		   }

		   s.SetOriginalRelativePosition(spaceshipPartObj.transform.localPosition);
		   s.SetOriginalRotation(spaceshipPartObj.transform.eulerAngles);
		   return;
	    }
	    for (int i = 0; i < spaceshipPartObj.transform.childCount; i++)
	    {
		    //Debug.Log(spaceshipPart.transform.GetChild(i).gameObject.name);
		    //Debug.Log(i);
		    handleGameObjects(spaceshipPartObj.transform.GetChild(i).gameObject);
	    }
	    //Debug.Log(i);
    }

    // Update is called once per frame
    void Update()
    {
		if (elapsedTime > SpaceshipTimekeeping.GetCrumbleTime(gameObject.name))
        {
        	Rigidbody cubeRigidbody = GetComponent<Rigidbody>();
        	cubeRigidbody.isKinematic = false;
        }
		elapsedTime += Time.deltaTime;
		if (elapsedTime < 2)
		{
			GameObject first_piece = GameObject.Find("engine_frt_geo");
			// Debug.Log(first_piece.transform.localPosition.x);
			// Debug.Log(first_piece.transform.localPosition.y);
			// Debug.Log(first_piece.transform.localPosition.z);
			// Debug.Log("===================================");
		}
		if (elapsedTime > 7)
		{
			GameObject first_piece = GameObject.Find("engine_frt_geo");
			SpaceshipTimekeeping.GetSpaceshipPart("engine_frt_geo").ReturnPieceToShip(first_piece);
			// first_piece.transform.localPosition = new Vector3(-0.05929808f, 0.02065961f, -0.0006725601f);
			// first_piece.transform.eulerAngles = new Vector3(0, 0, 0);
			// first_piece.GetComponent<Rigidbody>().isKinematic = true;
			// Debug.Log("===================================");
		}
    }
}
