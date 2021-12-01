using UnityEngine;

namespace Hunter.Common
{
    public static class InputExtensions
    {
        public static Vector2 ConvertMousePositionToNormalizedVector(this Vector2 mousePosition)
        {
            float screenHeight = Screen.height;
            float screenWidth = Screen.width;

            float x = mousePosition.x - screenWidth / 2;
            float y = mousePosition.y - screenHeight / 2;

            return new Vector2(x, y).normalized;
        }
    }
}