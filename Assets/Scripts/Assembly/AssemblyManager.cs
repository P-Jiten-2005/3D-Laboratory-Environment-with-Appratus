using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class AssemblyStep
{
    [TextArea]
    public string instruction;
    [TextArea]
    public string infoText;
    public AssemblyPart targetPart;
    public Transform targetAnchor;
    [Min(0f)] public float snapDistance = 0.07f;
    [Range(0f, 180f)] public float snapAngle = 25f;
}

public class AssemblyManager : MonoBehaviour
{
    [SerializeField] private List<AssemblyStep> steps = new();
    [SerializeField] private bool lockWrongParts = true;
    [SerializeField] private bool autoStartOnEnable = true;

    [Header("UI & Feedback")]
    [SerializeField] private AssemblyTooltip tooltip;
    [SerializeField] private bool highlightCurrentPart = true;

    [Header("Events")]
    [SerializeField] private UnityEvent<string> onInstructionChanged;
    [SerializeField] private UnityEvent<string> onInfoChanged;
    [SerializeField] private UnityEvent<int, int> onProgressChanged;
    [SerializeField] private UnityEvent<string> onError;
    [SerializeField] private UnityEvent<AssemblyPart> onStepCompleted;
    [SerializeField] private UnityEvent onAssemblyCompleted;

    private readonly HashSet<AssemblyPart> registeredParts = new();
    private int currentStepIndex = -1;
    private bool completed;

    public int CurrentStepIndex => currentStepIndex;
    public int TotalSteps => steps.Count;
    public bool IsCompleted => completed;

    private void OnEnable()
    {
        RegisterPartEvents();

        if (autoStartOnEnable)
        {
            StartAssembly();
        }
    }

    private void OnDisable()
    {
        foreach (var part in registeredParts)
        {
            part.Released -= HandlePartReleased;
            part.HoverEntered -= HandlePartHoverEntered;
            part.HoverExited -= HandlePartHoverExited;
            part.Grabbed -= HandlePartGrabbed;
        }

        registeredParts.Clear();
    }

    public void StartAssembly()
    {
        completed = false;
        currentStepIndex = 0;
        UpdatePartLockState();
        BroadcastCurrentStep();
    }

    public void ResetAssembly()
    {
        foreach (var part in registeredParts)
        {
            part.ResetPartState();
        }

        StartAssembly();
    }

    public void ResetCurrentStepPart()
    {
        if (!TryGetCurrentStep(out var step))
        {
            return;
        }

        step.targetPart.ResetPartState();
        UpdatePartLockState();
        BroadcastCurrentStep();
    }

    private void RegisterPartEvents()
    {
        foreach (var step in steps)
        {
            if (step == null || step.targetPart == null)
            {
                continue;
            }

            if (registeredParts.Add(step.targetPart))
            {
                step.targetPart.Released += HandlePartReleased;
                step.targetPart.HoverEntered += HandlePartHoverEntered;
                step.targetPart.HoverExited += HandlePartHoverExited;
                step.targetPart.Grabbed += HandlePartGrabbed;
            }
        }
    }

    private void HandlePartHoverEntered(AssemblyPart part)
    {
        if (tooltip != null)
        {
            tooltip.Show(part);
        }
    }

    private void HandlePartHoverExited(AssemblyPart part)
    {
        if (tooltip != null)
        {
            tooltip.Hide();
        }
    }

    private void HandlePartGrabbed(AssemblyPart part)
    {
        if (tooltip != null)
        {
            tooltip.Show(part);
        }
    }

    private void HandlePartReleased(AssemblyPart releasedPart)
    {
        if (tooltip != null)
        {
            tooltip.Hide();
        }

        if (completed || !TryGetCurrentStep(out var step))
        {
            return;
        }

        if (releasedPart != step.targetPart)
        {
            if (lockWrongParts)
            {
                onError?.Invoke($"Current step: place {step.targetPart.PartId}.");
            }
            return;
        }

        if (!IsWithinSnapThreshold(releasedPart.transform, step.targetAnchor, step.snapDistance, step.snapAngle))
        {
            onError?.Invoke($"Align {releasedPart.PartId} with the highlighted slot.");
            return;
        }

        releasedPart.SnapTo(step.targetAnchor);
        releasedPart.LockPlacement();
        onStepCompleted?.Invoke(releasedPart);

        currentStepIndex++;
        if (currentStepIndex >= steps.Count)
        {
            completed = true;
            onProgressChanged?.Invoke(steps.Count, steps.Count);
            onInstructionChanged?.Invoke("Assembly complete.");
            onInfoChanged?.Invoke("Great work. You finished the full hardware assembly.");
            onAssemblyCompleted?.Invoke();
            return;
        }

        UpdatePartLockState();
        BroadcastCurrentStep();
    }

    private void UpdatePartLockState()
    {
        if (!lockWrongParts)
        {
            foreach (var part in registeredParts)
            {
                part.SetInteractable(true);
            }
            return;
        }

        AssemblyPart currentPart = null;
        if (TryGetCurrentStep(out var step))
        {
            currentPart = step.targetPart;
        }

        foreach (var part in registeredParts)
        {
            bool isCurrent = part == currentPart;
            part.SetInteractable(isCurrent);

            if (highlightCurrentPart)
            {
                var highlighter = part.GetComponent<PartHighlighter>();
                if (highlighter != null)
                {
                    highlighter.SetHighlight(isCurrent);
                }
            }
        }
    }

    private void BroadcastCurrentStep()
    {
        if (!TryGetCurrentStep(out var step))
        {
            onInstructionChanged?.Invoke("No assembly steps configured.");
            onInfoChanged?.Invoke(string.Empty);
            onProgressChanged?.Invoke(0, steps.Count);
            return;
        }

        onInstructionChanged?.Invoke(step.instruction);
        onInfoChanged?.Invoke(step.infoText);
        onProgressChanged?.Invoke(currentStepIndex, steps.Count);
    }

    private bool TryGetCurrentStep(out AssemblyStep step)
    {
        if (currentStepIndex < 0 || currentStepIndex >= steps.Count)
        {
            step = null;
            return false;
        }

        step = steps[currentStepIndex];
        return step != null && step.targetPart != null && step.targetAnchor != null;
    }

    private static bool IsWithinSnapThreshold(Transform source, Transform target, float maxDistance, float maxAngle)
    {
        var distance = Vector3.Distance(source.position, target.position);
        if (distance > maxDistance)
        {
            return false;
        }

        var angle = Quaternion.Angle(source.rotation, target.rotation);
        return angle <= maxAngle;
    }
}

