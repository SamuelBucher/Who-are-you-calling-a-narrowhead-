using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace WAYCAN
{
    public class WAYCAN_Method
    {
        public static GraphicMeshSet ifNarrowMesh(Pawn pawn)
        {
            if (pawn.story.headType.narrow)
                return HumanlikeMeshPoolUtility.GetHumanlikeHeadSetForPawn(pawn, 0.85f, 0.85f); ;
            return HumanlikeMeshPoolUtility.GetHumanlikeHeadSetForPawn(pawn);
        }

    }
}
