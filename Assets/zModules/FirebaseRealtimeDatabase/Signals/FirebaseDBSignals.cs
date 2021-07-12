using System.Collections.Generic;
using strange.extensions.signal.impl;
using zModules.FirebaseRealtimeDatabase.Data.Vo;

public class FirebaseDBSignals
{
    public Signal<SenderVo> SendData = new Signal<SenderVo>();
    public Signal<GetDataVo> GetData = new Signal<GetDataVo>();
    public Signal<Dictionary<string, List<DataResultVo>>> DataLoaded = new Signal<Dictionary<string, List<DataResultVo>>>();

}
