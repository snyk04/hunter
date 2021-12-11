using UnityEngine;
using UnityEngine.UI;

namespace Hunter.UI
{
    public class Window
    {
        private readonly GameObject[] _interfaceElements; 
        private readonly Button _buttonToSelect;
        
        public Window(GameObject[] interfaceElements, Button buttonToSelect)
        {
            _interfaceElements = interfaceElements;
            _buttonToSelect = buttonToSelect;
        }

        public void Start()
        {
            SelectDefaults();
        }
        
        public void Enable()
        {
            SetElementsActiveness(true);

            SelectDefaults();
        }
        public void Disable()
        {
            SetElementsActiveness(false);
        }
        
        private void SelectDefaults()
        {
            _buttonToSelect?.Select();
        }
        private void SetElementsActiveness(bool isActive)
        {
            foreach (GameObject interfaceElement in _interfaceElements)
            {
                interfaceElement.SetActive(isActive);
            }
        }
    }
}
