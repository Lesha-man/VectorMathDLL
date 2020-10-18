using System;
using System.Linq;

namespace VectorAndPolygonMath
{
    static class Geometry
    {
        static private Vector2D MinDistansePointLineSigment(Vector2D a, Vector2D b, Vector2D point) //ближайшая точка на отрезке к точке point
        {
            Vector2D l = a - b;
            float t = Vector2D.Dot(a - point, l) / l.SqrLength;
            if      (t <= 0) return a;
            else if (t >= 1) return b;
            else             return a - l * t;
        }
        static public float MinDistansePointСontour(Vector2D[] vertex, Vector2D point, out Vector2D closestVectorPoly) //ближайшая точка на контуре многоугольника vertex к точке point, возвращает расстояние между ними
        {
            Vector2D closestVectorSegment;
            closestVectorPoly = MinDistansePointLineSigment(vertex[vertex.Length], vertex[0], point);
            float Rmin = Distance(closestVectorPoly, point);
            float R;
            for (int i = 1; i < vertex.Length; i++)
            {
                closestVectorSegment = MinDistansePointLineSigment(vertex[i - 1], vertex[i], point);
                R = Distance(closestVectorSegment, point);
                if (R < Rmin)
                {
                    Rmin = R;
                    closestVectorPoly = closestVectorSegment;
                }
            }
            return Rmin;
        }
        static public float Distance(Vector2D a, Vector2D b) //расстояние между двумя точками
        {
            return (b - a).Length;
        }

        static private float LineSide(Vector2D a, Vector2D b, Vector2D point) //по какую сторону находится точка point от прямой (a, b)(смотреть по знаку возвращаемого числа)
        {
            return (a.X - point.X) * (b.Y - a.Y) - (b.X - a.X) * (a.Y - point.Y);
        }

        static public bool InPoly(Vector2D[] vertex, Vector2D point) //находится ли точка point внутри выпуклого многоугольника. Точки многоугольника строго по часовой стрелке!!!
        {
            if (vertex.Length < 3) throw new Exception("Polygon must have over 2 points");

            if (LineSide(vertex.Last(), vertex.First(), point) >= 0) 
                return false;

            for (int i = 1; i < vertex.Length; i++)
                if (LineSide(vertex[i - 1], vertex[i], point) < 0) 
                    return false;

            return true;
        }

        static public bool IntersectionOfLineAndLineSigment(Vector2D line1, Vector2D line2, Vector2D lineSigment1, Vector2D lineSigment2) //пресекаются ли прямая (line1, line2) и отрезок (lineSigment1, lineSigment2)
        {
            float t = (line1 - line2) % (lineSigment1 - lineSigment2);
            if (t == 0) 
                return false;

            Vector2D P = new Vector2D(line1 % line2 * (lineSigment1.X - lineSigment2.X) - lineSigment1 % lineSigment2 * (line1.X - line2.X), line1 % line2 * (lineSigment1.Y - lineSigment2.Y) - lineSigment1 % lineSigment2 * (line1.Y - line2.Y));
            const float esp = 0.0001f;//не помню зачем это тут, написано кровью
            P /= t;
            if (((P.X <= lineSigment1.X + esp && P.X >= lineSigment2.X - esp) || 
                (P.X >= lineSigment1.X - esp && P.X <= lineSigment2.X + esp)) 
                && 
                ((P.Y <= lineSigment1.Y + esp && P.Y >= lineSigment2.Y - esp) || 
                (P.Y >= lineSigment1.Y - esp && P.Y <= lineSigment2.Y + esp)))
                return true;
            return false;
        }
        static public float LinePointDistance(Vector2D a, Vector2D b, Vector2D point) // расстояние от точки point до прямой (a,b)
        {
            return Math.Abs(point % (b - a) + b % a) / Distance(a, b);
        }
    }
}
