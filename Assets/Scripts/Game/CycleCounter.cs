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

    private float currentCycleTime;
    private int cycleCount;
    
	// Update is called once per frame
	void FixedUpdate () {
        if (!GameManager.Instance.Idle)
            return;

        currentCycleTime += Time.fixedDeltaTime * GameManager.Instance.gameSpeed;
        
        if(currentCycleTime >= cycleLength)
        {
            currentCycleTime -= cycleLength;
            cycleCount += 1;
            cycleCountText.text = cycleCount.ToString();
        }
        cycleMeter.fillAmount = currentCycleTime / cycleLength;
	}
}
