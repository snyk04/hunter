using UnityEngine;
using UnityEngine.UI;

namespace Hunter.UI
{
    public class WindowComponent : MonoBehaviour
    {
        [SerializeField] protected GameObject[] _interfaceElements;
        [SerializeField] protected Button _buttonToSelect;

        public Window Window { get; protected set; }
        
        private void Awake()
        {
            Window = new Window(_interfaceElements, _buttonToSelect);
        }
        private void Start()
        {
            Window.Start();
        }

        public void Enable()
        {
            Window.Enable();
        }
        public void Disable()
        {
            Window.Disable();
        }
    }
}
