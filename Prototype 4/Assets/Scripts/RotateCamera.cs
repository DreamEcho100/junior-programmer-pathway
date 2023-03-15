using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
	private float horizontalInput;

	[SerializeField]
	private float rotationSpeed = 50.0f;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		horizontalInput = Input.GetAxis("Horizontal");

		transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
	}
}
