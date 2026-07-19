using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PausarAR : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public ARRaycastManager raycastManager;

    public void Pausar()
    {
        if (planeManager != null)
            planeManager.enabled = false;

        if (raycastManager != null)
            raycastManager.enabled = false;
    }

    public void Reanudar()
    {
        if (planeManager != null)
            planeManager.enabled = true;

        if (raycastManager != null)
            raycastManager.enabled = true;
    }
}
