using System;
using UnityEngine;

namespace Hunter.Common
{
    public static class Vector2Extensions
    {
        public static bool ApproximatelyEquals(this Vector2 a, Vector2 b, float allowableError)
        {
            return Math.Abs(a.x - b.x) <= allowableError
                   && Math.Abs(a.y - b.y) <= allowableError;
        }
    }
}
