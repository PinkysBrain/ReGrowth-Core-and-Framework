using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace ReGrowthCore
{

    [HarmonyPatch(typeof(MemoryThoughtHandler), "TryGainMemory", new Type[]
    {
                typeof(Thought_Memory),
                typeof(Pawn)
    })]
    public static class TryGainMemory_Patch
    {
        private static void Postfix(MemoryThoughtHandler __instance, ref Thought_Memory newThought, Pawn otherPawn)
        {
            Log.Message("TryGainMemory_Patch - Postfix - var options = newThought.def.GetModExtension<ThoughtExtensions>(); - 1", true);
            var options = newThought.def.GetModExtension<ThoughtExtensions>();
            Log.Message("TryGainMemory_Patch - Postfix - if (options != null) - 2", true);
            if (options != null)
            {
                Log.Message("TryGainMemory_Patch - Postfix - if (options.removeThoughtsWhenAdded != null) - 3", true);
                if (options.removeThoughtsWhenAdded != null)
                {
                    Log.Message("TryGainMemory_Patch - Postfix - foreach (var thoughtDef in options.removeThoughtsWhenAdded) - 4", true);
                    foreach (var thoughtDef in options.removeThoughtsWhenAdded)
                    {
                        Log.Message("TryGainMemory_Patch - Postfix - __instance.pawn.needs.mood.thoughts.memories.RemoveMemoriesOfDef(thoughtDef); - 5", true);
                        __instance.pawn.needs.mood.thoughts.memories.RemoveMemoriesOfDef(thoughtDef);
                    }
                }
            }
        }
    }
}