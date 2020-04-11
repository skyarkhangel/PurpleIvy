﻿using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace PurpleIvy
{
    public class MapComponent_WeatherControl : MapComponent
    {
        public MapComponent_WeatherControl(Map map) : base(map)
        {

        }

        public override void ExposeData()
        {
            base.ExposeData();
        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();
            if (Find.TickManager.TicksGame % 250 == 0)
            {
                int count = this.map.listerThings.ThingsOfDef(PurpleIvyDefOf.PurpleIvy).Count;
                bool comeFromOuterSource;
                var tempComp = new WorldObjectComp_InfectedTile();
                tempComp.infectedTile = map.Tile;
                if (PurpleIvyData.getFogProgressWithOuterSources(count, tempComp, out comeFromOuterSource) > 0f && 
                    !map.gameConditionManager.ConditionIsActive(PurpleIvyDefOf.PurpleFogGameCondition))
                {
                    GameCondition_PurpleFog gameCondition =
                        (GameCondition_PurpleFog)GameConditionMaker.MakeConditionPermanent
                        (PurpleIvyDefOf.PurpleFogGameCondition);
                    map.gameConditionManager.RegisterCondition(gameCondition);
                    if (comeFromOuterSource == false)
                    {
                        Find.LetterStack.ReceiveLetter(gameCondition.LabelCap,
                        gameCondition.LetterText, gameCondition.def.letterDef,
                        new TargetInfo(map.Center, map, false));
                    }
                    else
                    {
                        Find.LetterStack.ReceiveLetter("PurpleFogСomesFromInfectedSites.".Translate(),
                        "PurpleFogСomesFromInfectedSitesDesc".Translate(), 
                        LetterDefOf.ThreatBig, new TargetInfo(map.Center, map, false));
                        Log.Message("PurpleFogСomesFromInfectedSites: " + map.ToString()
                            + " - " + Find.TickManager.TicksGame.ToString());
                    }
                    if (map.Parent.GetComponent<WorldObjectComp_InfectedTile>() == null)
                    {
                        var comp = new WorldObjectComp_InfectedTile();
                        comp.parent = map.Parent;
                        comp.StartInfection();
                        comp.gameConditionCaused = PurpleIvyDefOf.PurpleFogGameCondition;
                        comp.counter = count;
                        comp.infectedTile = map.Tile;
                        comp.radius = (int)(count / 100);
                        comp.fillRadius();
                        map.Parent.AllComps.Add(comp);
                        PurpleIvyData.TotalFogProgress[comp] = PurpleIvyData.getFogProgress(count);
                        Log.Message("Adding comp to: " + map.Parent.ToString());
                    }
                }
            }
            if (Find.TickManager.TicksGame % 60000 == 0)
            {
                Log.Message("Total PurpleIvy count on the map: " + 
                    this.map.listerThings.ThingsOfDef(PurpleIvyDefOf.PurpleIvy).Count.ToString(), true);
                Log.Message("Total Genny_ParasiteAlpha count on the map: " +
                    this.map.listerThings.ThingsOfDef(PurpleIvyDefOf.Genny_ParasiteAlpha).Count.ToString(), true);
                Log.Message("Total Genny_ParasiteBeta count on the map: " +
    this.map.listerThings.ThingsOfDef(PurpleIvyDefOf.Genny_ParasiteBeta).Count.ToString(), true);
                Log.Message("Total Genny_ParasiteOmega count on the map: " +
    this.map.listerThings.ThingsOfDef(PurpleIvyDefOf.Genny_ParasiteOmega).Count.ToString(), true);
                Log.Message("Total EggSac count on the map: " +
    this.map.listerThings.ThingsOfDef(PurpleIvyDefOf.EggSac).Count.ToString(), true);
                Log.Message("Total ParasiteEgg count on the map: " +
this.map.listerThings.ThingsOfDef(PurpleIvyDefOf.ParasiteEgg).Count.ToString(), true);
                Log.Message("Total GasPump count on the map: " +
this.map.listerThings.ThingsOfDef(PurpleIvyDefOf.GasPump).Count.ToString(), true);
                Log.Message("Total GenTurretBase count on the map: " +
this.map.listerThings.ThingsOfDef(PurpleIvyDefOf.GenTurretBase).Count.ToString(), true);
                Log.Message("Total Turret_GenMortarSeed count on the map: " +
this.map.listerThings.ThingsOfDef(PurpleIvyDefOf.Turret_GenMortarSeed).Count.ToString(), true);
            }
        }
    }
}

