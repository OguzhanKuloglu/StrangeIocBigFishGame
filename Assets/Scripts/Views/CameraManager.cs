using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Assets.Scripts.Views
{
    public class CameraManager : View
    {
        public event UnityAction<string> onTestEvent;
        
        private float zoomSize;
        public CinemachineVirtualCamera cam;
        public GameObject WorldLimit;

        
        public void SetFollowDump(float followDump)
        {
           // dump = followDump;
        } 
        public void SetZoomValue(float followDump,float currentZoom)
        {
            DOTween.To(()=> zoomSize, x=> zoomSize = x, currentZoom, followDump).OnUpdate(()=>cam.m_Lens.OrthographicSize = zoomSize);
        }

        public void UpdateWorldLimit(Vector2 MapSize)
        {
            WorldLimit.transform.localScale = new Vector3(MapSize.x, MapSize.y, 1);
            DOTween.Pause(this);
        }

        [Button("SHAKE")]
        public void Shake()
        {
            float shake = 0;
            var tComposer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
            Vector3 ShakeOffset = Vector3.zero;
            DOTween.To(()=> shake, x=> shake = x, 1, 0f)
                .OnComplete(()=>Right(tComposer,new Vector3(.7f,0f,.5f)));
            
        }

        public void Right(CinemachineFramingTransposer transposer,Vector3 shakeOffset)
        {
            float shake = 0;
            transposer.m_TrackedObjectOffset = shakeOffset;
            DOTween.To(()=> shake, x=> shake = x, 1, 0.1f)
                .OnComplete(()=>Left(transposer,new Vector3(-.7f,0f,-.5f)));
        }
        
        public void Left(CinemachineFramingTransposer transposer,Vector3 shakeOffset)
        {
            float shake = 0;
            transposer.m_TrackedObjectOffset = shakeOffset;
            DOTween.To(()=> shake, x=> shake = x, 1, 0.2f)
                .OnComplete(()=>Mid(transposer,Vector3.zero));
        }
        public void Mid(CinemachineFramingTransposer transposer,Vector3 shakeOffset)
        {
            transposer.m_TrackedObjectOffset = shakeOffset;
        }
    }
    
}
