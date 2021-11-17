using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienIndicator : MonoBehaviour
{
    public static GameObject AlienIndicatorObject; 

    static Dictionary<string, bool> seenStatus = new Dictionary<string, bool>();

    public static void AddToSeenStatus(string alien, bool seen) 
    {
        seenStatus[alien] = seen; 
    }

    public static void ActivateAlienIndicator()
    {
        AlienIndicatorObject = GameObject.FindGameObjectWithTag("AlienIndicator").transform.GetChild(0).gameObject;
        if(checkIfAnyTrue()) 
        {
            AlienIndicatorObject.SetActive(true);
        }
        else {
            AlienIndicatorObject.SetActive(false);
        }
    }

    public static bool checkIfAnyTrue() 
    {
        foreach(KeyValuePair<string, bool> entry in seenStatus)
        {
            if(entry.Value == true) 
            {
                return true; 
            }
        }
        return false; 
    }
}
