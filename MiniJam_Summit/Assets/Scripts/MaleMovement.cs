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

    public GameObject obstacles;
    List<Transform> rocks;

    MaleRestart resetGame;

    public float speed = 0;
    public float dodge_offset = 0;
    int dodge_counter = 0;

    public bool goUp = false;
    public bool inGame = false;
    public bool isPrepared = false;

    Animator anim;
    void Start()
    {
        femaleMov = FemaleCharacter.GetComponent<FemaleMovement>();
        lerpEvent = CameraManager.GetComponent<CameraLerpEvent>();
        resetGame = GetComponent<MaleRestart>();
        anim = GetComponent<Animator>();

        rocks = new List<Transform>();
        foreach (Transform i in obstacles.transform)
        {
            rocks.Add(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && !resetGame.restart_male && dodge_counter == 0 && !inGame && !lerpEvent.startMaleLerp && MaleCamera.transform.rotation.eulerAngles.y >= 170 &&!resetGame.cameraReset)
        {
            isPrepared = true;
            dodge_counter++;
            transform.Translate(Vector3.right * dodge_offset);
        }

        if (resetGame.restart_male)
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
                foreach (var r in rocks)
                {
                    if (IsInside(r.position))
                    {
                        //to_del = r;
                        Debug.Log(r.name);
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
                anim.SetBool("Climb", true);
                inGame = true;
                goUp = true;
                
            }
        }
    }

    bool IsInside(Vector3 pos)
    {
        Vector3 up_left = transform.position + new Vector3(-5f, 8f, 0f);
        if(pos.x < up_left.x && pos.x > up_left.x - 10f)
        {
            if(pos.y < up_left.y && pos.y > transform.position.y)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position + new Vector3(-5f, 8f, 0f), transform.position + new Vector3(5f, 8f, 0f));
    }
}
