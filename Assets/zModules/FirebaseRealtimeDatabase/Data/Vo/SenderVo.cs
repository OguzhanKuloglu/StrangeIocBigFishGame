using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace zModules.FirebaseRealtimeDatabase.Data.Vo
{
    [System.Serializable]
    [HideReferenceObjectPicker]
    public class SenderVo
    {
        public string TableName;
        public Dictionary<string, object> Data = new Dictionary<string, object>();
    }
}