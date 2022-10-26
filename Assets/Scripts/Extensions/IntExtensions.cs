using UnityEngine;

namespace Extensions
{
    public static class IntExtensions
    {
        public static int MinMax(this int x, int min, int max)
        {
            return Mathf.Max(Mathf.Min(x, max), min);
        }
    }
}
