using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleRestart : MonoBehaviour
{
    public GameObject CameraMale;

    public GameObject maleTrapdoll;
    public float cameraSpeed = 0;
    public bool restart = false;
    public bool cameraReset;

    Vector3 initialPosition;
    Vector3 initialCameraPosition;
    Vector3 current_vel;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialCameraPosition = CameraMale.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(restart)
        {
            Instantiate(maleTrapdoll, transform.position, transform.rotation);
            transform.position = initialPosition;
        }
        if(cameraReset)
        {
            CameraMale.transform.position = Vector3.SmoothDamp(CameraMale.transform.position, initialCameraPosition, ref current_vel, 1, cameraSpeed, Time.deltaTime);
            //Rigidbody rbAxe = Axe.GetComponent<Rigidbody>();
            //rbAxe.isKinematic = true;


            if (CameraMale.transform.position.y <= initialCameraPosition.y +0.5f )
                cameraReset = false;
            
        }
        Debug.Log(current_vel);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Obstacle")
        {
            restart = true;
            cameraReset = true;

        }
    }
}
