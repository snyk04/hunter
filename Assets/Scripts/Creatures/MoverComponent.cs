using Hunter.Creatures;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoverComponent : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    public Mover Mover { get; private set; }

    private void Awake()
    {
        Mover = new Mover(_speed, GetComponent<Rigidbody2D>());
    }
}
