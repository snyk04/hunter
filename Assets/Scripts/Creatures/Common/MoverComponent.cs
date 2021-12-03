using UnityEngine;

namespace Hunter.Creatures.Common
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoverComponent : MonoBehaviour
    {
        public Mover Mover { get; private set; }

        private void Awake()
        {
            Mover = new Mover(GetComponent<Rigidbody2D>());
        }
    }
}
