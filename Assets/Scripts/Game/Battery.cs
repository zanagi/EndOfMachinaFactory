using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Battery : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private float maxPower;
    private float currentPower, powerConsumption = 1.0f;

    private void Start()
    {
        currentPower = maxPower;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance.State != GameState.Idle)
            return;

        currentPower -= powerConsumption * GameManager.Instance.gameSpeed;
        fillImage.fillAmount = currentPower / maxPower;
    }
}
