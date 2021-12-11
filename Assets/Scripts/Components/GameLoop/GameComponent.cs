using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.GameLoop
{
    public class GameComponent : MonoBehaviour
    {
        [SerializeField] private DamageableComponent _player;
        
        public Game Game { get; private set; }

        private void Awake()
        {
            Game = new Game(_player.Damageable);
        }
    }
}
