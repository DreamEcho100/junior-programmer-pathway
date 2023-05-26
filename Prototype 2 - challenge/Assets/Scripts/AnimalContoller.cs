using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalContoller : MonoBehaviour
{
    public const string PLAYER_TAG = "Player";
    public const string FOOD_TAG = "Food";
    public const string ANIMAL_TAG = "Animal";

    [SerializeField]
    private GameObject HealthBarSlider;
    [SerializeField]
    private float maxAttackedCounter = 3f;

    private float attackedCounter = 0f;
    private float computedHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == FOOD_TAG)
        {

            Destroy(other.gameObject);

            attackedCounter++;

            if (attackedCounter == maxAttackedCounter)
            {
                Destroy(gameObject);
                GameManager.instance.IncrementPlayerScoreByOne();
            }
            else
            {
                computedHealth =
                    (
                        (maxAttackedCounter - attackedCounter)
                        / maxAttackedCounter
                    );

                HealthBarSlider.transform.localPosition = new Vector3(
                    (1 - computedHealth) / -2,
                    HealthBarSlider.transform.localPosition.y,
                    HealthBarSlider.transform.localPosition.z
                );
                HealthBarSlider.transform.localScale = new Vector3(
                    computedHealth,
                    HealthBarSlider.transform.localScale.y,
                    HealthBarSlider.transform.localScale.z
                );
            }
        }
        else if (other.gameObject.tag == PLAYER_TAG)
            GameManager.instance.DecrementPlayerLivesByOne();
    }

}
