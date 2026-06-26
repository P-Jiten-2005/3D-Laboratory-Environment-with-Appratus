using UnityEngine;

public class BeakerLiquid : MonoBehaviour
{
    public ChemicalType chemicalType;

    [Range(0,1)]
    public float fillAmount = 1f;

    public Renderer liquidRenderer;

    public Material copperMaterial;
    public Material ammoniaMaterial;
    public Material mixedMaterial;

    void Start()
    {
        UpdateMaterial();
    }

    public void UpdateMaterial()
    {
        if (chemicalType == ChemicalType.CopperSulfate)
            liquidRenderer.material = copperMaterial;

        else if (chemicalType == ChemicalType.Ammonia)
            liquidRenderer.material = ammoniaMaterial;
    }

    public void SetMixed()
    {
        chemicalType = ChemicalType.None;
        liquidRenderer.material = mixedMaterial;
    }
}