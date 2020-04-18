﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleMovement : MonoBehaviour
{
    CameraLerpEvent lerpEvent;
    public GameObject MaleCamera;

    MaleRestart resetGame;

    public float speed = 0;
    public float dodge_offset = 0;
    int dodge_counter = 0;

    public bool goUp = false;
    public bool inGame = false;



    void Start()
    {
        lerpEvent = MaleCamera.GetComponent<CameraLerpEvent>();
        resetGame = GetComponent<MaleRestart>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && dodge_counter == 0 && !lerpEvent.startLerp && MaleCamera.transform.rotation.eulerAngles.y >= 170 &&!resetGame.cameraReset)
        {
            inGame = true;
            goUp = true;
        }
        if(resetGame.restart)
        {
            inGame = false;
            goUp = false;
            resetGame.restart = false;
            dodge_counter = 0;
        }

        if (inGame)
        {
            //  Go up
            if (goUp)
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            
            //  Simple Dodge to test
            if (Input.GetKeyDown(KeyCode.A) && dodge_counter >= 1)
            {
                dodge_counter--;
                transform.Translate(Vector3.right * -dodge_offset);
            }
            if (Input.GetKeyDown(KeyCode.D) && dodge_counter <= 1)
            {
                dodge_counter++;
                transform.Translate(Vector3.right * dodge_offset);
            }
        }


    }
}
