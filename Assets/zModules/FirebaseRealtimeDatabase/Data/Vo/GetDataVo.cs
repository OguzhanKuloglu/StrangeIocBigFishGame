using Sirenix.OdinInspector;

namespace zModules.FirebaseRealtimeDatabase.Data.Vo
{
    [System.Serializable]
    [HideReferenceObjectPicker]
    public class GetDataVo
    {
        public string TableName;
        public string OrderBy;
    }
}