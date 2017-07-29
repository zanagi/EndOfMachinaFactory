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
    
    public void UpdatePower(float consumption)
    {
        currentPower -= consumption * GameManager.Instance.gameSpeed;
        fillImage.fillAmount = currentPower / maxPower;

        if(currentPower <= 0)
        {
            GameManager.Instance.End();
        }
    }
}
