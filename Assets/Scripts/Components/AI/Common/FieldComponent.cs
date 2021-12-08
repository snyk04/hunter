using UnityEngine;

namespace Hunter.AI.Common
{
    public class FieldComponent : MonoBehaviour
    {
        [SerializeField] private float _xLeftBorder;
        [SerializeField] private float _xRightBorder;
        [SerializeField] private float _yBotBorder;
        [SerializeField] private float _yTopBorder;

        public Field Field { get; private set; }
        
        private void Awake()
        {
            Field = new Field(_xLeftBorder, _xRightBorder, _yBotBorder, _yTopBorder);
        }
    }
}
