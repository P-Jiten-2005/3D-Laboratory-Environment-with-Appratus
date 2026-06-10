using UnityEngine;

public class BeakerPouring : MonoBehaviour
{
    public ParticleSystem waterParticles;

    public LiquidTransfer transfer;

    public BeakerLiquid source;

    public float pourAngle = 60f;

    void Update()
    {
        // Stop everything if empty
        if (source.fillAmount <= 0f)
        {
            if (waterParticles.isPlaying)
                waterParticles.Stop();

            transfer.isPouring = false;
            return;
        }

        float angle = Vector3.Angle(transform.up, Vector3.up);

        if (angle > pourAngle)
        {
            if (!waterParticles.isPlaying)
                waterParticles.Play();

            transfer.isPouring = true;
        }
        else
        {
            if (waterParticles.isPlaying)
                waterParticles.Stop();

            transfer.isPouring = false;
        }
    }
}