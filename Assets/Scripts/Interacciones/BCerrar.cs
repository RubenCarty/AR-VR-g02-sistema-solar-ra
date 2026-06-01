using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCerrar : MonoBehaviour
{
    public GameObject panel;
    public PausarAR pausarAR;

    void Start()
    {
        // Pausar AR
        if (pausarAR != null)
            pausarAR.Pausar();

        // 🛑 Pausar animaciones
        Time.timeScale = 0f;
    }

    public void CerrarPanel()
    {
        panel.SetActive(false);

        if (pausarAR != null)
            pausarAR.Reanudar();

        // ▶ Reanudar animaciones
        Time.timeScale = 1f;
    }
}
