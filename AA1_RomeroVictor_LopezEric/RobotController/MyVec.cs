using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotController
{
    public struct MyVec
    {

        public float x;
        public float y;
        public float z;

        public static MyVec right = new MyVec(1, 0, 0);
        public static MyVec up = new MyVec(0, 1, 0);
        public static MyVec forward = new MyVec(0, 0, 1);

        public MyVec(float x, float y, float z) 
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static MyVec operator +(MyVec v1, MyVec v2)
        {
            return new MyVec(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static MyVec operator *(float scalar, MyVec v)
        {
            return new MyVec(v.x * scalar, v.y * scalar, v.z * scalar);
        }

        public static MyVec operator *(MyVec v, MyQuat q)
        {
            MyQuat p = new MyQuat(v.x, v.y, v.z, 0f);
            p = q * p * MyQuat.Conjugate(q);
            return new MyVec(p.x, p.y, p.z);
        }

    }

}
