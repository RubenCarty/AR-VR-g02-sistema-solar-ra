using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementAR : MonoBehaviour
{
    public GameObject prefab; // SolarSystem prefab
    public ARRaycastManager raycastManager;
    public GameObject instructionsPanel;

    [Header("Ajustes de colocación")]
    public Camera arCamera;
    public float spawnForwardOffset = 1.0f; // metros hacia adelante de la cámara
    public float placementScale = 0.3f;     // escala inicial (ajusta según el tamaño real de tu prefab)

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject spawnedObject;

    void Start()
    {
        if (arCamera == null)
            arCamera = Camera.main;
    }

    void Update()
    {
        if (instructionsPanel != null && instructionsPanel.activeSelf)
            return;

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began && spawnedObject == null)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose pose = hits[0].pose;

                // Desplazamos el punto de colocación hacia adelante de la cámara
                // (proyectado sobre el plano horizontal) para no aparecer justo debajo del usuario
                Vector3 forwardFlat = arCamera.transform.forward;
                forwardFlat.y = 0;
                forwardFlat.Normalize();

                Vector3 spawnPosition = pose.position + forwardFlat * spawnForwardOffset;

                spawnedObject = Instantiate(prefab, spawnPosition, pose.rotation);
                spawnedObject.transform.localScale *= placementScale;

                PlanetSelector.Instance.RegisterSolarSystem(spawnedObject);
                PlanetFocusController.Instance.SetSolarSystemRoot(spawnedObject.transform);
            }
        }
    }
}