using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    public int PlayerNumber = 1;
    public float Speed = 6.0f;
    public float RotateSpeed = 20.0f;
    public float InputDeadzone = 0.1f;

    public int Score { get; private set; }


    public GameObject Roof;
    public GameObject Door;
    public GameObject Wall1;
    public GameObject Wall2;
    public GameObject Wall3;

    public GameObject CurrentPickup
    {
        get
        {
            GameObject _pickup = null;

            foreach (GameObject pickup in GameObject.FindGameObjectsWithTag("Pickup"))
            {
                if (pickup.GetComponent<PickupController>().target)
                {
                    int number1 = pickup.GetComponent<PickupController>().target.GetComponent<PlayerController>().PlayerNumber;
                    int number2 = this.GetComponent<PlayerController>().PlayerNumber;
                }
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
        // Cheaply hacking gravity until we have bounds
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        // Depending on player number
        Vector2 facingVector;
        switch (PlayerNumber)
        {
            case 1:
                facingVector = Gamepad.all.Count > 0 ? Gamepad.all[0].leftStick.ReadValue() : Vector2.zero;
                break;
            case 2:
                facingVector = Gamepad.all.Count > 0 ? Gamepad.all[0].rightStick.ReadValue() : Vector2.zero;
                break;
            case 3:
                facingVector = Gamepad.all.Count > 1 ? Gamepad.all[1].leftStick.ReadValue() : Vector2.zero;
                break;
            case 4:
                facingVector = Gamepad.all.Count > 1 ? Gamepad.all[1].rightStick.ReadValue() : Vector2.zero;
                break;
            default:
                facingVector = Vector2.zero;
                break;

        }



        // Check deadzones
        if (facingVector.x >= InputDeadzone || facingVector.x <= -InputDeadzone || facingVector.y >= InputDeadzone || facingVector.y <= -InputDeadzone)
        {


            // Turn to face the input
            // ---
            Vector3 targetDirection = (transform.position + new Vector3(facingVector.x, 0, facingVector.y)) - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = RotateSpeed * Time.fixedDeltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);


            // Move in forward vector
            // --
            //transform.Translate(Vector3.forward * Speed * Time.fixedDeltaTime);
            Vector3 translationVector = new Vector3(
                facingVector.x,
                0,
                facingVector.y
            ) * Speed * Time.fixedDeltaTime;

            // Make the moves, cut the shapes
            //transform.Translate(translationVector);
            transform.position = transform.position + translationVector;
        }
    }

    public void AddScore()
    {
        if (FindObjectOfType<GameManager>())
        {
            FindObjectOfType<GameManager>().PlaySfx(0);

            Score += GameObject.FindObjectOfType<GameManager>().PointsPerBlock;
            TriggerShackEnhancement();
        }
        else if (FindObjectOfType<TutorialManager>())
        {
            FindObjectOfType<TutorialManager>().Next();
        }
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

    void TriggerShackEnhancement()
    {
        if (Score >= 10 && Wall3)
        {
            Wall3.SetActive(true);
        }
        else if (Score >= 8 && Wall2)
        {
            Wall2.SetActive(true);
        }
        else if (Score >= 6 && Wall1)
        {
            Wall1.SetActive(true);
        }
        else if (Score >= 4 && Door)
        {
            Door.SetActive(true);
        }
        else if (Score >= 2 && Roof)
        {
            Roof.SetActive(true);
        }
    }
}
