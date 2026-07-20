using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject instructionPanel;
    public GameObject navigationPanel;

    private void Start()
    {
        instructionPanel.SetActive(true);
        navigationPanel.SetActive(false);
    }

    public void StartExperience()
    {
        instructionPanel.SetActive(false);
        navigationPanel.SetActive(true);
    }
}