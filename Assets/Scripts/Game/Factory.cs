using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Factory : MonoBehaviour
{
    [SerializeField]
    private float powerConsumption; // Power consumption of factory itself set in editor
    
    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.Instance.Idle)
            return;

        GameManager.Instance.battery.ReducePower(powerConsumption);
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.Idle)
            return;

        if(InputHandler.Instance.Clicked && !EventSystem.current.IsPointerOverGameObject())
        {
            // Raycast to see if machine/robot clicked
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(InputHandler.Instance.TouchPosition), out hit, 100.0f))
            {
                Robot robot = hit.transform.GetComponent<Robot>();
                if(robot)
                {
                    GameManager.Instance.robotWindow.SetRobot(robot);
                    return;
                }
                ClickEventTrigger cet = hit.transform.GetComponent<ClickEventTrigger>();
                if(cet)
                {
                    cet.TriggerEvent();
                    return;
                }
            }
        }
    }
}
