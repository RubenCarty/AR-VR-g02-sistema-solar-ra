using UnityEngine;

public class PlanetFocusController : MonoBehaviour
{
    public static PlanetFocusController Instance;

    [Header("Referencias")]
    public Camera arCamera;

    [Header("Configuración de enfoque")]
    public float focusDistance = 0.4f;
    public float zoomScale = 2.5f;
    [Range(0.5f, 20f)]
    public float followSpeed = 6f; // más alto = sigue/centra más rápido

    private Transform solarSystemRoot;
    private Transform focusedPlanet; // null = vista general

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;
    private bool hasOriginalPose = false;

    private void Awake()
    {
        Instance = this;
        if (arCamera == null)
            arCamera = Camera.main;
    }

    public void SetSolarSystemRoot(Transform root)
    {
        solarSystemRoot = root;
        originalPosition = root.position;
        originalRotation = root.rotation;
        originalScale = root.localScale;
        hasOriginalPose = true;
        focusedPlanet = null;
    }

    public void FocusOnPlanet(Transform planet)
    {
        if (!hasOriginalPose || solarSystemRoot == null || arCamera == null)
            return;

        focusedPlanet = planet;
    }

    public void ResetView()
    {
        focusedPlanet = null;
    }

    private void LateUpdate()
    {
        if (!hasOriginalPose || solarSystemRoot == null || arCamera == null)
            return;

        Vector3 targetPosition;
        Vector3 targetScale;

        if (focusedPlanet != null)
        {
            Vector3 desiredWorldPos = arCamera.transform.position + arCamera.transform.forward * focusDistance;

            targetScale = originalScale * zoomScale;

            // Distancia extra según el tamaño del objeto enfocado, para no quedar dentro de él
            Planet planetData = focusedPlanet.GetComponent<Planet>();
            float extraDistance = (planetData != null) ? planetData.visualRadius * zoomScale : 0f;

            Vector3 desiredWorldPosAdjusted = arCamera.transform.position + arCamera.transform.forward * (focusDistance + extraDistance);

            Vector3 delta = desiredWorldPosAdjusted - focusedPlanet.position;
            targetPosition = solarSystemRoot.position + delta;
        }
        else
        {
            targetPosition = originalPosition;
            targetScale = originalScale;
        }

        // Suavizado independiente del framerate, y no afectado por Time.timeScale
        float t = 1f - Mathf.Exp(-followSpeed * Time.unscaledDeltaTime);

        solarSystemRoot.position = Vector3.Lerp(solarSystemRoot.position, targetPosition, t);
        solarSystemRoot.rotation = Quaternion.Slerp(solarSystemRoot.rotation, originalRotation, t);
        solarSystemRoot.localScale = Vector3.Lerp(solarSystemRoot.localScale, targetScale, t);
    }
}