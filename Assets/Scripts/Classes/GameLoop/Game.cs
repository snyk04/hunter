using System;
using Hunter.Creatures.Common;

namespace Hunter.GameLoop
{
    public class Game
    {
        private readonly Damageable _player;

        public event Action GameStopped;

        public Game(Damageable player)
        {
            _player = player;

            _player.OnDestroy += StopGame;
        }

        private void StopGame()
        {
            GameStopped?.Invoke();
        }
    }
}
