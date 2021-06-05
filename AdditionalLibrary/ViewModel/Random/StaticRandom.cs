using System;
using System.Threading;

namespace AdditionalLibrary
{
    public static class StaticRandom
    {
        static int seed = Environment.TickCount;

        static readonly ThreadLocal<Random> random =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        public static int Rand()
        {
            return random.Value.Next();
        }

        public static int Rand(int x, int y)
        {
            return random.Value.Next(x, y);
        }
    }
}
