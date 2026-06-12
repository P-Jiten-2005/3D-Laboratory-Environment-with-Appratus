using UnityEngine;

public class LiquidTransfer : MonoBehaviour
{
    [Header("Source Flask")]
    public BeakerLiquid source;

    [Header("Target Flask")]
    public BeakerLiquid target;

    [Header("Transfer Settings")]
    public float transferRate = 0.2f;

    [HideInInspector]
    public bool isPouring = false;

    void Update()
    {
        if (!isPouring)
            return;

        if (source == null || target == null)
        {
            Debug.LogError("Source or Target not assigned!");
            return;
        }

        float amount = transferRate * Time.deltaTime;

        source.fillAmount -= amount;
        target.fillAmount += amount;

        source.fillAmount = Mathf.Clamp01(source.fillAmount);
        target.fillAmount = Mathf.Clamp01(target.fillAmount);

        Debug.Log(
            $"Source={source.fillAmount:F2} Target={target.fillAmount:F2}"
        );

        if (source.fillAmount <= 0f)
        {
            source.fillAmount = 0f;
            isPouring = false;
        }

        if (target.fillAmount >= 1f)
        {
            target.fillAmount = 1f;
            isPouring = false;
        }
    }
}