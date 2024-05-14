using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour {

    //se metti più sprite per satellite cambia il numero

    public GameObject Satellite_Immagine;

    ParticleSystem[] Particelle_Satellite;
    SpriteRenderer[] Satelliti_Sprites;

    bool Nuovo_Colore = false;
    float Colore_Nero = 0.2f;
    ParticleSystem.MainModule Cambio_Colore_Particelle;
    ParticleSystem.MainModule Cambio_Colore_Particelle_Morte;

    public float Vel_Sat = 8;

    public static float Vel_Satellite;

    bool Uno = false;

    private void Start()
    {
        Particelle_Satellite = GetComponentsInChildren<ParticleSystem>();
        Satelliti_Sprites = GetComponentsInChildren<SpriteRenderer>();

        Cambio_Colore_Particelle = Particelle_Satellite[1].main;
        Cambio_Colore_Particelle_Morte = Particelle_Satellite[0].main;

        Vel_Satellite = Vel_Sat;
    }

    private void Update()
    {
        if (Mondo.Perso == true && Uno == false) 
        {
            Perdita();
        }

        if(Vittoria.Vincita == true && Uno == false)
        {
            Successo();
        }

        Cambio_Colore();
    }

    void Perdita()
    {
        Uno = true;
        Vel_Satellite = 0;
        StartCoroutine(Attesa_Fine());
    }
    IEnumerator Attesa_Fine()
    {
        yield return new WaitForSeconds(1f);
        Satellite_Immagine.SetActive(false);
        Particelle_Satellite[0].Play();
        Debug.Log("Perso");
    }

    void Successo()
    {
        Uno = true;
        Vel_Satellite = 0;
        Debug.Log("Vinto");
    }

    void Cambio_Colore()
    {
        if (Nuovo_Colore == false)
        {
            Satelliti_Sprites[0].color = Color.Lerp(Satelliti_Sprites[0].color, Color.white, 0.1f);
            Satelliti_Sprites[1].color = Color.Lerp(Satelliti_Sprites[1].color, Color.white, 0.1f);
            Cambio_Colore_Particelle.startColor = Color.Lerp(Cambio_Colore_Particelle.startColor.color, Color.white, 0.1f);
            Cambio_Colore_Particelle_Morte.startColor = Color.Lerp(Cambio_Colore_Particelle.startColor.color, Color.white, 0.1f);
        }
        else
        {
            Satelliti_Sprites[0].color = Color.Lerp(Satelliti_Sprites[0].color, new Color(Colore_Nero, Colore_Nero, Colore_Nero, 1), 0.1f);
            Satelliti_Sprites[1].color = Color.Lerp(Satelliti_Sprites[1].color, new Color(Colore_Nero, Colore_Nero, Colore_Nero, 1), 0.1f);
            Cambio_Colore_Particelle.startColor = Color.Lerp(Cambio_Colore_Particelle.startColor.color, new Color(Colore_Nero, Colore_Nero, Colore_Nero, 1), 0.1f);
            Cambio_Colore_Particelle_Morte.startColor = Color.Lerp(Cambio_Colore_Particelle.startColor.color, new Color(Colore_Nero, Colore_Nero, Colore_Nero, 1), 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Vittoria")
        {
            Nuovo_Colore = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Vittoria")
        {
            Nuovo_Colore = true;
        }
    }
}
