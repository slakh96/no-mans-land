using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

public class RandomSpawner : MonoBehaviour
{
    public GameObject CollectiblePrefab;
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

        Debug.Log(terrain.terrainData.size);
        Debug.Log(terrain.transform.position);
        StartCoroutine(CollectibleDrop());
    }

    IEnumerator CollectibleDrop()
    {
		GameObject spaceship = GameObject.FindGameObjectWithTag("Spaceship");
		// Populate all the spaceship parts
        getAllSpaceshipParts(spaceship);
        for (int i = 0; i < spaceShipParts.Count; i++)
        {
            // generate random X position in the range (terrainXPos + 100, terrainXPos + 900)
            itemXPos = UnityEngine.Random.Range(terrainXPos + cushionAmount, terrainXPos + terrainXLength - cushionAmount); 
            // generate random Z position in the range (terrainZPos + 100, terrainZPos + 988)
            itemZPos = UnityEngine.Random.Range(terrainZPos + cushionAmount, terrainZPos + terrainZLength - cushionAmount);

			// Duplicate each spaceship part and spawn to a random location as a collectible
            GameObject instantiatedClone = Instantiate(spaceShipParts[i], new Vector3(itemXPos, terrainYPos + 5, itemZPos), Quaternion.identity);
			// Size it using the same scale as the actual ship
			instantiatedClone.transform.localScale = new Vector3(SpaceshipManager.SPACESHIP_SCALE, SpaceshipManager.SPACESHIP_SCALE, SpaceshipManager.SPACESHIP_SCALE);
			instantiatedClone.tag = "Collectible";
			// Note: All parts must spawn before the first part falls off the ship TODO fix this later
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
