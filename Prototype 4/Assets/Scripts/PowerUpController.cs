using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
	private const string RebelEnemiesPowerUp = "Rebel Enemies Power Up";

	private const string BulletShooterPowerUp = "Bullet Shooter Power Up";
	public string currentPowerUpName;

	[SerializeField]
	private float rotationSpeed = 100.0f;

	// Update is called once per frame
	void Update()
	{
		transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
	}
}
