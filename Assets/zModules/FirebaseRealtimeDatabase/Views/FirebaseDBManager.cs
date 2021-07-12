using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Assets.Scripts.Extensions;
using Firebase;
using Firebase.Database;
using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using zModules.FirebaseRealtimeDatabase.Data.Vo;

namespace zModules.FirebaseRealtimeDatabase.Views
{
    public class FirebaseDBManager : View
    {
        private bool isConnect = false;
        private bool isLoading = false;
        private DependencyStatus dependencyStatus;
        private DatabaseReference DBreference;
     
        public event UnityAction<Dictionary<string, List<DataResultVo>>> onDataLoaded;
        public event UnityAction onGeneretedUserId;

        public string TestUSERID = "TestUserID(Development)";
        [InlineEditor(InlineEditorModes.FullEditor)]
        public CD_FirebaseDBData DataViewer;

        public void PushRuntimeData(SenderVo senderVo)
        {
              DBreference.Child(senderVo.TableName).Child(DataViewer.UserId).UpdateChildrenAsync(senderVo.Data);
        }
        public void GetData(GetDataVo vo)
        {
           StartCoroutine(GetDataCoroutine(vo));  
           //DBreference.Child(senderVo.TableName).Child(DataViewer.UserId).UpdateChildrenAsync(senderVo.Data);
        }

        public Dictionary<string, List<DataResultVo>> DataResult = new Dictionary<string,List<DataResultVo>>();
        public IEnumerator GetDataCoroutine(GetDataVo vo)
        {
            DataResult = new Dictionary<string, List<DataResultVo>>();
            var db = DBreference.Child(vo.TableName).OrderByChild(vo.OrderBy).GetValueAsync();
            yield return new WaitUntil(predicate: () => db.IsCompleted);
            DataSnapshot dss = db.Result;
            foreach (var data in dss.Children)
            {
                foreach (var child in data.Children)
                {

                    DataResult.AddOrCreate(data.Key, new DataResultVo
                    {
                        Key = child.Key,
                        Value = child.Value.ToString()
                    }) ;

                }
            }
            onDataLoaded?.Invoke(DataResult);
        }



        #region EditorFunc
        //******* Editor Mode *******
        //***************************
        //***************************


