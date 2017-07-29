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

    // Update is called once per frame
    void Update()
    {

    }
}
