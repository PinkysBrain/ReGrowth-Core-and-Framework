using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

// Copyright Sarg - Alpha Biomes 2020 & Taranchuck

namespace ReGrowthCore
{
    public class ObjectSpawnsDef : Def
    {
        public ThingDef thingDef;
        public bool allowOnWater;
        public bool allowOnChunks;
        public IntRange numberToSpawn;
        public List<string> terrainValidationAllowed;
        public List<string> terrainValidationDisallowed;
        public List<BiomeDef> allowedBiomes;
        public string disallowedBiome;
        public bool findCellsOutsideColony = false;
    }
}
