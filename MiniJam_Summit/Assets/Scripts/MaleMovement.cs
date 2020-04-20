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

    public Transform particles;
    public GameObject prefab_particles;

    public float speed = 0;
    public float dodge_speed = 0;
    public float dodge_offset = 0;
    int dodge_counter = 0;

    public bool goUp = false;
    public bool inGame = false;
    public bool isPrepared = false;

    Vector3 actualPosition;
    bool maleDodgeLeft = false;
    bool maleDodgeRight = false;
    bool StartmaleDodgeLeft = false;
    bool StartmaleDodgeRight = false;
    bool dodging = false;

    Animator anim;
    void Start()
    {
        femaleMov = FemaleCharacter.GetComponent<FemaleMovement>();
        lerpEvent = CameraManager.GetComponent<CameraLerpEvent>();
        resetGame = GetComponent<MaleRestart>();
        anim = GetComponent<Animator>();
        //SearchForRocks();
    }

    //public void SearchForRocks()
    //{
    //    rocks = new List<Transform>();
    //    foreach (Transform i in obstacles.transform)
    //    {
    //        rocks.Add(i);
    //    }
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && !resetGame.restart_male && dodge_counter == 0 && !inGame && !lerpEvent.startMaleLerp && MaleCamera.transform.rotation.eulerAngles.y >= 170 &&!resetGame.cameraReset)
        {
            isPrepared = true;
            dodge_counter++;
            anim.SetBool("DodgeRight", true);

            //  Active Dodge movement
            actualPosition = transform.position;
            StartmaleDodgeRight = true;
        }
        //  Dodge movement loop RIGHT
        if (StartmaleDodgeRight && actualPosition.x >= transform.position.x + dodge_offset)
        {
            dodging = false;
            StartmaleDodgeRight = false;
            anim.SetBool("DodgeRight", false);
            anim.SetBool("DobleDodgeRight", false);
        }
        else if (StartmaleDodgeRight)
        {
            transform.Translate(Vector3.right * +dodge_speed * Time.deltaTime);
        }

        if (resetGame.restart_male)
        {
            inGame = false;
            goUp = false;
            isPrepared = false;
            dodge_counter = 0;
            anim.SetBool("Climb", false);
            anim.SetBool("DodgeRight", false);
            anim.SetBool("DobleDodgeRight", false);
            anim.SetBool("DodgeLeft", false);
            anim.SetBool("DobleDodgeLeft", false);
            maleDodgeRight = false;
            maleDodgeLeft = false;
        }

        if (inGame)
        {
            //  Go up
            if (goUp)
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            
            //  Simple Dodge to test
            if (Input.GetKeyDown(KeyCode.A) && dodge_counter >= 1 && !dodging)
            {
                dodging = true;
                dodge_counter--;
                if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("BracedHangHopLeft") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("BracedHangHopRight"))
                {
                    anim.SetBool("DobleDodgeLeft", true);
                }
                else
                {
                    anim.SetBool("DodgeLeft", true);
                }
                GetComponent<AudioSource>().Play();
                //  Active Dodge movement
                actualPosition = transform.position;
                maleDodgeLeft = true;
            }
            //  Dodge movement loop LEFT
            if (maleDodgeLeft && actualPosition.x <= transform.position.x - dodge_offset)
            {
                dodging = false;
                maleDodgeLeft = false;
                anim.SetBool("DodgeLeft", false);
                anim.SetBool("DobleDodgeLeft", false);
            }
            else if (maleDodgeLeft)
            {
                transform.Translate(Vector3.right * -dodge_speed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.D) && dodge_counter <= 1 && !dodging)
            {
                dodging = true;
                dodge_counter++;

                if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("BracedHangHopRight") || this.anim.GetCurrentAnimatorStateInfo(0).IsName("BracedHangHopLeft"))
                {
                    anim.SetBool("DobleDodgeRight", true);
                }
                else
                {
                    anim.SetBool("DodgeRight", true);
                }

                GetComponent<AudioSource>().Play();

                //  Active Dodge movement
                actualPosition = transform.position;
                maleDodgeRight = true;
            }
            //  Dodge movement loop RIGHT
            if (maleDodgeRight && actualPosition.x >= transform.position.x + dodge_offset)
            {
                dodging = false;
                maleDodgeRight = false;
                anim.SetBool("DodgeRight", false);
                anim.SetBool("DobleDodgeRight", false);
            }
            else if (maleDodgeRight)
            {
                transform.Translate(Vector3.right * +dodge_speed * Time.deltaTime);
            }


            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    List<Transform> to_del = new List<Transform>();
            //    foreach (var r in rocks)
            //    {
            //        if (r != null)
            //            if (IsInside(r.position))
            //            {
            //                to_del.Add(r);
            //            }
            //    }
            //    foreach (var d in to_del)
            //    {
            //        GameObject o = Instantiate<GameObject>(prefab_particles, particles);
            //        o.transform.position = d.position;
            //        rocks.Remove(d);
            //        Destroy(d.gameObject);
            //    }
            //}
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
        Vector3 up_left = transform.position + new Vector3(5f, 8f, 0f);
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
