﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;
using System.Linq;

namespace PurpleIvy
{
    public class AlienQueen : Pawn
    {
        private int spawnticks = 1200;
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            this.SetFactionDirect(PurpleIvyData.AlienFaction);
        }

        public override void Tick()
        {
            base.Tick();
            if (Find.TickManager.TicksGame > this.recoveryTick && this.health.Downed)
            {
                Log.Message("Test");
                this.health.Reset();
            }
            spawnticks--;
            if (spawnticks == 0)
            {
                foreach (IntVec3 current in GenAdj.CellsAdjacentCardinal(this))
                {
                    if (GenGrid.InBounds(current, this.Map))
                    {
                        if (current.GetPlant(this.Map) == null)
                        {
                            if (!GenCollection.Any<Thing>(GridsUtility.GetThingList(current, this.Map), (Thing t) => (t.def.IsBuildingArtificial || t.def.IsNonResourceNaturalRock)))
                            {
                                Plant newNest = (Plant)ThingMaker.MakeThing(ThingDef.Named("PI_Nest"));
                                GenSpawn.Spawn(newNest, current, this.Map);
                            }
                        }
                        else
                        {
                            Plant plant = current.GetPlant(this.Map);
                            if (plant.def.defName != "PI_Nest")
                            {
                                if (!GenCollection.Any<Thing>(GridsUtility.GetThingList(current, this.Map), (Thing t) => (t.def.IsBuildingArtificial || t.def.IsNonResourceNaturalRock)))
                                {
                                    plant.Destroy();
                                    Plant newNest = (Plant)ThingMaker.MakeThing(ThingDef.Named("PI_Nest"));
                                    GenSpawn.Spawn(newNest, current, this.Map);
                                }
                            }
                        }
                    }
                }
                spawnticks = 1200;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.recoveryTick, "recoveryTick", 0);
        }

        public int recoveryTick = 0;
    }
}
