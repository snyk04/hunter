using Hunter.UI;
using NUnit.Framework;
using UnityEngine;

namespace UI
{
    public class AlignerTest
    {
        private const bool FreezeX = false;
        private const bool FreezeY = false;
        private const bool FreezeZ = true;

        private readonly Vector3 _newPosition = Vector3.up;
        
        [Test]
        public void TestAlign()
        {
            Transform alignedObject = new GameObject().transform;
            Transform goalObject = new GameObject().transform;
            
            var aligner = new Aligner(alignedObject, goalObject, FreezeX, FreezeY, FreezeZ);
            goalObject.transform.position = Vector3.up;
            aligner.Align();
            
            Assert.AreEqual(_newPosition, alignedObject.transform.position);
        }
    }
}
