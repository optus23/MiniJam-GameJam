using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleMovement : MonoBehaviour
{
    CameraLerpEvent lerpEvent;
    public GameObject CameraManager;
    public GameObject FemaleCamera;

    public GameObject MaleCharacter;
    MaleMovement maleMov;

    FemaleRestart resetGame;

    public float speed = 0;
    public float dodge_offset = 0;
    int dodge_counter = 0;

    public bool goUp = false;
    public bool inGame = false;
    public bool isPrepared = false;

    Animator anim;

    float startAttackTime;


    void Start()
    {
        maleMov = MaleCharacter.GetComponent<MaleMovement>();
        lerpEvent = CameraManager.GetComponent<CameraLerpEvent>();
        resetGame = GetComponent<FemaleRestart>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        // Prepared
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !resetGame.restart_female && !inGame && dodge_counter == 0 && !lerpEvent.startFemaleLerp && FemaleCamera.transform.rotation.eulerAngles.y <= 190 && !resetGame.cameraReset)
        {
            isPrepared = true;
            dodge_counter++;
            transform.Translate(Vector3.right * -dodge_offset);
        }

       
        if (resetGame.restart_female)
        {
            inGame = false;
            goUp = false;
            isPrepared = false;
            dodge_counter = 0;
            anim.SetBool("Climb", false);
        }

        if (inGame)
        {
            //  Go up
            if (goUp)
                transform.Translate(Vector3.up * speed * Time.deltaTime);

            //  Simple Dodge to test
            if (Input.GetKeyDown(KeyCode.LeftArrow) && dodge_counter <= 1)
            {
                dodge_counter++;
                transform.Translate(Vector3.right * -dodge_offset);
                anim.SetBool("ChangeDirectionLeft", true);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && dodge_counter >= 1)
            {
                anim.SetBool("ChangeDirectionRight", true);
                dodge_counter--;
                transform.Translate(Vector3.right * dodge_offset);
            }
        }

        if (isPrepared)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && dodge_counter == 1)
            {
                isPrepared = false;
                dodge_counter--;
                transform.Translate(Vector3.right * dodge_offset);
            }
            // Go up
            if (maleMov.isPrepared)
            {
                anim.SetBool("Climb", true);
                inGame = true;
                goUp = true;
                
            }
        }


        if(Time.time - startAttackTime > anim.GetCurrentAnimatorStateInfo(0).length && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {

        }
       

    }
}