        [BoxGroup("Connection",ShowLabel = true)]
         [HideIf("isConnect")]
         [Button("CONNECT",ButtonSizes.Gigantic),GUIColor(0.8f,0.2f,0)]
         [DisableIf("isLoading")]
         //[DisableInEditorMode]
         [InfoBox("Switch to play mode for connection.",InfoMessageType.Info)]
         public void Init()
         {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
               dependencyStatus = task.Result;
               if (dependencyStatus == DependencyStatus.Available)
               {
                  //If they are avalible Initialize Firebase
                  InitializeFirebase();
               }
               else
               {
                  Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
               }
            });

           
            
         }
        
         private void InitializeFirebase()
         {
            Debug.Log("---> Database Available");

            DBreference = FirebaseDatabase.DefaultInstance.RootReference;
            Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            isConnect = true;
            if (DataViewer.FirsTimeUser)
            {
               GenereteUserId();
            }
         }
         
         [BoxGroup("Connection",ShowLabel = true)]
         [ShowIf("isConnect")]
         [Button("Connected", ButtonSizes.Gigantic), GUIColor(0, 1f, 0)]
         [DisableIf("isLoading")]
         public void Connected()
         {
            isConnect = false;
            DBreference = null;
            dependencyStatus = DependencyStatus.UnavailableDisabled;
         }
         
         [BoxGroup("UserSettings",ShowLabel = true)]
         [HorizontalGroup("UserSettings/Horizontal")]
         [Button("SING OUT (DELETE USERID)",ButtonSizes.Large)]
         [DisableIf("isLoading")]
         public void SingOut()
         {
            DataViewer.UserId = "";
            DataViewer.FirsTimeUser = true;
         }
         
         [HorizontalGroup("UserSettings/Horizontal")]
         [Button("LOGIN (GENERETE USERID)",ButtonSizes.Large)]         
         [DisableIf("isLoading")]
         private void GenereteUserId()
         {
            DBreference = FirebaseDatabase.DefaultInstance.RootReference;
            string userId = DBreference.Push().Key;
            DataViewer.UserId = userId;
            DataViewer.FirsTimeUser = false;
            onGeneretedUserId?.Invoke();
         }
         
         
         [BoxGroup("DataProcess",ShowLabel = true)]
         [HorizontalGroup("DataProcess/Horizontal")]
         [Button("GET TABLE",ButtonSizes.Large)]
         [DisableIf("isLoading")]
         public void GetTables()
         {
            Debug.Log("---> Get data process started.");
            isLoading = true;
            StartCoroutine(GetTableData());
         }
         
         [HorizontalGroup("DataProcess/Horizontal")]
         [Button("GET COLUMNS", ButtonSizes.Large)]
         [DisableIf("isLoading")]
         public void GetColumns()
         {
            Debug.Log("---> Get data process started.");
            isLoading = true;
            StartCoroutine(GetColumnsData());
         }

         [BoxGroup("DataProcess",ShowLabel = true)]
         [Button("Clear Local Table",ButtonSizes.Large)]
         [DisableIf("isLoading")]
         public void ClearLocalData()
         {
            DataViewer.Tables = new List<string>();
            DataViewer.RemovedTables = new List<string>();
         }
         
         [BoxGroup("DataProcess",ShowLabel = true)]
         [Button("Clear Local Columns",ButtonSizes.Large)]
         [DisableIf("isLoading")]
         public void ClearLocalColumns()
         {
            DataViewer.Columns = new Dictionary<string, List<ColumnsVo>>();
         }
         [BoxGroup("TableProcess",ShowLabel = true)]
         [HorizontalGroup("TableProcess/Horizontal")]
         [VerticalGroup("TableProcess/Horizontal/Left")]
         public string NewTableName = "";
         
         
         [BoxGroup("TableProcess",ShowLabel = true)]
         [HorizontalGroup("TableProcess/Horizontal")]
         [VerticalGroup("TableProcess/Horizontal/Left")]
         [Button("ADD NEW TABLE",ButtonSizes.Large),LabelText("ADD")]
         [DisableIf("isLoading")]
         public void AddNewTable()
         {
            if (NewTableName == "")
            {
               Debug.LogError("New Table Name not be null");
               return;
            }

            if (DataViewer.Tables.Contains(NewTableName))
            {
               Debug.LogError("The table you want to add already exists.");
               NewTableName = "";
               return;
            }
            DataViewer.Tables.Add(NewTableName);
            NewTableName = "";
          
         }
         
         [BoxGroup("TableProcess",ShowLabel = true)]
         [HorizontalGroup("TableProcess/Horizontal")]
         [VerticalGroup("TableProcess/Horizontal/Right")]
         public string RemoveTableName = "";
         
         [BoxGroup("TableProcess",ShowLabel = true)]
         [HorizontalGroup("TableProcess/Horizontal")]
         [VerticalGroup("TableProcess/Horizontal/Right")]
         [Button("REMOVE TABLE",ButtonSizes.Large)]
         [DisableIf("isLoading")]
         public void RemoveTable()
         {
            if (RemoveTableName == "")
            {
               Debug.LogError("Remove Table Name not be null");
               return;
            }
            if (!DataViewer.Tables.Contains(RemoveTableName))
            {
               Debug.LogError("The table you want to delete was not found.");
               RemoveTableName = "";
               return;
            }
            DataViewer.Tables.Remove(RemoveTableName);
            DataViewer.RemovedTables.Add(RemoveTableName);
            RemoveTableName = "";
         }

         
         [Button("PUSH",ButtonSizes.Gigantic),GUIColor(1f,0.55f,0f)]
         [DisableIf("isLoading")]
         public void PUSH()
         {
            if (!isConnect)
            {
               Debug.LogError("Not Connection yet.");
               return;
            }
            for (int i = 0; i < DataViewer.Tables.Count; i++)
            {
               Dictionary<string, object> data = new Dictionary<string, object>();
               if (DataViewer.Columns.ContainsKey(DataViewer.Tables[i]))
               {
                  var columns = DataViewer.Columns[DataViewer.Tables[i]];
                  for (int j = 0; j < columns.Count; j++)
                  {
                     data[columns[j].Key] = columns[j].Value;
                  }
                  DBreference.Child(DataViewer.Tables[i]).Child(TestUSERID).UpdateChildrenAsync(data);
               }
            }
            
            for (int x = 0; x < DataViewer.RemovedTables.Count; x++)
            {
               Dictionary<string, object> data = new Dictionary<string, object>();
               if (DataViewer.Columns.ContainsKey(DataViewer.RemovedTables[x]))
               {
                  if (DataViewer.Columns.ContainsKey(DataViewer.RemovedTables[x]))
                  {
                     DataViewer.Columns.Remove(DataViewer.RemovedTables[x]);
                  }
                  DBreference.Child(DataViewer.RemovedTables[x]).RemoveValueAsync();
               }
            }
            Debug.Log("---> Data Push.");
            GetTables();
            CODEGENERATION_TABLE();
            CODEGENERATION_COLUMNS();
         }
         
         
        //***************************
        //***************************
        //***************************
        #endregion

        #region IENumerators

        public IEnumerator GetTableData()
        {
              if (DBreference == null) 
              {
                 isConnect = false; 
                 isLoading = false;
                 Debug.LogError("No connection yet. Please connect to database first."); 
                 yield break;
              }
            var db = DBreference.GetValueAsync();
            
            yield return new WaitUntil(predicate: () => db.IsCompleted);
            DataViewer.Tables = new List<string>();
            DataViewer.RemovedTables = new List<string>();
            DataSnapshot dss = db.Result;
            foreach (var table in dss.Children)
            {
               DataViewer.Tables.Add(table.Key);
            }

            CODEGENERATION_TABLE();
            Debug.Log("---> Tables Data loaded.");
            isLoading = false;
        }
        public IEnumerator GetColumnsData()
        {
           DataViewer.Columns = new Dictionary<string, List<ColumnsVo>>();
           for (int i = 0; i < DataViewer.Tables.Count; i++)
           {
              var tableName = DataViewer.Tables[i];
              if (DBreference == null) 
              {
                 isConnect = false; 
                 isLoading = false;
                 Debug.LogError("No connection yet. Please connect to database first."); 
                 yield break;
              }
              var db = DBreference.Child(tableName).Child(TestUSERID).GetValueAsync();
            
              yield return new WaitUntil(predicate: () => db.IsCompleted);
              DataSnapshot dss = db.Result;
              foreach (var Columns in dss.Children)
              {
                 DataViewer.Columns.AddOrCreate(tableName,new ColumnsVo()
                 {
                    Key = Columns.Key,
                    Value = Columns.Value.ToString()
                 });
              }
           }
          
           CODEGENERATION_COLUMNS();
           Debug.Log("---> Columns Data loaded.");
           isLoading = false;
        }
        

        #endregion
       
        #region CodeGeneration

        public void CODEGENERATION_TABLE()
        {
           string addition_table = "";
           for (int i = 0; i < DataViewer.Tables.Count; i++)
           {
              addition_table+="\r\t\t";
              addition_table+= "//-%Name%";
              addition_table+="\r\t\t";
              addition_table+="public const string %Name% = \"%Name%\";";
              addition_table+="\r\t\t";
              addition_table+="//-";
              addition_table+="\r\t\t";
              addition_table = addition_table.Replace("%Name%", DataViewer.Tables[i]);
           }
           addition_table+="ADDPOINT";
           CodeGeneration("", "TableName.cs", addition_table);
        }
        public void CODEGENERATION_COLUMNS()
        {
            
           for (int i = 0; i < DataViewer.Tables.Count; i++)
           {
              string addition_columns = "";
              if (DataViewer.Columns.ContainsKey(DataViewer.Tables[i]))
              {
                 var columns = DataViewer.Columns[DataViewer.Tables[i]];
                 for (int j = 0; j < columns.Count; j++)
                 {
                    addition_columns+="\r\t\t";
                    addition_columns+= "//-%Name%";
                    addition_columns+="\r\t\t";
                    addition_columns+="public const string %Name% = \"%Name%\";";
                    addition_columns+="\r\t\t";
                    addition_columns+="//-";
                    addition_columns+="\r\t\t";
                    addition_columns = addition_columns.Replace("%Name%", columns[j].Key);
                 }
                 addition_columns+="ADDPOINT";
                 CodeGeneration("",DataViewer.Tables[i]+"_Columns.cs",addition_columns);
              }
           }

        }
        
        private void CodeGeneration(string text,string constantName,string addition)
        {

            string constantPath = "Assets/zModules/FirebaseRealtimeDatabase/Constant/" + constantName;
            //UnityEngine.Object obj =   AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(constantPath);

            string data;
            data = LoadTemplate();
            
            data = data.Replace("//*ADDITION*//",addition);
            //data = data.Replace("%Name%", text);
            data = data.Replace("%ConstantName%", constantName.Replace(".cs"," "));
            data = data.Replace("ADDPOINT","//*ADDITION*//");
            SaveFile(data, constantPath);

        }
        
        private string LoadTemplate()
        {
            try
            {
                string data = string.Empty;
                string path = "Assets/zModules/FirebaseRealtimeDatabase/TemplateConstant" + ".txt";
                StreamReader theReader = new StreamReader(path, Encoding.Default);
                using (theReader)
                {
                    data = theReader.ReadToEnd();
                    theReader.Close();
                }

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}\n", e.Message);
                return string.Empty;
            }
        }
        
        private string LoadFileOnPath(string filePath)
        {
            try
            {
                string data = string.Empty;
                string path = filePath;
                StreamReader theReader = new StreamReader(path, Encoding.Default);
                using (theReader)
                {
                    data = theReader.ReadToEnd();
                    theReader.Close();
                }

                return data;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return string.Empty;
            }
        }
        
        public static void SaveFile(string data, string filename)
        {
           using (StreamWriter outfile = new StreamWriter(new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite),
              Encoding.Default))
           {
              outfile.Write(data);
           }
        }

        #endregion
       
        
    }
}
