using UnityEngine;
using System.Collections;

public class EventNotification : MonoBehaviour
{
    private static Vector3 positionToRobot = new Vector3(0, 40, 0);

    public Robot robot;
    private Transform backTransform, frontTransform;
    private Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (!animator)
            animator = GetComponent<Animator>();
        if (!robot)
        {
            gameObject.SetActive(false);
            return;
        }
        if (!GameManager.Instance.Idle)
        {
            animator.speed = 0.0f;
            return;
        }
        animator.speed = 1.0f;

        if (GameManager.Instance.robotWindow.gameObject.activeSelf && GameManager.Instance.robotWindow.robot == robot)
        {
            transform.SetParent(frontTransform, true);
            transform.position = GameManager.Instance.robotWindow.TalkIconPosition;
            return;
        }
        transform.SetParent(backTransform, true);
        transform.position = Camera.main.WorldToScreenPoint(robot.modelTransform.position) + positionToRobot;
    }

    public void SetBackTransform(Transform back)
    {
        backTransform = back;
        frontTransform = transform.parent;
    }
}
