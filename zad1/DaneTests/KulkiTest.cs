using System.Numerics;
using Dane.Components;
using NUnit.Framework;

namespace Dane.Tests {
    public class Tests {
        private DaneAPI balls;
        private InterfaceKulka ball1, ball2, ball3;

        // Setup:
        // 

        [SetUp]
        public void Setup() {

            ITransform transformTest1 = DaneAPI.CreateTransform(new Vector2(5, 10), 1.0f);
            ITransform transformTest2 = DaneAPI.CreateTransform(new Vector2(8, 4), 1.5f);
            ITransform transformTest3 = DaneAPI.CreateTransform(new Vector2(2, 9), 1.2f);

            IRigidBody rigidBodyTest1 = DaneAPI.CreateRigidBody(new Vector2(0.9f, 1.0f), 1.0f);
            IRigidBody rigidBodyTest2 = DaneAPI.CreateRigidBody(new Vector2(0.9f, 1.2f), 0.5f);
            IRigidBody rigidBodyTest3 = DaneAPI.CreateRigidBody(new Vector2(1.1f, 0.9f), 0.7f);

            balls = DaneAPI.CreateBallsList();
            ball1 = DaneAPI.CreateBall(0, transformTest1, rigidBodyTest1);
            ball2 = DaneAPI.CreateBall(1, transformTest2, rigidBodyTest2);
            ball3 = DaneAPI.CreateBall(2, transformTest3, rigidBodyTest3);
        }

        // Test:
        // 

        [Test]
        public void AddBallTest() {

            balls.Add(ball1);
            Assert.AreEqual(1, balls.GetCount());
            balls.Add(ball2);
            Assert.AreEqual(2, balls.GetCount());
            balls.Add(ball3);
            Assert.AreEqual(3, balls.GetCount());

            Assert.AreEqual(ball1, balls.Get(0));
            Assert.AreEqual(ball2, balls.Get(1));
            Assert.AreEqual(ball3, balls.Get(2));
        }
    }
}