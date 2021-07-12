using Assets.Scripts.Context;
using strange.extensions.context.impl;

namespace Assets.Scripts.Root
{
    public class GameRoot : ContextView
    {
        public GameContext _gameContext;
        void Awake()
        {
            _gameContext = new GameContext(this);
            context = _gameContext;
            _gameContext.LoadData();
        }

        private void OnApplicationQuit()
        {
            _gameContext.SaveData();
        }
    }

}
