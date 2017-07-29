using UnityEngine;
using System.Collections.Generic;

public class Factory : MonoBehaviour
{
    [SerializeField]
    private GameObject machineContainer;
    private Machine[] machines;
    private float powerConsumption;

    // Use this for initialization
    void Start()
    {
        machines = machineContainer.GetComponentsInChildren<Machine>();

        foreach (var machine in machines)
            powerConsumption += machine.Consumption;
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.battery.UpdatePower(powerConsumption);
    }
}
