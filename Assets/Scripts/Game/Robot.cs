using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour
{
    private static float sleepModifier = 0.2f;

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
    public bool IsDead
    {
        get { return currentPower <= 0; }
    }
    private bool sleeping;
    public bool IsSleeping
    {
        get { return sleeping; }
    }

    // VN
    [SerializeField]
    private VNTextEvent idleEvent;

    // Animator
    private Animator animator;

    private void Start()
    {
        currentPower = maxPower;
        animator = GetComponentInParent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        if (!GameManager.Instance.Idle || IsDead)
            return;

        if (sleeping)
        {
            currentPower -= consumption * sleepModifier;
        }
        else
        {
            currentPower -= consumption;
            machine.AddProgress(Random.Range(0.5f * proficiency, 1.5f * proficiency));
        }

        // Check if out of power
        if (currentPower <= 0)
        {
            currentPower = 0;

            if(!sleeping)
            {
                animator.Play("Fall");
            }
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
