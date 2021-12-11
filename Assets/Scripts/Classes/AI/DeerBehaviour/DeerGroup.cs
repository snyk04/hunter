using Hunter.AI.Common;
using UnityEngine;
using Random = System.Random;

namespace Hunter.AI.DeerBehaviour
{
    public class DeerGroup
    {
        private const float SpawnDispersion = 3;
        
        private const int MinAmountOfDeer = 3;
        private const int MaxAmountOfDeer = 10;

        private readonly GameObject _deerPrefab;
        private readonly Vector2 _groupPosition;
        private readonly Field _field;
        
        public DeerGroup(GameObject deerPrefab, Vector2 groupPosition, Field field)
        {
            _deerPrefab = deerPrefab;
            _groupPosition = groupPosition;
            _field = field;
        }

        public void SpawnDeer()
        {
            Transform container = new GameObject().transform;
            container.name = "DeerGroup";
            
            var random = new Random();
            int amountOfDeer = random.Next(MinAmountOfDeer, MaxAmountOfDeer);
            for (int i = 0; i < amountOfDeer; i++)
            {
                Vector2 deerPosition = _groupPosition + UnityEngine.Random.insideUnitCircle * SpawnDispersion;

                GameObject deer = Object.Instantiate(_deerPrefab, deerPosition, Quaternion.identity, container);
                deer.GetComponent<DeerComponent>().Initialize(_field);
            }
        }
    }
}
