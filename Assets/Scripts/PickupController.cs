using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            this.transform.position = target.transform.position + new Vector3(0, 0.7f, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (target == null)
        {
            target = collision.gameObject;
        }
    }
}
