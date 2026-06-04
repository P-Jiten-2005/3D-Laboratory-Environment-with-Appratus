using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Attach this to every XRSocketInteractor in the lab.
// Set 'requiredTag' in the Inspector to control what snaps here.
public class SocketValidator : MonoBehaviour
{
    // Set this in the Inspector � e.g. "TestTube" or "PHPaper"
    public string requiredTag;

    void Start()
    {
        // 'selectEntered' fires when something snaps into this socket
        var socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
        socket.selectEntered.AddListener(OnItemSnapped);
    }

    void OnItemSnapped(SelectEnterEventArgs args)
    {
        // Get the LabItemData from the object that just snapped in
        var item = args.interactableObject.transform
                       .GetComponent<LabItemData>();

        // If no LabItemData, or wrong tag � ignore
        if (item == null) return;
        if (!args.interactableObject.transform.CompareTag(requiredTag)) return;

        // Tell the experiment controller something was socketed
        GetComponentInParent<ExperimentController>()
            ?.OnItemSocketed(item, requiredTag);
    }
}

