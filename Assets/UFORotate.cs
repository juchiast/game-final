using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UFORotate : MonoBehaviour
{
    public float speed = 0.1f;
    void Update()
    {
        transform.RotateAround(Vector3.up, speed);
    }

    private void OnCollisionEnter(Collision other)
    {
        var body = GetComponent<Rigidbody>();
        body.useGravity = true;
        var ang = Vector3.up * 10;
        ang.z = 1;
        body.angularVelocity = ang; 
        
        var player = GetComponentInParent<PlayerControl>();
        if (player != null)
        {
            Destroy(player);
            body.velocity = Vector3.forward * 20;
            GameObject.Find("SceneScripts").GetComponent<SceneStep>().DelayedAddRestartMenu();
        }
        else
        {
            body.velocity = Vector3.back * 20;
        }

        Destroy(this);
    }
}
