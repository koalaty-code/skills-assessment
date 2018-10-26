using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using FizzLib.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzLib.Tests.Services
{
    [TestClass]
    public class FizzBuzzTest
    {
        private IFizzBuzz _fizzBuzz;
        private Randomizer _random;

        [TestInitialize]
        public void BeforeEach()
        {
            _random = new Randomizer();

            _fizzBuzz = new FizzBuzz();
        }

        [TestClass]
        public class GetFizzBuzzTest : FizzBuzzTest
        {
            [TestMethod]
            public void ShouldWriteToConsoleForEveryNumberInRangeInclusive()
            {
                var expectedMin = _random.Number(1, 100);
                var expectedMax = expectedMin + _random.Number(1, 100);
                var expectedCount = expectedMax - expectedMin + 1;

                var actualList = _fizzBuzz.GetFizzBuzz(expectedMin, expectedMax);

                actualList.Count().Should().Be(expectedCount);
            }

            [TestMethod]
            public void ShouldWriteFizzWhenNumberIsDivisibleByThree()
            {
                var expectedNumber = _random.Number(1, 10) * 3;

                var actualResult = _fizzBuzz.GetFizzBuzz(expectedNumber, expectedNumber).First();

                actualResult.Should().Contain("Fizz");
            }

            [TestMethod]
            public void ShouldWriteBuzzWhenNumberIsDivisibleByFive()
            {
                var expectedNumber = _random.Number(1, 10) * 5;

                var actualResult = _fizzBuzz.GetFizzBuzz(expectedNumber, expectedNumber).First();

                actualResult.Should().Contain("Buzz");
            }

            [TestMethod]
            public void ShouldWriteFizzBuzzWhenNumberIsDivisibleByThreeAndFive()
            {
                var expectedNumber = _random.Number(1, 10) * 3 * 5;

                var actualResult = _fizzBuzz.GetFizzBuzz(expectedNumber, expectedNumber).First();

                actualResult.Should().Be("FizzBuzz");
            }

            [TestMethod]
            public void ShouldWriteValueForAnyCustomRulesPassedIn()
            {
                var expectedKey1 = _random.Number(1, 10);
                var expectedKey2 = expectedKey1 + _random.Number(1, 10);
                var expectedMinMax = expectedKey1 * expectedKey2;
                var expectedRules = new Dictionary<int, string>
                {
                    [expectedKey1] = _random.String2(_random.Number(1, 10)),
                    [expectedKey2] = _random.String2(_random.Number(1, 10))
                };

                var actualResult = _fizzBuzz.GetFizzBuzz(expectedMinMax, expectedMinMax, expectedRules).First();

                actualResult.Should().Contain(expectedRules[expectedKey1] + expectedRules[expectedKey2]);
            }

            [TestMethod]
            public void ShouldNotTryToDivideByZeroIfCustomRuleIsPassedInWithZeroAsKey()
            {
                var expectedRules = new Dictionary<int, string>
                {
                    [0] = _random.String(_random.Number(1, 10))
                };

                _fizzBuzz.GetFizzBuzz(additionalRules: expectedRules);
            }

            [TestMethod]
            public void ShouldReturnAfterMaxIntegerValueIsFizzBuzzed()
            {
                var actualList = _fizzBuzz.GetFizzBuzz(int.MaxValue, int.MaxValue);

                actualList.Count().Should().Be(1);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void ShouldThrowArgumentExceptionWhenMinExceedsMax()
            {
                var expectedMin = _random.Number(100);

                _fizzBuzz.GetFizzBuzz(expectedMin, expectedMin - 1).ToList();
            }
        }
    }
}
