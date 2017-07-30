using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CycleCounter : MonoBehaviour {

    private static readonly int basicValue = 10, xyzValue = 150, cycleLength = 300;

    [SerializeField]
    private Image cycleMeter;
    [SerializeField]
    private Text cycleCountText;

    private float currentValue;
    private int cycleCount;
    
	// Update is called once per frame
	private void FixedUpdate () {
        if (!GameManager.Instance.Idle)
            return;

        // currentValue += 1;
        CheckCycleOverflow();
        cycleMeter.fillAmount = currentValue / cycleLength;
	}

    public void AddValue(bool isBasicResource)
    {
        currentValue += isBasicResource ? basicValue : xyzValue;
    }

    private void CheckCycleOverflow()
    {
        if (currentValue >= cycleLength)
        {
            currentValue -= cycleLength;
            cycleCount += 1;
            cycleCountText.text = cycleCount.ToString();
        }
    }
}
