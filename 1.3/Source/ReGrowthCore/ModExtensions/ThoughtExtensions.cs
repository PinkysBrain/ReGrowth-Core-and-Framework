using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ReGrowthCore
{
    public class ThoughtExtensions : DefModExtension
    {
        public List<ThoughtDef> removeThoughtsWhenAdded;
    }
}

