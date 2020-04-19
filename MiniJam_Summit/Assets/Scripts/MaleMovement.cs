using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleMovement : MonoBehaviour
{
    CameraLerpEvent lerpEvent;
    public GameObject CameraManager;
    public GameObject MaleCamera;

    public GameObject FemaleCharacter;
    FemaleMovement femaleMov;

    Transform hit;
    public GameObject obstacles;
    List<Transform> rocks;

    MaleRestart resetGame;

    public float speed = 0;
    public float dodge_offset = 0;
    int dodge_counter = 0;

    public bool goUp = false;
    public bool inGame = false;
    public bool isPrepared = false;


    void Start()
    {
        femaleMov = FemaleCharacter.GetComponent<FemaleMovement>();
        lerpEvent = CameraManager.GetComponent<CameraLerpEvent>();
        resetGame = GetComponent<MaleRestart>();
        hit = transform.Find("Hit");
        if (hit == null)
            Debug.Log("OH NO");

        rocks = new List<Transform>();
        foreach (Transform i in obstacles.transform)
        {
            rocks.Add(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && !resetGame.restart && dodge_counter == 0 && !inGame && !lerpEvent.startMaleLerp && MaleCamera.transform.rotation.eulerAngles.y >= 170 &&!resetGame.cameraReset)
        {
            isPrepared = true;
            dodge_counter++;
            transform.Translate(Vector3.right * dodge_offset);
        }

        if (resetGame.restart)
        {
            inGame = false;
            goUp = false;
            isPrepared = false;
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

            if (Input.GetKeyDown(KeyCode.W))
            {
                Transform to_del = null;
                foreach(var r in rocks)
                {
                    if (r.GetComponent<BoxCollider>().bounds.Contains(hit.position))
                    {
                        to_del = r;
                    }
                }
                if (to_del != null)
                {
                    rocks.Remove(to_del);
                    Destroy(to_del.gameObject);
                }
            }
        }

        if (isPrepared)
        {
            if(Input.GetKeyDown(KeyCode.A) && dodge_counter == 1)
            {
                isPrepared = false;
                dodge_counter--;
                transform.Translate(Vector3.right * -dodge_offset);
            }
            // Go up
            if (femaleMov.isPrepared)
            {
                inGame = true;
                goUp = true;
                
            }
        }
    }
}
