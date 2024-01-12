# Team Description
**ID:** 

  - Grup C

**Names:**

  - Victor Romero

<img src="/Images/VictorImg.jpg" style=" width:160px ; height:200px "  >

  
  - Eric Lopez
<img src="/Images/EricImg.jpg" style=" width:160px ; height:200px "  >



**Emails:**

  - victor.romero.2@enti.cat
  
  - eric.lopez@enti.cat


**Explanation Exercise 2:**

The most efficent way to do the interpolation in this exercise was to perform a simple lerp for all the joints separately.
This can be performed since all the rotations only need to be executed in one axis alone. 
This is also made to avoid breaking the robot constraints since it would start rotating on axis we don't want it to rotate to.

In addition, we implemented the Slerp which uses the quaternions directly, but as we've seen this was less efficient since it needs the whole quaternion as an argument in our case, 
therefore it is more expensive than just rotate in one axis each joint.


**Exercise Location**

The order of each location is represented in this order: Object in scene (if existent), Script & Function

Exercise 1:
 1. IK_tentacles, NotifyShoot()
 2. BlueTarget, MovingTarget, Update()
 3. Scorpion, IK_Scorpion, Update()
 4. Main Camera, Reset, Update()

Exercise 2:
 1. Scorpion, IK_Scorpion, Update()
 2. Ball, MovingBall, OnCollisionEnter()
 3. Ball, MovingBall, CalculateTrajectory() & Update()
 4. Ball, MovingBall, Update()


Exercise 3:
 1. Scorpion, IK_Scorpion, UpdateFutureLegBases()
 2. Located inside Obstacles Object in Unity Editor
 3. MyScorpionController (DLL) -> LerpLegs()
 4. Scorpion, IK_Scorpion, UpdateBodyPosition()
 5. Scorpion, IK_Scorpion, UpdateBodyRotation()


Exercise 4:
 1. MyScorpionController, updateTail() & TargetApproach(Vector3 target)
 2. MyScorpionController, TargetApproach(Vector3 target) & NewError(int i)


Exercise 5:
 1. MyOctopusController, update_ccd()
 2. OctopusBlueTeam, NotifyShoot()
 3. TargetFollow, TargetFollow, InitFollowTarget(Transform newTarget)
