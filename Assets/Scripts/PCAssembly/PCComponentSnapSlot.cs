using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace PCAssembly
{
    public class PCComponentSnapSlot : MonoBehaviour
    {
        [Header("Configuration")]
        public string targetComponentType = "RAM";
        public Transform snapTransform;
        public GameObject ghostPreview;
        public float snapRadius = 0.15f;
        public float alignmentStrength = 5f;
        
        [Header("Feedback")]
        public AudioClip snapSound;
        public float hapticIntensity = 0.5f;
        public float hapticDuration = 0.1f;

        [Header("State")]
        public bool isOccupied = false;
        public GameObject installedComponent;

        private XRGrabInteractable currentHoveredInteractable;

        private void Start()
        {
            if (ghostPreview != null) ghostPreview.SetActive(false);
            if (snapTransform == null) snapTransform = transform;
            
            // Ensure we have a trigger collider
            var col = GetComponent<SphereCollider>();
            if (col == null)
            {
                col = gameObject.AddComponent<SphereCollider>();
                col.isTrigger = true;
                col.radius = snapRadius;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isOccupied) return;

            var metadata = other.GetComponentInParent<PCComponentMetadata>();
            if (metadata != null && metadata.data != null && metadata.data.componentType == targetComponentType)
            {
                var interactable = metadata.GetComponent<XRGrabInteractable>();
                if (interactable != null && interactable.isSelected)
                {
                    currentHoveredInteractable = interactable;
                    currentHoveredInteractable.selectExited.AddListener(OnGrabReleased);
                    ShowGhost(true);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (isOccupied || currentHoveredInteractable == null) return;

            // Assisted alignment logic
            if (currentHoveredInteractable.isSelected)
            {
                float dist = Vector3.Distance(currentHoveredInteractable.transform.position, snapTransform.position);
                if (dist < snapRadius)
                {
                    // Gently guide the held object towards the snap transform
                    // Note: This modifies the transform directly which might cause slight jitter with some VR setups,
                    // but it provides the "magnetic" feeling requested.
                    float t = Time.deltaTime * alignmentStrength * (1f - dist / snapRadius);
                    currentHoveredInteractable.transform.position = Vector3.Lerp(currentHoveredInteractable.transform.position, snapTransform.position, t);
                    currentHoveredInteractable.transform.rotation = Quaternion.Slerp(currentHoveredInteractable.transform.rotation, snapTransform.rotation, t);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (isOccupied) return;

            var metadata = other.GetComponentInParent<PCComponentMetadata>();
            if (metadata != null && currentHoveredInteractable != null && metadata.gameObject == currentHoveredInteractable.gameObject)
            {
                CleanupHover();
            }
        }

        private void CleanupHover()
        {
            if (currentHoveredInteractable != null)
            {
                currentHoveredInteractable.selectExited.RemoveListener(OnGrabReleased);
                currentHoveredInteractable = null;
            }
            ShowGhost(false);
        }

        private void ShowGhost(bool show)
        {
            if (ghostPreview != null)
            {
                ghostPreview.SetActive(show);
                if (show)
                {
                    ghostPreview.transform.position = snapTransform.position;
                    ghostPreview.transform.rotation = snapTransform.rotation;
                }
            }
        }

        private void OnGrabReleased(SelectExitEventArgs args)
        {
            if (isOccupied || currentHoveredInteractable == null) return;

            // Check distance again to be sure
            if (Vector3.Distance(currentHoveredInteractable.transform.position, snapTransform.position) < snapRadius * 1.5f)
            {
                SnapComponent(currentHoveredInteractable.gameObject, args.interactorObject);
            }
            else
            {
                CleanupHover();
            }
        }

        public void SnapComponent(GameObject component, IXRInteractor interactor = null)
        {
            isOccupied = true;
            installedComponent = component;
            
            // Disable interaction
            var interactable = component.GetComponent<XRGrabInteractable>();
            if (interactable != null)
            {
                interactable.enabled = false;
            }

            // Disable physics
            var rb = component.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            // Perfect alignment
            component.transform.SetParent(snapTransform);
            component.transform.localPosition = Vector3.zero;
            component.transform.localRotation = Quaternion.identity;

            // Feedback
            if (snapSound != null) AudioSource.PlayClipAtPoint(snapSound, transform.position);
            
            if (interactor is XRBaseInputInteractor inputInteractor)
            {
                inputInteractor.SendHapticImpulse(hapticIntensity, hapticDuration);
            }
            
            ShowGhost(false);
            
            // Cleanup references
            if (currentHoveredInteractable != null)
            {
                currentHoveredInteractable.selectExited.RemoveListener(OnGrabReleased);
                currentHoveredInteractable = null;
            }

            Debug.Log($"{targetComponentType} Installed successfully in {gameObject.name}");
        }
    }
}
