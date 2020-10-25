﻿using UnityEngine;
using Verse;

namespace ReGrowthCore
{
	public class WeatherOverlay_Sandstorm : SkyOverlay
	{
		public WeatherOverlay_Sandstorm()
		{
			worldOverlayMat = null;
			worldOverlayPanSpeed1 = 0.0005f;
			worldOverlayPanSpeed2 = 0.0004f;
			worldPanDir1 = new Vector2(1f, 1f);
			worldPanDir2 = new Vector2(1f, -1f);
		}

		public override void TickOverlay(Map map)
		{
			if (worldOverlayMat == null)
			{
				worldOverlayMat = MaterialPool.MatFrom("Weather/SandstormWorldOverlay");
				worldOverlayMat.CopyPropertiesFromMaterial(MatLoader.LoadMat("Weather/FogOverlayWorld"));
				worldOverlayMat.shader = MatLoader.LoadMat("Weather/FogOverlayWorld").shader;
				worldOverlayMat.SetTexture("_MainTex", ContentFinder<Texture2D>.Get("Weather/SandstormWorldOverlay"));
				worldOverlayMat.SetTexture("_MainTex2", ContentFinder<Texture2D>.Get("Weather/SandstormWorldOverlay"));
				//this.worldOverlayMat.SetColor("_TuningColor", new Color(1, 1, 1, 1)); //0.2720588f, 0.2720588f, 0.2720588f, 0.05882353f));
			}
			base.TickOverlay(map);
		}
	}
}
