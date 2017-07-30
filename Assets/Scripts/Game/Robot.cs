using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour
{
    public Transform modelTransform;

    [SerializeField]
    private Machine machine;
    [SerializeField]
    private int proficiency = 1;
    [SerializeField]
    private float consumption = 1.0f;
    [SerializeField]
    private float maxPower = 1000.0f;
    private float currentPower;
    
    public float PowerRatio
    {
        get { return currentPower / maxPower; }
    }

    // VN
    [SerializeField]
    private VNTextEvent idleEvent;

    private void Start()
    {
        currentPower = maxPower;
    }

    protected virtual void FixedUpdate()
    {
        if (!GameManager.Instance.Idle)
            return;

        machine.AddProgress(Random.Range(0.5f * proficiency, 1.5f * proficiency));
        currentPower -= consumption;

        if (currentPower <= 0)
        {
            currentPower = 0;
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

    public void ActivateDialogue()
    {
        if (!idleEvent)
            return;

        Instantiate(idleEvent, transform);
        GameManager.Instance.SetState(GameState.Event);
    }
}
