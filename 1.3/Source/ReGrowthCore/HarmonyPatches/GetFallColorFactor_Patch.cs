using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace ReGrowthCore
{
	[StaticConstructorOnStartup]
	internal static class HarmonyInit
	{
		static HarmonyInit()
		{
			new Harmony("Helixien.ReGrowthCore").PatchAll();
        }
    }

    [HarmonyPatch(typeof(PlantFallColors), "GetFallColorFactor")]
    public static class GetFallColorFactor
    {
        public static float fallColorFactor = 0f;
        public static void Postfix(ref float __result)
        {
            fallColorFactor = __result;
        }
    }
}
