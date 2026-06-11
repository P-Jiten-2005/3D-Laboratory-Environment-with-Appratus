using UnityEngine;

public class BeakerPouring : MonoBehaviour
{
    [Header("Liquid References")]
    public BeakerLiquid source;
    public LiquidTransfer transfer;

    [Header("Visual Stream")]
    public GameObject waterStream;

    [Header("Pour Settings")]
    public float pourAngle = 60f;

    void Update()
    {
        // Stop pouring if source is empty
        if (source.fillAmount <= 0f)
        {
            if (waterStream.activeSelf)
                waterStream.SetActive(false);

            transfer.isPouring = false;
            return;
        }

        // Calculate tilt angle
        float angle = Vector3.Angle(transform.up, Vector3.up);

        // Start pouring
        if (angle > pourAngle)
        {
            if (!waterStream.activeSelf)
                waterStream.SetActive(true);

            transfer.isPouring = true;
        }
        // Stop pouring
        else
        {
            if (waterStream.activeSelf)
                waterStream.SetActive(false);

            transfer.isPouring = false;
        }
    }
}