using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneta : MonoBehaviour {

    Animation Animazione_moneta;
    ParticleSystem Prese;

    Stelle_prese GetStelle_Prese;

    private void Start()
    {
        Animazione_moneta = GetComponent<Animation>();
        Prese = GetComponentInChildren<ParticleSystem>();
        GetStelle_Prese = FindObjectOfType<Stelle_prese>();

        StartCoroutine(Attesa_Inizio());
    }

    IEnumerator Attesa_Inizio()
    {
        float Tempo_Attesa_Random = Random.Range(0f, 2f);

        yield return new WaitForSeconds(Tempo_Attesa_Random);

        Animazione_moneta.Play("An_Moneta_Inizio");

        StartCoroutine(Attesa_Animazione_Inizio());
    }

    IEnumerator Attesa_Animazione_Inizio()
    {
        yield return new WaitForSeconds(Animazione_moneta["An_Moneta_Inizio"].length);

        Animazione_moneta.Play("An_Moneta");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Satellite")
        {
            Animazione_moneta.Stop("An_Moneta");
            Animazione_moneta.Play("An_Moneta_Fine");
            Prese.Play();
            StartCoroutine(Attesa_Distruzione());
        }
    }

    IEnumerator Attesa_Distruzione()
    {
        yield return new WaitForSeconds(1);
        GetStelle_Prese.Aggiunta_Stella();
        Destroy(gameObject);
    }

}
