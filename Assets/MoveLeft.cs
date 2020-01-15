using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 0.15f;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > -4)
        {
            transform.position += Vector3.left * speed;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        GetComponent<Rigidbody>().useGravity = true;
        Destroy(this);
    }
}
