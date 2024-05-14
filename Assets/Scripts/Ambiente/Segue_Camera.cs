using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segue_Camera : MonoBehaviour {

    public GameObject Camera_Posizione;

    Vector3 Non_Saprei = Vector3.zero;

    private void Start()
    {
        transform.position = new Vector3(Camera_Posizione.transform.position.x, Camera_Posizione.transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        Movimento();
    }

    void Movimento()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(Camera_Posizione.transform.position.x, Camera_Posizione.transform.position.y, transform.position.z), ref Non_Saprei, 0.5f);
    }
}
