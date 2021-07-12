using Assets.Scripts.Data.Vo;
using Assets.Scripts.Views;
using strange.extensions.mediation.impl;

namespace Assets.Scripts.Mediators
{
    public class ScreenManagerMediator : Mediator
    {
        [Inject] public ScreenManagerView view{ get; set;}
        [Inject] public ScreenPanelSignals ScreenSignals { get; set; }
        
       
        public override void OnRegister()
        {
            base.OnRegister();
            ScreenSignals.OpenPanel.AddListener(OnOpenPanel);
            ScreenSignals.ClearPanel.AddListener(OnClearPanel);

        }

        public override void OnRemove()
        {
            base.OnRemove();
            ScreenSignals.OpenPanel.RemoveListener(OnOpenPanel);
            ScreenSignals.ClearPanel.RemoveListener(OnClearPanel);

        }

        public void OnOpenPanel(PanelVo vo)
        {
            view.OpenPanel(vo);
        }
        public void OnClearPanel(int layer)
        {
            view.ClearPanel(layer);
        }
      
    }    
}

