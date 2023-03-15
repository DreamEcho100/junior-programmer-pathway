using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private GameObject focalPoint;
	private new Rigidbody rigidbody;
	private float verticalInput;

	[SerializeField]
	private float speed = 5.0f;

	// Start is called before the first frame update
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		focalPoint = GameObject.Find("FocalPoint");
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		verticalInput = Input.GetAxis("Vertical");

		// FixedUpdate is framerate independent and is used for consistent
		// physics regardless of computer speed.
		//
		// Don't multiply the force by Time.deltaTime: AddForce already applies
		// the force during a single physics cycle in its default mode
		// (ForceMode.Force), thus multiplying it by Time.deltaTime will actually
		// result in a force 50 times smaller (there are 50 physics cycles per
		// second, thus Time.deltaTime is 1/50)
		rigidbody.AddForce(focalPoint.transform.forward * speed * verticalInput);
	}
}
