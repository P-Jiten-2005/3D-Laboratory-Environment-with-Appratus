using UnityEngine;

public class BeakerPouring : MonoBehaviour
{
    [Header("References")]
    public BeakerLiquid source;
    public LiquidTransfer transfer;
    public LineRenderer waterStream;

    [Header("Pour Settings")]
    public float pourAngle = 60f;

    void Update()
    {
        // Safety checks
        if (source == null)
        {
            Debug.LogError("Source BeakerLiquid not assigned!");
            return;
        }

        if (transfer == null)
        {
            Debug.LogError("LiquidTransfer not assigned!");
            return;
        }

        if (waterStream == null)
        {
            Debug.LogError("WaterStream LineRenderer not assigned!");
            return;
        }

        // Stop if source is empty
        if (source.fillAmount <= 0f)
        {
            waterStream.enabled = false;
            transfer.isPouring = false;
            return;
        }

        // Calculate tilt angle
        float angle = Vector3.Angle(transform.up, Vector3.up);

        // Debug
        Debug.Log("Current Angle = " + angle);

        // Check if flask is tilted enough
        if (angle > pourAngle)
        {
            waterStream.enabled = true;
            transfer.isPouring = true;

            Debug.Log("POURING");
        }
        else
        {
            waterStream.enabled = false;
            transfer.isPouring = false;
        }
    }
}