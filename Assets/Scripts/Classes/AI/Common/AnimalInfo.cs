using Hunter.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.Common
{
    public class AnimalInfo
    {
        public readonly IAnimal Animal;
        public readonly Transform Transform;
        public readonly Mover Mover;
        public readonly Rigidbody2D Rigidbody2D;
        public readonly Field Field;

        public Vector2 Position => Transform.Position();
        
        public AnimalInfo(IAnimal animal, Transform transform, Mover mover, Rigidbody2D rigidbody2D, Field field)
        {
            Animal = animal;
            Transform = transform;
            Mover = mover;
            Rigidbody2D = rigidbody2D;
            Field = field;
        }
    }
}
