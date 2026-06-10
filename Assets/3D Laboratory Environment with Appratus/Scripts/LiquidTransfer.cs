using UnityEngine;

public class LiquidTransfer : MonoBehaviour
{
    public BeakerLiquid source;
    public BeakerLiquid target;

    public float transferRate = 0.2f;

    public bool isPouring;

    void Update()
    {
        if(isPouring)
        {
            float amount = transferRate * Time.deltaTime;

            source.fillAmount -= amount;
            target.fillAmount += amount;

            source.fillAmount = Mathf.Clamp01(source.fillAmount);
            target.fillAmount = Mathf.Clamp01(target.fillAmount);

            // Stop if source is empty
            if(source.fillAmount <= 0)
            {
                source.fillAmount = 0;
                isPouring = false;
            }

            // Stop if target is full
            if(target.fillAmount >= 1)
            {
                target.fillAmount = 1;
                isPouring = false;
            }
        }
    }
}