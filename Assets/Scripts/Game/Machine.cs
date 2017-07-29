using UnityEngine;
using System.Collections;

public class Machine : MonoBehaviour
{
    public bool On { get; private set; }

    [SerializeField]
    private float consumption;
    public float Consumption { get { return On ? consumption : 0; } }
    
}
