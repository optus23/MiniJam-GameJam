using UnityEngine;
using System.Collections;

public class CanvasInv : MonoBehaviour
{

    public GameObject menu; // Assign in inspector
    private bool isShowing;

    CameraLerpEvent camera_bool;
    public GameObject camera_manager;

    void Start()
    {
        camera_bool = camera_manager.GetComponent<CameraLerpEvent>();
        isShowing = true;
    }

    void Update()
    {
        if (camera_bool.startMaleLerp)
        {
            menu.SetActive(true);
            isShowing = true;
        }
        else
        {
            menu.SetActive(false);
            isShowing = false;

        }
    }
}
