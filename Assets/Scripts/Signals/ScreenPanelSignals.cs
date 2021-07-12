using Assets.Scripts.Data.Vo;
using strange.extensions.signal.impl;

public class ScreenPanelSignals
{
    public Signal<PanelVo> OpenPanel = new Signal<PanelVo>();
    public Signal<int> ClearPanel = new Signal<int>();

}
