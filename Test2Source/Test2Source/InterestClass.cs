using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2Source
{
    public class InterestClass
    {
        // Calculate compound interest
        public static double CompoundInterestForecast(double principalValue,double interestRate,int compoundRate,double numYears)
        {
            // Get modifier
            double modifier = 1 + (interestRate / compoundRate);

            // Get exponent
            double exponent = compoundRate * numYears;

            // Get multiplier
            double multiplier = Math.Pow(modifier, exponent);

            // Get value
            double totalValue = Math.Round((principalValue * multiplier),2);

            return totalValue;
        }
    }
}
