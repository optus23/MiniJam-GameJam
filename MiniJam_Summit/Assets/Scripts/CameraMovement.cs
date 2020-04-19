using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    MaleMovement maleMov;
    public GameObject MaleCharacter;
    FemaleMovement femaleMov;
    public GameObject FemaleCharacter;

    Vector3 initialPosition;
    public float speed = 0;

    void Start()
    {
        if(MaleCharacter.TryGetComponent<MaleMovement>(out maleMov))
        {}
        if (FemaleCharacter.TryGetComponent<FemaleMovement>(out femaleMov))
        {}

        initialPosition = transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(initialPosition.x, transform.position.y, initialPosition.z);

        //  Go up
        if (maleMov.inGame && maleMov.goUp && femaleMov.inGame && femaleMov.goUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            femaleMov.isPrepared = false;
            maleMov.isPrepared = false;
        }
            
    }
}
