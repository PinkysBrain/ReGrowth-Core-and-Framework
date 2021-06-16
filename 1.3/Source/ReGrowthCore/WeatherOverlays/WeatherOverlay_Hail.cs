using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace ReGrowthCore
{
	public class WeatherOverlay_Hail : SkyOverlay
	{
		public int nextDamageTick;
		public WeatherOverlay_Hail()
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
				worldOverlayMat = MaterialPool.MatFrom("Weather/HailWorldOverlay");
				worldOverlayMat.CopyPropertiesFromMaterial(MatLoader.LoadMat("Weather/SnowOverlayWorld"));
				worldOverlayMat.shader = MatLoader.LoadMat("Weather/SnowOverlayWorld").shader;
				worldOverlayMat.SetTexture("_MainTex", ContentFinder<Texture2D>.Get("Weather/HailWorldOverlay"));
				worldOverlayMat.SetTexture("_MainTex2", ContentFinder<Texture2D>.Get("Weather/HailWorldOverlay"));
				//this.worldOverlayMat.SetColor("_TuningColor", new Color(1, 1, 1, 1)); //0.2720588f, 0.2720588f, 0.2720588f, 0.05882353f));
			}
			base.TickOverlay(map);

			if (!ReGrowthSettings.DisableHailDamage && map.weatherManager.CurWeatherPerceived == RGDefOf.RG_Hail)
            {
				if (nextDamageTick == 0)
				{
					nextDamageTick = Find.TickManager.TicksGame + Rand.RangeInclusive(300, 600);
				}

				if (Find.TickManager.TicksGame > nextDamageTick)
				{
					DoDamage(map);
					nextDamageTick = Find.TickManager.TicksGame + Rand.RangeInclusive(300, 600);
				}
			}

		}

		public void DoDamage(Map map)
        {
			var victimCandidates = map.mapPawns.AllPawns.Where(x => !x.RaceProps.IsMechanoid && x.Spawned && !x.Position.Roofed(map));
			var victims = RandomlySelectedItems(victimCandidates, (int)(victimCandidates.Count() / 10f)).ToList();
			for (int num = victims.Count - 1; num >= 0; num--)
            {
				var damageInfo = new DamageInfo(DamageDefOf.Blunt, Rand.Range(0.1f, 0.5f));
				victims[num].TakeDamage(damageInfo);
            }
		}

		public static IEnumerable<Pawn> RandomlySelectedItems(IEnumerable<Pawn> sequence, int count)
		{
			return sequence.InRandomOrder().Take(count);
		}
	}
}
