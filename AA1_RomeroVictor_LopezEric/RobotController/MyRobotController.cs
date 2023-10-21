using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotController
{
    public class MyRobotController
    {

        #region public methods



        public string Hi()
        {

            string s = "Victor Romero, Eric Lopez";
            return s;

        }


        //EX1: this function will place the robot in the initial position

        public void PutRobotStraight(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {
            if (_currentExercise != Exercise.EX_1)
            {
                _currentExercise = Exercise.EX_1;
            }

            rot0 = Rotate(MyQuat.NullQ, MyVec.up, initialAngles[0]);
            rot1 = Rotate(rot0, MyVec.right, initialAngles[1]);
            rot2 = Rotate(rot1, MyVec.right, initialAngles[2]);
            rot3 = Rotate(rot2, MyVec.right, initialAngles[3]);
        }



        //EX2: this function will interpolate the rotations necessary to move the arm of the robot until its end effector collides with the target (called Stud_target)
        //it will return true until it has reached its destination. The main project is set up in such a way that when the function returns false, the object will be droped and fall following gravity.


        public bool PickStudAnim(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {

            if (_currentExercise != Exercise.EX_2)
            {
                _currentExercise = Exercise.EX_2;
                TimeReset();
            }

            //todo: add a check for your condition
            bool myCondition = time < 1.0f;



            if (myCondition)
            {
                rot0 = Rotate(MyQuat.NullQ, MyVec.up, MyQuat.Lerp(initialAngles[0], finalAngles[0], time));
                rot1 = Rotate(rot0, MyVec.right, MyQuat.Lerp(initialAngles[1], finalAngles[1], time));
                rot2 = Rotate(rot1, MyVec.right, MyQuat.Lerp(initialAngles[2], finalAngles[2], time));
                rot3 = Rotate(rot2, MyVec.right, MyQuat.Lerp(initialAngles[3], finalAngles[3], time));

                time = Utility.Clamp(robotSpeed + time, 0.0f, 1.0f);

                return true;
            }


            rot0 = Rotate(MyQuat.NullQ, MyVec.up, finalAngles[0]);
            rot1 = Rotate(rot0, MyVec.right, finalAngles[1]);
            rot2 = Rotate(rot1, MyVec.right, finalAngles[2]);
            rot3 = Rotate(rot2, MyVec.right, finalAngles[3]);

            return false;
        }


        //EX3: this function will calculate the rotations necessary to move the arm of the robot until its end effector collides with the target (called Stud_target)
        //it will return true until it has reached its destination. The main project is set up in such a way that when the function returns false, the object will be droped and fall following gravity.
        //the only difference wtih exercise 2 is that rot3 has a swing and a twist, where the swing will apply to joint3 and the twist to joint4

        public bool PickStudAnimVertical(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {

            if (_currentExercise != Exercise.EX_3)
            {
                _currentExercise = Exercise.EX_3;
                TimeReset();
            }

            //todo: add a check for your condition
            bool myCondition = time < 1.0f;



            if (myCondition)
            {
                rot0 = Rotate(MyQuat.NullQ, MyVec.up, MyQuat.Lerp(initialAngles[0], finalAngles[0], time));
                rot1 = Rotate(rot0, MyVec.right, MyQuat.Lerp(initialAngles[1], finalAngles[1], time));
                rot2 = Rotate(rot1, MyVec.right, MyQuat.Lerp(initialAngles[2], finalAngles[2], time));

                tempRot2 = rot2;
                localSwing = MyQuat.FromAxisAngle(MyVec.right, MyQuat.Lerp(initialAngles[3], finalAngles[3], time));
                rot3 = Rotate(localSwing, tempTwistAxis, MyQuat.Lerp(initTwistAngle, finalTwistAngle, time));

                time = Utility.Clamp(robotSpeed + time, 0.0f, 1.0f);

                return true;
            }


            rot0 = Rotate(MyQuat.NullQ, MyVec.up, finalAngles[0]);
            rot1 = Rotate(rot0, MyVec.right, finalAngles[1]);
            rot2 = Rotate(rot1, MyVec.right, finalAngles[2]);

            tempRot2 = rot2;
            localSwing = MyQuat.FromAxisAngle(MyVec.right, finalAngles[3]);
            rot3 = Rotate(localSwing, tempTwistAxis, finalTwistAngle);

            return false;
        }

        // We get the localSwing and we pass it to world space
        public static MyQuat GetSwing(MyQuat rot3)
        {
            return tempRot2 * (MyQuat.Conjugate(GetLocalTwist(rot3)) * rot3);
        }

        // rot3 contains localTwist + localSwing
        // Rotations with different rotation axis, so we can obtain the twist part
        public static MyQuat GetLocalTwist(MyQuat rot3)
        {
            return new MyQuat(rot3.w, tempTwistAxis.x * rot3.x, tempTwistAxis.y * rot3.y,
                tempTwistAxis.z * rot3.z);
        }


        // We get the localTwist and we pass it to world space
        public static MyQuat GetTwist(MyQuat rot3)
        {
            return GetSwing(rot3) * GetLocalTwist(rot3);
        }


        #endregion


        #region private and internal methods

        internal int TimeSinceMidnight { get { return (DateTime.Now.Hour * 3600000) + (DateTime.Now.Minute * 60000) + (DateTime.Now.Second * 1000) + DateTime.Now.Millisecond; } }

        internal MyQuat Multiply(MyQuat q1, MyQuat q2)
        {

            //The * operator is overloaded in MyQuat.cs for quats multiplication and quat with scalar
            return q1 * q2;
        }

        internal MyQuat Rotate(MyQuat currentRotation, MyVec axis, float angle)
        {
            return Multiply(currentRotation, MyQuat.FromAxisAngle(axis, angle));
        }

        //todo: add here all the functions needed

        enum Exercise { NONE, EX_1, EX_2, EX_3 };
        private Exercise _currentExercise = Exercise.NONE;

        //Ex1
        private float[] initialAngles = { 73f, 350f, 94f, 20f };

        //Ex2
        private float[] finalAngles = { 40f, 360f, 85f, 20f };

        private float time = 0.0f;
        private void TimeReset()
        {
            time = 0.0f;
        }

        private float robotSpeed = 0.003f;

        //Ex3
        private float initTwistAngle = 34f;
        private float finalTwistAngle = -60f;
        private MyQuat localSwing;
        private static MyVec tempTwistAxis = MyVec.up;

        //We need this variable as a temporal assign for the rotation2 of the joint 3 as we dont want
        //it to override the value and make the GetSwing with the stored value
        private static MyQuat tempRot2;

        #endregion


    }
}
