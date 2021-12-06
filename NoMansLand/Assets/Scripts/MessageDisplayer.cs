using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageDisplayer : MonoBehaviour
{
    public GameObject tutorialCanvas; 
    public GameObject spaceshipPart; 
    public GameObject player;
    public GameObject sampleSizeObj;
    private bool hasPickedUp = false; 

    // Start is called before the first frame update
    private string[] TextToDisplay = new string[]{"hello.", "welcome to no mans land",
     "let's teach you to play!", "your task is to collect spaceship parts & repair the ship", 
     "you will find parts scattered around the map", "press Square to pick up an item", "here's an item. pick it up.",
     "congrats! now drop it off at the ship"}; 

    void Start()
    {
        Debug.Log(player.transform.position);
        int i = 3;
        StartCoroutine(DisplayText(i));
        i++;
    }

    IEnumerator DisplayText(int i)
    {
        bool display = true;
        while(i < 7)
        {
            if(i == 6) 
            {
                Debug.Log("Spawned");
                GameObject instantiatedClone = Instantiate(spaceshipPart, new Vector3(player.transform.position[0], player.transform.position[1] + 65, player.transform.position[2]), Quaternion.identity);
                instantiatedClone.transform.localScale = resizeObjToTarget(sampleSizeObj, instantiatedClone);
			    instantiatedClone.tag = "Collectible";
			    // Allow cloned part to respond to gravity
                instantiatedClone.GetComponent<Rigidbody>().isKinematic = false;
                display = false; 
                StartCoroutine(StartPickupWait());
                // while(!s.pickupSuccessful) 
                // {
                //     Debug.Log("Didn't pick up yet!");
                // }
                // Debug.Log("Picked up!");
            } 
            Debug.Log(i);
            yield return new WaitForSeconds(4f);
            tutorialCanvas.GetComponent<Text>().text = TextToDisplay[i];
            i = i + 1;
        }
    }

    IEnumerator StartPickupWait() {
      Debug.Log("Start");
 
      yield return new WaitUntil(CheckIfPlayerPickedUpItem);
 
      Debug.Log("Finish");
      hasPickedUp = true;
      int i = 6; 
      while (i < TextToDisplay.Length) 
      {
          tutorialCanvas.GetComponent<Text>().text = TextToDisplay[i];
          i = i + 1;
      }
      
   }

    //Returns false while the player still hasn't picked up the item, and true when they have 
   bool CheckIfPlayerPickedUpItem() {
      CharacterControllerMovement s = player.GetComponent<CharacterControllerMovement>();
      while (s.pickupSuccessful == false) {
          return false; 
      }
      return true; 
   }

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
