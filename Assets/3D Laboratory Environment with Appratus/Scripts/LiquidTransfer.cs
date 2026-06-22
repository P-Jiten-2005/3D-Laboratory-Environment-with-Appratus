using UnityEngine;

public class LiquidTransfer : MonoBehaviour
{
    [Header("References")]
    public BeakerLiquid source;
    public BeakerLiquid target;

    public BeakerPouring sourcePouring;

    public WaterStreamRenderer stream;

    [Header("Transfer")]
    public float transferRate = 0.15f;

    void Update()
    {
        bool pouring = sourcePouring.IsPouring();

        if (pouring &&
            source.fillAmount > 0 &&
            target.fillAmount < 1)
        {
            TransferLiquid();

            stream.ShowStream();
        }
        else
        {
            stream.HideStream();
        }
    }

    void TransferLiquid()
    {
        float amount = transferRate * Time.deltaTime;

        source.fillAmount -= amount;
        target.fillAmount += amount;

        source.fillAmount =
            Mathf.Clamp01(source.fillAmount);

        target.fillAmount =
            Mathf.Clamp01(target.fillAmount);
    }
}