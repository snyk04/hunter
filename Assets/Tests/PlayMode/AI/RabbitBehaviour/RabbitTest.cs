using System;
using System.Collections;
using Hunter.AI.RabbitBehaviour;
using Hunter.Creatures.Common;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace AI.RabbitBehaviour
{
    public class RabbitTest
    {
        private const int TestSceneId = 1;
        
        private const float WanderingSpeed = 0.5f;
        private const float WanderingRadius = 3;
        private const float DetectionRadius = 5;

        private const float FleeSpeed = 1.5f;
        private const float FleeStopDistance = 7;

        private void CreateRabbit(out Rabbit rabbit, out Rigidbody2D rigidbody2D)
        {
            var rabbitObject = new GameObject();
            rabbitObject.AddComponent<CircleCollider2D>();
            rigidbody2D = rabbitObject.AddComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 0;
            Mover mover = rabbitObject.AddComponent<MoverComponent>().Mover;
            rabbit = new Rabbit(rabbitObject.transform, mover, WanderingSpeed, WanderingRadius, DetectionRadius,
                FleeSpeed, FleeStopDistance);
        }
        private void CreatePursuer(out GameObject pursuer)
        {
            pursuer = new GameObject
            {
                transform =
                {
                    position = new Vector3(DetectionRadius, 0, 0)
                }
            };
            pursuer.AddComponent<Rigidbody2D>().gravityScale = 0;
            pursuer.AddComponent<CircleCollider2D>();
            pursuer.AddComponent<MoverComponent>();
        }
        
        [UnityTest]
        public IEnumerator FleeToWanderingStateTest()
        {
            SceneManager.LoadScene(TestSceneId);
            yield return null;

            CreateRabbit(out Rabbit rabbit, out Rigidbody2D rigidbody2D);
            CreatePursuer(out GameObject pursuer);
            rabbit.Update();
            
            Object.Destroy(pursuer);
            yield return null;
            rabbit.Update();
            rabbit.Update();
            Assert.AreEqual(WanderingSpeed, Math.Round(rigidbody2D.velocity.magnitude, 1));
        }
        [UnityTest]
        public IEnumerator WanderingToFleeTest()
        {
            SceneManager.LoadScene(TestSceneId);
            yield return null;
            
            CreateRabbit(out Rabbit rabbit, out Rigidbody2D rigidbody2D);
            rabbit.Update();
            Assert.AreEqual(WanderingSpeed, Math.Round(rigidbody2D.velocity.magnitude, 1));

            CreatePursuer(out GameObject pursuer);
            rabbit.Update();
            yield return null;
            rabbit.Update();
            Assert.AreEqual(FleeSpeed, Math.Round(rigidbody2D.velocity.magnitude, 1));
        }
    }
}
