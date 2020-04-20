﻿using System.Collections;
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
    public float dodge_speed = 0;
    public float dodge_offset = 0;
    int dodge_counter = 0;

    public bool goUp = false;
    public bool inGame = false;
    public bool isPrepared = false;

    Animator anim;

    bool femaleDodgeLeft = false;
    bool femaleDodgeRight = false;
    bool StartfemaleDodgeLeft = false;
    bool StartfemaleDodgeRight = false;
    Vector3 actualPosition;
    bool dodging = false;


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
            anim.SetBool("ChangeDirectionLeft", true);

            //  Active Dodge movement
            actualPosition = transform.position;
            StartfemaleDodgeLeft = true;
        }
        //  Dodge movement loop LEFT
        if (StartfemaleDodgeLeft && actualPosition.x <= transform.position.x - dodge_offset)
        {
            dodging = false;
            StartfemaleDodgeLeft = false;
            anim.SetBool("ChangeDirectionLeft", false);
            anim.SetBool("DobleDodgeLeft", false);

        }
        else if (StartfemaleDodgeLeft)
        {
            transform.Translate(Vector3.right * -dodge_speed * Time.deltaTime);
        }


        if (resetGame.restart_female)
        {
            inGame = false;
            goUp = false;
            isPrepared = false;
            dodge_counter = 0;
            anim.SetBool("Climb", false);
            anim.SetBool("ChangeDirectionLeft", false);
            anim.SetBool("DobleDodgeLeft", false);

        }

        if (inGame)
        {
            //  Go up
            if (goUp)
                transform.Translate(Vector3.up * speed * Time.deltaTime);

            //  Dodge
            if (Input.GetKeyDown(KeyCode.LeftArrow) && dodge_counter <= 1 && !dodging)
            {
                dodging = true;
                dodge_counter++;
                if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("BracedHangHopLeft") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("BracedHangHopRight"))
                {
                    anim.SetBool("DobleDodgeLeft", true);
                }
                else
                {
                    anim.SetBool("ChangeDirectionLeft", true);
                }

                //  Active Dodge movement
                actualPosition = transform.position;
                femaleDodgeLeft = true;

            }
            //  Dodge movement loop LEFT
            if (femaleDodgeLeft && actualPosition.x <= transform.position.x - dodge_offset)
            {
                dodging = false;
                femaleDodgeLeft = false;
                anim.SetBool("ChangeDirectionLeft", false);
                anim.SetBool("DobleDodgeLeft", false);
            }
            else if(femaleDodgeLeft)
            {
                transform.Translate(Vector3.right * -dodge_speed * Time.deltaTime);
            }


            if (Input.GetKeyDown(KeyCode.RightArrow) && dodge_counter >= 1 && !dodging)
            {
                dodging = true;
                dodge_counter--;

                if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("BracedHangHopRight") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("BracedHangHopLeft"))
                {
                    anim.SetBool("DobleDodgeRight", true);
                }
                else
                {
                    anim.SetBool("ChangeDirectionRight", true);
                }

                //  Active Dodge movement
                actualPosition = transform.position;
                femaleDodgeRight = true;
            }
            //  Dodge movement loop RIGHT
            if (femaleDodgeRight && actualPosition.x >= transform.position.x + dodge_offset)
            {
                dodging = false;
                femaleDodgeRight = false;
                anim.SetBool("ChangeDirectionRight", false);
                anim.SetBool("DobleDodgeRight", false);

            }
            else if (femaleDodgeRight)
            {
                transform.Translate(Vector3.right * + dodge_speed * Time.deltaTime);
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

      

    }
}
