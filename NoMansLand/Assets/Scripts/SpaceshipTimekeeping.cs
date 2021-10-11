using System.Collections.Generic;
using UnityEngine;
namespace DefaultNamespace
{
    public static class SpaceshipTimekeeping
    {
		// A multiplier to control how long initially it takes for the ship to completely crumble, in units of 53s.
		// E.g. timeMultiplier == 1.0 => 1 * 53s until spaceship finishes crumbling.
		// timeMultiplier == 5.0 => 5 * 53s = 265s = 4min25s until it finishes crumbling. 
		static float crumbleTimeMultiplier = 5.0f;

		// A dictionary containing the order of parts to fall off
		static Dictionary<string, int> baseCrumbleTimes =  
            new Dictionary<string, int>(){
                {"engine_frt_geo", 1},
                {"engine_lft_geo", 2},
                {"engine_rt_geo", 3},
                {"mEngine_lft", 4},
                {"mEngine_rt", 5},
                {"tank_lft_geo", 6},
                {"tank_rt_geo", 7},
                {"elevon_lft_geo", 8},
                {"wingFlap_lft_geo", 9},
                {"elevon_rt_geo", 10},
                {"wingFlap_rt", 11},
                {"mainSpaceShuttleBody_geo", 12},
                {"pCube5", 13},
                {"pCube4", 14},
                {"pCylinder9", 15},
                {"pCylinder10", 16},
                {"pCylinder11", 17},
                {"pCylinder12", 18},
                {"pCube5 1", 19},
                {"pCube4 1", 20},
                {"pCylinder9 1", 21},
                {"pCylinder10 1", 22},
                {"pCylinder13", 23},
                {"pCylinder14", 24},
                {"polySurface17", 25},
                {"polySurface2", 26},
                {"polySurface4", 27},
                {"polySurface6", 28},
                {"polySurface8", 29},
                {"polySurface10", 30},
                {"polySurface12", 31},
                {"polySurface14", 32},
                {"polySurface16", 33},
                {"polySurface15", 34},
                {"polySurface2 1", 35},
                {"polySurface4 1", 36},
                {"polySurface6 1", 37},
                {"polySurface8 1", 38},
                {"polySurface10 1", 39},
                {"polySurface12 1", 40},
                {"polySurface14 1", 41},
                {"polySurface16 1", 42},
                {"polySurface15 1", 43},
                {"polySurface18", 44},
                {"polySurface2 2", 45},
                {"polySurface4 2", 46},
                {"polySurface6 2", 47},
                {"polySurface8 2", 48},
                {"polySurface10 2", 49},
                {"polySurface12 2", 50},
                {"polySurface14 2", 51},
                {"polySurface16 2", 52},
                {"polySurface15 2", 53},
                {"polySurface18 1", 54}
            };
		// A constant to add additional time until the next part falls off of the spaceship
        static float bonusTime = 0;
		
		// Gets the crumble time for an object with a particular name; returns 0 if object not found
        public static float GetCrumbleTime(string name)
        {
        	if (!baseCrumbleTimes.ContainsKey(name))
			{
				Debug.Log("GetCrumbleTime: part with name " + name + " not found in dictionary");
				return 0;
			}
			return baseCrumbleTimes[name] * crumbleTimeMultiplier + bonusTime;
        }
		// Adds additional time to wait before the next part falls off of the spaceship
		public static void AddBonusTime(float additionalTime)
		{
			bonusTime += additionalTime;
		}
    }
}