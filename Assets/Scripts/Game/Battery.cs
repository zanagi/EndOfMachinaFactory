using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Battery : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private float maxPower;
    private float currentPower;

    private void Start()
    {
        currentPower = maxPower;
    }
    
    public void ReducePower(float consumption)
    {
        currentPower -= consumption * GameManager.Instance.gameSpeed;
        UpdateFill();

        if (currentPower <= 0)
        {
            GameManager.Instance.End();
        }
    }

    public void AddPower(float amount)
    {
        if (amount <= 0)
            return;
        currentPower = Mathf.Min(maxPower, currentPower + amount);
        UpdateFill();
    }

    private void UpdateFill()
    {
        fillImage.fillAmount = currentPower / maxPower;
    }
}
