using UnityEngine;


public class FlaskRotationLimiter : MonoBehaviour
{
    [Header("Maximum allowed tilt angle")]
    [Range(0f, 89f)]
    public float maxTilt = 85f;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (grabInteractable == null)
        {
            Debug.LogWarning("XRGrabInteractable not found on " + gameObject.name);
        }
    }

    private void LateUpdate()
    {
        if (grabInteractable == null || !grabInteractable.isSelected)
            return;

        Vector3 euler = transform.eulerAngles;

        float x = NormalizeAngle(euler.x);
        float z = NormalizeAngle(euler.z);

        x = Mathf.Clamp(x, -maxTilt, maxTilt);
        z = Mathf.Clamp(z, -maxTilt, maxTilt);

        transform.rotation = Quaternion.Euler(x, euler.y, z);
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f)
            angle -= 360f;

        return angle;
    }
}