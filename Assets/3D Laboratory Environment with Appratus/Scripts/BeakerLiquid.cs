using UnityEngine;

public class BeakerLiquid : MonoBehaviour
{
    public Material liquidMaterial;

    [Range(0f, 1f)]
    public float fillAmount = 0.5f;

    void Update()
    {
        liquidMaterial.SetFloat("FillVolume", fillAmount);
    }
}