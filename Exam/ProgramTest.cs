using System;
using System.Linq;
using NUnit.Framework;
using Exam;

namespace ExamTests
{
    [TestFixture]
    public class ProgramTestSuite
    {
        //Class Constructor
        public ProgramTestSuite()
        {
        }

        //Test 01 : Post Range Fails
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(101)]
        public void TestForOutOfRangePostValues(int value)
        {
            var result = Program.processStatistics(value);
            Assert.IsFalse(result, $"{value} is not a valid range");
        }

        //Test 02 : Post Range Success
        [TestCase(1)]
        [TestCase(100)]
        public void TestForInRangePostValues(int value)
        {
            var result = Program.processStatistics(value);
            Assert.IsTrue(result, $"{value} is a valid range");
        }
    }
}
