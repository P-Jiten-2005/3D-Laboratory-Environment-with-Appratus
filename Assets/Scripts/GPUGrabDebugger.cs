using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GPUGrabDebugger : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private BoxCollider col;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();

        if (grabInteractable != null)
        {
            grabInteractable.hoverEntered.AddListener(OnHoverEntered);
            grabInteractable.hoverExited.AddListener(OnHoverExited);
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
            grabInteractable.selectExited.AddListener(OnSelectExited);
            
            // Fix: Explicitly assign colliders
            if (grabInteractable.colliders.Count == 0 && col != null)
            {
                grabInteractable.colliders.Add(col);
                Debug.Log("[GPUGrabDebugger] Explicitly added BoxCollider to XRGrabInteractable.");
            }
        }

        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.isKinematic = false;
            rb.useGravity = true;
            Debug.Log("[GPUGrabDebugger] Rigidbody settings verified.");
        }
        
        if (col != null)
        {
            col.isTrigger = false;
            Debug.Log("[GPUGrabDebugger] BoxCollider verified (isTrigger = false).");
        }
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log($"[GPUGrabDebugger] Hover Entered by: {args.interactorObject.transform.name}");
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        Debug.Log($"[GPUGrabDebugger] Hover Exited by: {args.interactorObject.transform.name}");
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log($"[GPUGrabDebugger] Select Entered by: {args.interactorObject.transform.name}");
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log($"[GPUGrabDebugger] Select Exited by: {args.interactorObject.transform.name}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"[GPUGrabDebugger] Collision detected with: {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[GPUGrabDebugger] Trigger detected with: {other.gameObject.name}");
    }
}
