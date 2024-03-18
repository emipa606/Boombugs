using System.Collections.Generic;
using System.Linq;
using LudeonTK;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI.Group;
using Verse.Sound;

namespace Boombugs;

[StaticConstructorOnStartup]
internal class BoomTunnelHiveSpawner : TunnelHiveSpawner
{
    private static MaterialPropertyBlock matPropertyBlock = new MaterialPropertyBlock();

    [TweakValue("Gameplay", 0f, 1f)] private static readonly float DustMoteSpawnMTB = 0.2f;

    [TweakValue("Gameplay", 0f, 1f)] private static readonly float FilthSpawnMTB = 0.3f;

    [TweakValue("Gameplay", 0f, 10f)] private static readonly float FilthSpawnRadius = 3f;

    private static readonly Material TunnelMaterial =
        MaterialPool.MatFrom("Things/Filth/Grainy/GrainyA", ShaderDatabase.Transparent);

    private static readonly List<ThingDef> filthTypes = [];
    private new readonly FloatRange ResultSpawnDelay = new FloatRange(26f, 30f);
    private new int secondarySpawnTick;
    private Sustainer sustainer;

    public new static void ResetStaticData()
    {
        filthTypes.Clear();
        filthTypes.Add(ThingDefOf.Filth_Dirt);
        filthTypes.Add(ThingDefOf.Filth_Fuel);
        filthTypes.Add(ThingDefOf.Filth_Dirt);
        filthTypes.Add(ThingDefOf.Filth_Dirt);
        filthTypes.Add(ThingDefOf.Filth_Dirt);
        filthTypes.Add(ThingDefOf.Filth_Dirt);
        filthTypes.Add(ThingDefOf.Filth_RubbleRock);
    }

    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, respawningAfterLoad);
        if (!respawningAfterLoad)
        {
            secondarySpawnTick = Find.TickManager.TicksGame + ResultSpawnDelay.RandomInRange.SecondsToTicks();
        }

        CreateSustainer();
    }


    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref secondarySpawnTick, "secondarySpawnTick");
        Scribe_Values.Look(ref spawnHive, "spawnHive", true);
        Scribe_Values.Look(ref insectsPoints, "insectsPoints");
        Scribe_Values.Look(ref spawnedByInfestationThingComp, "spawnedByInfestationThingComp");
    }

    private void CreateSustainer()
    {
        LongEventHandler.ExecuteWhenFinished(delegate
        {
            var tunnel = SoundDefOf.Tunnel;
            sustainer = tunnel.TrySpawnSustainer(SoundInfo.InMap(this, MaintenanceType.PerTick));
        });
    }

    public override void Tick()
    {
        if (!Spawned)
        {
            return;
        }

        sustainer.Maintain();
        var vector = Position.ToVector3Shifted();
        if (Rand.MTBEventOccurs(FilthSpawnMTB, 1f, 1.TicksToSeconds()) &&
            CellFinder.TryFindRandomReachableCellNearPosition(Position, Position, Map, FilthSpawnRadius,
                TraverseParms.For(TraverseMode.NoPassClosedDoors), null, null,
                out var result))
        {
            FilthMaker.TryMakeFilth(result, Map, filthTypes.RandomElement());
        }

        if (Rand.MTBEventOccurs(DustMoteSpawnMTB, 1f, 1.TicksToSeconds()))
        {
            var loc = new Vector3(vector.x, 0f, vector.z) { y = AltitudeLayer.MoteOverhead.AltitudeFor() };
            FleckMaker.ThrowDustPuffThick(loc, Map, Rand.Range(1.5f, 3f), new Color(1f, 1f, 1f, 2.5f));
        }

        if (secondarySpawnTick > Find.TickManager.TicksGame)
        {
            return;
        }

        sustainer.End();
        var map = Map;
        var position = Position;
        Destroy();
        if (spawnHive)
        {
            var obj = (Hive)GenSpawn.Spawn(ThingMaker.MakeThing(BoomThingDefOf.BoomHive), position, map);
            obj.SetFaction(Faction.OfInsects);
            obj.questTags = questTags;
            foreach (var comp in obj.GetComps<CompSpawner>())
            {
                if (comp.PropsSpawner.thingToSpawn != ThingDefOf.Chemfuel)
                {
                    continue;
                }

                comp.TryDoSpawn();
                break;
            }
        }

        if (!(insectsPoints > 0f))
        {
            return;
        }

        BoomHive.ResetStaticData();
        insectsPoints = Mathf.Max(insectsPoints, BoomHive.spawnablePawnKinds.Min(x => x.combatPower));
        var pointsLeft = insectsPoints;
        var list = new List<Pawn>();
        var num = 0;
        PawnKindDef result2;
        for (; pointsLeft > 0f; pointsLeft -= result2.combatPower)
        {
            num++;
            if (num > 1000)
            {
                Log.Error("Too many iterations.");
                break;
            }

            var left = pointsLeft;
            if (!Hive.spawnablePawnKinds.Where(x => x.combatPower <= left).TryRandomElement(out result2))
            {
                break;
            }

            var pawn = PawnGenerator.GeneratePawn(result2, Faction.OfInsects);
            GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(position, map, 2), map);
            pawn.mindState.spawnedByInfestationThingComp = spawnedByInfestationThingComp;
            list.Add(pawn);
        }

        if (list.Any())
        {
            LordMaker.MakeNewLord(Faction.OfInsects, new LordJob_AssaultColony(Faction.OfInsects, true, false), map,
                list);
        }
    }
}