using Hunter.Creatures.Common;
using NUnit.Framework;
using UnityEngine;

namespace Creatures.Common
{
    public class MoverTest
    {
        private const int Speed = 5;
        private readonly Vector2 _direction = Vector2.right;

        private void CreateMover(out Mover mover, out Rigidbody2D rigidbody)
        {
            var gameObject = new GameObject();
            rigidbody = gameObject.AddComponent<Rigidbody2D>();
            rigidbody.isKinematic = true;

            mover = new Mover(Speed, rigidbody);
        }
        
        [Test]
        public void TestMove()
        {
            CreateMover(out Mover mover, out Rigidbody2D rigidbody);
            
            mover.Move(_direction);
            Assert.AreEqual(_direction * Speed, rigidbody.velocity);
        }
        
        [Test]
        public void TestStop()
        {
            CreateMover(out Mover mover, out Rigidbody2D rigidbody);
            
            mover.Move(_direction);
            mover.Stop();
            Assert.AreEqual(Vector2.zero, rigidbody.velocity);
        }
    }
}
