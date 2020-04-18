using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerpEvent : MonoBehaviour
{
    MaleMovement maleMov;
    public GameObject MaleCharacter;
    MaleRestart maleReset;

    Quaternion AfterTutorialMale;
    Quaternion Init;

    public float speed = 0;
    public bool startLerp = false;


    //  The objective of this script is to Lerp camera 90º on y and Move to 0 the z position to show player the background
    void Start()
    {
        maleMov = MaleCharacter.GetComponent<MaleMovement>();
        maleReset = MaleCharacter.GetComponent<MaleRestart>();

        Init = transform.rotation;
        AfterTutorialMale = Quaternion.EulerAngles(0, 90, 0);
    }

    void Update()
    {
        if(!maleMov.inGame && !maleMov.goUp && !maleReset.cameraReset)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                startLerp = true;

            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                startLerp = false;

            }
        }
        

        if (startLerp)
            transform.rotation = Quaternion.Slerp(transform.rotation, AfterTutorialMale, speed * Time.deltaTime);      
        else      
            transform.rotation = Quaternion.Slerp(transform.rotation, Init, speed * Time.deltaTime);      
    }
}
