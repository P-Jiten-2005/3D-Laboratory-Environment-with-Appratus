using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections.Generic;

public class FloatingUIManager : MonoBehaviour
{
    public static FloatingUIManager Instance { get; private set; }

    [Header("Configuration")]
    public FloatingUIController uiPrefab;
    
    private FloatingUIController uiInstance;
    private XRInteractionManager interactionManager;
    private HashSet<IXRInteractable> subscribedInteractables = new HashSet<IXRInteractable>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (uiPrefab != null && uiInstance == null)
        {
            uiInstance = Instantiate(uiPrefab);
            uiInstance.transform.SetParent(transform);
        }
    }

    private void Start()
    {
        interactionManager = FindAnyObjectByType<XRInteractionManager>();
        if (interactionManager != null)
        {
            // Subscribe to future registrations
            interactionManager.interactableRegistered += OnInteractableRegistered;
            interactionManager.interactableUnregistered += OnInteractableUnregistered;

            // Handle already registered ones
            List<IXRInteractable> interactables = new List<IXRInteractable>();
            interactionManager.GetRegisteredInteractables(interactables);
            foreach (var interactable in interactables)
            {
                TrySubscribe(interactable);
            }
        }
    }

    private void OnInteractableRegistered(InteractableRegisteredEventArgs args)
    {
        TrySubscribe(args.interactableObject);
    }

    private void OnInteractableUnregistered(InteractableUnregisteredEventArgs args)
    {
        Unsubscribe(args.interactableObject);
    }

    private void TrySubscribe(IXRInteractable interactable)
    {
        if (interactable == null || subscribedInteractables.Contains(interactable)) return;

        if (interactable is MonoBehaviour mb)
        {
            if (mb.GetComponent<PCComponentMetadata>() != null)
            {
                if (interactable is IXRSelectInteractable selectInteractable)
                {
                    selectInteractable.selectEntered.AddListener(OnSelectEntered);
                    selectInteractable.selectExited.AddListener(OnSelectExited);
                    subscribedInteractables.Add(interactable);
                }
            }
        }
    }

    private void Unsubscribe(IXRInteractable interactable)
    {
        if (interactable == null || !subscribedInteractables.Contains(interactable)) return;

        if (interactable is IXRSelectInteractable selectInteractable)
        {
            selectInteractable.selectEntered.RemoveListener(OnSelectEntered);
            selectInteractable.selectExited.RemoveListener(OnSelectExited);
        }
        subscribedInteractables.Remove(interactable);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactableObject is MonoBehaviour mb)
        {
            PCComponentMetadata metadata = mb.GetComponent<PCComponentMetadata>();
            if (metadata != null && uiInstance != null)
            {
                uiInstance.Show(metadata);
            }
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        if (uiInstance != null)
        {
            uiInstance.Hide();
        }
    }

    private void OnDestroy()
    {
        if (interactionManager != null)
        {
            interactionManager.interactableRegistered -= OnInteractableRegistered;
            interactionManager.interactableUnregistered -= OnInteractableUnregistered;
        }

        foreach (var interactable in subscribedInteractables)
        {
            if (interactable is IXRSelectInteractable selectInteractable)
            {
                selectInteractable.selectEntered.RemoveListener(OnSelectEntered);
                selectInteractable.selectExited.RemoveListener(OnSelectExited);
            }
        }
        subscribedInteractables.Clear();
    }

    // Helper to manually register if needed (backwards compatibility for my setup script)
    public void RegisterInteractable(XRGrabInteractable interactable)
    {
        TrySubscribe(interactable);
    }
}
