﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleRestart : MonoBehaviour
{
    public GameObject CameraMale;
    public GameObject RespawnParticles;
    public GameObject maleTrapdoll;
    public float cameraSpeed = 0;
    public bool restart = false;
    public bool cameraReset;
    public float respawnSpeed;
    bool respawned = false;
    public Animator respawnAnimation;

    Vector3 initialPosition;
    Vector3 initialCameraPosition;
    Vector3 current_vel;

    void Start()
    {
        initialPosition = transform.position;
        initialCameraPosition = CameraMale.transform.position;
        RespawnParticles.SetActive(false);
    }

    void Update()
    {
        if(restart)
        {
            GameObject deadBody = Instantiate(maleTrapdoll, transform.position, transform.rotation);
            Destroy(deadBody, 3);

            transform.position = new Vector3(initialPosition.x, initialPosition.y - 2, initialPosition.z);
        }

        if(cameraReset)
        {
            CameraMale.transform.position = Vector3.SmoothDamp(CameraMale.transform.position, initialCameraPosition, ref current_vel, 0.5f, cameraSpeed, Time.deltaTime);

            //  Emerge character from the floor
            if (CameraMale.transform.position.y <= initialCameraPosition.y + 2f && !respawned)
            {
                transform.Translate(Vector3.up * respawnSpeed * Time.deltaTime);
                RespawnParticles.SetActive(true);

                respawnAnimation = GetComponent<Animator>();
                respawnAnimation.Play("RespawnRotation");

                if (transform.position.y >= initialPosition.y)
                {
                    RespawnParticles.SetActive(false);
                    respawned = true;
                    if (CameraMale.transform.position.y <= initialCameraPosition.y + 0.1f)
                    {
                        cameraReset = false;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Obstacle")
        {
            restart = true;
            cameraReset = true;
            respawned = false;

        }
    }
}