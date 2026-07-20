using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementAR : MonoBehaviour
{
    public GameObject prefab;
    public ARRaycastManager raycastManager;
    public GameObject instructionsPanel;

    [Header("Ajustes de colocación")]
    public float placementScale = 0.3f; // ajusta según el tamaño real de tu prefab

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject spawnedObject;

    void Update()
    {
        if (instructionsPanel != null && instructionsPanel.activeSelf)
            return;

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            return;

        if (touch.phase == TouchPhase.Began && spawnedObject == null)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose pose = hits[0].pose;

                spawnedObject = Instantiate(prefab, pose.position, pose.rotation);
                spawnedObject.transform.localScale *= placementScale;

                PlanetSelector.Instance.RegisterSolarSystem(spawnedObject);
                PlanetFocusController.Instance.SetSolarSystemRoot(spawnedObject.transform);
            }
        }
    }
}