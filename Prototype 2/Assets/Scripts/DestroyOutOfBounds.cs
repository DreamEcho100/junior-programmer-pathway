using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField]
    private float topBound = 30, lowerBound = -10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > topBound || transform.position.z < lowerBound)
        {
            Destroy(gameObject);
        }
    }
}
