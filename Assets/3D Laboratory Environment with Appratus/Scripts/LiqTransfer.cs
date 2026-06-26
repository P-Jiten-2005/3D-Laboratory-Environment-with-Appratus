using UnityEngine;

public class LiqTransfer : MonoBehaviour
{
    [Header("Beakers")]
    public BeakLiquid source;
    public BeakLiquid target;

    [Header("Pour Detection")]
    public BeakPouring sourcePouring;

    [Header("Stream")]
    public WaterStream stream;

    [Header("Raycast")]
    public Transform pourPoint;
    public LayerMask receiverLayer;
    public float maxDistance = 0.5f;

    [Header("Transfer")]
    public float transferRate = 0.15f;

    private bool targetDetected;

    void Update()
    {
        DetectTarget();

        bool canPour =
            sourcePouring.IsPouring() &&
            targetDetected &&
            source.fillAmount > 0f &&
            target.fillAmount < 1f;

        if (canPour)
        {
            TransferLiquid();

            if (stream != null)
                stream.ShowStream();
        }
        else
        {
            if (stream != null)
                stream.HideStream();
        }
    }

    void DetectTarget()
    {
        RaycastHit hit;

        if (Physics.Raycast(
            pourPoint.position,
            pourPoint.forward,
            out hit,
            maxDistance,
            receiverLayer))
        {
            targetDetected = true;
            Debug.Log("Target Detected");
        }
        else
        {
            targetDetected = false;
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
        Debug.Log("Pouring");
    }

    private void OnDrawGizmos()
    {
        if (pourPoint == null)
            return;

        Gizmos.color = Color.blue;

        Gizmos.DrawRay(
            pourPoint.position,
            pourPoint.forward * maxDistance);
    }
}