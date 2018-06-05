using UnityEngine;

public static class Helper
{
  
    public static float ClampAngle(float angle, float min, float max)
    {
        //Keeps camera from making full rotations
        do
        {
            if (angle < -360)
            {
                angle += 360;
            }
            if (angle > 360)
            {
                angle -= 360;
            }
        } while (angle < -360 || angle > 360);

        return Mathf.Clamp(angle, min, max);
    }

}