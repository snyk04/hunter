using System;
using System.Collections;
using Hunter.Common;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Common
{
    public class DateTimeExtensionsTest
    {
        private const int SecondsToWait = 2;
        
        [UnityTest]
        public IEnumerator GetPassedSecondsTest()
        {
            DateTime timePoint = DateTime.Now;

            yield return new WaitForSeconds(SecondsToWait);
            Assert.AreEqual(SecondsToWait, Math.Round(timePoint.GetPassedSeconds()));
        }
    }
}
