using UnityEngine;
using TMPro;
using System.Linq;

public class PlanetSelector : MonoBehaviour
{
    public static PlanetSelector Instance;

    [Header("UI")]
    public TMP_Text planetNameText;

    private Planet[] planets;
    private int currentIndex = -1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (planetNameText != null)
            planetNameText.gameObject.SetActive(false);
    }

    public void RegisterSolarSystem(GameObject solarSystem)
    {
        planets = solarSystem.GetComponentsInChildren<Planet>()
                             .OrderBy(p => p.planetIndex)
                             .ToArray();

        Debug.Log("Planetas encontrados: " + planets.Length);
    }

    public void SelectPlanet(int index)
    {
        if (planets == null || index < 0 || index >= planets.Length)
            return;

        currentIndex = index;

        planetNameText.gameObject.SetActive(true);
        planetNameText.text = planets[index].planetName;

        PlanetFocusController.Instance.FocusOnPlanet(planets[index].transform);
    }

    public void NextPlanet()
    {
        if (planets == null || planets.Length == 0)
            return;

        currentIndex = (currentIndex + 1) % planets.Length;
        SelectPlanet(currentIndex);
    }

    public void PreviousPlanet()
    {
        if (planets == null || planets.Length == 0)
            return;

        if (currentIndex == -1)
            currentIndex = planets.Length - 1;
        else
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = planets.Length - 1;
        }

        SelectPlanet(currentIndex);
    }

    // Botón para volver a la vista general
    public void DeselectAndReset()
    {
        currentIndex = -1;
        if (planetNameText != null)
            planetNameText.gameObject.SetActive(false);

        PlanetFocusController.Instance.ResetView();
    }
}