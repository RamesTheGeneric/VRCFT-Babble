using Collections;
using System;
using System.Threading;
using VRCFaceTracking;
using VRCFaceTracking.Params;
using static VRCFT_Babble.BabbleExpressions;
using static VRCFT_Babble.BabbleExpressions.UnifiedBabbleExpression;

namespace VRCFT_Babble
{
    public class Babble : ExtTrackingModule
    {
        public override (bool SupportsEye, bool SupportsExpression) Supported => (false, true);

        private TwoKeyDictionary<UnifiedBabbleExpression, string, float> _mouthShape;
        private BabbleOSC _receiver;
        private const int DefaultPort = 9000;

        public override (bool eyeSuccess, bool expressionSuccess) Initialize(bool eye, bool lip)
        {
            if (_receiver != null)
            {
                Logger.Error("BabbleOSC connection already exists.");
                return (false, false);
            }

            _receiver = new BabbleOSC("127.0.0.1", DefaultPort);
            _mouthShape = new TwoKeyDictionary<UnifiedBabbleExpression, string, float>();
            foreach (var shape in ExpressionToAddress)
                _mouthShape.Add(shape.Key, shape.Value, 0f);
            
            return Supported;
        }
        
        public override Action GetUpdateThreadFunc()
        {
            return () =>
            {
                while (true)
                {
                    Update();
                    Thread.Sleep(10);
                }
            };
        }

        public void Update()
        {
            try
            {
                switch (_receiver.message.Value.GetType())
                {
                    // case Type decimalType when decimalType == typeof(decimal):
                    // case Type doubleType when doubleType == typeof(double):
                    case Type floatType when floatType == typeof(float):
                    {
                        if (_mouthShape.ContainsKey2(_receiver.message.Address))
                            _mouthShape.SetByKey2(_receiver.message.Address, BitConverter.ToSingle(_receiver.message.Data, 0));
                        
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            // Assuming x is left/right, y is up/down, z is forward/backwards
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.JawLeft].Weight = _mouthShape.GetByKey1(JawLeft);
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.JawRight].Weight = _mouthShape.GetByKey1(JawRight);
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.JawForward].Weight = _mouthShape.GetByKey1(JawForward);
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.JawOpen].Weight = _mouthShape.GetByKey1(JawOpen);
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.TongueOut].Weight = _mouthShape.GetByKey1(TongueOut);
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthCornerPullLeft].Weight = _mouthShape.GetByKey1(MouthSmile_L); // Test!
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthCornerPullRight].Weight = _mouthShape.GetByKey1(MouthSmile_R); // Test!
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthCornerSlantLeft].Weight = _mouthShape.GetByKey1(MouthSmile_L); // Test!
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthCornerSlantRight].Weight = _mouthShape.GetByKey1(MouthSmile_R); // Test!
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthFrownLeft].Weight = _mouthShape.GetByKey1(MouthFrown_L);
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.MouthFrownRight].Weight = _mouthShape.GetByKey1(MouthFrown_R);
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.CheekPuffLeft].Weight = _mouthShape.GetByKey1(CheekPuff);
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.CheekPuffRight].Weight = _mouthShape.GetByKey1(CheekPuff);
             
            var pucker = _mouthShape.GetByKey1(MouthPucker) - _mouthShape.GetByKey1(MouthFunnel);
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerLowerLeft].Weight = pucker;
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerLowerRight].Weight = pucker;
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerUpperLeft].Weight = pucker;
            UnifiedTracking.Data.Shapes[(int)UnifiedExpressions.LipPuckerUpperRight].Weight = pucker;
        }

        public override void Teardown()
        {
            _receiver.Teardown();
        }
    }
}
