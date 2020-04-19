using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleRestart : MonoBehaviour
{
    public GameObject CameraFemale;
    public GameObject RespawnParticles;
    public GameObject femaleTrapdoll;
    public float cameraSpeed = 0;
    public bool restart = false;
    public bool cameraReset;
    public float respawnSpeed;
    bool respawned = false;

    public GameObject MaleCharacter;
    MaleRestart maleRestart;
    //public Animator respawnAnimation;

    float timer_camera_dead = 1.0f;

    Vector3 initialPosition;
    Vector3 initialCameraPosition;
    Vector3 current_vel;

    void Start()
    {
        maleRestart = MaleCharacter.GetComponent<MaleRestart>();
        initialPosition = transform.position;
        initialCameraPosition = CameraFemale.transform.position;
        RespawnParticles.SetActive(false);
    }

    void Update()
    {
        if (restart)
        {
            GameObject deadBody = Instantiate(femaleTrapdoll, transform.position, transform.rotation);
            Destroy(deadBody, 7);

            transform.position = new Vector3(initialPosition.x, initialPosition.y - 2, initialPosition.z);
        }


        if (cameraReset)
        {

            timer_camera_dead -= Time.deltaTime;
            if (timer_camera_dead <= 0.0f)
                CameraFemale.transform.position = Vector3.SmoothDamp(CameraFemale.transform.position, initialCameraPosition, ref current_vel, 0.5f, cameraSpeed, Time.deltaTime);


            //  Emerge character from the floor
            if (CameraFemale.transform.position.y <= initialCameraPosition.y + 2f && !respawned)
            {
                transform.Translate(Vector3.up * respawnSpeed * Time.deltaTime);
                RespawnParticles.SetActive(true);

                //respawnAnimation = GetComponent<Animator>();
                //respawnAnimation.Play("RespawnRotation");

                if (transform.position.y >= initialPosition.y)
                {
                    RespawnParticles.SetActive(false);
                    respawned = true;
                    if (CameraFemale.transform.position.y <= initialCameraPosition.y + 0.1f)
                    {
                        cameraReset = false;
                        timer_camera_dead = 1.0f;
                    }
                }
            }
        }
    }

    public void ResetLevel()
    {
        restart = true;
        cameraReset = true;
        respawned = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            ResetLevel();
            maleRestart.ResetLevel();
        }
    }
}
