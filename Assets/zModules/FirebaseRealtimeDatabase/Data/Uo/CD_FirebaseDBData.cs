using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using zModules.FirebaseRealtimeDatabase.Data.Vo;

[CreateAssetMenu(menuName = "Firebase-RealtimeDatabase/Data/FirebaseDBData",order = 0)]
public class CD_FirebaseDBData : SerializedScriptableObject
{
   private bool isConnect = false;
   [ReadOnly]
   public bool FirsTimeUser;
   [ReadOnly]
   public string UserId;
   [ReadOnly]
   public string UserName;
   [ReadOnly]
   public List<string> Tables = new List<string>();
   [ReadOnly]
   public List<string> RemovedTables = new List<string>();

   [DictionaryDrawerSettings(KeyLabel = "Table Name", ValueLabel = "Column Name") ]
   public Dictionary<string, List<ColumnsVo>> Columns = new Dictionary<string, List<ColumnsVo>>();
   
   
}





