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
	    setOriginalPartData(spaceship);
    }
	
    // A recursive function to set the original positions and rotations of the spacship parts. Called at the start of the game
    void setOriginalPartData(GameObject spaceshipPartObj)
    {
	    if (spaceshipPartObj.transform.childCount == 0)
	    { 
		   SpaceshipPart s = SpaceshipManager.GetSpaceshipPart(spaceshipPartObj.name);
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
		    setOriginalPartData(spaceshipPartObj.transform.GetChild(i).gameObject);
	    }
    }

    // Update is called once per frame
    void Update()
    {
		if (elapsedTime > SpaceshipManager.GetCrumbleTime(gameObject.name) && !SpaceshipManager.IsDropped(gameObject.name))
        {
        	Rigidbody cubeRigidbody = GetComponent<Rigidbody>();
        	cubeRigidbody.isKinematic = false;
            SpaceshipManager.RecordDropPartFromShip(gameObject.name);
        }
		elapsedTime += Time.deltaTime;
		
		//TODO remove this and only call it when the player brings back a piece
		if (elapsedTime > 7)
		{
			//GameObject first_piece = GameObject.Find("engine_frt_geo");
			//SpaceshipManager.GetSpaceshipPart("engine_frt_geo").ReturnPieceToShip(first_piece);
		}
    }
}
