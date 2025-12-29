using UnityEngine;

public class ScreenTopAnchor3D : MonoBehaviour
{
    [Header("Settings")]
    public float distanceFromCamera = 100f;
    [Range(0.9f, 1.2f)]
    public float verticalOffset = 1.0f;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    void LateUpdate()
    {
        if(mainCamera == null)
        {
            return;
        }
        UpdatePosition();
    }
    void UpdatePosition()
    {
        Vector3 targetViewportPos = new Vector3(0.5f, verticalOffset, distanceFromCamera);
        Vector3 worldPos = mainCamera.ViewportToWorldPoint(targetViewportPos);
        transform.position = worldPos;
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
    }
}
