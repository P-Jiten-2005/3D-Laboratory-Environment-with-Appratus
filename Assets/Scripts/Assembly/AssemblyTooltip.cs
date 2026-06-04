using TMPro;
using UnityEngine;

public class AssemblyTooltip : MonoBehaviour
{
    [SerializeField] private GameObject contentRoot;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Vector3 offset = new Vector3(0, 0.2f, 0);
    [SerializeField] private bool lookAtCamera = true;

    private Camera mainCamera;
    private AssemblyPart currentTarget;

    private void Awake()
    {
        mainCamera = Camera.main;
        Hide();
    }

    private void LateUpdate()
    {
        if (currentTarget == null || !contentRoot.activeSelf)
        {
            return;
        }

        transform.position = currentTarget.transform.position + offset;

        if (lookAtCamera && mainCamera != null)
        {
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        }
    }

    public void Show(AssemblyPart part)
    {
        currentTarget = part;
        titleText.text = part.PartId;
        descriptionText.text = part.Description;
        contentRoot.SetActive(true);
    }

    public void Hide()
    {
        contentRoot.SetActive(false);
        currentTarget = null;
    }
}
