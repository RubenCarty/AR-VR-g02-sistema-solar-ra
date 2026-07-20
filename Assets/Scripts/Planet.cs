using UnityEngine;

public class Planet : MonoBehaviour
{
    public string planetName;
    public int planetIndex;

    [Tooltip("Radio visual aproximado (para el enfoque de cámara)")]
    public float visualRadius = 0.3f;

    [Header("Documentación")]
    [TextArea(3, 10)]
    public string description;
}