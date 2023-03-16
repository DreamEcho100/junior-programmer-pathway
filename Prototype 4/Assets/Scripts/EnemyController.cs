using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private new Rigidbody rigidbody;
	private GameObject player;

	private Vector3 lookDirection;

	[SerializeField]
	private float speed = 3.0f;

	// Start is called before the first frame update
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		player = GameObject.Find("Player");
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position.y < -10)
		{
			Destroy(gameObject);
			GameManager.instance.enemiesSpawnedCounter--;
		}
		if (!player) return;
		// If you are confused about normalize, all it does it "resize" your
		// vector values (x,y,z) to be less than or equal to 1, while maintaining
		// the proportion.
		//
		// So your vector essentially becomes just a pointer instead of directly
		// reaching out to the player and touching it.
		//
		// Small additional detail: The vector is normalized to have a length of 1,
		// where the lengths is sqrt(x^2+y^2+z^2). You can access the length by
		// calling "magnitude" on your vector to check this.
		//
		// It may help, or confuse, people to know that normalization of a vector
		// turns the vector into a unit vector. A unit vector is simply a vector with
		// magnitude (length) one.
		//
		// By normalizing a vector you are effectively eliminating the magnitude or
		// distance between two points and keeping only the directional aspect of
		// the vector.
		lookDirection = (player.transform.position - transform.position).normalized;

		rigidbody.AddForce(
			lookDirection * speed
		);
	}
}
