using System;

namespace VectorAndPolygonMath
{
    class Vector2D
    {
        #region Incaps
        bool notActualLenght;
        bool notActualSqrLenght;
        private float x;
        private float y;
        private float sqrLenght;
        private float lenght;
        #endregion
        public float X
        {
            get => x; 
            set
            {
                notActualLenght = true;
                notActualSqrLenght = true;
                x = value;
            }
        }
        public float Y
        {
            get => y; 
            set
            {
                notActualLenght = true;
                notActualSqrLenght = true;
                y = value;
            }
        }
        public float SqrLength
        {
            get
            {
                if (notActualSqrLenght)
                    UpdateSqrLength();
                return sqrLenght;
            }
        }
        public float Length
        {
            get
            {
                if (notActualLenght)
                    UpdateLength();

                return lenght;
            }
            set
            {
                if (notActualLenght)
                    UpdateLength();

                if (lenght < 0.0001f)
                    X = Y = float.MaxValue;

                X /= lenght * value;
                Y /= lenght * value;
            }
        }
        
        public Vector2D(float x, float y)
        {
            X = x; 
            Y = y;
        }
        
        private void UpdateLength()
        {
            lenght = (float)Math.Sqrt(sqrLenght);
            notActualLenght = false;
        }
        private void UpdateSqrLength()
        {
            sqrLenght = X * X + Y * Y;
            notActualLenght = false;
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
            return (Vector1.X == Vector2.X) && (Vector1.X == Vector2.X);
        }
        static public bool operator !=(Vector2D Vector1, Vector2D Vector2)
        {
            return (Vector1.X != Vector2.X) || (Vector1.X != Vector2.X);
        }

        public override bool Equals(object o)
        {
            if (o == null)
                return false;
            return true;
        }

        public override int GetHashCode() { return 0; }
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

        public Vector2D GetCuted()
        {
            if (Length < 0.0001) 
                return new Vector2D(float.MaxValue, float.MaxValue);

            return new Vector2D(X / Length, Y / Length);
        }
    }
}
