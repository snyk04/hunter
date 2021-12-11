using Hunter.GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace Hunter.UI
{
    public class GameOverWindow : Window
    {
        public GameOverWindow(Game game, GameObject[] interfaceElements, Button buttonToSelect) : base(interfaceElements,
            buttonToSelect)
        {
            game.GameStopped += Enable;
        }
    }
}
