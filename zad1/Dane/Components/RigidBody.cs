using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Dane.Components {
    public interface IRigidBody {
        Vector2 Velocity { get; set; }
        float Mass { get; set; }
    }

    internal class RigidBody : IRigidBody {
        public Vector2 Velocity { get; set; }
        public float Mass { get; set; }

        public RigidBody (
            Vector2 newVelocity,
            float newMass
        ) {
            Velocity = newVelocity;
            Mass = newMass;
        }
    }
}
