using System;
using System.Reflection;
using HarmonyLib;
using Multiplayer.API;
using Verse;

namespace ReGrowthCore
{
	[StaticConstructorOnStartup]
	internal static class MultiplayerSupport
	{
		static MultiplayerSupport()
		{
			if (!MP.enabled)
			{
				return;
			}
			MP.RegisterSyncMethod(typeof(CompTreeLeavesSpawner), "TryFindSpawnCell", null);
			var method = AccessTools.Method(typeof(CompTreeLeavesSpawner), "TryFindSpawnCell", null, null);
			MultiplayerSupport.harmony.Patch(method, new HarmonyMethod(typeof(MultiplayerSupport),
				"FixRNGPre", null), new HarmonyMethod(typeof(MultiplayerSupport), "FixRNGPos", null), null, null);
		}

		private static void FixRNGPre()
		{
			Rand.PushState(Find.TickManager.TicksAbs);
		}

		private static void FixRNGPos()
		{
			Rand.PopState();
		}

		private static Harmony harmony = new Harmony("rimworld.regrowthcore.multiplayersupport");
	}
}

