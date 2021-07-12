using UnityEngine;

namespace zModules.FirebaseRealtimeDatabase.Model
{
 
    public class FirebaseDBModel : IFirebaseDBModel
    {
        private CD_FirebaseDBData _firebaseDBData;

        public CD_FirebaseDBData FirebaseDBData
        {
            get
            {
                if (_firebaseDBData == null)
                    OnPostConstruct();
                return _firebaseDBData;
            }
            set { }
        }
        
        [PostConstruct]
        public void OnPostConstruct()
        {
            _firebaseDBData = Resources.Load<CD_FirebaseDBData>("Data/FirebaseDBData");
        }

        #region Func

        public bool GetFirstTimeUser()
        {
            return _firebaseDBData.FirsTimeUser;
        }

        public string GetUserId()
        {
            return _firebaseDBData.UserId;
        }
        public string GetUserName()
        {
            return _firebaseDBData.UserName;
        }
        public void SetFirstTimeUser(bool value)
        {
            _firebaseDBData.FirsTimeUser = value;
        }

        public void SetUserId(string userId)
        {
            _firebaseDBData.UserId = userId;
        }
        public void SetUserName(string userName)
        {
            _firebaseDBData.UserName = userName;
        }
        #endregion
    }   
}
