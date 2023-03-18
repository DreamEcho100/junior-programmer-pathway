using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
	private new Rigidbody rigidbody;

	[SerializeField]
	private float
		speed = 5.0f,
		zBoundary = -10.0f;

	void FixedUpdate()
	{
		rigidbody.AddForce(Vector3.forward * -speed); // , ForceMode.Impulse
	}

	// Start is called before the first frame update
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position.z < zBoundary)
		{
			Destroy(gameObject);
		}
	}
}
