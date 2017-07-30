using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RobotInfoWindow : MonoBehaviour
{
    private static Vector3 positionToRobot = new Vector3(0, 25, 0);

    [SerializeField]
    private Robot robot;

    [SerializeField]
    private Image batteryFill;
    
    private void Update()
    {
        UpdatePosition();
        UpdateBattery();
    }

    private void UpdatePosition()
    {
        transform.position = Camera.main.WorldToScreenPoint(robot.transform.position) + positionToRobot;
    }

    private void UpdateBattery()
    {
        batteryFill.fillAmount = robot.PowerRatio;
    }

    public void Close()
    {
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
}
