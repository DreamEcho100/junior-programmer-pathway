using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject Player;

    private int _score = 0;
    private int _lives = 3;

    public int score
    {
        get { return _score; }
    }
    public int lives
    {
        get { return _lives; }
    }

    public void IncrementPlayerScoreByOne()
    {
        _score++;
    }
    public void DecrementPlayerLivesByOne()
    {
        if (_lives != 0)
            _lives--;

        if (lives == 0)
        {
            Destroy(Player.gameObject);
            Debug.Log("Game Over!");
        }
        else
        {
            Debug.Log($"Player lives: {lives}");
        }
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
