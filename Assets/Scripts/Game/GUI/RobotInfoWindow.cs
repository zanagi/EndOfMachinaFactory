﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RobotInfoWindow : MonoBehaviour
{
    private static Vector3 positionToRobot = new Vector3(0, 25, 0);

    [SerializeField]
    private Robot robot;
    [SerializeField]
    private Image batteryFill;
    [SerializeField]
    private Button talkButton;
    [SerializeField]
    private Button sleepButton;
    [SerializeField]
    private Button transferButton;
    
    public void Update()
    {
        if (!robot)
        {
            gameObject.SetActive(false);
            return;
        }
        if (!GameManager.Instance.Idle)
            return;

        UpdatePosition();
        UpdateBattery();
        UpdateButtons();
    }

    private void UpdatePosition()
    {
        transform.position = Camera.main.WorldToScreenPoint(robot.modelTransform.position) + positionToRobot;
    }

    private void UpdateBattery()
    {
        batteryFill.fillAmount = robot.PowerRatio;
    }

    private void UpdateButtons()
    {
        talkButton.interactable = !robot.IsSleeping;
        sleepButton.interactable = !robot.IsDead;
        transferButton.interactable = !robot.IsDead && !robot.IsSleeping;
    }

    public void Close()
    {
        if (!GameManager.Instance.Idle)
            return;

        gameObject.SetActive(false);
    }

    public void SetRobot(Robot robot)
    {
        this.robot = robot;

        // Update all values after changing target robot
        Update();

        // Show window (if needed)
        gameObject.SetActive(true);
    }

    public void ActivateDialogue()
    {
        robot.ActivateDialogue();
    }

    public void SwapRobotSleep()
    {
        robot.SwapSleep();
    }

    public void Transfer()
    {
        robot.ActivateTransfer();
    }
}