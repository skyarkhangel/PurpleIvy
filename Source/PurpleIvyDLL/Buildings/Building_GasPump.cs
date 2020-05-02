﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

namespace PurpleIvy
{
    public class Building_GasPump : Building
    {
        private int pumpfreq = 10;
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            this.SetFactionDirect(PurpleIvyData.AlienFaction);
        }

        public override void TickRare()
        {
            base.TickRare();
            pumpfreq--;
            if (pumpfreq == 0)
            {
                PurpleIvyMoteMaker.ThrowToxicGas(base.Position.ToVector3Shifted(), this.Map);
                pumpfreq = 3;
            }
        }
    }
}

