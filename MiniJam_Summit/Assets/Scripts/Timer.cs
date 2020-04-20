using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timer = 5f;
    float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - time >= timer)
        {
            Destroy(gameObject);
        }
    }
}
