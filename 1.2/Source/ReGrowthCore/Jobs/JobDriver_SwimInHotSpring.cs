using System;
using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;
using Verse.AI;
using Verse.Noise;

namespace ReGrowthCore
{
    public class JobDriver_SwimInHotSpring : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            Log.Message("JobDriver_SwimInHotSpring : JobDriver - TryMakePreToilReservations - return pawn.Reserve(TargetA, job); - 1", true);
            return pawn.Reserve(TargetA, job);
        }
        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOn(() => !JoyUtility.EnjoyableOutsideNow(pawn));
            Toil goToil = Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.OnCell);
            yield return new Toil { initAction = delegate () { Log.Message("JobDriver_SwimInHotSpring : JobDriver - MakeNewToils - yield return goToil; - 4", true); } };
            yield return goToil;
            Toil toil = new Toil();
            toil.initAction = delegate
            {
                startTick = Find.TickManager.TicksGame;
                this.pawn.jobs.posture = Rand.Chance(0.5f) ? PawnPosture.LayingOnGroundFaceUp : PawnPosture.LayingOnGroundNormal;
                this.pawn.Drawer.renderer.graphics.ClearCache();
                this.pawn.Drawer.renderer.graphics.apparelGraphics.Clear();
            };
            toil.tickAction = delegate
            {
                if (Find.TickManager.TicksGame > startTick + job.def.joyDuration)
                {
                    pawn.needs?.mood?.thoughts.memories.TryGainMemory(RGDefOf.RG_HotSpringThought);
                    OnComplection();
                    EndJobWith(JobCondition.Succeeded);
                }
                else
                {
                    JoyUtility.JoyTickCheckEnd(pawn);
                }
                this.pawn.GainComfortFromCellIfPossible(false);
            };
            toil.AddFinishAction(delegate
            {
                OnComplection();
            });
            toil.defaultCompleteMode = ToilCompleteMode.Never;
            yield return new Toil { initAction = delegate () { Log.Message("JobDriver_SwimInHotSpring : JobDriver - MakeNewToils - yield return toil; - 23", true);
                OnComplection();
            } };
            yield return toil;
        }

        private void OnComplection()
        {
            Log.Message("JobDriver_SwimInHotSpring : JobDriver - MakeNewToils - this.pawn.Drawer.renderer.graphics.ResolveApparelGraphics(); - 20", true);
            this.pawn.Drawer.renderer.graphics.ResolveApparelGraphics();
            Log.Message("JobDriver_SwimInHotSpring : JobDriver - MakeNewToils - this.pawn.Drawer.renderer.RenderPortrait(); - 21", true);
            this.pawn.Drawer.renderer.RenderPortrait();
            Log.Message("JobDriver_SwimInHotSpring : JobDriver - MakeNewToils - }); - 22", true);
        }
    }
}