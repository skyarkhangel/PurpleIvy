﻿using System;
using System.Collections.Generic;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace PurpleIvy
{
    public class Building_СontainmentBreach : Building_WorkTable, IThingHolder
    {

        public Building_СontainmentBreach()
        {
            this.innerContainer = new ThingOwner<Thing>(this, false, LookMode.Deep);
        }

        public override void PostMake()
        {
            base.PostMake();
            this.maxNumAliens = this.def.GetModExtension<DefModExtension_СontainmentBreach>().maxNumAliens;
            this.blackoutProtection = this.def.GetModExtension<DefModExtension_СontainmentBreach>().blackoutProtection;
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            this.maxNumAliens = this.def.GetModExtension<DefModExtension_СontainmentBreach>().maxNumAliens;
            this.blackoutProtection = this.def.GetModExtension<DefModExtension_СontainmentBreach>().blackoutProtection;
        }

        public bool HasBloodInAlien(Pawn alien)
        {
            bool result = false;
            if (this.RecoveryBloodData != null && this.RecoveryBloodData.ContainsKey(alien))
            {
                if (this.RecoveryBloodData[alien] == 0
                    || this.RecoveryBloodData[alien] < Find.TickManager.TicksGame)
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }

        public bool HasToxinInAlien(Pawn alien)
        {
            bool result = false;
            if (this.RecoveryToxinData != null && this.RecoveryToxinData.ContainsKey(alien))
            {
                if (this.RecoveryToxinData[alien] == 0
                    || this.RecoveryToxinData[alien] < Find.TickManager.TicksGame)
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }
        public bool HasJobOnRecipe(Job job, out JobDef jobDef)
        {
            bool result = false;
            jobDef = null;
            foreach (var alien in this.Aliens)
            {
                if (job.RecipeDef == PurpleIvyDefOf.DrawAlienBlood)
                {
                    result = this.HasBloodInAlien(alien);
                    jobDef = PurpleIvyDefOf.PI_DrawAlienBlood;
                    if (result == true) break;
                }
                else if (job.RecipeDef == PurpleIvyDefOf.DrawAlphaAlienBlood && 
                    "Genny_ParasiteAlpha" == alien.def.defName)
                {
                    result = this.HasBloodInAlien(alien);
                    jobDef = PurpleIvyDefOf.PI_DrawAlienBlood;
                    if (result == true) break;
                }
                else if (job.RecipeDef == PurpleIvyDefOf.DrawBetaAlienBlood 
                    && "Genny_ParasiteBeta" == alien.def.defName)
                {
                    result = this.HasBloodInAlien(alien);
                    jobDef = PurpleIvyDefOf.PI_DrawAlienBlood;
                    if (result == true) break;
                }
                else if (job.RecipeDef == PurpleIvyDefOf.DrawGammaAlienBlood
    && "Genny_ParasiteGamma" == alien.def.defName)
                {
                    result = this.HasBloodInAlien(alien);
                    jobDef = PurpleIvyDefOf.PI_DrawAlienBlood;
                    if (result == true) break;
                }
                else if (job.RecipeDef == PurpleIvyDefOf.DrawOmegaAlienBlood && "Genny_ParasiteOmega" == alien.def.defName)
                {
                    result = this.HasBloodInAlien(alien);
                    jobDef = PurpleIvyDefOf.PI_DrawAlienBlood;
                    if (result == true) break;
                }
                else if (job.RecipeDef == PurpleIvyDefOf.DrawKorsolianToxin)
                {
                    result = this.HasToxinInAlien(alien);
                    jobDef = PurpleIvyDefOf.PI_DrawKorsolianToxin;
                    if (result == true) break;
                }
                else if (job.RecipeDef == PurpleIvyDefOf.PreciseVivisectionAlpha 
                    && "Genny_ParasiteAlpha" == alien.def.defName)
                {
                    result = true;
                    jobDef = PurpleIvyDefOf.PI_PreciseVivisection;
                    break;
                }
                else if (job.RecipeDef == PurpleIvyDefOf.PreciseVivisectionBeta && 
                    "Genny_ParasiteBeta" == alien.def.defName)
                {
                    result = true;
                    jobDef = PurpleIvyDefOf.PI_PreciseVivisection;
                    break;
                }
                else if (job.RecipeDef == PurpleIvyDefOf.PreciseVivisectionGamma &&
    "Genny_ParasiteGamma" == alien.def.defName)
                {
                    result = true;
                    jobDef = PurpleIvyDefOf.PI_PreciseVivisection;
                    break;
                }
                else if (job.RecipeDef == PurpleIvyDefOf.PreciseVivisectionOmega 
                    && "Genny_ParasiteOmega" == alien.def.defName)
                {
                    result = true;
                    jobDef = PurpleIvyDefOf.PI_PreciseVivisection;
                    break;
                }
            }
            if (result == false)
            {
                if (job.RecipeDef == PurpleIvyDefOf.PreciseVivisectionAlpha)
                {
                    result = true;
                    jobDef = PurpleIvyDefOf.PI_PreciseVivisection;
                }
                else if (job.RecipeDef == PurpleIvyDefOf.PreciseVivisectionBeta)
                {
                    result = true;
                    jobDef = PurpleIvyDefOf.PI_PreciseVivisection;
                }
                else if (job.RecipeDef == PurpleIvyDefOf.PreciseVivisectionGamma)
                {
                    result = true;
                    jobDef = PurpleIvyDefOf.PI_PreciseVivisection;
                }
                else if (job.RecipeDef == PurpleIvyDefOf.PreciseVivisectionOmega)
                {
                    result = true;
                    jobDef = PurpleIvyDefOf.PI_PreciseVivisection;
                }
            }
            return result;
        }

        public bool AlienHasJobOnRecipe(Pawn alien, RecipeDef recipe)
        {
            bool result = false;
            if (recipe == PurpleIvyDefOf.DrawAlienBlood)
            {
                result = this.HasBloodInAlien(alien);
            }
            else if (recipe == PurpleIvyDefOf.DrawAlphaAlienBlood)
            {
                if ("Genny_ParasiteAlpha" == alien.def.defName)
                {
                    result = this.HasBloodInAlien(alien);
                }
            }
            else if (recipe == PurpleIvyDefOf.DrawBetaAlienBlood)
            {
                if ("Genny_ParasiteBeta" == alien.def.defName)
                {
                    result = this.HasBloodInAlien(alien);
                }
            }
            else if (recipe == PurpleIvyDefOf.DrawGammaAlienBlood)
            {
                if ("Genny_ParasiteGamma" == alien.def.defName)
                {
                    result = this.HasBloodInAlien(alien);
                }
            }
            else if (recipe == PurpleIvyDefOf.DrawOmegaAlienBlood)
            {
                if ("Genny_ParasiteOmega" == alien.def.defName)
                {
                    result = this.HasBloodInAlien(alien);
                }
            }
            else if (recipe == PurpleIvyDefOf.DrawKorsolianToxin)
            {
                result = this.HasToxinInAlien(alien);
            }
            return result;
        }

        public ThingDef GetAlienBloodByRecipe(RecipeDef recipe)
        {
            ThingDef AlienBlood = null;
            foreach (var alien in this.Aliens)
            {
                if (this.AlienHasJobOnRecipe(alien, recipe))
                {
                    string bloodType = "PI_" + alien.def.defName.Replace("Genny_Parasite", "") + "Blood";
                    AlienBlood = DefDatabase<ThingDef>.GetNamed(bloodType, false);
                    if (this.RecoveryBloodData == null)
                    {
                        this.RecoveryBloodData = new Dictionary<Pawn, int>();
                    }
                    this.RecoveryBloodData[alien] = new IntRange(30000, 60000).RandomInRange + Find.TickManager.TicksGame;
                    break;
                }
            }
            return AlienBlood;
        }

        public ThingDef GetKorsolianToxin(RecipeDef recipe)
        {
            ThingDef KorsolianToxin = null;
            foreach (var alien in this.Aliens)
            {
                if (this.AlienHasJobOnRecipe(alien, recipe))
                {
                    KorsolianToxin = PurpleIvyDefOf.PI_KorsolianToxin;
                    if (this.RecoveryToxinData == null)
                    {
                        this.RecoveryToxinData = new Dictionary<Pawn, int>();
                    }
                    this.RecoveryToxinData[alien] = new IntRange(30000, 60000).RandomInRange + Find.TickManager.TicksGame;
                    break;
                }
            }
            return KorsolianToxin;
        }
        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (Aliens.Count > 0)
            {
                foreach (var alien in Aliens)
                {
                    var str = alien.Label;
                    if (this.RecoveryBloodData != null && this.RecoveryBloodData.ContainsKey(alien))
                    {
                        str += " - " + (this.RecoveryBloodData[alien] - Find.TickManager.TicksGame);
                    }
                    else
                    {
                        str += " - 0";
                    }
                    if (this.RecoveryToxinData != null && this.RecoveryToxinData.ContainsKey(alien))
                    {
                        str += " - " + (this.RecoveryToxinData[alien] - Find.TickManager.TicksGame);
                    }
                    else
                    {
                        str += " - 0";
                    }
                    stringBuilder.Append(str + "\n");
                }
            }
            stringBuilder.Append(base.GetInspectString());
            return stringBuilder.ToString();
        }
        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn selPawn)
        {
            foreach (FloatMenuOption fmo in base.GetFloatMenuOptions(selPawn))
            {
                yield return fmo;
            }
            if (this.Aliens != null && this.Aliens.Count > 0)
            {
                yield return new FloatMenuOption(Translator.Translate("ConductResearch"), delegate ()
                {
                    if (selPawn != null)
                    {
                        Job job = JobMaker.MakeJob(PurpleIvyDefOf.PI_ConductResearchOnAliens, this);
                        selPawn.jobs.TryTakeOrderedJob(job, 0);
                    }
                }, MenuOptionPriority.Default, null, null, 0f, null, null);
            }
            else
            {
                yield return new FloatMenuOption("NoAliensToConductResearch".Translate(), null,
                    MenuOptionPriority.Default, null, null, 0f, null, null);
            }
            yield break;
        }

        public ThingOwner GetDirectlyHeldThings()
        {
            return this.innerContainer;
        }

        public void GetChildHolders(List<IThingHolder> outChildren)
        {
            ThingOwnerUtility.AppendThingHoldersFromThings(outChildren, this.GetDirectlyHeldThings());
        }

        public List<Pawn> Aliens
        {
            get
            {
                List<Pawn> Aliens = new List<Pawn>();
                for (int i = 0; i < this.innerContainer.Count; i++)
                {
                    Aliens.Add((Pawn)this.innerContainer[i]);
                }
                return Aliens;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Deep.Look<ThingOwner>(ref this.innerContainer, "innerContainer", new object[]
            {
                this
            });
            Scribe_Collections.Look<Pawn, int>(ref this.RecoveryBloodData, "RecoveryBloodData",
                LookMode.Reference, LookMode.Value, ref this.RecoveryBloodDataKeys, ref this.RecoveryBloodDataValues);
            Scribe_Collections.Look<Pawn, int>(ref this.RecoveryToxinData, "RecoveryToxinData",
                LookMode.Reference, LookMode.Value, ref this.RecoveryToxinDataKeys, ref this.RecoveryToxinDataValues);
        }

        public Dictionary<Pawn, int> RecoveryBloodData = new Dictionary<Pawn, int>();

        public List<Pawn> RecoveryBloodDataKeys = new List<Pawn>();

        public List<int> RecoveryBloodDataValues = new List<int>();

        public Dictionary<Pawn, int> RecoveryToxinData = new Dictionary<Pawn, int>();

        public List<Pawn> RecoveryToxinDataKeys = new List<Pawn>();

        public List<int> RecoveryToxinDataValues = new List<int>();

        public ThingOwner innerContainer;

        public int maxNumAliens;

        public bool blackoutProtection;
    }
}

