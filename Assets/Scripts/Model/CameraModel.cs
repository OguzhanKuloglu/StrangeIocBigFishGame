using UnityEngine;
using CameraType = Assets.Scripts.Enums.CameraType;

namespace Assets.Scripts.Model
{
 
    public class CameraModel : ICameraModel
    {
        private CD_CameraData _cdCameraData;
        private RD_CameraData _rdCameraData;

        public CD_CameraData CDCameraData
        {
            get
            {
                if (_cdCameraData == null)
                    OnPostConstruct();
                return _cdCameraData;
            }
            set { }
        }
        public RD_CameraData RDCameraData
        {
            get
            {
                if (_rdCameraData == null)
                    OnPostConstruct();
                return _rdCameraData;
            }
            set { }
        }

        

        [PostConstruct]
        public void OnPostConstruct()
        {
            _cdCameraData = Resources.Load<CD_CameraData>("Data/CameraData");
            _rdCameraData = Resources.Load<RD_CameraData>("Data/RD_CameraData");
        }

        #region Func
            public void ZoomOut(CameraType type)
            {
                if (_cdCameraData.List[type].MaxZoomOut <= _rdCameraData.ZoomValue)
                    return;
                _rdCameraData.ZoomValue += _cdCameraData.List[type].ZoomFactor;
            }
            public void ZoomIn(CameraType type)
            {
                if (_cdCameraData.List[type].MaxZoomIn >= _rdCameraData.ZoomValue)
                    return;
                _rdCameraData.ZoomValue -= _cdCameraData.List[type].ZoomFactor;
            }
            public void SetLevelMaxZoomIn(CameraType type)
            {
                _rdCameraData.ZoomValue = _cdCameraData.List[type].MaxZoomIn;
            }
            public float GetFollowDump()
            {
                return _cdCameraData.FollowDump;
            }
            public float GetZoomValue()
            {
                return _rdCameraData.ZoomValue;
            }
            public void Reset()
            {
                _rdCameraData.ZoomValue = _cdCameraData.DefaultZoom;
            }
        #endregion
    }   
}
