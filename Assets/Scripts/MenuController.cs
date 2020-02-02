using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        if (gamepad.startButton.wasPressedThisFrame)
        {
            SceneManager.LoadScene("TutorialScene", LoadSceneMode.Single);
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            GameSetup.PlayerCount = 2;
            GameObject.FindGameObjectWithTag("playercount").GetComponent<Text>().text = "2 Players";
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            GameSetup.PlayerCount = 3;

            GameObject.FindGameObjectWithTag("playercount").GetComponent<Text>().text = "3 Players";
        }
        else if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            GameSetup.PlayerCount = 4;

            GameObject.FindGameObjectWithTag("playercount").GetComponent<Text>().text = "4 Players";
        }
    }
}
