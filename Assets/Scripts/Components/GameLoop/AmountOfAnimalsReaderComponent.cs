using Classes.GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace Components.GameLoop
{
    public class AmountOfAnimalsReaderComponent : MonoBehaviour
    {
        [SerializeField] private InputField _amountOfDeerGroupsField;
        [SerializeField] private InputField _amountOfRabbitsField;
        [SerializeField] private InputField _amountOfWolvesField;
        
        public AmountOfAnimalsReader AmountOfAnimalsReader { get; private set; }

        private void Awake()
        {
            AmountOfAnimalsReader =
                new AmountOfAnimalsReader(_amountOfDeerGroupsField, _amountOfRabbitsField, _amountOfWolvesField);
        }

        public void Read()
        {
            AmountOfAnimalsReader.Read();
        }
    }
}
