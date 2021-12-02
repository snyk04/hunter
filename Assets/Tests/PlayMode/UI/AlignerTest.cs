using System.Collections;
using Hunter.UI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace UI
{
    public class AlignerTest
    {
        private const bool FreezeX = false;
        private const bool FreezeY = false;
        private const bool FreezeZ = true;

        private readonly Vector3 NewPosition = Vector3.up;
        
        [UnityTest]
        public IEnumerator TestAlign()
        {
            Transform alignedObject = new GameObject().transform;
            Transform goalObject = new GameObject().transform;
            
            var aligner = new Aligner(alignedObject, goalObject, FreezeX, FreezeY, FreezeZ);
            goalObject.transform.position = Vector3.up;
            aligner.Align();
            
            Assert.AreEqual(NewPosition, alignedObject.transform.position);
            
            yield return null;
        }
    }
}
