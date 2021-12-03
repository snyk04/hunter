using System;
using System.Collections;
using Hunter.Creatures.Common;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Creatures.Common
{
    public class RotatorTest
    {
        [UnityTest]
        public IEnumerator TestRotate()
        {
            Transform transform = new GameObject().transform;
            var rotator = new Rotator(transform);
            
            rotator.LookAt(new Vector2(1, 1));
            yield return null;
            Assert.AreEqual(315, Math.Round(transform.eulerAngles.z));
            
            rotator.LookAt(new Vector2(-1, 1));
            yield return null;
            Assert.AreEqual(45, Math.Round(transform.eulerAngles.z));
        }
    }
}
