using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	private const string ENEMY_TAG = "Enemy";

	public GameObject target;
	public float speed = 10.0f;
	public float speedRot = 10.0f;
	// public Transform playerTransform;

	void Start()
	{
		// playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		//If the target is gone, destroy the bullet
		if (!target) { DestroyGameObject(); return; }
		RotateTowardsTarget();
	}

	private void RotateTowardsTarget()
	{
		//Turn and move the bullet toward the target
		Vector3 lookDirection = target.transform.position - transform.position;

		Quaternion rotateTarget =
				Quaternion.LookRotation(lookDirection) *
				Quaternion.Euler(90.0f, 0, 0);

		Quaternion rotate = Quaternion.RotateTowards(
				transform.rotation,
				rotateTarget,
				speedRot
		);

		transform.rotation = rotate;
	}

	void FixedUpdate()
	{
		//If the target is gone, destroy the bullet
		if (!target) { DestroyGameObject(); return; }
		RotateTowardsTarget();
		transform.Translate(Vector3.up * Time.deltaTime * speed);
	}

	void OnCollisionEnter(Collision other)
	{
		switch (other.gameObject.tag)
		{
			case ENEMY_TAG:
				StartCoroutine(nameof(DestroyAfterTime));
				break;
		}
	}

	private IEnumerator DestroyAfterTime()
	{
		yield return new WaitForSeconds(0.1f);
		DestroyGameObject();
	}

	void DestroyGameObject()
	{
		GameManager.instance.bulletsSpawned--;
		Destroy(gameObject);
	}
}
