using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNEPTUNO : MonoBehaviour
{
    public float speed = 28f;

    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}