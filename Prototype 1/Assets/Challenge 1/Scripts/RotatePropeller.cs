using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePropeller : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1000.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }
}
