using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }
    }
}
