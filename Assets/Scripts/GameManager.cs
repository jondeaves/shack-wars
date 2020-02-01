﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    running,
    complete
}

public class GameManager : MonoBehaviour
{
    public int WinScore = 10;
    public List<GameObject> players = new List<GameObject>();
    private GameState _state;
    public GameObject Winner { get; private set; }

    // Make global
    public static GameManager Instance
    {
        get;
        set;
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _state = GameState.running;
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == GameState.complete)
        {
            return;
        }

        for (int iPlayer = 0; iPlayer < players.Count; iPlayer += 1)
        {
            GameObject player = players[iPlayer];
            PlayerController playerController = player.GetComponent<PlayerController>();

            if (playerController.Score >= WinScore)
            {
                Winner = player;
                _state = GameState.complete;
                SetEndData();
                SceneManager.LoadSceneAsync("EndScene", LoadSceneMode.Single);
            }

        }
    }

    private void SetEndData()
    {

        for (int iPlayer = 0; iPlayer < players.Count; iPlayer += 1)
        {
            PlayerController playerController = players[iPlayer].GetComponent<PlayerController>();

            GameStats.WinScore = WinScore;
            GameStats.playerScores.Add(playerController.Score);

            if (playerController.Score >= WinScore)
            {
                // Set static stats
                GameStats.Winner = playerController.PlayerNumber;
            }

        }
    }
}
