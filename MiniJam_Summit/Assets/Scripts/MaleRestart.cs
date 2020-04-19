using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleRestart : MonoBehaviour
{
    public GameObject CameraMale;
    public GameObject RespawnParticles;
    public GameObject maleTrapdoll;
    public float cameraSpeed = 0;
    public bool restart_male = false;
    public bool cameraReset;
    public float respawnSpeed;
    bool respawned = false;

    public GameObject FemaleCharacter;
    FemaleRestart femaleRestart;
    //public Animator respawnAnimation;

    float timer_camera_dead = 1.0f;

    Vector3 initialPosition;
    Vector3 initialCameraPosition;
    Vector3 current_vel;

    void Start()
    {
        femaleRestart = FemaleCharacter.GetComponent<FemaleRestart>();
        initialPosition = transform.position;
        initialCameraPosition = CameraMale.transform.position;
        RespawnParticles.SetActive(false);
    }

    void Update()
    {
        if (restart_male)
        {
            GameObject deadBody = Instantiate(maleTrapdoll, transform.position, transform.rotation);
            Destroy(deadBody, 7);

            transform.position = new Vector3(initialPosition.x, initialPosition.y - 2, initialPosition.z);
            restart_male = false;
        }

        if(cameraReset)
        {
            
            timer_camera_dead -= Time.deltaTime;
            if(timer_camera_dead <= 0.0f)
                CameraMale.transform.position = Vector3.SmoothDamp(CameraMale.transform.position, initialCameraPosition, ref current_vel, 0.5f, cameraSpeed, Time.deltaTime);


            //  Emerge character from the floor
            if (CameraMale.transform.position.y <= initialCameraPosition.y + 2f && !respawned)
            {
                transform.Translate(Vector3.up * respawnSpeed * Time.deltaTime);
                RespawnParticles.SetActive(true);

                //respawnAnimation = GetComponent<Animator>();
                //respawnAnimation.Play("RespawnRotation");

                if (transform.position.y >= initialPosition.y)
                {
                    RespawnParticles.SetActive(false);
                    respawned = true;
                    if (CameraMale.transform.position.y <= initialCameraPosition.y + 0.1f)
                    {
                        cameraReset = false;
                        timer_camera_dead = 1.0f;
                    }
                }
            }
        }
    }

    public void ResetMaleLevel()
    {
        restart_male = true;
        cameraReset = true;
        respawned = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Obstacle")
        {
            ResetMaleLevel();
            femaleRestart.ResetFemaleLevel();
        }
    }
}
