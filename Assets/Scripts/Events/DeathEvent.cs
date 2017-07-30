using UnityEngine;
using System.Collections;

public class DeathEvent : MiniEvent
{
    protected override void CompleteEvent()
    {
        var robot = GetComponentInParent<Robot>();
        Destroy(robot.gameObject);

        if (nextEvent)
        {
            nextEvent.active = true;
        }
        else
        {
            GameManager.Instance.SetState(GameState.Idle);
        }
    }

    protected override void LateUpdate()
    {
        if (CanUpdate)
            CompleteEvent();
    }
}
