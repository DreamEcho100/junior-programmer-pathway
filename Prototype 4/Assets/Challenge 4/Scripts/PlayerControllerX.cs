using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
	private Rigidbody playerRb;
	private float speed = 500;
	private GameObject focalPoint;

	public bool hasPowerup;
	public GameObject powerupIndicator;
	public ParticleSystem dashingIndicator;
	public int powerUpDuration = 5;

	private float normalStrength = 10; // how hard to hit enemy without powerup
	private float powerupStrength = 25; // how hard to hit enemy with powerup

	private bool isDashing = false;
	private Vector3
		powerupIndicatorOffset = new Vector3(0, -0.6f, 0),
		playerRbAddedForce;


	void Start()
	{
		playerRb = GetComponent<Rigidbody>();
		focalPoint = GameObject.Find("Focal Point");
	}

	void Update()
	{
		// Add force to player in direction of the focal point (and camera)
		float verticalInput = Input.GetAxis("Vertical");

		playerRbAddedForce = focalPoint.transform.forward * verticalInput * speed * Time.deltaTime;


		if (Input.GetKeyDown(KeyCode.Space))
		{
			isDashing = true;
			dashingIndicator.Play();
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			isDashing = false;
			dashingIndicator.Stop();
		}

		if (Input.GetKey(KeyCode.Space))
		{
			playerRbAddedForce *= 2;
			powerupIndicator.transform.position = transform.position + powerupIndicatorOffset;
		}

		playerRb.AddForce(playerRbAddedForce);

		// Set powerup indicator position to beneath player
		if (hasPowerup)
			powerupIndicator.transform.position = transform.position + powerupIndicatorOffset;
		// powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

	}

	// If Player collides with powerup, activate powerup
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Powerup"))
		{
			Destroy(other.gameObject);
			hasPowerup = true;
			powerupIndicator.SetActive(true);
			StartCoroutine(nameof(PowerupCooldown));
		}
	}

	// Coroutine to count down powerup duration
	IEnumerator PowerupCooldown()
	{
		yield return new WaitForSeconds(powerUpDuration);
		hasPowerup = false;
		powerupIndicator.SetActive(false);
	}

	// If Player collides with enemy
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
			Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

			if (hasPowerup) // if have powerup hit enemy with powerup force
			{
				enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
			}
			else // if no powerup, hit enemy with normal strength 
			{
				enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
			}


		}
	}



}
