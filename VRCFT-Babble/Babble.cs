using Collections;
using System;
using System.Threading;
using ViveSR.anipal.Lip;
using VRCFaceTracking;
using static VRCFT_Babble.BabbleExpressions;
using static VRCFT_Babble.BabbleExpressions.UnifiedBabbleExpression;

namespace VRCFT_Babble
{
    public class Babble : ExtTrackingModule
    {
        public override (bool SupportsEye, bool SupportsLip) Supported => (false, true);

        private TwoKeyDictionary<UnifiedBabbleExpression, string, float> _mouthShape;
        private BabbleOSC _receiver;
        private const int DefaultPort = 9000;

        public override (bool eyeSuccess, bool lipSuccess) Initialize(bool eye, bool lip)
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
            UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.JawLeft] = _mouthShape.GetByKey1(JawLeft);
            UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.JawRight] = _mouthShape.GetByKey1(JawRight);
            UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.JawForward] = _mouthShape.GetByKey1(JawForward);
            UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.JawOpen] = _mouthShape.GetByKey1(JawOpen);
            UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.TongueLongStep2] = _mouthShape.GetByKey1(TongueOut);
            UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.MouthPout] = _mouthShape.GetByKey1(MouthPucker) - _mouthShape.GetByKey1(MouthFunnel);
            UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.MouthSmileLeft] = _mouthShape.GetByKey1(MouthSmile_L);
            UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.MouthSadLeft] = _mouthShape.GetByKey1(MouthFrown_L);
            UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.MouthSmileRight] = _mouthShape.GetByKey1(MouthSmile_R);
            UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.MouthSadRight] = _mouthShape.GetByKey1(MouthFrown_R);
            UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.CheekPuffLeft] = _mouthShape.GetByKey1(CheekPuff);
            UnifiedTrackingData.LatestLipData.LatestShapes[(int)LipShape_v2.CheekPuffRight] = _mouthShape.GetByKey1(CheekPuff);
        }

        public override void Teardown()
        {
            
        }
    }
}
