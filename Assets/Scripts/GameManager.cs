using System.Collections.Generic;
using UnityEngine;

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
    private GameObject winner;

    // Start is called before the first frame update
    void Start()
    {
        _state = GameState.running;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject player in players)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();

            if (_state == GameState.complete && winner != null)
            {
                PlayerController winnerController = this.winner.GetComponent<PlayerController>();
                Debug.Log(string.Format("Player {0} has {1}", playerController.PlayerNumber, playerController.PlayerNumber == winnerController.PlayerNumber ? "won" : "lost"));
            }
            else if (player.CompareTag("Player"))
            {
                if (playerController.Score >= WinScore)
                {
                    winner = player;
                    _state = GameState.complete;
                }
            }
        }
    }
}
