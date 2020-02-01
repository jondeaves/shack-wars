using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropoffController : MonoBehaviour
{
    public GameObject TargetPlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (TargetPlayer == null)
        {
            return;
        }

        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if (collision.gameObject == TargetPlayer && playerController.CurrentPickup != null)
        {
            // Score point to player
            collision.gameObject.GetComponent<PlayerController>().AddScore(1);

            playerController.CurrentPickup.transform.position = new Vector3(5f, 0f, 5f);
            playerController.CurrentPickup.GetComponent<PickupController>().SetTarget(null);

        }
    }
}
