using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMOON : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
