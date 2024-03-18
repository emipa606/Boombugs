using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Boombugs;

internal class BoomHive : Hive
{
    public new static readonly List<PawnKindDef> spawnablePawnKinds = [];

    public new static void ResetStaticData()
    {
        spawnablePawnKinds.Clear();
        spawnablePawnKinds.Add(BoomPawnKindDefOf.Boomscarab);
        spawnablePawnKinds.Add(BoomPawnKindDefOf.Boompede);
        spawnablePawnKinds.Add(BoomPawnKindDefOf.Boomspider);
    }
}