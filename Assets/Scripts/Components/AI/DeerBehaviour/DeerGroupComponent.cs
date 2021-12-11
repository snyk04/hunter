using Hunter.AI.Common;
using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public class DeerGroupComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _deerPrefab;

        public DeerGroup DeerGroup { get; private set; }
        
        public void Initialize(Field field)
        {
            DeerGroup = new DeerGroup(_deerPrefab, transform.position, field);
            
            DeerGroup.SpawnDeer();
        }
    }
}
