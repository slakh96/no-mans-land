using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject CollectiblePrefab;
    public GameObject terrain;
    private List<GameObject> spaceShipParts = new List<GameObject>();
    public int terrainXPos; // corner X position of the terrain 
    public int terrainYPos; // corner Y position of the terrain 
    public int terrainZPos; // corner Z position of the terrain 
    public int terrainXLength = 900; // length of terrain in X axis 
    public int terrainZLength = 988; // length of terrain in Z axis 
    public int itemXPos; // x position of item 
    public int itemZPos; // z position of item 
    public const int cushionAmount = 100; // to ensure spawning does not happen at the edges (at the borders)
    public int collectibleCount;
    
    // Start is called before the first frame update
    void Start()
    {
        terrainXPos = (int)terrain.transform.position[0];
        terrainYPos = (int)terrain.transform.position[1];
        terrainZPos = (int)terrain.transform.position[2];
        StartCoroutine(CollectibleDrop());
    }

    IEnumerator CollectibleDrop()
    {
		//Debug.Log("Before loading in spaceship");
		//yield return new WaitForSeconds(1.5f);
        GameObject spaceship = Instantiate(Resources.Load("spaceShip1211", typeof(GameObject))) as GameObject;
		Vector3 pos = spaceship.transform.position;
		pos.x = 12;
		spaceship.transform.position = pos;
		//Debug.Log("After loading in spaceship");
		//yield return new WaitForSeconds(1.5f);
		
		//GameObject spaceship = GameObject.FindGameObjectWithTag("Spaceship");
        getAllSpaceshipParts(spaceship);
        for (int i = 0; i < spaceShipParts.Count; i++)
        {
            // generate random X position in the range (terrainXPos + 100, terrainXPos + 900)
            itemXPos = UnityEngine.Random.Range(terrainXPos + cushionAmount, terrainXPos + terrainXLength); 
            // generate random Y position in the range (terrainZPos + 100, terrainZPos + 988)
            itemZPos = UnityEngine.Random.Range(terrainZPos + cushionAmount, terrainZPos + terrainZLength);
            spaceShipParts[i].transform.localScale = new Vector3(200.0f, 200.0f, 200.0f);
			spaceShipParts[i].tag = "Collectible";
            //Instantiate(CollectiblePrefab, new Vector3(itemXPos, terrainYPos + 10, itemZPos), Quaternion.identity);
            Instantiate(spaceShipParts[i], new Vector3(itemXPos, terrainYPos + 10, itemZPos), Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
            collectibleCount += 1;
        }
    }
    
    private void getAllSpaceshipParts(GameObject spaceshipPartObj)
    {
        if (spaceshipPartObj.transform.childCount == 0)
        {
            Debug.Log(spaceshipPartObj.name);
            spaceShipParts.Add(spaceshipPartObj);
            return;
        }
        for (int i = 0; i < spaceshipPartObj.transform.childCount; i++)
        {
            getAllSpaceshipParts(spaceshipPartObj.transform.GetChild(i).gameObject);
        }
    }
    
}
