using RimWorld;
using Verse;

namespace Boombugs;

[DefOf]
public static class BoomThingDefOf
{
    public static ThingDef BoomHive;
    public static ThingDef BoomTunnelHiveSpawner;

    static BoomThingDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(BoomThingDefOf));
    }
}