using UnityEngine;

namespace Hunter.Creatures
{
    public class RotatorComponent : MonoBehaviour
    {
        public Rotator Rotator { get; private set; }

        private void Awake()
        {
            Rotator = new Rotator(transform);
        }
    }
}
