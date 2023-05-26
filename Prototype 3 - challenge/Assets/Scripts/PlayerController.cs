using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private new Rigidbody rigidbody;
	private Animator animator;
	private AudioSource audioSource, mainCameraAudioSource;

	private bool isOnGround = true;

	public float introductionSecs = 2.0f;
	private float elapsedIntroductionSecs;
	private Vector3 preStartPos;
	private Vector3 startPos = new Vector3(0.0f, 0.0f, 0.0f);

	private int jumpCounter = 0;
	private const string
		GROUND_TAG = "Ground",
		OBSTACLE_TAG = "Obstacle";

	[SerializeField]
	private ParticleSystem
		explosionParticle,
		dirtParticle;
	[SerializeField]
	private AudioClip jumpSound, crashSound;
	[SerializeField]
	private float
	JumpForce = 13f,
	GravityModifier = 2.5f;

	void Start()
	{
		// Get the Rigidbody, Animator, and AudioSource components from this object
		rigidbody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();

		// Find the AudioSource component on the "Main Camera" object
		mainCameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

		// Increase the strength of gravity by the GravityModifier variable
		Physics.gravity *= GravityModifier;

		// Save the starting position of this object
		preStartPos = transform.position;
	}

	void Update()
	{
		// Check if the game is still in the introduction phase or if it's already over
		if (!GameManager.instance.isGameStarting && !GameManager.instance.isGameOver)
		{
			// Increment the elapsed time for the introduction phase
			elapsedIntroductionSecs += Time.deltaTime;

			// Move the player from the pre-start position to the start position over the specified time
			transform.position = Vector3.Lerp(
					preStartPos,
					startPos,
					elapsedIntroductionSecs / introductionSecs
			);

			// If the elapsed time exceeds the specified introduction time, start the game
			if (elapsedIntroductionSecs > introductionSecs)
				GameManager.instance.StartGame();

			// Return to exit the function and skip the rest of the code in this update cycle
			return;
		}

		// If the game is over, gradually decrease the volume of the background music
		if (GameManager.instance.isGameOver)
		{
			// If the background music is not playing, return to exit the function and skip the rest of the code in this update cycle
			if (!mainCameraAudioSource.isPlaying)
				return;

			// Calculate the amount by which to decrease the volume based on the elapsed time and the volume down rate
			float volumeDown = (Time.deltaTime * GameManager.instance.volumeDownRate);

			// Decrease the volume of the background music by the calculated amount, as long as it won't go below zero
			if (mainCameraAudioSource.volume - volumeDown > 0)
				mainCameraAudioSource.volume = mainCameraAudioSource.volume - volumeDown;
			else
				// If the volume would go below zero, stop the background music
				mainCameraAudioSource.Stop();
		}

		// If the player presses the space key and is either on the ground or has not exceeded the maximum number of jumps, make the player jump
		if (
				Input.GetKeyDown(KeyCode.Space) &&
				(isOnGround || jumpCounter < 2)
		)
		{
			// Set the player to be in the air and reset its velocity
			isOnGround = false;
			rigidbody.velocity = Vector3.zero;

			// Add an upward force to the player to make it jump
			rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);

			// Play a sound effect for the jump
			audioSource.PlayOneShot(jumpSound, 1.0f);

			// Stop the dust particle effect
			dirtParticle.Stop();

			// If this is the first jump, play the running jump animation, otherwise play the jumping animation
			if (jumpCounter < 1)
				animator.Play("Running_Jump", -1, 0f);
			else
				animator.SetTrigger("Jump_trig");

			// Increment the jump counter
			jumpCounter++;
		}

		// If the player presses the D key and the player is on the ground, increase the game speed to double, and if they release the key, set the game speed back to normal
		if (Input.GetKeyDown(KeyCode.D) && isOnGround)
			GameManager.instance.gameSpeed = 2;
		else if (Input.GetKeyUp(KeyCode.D))
			GameManager.instance.gameSpeed = 1;
	}


	private void OnCollisionEnter(Collision collision)
	{
		// Check the tag of the collided object
		switch (collision.gameObject.tag)
		{
			// If the collided object is tagged as "Ground"
			case GROUND_TAG:
				// Set isOnGround to true
				isOnGround = true;
				// Reset the jump counter to zero
				jumpCounter = 0;
				// If the game is not over, play the dirt particle effect
				if (!GameManager.instance.isGameOver) dirtParticle.Play();
				break;

			// If the collided object is tagged as "Obstacle"
			case OBSTACLE_TAG:
				// Set the "Death_b" parameter to true in the animator
				animator.SetBool("Death_b", true);
				// Set the "DeathType_int" parameter to 1 in the animator
				animator.SetInteger("DeathType_int", 1);
				// Stop the dirt particle effect
				dirtParticle.Stop();
				// If the game is not over
				if (!GameManager.instance.isGameOver)
				{
					// Play the explosion particle effect
					explosionParticle.Play();
					// Play the crash sound effect
					audioSource.PlayOneShot(crashSound, 1.0f);
				}
				// Set the game over state to true in the GameManager
				GameManager.instance.SetIsGameOverTrue();
				// Log "Game Over!" to the console
				Debug.Log("Game Over!");
				// Log the final score to the console
				Debug.Log($"Your final score is {(int)GameManager.instance.score}");
				break;
		}
	}

}
