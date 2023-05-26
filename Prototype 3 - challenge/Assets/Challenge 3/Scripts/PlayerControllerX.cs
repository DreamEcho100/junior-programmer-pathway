using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
	public bool gameOver = false;

	public float
		floatForce = 50f,
		bounceForce = 20f,
		upperBoundary = 14.5f;
	private float gravityModifier = 1.5f;
	private Rigidbody playerRb;

	public ParticleSystem explosionParticle;
	public ParticleSystem fireworksParticle;

	private AudioSource playerAudio;
	public AudioClip
		moneySound,
		explodeSound,
		bounceSound;


	// Start is called before the first frame update
	void Start()
	{
		Physics.gravity *= gravityModifier;
		playerAudio = GetComponent<AudioSource>();
		playerRb = GetComponent<Rigidbody>();

		// Apply a small upward force at the start of the game
		playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

	}

	// Update is called once per frame
	void Update()
	{
		if (gameOver) return;

		if (transform.position.y > upperBoundary)
		{
			transform.position = new Vector3(transform.position.x, upperBoundary, transform.position.z);
			playerRb.velocity = new Vector3(0, 0, 0);
		}

		// While space is pressed and player is low enough, float up
		if (Input.GetKey(KeyCode.Space))
		{
			playerRb.AddForce(Vector3.up * Time.deltaTime * floatForce, ForceMode.Impulse);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		// if player collides with bomb, explode and set gameOver to true
		if (other.gameObject.CompareTag("Bomb"))
		{
			explosionParticle.Play();
			playerAudio.PlayOneShot(explodeSound, 1.0f);
			gameOver = true;
			Debug.Log("Game Over!");
			Destroy(other.gameObject);
		}

		// if player collides with money, fireworks
		else if (other.gameObject.CompareTag("Money"))
		{
			fireworksParticle.Play();
			playerAudio.PlayOneShot(moneySound, 1.0f);
			Destroy(other.gameObject);

		}

		else if (other.gameObject.CompareTag("Ground"))
		{
			if (gameOver) return;
			playerAudio.PlayOneShot(bounceSound, 1.0f);
			playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
		}

	}

}
