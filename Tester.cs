using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections;

namespace Project
{
    [TestFixture]
    class Tester
    {
        [Test]
        public void FindAvgContractsPerCust()
        {
            int contracts = 2;
            int contracts1 = 1;
            int contracts2 = 1;
            int contracts3 = 2;
            int contracts4 = 2;

            int customers = 5;
            int avgContracts = ((contracts + contracts1 + contracts2 + contracts3 + contracts4) / customers);

            Assert.AreEqual(avgContracts, 1);
        }
        [Test]
        public void FindOpenContracts()
        {
            String[] list = new String[3];
            list[0] = "open";
            list[1] = "open";
            list[2] = "closed";
            int totalOpen = 0;

            for (int i = 0; i < list.Length; i++)
            {
                if (list.GetValue(i).Equals("open"))
                {
                    int open = 1;
                    totalOpen += open;
                }
            }

            Assert.AreEqual(totalOpen, 2);
        }

        [Test]
        public void AvgContractLength()
        {
            int a = 9;
            int a1 = 5;
            int b = 8;
            int b2 = 4;
            int c = 2;
            int c2 = 1;

            int d = 3;

            int a2 = a - a1;
            int b3 = b - b2;
            int c3 = c - c2;

            int average = (a2 + b3 + c3 / d);

            Assert.AreEqual(average, 8);

        }
        [Test]
        public void FindContractTime()
        {
            int startDate = 2019;
            int endDate = 2022;

            int timeLeft = (endDate - startDate);

            Assert.AreEqual(timeLeft, 3);
        }
        [Test]
        public void FindAvgContractValue()
        {
            int contractValue1 = 3000;
            int contractValue2 = 1500;
            int numOfContracts = 2;

            int avgContractVal = ((contractValue1 + contractValue2) / numOfContracts);

            Assert.AreEqual(avgContractVal, 2250);
        }
       
       
    }
}
