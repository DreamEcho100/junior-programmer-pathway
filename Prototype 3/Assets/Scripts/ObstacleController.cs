using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
	[SerializeField]
	private float leftBound = -15;

	// Update is called once per frame
	void Update()
	{
		if (transform.position.x < leftBound) Destroy(gameObject);
	}
}
