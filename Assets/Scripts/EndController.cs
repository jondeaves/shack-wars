using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndController : MonoBehaviour
{
    private void Start()
    {
        GameObject.FindGameObjectWithTag("Delay").GetComponent<AudioSource>().PlayDelayed(1f);

        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = GetLabelText(GameStats.Winner, false);

        List<int> loserScores = new List<int>();

        for (int iPlayer = 0; iPlayer < GameStats.playerScores.Count; iPlayer += 1)
        {
            int playerNumber = iPlayer + 1;

            if (playerNumber != GameStats.Winner)
            {
                loserScores.Add(playerNumber);
                Debug.Log(string.Format("Player {0} did not win", playerNumber));
            }
        }


        GameObject[] loserLabels = GameObject.FindGameObjectsWithTag("LoserText");

        for (int iLabel = 0; iLabel < loserLabels.Length; iLabel += 1)
        {
            GameObject loserTextLabel = loserLabels[iLabel];

            if (loserScores.Count <= iLabel)
            {
                // No score
                loserTextLabel.SetActive(false);
            }
            else
            {
                loserTextLabel.SetActive(true);
                loserTextLabel.GetComponent<Text>().text = GetLabelText(loserScores[iLabel]);
            }
        }

        GameStats.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        AudioSource fadeInSource = GameObject.FindGameObjectWithTag("FadeIn").GetComponent<AudioSource>();
        if (fadeInSource.volume < 0.6f)
        {
            fadeInSource.volume += 0.1f * Time.deltaTime;
        }


        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        if (gamepad.startButton.wasPressedThisFrame)
        {
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        }
    }

    private string GetLabelText(int playerNumber, bool showScore = true)
    {
        string template = showScore ? "Player {0}: {1}/{2}" : "Player {0}";

        return string.Format(template, playerNumber, GameStats.playerScores[playerNumber - 1], GameStats.WinScore);
    }
}
