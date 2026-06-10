using UnityEngine;

public class FlaskPouring : MonoBehaviour
{
    [Header("Liquid Material")]
    public Material liquidMaterial;

    [Header("Fill Settings")]
    [Range(0f, 1f)]
    public float fillAmount = 0.58f;

    private float originalFill;

    [Header("Pouring")]
    public float startPourAngle = 50f;
    public float maxPourAngle = 170f;

    public float emptySpeed = 0.3f;
    public float refillSpeed = 0.5f;

    void Start()
    {
        originalFill = fillAmount;
    }

    void Update()
    {
        float angle = Vector3.Angle(transform.up, Vector3.up);

        if (angle > startPourAngle)
        {
            float pourFactor =
                Mathf.InverseLerp(startPourAngle, maxPourAngle, angle);

            fillAmount -= emptySpeed * pourFactor * Time.deltaTime;
        }
        else
        {
            fillAmount += refillSpeed * Time.deltaTime;
        }

        fillAmount = Mathf.Clamp(fillAmount, 0f, originalFill);

        liquidMaterial.SetFloat("_FillAmount", fillAmount);
    }
}