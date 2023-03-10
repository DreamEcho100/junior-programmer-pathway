using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;

    [SerializeField]
    private float
        speed = 21.0f,
        xRange = 21.0f,
        zMinRange = 0,
        zMaxRange = 7.5f;
    [SerializeField]
    private GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }

        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {

            if (Mathf.Abs(transform.position.x) > xRange)
            {
                // If the player on the lefts side on -18 >> (-18/Mathf.Abs(-18)) >> (-18/18) >> -1
                // If the player on the lefts side on 18 >> (18/Mathf.Abs(18)) >> (18/18) >> 1
                float currentDir = (transform.position.x / Mathf.Abs(transform.position.x));
                // Reverse the currentDir and appear on the other side
                transform.position = new Vector3(xRange * currentDir * -1, 0, 0);
                // A return for not using an else statement after it
                return;
            }

            transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);
        }

        verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0)
        {
            if (
                (verticalInput > 0 && transform.position.z <= zMaxRange) ||
                (verticalInput < 0 && transform.position.z >= zMinRange)
            )
                transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        }
    }
}

/*

if (horizontalInput == 0) return;

if (Mathf.Abs(transform.position.x) > xRange)
{
    if (horizontalInput <= 0)
    {
        transform.position = new Vector3(xRange, 0, 0);
    }
    else if ((horizontalInput >= 0))
    {
        transform.position = new Vector3(-xRange, 0, 0);
    }
}
*/

//if (
//    (horizontalInput <= 0 && transform.position.x < -xRange) ||
//    (horizontalInput >= 0 && transform.position.x > xRange)
//) return;

//if (Mathf.Abs(transform.position.x) > 10)

//if (transform.position.x < -xRange)
//    transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
//if (transform.position.x > xRange)
//    transform.position = new Vector3(xRange, transform.position.y, transform.position.z);