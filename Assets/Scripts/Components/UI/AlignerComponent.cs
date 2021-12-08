using UnityEngine;

namespace Hunter.UI
{
    public class AlignerComponent : MonoBehaviour
    {
        [SerializeField] private Transform _alignedObject;
        [SerializeField] private Transform _goalObject;
        
        [Header("Constraints")]
        [SerializeField] private bool _freezeX;
        [SerializeField] private bool _freezeY;
        [SerializeField] private bool _freezeZ;
        
        private Aligner _aligner;

        private void Awake()
        {
            _aligner = new Aligner(_alignedObject, _goalObject, _freezeX, _freezeY, _freezeZ);
        }
        private void Update()
        {
            _aligner.Align();
        }
    }
}
