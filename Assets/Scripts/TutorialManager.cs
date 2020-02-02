using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private int tutorialNumber = 1;

    public GameObject[] TutorialSteps;

    // Start is called before the first frame update
    void Start()
    {
        TutorialSteps[0].SetActive(true);
        TutorialSteps[1].SetActive(false);
        TutorialSteps[2].SetActive(false);
        TutorialSteps[3].SetActive(false);

        SetTutorialText();
    }

    // Update is called once per frame
    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        if (gamepad.startButton.wasPressedThisFrame && tutorialNumber > 4)
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }

    }

    public void Next()
    {
        if (TutorialSteps.Length >= (tutorialNumber))
        {
            TutorialSteps[tutorialNumber - 1].SetActive(false);
        }

        if (TutorialSteps.Length >= (tutorialNumber + 1))
        {
            TutorialSteps[tutorialNumber].SetActive(true);
        }

        tutorialNumber++;

        SetTutorialText();
    }

    private void SetTutorialText()
    {
        string msg = string.Format("Player {0} pick up the box and drop it off in the shack", tutorialNumber);

        if (tutorialNumber > TutorialSteps.Length)
        {
            msg = string.Format("Press start to begin");
        }

        GameObject.FindObjectOfType<Text>().text = msg;
    }
}
