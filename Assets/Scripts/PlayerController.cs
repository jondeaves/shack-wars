using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    public int PlayerNumber = 1;
    public float Speed = 6.0f;
    public float InputDeadzone = 0.1f;

    public int Score { get; private set; }

    public GameObject CurrentPickup
    {
        get
        {
            GameObject _pickup = null;

            foreach (GameObject pickup in GameObject.FindGameObjectsWithTag("Pickup"))
            {
                if (pickup.GetComponent<PickupController>().target == this.gameObject)
                {
                    _pickup = pickup;
                }
            }

            return _pickup;
        }
    }

    private void Start()
    {
        Score = 0;
    }

    void FixedUpdate()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        // Depending on player number
        StickControl controller = PlayerNumber == 1 ? gamepad.leftStick : gamepad.rightStick;

        Vector2 moveVector = controller.ReadValue();

        // Check deadzones
        Vector3 translationVector = new Vector3(
            moveVector.x >= InputDeadzone || moveVector.x <= -InputDeadzone ? moveVector.x : 0,
            0,
            moveVector.y >= InputDeadzone || moveVector.y <= -InputDeadzone ? moveVector.y : 0
        ) * Speed * Time.fixedDeltaTime;

        // Make the moves, cut the shapes
        transform.Translate(translationVector);
    }

    public void AddScore(int points)
    {
        Score += points;

        Debug.Log(string.Format("Adding point for player {0}: {1}", PlayerNumber, Score));
    }

    void OnCollisionEnter(Collision collision)
    {
        // Stealing
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController otherPlayerController = collision.gameObject.GetComponent<PlayerController>();

            if (otherPlayerController.CurrentPickup != null)
            {
                otherPlayerController.CurrentPickup.GetComponent<PickupController>().SetTarget(this.gameObject);
            }

        }
    }
}
