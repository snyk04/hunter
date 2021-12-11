using UnityEngine;
using UnityEngine.UI;

namespace Hunter.UI
{
    public class WindowComponent : MonoBehaviour
    {
        [SerializeField] private GameObject[] _interfaceElements;
        [SerializeField] private Button _buttonToSelect;

        public Window Window { get; private set; }
        
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
