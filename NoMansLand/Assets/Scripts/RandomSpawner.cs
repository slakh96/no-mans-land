using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

public class RandomSpawner : MonoBehaviour
{
    public GameObject CollectiblePrefab;
    public GameObject HealthItem; // the health regenerate item 
    public int HEALTH_ITEM_COUNT = 5; // number of health regenerate items spawned 
    public Terrain terrain;
    private List<GameObject> spaceShipParts = new List<GameObject>(); // A list of all the individual parts of the ship
    public int terrainXPos; // corner X position of the terrain 
    public int terrainYPos; // corner Y position of the terrain 
    public int terrainZPos; // corner Z position of the terrain 
    public int terrainXLength; // length of terrain in X axis 
    public int terrainZLength; // length of terrain in Z axis 
    public int itemXPos; // x position of item 
    public int itemZPos; // z position of item 
    public const int cushionAmount = 50; // to ensure spawning does not happen at the edges (at the borders)
    
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
        for (int i = 0; i < spaceShipParts.Count; i++)
        {
            // generate random X position in the range (terrainXPos + 50, terrainXPos + terrainXLength - 50)
            itemXPos = UnityEngine.Random.Range(terrainXPos + cushionAmount, terrainXPos + terrainXLength - cushionAmount); 
            // generate random Z position in the range (terrainZPos + 50, terrainZPos + terrainZLength - 50)
            itemZPos = UnityEngine.Random.Range(terrainZPos + cushionAmount, terrainZPos + terrainZLength - cushionAmount);
			// Get current material of original part
			Material currentMaterial = spaceShipParts[i].GetComponent<Renderer>().material; 

			// Duplicate each spaceship part and spawn to a random location as a collectible
            GameObject instantiatedClone = Instantiate(spaceShipParts[i], new Vector3(itemXPos, terrainYPos + 3, itemZPos), Quaternion.identity);
			// Size it using the same scale as the actual ship
			instantiatedClone.transform.localScale = spaceship.transform.localScale;
			instantiatedClone.tag = "Collectible";
			// Allow cloned part to respond to gravity
            instantiatedClone.GetComponent<Rigidbody>().isKinematic = false;
			// Set the material correctly
			spaceShipParts[i].GetComponent<Renderer>().material = currentMaterial;
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

            GameObject instantiatedClone = Instantiate(HealthItem, new Vector3(itemXPos, terrainYPos + 5, itemZPos), Quaternion.identity);
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
            spaceShipParts.Add(spaceshipPartObj);
            return;
        }
        for (int i = 0; i < spaceshipPartObj.transform.childCount; i++)
        {
            getAllSpaceshipParts(spaceshipPartObj.transform.GetChild(i).gameObject);
        }
    }
    
}
