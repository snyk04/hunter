using System;
using UnityEngine;

namespace Hunter.Common
{
    public static class Vector3Extensions
    {
        public static bool ApproximatelyEquals(this Vector3 a, Vector3 b, float allowableError)
        {
            return Math.Abs(a.x - b.x) <= allowableError
                   && Math.Abs(a.y - b.y) <= allowableError
                   && Math.Abs(a.z - b.z) <= allowableError;
        }

        public static Vector3 Add(this Vector3 a, Vector2 b)
        {
            return a + new Vector3(b.x, b.y);
        }
    }
}
