using System.Collections;
using Hunter.Creatures.Common;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Creatures
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
        
        [UnityTest]
        public IEnumerator TestMove()
        {
            CreateMover(out Mover mover, out Rigidbody2D rigidbody);
            
            mover.Move(_direction);
            yield return null;
            Assert.AreEqual(_direction * Speed, rigidbody.velocity);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestStop()
        {
            CreateMover(out Mover mover, out Rigidbody2D rigidbody);
            
            mover.Move(_direction);
            yield return null;
            mover.Stop();
            yield return null;
            Assert.AreEqual(Vector2.zero, rigidbody.velocity);
            yield return null;
        }
    }
}
