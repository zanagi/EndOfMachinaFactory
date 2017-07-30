using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNotificator : MonoBehaviour {

    [SerializeField]
    private Transform backTransform;
    [SerializeField]
    private EventNotification notificationPrefab;
    private List<EventNotification> notifications = new List<EventNotification>();

	public void InstantiateNotification(Robot robot)
    {
        foreach(var temp in notifications)
        {
            if (temp.robot == robot)
                return;
        }

        var notification = Instantiate(notificationPrefab, transform);
        notification.robot = robot;
        notification.SetBackTransform(backTransform);
        notifications.Add(notification);
    }

    public void RemoveNotification(Robot robot)
    {
        foreach (var temp in notifications)
        {
            if (temp.robot == robot)
            {
                Destroy(temp.gameObject);
                notifications.Remove(temp);
                return;
            }
        }
    }

    public void Clear()
    {
        Destroy(gameObject);
    }
}
