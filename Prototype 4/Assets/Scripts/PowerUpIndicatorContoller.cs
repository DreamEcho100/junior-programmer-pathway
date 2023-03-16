using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpIndicatorContoller : MonoBehaviour
{
	private GameObject player;

	[SerializeField]
	private Vector3 offset = new Vector3(0, -0.5f, 0);
	[SerializeField]
	private float rotationSpeed = 100.0f;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.Find("Player");
	}

	// Update is called once per frame
	void Update()
	{
		if (!transform.gameObject.activeInHierarchy) return;

		transform.position = player.transform.position + offset;
		transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
	}
}
