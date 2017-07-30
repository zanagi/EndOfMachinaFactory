using UnityEngine;
using System.Collections;

public class RobotEventCheckXYZ1 : RobotEventChecker
{
    private void LateUpdate()
    {
        if (!GameManager.Instance.Idle)
            return;

        if(complete)
        {
            Destroy(gameObject);
            return;
        }

        complete = robot.SetNextEvent(eventPrefab);
    }
}
