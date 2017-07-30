using UnityEngine;
using System.Collections;

public class RobotEventChecker : MonoBehaviour
{
    protected bool complete;
    [SerializeField]
    protected GameObject eventPrefab;
    protected Robot robot;

    private void Start()
    {
        robot = GetComponentInParent<Robot>();
    }
}
