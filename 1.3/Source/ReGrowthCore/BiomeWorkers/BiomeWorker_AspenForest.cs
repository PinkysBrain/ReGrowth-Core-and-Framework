using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace ReGrowthAspenForest
{
	public class BiomeWorker_AspenForest : BiomeWorker
	{
		public override float GetScore(Tile tile, int tileID)
		{
			if (tile.WaterCovered)
			{
				return -100f;
			}
			if (tile.temperature < -10f)
			{
				return 0f;
			}
			if (tile.rainfall < 600f)
			{
				return 0f;
			}
			Vector3 tileCenter = Find.WorldGrid.GetTileCenter(tileID);
			var value = BiomePerlin.GetNoiseFor(BiomeDef.Named("RG-AF_AspenForest")).GetValue(tileCenter);
			if (value > 0.6f && (0f - tile.temperature) > 0)
            {
				return (0f - tile.temperature) + 100;
            }
			return 0f;
		}
	}
}
