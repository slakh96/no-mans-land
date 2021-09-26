using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject CollectiblePrefab;
    public int xPos;
    public int zPos;
    public int collectibleCount;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CollectibleDrop());
    }

    IEnumerator CollectibleDrop()
    {
        while (collectibleCount < 10)
        {
            xPos = UnityEngine.Random.Range(215, 760);
            zPos = UnityEngine.Random.Range(215, 780);


            Instantiate(CollectiblePrefab, new Vector3(xPos, 3, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            collectibleCount += 1;
        }
    }
    
}
