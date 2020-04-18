using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleMovement : MonoBehaviour
{
    public float speed = 0;
    public float dodge_offset = 0;

    void Start()
    {

    }

    void Update()
    {
        //Go up
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        //Simple Dodge to test
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(Vector3.right * -dodge_offset);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(Vector3.right * dodge_offset);

        }

    }
}
