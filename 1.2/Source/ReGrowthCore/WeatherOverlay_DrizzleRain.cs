﻿using UnityEngine;
using Verse;

namespace ReGrowthCore
{
	public class WeatherOverlay_DrizzleRain : SkyOverlay
	{
		public WeatherOverlay_DrizzleRain()
		{
			worldOverlayMat = null;
			worldOverlayPanSpeed1 = 0.015f;
			worldPanDir1 = new Vector2(-0.25f, -1f);
			worldPanDir1.Normalize();
			worldOverlayPanSpeed2 = 0.022f;
			worldPanDir2 = new Vector2(-0.24f, -1f);
			worldPanDir2.Normalize();
		}

        public override void TickOverlay(Map map)
        {
			if (worldOverlayMat == null)
            {
				worldOverlayMat = MaterialPool.MatFrom("Weather/DrizzleRainWorldOverlay");
			}
			base.TickOverlay(map);
		}
	}
}
