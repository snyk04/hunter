using UnityEngine;

namespace Hunter.Objects
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class MapBorderComponent : MonoBehaviour
    {
        public MapBorder MapBorder { get; private set; }

        private void Awake()
        {
            MapBorder = new MapBorder();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            MapBorder.OnTriggerEnter2D(other);
        }
    }
}
