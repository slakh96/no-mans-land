using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

public class RandomSpawner : MonoBehaviour
{
	public GameObject sampleSizeObj;
    public GameObject HealthItem; // the health regenerate item 
    public int HEALTH_ITEM_COUNT = 10; // number of health regenerate items spawned 
    public Terrain terrain;
    private List<GameObject> allSpaceShipParts = new List<GameObject>(); // A list of all the individual parts of the ship
    private List<GameObject> spaceShipPartsToSpawn = new List<GameObject>();
    public int terrainXPos; // corner X position of the terrain 
    public int terrainYPos; // corner Y position of the terrain 
    public int terrainZPos; // corner Z position of the terrain 
    public int terrainXLength; // length of terrain in X axis 
    public int terrainZLength; // length of terrain in Z axis 
    public int itemXPos; // x position of item 
    public int itemZPos; // z position of item 
    public const int cushionAmount = 50; // to ensure spawning does not happen at the edges (at the borders)

    private GameObject sampleObj;
    // Start is called before the first frame update
    void Start()
    {
        terrainXPos = (int)terrain.transform.position[0];
        terrainYPos = (int)terrain.transform.position[1];
        terrainZPos = (int)terrain.transform.position[2];

        terrainXLength = (int)terrain.terrainData.size[0];
        terrainZLength = (int)terrain.terrainData.size[2];
        StartCoroutine(CollectibleDrop());
        StartCoroutine(SpawnHealthItem());
    }

    IEnumerator CollectibleDrop()
    {
		GameObject spaceship = GameObject.FindGameObjectWithTag("Spaceship");
		if (spaceship == null)
		{
			Debug.Log("Spaceship is NULL; probably forgot the tag on the spaceship");
			yield break;
		}
		// Populate all the spaceship parts
        getAllSpaceshipParts(spaceship);
        getRelevantSpaceshipParts(spaceship);
        for (int i = 0; i < spaceShipPartsToSpawn.Count; i++)
        {
            // generate random X position in the range (terrainXPos + 50, terrainXPos + terrainXLength - 50)
            itemXPos = UnityEngine.Random.Range(terrainXPos + cushionAmount, terrainXPos + terrainXLength - cushionAmount); 
            // generate random Z position in the range (terrainZPos + 50, terrainZPos + terrainZLength - 50)
            itemZPos = UnityEngine.Random.Range(terrainZPos + cushionAmount, terrainZPos + terrainZLength - cushionAmount);
			// Get current material of original part
			Material currentMaterial = spaceShipPartsToSpawn[i].GetComponent<Renderer>().material; 

			// Duplicate each spaceship part and spawn to a random location as a collectible
            GameObject instantiatedClone = Instantiate(spaceShipPartsToSpawn[i], new Vector3(itemXPos, terrainYPos + 3, itemZPos), Quaternion.identity);
			// Resize clone to target size picked by us.
			instantiatedClone.transform.localScale = resizeObjToTarget(sampleSizeObj, instantiatedClone);
			instantiatedClone.tag = "Collectible";
			// Allow cloned part to respond to gravity
            instantiatedClone.GetComponent<Rigidbody>().isKinematic = false;
			// Set the material correctly
			spaceShipPartsToSpawn[i].GetComponent<Renderer>().material = currentMaterial;
			// Note: All parts must spawn before the first part falls off the ship TODO fix this later
            yield return new WaitForSeconds(0.01f);
        }
        
    }

    IEnumerator SpawnHealthItem()
    {
        for (int i = 0; i < HEALTH_ITEM_COUNT; i++)
        {
            // generate random X position in the range (terrainXPos + 50, terrainXPos + terrainXLength - 50)
            itemXPos = UnityEngine.Random.Range(terrainXPos + cushionAmount, terrainXPos + terrainXLength - cushionAmount); 
            // generate random Z position in the range (terrainZPos + 50, terrainZPos + terrainZLength - 50)
            itemZPos = UnityEngine.Random.Range(terrainZPos + cushionAmount, terrainZPos + terrainZLength - cushionAmount);

            GameObject instantiatedClone = Instantiate(HealthItem, new Vector3(itemXPos, terrainYPos + 150, itemZPos), Quaternion.identity);
			// Size it using the same scale as the actual ship
			instantiatedClone.tag = "HealthItem"; 
            yield return new WaitForSeconds(0.01f);
        }
    }
    
	// A recursive function to go through all the spaceship parts and add their gameObjects to the list
    private void getAllSpaceshipParts(GameObject spaceshipPartObj)
    {
        if (spaceshipPartObj.transform.childCount == 0)
        {
	        allSpaceShipParts.Add(spaceshipPartObj);

        }
        for (int i = 0; i < spaceshipPartObj.transform.childCount; i++)
        {
            getAllSpaceshipParts(spaceshipPartObj.transform.GetChild(i).gameObject);
        }
    }
    
    private void getRelevantSpaceshipParts(GameObject spaceshipPartObj)
    {
	    if (spaceshipPartObj.transform.childCount == 0)
	    {
		    if (!(spaceshipPartObj.name.Contains("polySurface8") || spaceshipPartObj.name.Contains("polySurface6") || spaceshipPartObj.name.Contains("polySurface2") || spaceshipPartObj.name.Contains("polySurface4") || spaceshipPartObj.name.Contains("pyCylinder10"))    )
		    {
			    spaceShipPartsToSpawn.Add(spaceshipPartObj);
		    }
		    else
		    {
			    bool found = false;
			    while (!found)
			    {
				    GameObject randomObj = allSpaceShipParts[UnityEngine.Random.Range(0,allSpaceShipParts.Count)];
				    if (!(randomObj.name.Contains("polySurface8") || randomObj.name.Contains("polySurface6") || randomObj.name.Contains("polySurface2") || randomObj.name.Contains("polySurface4") || randomObj.name.Contains("pyCylinder10"))    )
				    {
					    spaceShipPartsToSpawn.Add(randomObj);
					    found = true;
				    }
			    }
		    }
		    return;
	    }
	    for (int i = 0; i < spaceshipPartObj.transform.childCount; i++)
	    {
		    getRelevantSpaceshipParts(spaceshipPartObj.transform.GetChild(i).gameObject);
	    }
    }
    
    // Resize go to target size.
    private Vector3 resizeObjToTarget(GameObject target, GameObject go)
    {
	    Vector3 refSize = target.GetComponent<Renderer>().bounds.size;
		    float resizeX = refSize.x / go.GetComponent<Renderer>().bounds.size.x;
		    float resizeY = refSize.y / go.GetComponent<Renderer>().bounds.size.y;
		    float resizeZ = refSize.z / go.GetComponent<Renderer>().bounds.size.z;
 
		    resizeX *= go.transform.localScale.x;
		    resizeY *= go.transform.localScale.y;
		    resizeZ *= go.transform.localScale.z;

		    return new Vector3(resizeX, resizeY, resizeZ);
    }
    
}
