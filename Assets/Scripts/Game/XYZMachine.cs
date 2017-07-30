using UnityEngine;
using System.Collections;

public class XYZMachine : Machine
{
    private static readonly int requiredResourceCount = 5;
    private bool resourcesTaken;

    public override void AddProgress(float amount)
    {
        if (amount <= 0)
            return;

        if(!resourcesTaken)
        {
            if(!GameManager.Instance.resourceContainer.HasBasicResources(requiredResourceCount))
            {
                return;
            }
            GameManager.Instance.resourceContainer.TakeBasicResources(requiredResourceCount);
            resourcesTaken = true;
        }
        resourceProgress += amount;

        if (resourceProgress >= requiredResourceProgress)
        {
            resourceProgress = 0; // Not a basic resource, so set progress to zero after completion
            GameManager.Instance.resourceContainer.AddResource(resource, slideArea);
            resourcesTaken = false;
        }
    }
}
