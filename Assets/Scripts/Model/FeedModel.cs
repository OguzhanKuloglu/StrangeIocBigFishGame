using UnityEngine;

namespace Assets.Scripts.Model
{
 
    public class FeedModel : IFeedModel
    {
        private CD_FeedData _feedData;

        public CD_FeedData FeedData
        {
            get
            {
                if (_feedData == null)
                    OnPostConstruct();
                return _feedData;
            }
            set { }
        }
        
        [PostConstruct]
        public void OnPostConstruct()
        {
            _feedData = Resources.Load<CD_FeedData>("Data/FeedData");
        }
        
    }   
}
