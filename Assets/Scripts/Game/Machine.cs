using UnityEngine;
using System.Collections;

public class Machine : MonoBehaviour
{
    protected static int requiredResourceProgress = 100;

    public bool On { get; private set; }

    [SerializeField]
    protected Resource resource;
    protected int resourceProgress;

    [SerializeField]
    private float powerConsumption = 10.0f;
    public float PowerConsumption { get { return powerConsumption; } }
    
    public virtual void AddProgress(int amount)
    {
        if (amount <= 0)
            return;

        resourceProgress += amount;

        if(resourceProgress >= requiredResourceProgress)
        {
            resourceProgress -= requiredResourceProgress;
            GameManager.Instance.resourceContainer.AddResource(resource);
        }
    }
}
