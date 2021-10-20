using System.Collections.Generic;
using UnityEngine;
namespace DefaultNamespace
{
    public static class SpaceshipManager
    {
		// A multiplier to control how long between each part crumbling. There are 54 parts total.
		// E.g. TIME_BETWEEN_CRUMBLES_SEC == 1.0f => 1 * 54s until spaceship finishes crumbling.
		// TIME_BETWEEN_CRUMBLES_SEC == 5.0f => 5 * 54s = 270s = 4min30s until it finishes crumbling. 
		static float TIME_BETWEEN_CRUMBLES_SEC = 5.0f;

		// A listing of all the parts that have dropped off the ship so far
		static List<string> droppedParts = new List<string>();
		
		// The crumble time to set the next added part of the ship to
		static int nextCrumbleTime = 55;
		
		// A boolean showing if the game is won
		static bool gameWon = false;
		
		// Time bonus earned when the player deposits a material
    	static float TIME_BONUS = 30;
		
		// The relative size of the spaceship and its parts
		public static float SPACESHIP_SCALE = 200.0f;		

		// A mapping from spaceship part name to spacship part object
		static Dictionary<string, SpaceshipPart> spaceshipParts =  
            new Dictionary<string, SpaceshipPart>(){
				{"engine_frt_geo", new SpaceshipPart(1)},
                {"engine_lft_geo", new SpaceshipPart(2)},
                {"engine_rt_geo", new SpaceshipPart(3)},
                {"mEngine_lft", new SpaceshipPart(4)},
                {"mEngine_rt", new SpaceshipPart(5)},
                {"tank_lft_geo", new SpaceshipPart(6)},
                {"tank_rt_geo", new SpaceshipPart(7)},
                {"elevon_lft_geo", new SpaceshipPart(8)},
                {"wingFlap_lft_geo", new SpaceshipPart(9)},
                {"elevon_rt_geo", new SpaceshipPart(10)},
                {"wingFlap_rt", new SpaceshipPart(11)},
				{"mainSpaceShuttleBody_geo", new SpaceshipPart(12)},
                {"pCube5", new SpaceshipPart(13)},
                {"pCube4", new SpaceshipPart(14)},
                {"pCylinder9", new SpaceshipPart(15)},
                {"pCylinder10", new SpaceshipPart(16)},
                {"pCylinder11", new SpaceshipPart(17)},
                {"pCylinder12", new SpaceshipPart(18)},
                {"pCube5 1", new SpaceshipPart(19)},
                {"pCube4 1", new SpaceshipPart(20)},
                {"pCylinder9 1", new SpaceshipPart(21)},
                {"pCylinder10 1", new SpaceshipPart(22)},
                {"pCylinder13", new SpaceshipPart(23)},
                {"pCylinder14", new SpaceshipPart(24)},
                {"polySurface17", new SpaceshipPart(25)},
                {"polySurface2", new SpaceshipPart(26)},
                {"polySurface4", new SpaceshipPart(27)},
                {"polySurface6", new SpaceshipPart(28)},
                {"polySurface8", new SpaceshipPart(29)},
                {"polySurface10", new SpaceshipPart(30)},
                {"polySurface12", new SpaceshipPart(31)},
                {"polySurface14", new SpaceshipPart(32)},
                {"polySurface16", new SpaceshipPart(33)},
                {"polySurface15", new SpaceshipPart(34)},
                {"polySurface2 1", new SpaceshipPart(35)},
                {"polySurface4 1", new SpaceshipPart(36)},
                {"polySurface6 1", new SpaceshipPart(37)},
                {"polySurface8 1", new SpaceshipPart(38)},
                {"polySurface10 1", new SpaceshipPart(39)},
                {"polySurface12 1", new SpaceshipPart(40)},
                {"polySurface14 1", new SpaceshipPart(41)},
                {"polySurface16 1", new SpaceshipPart(42)},
                {"polySurface15 1", new SpaceshipPart(43)},
                {"polySurface18", new SpaceshipPart(44)},
                {"polySurface2 2", new SpaceshipPart(45)},
                {"polySurface4 2", new SpaceshipPart(46)},
                {"polySurface6 2", new SpaceshipPart(47)},
                {"polySurface8 2", new SpaceshipPart(48)},
                {"polySurface10 2", new SpaceshipPart(49)},
                {"polySurface12 2", new SpaceshipPart(50)},
                {"polySurface14 2", new SpaceshipPart(51)},
                {"polySurface16 2", new SpaceshipPart(52)},
                {"polySurface15 2", new SpaceshipPart(53)},
                {"polySurface18 1", new SpaceshipPart(54)}
		};
		// A constant to add additional time until the next part falls off of the spaceship
        static float bonusTime = 0;
		
