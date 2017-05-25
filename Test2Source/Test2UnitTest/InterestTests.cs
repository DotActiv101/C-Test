using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test2Source;

namespace Test2UnitTest
{
    [TestClass]
    public class InterestTests
    {
        [TestMethod]
        public void CompoundInt_ValidAmounts_ReturnsBalance()
        {
            // Declare required amounts
            double principalValue = 5000;
            double interestRate = 0.05;
            int compoundRate = 12;
            double numYears = 10;

            // Get compund interest
            double returnValue = InterestClass.CompoundInterestForecast(principalValue, interestRate, compoundRate, numYears);

            // Verify balance based on test values  
            double testBlance = 8235.05;

            Assert.AreEqual(returnValue, testBlance, 0.01, "Final values is incorrect");
        }
    }
}
