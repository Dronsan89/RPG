using UnityEngine;

namespace Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 ZeroX(this Vector3 vector)
        {
            vector.x = 0f;
            return vector;
        }

        public static Vector3 ZeroY(this Vector3 vector)
        {
            vector.y = 0f;
            return vector;
        }
    }

}
