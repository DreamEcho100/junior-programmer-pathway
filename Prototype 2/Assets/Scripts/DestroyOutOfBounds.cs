using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public const string PLAYER_TAG = "Player";
    public const string FOOD_TAG = "Food";
    public const string ANIMAL_TAG = "Animal";

    [SerializeField]
    private float
        topBound = 30,
        lowerBound = -10,
        leftBound = -24,
        rightBound = 24;

    // Update is called once per frame
    void Update()
    {
        if (
            transform.position.z > topBound ||
            transform.position.x > rightBound ||
            transform.position.x < leftBound ||
            transform.position.z < lowerBound
        )
        {
            if (gameObject.CompareTag(FOOD_TAG)) GameManager.instance.DecrementPlayerLivesByOne();
            Destroy(gameObject);
        }
    }
}
