using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOrbit : MonoBehaviour
{
    public Transform center;   // Aquí asignas el Sol
    public float speed = 90f;

    void Update()
    {
        if (center == null) return;

        transform.RotateAround(center.position, Vector3.up, speed * Time.deltaTime);
    }
}
