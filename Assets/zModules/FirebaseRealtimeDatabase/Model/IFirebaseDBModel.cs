
namespace zModules.FirebaseRealtimeDatabase.Model
{
    public interface IFirebaseDBModel
    {
        CD_FirebaseDBData FirebaseDBData { get; set; }
        bool GetFirstTimeUser();
        string GetUserId();
        string GetUserName();

        void SetFirstTimeUser(bool value);
        
        void SetUserId(string userId);
        void SetUserName(string userName);

    }   
}
