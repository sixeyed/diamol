using System.Numerics;
using System.Text;

namespace Pi.Math
{
    public class HighPrecision
    {
        private static BigInteger denom;
        private static int precision;
        private static int slop = 4;
        private BigInteger num;

        public HighPrecision(BigInteger numerator, BigInteger denominator)
        {
            // public constructor rescales numerator as needed
            num = (HighPrecision.denom * numerator) / denominator;
        }

        private HighPrecision(BigInteger numerator)
        {
            // private constructor for when we already know the scaling
            num = numerator;
        }

        public static int Precision
        {
            get { return precision; }
            set
            {
                HighPrecision.precision = value;
                denom = BigInteger.Pow(10, HighPrecision.precision + slop);  // leave some buffer
            }
        }

        public bool IsZero
        {
            get { return num.IsZero; }
        }

        public BigInteger Numerator
        {
            get { return num; }
        }

        public BigInteger Denominator
        {
            get { return HighPrecision.denom; }
        }

        public static HighPrecision operator *(int left, HighPrecision right)
        {
            // no need to resale when multiplying by an int
            return new HighPrecision(right.num * left);
        }

        public static HighPrecision operator *(HighPrecision left, HighPrecision right)
        {
            // a/D * b/D = ab/D^2 = (ab/D)/D
            return new HighPrecision((left.num * right.num) / HighPrecision.denom);
        }

        public static HighPrecision operator /(HighPrecision left, int right)
        {
            // no need to rescale when dividing by an int
            return new HighPrecision(left.num / right);
        }

        public static HighPrecision operator +(HighPrecision left, HighPrecision right)
        {
            // when we know the denominators are the same, can just add the numerators
            return new HighPrecision(left.num + right.num);
        }

        public static HighPrecision operator -(HighPrecision left, HighPrecision right)
        {
            // when we know the denominators are the same, can just subtract the numerators
            return new HighPrecision(left.num - right.num);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // pull out the integer part
            BigInteger remain;
            BigInteger quotient = BigInteger.DivRem(num, HighPrecision.denom, out remain);
            int integerDigits = quotient.IsZero ? 1 : (int)BigInteger.Log10(quotient) + 1;
            sb.Append(quotient.ToString());

            int i = 0;
            BigInteger smallDenom = HighPrecision.denom / 10;
            BigInteger tempRemain;

            // pull out all of the 0s after the decimal point
            while (i++ < HighPrecision.Precision && (quotient = BigInteger.DivRem(remain, smallDenom, out tempRemain)).IsZero)
            {
                smallDenom /= 10;
                remain = tempRemain;
                sb.Append('0');
            }

            // append the rest of the remainder
            sb.Append(remain.ToString());

            // truncate and insert the decimal point
            return sb.ToString().Remove(integerDigits + HighPrecision.Precision).Insert(integerDigits, ".");
        }
    }
}