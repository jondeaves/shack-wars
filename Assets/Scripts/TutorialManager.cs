using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private int tutorialNumber = 1;
    private bool isComplete = false;

    public GameObject[] TutorialSteps;

    // Start is called before the first frame update
    void Start()
    {
        if (GameSetup.PlayerCount == 1)
        {
            GameSetup.PlayerCount = 2;
        }
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

        if (isComplete)
        {
            TutorialSteps[0].SetActive(false);
            TutorialSteps[1].SetActive(false);
            TutorialSteps[2].SetActive(false);
            TutorialSteps[3].SetActive(false);
        }

        if (gamepad.startButton.wasPressedThisFrame && isComplete)
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

        if (tutorialNumber > GameSetup.PlayerCount)
        {
            isComplete = true;
            msg = string.Format("Press start to begin");
        }

        GameObject.FindObjectOfType<Text>().text = msg;
    }
}
