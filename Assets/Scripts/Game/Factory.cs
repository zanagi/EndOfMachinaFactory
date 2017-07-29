using UnityEngine;
using System.Collections.Generic;

public class Factory : MonoBehaviour
{
    [SerializeField]
    private GameObject machineContainer;
    private Machine[] machines;

    [SerializeField]
    private float powerConsumption; // Power consumption of factory itself set in editor
    
    // Use this for initialization
    private void Start()
    {
        machines = machineContainer.GetComponentsInChildren<Machine>();

        foreach (var machine in machines)
            powerConsumption += machine.PowerConsumption;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.Instance.Idle)
            return;

        GameManager.Instance.battery.UpdatePower(powerConsumption);
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.Idle)
            return;

        if(InputHandler.Instance.Clicked)
        {
            // Raycast to see if machine/robot clicked
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(InputHandler.Instance.TouchPosition), out hit, 100.0f))
            {
                Robot robot = hit.transform.GetComponent<Robot>();
                if(robot)
                {
                    Debug.Log("Clicked: " + robot.name);
                }
            }
        }
    }

    public void TurnOffMachine(Machine machine)
    {

    }
}
