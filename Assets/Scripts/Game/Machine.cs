using UnityEngine;
using System.Collections;

public class Machine : MonoBehaviour
{
    private static int requiredResourceProgress = 1000;

    public bool On { get; private set; }

    [SerializeField]
    private Resource resource;
    private int resourceProgress;

    [SerializeField]
    private float powerConsumption = 10.0f;
    public float PowerConsumption { get { return powerConsumption; } }
    
    public void AddProgress(int amount)
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
