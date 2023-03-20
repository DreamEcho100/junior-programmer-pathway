using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private int collidedEnemiesCounter = 0;
	private GameObject focalPoint;

	private new Rigidbody rigidbody;
	private float verticalInput;
	private bool isOnGround = true;

	private const string POWER_UP_TAG = "PowerUp";
	private const string ENEMY_TAG = "Enemy";
	private const string GROUND_TAG = "Ground";

	private const string RebelEnemiesPowerUp = "Rebel Enemies Power Up";
	private const string BulletShooterPowerUp = "Bullet Shooter Power Up";
	private const string SmashJumpPowerUp = "Smash Jump Power Up";


	[SerializeField]
	private string currentPowerUp;

	[SerializeField]
	private GameObject powerUpIndicator, bullet;
	[SerializeField]
	private float
		speed = 5.0f,
		powerUpStrength = 15.0f;

	[SerializeField]
	private bool hasPowerUp;

	// Start is called before the first frame update
	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		focalPoint = GameObject.Find("Focal Point");
		// powerUpIndicator = GameObject.Find("PowerUpIndicator");
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (
			GameManager.instance.enemiesSpawnedCounter > 3 &&
			collidedEnemiesCounter == GameManager.instance.enemiesSpawnedCounter &&
			!hasPowerUp
		)
		{
			Debug.Log("Game Over!");
			return;
		}

		verticalInput = Input.GetAxis("Vertical");

		if (hasPowerUp && Input.GetKey(KeyCode.Space))
		{
			switch (currentPowerUp)
			{
				case BulletShooterPowerUp:
					ShootEnemiesWithBullets();
					break;
				case SmashJumpPowerUp:
					if (!isOnGround) break;
					isOnGround = false;
					rigidbody.velocity = Vector3.zero;
					rigidbody.AddForce(Vector3.up * 600);
					break;
			}
		}

		// FixedUpdate is framerate independent and is used for consistent
		// physics regardless of computer speed.
		//
		// Don't multiply the force by Time.deltaTime: AddForce already applies
		// the force during a single physics cycle in its default mode
		// (ForceMode.Force), thus multiplying it by Time.deltaTime will actually
		// result in a force 50 times smaller (there are 50 physics cycles per
		// second, thus Time.deltaTime is 1/50)
		if (verticalInput != 0) rigidbody.AddForce(focalPoint.transform.forward * speed * verticalInput);
	}

	void Update()
	{
		if (transform.position.y < -10)
		{
			Destroy(gameObject);
			powerUpIndicator.SetActive(false);
			Debug.Log("Game Over!");
		}
	}

	void OnCollisionEnter(Collision other)
	{

		switch (other.gameObject.tag)
		{

			case ENEMY_TAG:
				collidedEnemiesCounter++;
				if (!hasPowerUp || currentPowerUp != RebelEnemiesPowerUp) break;

				Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
				Vector3 awayFromPlayer = (other.gameObject.transform.position - transform.position);

				enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);

				break;

			case GROUND_TAG:
				if (currentPowerUp == SmashJumpPowerUp && !isOnGround)
				{
					rigidbody.velocity = Vector3.zero;
					EnemyController[] enemies = FindObjectsOfType<EnemyController>();
					foreach (EnemyController enemy in enemies)
					{
						Vector3 lookToEnemy = (enemy.transform.position - transform.position);
						if (
							Mathf.Abs(lookToEnemy.x) < 3 ||
							Mathf.Abs(lookToEnemy.z) < 3
						)
						{
							enemy.GetComponent<Rigidbody>().AddForce(lookToEnemy.normalized * 800);
						}
					}
					isOnGround = true;
				}
				break;
		}
	}

	void ShootEnemiesWithBullets()
	{
		if (
			GameManager.instance.bulletsSpawned >= GameManager.instance.enemiesSpawnedCounter * 10
		) return;

		EnemyController[] enemies = FindObjectsOfType<EnemyController>();

		foreach (EnemyController enemy in enemies)
		{
			Vector3 lookToEnemy = enemy.transform.position - transform.position;
			Vector3 starPos = transform.position + lookToEnemy.normalized + new Vector3(0, 1, 0);

			Quaternion rotate = Quaternion.LookRotation(lookToEnemy, Vector3.up) * Quaternion.Euler(90, 0, 0);

			// Create bullet and send enemy gameObject in bullet as a target
			GameObject bulletInstantiate = Instantiate(bullet, starPos, rotate);
			bulletInstantiate.GetComponent<BulletController>().target = enemy.gameObject;
			bullet.GetComponent<Rigidbody>().velocity =
				transform.position.normalized *
				bulletInstantiate.GetComponent<BulletController>().speed;
			GameManager.instance.bulletsSpawned++;

		}
	}

	void OnCollisionExit(Collision other)
	{
		switch (other.gameObject.tag)
		{

			case ENEMY_TAG:
				collidedEnemiesCounter--;
				break;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		switch (other.tag)
		{
			case POWER_UP_TAG:
				hasPowerUp = true;
				currentPowerUp = other.gameObject.GetComponent<PowerUpController>().currentPowerUpName;
				powerUpIndicator.SetActive(true);
				Destroy(other.gameObject);
				GameManager.instance.powerUpsSpawnedCounter--;
				StartCoroutine(nameof(PowerUpCountdownRoutine));
				break;
		}
	}

	IEnumerator PowerUpCountdownRoutine()
	{
		yield return new WaitForSeconds(7.0f);
		hasPowerUp = false;
		currentPowerUp = null;
		powerUpIndicator.SetActive(false);
	}


}
