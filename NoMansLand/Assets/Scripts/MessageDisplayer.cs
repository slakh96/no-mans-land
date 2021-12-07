using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageDisplayer : MonoBehaviour
{
    public GameObject tutorialCanvas; 
    public GameObject spaceshipPart; 
    public GameObject healthItem; 
    public GameObject player;
    public GameObject sampleSizeObj;

    // Start is called before the first frame update
    private string[] TextToDisplay = new string[]{"hello.", "welcome to no mans land",
     "let's teach you to play!", "your task is to collect spaceship parts & repair the ship", 
     "you will find parts scattered around the map", "press Square to pick up an item", "here's an item. pick it up.",
     "good! now drop it off at the ship", "congrats! you're a natural!", "you will also find health items around the map", 
     "collect them to replenish your oxygen", "here's a health item, collect it", "good job!", 
     "the alien indicator will turn red if aliens are nearby", "press X to jump and L1 to sprint", "you're ready to play!", "let's start the game!"
     }; 

    void Start()
    {
        Debug.Log(player.transform.position);
        int i = 0;
        StartCoroutine(DisplayText(i));
        i++;
    }

    IEnumerator DisplayText(int i)
    {
        while(i < 7)
        {
            if(i == 6) 
            {
                GameObject samplePart = Instantiate(spaceshipPart, new Vector3(player.transform.position[0] - 5, player.transform.position[1] + 85, player.transform.position[2]), Quaternion.identity);
                samplePart.transform.localScale = resizeObjToTarget(sampleSizeObj, samplePart);
			    samplePart.tag = "Collectible";
			    // Allow insantiated part to respond to gravity
                samplePart.GetComponent<Rigidbody>().isKinematic = false;
                StartCoroutine(StartTutorialProcedure());
            } 
            // Wait for 4 seconds 
            yield return new WaitForSeconds(4f);
            // Display next instruction on screen
            tutorialCanvas.GetComponent<Text>().text = TextToDisplay[i++];
        }
    }

    IEnumerator StartTutorialProcedure() {
      yield return new WaitUntil(checkIfPickedUpItem);

      int i = 6; 
      while (i <= 7) 
      {
          // Display the next instruction on screen 
          tutorialCanvas.GetComponent<Text>().text = TextToDisplay[i++];
      }
    
      // Halt until player has deposited the item to the spaceship 
      yield return new WaitUntil(checkIfDeposited);
      i = 8;
      while (i < 12) 
      {
          // Display the next instruction on screen 
          tutorialCanvas.GetComponent<Text>().text = TextToDisplay[i++];
      }

      // Now instantiate the health item 
      GameObject instantiatedHealthItem = Instantiate(healthItem, new Vector3(player.transform.position[0] - 5, player.transform.position[1] + 65, player.transform.position[2]), Quaternion.identity);
      // Halt until the player has picked up the instantiated health item 
      yield return new WaitUntil(checkIfPickedUpHealthItem);
      i = 12;
      while (i < TextToDisplay.Length) 
      {
        // Display the next instruction on screen 
        tutorialCanvas.GetComponent<Text>().text = TextToDisplay[i++];
        // Wait for 4 seconds 
        yield return new WaitForSeconds(4);
      }
      // Wait for 2 seconds 
      yield return new WaitForSeconds(2);
      // Now Redirect user to Main Game Scene 
      MainMenuScript.StartGame();
   }

   // This helper function will determine when the player has picked up the health item 
   bool checkIfPickedUpHealthItem() {
      CharacterControllerMovement s = player.GetComponent<CharacterControllerMovement>();
      while (s.healthPickupSuccessful == false) {
          return false; 
      }
      return true; 
   }

   // This helper function will determine when the player has deposited the spaceship part 
   bool checkIfDeposited() {
      CharacterControllerMovement s = player.GetComponent<CharacterControllerMovement>();
      while (s.dropoffSuccessful == false) {
          return false; 
      }
      return true; 
   }

    // This helper function will determine if the player has picked up the sample item 
   bool checkIfPickedUpItem() {
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
