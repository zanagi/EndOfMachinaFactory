using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour
{
    private static float sleepModifier = 0.2f;
    private static string fallAnimName = "Fall", riseAnimName = "Rise", idleAnimName="Idle";

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

    // VN (prefabs)
    [SerializeField]
    private VNTextEvent idleEvent;
    [SerializeField]
    private GameObject transferEvent;
    [SerializeField]
    private GameObject deathEvent;

    // Animator
    private Animator animator;
    private bool animationPaused;

    // Particles
    [SerializeField]
    private GameObject sleepPS;
    [SerializeField]
    private GameObject transferPS;

    private void Start()
    {
        currentPower = maxPower;
        animator = GetComponentInParent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        if (!GameManager.Instance.Idle)
        {
            if(!animationPaused)
            {
                animationPaused = true;
                animator.speed = 0.0f;
            }
            StopParticles();
            return;
        }
        if(animationPaused)
        {
            animationPaused = false;
            animator.speed = 1.0f;
        }
        // Check if out of power
        if (IsDead)
        {
            HandleDeath();
            return;
        }

        if (sleeping)
        {
            currentPower -= consumption * sleepModifier;
        }
        else
        {
            currentPower -= consumption;
            machine.AddProgress(Random.Range(0.5f * proficiency, 1.5f * proficiency));
        }
        // Update particle system activity
        sleepPS.SetActive(sleeping);
        transferPS.SetActive(!sleeping);
    }

    private void StopParticles()
    {
        sleepPS.SetActive(false);
        transferPS.SetActive(false);
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

        if(IsDead)
            Instantiate(deathEvent, transform);
        else 
            Instantiate(idleEvent, transform);
        GameManager.Instance.SetState(GameState.Event);
    }

    public void SwapSleep()
    {
        sleeping = !sleeping;
        animator.Play(sleeping ? fallAnimName : riseAnimName);
    }

    public void ActivateTransfer()
    {
        if(!transferEvent)
        {
            Transfer();
            return;
        }
        Instantiate(transferEvent, transform);
    }

    public void Transfer()
    {
        GameManager.Instance.battery.AddPower(currentPower);
        currentPower = 0;
    }

    public void End()
    {
        animator.Play(fallAnimName);
    }

    private void HandleDeath()
    {
        currentPower = 0;

        if (!sleeping)
        {
            animator.Play(fallAnimName);
        }
        StopParticles();
    }
}
