namespace Meerkat.Utility
{
    public class Math
    {
        public static int Mod(int x, int m) {
            return (x % m + m) % m;
        }
    }
}
