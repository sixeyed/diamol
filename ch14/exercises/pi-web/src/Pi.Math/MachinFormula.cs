using System;
using System.Collections.Generic;
using System.Linq;

namespace Pi.Math
{
    public static class MachinFormula
    {
        /// <summary>
        /// Calculates Pi to the specified number of decimal places
        /// </summary>
        /// <see cref="https://latkin.org/blog/2012/03/20/how-to-calculate-1-million-digits-of-pi/"/>
        /// <param name="decimalPlaces">Decimal places to calculate</param>
        /// <param name="recordMetrics">Whether to record calculation metrics</param>
        /// <returns>Pi</returns>
        public static HighPrecision Calculate(int decimalPlaces)
        {
            return CalculateInternal(decimalPlaces);
        }

        private static HighPrecision CalculateInternal(int decimalPlaces)
        {
            HighPrecision.Precision = decimalPlaces;
            HighPrecision first = 4 * Atan.Calculate(5);
            HighPrecision second = Atan.Calculate(239);
            var pi = 4 * (first - second);
            return pi;
        }
    }
}
