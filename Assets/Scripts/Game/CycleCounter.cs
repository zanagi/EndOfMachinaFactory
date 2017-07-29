using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CycleCounter : MonoBehaviour {

    [SerializeField]
    private Image cycleMeter;

    [SerializeField]
    private Text cycleCountText;
    
    [SerializeField]
    private float cycleLength;

    [SerializeField]
    private int cycleSpeed = 1;

    private float currentCycleTime;
    private int cycleCount;
    
	// Update is called once per frame
	void FixedUpdate () {
        currentCycleTime += Time.fixedDeltaTime * cycleSpeed;
        
        if(currentCycleTime >= cycleLength)
        {
            currentCycleTime -= cycleLength;
            cycleCount += 1;
            cycleCountText.text = cycleCount.ToString();
        }
        cycleMeter.fillAmount = currentCycleTime / cycleLength;
	}
}
