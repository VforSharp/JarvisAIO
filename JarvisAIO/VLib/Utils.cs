using System;
using SharpDX;

namespace JarvisAIO.VLib
{
    public class Utils
    {
        public static Object[] VectorPointProjectionOnLineSegment(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            float cx = v3.X;
            float cy = v3.Y;
            float ax = v1.X;
            float ay = v1.Y;
            float bx = v2.X;
            float by = v2.Y;
            float rL = ((cx - ax) * (bx - ax) + (cy - ay) * (by - ay)) / ((float)Math.Pow(bx - ax, 2) + (float)Math.Pow(by - ay, 2));
            Vector3 pointLine = new Vector3(ax + rL * (bx - ax), ay + rL * (by - ay), 0);
            float rS;
            if (rL < 0)
            {
                rS = 0;
            }
            else if (rL > 1)
            {
                rS = 1;
            }
            else
            {
                rS = rL;
            }
            bool isOnSegment;
            if (rS.CompareTo(rL) == 0)
            {
                isOnSegment = true;
            }
            else
            {
                isOnSegment = false;
            }
            Vector3 pointSegment = new Vector3();
            if (isOnSegment)
            {
                pointSegment = pointLine;
            }
            else
            {
                pointSegment = new Vector3(ax + rS * (bx - ax), ay + rS * (by - ay), 0);
            }
            return new object[3] { pointSegment, pointLine, isOnSegment };
        }

        public static bool IsValidVector3(Vector3 vector)
        {
            if (vector.X.CompareTo(0.0f) == 0 && vector.Y.CompareTo(0.0f) == 0 && vector.Z.CompareTo(0.0f) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static double GetDistanceSqr(Vector3 distance1, Vector3 distance2)
        {
            float area1 = distance1.Y;
            float area2 = distance2.Y;
            double distance = Math.Pow((distance1.X - distance2.X), 2) +
                              Math.Pow(((area1) - (area2)), 2);
            return distance;
        }

        public static double GetDistance(Vector3 distance1, Vector3 distance2)
        {
            double distance = GetDistanceSqr(distance1, distance2);
            distance = Math.Sqrt(distance);
            return distance;
        }



    }
}
