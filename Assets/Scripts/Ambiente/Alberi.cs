using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alberi : MonoBehaviour {

    Animation Movimento;

    float Tempo_Attesa;
    bool Inizio_Animazione;

    private void Start()
    {
        Movimento = GetComponent<Animation>();

        Movimento.Play("An_Alberi_03");

        Tempo_Attesa = Random.Range(5f, 15f);
        
        Inizio_Animazione = false;
    }

    private void Update()
    {
        if(Inizio_Animazione == false)
        {
            Timer();
        }
        else
        {
            Animazione();
        }
    }

    void Timer()
    {
        Tempo_Attesa -= Time.deltaTime;

        if (Tempo_Attesa < 0) 
        {
            Inizio_Animazione = true;
        }
    }

    void Animazione()
    {
        Movimento.Stop("An_Alberi_03");

        int Numero_Animazione = Random.Range(0, 2);

        if(Numero_Animazione == 0)
        {
            Movimento.Play("An_Alberi_01");
        }
        else
        {
            Movimento.Play("An_Alberi_02");
        }

        Tempo_Attesa = Random.Range(5f, 15f);
        Inizio_Animazione = false;
        StartCoroutine(Attesa_Inizio());
    }

    IEnumerator Attesa_Inizio()
    {
        yield return new WaitForSeconds(Movimento["An_Alberi_01"].length);
        Movimento.Play("An_Alberi_03");
    }
}
