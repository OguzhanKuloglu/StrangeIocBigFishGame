
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Model
{
    public interface ICameraModel
    {
        CD_CameraData CDCameraData { get; set; }
        RD_CameraData RDCameraData { get; set; }
        void ZoomOut(CameraType type);
        void ZoomIn(CameraType type);
        void SetLevelMaxZoomIn(CameraType type);
        float GetFollowDump();
        float GetZoomValue();
        void Reset();

    }   
}
