using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienIndicator : MonoBehaviour
{
    public static GameObject AlienIndicatorObject; 

    public static void SetAlienIndicatorActive() 
    {
        AlienIndicatorObject = GameObject.Find("AlienIndicatorCanvas");
        AlienIndicatorObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
