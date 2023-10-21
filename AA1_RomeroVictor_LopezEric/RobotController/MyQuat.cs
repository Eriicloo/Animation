using System;
using System.Timers;

namespace RobotController
{
    public static class Utility 
    {
        public static float Deg2Rad = (float)(Math.PI / 180);
        public static float Rad2Deg = (float)(180 / Math.PI);

        public static float Clamp(float value, float minValue, float maxValue)
        {
            return (value < minValue) ? minValue : (value > maxValue) ? maxValue : value;
        }
    }


    public struct MyQuat
    {

        public float w;
        public float x;
        public float y;
        public float z;

        public MyQuat(float w, float x, float y, float z)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
            Normalize();
        }

        public static MyQuat NullQ
        {
            get
            {
                MyQuat a;
                a.w = 1;
                a.x = 0;
                a.y = 0;
                a.z = 0;
                return a;

            }
        }

        public MyQuat Normalize()
        {
            float magnitude = (float)Math.Sqrt(w * w + x * x + y * y + z * z);
            w /= magnitude;
            x /= magnitude;
            y /= magnitude;
            z /= magnitude;
            return this;
        }

        public static MyQuat operator *(MyQuat q1, MyQuat q2)
        {
            float w = (q1.w * q2.w - q1.x * q2.x - q1.y * q2.y - q1.z * q2.z);
            float x = (q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y);
            float y = (q1.w * q2.y - q1.x * q2.z + q1.y * q2.w + q1.z * q2.x);
            float z = (q1.w * q2.z + q1.x * q2.y - q1.y * q2.x + q1.z * q2.w);

            return new MyQuat(w, x, y, z);
        }

        public static MyQuat operator *(float scalar, MyQuat q)
        {
            return new MyQuat(scalar * q.w, scalar * q.x, scalar * q.y, scalar * q.z);
        }

        public static MyQuat Add(MyQuat q1, MyQuat q2)
        {
            return new MyQuat(q1.w + q2.w, q1.x + q2.x, q1.y + q2.y, q1.z + q2.z);
        }

        public string ToString()
        {
            return "(" + w + ", " + x + ", " + y + ", " + z + ")";
        }


        public static MyQuat Conjugate(MyQuat q)
        {
            return new MyQuat(q.w, -q.x, -q.y, -q.z);
        }

        public static MyQuat FromAxisAngle(MyVec axis, float angle)
        {
            float halfAngleOfRotation = (angle / 2) * Utility.Deg2Rad;
            float sinRotAngle = (float)Math.Sin(halfAngleOfRotation);

            float w = (float)Math.Cos(halfAngleOfRotation);
            float x = axis.x * sinRotAngle;
            float y = axis.y * sinRotAngle;
            float z = axis.z * sinRotAngle;

            return new MyQuat(w, x, y, z);
        }

        public void ToAxisAngle(out MyVec axis, out float angle)
        {
            float v = (float)Math.Sqrt(1 - (w * w));
            axis = new MyVec(x / v, y / v, z / v);
            angle = 2 * (float)Math.Acos(w) * Utility.Rad2Deg;
        }

        public static MyQuat Lerp(MyQuat q1, MyQuat q2, float t)
        {
           return Add(((1 - t) * q1), (t * q2));
        }

        public static float Lerp(float begin, float end, float t)
        {
            // (1-t) p + t q
            return ((1 - t) * begin) + (t * end);

        }

        public static MyQuat Slerp(MyQuat q1, MyQuat q2, float t, float angle)
        {
            float f = 1.0f - 0.7878088f * (float)Math.Asin(angle);
            float k = 0.5069269f;
            f *= f;
            k *= f;
            float b = 2 * k;
            float c = -3 * k;
            float d = 1 + k;
            t = t * (b * t + c) + d;

            return Lerp(q1, q2, t);

        }

    }
}