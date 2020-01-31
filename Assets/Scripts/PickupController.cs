﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public GameObject target;
    private float _targetTimeoutCounter = 0f;
    private float _targetTimeoutDuration = 0.3f;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            this.transform.position = target.transform.position + new Vector3(0, 0.7f, 0);
        }

        if (_targetTimeoutCounter > 0)
        {
            _targetTimeoutCounter -= Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (target == null)
        {
            target = collision.gameObject;
        }
    }

    public void SetTarget(GameObject _target)
    {
        if (_targetTimeoutCounter > 0)
        {
            return;
        }

        Debug.Log("Changing target");

        this.target = _target;

        _targetTimeoutCounter = _targetTimeoutDuration;
    }
}
