using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Dane.Components {
    public interface ITransform {
        Vector2 Position { get; set; }
        float Diameter { get; set; }
    }
    
    internal class Transform : ITransform {
        public Vector2 Position { get; set; }
        public float Diameter { get; set; }
    
        public Transform (
            Vector2 newVelocity,
            float newDiameter
        ) {
            Position = newVelocity;
            Diameter = newDiameter;
        }
    }
}
