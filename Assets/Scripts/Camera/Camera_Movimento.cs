using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movimento : MonoBehaviour {

    public GameObject Mondo_Oggetto;

    PolygonCollider2D Mondo_Collider;

    Vector3 Non_Saprei = Vector3.zero;

    private void Start()
    {
        Mondo_Collider = Mondo_Oggetto.GetComponent<PolygonCollider2D>();
        transform.position = new Vector3(Mondo_Collider.bounds.center.x, Mondo_Collider.bounds.center.y - 5f, transform.position.z);
    }

    private void FixedUpdate()
    {
        Movimento();   
    }

    void Movimento()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(Mondo_Collider.bounds.center.x, transform.position.y, transform.position.z), ref Non_Saprei, 1);
    }

}
