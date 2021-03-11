using RimWorld;
using Verse;

namespace Boombugs
{
    // Token: 0x02000FF4 RID: 4084
    [DefOf]
    public static class BoomPawnKindDefOf
    {
        // Token: 0x040039BF RID: 14783
        public static PawnKindDef Boompede;
        public static PawnKindDef Boomscarab;

        public static PawnKindDef Boomspider;

        // Token: 0x060064FC RID: 25852 RVA: 0x00232C33 File Offset: 0x00230E33
        static BoomPawnKindDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
        }
    }
}