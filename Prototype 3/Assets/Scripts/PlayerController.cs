using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private new Rigidbody rigidbody;
	private Animator animator;

	private bool
		isOnGround = true;
	private const string
		GROUND_TAG = "Ground",
		OBSTACLE_TAG = "Obstacle";

	[SerializeField]
	private float
	JumpForce = 13f,
	GravityModifier = 2.5f;

	// Start is called before the first frame update
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();

		Physics.gravity *= GravityModifier;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !GameManager.instance.isGameOver)
		{
			rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
			isOnGround = false;
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
				break;
			case OBSTACLE_TAG:
				GameManager.instance.SetIsGameOverTrue();
				animator.SetBool("Death_b", true);
				animator.SetInteger("DeathType_int", 1);
				Debug.Log("Game Over!");
				break;
		}
	}
}
