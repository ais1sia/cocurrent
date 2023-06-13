using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);
        public static readonly Vector2 Up = new Vector2(0f, 1f);
        public static readonly Vector2 Down = new Vector2(0f, -1f);
        public static readonly Vector2 Left = new Vector2(-1f, 0f);
        public static readonly Vector2 Right = new Vector2(1f, 0f);

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

        public static float Dystans2(Vector2 p1, Vector2 p2)
        {
            float roznicaX = p1.X - p2.X;
            float roznicaY = p1.Y - p2.Y;
            return roznicaX * roznicaX + roznicaY * roznicaY;
        }

        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2
            {
                X = lhs.X + rhs.X,
                Y = lhs.Y + rhs.Y,
            };
        }

        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2
            {
                X = lhs.X - rhs.X,
                Y = lhs.Y - rhs.Y,
            };
        }

        public static float Dystans(Vector2 p1, Vector2 p2)
        {
            return MathF.Sqrt(Dystans2(p1, p2));
        }

        public static float Skalar(Vector2 p1, Vector2 p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y;
        }

        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return !(lhs == rhs);
        }

        public static Vector2 operator *(Vector2 lhs, float d)
        {
            return new Vector2
            {
                X = lhs.X * d,
                Y = lhs.Y * d,
            };
        }
        public static Vector2 operator *(float d, Vector2 lhs)
        {
            return new Vector2
            {
                X = lhs.X * d,
                Y = lhs.Y * d,
            };
        }
        public static Vector2 operator -(Vector2 lhs, float d)
        {
            return new Vector2
            {
                X = lhs.X - d,
                Y = lhs.Y - d,
            };
        }

        public static Vector2 Normal(Vector2 vector)
        {
            return new Vector2(-vector.Y, vector.X);
        }

        public static float Dot(Vector2 vector1, Vector2 vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y;
        }

        public static float Magnitude(Vector2 v)
        {
            return MathF.Sqrt(v.X * v.X + v.Y * v.Y);
        }

        public static Vector2 operator /(Vector2 lhs, float d)
        {
            return new Vector2
            {
                X = lhs.X / d,
                Y = lhs.Y / d,
            };
        }
        public static Vector2 operator /(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2
            {
                X = lhs.X / rhs.X,
                Y = lhs.Y / rhs.Y,
            };
        }

        public static Vector2 operator /(float d, Vector2 lhs)
        {
            return new Vector2
            {
                X = d / lhs.X,
                Y = d / lhs.Y,
            };
        }
    }
}
