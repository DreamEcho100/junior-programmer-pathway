using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [SerializeField]
    private GameObject dogPrefab;

    [SerializeField]
    private float CanSentDogIntyervalTime = 0.2f;

    private bool CanSentDog;

    private void Start()
    {
        CanSentDog = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanSentDog) return;

        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            CanSentDog = false;
            Invoke(nameof(SetCanSentDogTrue), CanSentDogIntyervalTime);
        }
    }

    private void SetCanSentDogTrue()
    {
        CanSentDog = true;
    }
}
