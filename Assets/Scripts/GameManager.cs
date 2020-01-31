using System.Collections;
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

        foreach (GameObject player in players)
        {
            if (player.CompareTag("Player"))
            {
                if (player.GetComponent<PlayerController>().Score >= WinScore)
                {
                    Debug.Log(string.Format("Player {0} has won", player.GetComponent<PlayerController>().PlayerNumber));
                    _state = GameState.complete;
                }
            }
        }
    }
}
