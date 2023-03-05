using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    //private float fire3Input;
    private int offsetsIndex = 0;
    private readonly Vector3[] offests = { new Vector3(0, 6, -10), new Vector3(0, 3, 1) };

    [SerializeField]
    private Vector3 offest;


    // Start is called before the first frame update
    void Start()
    {
        offest = offests[offsetsIndex];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (offsetsIndex + 1 > offests.Length - 1) offsetsIndex = 0;
            else offsetsIndex++;

            offest = offests[offsetsIndex];
        }

        transform.position = player.transform.position + offest;
    }
}
