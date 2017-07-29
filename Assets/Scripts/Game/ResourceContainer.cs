using UnityEngine;
using System.Collections;

public class ResourceContainer : MonoBehaviour
{
    private ResourceCounter[] counters;

    // Use this for initialization
    void Start()
    {
        counters = GetComponentsInChildren<ResourceCounter>();
    }

    public void AddResource(Resource resource)
    {
        foreach(var counter in counters)
        {
            if(counter.resource == resource)
            {
                counter.ChangeResourceCount(1);
                return;
            }
        }
    }

    public bool HasBasicResources(int count)
    {
        foreach (var counter in counters)
        {
            if (counter.resource.IsBasic() && counter.ResourceCount < count)
                return false;
        }
        return true;
    }

    public void TakeBasicResources(int count)
    {
        foreach (var counter in counters)
        {
            if (counter.resource.IsBasic())
                counter.ChangeResourceCount(-count);
        }
    }
}
