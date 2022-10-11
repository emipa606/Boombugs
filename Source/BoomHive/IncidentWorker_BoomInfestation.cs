using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace Boombugs;

internal class IncidentWorker_BoomInfestation : IncidentWorker_Infestation
{
    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        var map = (Map)parms.target;
        var t = SpawnTunnels(Mathf.Max(GenMath.RoundRandom(parms.points / 220f), 1), map);
        SendStandardLetter(parms, t, Array.Empty<NamedArgument>());
        Find.TickManager.slower.SignalForceNormalSpeedShort();
        return true;
    }

    private static Thing SpawnTunnels(int hiveCount, Map map, bool spawnAnywhereIfNoGoodCell = false,
        bool ignoreRoofedRequirement = false, string questTag = null)
    {
        if (!InfestationCellFinder.TryFindCell(out var loc, map))
        {
            if (!spawnAnywhereIfNoGoodCell)
            {
                return null;
            }

            if (!RCellFinder.TryFindRandomCellNearTheCenterOfTheMapWith(delegate(IntVec3 x)
                {
                    if (!x.Standable(map) || x.Fogged(map))
                    {
                        return false;
                    }

                    var result = false;
                    var num = GenRadial.NumCellsInRadius(3f);
                    for (var j = 0; j < num; j++)
                    {
                        var c = x + GenRadial.RadialPattern[j];
                        if (!c.InBounds(map))
                        {
                            continue;
                        }

                        var roof = c.GetRoof(map);
                        if (roof is not { isThickRoof: true })
                        {
                            continue;
                        }

                        result = true;
                        break;
                    }

                    return result;
                }, map, out loc))
            {
                return null;
            }
        }

        BoomTunnelHiveSpawner.ResetStaticData();
        var thing = GenSpawn.Spawn(ThingMaker.MakeThing(BoomThingDefOf.BoomTunnelHiveSpawner), loc, map,
            WipeMode.FullRefund);
        QuestUtility.AddQuestTag(thing, questTag);
        for (var i = 0; i < hiveCount - 1; i++)
        {
            loc = CompSpawnerHives.FindChildHiveLocation(thing.Position, map, BoomThingDefOf.BoomHive,
                BoomThingDefOf.BoomHive.GetCompProperties<CompProperties_SpawnerHives>(), ignoreRoofedRequirement,
                true);
            if (!loc.IsValid)
            {
                continue;
            }

            thing = GenSpawn.Spawn(ThingMaker.MakeThing(BoomThingDefOf.BoomTunnelHiveSpawner), loc, map,
                WipeMode.FullRefund);
            QuestUtility.AddQuestTag(thing, questTag);
        }

        return thing;
    }
}