using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

	// private new Rigidbody rigidbody;
	private const string PLAYER_TAG = "Player";

	[SerializeField]
	private float
		speed = 35,
		startDelay = 2,
		repeatRate = 2;

	// Start is called before the first frame update
	void Start()
	{
		// rigidbody = transform.GetComponent<Rigidbody>();
		// playerController = GameObject.Find(PLAYER_TAG).GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.instance.isGameOver) return;

		transform.Translate(Vector3.left * Time.deltaTime * speed);
		// rigidbody.velocity = Vector3.left * speed;
	}
}
