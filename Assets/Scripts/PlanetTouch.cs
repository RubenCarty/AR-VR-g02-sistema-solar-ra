using UnityEngine;

public class PlanetTouch : MonoBehaviour
{
    private Planet planet;

    private static float lastTapTime = -1f;
    private static Planet lastTappedPlanet;
    private const float doubleTapThreshold = 0.3f; // segundos entre taps

    private void Awake()
    {
        planet = GetComponent<Planet>();
    }

    private void OnMouseDown()
    {
        if (planet == null)
            return;

        // Comportamiento actual: seleccionar y enfocar al tocar
        PlanetSelector.Instance.SelectPlanet(planet.planetIndex);

        // Detectar si es un segundo tap rápido sobre el MISMO planeta
        float now = Time.unscaledTime;
        bool isDoubleTap = (planet == lastTappedPlanet) && (now - lastTapTime <= doubleTapThreshold);

        lastTapTime = now;
        lastTappedPlanet = planet;

        if (isDoubleTap)
        {
            PlanetInfoPanel.Instance.ShowInfo(planet);
        }
    }
}