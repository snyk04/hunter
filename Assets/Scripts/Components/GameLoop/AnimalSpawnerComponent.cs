using Classes.GameLoop;
using Hunter.AI.Common;
using UnityEngine;

namespace Hunter.GameLoop
{
    public class AnimalSpawnerComponent : MonoBehaviour
    {
        [Header("Amount of animals to spawn")]
        [SerializeField] private int _amountOfDeerGroups;
        [SerializeField] private int _amountOfRabbits;
        [SerializeField] private int _amountOfWolves;
        
        [Header("Animal prefabs")]
        [SerializeField] private GameObject _deerPrefab;
        [SerializeField] private GameObject _rabbitPrefab;
        [SerializeField] private GameObject _wolfPrefab;

        [Header("References")]
        [SerializeField] private FieldComponent _field;

        public AnimalSpawner AnimalSpawner { get; private set; }
        
        private void Awake()
        {
            int amountOfDeerGroups = AmountOfAnimalsTransmitter.AmountOfDeerGroups == 0
                ? _amountOfDeerGroups : AmountOfAnimalsTransmitter.AmountOfDeerGroups;
            int amountOfRabbits = AmountOfAnimalsTransmitter.AmountOfRabbits == 0
                ? _amountOfRabbits : AmountOfAnimalsTransmitter.AmountOfRabbits;
            int amountOfWolves = AmountOfAnimalsTransmitter.AmountOfWolves == 0
                ? _amountOfWolves : AmountOfAnimalsTransmitter.AmountOfWolves;

            AnimalSpawner = new AnimalSpawner(amountOfDeerGroups, amountOfRabbits, amountOfWolves,
                _deerPrefab, _rabbitPrefab, _wolfPrefab,
                _field.Field);
        }
        private void Start()
        {
            AnimalSpawner.SpawnAllAnimals();
        }
    }
}