		// Gets the crumble time for an object with a particular name; returns 0 if object not found
        public static float GetCrumbleTime(string name)
        {
	        if (name.Contains("(Clone)"))
	        {
				// If the item has "Clone" in it, it is a collectible part to pick up -- should not crumble until after the end of
				// the game
		        return TIME_BETWEEN_CRUMBLES_SEC * spaceshipParts.Count + 1;
	        }

	        if (!spaceshipParts.ContainsKey(name))
			{
				Debug.Log("ERROR GetCrumbleTime: part with name " + name + " not found in dictionary");
				return 0;
			}
			return spaceshipParts[name].GetTimeToCrumble() * TIME_BETWEEN_CRUMBLES_SEC + bonusTime;
        }
		// Gets the spaceship part with name name, or returns null
		public static SpaceshipPart GetSpaceshipPart(string name) {
			if (!spaceshipParts.ContainsKey(name))
			{
				Debug.Log("ERROR GetCrumbleTime: part with name " + name + " not found in dictionary");
				return null;
			}
			return spaceshipParts[name];
		}
		// Adds additional time to wait before the next part falls off of the spaceship
		public static void addBonusTime()
		{
			bonusTime += TIME_BONUS;
		}
		// DropPartFromShip adds the dropped part to the list and makes it drop
		public static void DropPartFromShip(string partName) {
			droppedParts.Add(partName);
			GameObject part = GameObject.Find(partName);
			if (part == null)
			{
				Debug.Log("ERROR RecordDropPartFromShip: Part not found! " + partName);
				return;
			}
			part.GetComponent<Rigidbody>().isKinematic = false;
		}
		// AddPartToShip records that a part was added to the ship, adds the part back to the ship and returns the name
		// automatically selects the most recently dropped part to add back to the ship
		public static string AddPartToShip() {
			if (droppedParts.Count < 1){
				Debug.Log("ERROR AddPartToShip: No parts are off the ship, game should be over");
				return "";
			}
			string partName = droppedParts[droppedParts.Count - 1];
			droppedParts.RemoveAt(droppedParts.Count - 1);
			GameObject part = GameObject.Find(partName);
			SpaceshipManager.GetSpaceshipPart(partName).ReturnPieceToShip(part);
			SpaceshipManager.GetSpaceshipPart(partName).SetTimeToCrumble(nextCrumbleTime);
			nextCrumbleTime = nextCrumbleTime + 1;
			
			// Check if the game has been won, has the player added the final part
			if(droppedParts.Count == 0) {
				gameWon = true;
			}
			addBonusTime();
			return partName;
		}
		// IsDropped returns whether or not the part has already been dropped
		public static bool IsDropped(string partName) {
			return droppedParts.Contains(partName);
		}
		
    	// A recursive function to set the original positions and rotations of the spacship parts.
		// Must be called at the start of the game
    	public static void SetOriginalPartData(GameObject spaceshipPartObj)
    	{
	    	if (spaceshipPartObj.transform.childCount == 0)
	    	{ 
		   		SpaceshipPart s = GetSpaceshipPart(spaceshipPartObj.name);
		   		if (s == null)
		   		{
			   		Debug.Log("ERROR spaceship part not found in dictionary: " + spaceshipPartObj.name);
			   		return;
		   		}
		   		s.SetOriginalRelativePosition(spaceshipPartObj.transform.localPosition);
		   		s.SetOriginalRotation(spaceshipPartObj.transform.eulerAngles);
		   		return;
	    	}
	    	for (int i = 0; i < spaceshipPartObj.transform.childCount; i++)
	    	{
		    	SetOriginalPartData(spaceshipPartObj.transform.GetChild(i).gameObject);
	    	}
    	}
		// Returns whether all the parts are on the spaceship, thus the game is won
		public static bool SpaceshipComplete() 
		{
			return gameWon;
		}
		// Returns whether all the parts have fallen off the spaceship, thus the game is lost
		public static bool SpaceshipDestroyed()
		{
			return droppedParts.Count == spaceshipParts.Count;
		}
    }
}