using UnityEngine;
using TMPro;

public class PlanetInfoPanel : MonoBehaviour
{
    public static PlanetInfoPanel Instance;

    [Header("UI")]
    public GameObject panel;
    public TMP_Text titleText;
    public TMP_Text bodyText;

    [Header("Paneles a ocultar mientras se muestra la info")]
    public GameObject navigationPanel;

    [Header("Info general del sistema solar")]
    public string solarSystemTitle = "El Sistema Solar";
    [TextArea(3, 10)]
    public string solarSystemDescription = "Escribe aquí la información general del sistema solar...";

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void ShowInfo(Planet planet)
    {
        if (planet == null || panel == null)
            return;

        titleText.text = planet.planetName;
        bodyText.text = string.IsNullOrEmpty(planet.description)
            ? "Aún no hay información cargada para este elemento."
            : planet.description;

        panel.SetActive(true);

        if (navigationPanel != null)
            navigationPanel.SetActive(false);

        Time.timeScale = 0f;
    }

    public void ShowSolarSystemInfo()
    {
        titleText.text = solarSystemTitle;
        bodyText.text = solarSystemDescription;

        panel.SetActive(true);

        if (navigationPanel != null)
            navigationPanel.SetActive(false);

        Time.timeScale = 0f;
    }

    public void ClosePanel()
    {
        panel.SetActive(false);

        if (navigationPanel != null)
            navigationPanel.SetActive(true);

        Time.timeScale = 1f;
    }
}