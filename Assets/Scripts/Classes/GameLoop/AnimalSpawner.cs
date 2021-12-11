using Hunter.AI.Common;
using Hunter.AI.DeerBehaviour;
using Hunter.AI.RabbitBehaviour;
using Hunter.AI.WolfBehaviour;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Classes.GameLoop
{
    public class AnimalSpawner
    {
        private readonly int _amountOfDeerGroups;
        private readonly int _amountOfRabbits;
        private readonly int _amountOfWolves;

        private readonly GameObject _deerGroupPrefab;
        private readonly GameObject _rabbitPrefab;
        private readonly GameObject _wolfPrefab;

        private readonly Field _field;

        public AnimalSpawner(int amountOfDeerGroups, int amountOfRabbits, int amountOfWolves,
            GameObject deerGroupPrefab, GameObject rabbitPrefab, GameObject wolfPrefab,
            Field field)
        {
            _amountOfDeerGroups = amountOfDeerGroups;
            _amountOfRabbits = amountOfRabbits;
            _amountOfWolves = amountOfWolves;

            _deerGroupPrefab = deerGroupPrefab;
            _rabbitPrefab = rabbitPrefab;
            _wolfPrefab = wolfPrefab;

            _field = field;
        }

        public void SpawnAllAnimals()
        {
            SpawnDeerGroups();
            SpawnRabbits();
            SpawnWolves();
        }
        
        private void SpawnDeerGroups()
        {
            Transform container = new GameObject().transform;
            container.name = "DeerGroups";
            
            for (int i = 0; i < _amountOfDeerGroups; i++)
            {
                Vector2 deerGroupPosition = Random.insideUnitCircle;
                deerGroupPosition.x *= _field.XRightBorder;
                deerGroupPosition.y *= _field.YTopBorder;

                GameObject deerGroup = Object.Instantiate(_deerGroupPrefab, deerGroupPosition, Quaternion.identity, container);
                deerGroup.GetComponent<DeerGroupComponent>().Initialize(_field);
            }
        }
        private void SpawnRabbits()
        {
            Transform container = new GameObject().transform;
            container.name = "Rabbits";
            
            for (int i = 0; i < _amountOfRabbits; i++)
            {
                Vector2 rabbitPosition = Random.insideUnitCircle;
                rabbitPosition.x *= _field.XRightBorder;
                rabbitPosition.y *= _field.YTopBorder;

                GameObject rabbit = Object.Instantiate(_rabbitPrefab, rabbitPosition, Quaternion.identity, container);
                rabbit.GetComponent<RabbitComponent>().Initialize(_field);
            }
        }
        private void SpawnWolves()
        {
            Transform container = new GameObject().transform;
            container.name = "Wolves";
            
            for (int i = 0; i < _amountOfWolves; i++)
            {
                Vector2 wolfPosition = Random.insideUnitCircle;
                wolfPosition.x *= _field.XRightBorder;
                wolfPosition.y *= _field.YTopBorder;

                GameObject wolf = Object.Instantiate(_wolfPrefab, wolfPosition, Quaternion.identity, container);
                wolf.GetComponent<WolfComponent>().Initialize(_field);
            }
        }
    }
}
