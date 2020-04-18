using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerpEvent : MonoBehaviour
{
    //public GameObject MaleCamera;
    Quaternion AfterTutorialMale;
    Quaternion Init;
    public float speed = 0;
    bool startLerp = false;

    //  The objective of this script is to Lerp camera 90º on y and Move to 0 the z position to show player the background
    void Start()
    {
        Init = transform.rotation;
        AfterTutorialMale = Quaternion.EulerAngles(0, 90, 0);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            startLerp = true;

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            startLerp = false;

        }



        if (startLerp)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, AfterTutorialMale, speed * Time.deltaTime);
            //if (transform.rotation.eulerAngles.y <= 120)
            //    startLerp = false;
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Init, speed * Time.deltaTime);
        }
    }
}
