using RimWorld;
using Verse;

namespace Boombugs
{
    // Token: 0x02000FE5 RID: 4069
    [DefOf]
    public static class BoomThingDefOf
    {
        // Token: 0x04003708 RID: 14088
        public static ThingDef BoomHive;
        public static ThingDef BoomTunnelHiveSpawner;

        // Token: 0x060064EE RID: 25838 RVA: 0x00232B45 File Offset: 0x00230D45
        static BoomThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BoomThingDefOf));
        }
    }
}