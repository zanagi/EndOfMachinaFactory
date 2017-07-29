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

    public void AddResource(Resource resource, SlideArea slideArea)
    {
        foreach(var counter in counters)
        {
            if(counter.resource == resource)
            {
                counter.ChangeResourceCount(1);

                if(resource.IsBasic())
                    GenerateResourceObject(resource, slideArea);
                return;
            }
        }
    }

    private void GenerateResourceObject(Resource resource, SlideArea slideArea)
    {
        var resourcePrefab = (resource == Resource.X) ? ContentManager.Instance.xResourceObject 
                                : (resource == Resource.Y) ? ContentManager.Instance.yResourceObject : ContentManager.Instance.zResourceObject;
        var temp = Instantiate(resourcePrefab, slideArea.transform);
        temp.transform.position = slideArea.start.position;
        temp.slideArea = slideArea;
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
