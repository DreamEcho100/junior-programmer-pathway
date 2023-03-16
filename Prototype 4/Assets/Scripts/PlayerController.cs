using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private GameObject focalPoint;

	private new Rigidbody rigidbody;
	private float verticalInput;

	private const string POWER_UP_TAG = "PowerUp";
	private const string ENEMY_TAG = "Enemy";


	[SerializeField]
	private GameObject powerUpIndicator;
	[SerializeField]
	private float
		speed = 5.0f,
		powerUpStrength = 15.0f;

	[SerializeField]
	private bool hasPowerUp = false;

	// Start is called before the first frame update
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		focalPoint = GameObject.Find("FocalPoint");
		// powerUpIndicator = GameObject.Find("PowerUpIndicator");
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

	void OnCollisionEnter(Collision other)
	{

		switch (other.gameObject.tag)
		{

			case ENEMY_TAG:
				if (!hasPowerUp) break;

				Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
				Vector3 awayFromPlayer = (other.gameObject.transform.position - transform.position);

				Debug.Log($"Collided with {other.gameObject.name} with power up set to {hasPowerUp}");
				enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);

				break;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
			case POWER_UP_TAG:
				hasPowerUp = true;
				powerUpIndicator.SetActive(true);
				Destroy(other.gameObject);
				StartCoroutine(nameof(PowerUpCountdownRoutine));
				break;
		}
	}

	IEnumerator PowerUpCountdownRoutine()
	{
		yield return new WaitForSeconds(7.0f);
		hasPowerUp = false;
		powerUpIndicator.SetActive(false);
	}
}
