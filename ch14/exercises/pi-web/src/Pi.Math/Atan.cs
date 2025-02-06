namespace Pi.Math
{
    public class Atan
    {

        public static HighPrecision Calculate(int denominator)
        {
            HighPrecision result = new HighPrecision(1, denominator);
            int xSquared = denominator * denominator;

            int divisor = 1;
            HighPrecision term = result;

            while (!term.IsZero)
            {
                divisor += 2;
                term /= xSquared;
                result -= term / divisor;

                divisor += 2;
                term /= xSquared;
                result += term / divisor;
            }

            return result;
        }
    }
}