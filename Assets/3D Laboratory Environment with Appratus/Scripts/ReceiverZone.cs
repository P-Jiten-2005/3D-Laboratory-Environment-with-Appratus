using UnityEngine;

public class ReceiverZone : MonoBehaviour
{
    public BeakerLiquid parentLiquid;

    private void OnTriggerEnter(Collider other)
    {
        BeakerLiquid incoming = other.GetComponent<BeakerLiquid>();

        if (incoming == null) return;

        ReactionManager.Instance.RegisterContact(parentLiquid, incoming);
    }
}