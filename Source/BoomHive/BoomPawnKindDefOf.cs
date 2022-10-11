using RimWorld;
using Verse;

namespace Boombugs;

[DefOf]
public static class BoomPawnKindDefOf
{
    public static PawnKindDef Boompede;
    public static PawnKindDef Boomscarab;

    public static PawnKindDef Boomspider;

    static BoomPawnKindDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
    }
}