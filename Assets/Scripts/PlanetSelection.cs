using UnityEngine;

public class PlanetSelection : MonoBehaviour
{
    private Camera arCamera;

    void Start()
    {
        arCamera = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began)
            return;

        Ray ray = arCamera.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Planet planet = hit.collider.GetComponent<Planet>();

            if (planet != null)
            {
                Debug.Log("Seleccionaste: " + planet.planetName);
            }
        }
    }
}