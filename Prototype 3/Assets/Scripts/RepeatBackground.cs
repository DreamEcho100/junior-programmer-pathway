using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
	private Vector3 startPos;
	private float midPoint;

	[SerializeField]
	private int RepeatedBgWidthCount = 2;

	// Start is called before the first frame update
	void Start()
	{
		startPos = transform.position;
		// (float)(GetComponent<Renderer>().bounds.size.x)
		// GetComponent<BoxCollider>().size.x
		midPoint = GetComponent<SpriteRenderer>().bounds.size.x / RepeatedBgWidthCount;
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position.x <= startPos.x - midPoint)
		{
			transform.position = startPos;
		}
	}
}
