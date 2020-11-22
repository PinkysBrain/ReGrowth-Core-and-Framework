using System;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using UnityEngine;
using RimWorld.Planet;

// Copyright Sarg - Alpha Biomes 2020 & Taranchuck

namespace ReGrowthCore
{
    public class MapComponentExtender : MapComponent
    {
        public bool verifyFirstTime = true;
        public int spawnCounter = 0;

        public MapComponentExtender(Map map) : base(map)
        {

        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref this.verifyFirstTime, "verifyFirstTime", true, true);
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();
            if (verifyFirstTime)
            {
                this.DoMapSpawns();
            }
        }

        public bool CanSpawnAt(IntVec3 c, ObjectSpawnsDef element)
        {
            if (!element.allowOnChunks)
            {
                foreach (var item in c.GetThingList(map))
                {
                    if (item?.def?.thingCategories != null)
                    {
                        foreach (var category in item.def.thingCategories)
                        {
                            if (category == ThingCategoryDefOf.Chunks || category == ThingCategoryDefOf.StoneChunks)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            TerrainDef terrain = c.GetTerrain(map);

            bool flagAllowed = true;

            foreach (string allowed in element.terrainValidationAllowed)
            {
                if (terrain.defName == allowed)
                {
                    break;
                }
                else flagAllowed = false;
            }
            if (!flagAllowed) return false;

            foreach (string notAllowed in element.terrainValidationDisallowed)
            {
                if (terrain.HasTag(notAllowed))
                {
                    return false;
                }
            }

            if (!element.allowOnWater && terrain.IsWater)
            {
                return false;
            }

            if (element.findCellsOutsideColony)
            {
                if (!OutOfCenter(c, map, 60))
                {
                    return false;
                }
            }

            return true;
        }
        public void DoMapSpawns()
        {
            foreach (ObjectSpawnsDef element in DefDatabase<ObjectSpawnsDef>.AllDefs.Where(element => element.allowedBiome == map.Biome.defName))
            {
                IEnumerable<IntVec3> tmpTerrain = map.AllCells.InRandomOrder();
                int extraGeneration = 0;
                foreach (string biome in element.biomesWithExtraGeneration)
                {
                    if (map.Biome.defName == biome)
                    {
                        extraGeneration = element.extraGeneration;
                    }
                }

                if (spawnCounter == 0)
                {
                    spawnCounter = Rand.RangeInclusive(element.numberToSpawn.min, element.numberToSpawn.max) + extraGeneration;
                }
                foreach (IntVec3 c in tmpTerrain)
                {
                    bool canSpawn = CanSpawnAt(c, element);
                    if (canSpawn)
                    {
                        Thing thing = (Thing)ThingMaker.MakeThing(element.thingDef, null);
                        CellRect occupiedRect = GenAdj.OccupiedRect(c, thing.Rotation, thing.def.Size);
                        if (occupiedRect.InBounds(map))
                        {
                            canSpawn = true;
                            foreach (IntVec3 c2 in occupiedRect)
                            {
                                if (!CanSpawnAt(c2, element))
                                {
                                    canSpawn = false;
                                    break;
                                }
                            }
                            if (canSpawn)
                            {
                                GenSpawn.Spawn(thing, c, map);
                                spawnCounter--;
                            }
                        }
                    }

                    if (canSpawn && spawnCounter <= 0)
                    {
                        spawnCounter = 0;
                        break;
                    }
                }
            }
            this.verifyFirstTime = false;
        }

        public static bool OutOfCenter(IntVec3 c, Map map, int centerDist)
        {
            IntVec3 CenterPoint = map.Center;
            return c.x < CenterPoint.x - centerDist || c.z < CenterPoint.z - centerDist || c.x >= CenterPoint.x + centerDist || c.z >= CenterPoint.z + centerDist;
        }
    }
}