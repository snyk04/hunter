using UnityEngine;

namespace Hunter.Creatures.Common
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
