using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementAR : MonoBehaviour
{
    public GameObject prefab; // SolarSystem prefab
    public ARRaycastManager raycastManager;
    public GameObject instructionsPanel;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject spawnedObject;

    void Update()
    {
        // Bloquear si el panel está abierto
        if (instructionsPanel != null && instructionsPanel.activeSelf)
            return;

        // Detectar toque
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose pose = hits[0].pose;

                // Instanciar una sola vez
                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(prefab, pose.position, pose.rotation);
                }
                else
                {
                    // mover si ya existe
                    spawnedObject.transform.position = pose.position;
                }
            }
        }
    }
}
