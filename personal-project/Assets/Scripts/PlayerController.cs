using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	private new Rigidbody rigidbody;

	private float verticalInput, horizontalInput;

	private const string ENEMY_TAG = "Enemy";
	private const string POWER_UP_TAG = "PowerUp";

	[SerializeField]
	private float
		speed = 10.0f,
		zBound = 6.0f;


	// Start is called before the first frame update
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		MovePlayer();
		ConstrainPlayerPosition();
	}

	private void MovePlayer()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");

		if (verticalInput != 0 || horizontalInput != 0)
			rigidbody.AddForce(
				(Vector3.forward * speed * verticalInput) +
				(Vector3.right * speed * horizontalInput)
			);
	}

	private void ConstrainPlayerPosition()
	{
		if (transform.position.z < -zBound)
			transform.position = new Vector3(
				transform.position.x,
				transform.position.y,
				-zBound
			);

		if (transform.position.z > zBound)
			transform.position = new Vector3(
				transform.position.x,
				transform.position.y,
				zBound
			);
	}

	void OnCollisionEnter(Collision other)
	{
		switch (other.gameObject.tag)
		{
			case ENEMY_TAG:
				Debug.Log("Player collided with an enemy!");
				break;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		switch (other.gameObject.tag)
		{
			case POWER_UP_TAG:
				Destroy(other.gameObject);
				break;
		}
	}
}
