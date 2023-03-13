using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

	// private new Rigidbody rigidbody;

	[SerializeField]
	private float
		speed = 25,
		startDelay = 2,
		repeatRate = 2;

	// Start is called before the first frame update
	void Start()
	{
		// rigidbody = transform.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(Vector3.left * Time.deltaTime * speed);
		// rigidbody.velocity = Vector3.left * speed;
	}
}
