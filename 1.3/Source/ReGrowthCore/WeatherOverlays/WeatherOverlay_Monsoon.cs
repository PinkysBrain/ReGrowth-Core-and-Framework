using UnityEngine;
using Verse;

namespace ReGrowthCore
{
	public class WeatherOverlay_Monsoon : SkyOverlay
	{
		public WeatherOverlay_Monsoon()
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
				worldOverlayMat = MaterialPool.MatFrom("Weather/MonsoonWorldOverlay");
				worldOverlayMat.CopyPropertiesFromMaterial(MatLoader.LoadMat("Weather/RainOverlayWorld"));
				worldOverlayMat.shader = MatLoader.LoadMat("Weather/RainOverlayWorld").shader;
				worldOverlayMat.SetTexture("_MainTex", ContentFinder<Texture2D>.Get("Weather/MonsoonWorldOverlay"));
				worldOverlayMat.SetTexture("_MainTex2", ContentFinder<Texture2D>.Get("Weather/MonsoonWorldOverlay"));
				//this.worldOverlayMat.SetColor("_TuningColor", new Color(1, 1, 1, 1)); //0.2720588f, 0.2720588f, 0.2720588f, 0.05882353f));
			}
			base.TickOverlay(map);
		}
	}
}
