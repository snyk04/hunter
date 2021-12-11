using Hunter.GameLoop;
using UnityEngine;

namespace Hunter.UI
{
    public class GameOverWindowComponent : WindowComponent
    {
        [SerializeField] private GameComponent _gameComponent;
        
        private void Awake()
        {
            Window = new GameOverWindow(_gameComponent.Game, _interfaceElements, _buttonToSelect);
        }
    }
}
