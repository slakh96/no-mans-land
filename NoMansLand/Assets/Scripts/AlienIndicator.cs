using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienIndicator : MonoBehaviour
{
    public static GameObject AlienIndicatorObjectDefault;
	public static GameObject AlienIndicatorObjectSeen; 

    static Dictionary<string, bool> seenStatus = new Dictionary<string, bool>();

    public static void AddToSeenStatus(string alien, bool seen) 
    {
        seenStatus[alien] = seen; 
    }

    public static void ActivateAlienIndicator()
   {
       AlienIndicatorObjectDefault = GameObject.FindGameObjectWithTag("AlienIndicator").transform.GetChild(0).gameObject;
       AlienIndicatorObjectSeen = GameObject.FindGameObjectWithTag("AlienIndicator").transform.GetChild(1).gameObject;
       if(checkIfAnyTrue()) 
       {
           AlienIndicatorObjectDefault.SetActive(false);
           AlienIndicatorObjectSeen.SetActive(true);
       }
       else {
           AlienIndicatorObjectDefault.SetActive(true);
           AlienIndicatorObjectSeen.SetActive(false);
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
