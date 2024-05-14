using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vittoria : MonoBehaviour {

    public float Tempo_Totale = 5f;

    float Tempo;
    bool Dentro = false;
    bool Una_Vittoria = false;
    bool Uno = false;
    bool Due = false;
    bool Tre = false;

    public static bool Vincita = false;

    private void Start()
    {
        Tempo = Tempo_Totale;
        Vincita = false;
    }

    private void Update()
    {
        if (Una_Vittoria == false)
        {
            if (Dentro == true)
            {
                Timer();
            }
            else
            {
                Tempo = Tempo_Totale;
            }
        }
    }

    void Timer()
    {
        Tempo -= Time.deltaTime;

        if (Tempo < 3 && Tre == false)
        {
            Tre = true;
            //Debug.Log("3");
        }
        else if (Tempo < 2 && Due == false)
        {
            Due = true;
            //Debug.Log("2");
        }
        else if (Tempo < 1 && Uno == false)
        {
            Uno = true;
            //Debug.Log("1");
        }
        else if (Tempo < 0) 
        {
            Una_Vittoria = true;
            Vincita = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Satellite")
        {
            Dentro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Satellite")
        {
            Dentro = false;
            Uno = Due = Tre = false;
        }
    }
}
