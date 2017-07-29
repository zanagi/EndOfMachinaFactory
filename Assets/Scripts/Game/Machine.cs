using UnityEngine;
using System.Collections;

public class Machine : MonoBehaviour
{
    protected static int requiredResourceProgress = 500;

    public bool On { get; private set; }

    [SerializeField]
    protected Resource resource;
    protected float resourceProgress;
    
    [SerializeField]
    protected SlideArea slideArea;

    public virtual void AddProgress(float amount)
    {
        if (amount <= 0)
            return;

        resourceProgress += amount;

        if(resourceProgress >= requiredResourceProgress)
        {
            resourceProgress -= requiredResourceProgress;
            GameManager.Instance.resourceContainer.AddResource(resource, slideArea);
        }
    }
}
