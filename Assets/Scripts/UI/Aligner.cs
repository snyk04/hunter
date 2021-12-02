using UnityEngine;

namespace Hunter.UI
{
    public class Aligner
    {
        private readonly Transform _alignedObject;
        private readonly Transform _goalObject;
        private readonly bool _freezeX;
        private readonly bool _freezeY;
        private readonly bool _freezeZ;

        public Aligner(Transform alignedObject, Transform goalObject, bool freezeX, bool freezeY, bool freezeZ)
        {
            _alignedObject = alignedObject;
            _goalObject = goalObject;

            _freezeX = freezeX;
            _freezeY = freezeY;
            _freezeZ = freezeZ;
        }

        public void Align()
        {
            Vector3 alignedObjectPosition = _alignedObject.position;
            Vector3 goalObjectPosition = _goalObject.position;
            
            float x = _freezeX ? alignedObjectPosition.x : goalObjectPosition.x;
            float y = _freezeY ? alignedObjectPosition.y : goalObjectPosition.y;
            float z = _freezeZ ? alignedObjectPosition.z : goalObjectPosition.z;

            _alignedObject.transform.position = new Vector3(x, y, z);
        }
    }
}
