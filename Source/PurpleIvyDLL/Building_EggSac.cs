﻿using System;
using System.Collections.Generic;
using System.Text;
using Verse;
using Verse.AI;
using Verse.Sound;
using RimWorld;

namespace PurpleIvy
{
    public class Building_EggSac : Building
    {
        Faction factionDirect = Find.FactionManager.FirstFactionOfDef(DefDatabase<FactionDef>.GetNamed("Genny", true));
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            this.SetFactionDirect(factionDirect);
            base.SpawnSetup(map, respawningAfterLoad);
        }
    }
}

