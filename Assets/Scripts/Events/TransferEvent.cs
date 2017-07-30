using UnityEngine;
using System.Collections;

public class TransferEvent : MiniEvent
{
    protected override void CompleteEvent()
    {
        var robot = GetComponentInParent<Robot>();
        robot.Transfer();
        base.CompleteEvent();
    }

    protected override void LateUpdate()
    {
        if (CanUpdate)
            CompleteEvent();
    }
}
