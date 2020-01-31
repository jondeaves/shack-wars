using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropoffController : MonoBehaviour
{
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
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();


            if (playerController.CurrentPickup != null)
            {
                // Score point to player
                collision.gameObject.GetComponent<PlayerController>().AddScore(1);

                playerController.CurrentPickup.transform.position = new Vector3(-0.77f, 0.68f, -1.08f);
                playerController.CurrentPickup.GetComponent<PickupController>().target = null;
            }

        }
    }
}
