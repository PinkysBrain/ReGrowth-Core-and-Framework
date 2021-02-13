using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace ReGrowthCore
{

    [HarmonyPatch(typeof(WeatherManager), "CurWeatherPerceived", MethodType.Getter)]
    public static class CurWeatherPerceived_Patch
    {
        private static Dictionary<Map, WeatherDef> weathers = new Dictionary<Map, WeatherDef>();
        private static void Postfix(WeatherManager __instance, WeatherDef __result)
        {
            if (weathers.TryGetValue(__instance.map, out WeatherDef value) && __result != value)
            {
                weathers[__instance.map] = __result;
                var options = __result.GetModExtension<WeatherLetterExtensions>();
                if (options != null)
                {
                    Find.LetterStack.ReceiveLetter(options.letterTitle, options.letterText, options.letterDef);
                }
            }
            else
            {
                weathers[__instance.map] = __result;
            }
        }
    }
}