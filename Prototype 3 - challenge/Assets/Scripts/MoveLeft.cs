using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

	// private new Rigidbody rigidbody;
	private const string PLAYER_TAG = "Player";

	[SerializeField]
	private float speed = 35;

	// Start is called before the first frame update
	void Start()
	{
		// rigidbody = transform.GetComponent<Rigidbody>();
		// playerController = GameObject.Find(PLAYER_TAG).GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.instance.isGameOver || !GameManager.instance.isGameStarting) return;

		transform.Translate(Vector3.left * Time.deltaTime * speed * GameManager.instance.gameSpeed);
		// rigidbody.velocity = Vector3.left * speed;
	}
}
