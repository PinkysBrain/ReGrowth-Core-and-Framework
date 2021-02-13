using UnityEngine;
using Verse;

namespace ReGrowthCore
{
	public class WeatherOverlay_Sandstorm : SkyOverlay
	{
		public WeatherOverlay_Sandstorm()
		{
			worldOverlayMat = null;
			worldOverlayPanSpeed1 = 0.015f;
			worldPanDir1 = new Vector2(0.25f, 0);
			worldPanDir1.Normalize();
			worldOverlayPanSpeed2 = 0.022f;
			worldPanDir2 = new Vector2(0.24f, 0);
			worldPanDir2.Normalize();
		}

		public override void TickOverlay(Map map)
		{
			if (worldOverlayMat == null)
			{
				worldOverlayMat = MaterialPool.MatFrom("Weather/SandstormWorldOverlay");
				worldOverlayMat.CopyPropertiesFromMaterial(MatLoader.LoadMat("Weather/SnowOverlayWorld"));
				worldOverlayMat.shader = MatLoader.LoadMat("Weather/SnowOverlayWorld").shader;
				worldOverlayMat.SetTexture("_MainTex", ContentFinder<Texture2D>.Get("Weather/SandstormWorldOverlay"));
				worldOverlayMat.SetTexture("_MainTex2", ContentFinder<Texture2D>.Get("Weather/SandstormWorldOverlay"));
			}
			base.TickOverlay(map);
		}
	}
}
