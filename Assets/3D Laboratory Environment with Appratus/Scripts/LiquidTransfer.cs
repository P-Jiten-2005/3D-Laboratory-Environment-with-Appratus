using UnityEngine;

public class LiquidTransfer : MonoBehaviour
{
    public Transform pourPoint;

    public LayerMask receiverLayer;

    public float rayDistance = 0.4f;

    public float transferRate = 0.2f;

    public float pourAngle = 50f;

    public BeakerLiquid source;

    private BeakerLiquid target;

    void Update()
    {
        float angle =
            Vector3.Angle(
                transform.up,
                Vector3.up);

        if (angle < pourAngle)
            return;

        Debug.DrawRay(
            pourPoint.position,
            pourPoint.forward *
            rayDistance,
            Color.red);

        Ray ray = new Ray(
            pourPoint.position,
            pourPoint.forward);

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            rayDistance,
            receiverLayer))
        {
            target =
                hit.GetComponentInParent<BeakerLiquid>();

            if (target != null)
            {
                TransferLiquid();
            }
        }
    }

    void TransferLiquid()
    {
        if (source.fillAmount <= 0)
            return;

        float amount =
            transferRate *
            Time.deltaTime;

        source.fillAmount -= amount;

        target.fillAmount += amount;

        source.fillAmount =
            Mathf.Clamp01(
                source.fillAmount);

        target.fillAmount =
            Mathf.Clamp01(
                target.fillAmount);

        CheckReaction();
    }

    void CheckReaction()
    {
        bool reaction1 =
            source.chemical ==
            ChemicalType.CopperSulfate &&
            target.chemical ==
            ChemicalType.Ammonia;

        bool reaction2 =
            source.chemical ==
            ChemicalType.Ammonia &&
            target.chemical ==
            ChemicalType.CopperSulfate;

        if (reaction1 || reaction2)
        {
            source.SetChemical(
                ChemicalType.CopperAmmoniaComplex);

            target.SetChemical(
                ChemicalType.CopperAmmoniaComplex);
        }
    }
}