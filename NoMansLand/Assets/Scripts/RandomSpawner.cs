using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject CollectiblePrefab;
    public GameObject terrain; 
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
        while (collectibleCount < 10)
        {
            // generate random X position in the range (terrainXPos + 100, terrainXPos + 900)
            itemXPos = UnityEngine.Random.Range(terrainXPos + cushionAmount, terrainXPos + terrainXLength); 
            // generate random Y position in the range (terrainZPos + 100, terrainZPos + 988)
            itemZPos = UnityEngine.Random.Range(terrainZPos + cushionAmount, terrainZPos + terrainZLength);

            Instantiate(CollectiblePrefab, new Vector3(itemXPos, terrainYPos + 10, itemZPos), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            collectibleCount += 1;
        }
    }
    
}
