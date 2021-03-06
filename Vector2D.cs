﻿using System;
using System.Xml.Serialization;

namespace VectorAndPolygonMath
{
    public class Vector2D
    {
        #region EncapsulatedFilds
        private const double eps = 0.00001f;
        private float x;
        private float y;
        private float sqrLength;
        private float length;
        #endregion
        public static Vector2D NaN_Vector { get; } = new Vector2D(float.NaN, float.NaN);
        public float X
        #region X
        {
            get => x;
            set
            {
                length = sqrLength = float.NaN;
                x = value;
            }
        }
        #endregion
        public float Y
        #region Y
        {
            get => y;
            set
            {
                length = sqrLength = float.NaN;
                y = value;
            }
        }
        #endregion
        [XmlIgnore]
        public float SqrLength
        #region SqrtLength
        {
            get
            {
                if (float.IsNaN(sqrLength))
                    UpdateSqrLength();
                return sqrLength;
            }
        }
        private void UpdateSqrLength()
        {
            sqrLength = X * X + Y * Y;
        }
        #endregion
        [XmlIgnore]
        public float Length
        #region Length
        {
            get
            {
                if (float.IsNaN(length))
                    UpdateLength();

                return length;
            }
            set
            {
                if (float.IsNaN(length))
                    UpdateLength();

                if (length < eps)
                    X = Y = float.NaN;

                X /= length * value;
                Y /= length * value;
            }
        }
        private void UpdateLength()
        {
            length = (float)Math.Sqrt(SqrLength);
        }
        #endregion

        public Vector2D()
        {
            X = 0;
            Y = 0;
        }
        public Vector2D(Vector2D vector)
        {
            X = vector.X;
            Y = vector.Y;
        }
        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        #region operators
        static public Vector2D operator +(Vector2D Vector1, Vector2D Vector2) 
        {
            return new Vector2D(Vector1.X + Vector2.X, Vector1.Y + Vector2.Y);
        }
        static public Vector2D operator -(Vector2D Vector)
        {
            return new Vector2D(-Vector.X, -Vector.Y);
        }
        static public Vector2D operator -(Vector2D Vector1, Vector2D Vector2)
        {
            return new Vector2D(Vector1.X - Vector2.X, Vector1.Y - Vector2.Y);
        }
        static public Vector2D operator *(Vector2D Vector1, Vector2D Vector2)
        {
            return new Vector2D(Vector1.X * Vector2.X, Vector1.Y * Vector2.Y);
        }
        static public Vector2D operator *(Vector2D Vector1, float valuev)
        {
            return new Vector2D(Vector1.X * valuev, Vector1.Y * valuev);
        }
        static public Vector2D operator *(float valuev, Vector2D Vector1)
        {
            return Vector1 * valuev;
        }
        static public Vector2D operator /(Vector2D Vector1, Vector2D Vector2)
        {
            return new Vector2D(Vector1.X / Vector2.X, Vector1.Y / Vector2.Y);
        }
        static public Vector2D operator /(Vector2D Vector1, float valuev)
        {
            return new Vector2D(Vector1.X / valuev, Vector1.Y / valuev);
        }
        static public bool operator ==(Vector2D Vector1, Vector2D Vector2)
        {
            return (Vector1.X == Vector2.X) && (Vector1.Y == Vector2.Y);
        }
        static public bool operator !=(Vector2D Vector1, Vector2D Vector2)
        {
            return (Vector1.X != Vector2.X) || (Vector1.Y != Vector2.Y);
        }
        static public float operator %(Vector2D vec1, Vector2D vec2)
        {
            return vec1.X * vec2.Y - (vec1.Y * vec2.X);
        }
        static public Vector2D operator %(float a, Vector2D vec)
        {
            return new Vector2D(-a * vec.Y, vec.X * a);
        }
        static public Vector2D operator %(Vector2D vec, float a)
        {
            return new Vector2D(a * vec.Y, -vec.X * a);
        }

        #endregion

        static public float Dot(Vector2D vec1, Vector2D vec2)
        {
            return vec1.X * vec2.X + vec1.Y * vec2.Y;
        }
        public override bool Equals(object o)
        {
            Vector2D vector = o as Vector2D;
            if (vector == null)
                return false;
            return X == vector.X && Y == vector.Y;
        }
        public override int GetHashCode()
        {
            return (x.GetHashCode() << 16 + y.GetHashCode() >> 16); 
        }

        public Vector2D Normalize()
        {
            if (Length < eps)
                return new Vector2D(NaN_Vector);

            return new Vector2D(X / Length, Y / Length);
        }
        public Vector2D Normalize(float newLength)
        {
            return Normalize() * newLength;
        }
    }
}
