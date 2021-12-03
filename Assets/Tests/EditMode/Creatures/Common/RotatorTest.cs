using System;
using Hunter.Creatures.Common;
using NUnit.Framework;
using UnityEngine;

namespace Creatures.Common
{
    public class RotatorTest
    {
        [Test]
        public void TestRotate()
        {
            Transform transform = new GameObject().transform;
            var rotator = new Rotator(transform);
            
            rotator.LookAt(new Vector2(1, 1));
            Assert.AreEqual(315, Math.Round(transform.eulerAngles.z));
            
            rotator.LookAt(new Vector2(-1, 1));
            Assert.AreEqual(45, Math.Round(transform.eulerAngles.z));
        }
    }
}
