﻿using System.Collections.Generic;
using UnityEngine;
namespace DefaultNamespace
{
    public static class SpaceshipTimekeeping
    {
		// A multiplier to control how long initially it takes for the ship to completely crumble, in units of 54s.
		// E.g. timeMultiplier == 1.0 => 1 * 54s until spaceship finishes crumbling.
		// timeMultiplier == 5.0 => 5 * 54s = 270s = 4min30s until it finishes crumbling. 
		static float crumbleTimeMultiplier = 5.0f;
		
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
        	if (!spaceshipParts.ContainsKey(name))
			{
				Debug.Log("GetCrumbleTime: part with name " + name + " not found in dictionary");
				return 0;
			}
			return spaceshipParts[name].GetTimeToCrumble() * crumbleTimeMultiplier + bonusTime;
        }

		public static SpaceshipPart GetSpaceshipPart(string name) {
			if (!spaceshipParts.ContainsKey(name))
			{
				Debug.Log("GetCrumbleTime: part with name " + name + " not found in dictionary");
				return null;
			}
			return spaceshipParts[name];
		}
		// Adds additional time to wait before the next part falls off of the spaceship
		public static void AddBonusTime(float additionalTime)
		{
			bonusTime += additionalTime;
		}
    }
}