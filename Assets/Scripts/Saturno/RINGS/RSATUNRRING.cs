using UnityEngine;

public class SaturnRingFollow : MonoBehaviour
{
    public Transform saturn;

    [Header("Velocidad de giro de los anillos")]
    public float ringRotationSpeed = 15f;

    void LateUpdate()
    {
        // Seguir posición de Saturno
        transform.position = saturn.position;

        // Seguir rotación de Saturno
        transform.rotation = saturn.rotation;

        // Girar anillos sobre su eje local
        transform.Rotate(Vector3.up * ringRotationSpeed * Time.deltaTime, Space.Self);
    }
}