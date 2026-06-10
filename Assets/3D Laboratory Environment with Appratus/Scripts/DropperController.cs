using UnityEngine;

public class DropperController : MonoBehaviour
{
    public bool isFilled = false;

    public GameObject dropVisual;

    private void OnTriggerEnter(Collider other)
    {
        // Fill dropper from red cabbage juice
        if (other.GetComponent<IndicatorBeaker>())
        {
            FillDropper();
        }

        // Detect tube mouth
        TubeMouthTrigger tubeTrigger =
            other.GetComponent<TubeMouthTrigger>();

        if (tubeTrigger != null && isFilled)
        {
            ReleaseDrop(tubeTrigger);
        }
    }

    void FillDropper()
    {
        if (isFilled)
            return;

        isFilled = true;

        dropVisual.SetActive(true);

        Debug.Log("Dropper Filled");
    }

    void ReleaseDrop(TubeMouthTrigger tubeTrigger)
    {
        isFilled = false;

        tubeTrigger.testTube.AddIndicator();

        DropDrop();
    }

    void DropDrop()
    {
        dropVisual.transform.parent = null;

        Rigidbody rb =
            dropVisual.AddComponent<Rigidbody>();

        rb.useGravity = true;

        rb.mass = 0.01f;
    }
}