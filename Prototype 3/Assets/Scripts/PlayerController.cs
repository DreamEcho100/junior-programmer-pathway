using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private new Rigidbody rigidbody;
	private Animator animator;
	private AudioSource audioSource, mainCameraAudioSource;

	private bool isOnGround = true;
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

	// Start is called before the first frame update
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();

		mainCameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

		Physics.gravity *= GravityModifier;
	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.instance.isGameOver && mainCameraAudioSource.isPlaying)
		{
			float volumeDown = (Time.deltaTime * GameManager.instance.volumeDownRate);
			if (mainCameraAudioSource.volume - volumeDown > 0)
			{
				mainCameraAudioSource.volume = mainCameraAudioSource.volume - volumeDown;
			}
			else
			{
				mainCameraAudioSource.Stop();
			}
			return;
		}

		if (
			Input.GetKeyDown(KeyCode.Space) &&
			isOnGround
		)
		{
			isOnGround = false;
			rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
			audioSource.PlayOneShot(jumpSound, 1.0f);
			dirtParticle.Stop();
			animator.SetTrigger("Jump_trig");
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		// if (collision.gameObject.CompareTag(GROUND_TAG))
		// 	isOnGround = true;

		switch (collision.gameObject.tag)
		{
			case GROUND_TAG:
				isOnGround = true;
				if (!GameManager.instance.isGameOver) dirtParticle.Play();
				break;
			case OBSTACLE_TAG:
				animator.SetBool("Death_b", true);
				animator.SetInteger("DeathType_int", 1);
				dirtParticle.Stop();
				if (!GameManager.instance.isGameOver)
				{
					explosionParticle.Play();
					audioSource.PlayOneShot(crashSound, 1.0f);
				}
				GameManager.instance.SetIsGameOverTrue();
				Debug.Log("Game Over!");
				break;
		}
	}
}
