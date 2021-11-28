using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ReGrowthCore
{
    public class WeatherLetterExtensions : DefModExtension
    {
        public LetterDef letterDef;
        public string letterTitle;
        public string letterText;
    }
}

