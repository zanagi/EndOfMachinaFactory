using UnityEngine;
using System.Collections;

public class Machine : MonoBehaviour
{
    public bool On { get; private set; }

    [SerializeField]
    private float powerConsumption = 10.0f;
    public float PowerConsumption { get { return powerConsumption; } }
    
}
