using UnityEngine;

namespace DayOne.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 GetXZ(this Vector3 position)
        {
            return new Vector2(position.x, position.z);
        }

        public static Vector3 AsXZ(this Vector3 position)
        {
            return new Vector3(position.x, 0, position.y);
        }

        public static Vector3 AsXZ(this Vector2 position)
        {
            return new Vector3(position.x, 0, position.y);
        }

        public static Vector2 Rotated(this Vector2 direction, float angle)
        {
            var radians = angle * Mathf.Deg2Rad;
            var cos = Mathf.Cos(radians);
            var sin = Mathf.Sin(radians);

            return new Vector2(direction.x * cos - direction.y * sin, direction.x * sin + direction.y * cos);
        }
    }
}