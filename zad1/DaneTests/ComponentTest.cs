using System.Numerics;
using NUnit.Framework;
using Dane;
using Dane.Components;

namespace Dane.Tests {
    internal class ComponentTest {
        private ITransform transform1, transform2;
        private IRigidBody rigidBody1, rigidBody2;

        // Setup:
        // 

        [SetUp]
        public void Setup() {
            transform1 = DaneAPI.CreateTransform(new Vector2(5, 9), 0.9f);
            transform2 = DaneAPI.CreateTransform(new Vector2(5, 9), 1.2f);

            rigidBody1 = DaneAPI.CreateRigidBody(new Vector2(0.8f, 1.1f), 0.8f);
            rigidBody2 = DaneAPI.CreateRigidBody(new Vector2(0.8f, 1.1f), 0.9f);
        }

        [Test]
        public void CreateTransformTest() {
            Assert.NotNull(transform1);
            Assert.AreNotEqual(transform1, transform2);
            Assert.AreEqual(transform1.Position, transform2.Position);
            Assert.AreNotEqual(transform1.Diameter, transform2.Diameter);
        }

        [Test]
        public void CreateRigidbodyTest() {
            Assert.NotNull(rigidBody1);
            Assert.AreNotEqual(rigidBody1, rigidBody2);
            Assert.AreEqual(rigidBody1.Velocity, rigidBody2.Velocity);
            Assert.AreNotEqual(rigidBody1.Mass, rigidBody2.Mass);
        }
    }
}
