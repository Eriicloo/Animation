using System;

namespace RobotController
{
    public static class Utility 
    {
        public static float Deg2Rad = (float)(Math.PI / 180);
        public static float Rad2Deg = (float)(180 / Math.PI);
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
            float length = (float)Math.Sqrt(x * x + y * y + z * z + w * w);
            x /= length;
            y /= length;
            z /= length;
            w /= length;
            return this;
        }

        public string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ", " + w + ")";
        }


        public static MyQuat Conjugate(MyQuat q)
        {
            return new MyQuat(q.w, -q.x, -q.y, -q.z);
        }

        public static MyQuat FromAxisAngle(MyVec axis, float angle)
        {
            float halfAngleOfRotation = (angle / 2) * Utility.Deg2Rad;
            float sinRotAngle = (float)Math.Sin(halfAngleOfRotation);

            float w = (float)Math.Cos(angle/2);
            float x = axis.x * sinRotAngle;
            float y = axis.y * sinRotAngle;
            float z = axis.z * sinRotAngle;

            return new MyQuat(x, y, z, w).Normalize();
        }


    }



    public struct MyVec
    {

        public float x;
        public float y;
        public float z;
    }
}