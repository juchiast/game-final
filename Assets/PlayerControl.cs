using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Camera camera;

    private GameObject ufo;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        ufo = GameObject.Find("UFO");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            gameObject.AddComponent<MoveLeft>();
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            Destroy(gameObject.GetComponent<MoveLeft>());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            gameObject.AddComponent<MoveRight>();
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            Destroy(gameObject.GetComponent<MoveRight>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this);
    }
}
