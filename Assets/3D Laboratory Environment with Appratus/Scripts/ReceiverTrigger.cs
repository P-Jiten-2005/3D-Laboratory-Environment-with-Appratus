using UnityEngine;

public class ReceiverTrigger : MonoBehaviour
{
    public LiquidTransfer transfer;

    void OnParticleTrigger()
    {
        transfer.isPouring = true;
    }

    void OnTriggerExit(Collider other)
    {
        transfer.isPouring = false;
    }
}