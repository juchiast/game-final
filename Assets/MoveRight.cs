using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float speed = 0.15f;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 4)
        {
            transform.position += Vector3.right * speed;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        GetComponent<Rigidbody>().useGravity = true;
        Destroy(this);
    }
}
