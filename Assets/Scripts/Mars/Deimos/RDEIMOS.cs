using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RDEIMOS : MonoBehaviour
{
    public float speed = 12f;

    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}

