using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RURANO : MonoBehaviour
{
    public float speed = -25f;

    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
