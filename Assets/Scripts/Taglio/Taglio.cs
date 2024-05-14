using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taglio : MonoBehaviour {

    public GameObject[] Estremi;

    LineRenderer Linea;
    EdgeCollider2D Morte_Collider;

    Vector3[] Punti = new Vector3[2];

    private void Start()
    {
        Linea = GetComponent<LineRenderer>();
        Morte_Collider = GetComponent<EdgeCollider2D>();

        Linea.positionCount = 2;

        Estremi[0].SetActive(false);
        Estremi[1].SetActive(false);
    }

    private void Update()
    {
        Presa_Punti();
    }

    void Presa_Punti()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Punti[0] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Punti[0].z = 0;

            Estremi[0].transform.position = Punti[0];

            Estremi[0].SetActive(true);
            Estremi[1].SetActive(true);
        }
        else if (Input.GetMouseButton(0))
        {
            Linea.enabled = true;
            Punti[1] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Punti[1].z = 0;

            Estremi[1].transform.position = Punti[1];

            Linea.SetPositions(Punti);
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            Linea.enabled = false;

            Estremi[0].SetActive(false);
            Estremi[1].SetActive(false);
        }
    }

    public void Creazione_Collider_Perdita(Vector2 Primo_Collider, Vector2 Secondo_Collider)
    {
        Vector2[] Punti_Finali = new Vector2[2];

        Punti_Finali[0] = Primo_Collider;
        Punti_Finali[1] = Secondo_Collider;

        Morte_Collider.points = Punti_Finali;

    }   
}
