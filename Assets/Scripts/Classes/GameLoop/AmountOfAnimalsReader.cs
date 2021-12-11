using UnityEngine.UI;

namespace Classes.GameLoop
{
    public class AmountOfAnimalsReader
    {
        private readonly InputField _amountOfDeerGroupsField;
        private readonly InputField _amountOfRabbitsField;
        private readonly InputField _amountOfWolvesField;

        public AmountOfAnimalsReader(InputField amountOfDeerGroupsField, InputField amountOfRabbitsField, InputField amountOfWolvesField)
        {
            _amountOfDeerGroupsField = amountOfDeerGroupsField;
            _amountOfRabbitsField = amountOfRabbitsField;
            _amountOfWolvesField = amountOfWolvesField;
        }

        public void Read()
        {
            AmountOfAnimalsTransmitter.AmountOfDeerGroups = int.Parse(_amountOfDeerGroupsField.text);
            AmountOfAnimalsTransmitter.AmountOfRabbits = int.Parse(_amountOfRabbitsField.text);
            AmountOfAnimalsTransmitter.AmountOfWolves = int.Parse(_amountOfWolvesField.text);
        }
    }
}
