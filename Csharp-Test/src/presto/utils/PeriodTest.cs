using presto.value;
using NUnit.Framework;
using System;
using presto.grammar;

namespace presto.utils
{


	[TestFixture]
    public class PeriodTest
    {

        [Test]
        public void TestMany()
        {
            TestOne("P1Y");
            TestOne("P1Y2M");
            TestOne("P2M");
            TestOne("P3D");
            TestOne("PT5H");
            TestOne("PT1.123S");


        }

        private void TestOne(String s)
        {
            Period period = Period.Parse(s);
            Assert.AreEqual(s, period.ToString());
        }


    }
}
