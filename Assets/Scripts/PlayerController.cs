using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int PlayerNumber = 1;
    public float speed = 10.0f;

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

    // Update is called once per frame
    void Update()
    {
        // Find out direction vector
        Vector3 translationVector = new Vector3(
            Input.GetAxis(string.Format("Horizontal {0}", PlayerNumber)),
            0,
            Input.GetAxis(string.Format("Vertical {0}", PlayerNumber))
        );

        translationVector.Normalize();
        translationVector = translationVector * speed * Time.deltaTime;

        // Make the moves
        transform.Translate(translationVector);
    }

    public void AddScore(int points)
    {
        Score += points;

        Debug.Log(string.Format("Adding point for player {0}: {1}", PlayerNumber, Score));
    }
}
