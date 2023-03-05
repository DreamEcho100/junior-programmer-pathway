using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 15.0f, turnSpeed = 60.0f, turnSpeedTrimmed;

    [SerializeField]
    private int playerNumber;

    private Dictionary<int, List<KeyCode>> keyMap = new()
    {
        {
            1,
            new List<KeyCode>() {
                KeyCode.W,
                KeyCode.D,
                KeyCode.S,
                KeyCode.A,
            }
        },
        {
            2,
            new List<KeyCode>() {
                KeyCode.UpArrow,
                KeyCode.RightArrow,
                KeyCode.DownArrow,
                KeyCode.LeftArrow,
            }
        },
    };
    private List<KeyCode> playerKeys;
    private float horizontalInput, forwardInput;

    // Start is called before the first frame update
    void Start()
    {
        playerKeys = keyMap[playerNumber];
    }

    // Update is called once per frame
    void Update()
    {
        var upKey = playerKeys[0];
        var downKey = playerKeys[2];
        forwardInput = Input.GetKey(upKey) ? 1 : Input.GetKey(downKey) ? -1 : 0;

        if (forwardInput != 0)
        {
            transform.Translate(
                Vector3.forward * Time.deltaTime * speed * forwardInput
            );
        }

        var rightKey = playerKeys[1];
        var leftKey = playerKeys[3];
        horizontalInput = Input.GetKey(rightKey) ? 1 : Input.GetKey(leftKey) ? -1 : 0;

        if (horizontalInput != 0)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
        }
    }
}
