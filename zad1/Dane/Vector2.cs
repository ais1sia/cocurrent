using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public static readonly Vector2 Zero = new Vector2(0, 0);

        public float X { get; set; }
        public float Y { get; set; }
        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(Vector2 other)
        {
            float xDiff = X - other.X;
            float yDiff = Y - other.Y;
            return xDiff * xDiff + yDiff * yDiff < 9.99999944E-11f;
        }

        public bool CzyZero()
        {
            return Equals(Zero);
        }

        public void Deconstruct(out float x, out float y)
        {
            x = this.X;
            y = this.Y;
        }


        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2
            {
                X = lhs.X + rhs.X,
                Y = lhs.Y + rhs.Y,
            };
        }
       
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return !(lhs == rhs);
        }
        
    }
}
