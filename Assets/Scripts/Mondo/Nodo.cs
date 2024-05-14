using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour {

    Animation Animazione_Nodo;
    float Tempo_Attesa;
    bool Timer_Avvio = false;

    private void Start()
    {
        Animazione_Nodo = GetComponent<Animation>();
        Cambio_Dimensione();
    }

    void Cambio_Dimensione()
    {
        int Dimensione_Random = Random.Range(0, 3);

        if(Dimensione_Random == 0)
        {
            Animazione_Nodo.Play("An_Nodi_Inizio_01");
        }
        else if(Dimensione_Random == 1)
        {
            Animazione_Nodo.Play("An_Nodi_Inizio_02");
        }
        else
        {
            Animazione_Nodo.Play("An_Nodi_Inizio_03");
        }
    }

    private void Update()
    {
        if(Timer_Avvio == false)
        {
            Tempo_Attesa = Random.Range(3f, 6f);
            Timer_Avvio = true;
        }
        else
        {
            Timer();
        }
    }

    void Timer()
    {
        Tempo_Attesa -= Time.deltaTime;

        if(Tempo_Attesa < 0)
        {
            Animazione_Nodo.Play("An_Nodi_Attesa");
            Timer_Avvio = false;
        }
    }
}
