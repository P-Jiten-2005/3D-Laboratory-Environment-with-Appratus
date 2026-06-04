using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class AssemblyPart : MonoBehaviour
{
    [SerializeField] private string partId = "Part";
    [SerializeField] [TextArea] private string description = "Part description...";
    [SerializeField] private bool lockAfterPlacement = true;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private Transform initialParent;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool initialKinematic;
    private bool initialUseGravity;

    public event Action<AssemblyPart> Released;
    public event Action<AssemblyPart> HoverEntered;
    public event Action<AssemblyPart> HoverExited;
    public event Action<AssemblyPart> Grabbed;

    public string PartId => partId;
    public string Description => description;
    public bool LockAfterPlacement => lockAfterPlacement;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        initialParent = transform.parent;
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        if (rb != null)
        {
            initialKinematic = rb.isKinematic;
            initialUseGravity = rb.useGravity;
        }
    }

    private void OnEnable()
    {
        grabInteractable.hoverEntered.AddListener(OnHoverEntered);
        grabInteractable.hoverExited.AddListener(OnHoverExited);
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        grabInteractable.hoverEntered.RemoveListener(OnHoverEntered);
        grabInteractable.hoverExited.RemoveListener(OnHoverExited);
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    public void SetInteractable(bool isEnabled)
    {
        grabInteractable.enabled = isEnabled;
    }

    public void SnapTo(Transform target)
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        transform.SetPositionAndRotation(target.position, target.rotation);
        transform.SetParent(target, true);
    }

    public void LockPlacement()
    {
        if (lockAfterPlacement)
        {
            grabInteractable.enabled = false;
        }
    }

    public void ResetPartState()
    {
        grabInteractable.enabled = true;
        transform.SetParent(initialParent, true);
        transform.SetPositionAndRotation(initialPosition, initialRotation);

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = initialKinematic;
            rb.useGravity = initialUseGravity;
        }
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        HoverEntered?.Invoke(this);
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        HoverExited?.Invoke(this);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Grabbed?.Invoke(this);
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        Released?.Invoke(this);
    }
}

