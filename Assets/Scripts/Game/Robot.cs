using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour
{
    [SerializeField]
    private Machine machine;
    [SerializeField]
    private int proficiency = 1;
    [SerializeField]
    private float consumption;
    [SerializeField]
    private float maxPower;
    private float currentPower;

    private void Start()
    {
        currentPower = maxPower;
    }

    protected virtual void FixedUpdate()
    {
        if (!GameManager.Instance.Idle)
            return;

        machine.AddProgress(proficiency);
        currentPower -= consumption;

        if (currentPower <= 0)
        {
            currentPower = 0;

            // TODO: BB event
        }
    }

    public void SetProficiency(int proficiency)
    {
        this.proficiency = proficiency;
    }

    public void SetPowerConsumption(int consumption)
    {
        this.consumption = consumption;
    }
}
