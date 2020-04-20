using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerpEvent : MonoBehaviour
{
    public GameObject maleCamera;
    public GameObject femaleCamera;

    MaleMovement maleMov;
    public GameObject MaleCharacter;

    FemaleMovement femaleMov;
    public GameObject FemaleCharacter;

    MaleRestart maleReset;

    Quaternion AfterTutorialMale;
    Quaternion AfterTutorialFemale;
    Quaternion InitMale;
    Quaternion InitFemale;

    public float speed = 0;
    public bool startMaleLerp = false;
    public bool startFemaleLerp = false;

    bool start = true;

    //  The objective of this script is to Lerp camera 90º on y and Move to 0 the z position to show player the background
    void Start()
    {
        maleMov = MaleCharacter.GetComponent<MaleMovement>();
        maleReset = MaleCharacter.GetComponent<MaleRestart>();

        femaleMov = FemaleCharacter.GetComponent<FemaleMovement>();
    

        InitMale = maleCamera.transform.rotation;
        AfterTutorialMale = Quaternion.EulerAngles(0, 90, 0);    

        InitFemale = femaleCamera.transform.rotation;
        AfterTutorialFemale = Quaternion.EulerAngles(0, -90, 0);


    }

    void Update()
    {

        if (start || !maleMov.inGame && !maleMov.goUp && !maleReset.cameraReset && !femaleMov.inGame && !femaleMov.goUp)
        {
            if (start || Input.GetKeyDown(KeyCode.A) && !maleMov.isPrepared)
            {
                startMaleLerp = true;

            }
            if (Input.GetKeyDown(KeyCode.D) && !maleMov.isPrepared)
            {
                startMaleLerp = false;

            }

            if(start || Input.GetKeyDown(KeyCode.RightArrow) && !femaleMov.isPrepared)
            {
                startFemaleLerp = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !femaleMov.isPrepared)
            {
                startFemaleLerp = false;
            }
            start = false;
        }
        

        if (startMaleLerp)
            maleCamera.transform.rotation = Quaternion.Slerp(maleCamera.transform.rotation, AfterTutorialMale, speed * Time.deltaTime);      
        else
            maleCamera.transform.rotation = Quaternion.Slerp(maleCamera.transform.rotation, InitMale, speed * Time.deltaTime);

        if (startFemaleLerp)
            femaleCamera.transform.rotation = Quaternion.Slerp(femaleCamera.transform.rotation, AfterTutorialFemale, speed * Time.deltaTime);
        else
            femaleCamera.transform.rotation = Quaternion.Slerp(femaleCamera.transform.rotation, InitFemale, speed * Time.deltaTime);
    }
}
