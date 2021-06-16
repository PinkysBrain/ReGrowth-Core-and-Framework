using UnityEngine;
using Verse;

namespace ReGrowthCore
{
	public class WeatherOverlay_Blizzard : SkyOverlay
	{
		public WeatherOverlay_Blizzard()
		{
			worldOverlayMat = null;
			worldOverlayPanSpeed1 = 0.008f;
			worldPanDir1 = new Vector2(-0.5f, -1f);
			worldPanDir1.Normalize();
			worldOverlayPanSpeed2 = 0.009f;
			worldPanDir2 = new Vector2(-0.48f, -1f);
			worldPanDir2.Normalize();
		}

		public override void TickOverlay(Map map)
		{
			if (worldOverlayMat == null)
			{
				worldOverlayMat = MaterialPool.MatFrom("Weather/BlizzardWorldOverlay");
				worldOverlayMat.CopyPropertiesFromMaterial(MatLoader.LoadMat("Weather/SnowOverlayWorld"));
				worldOverlayMat.shader = MatLoader.LoadMat("Weather/SnowOverlayWorld").shader;
				worldOverlayMat.SetTexture("_MainTex", ContentFinder<Texture2D>.Get("Weather/BlizzardWorldOverlay"));
				worldOverlayMat.SetTexture("_MainTex2", ContentFinder<Texture2D>.Get("Weather/BlizzardWorldOverlay"));
				//this.worldOverlayMat.SetColor("_TuningColor", new Color(1, 1, 1, 1)); //0.2720588f, 0.2720588f, 0.2720588f, 0.05882353f));
			}
			base.TickOverlay(map);
		}
	}
}
