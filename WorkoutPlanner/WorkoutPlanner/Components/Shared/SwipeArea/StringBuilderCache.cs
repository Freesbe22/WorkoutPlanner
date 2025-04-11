using System.Text;
//Sourced from MudBlazor (https://mudblazor.com/)

namespace WorkoutPlanner.Components.Shared.SwipeArea
{
    internal static class StringBuilderCache
    {
        // The value 360 was chosen in discussion with performance experts as a compromise between using
        // as little memory (per thread) as possible and still covering a large part of short-lived
        // StringBuilder creations on the startup path of VS designers.
        private const int MaxBuilderSize = 360;

        [ThreadStatic]
        private static StringBuilder? _cachedInstance;

        public static StringBuilder Acquire(int capacity = MaxBuilderSize)
        {
            if (capacity <= MaxBuilderSize)
            {
                var sb = _cachedInstance;
                if (sb is not null)
                {
                    // Avoid StringBuilder block fragmentation by getting a new StringBuilder
                    // when the requested size is larger than the current capacity
                    if (capacity <= sb.Capacity)
                    {
                        _cachedInstance = null;
                        sb.Clear();
                        return sb;
                    }
                }
            }
            return new StringBuilder(capacity);
        }

        public static void Release(StringBuilder sb)
        {
            if (sb.Capacity <= MaxBuilderSize)
            {
                _cachedInstance = sb;
            }
        }

        public static string GetStringAndRelease(StringBuilder sb)
        {
            var result = sb.ToString();
            Release(sb);
            return result;
        }
    }
}
