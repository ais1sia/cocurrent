using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Dane;
using Logika;
using Dane.Components;

namespace Logika {
    public class Kolizja {

        public static bool IsBallsCollides(InterfaceKulka ball, InterfaceKulka other) {
            var centerOne = ball.Transform.Position + (Vector2.One * ball.Transform.Diameter / 2) + ball.RigidBody.Velocity * (16 / 1000f);
            var centerTwo = other.Transform.Position + (Vector2.One * other.Transform.Diameter / 2) + other.RigidBody.Velocity * (16 / 1000f);
  
            var distance = Vector2.Distance(centerOne, centerTwo);
            var diameterSum = (ball.Transform.Diameter + other.Transform.Diameter) / 2f;

            return distance <= diameterSum;
        }

        public static void HandleCollision(InterfaceKulka ball, InterfaceKulka other) {
            var centerOne = ball.Transform.Position + (Vector2.One * ball.Transform.Diameter / 2);
            var centerTwo = other.Transform.Position + (Vector2.One * other.Transform.Diameter / 2);
            
            var unitNormalVector = Vector2.Normalize(centerTwo - centerOne);
            var unitTangentVector = new Vector2(-unitNormalVector.Y, unitNormalVector.X);

            var velocityOneNormal = Vector2.Dot(unitNormalVector, ball.RigidBody.Velocity);
            var velocityOneTangent = Vector2.Dot(unitTangentVector, ball.RigidBody.Velocity);
            var velocityTwoNormal = Vector2.Dot(unitNormalVector, other.RigidBody.Velocity);
            var velocityTwoTangent = Vector2.Dot(unitTangentVector, other.RigidBody.Velocity);

            var newNormalVelocityOne = (velocityOneNormal * (ball.RigidBody.Mass - other.RigidBody.Mass) + 
                2 * other.RigidBody.Mass * velocityTwoNormal) / (ball.RigidBody.Mass + other.RigidBody.Mass);
            var newNormalVelocityTwo = (velocityTwoNormal * (other.RigidBody.Mass - ball.RigidBody.Mass) + 
                2 * ball.RigidBody.Mass * velocityOneNormal) / (ball.RigidBody.Mass + other.RigidBody.Mass);

            var newVelocityOne = Vector2.Multiply(unitNormalVector, newNormalVelocityOne) + Vector2.Multiply(unitTangentVector, velocityOneTangent);
            var newVelocityTwo = Vector2.Multiply(unitNormalVector, newNormalVelocityTwo) + Vector2.Multiply(unitTangentVector, velocityTwoTangent);

            ball.RigidBody.Velocity = newVelocityOne;
            other.RigidBody.Velocity = newVelocityTwo;
        }
    }
}
