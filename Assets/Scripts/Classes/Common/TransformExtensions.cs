using UnityEngine;

namespace Hunter.Common
{
    public static class TransformExtensions
    {
        public static Vector2 Position(this Transform transform)
        {
            return new Vector2(transform.position.x, transform.position.y);
        }
    }
}
