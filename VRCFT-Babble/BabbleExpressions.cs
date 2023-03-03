using System.Collections.Generic;

namespace VRCFT_Babble
{
    public static class BabbleExpressions
    {
        public enum UnifiedBabbleExpression
        { 
            CheekPuff,
            CheekSquint_L,
            CheekSquint_R,
            NoseSneer_L,
            NoseSneer_R,
            JawOpen,
            JawForward,
            JawLeft,
            JawRight,
            MouthFunnel,
            MouthPucker,
            MouthLeft,
            MouthRight,
            MouthRollUpper,
            MouthRollLower,
            MouthShrugUpper,
            MouthShrugLower,
            MouthClose,
            MouthSmile_L,
            MouthSmile_R,
            MouthFrown_L,
            MouthFrown_R,
            MouthDimple_L,
            MouthDimple_R,
            MouthUpperUp_L,
            MouthUpperUp_R,
            MouthLowerDown_L,
            MouthLowerDown_R,
            MouthPress_L,
            MouthPress_R,
            MouthStretch_L,
            MouthStretch_R,
            TongueOut
        }

        public static Dictionary<UnifiedBabbleExpression, string> ExpressionToAddress = new Dictionary<UnifiedBabbleExpression, string>()
        {
            { UnifiedBabbleExpression.CheekPuff, "/cheekPuff" },
            { UnifiedBabbleExpression.CheekSquint_L, "/cheekSquint_L" },
            { UnifiedBabbleExpression.CheekSquint_R, "/cheekSquint_R" },
            { UnifiedBabbleExpression.NoseSneer_L, "/noseSneer_L" },
            { UnifiedBabbleExpression.NoseSneer_R, "/noseSneer_R" },
            { UnifiedBabbleExpression.JawOpen, "/jawOpen" },
            { UnifiedBabbleExpression.JawForward, "/jawForward" },
            { UnifiedBabbleExpression.JawLeft, "/jawLeft" },
            { UnifiedBabbleExpression.JawRight, "/jawRight" },
            { UnifiedBabbleExpression.MouthFunnel, "/mouthFunnel" },
            { UnifiedBabbleExpression.MouthPucker, "/mouthPucker" },
            { UnifiedBabbleExpression.MouthLeft, "/mouthLeft" },
            { UnifiedBabbleExpression.MouthRight, "/mouthRight" },
            { UnifiedBabbleExpression.MouthRollUpper, "/mouthRollUpper" },
            { UnifiedBabbleExpression.MouthRollLower, "/mouthRollLower" },
            { UnifiedBabbleExpression.MouthShrugUpper, "/mouthShrugUpper" },
            { UnifiedBabbleExpression.MouthShrugLower, "/mouthShrugLower" },
            { UnifiedBabbleExpression.MouthClose, "/mouthClose" },
            { UnifiedBabbleExpression.MouthSmile_L, "/mouthSmile_L" },
            { UnifiedBabbleExpression.MouthSmile_R, "/mouthSmile_R" },
            { UnifiedBabbleExpression.MouthFrown_L, "/mouthFrown_L" },
            { UnifiedBabbleExpression.MouthFrown_R, "/mouthFrown_R" },
            { UnifiedBabbleExpression.MouthDimple_L, "/mouthDimple_L" },
            { UnifiedBabbleExpression.MouthDimple_R, "/mouthDimple_R" },
            { UnifiedBabbleExpression.MouthUpperUp_L, "/mouthUpperUp_L" },
            { UnifiedBabbleExpression.MouthUpperUp_R, "/mouthUpperUp_R" },
            { UnifiedBabbleExpression.MouthLowerDown_L, "/mouthLowerDown_L" },
            { UnifiedBabbleExpression.MouthLowerDown_R, "/mouthLowerDown_R" },
            { UnifiedBabbleExpression.MouthPress_L, "/mouthPress_L" },
            { UnifiedBabbleExpression.MouthPress_R, "/mouthPress_R" },
            { UnifiedBabbleExpression.MouthStretch_L, "/mouthStretch_L" },
            { UnifiedBabbleExpression.MouthStretch_R, "/mouthStretch_R" },
            { UnifiedBabbleExpression.TongueOut, "/tongueOut" }
        };
    }
}
