using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private float verticalInput;

    [SerializeField]
    private float speed = 15.0f, rotationSpeed = 100.0f;

    // Update is called once per frame
    void Update()
    {
        // Get the user's vertical input
        verticalInput = Input.GetAxis("Vertical");

        // Move the plane forward at a constant rate
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (verticalInput != 0)
            // Tilt the plane up/down based on up/down arrow keys
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime * verticalInput);
    }
}
