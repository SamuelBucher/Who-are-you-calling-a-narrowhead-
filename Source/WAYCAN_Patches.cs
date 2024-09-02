using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;
using System.Reflection;

namespace WAYCAN
{
    [StaticConstructorOnStartup]
    public class HarmonyPatches
    {
        private static readonly MethodInfo isNarrowMesh = AccessTools.Method(typeof(WAYCAN_Method), nameof(WAYCAN_Method.ifNarrowMesh), new Type[] { typeof(Verse.Pawn) });
        private static readonly MethodInfo getHumanlikeHeadSetForPawn = AccessTools.Method(typeof(HumanlikeMeshPoolUtility), nameof(HumanlikeMeshPoolUtility.GetHumanlikeHeadSetForPawn), new Type[] { typeof(Verse.Pawn), typeof(float), typeof(float) });

        static HarmonyPatches()
        {
            Harmony harmony = new Harmony(id: "rimworld.SamBucher.WAYCAN");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(PawnRenderNode_Apparel), nameof(PawnRenderNode_Apparel.MeshSetFor))]
        public static class MeshSetFor_Apparel_Patch
        {
            [HarmonyTranspiler]
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                List<CodeInstruction> code = new List<CodeInstruction>(instructions);

                for (int i = 0; i < code.Count; i++)
                {
                    if (code[i].opcode == OpCodes.Ldc_R4 && code[i + 2].Calls(getHumanlikeHeadSetForPawn))
                    {
                        yield return new CodeInstruction(OpCodes.Call, isNarrowMesh);
                        i += 2;
                    }
                    else
                        yield return code[i];
                }
            }
        }
    }
}
