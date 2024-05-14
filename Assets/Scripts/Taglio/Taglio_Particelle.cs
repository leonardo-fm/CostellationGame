using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taglio_Particelle : MonoBehaviour {

    ParticleSystem Particelle;

    private void Start()
    {
        Particelle = GetComponent<ParticleSystem>();
    }

    public void Particelle_Taglio(Vector2 Punto_Uno, Vector2 Punto_Due)
    {
        float Lunghezza_Taglio = Vector2.Distance(Punto_Uno, Punto_Due);
        Vector2 PuntoMedio = new Vector2((Punto_Uno.x + Punto_Due.x) / 2, (Punto_Uno.y + Punto_Due.y) / 2);

        Vector2 Direzione;
        Vector3 Direzione_Tridimensionale;
        if (Punto_Uno.y >= Punto_Due.y)
        {
            Direzione = new Vector2((Punto_Uno.x - Punto_Due.x), (Punto_Uno.y - Punto_Due.y));
            Direzione_Tridimensionale = new Vector3(Direzione.x, Direzione.y, 0);
        }
        else
        {
            Direzione = new Vector2((-Punto_Uno.x + Punto_Due.x), (-Punto_Uno.y + Punto_Due.y));
            Direzione_Tridimensionale = new Vector3(Direzione.x, Direzione.y, 0);
        }

        float Angolo = Vector2.Angle(Vector2.right, Direzione);

        Vector3 Prodotto = Vector3.Cross(new Vector3(0, 0, 1), Direzione_Tridimensionale);

        Ray NuovoPuntoM = new Ray(PuntoMedio, Prodotto.normalized);

        RaycastHit2D Verifica_Mondo = Physics2D.Raycast(NuovoPuntoM.GetPoint(.2f), Prodotto.normalized, 1, 1 << 8);
        Debug.DrawRay(NuovoPuntoM.GetPoint(.2f), Prodotto.normalized, Color.red, 2);

        if (Verifica_Mondo.collider && Verifica_Mondo.collider.tag == "Mondo")
        {
            Angolo += 180;
        }

        Particelle.transform.rotation = Quaternion.Euler(0, 0, Angolo);
        Particelle.transform.position = PuntoMedio;
        ParticleSystem.ShapeModule Valori = Particelle.shape;
        Valori.radius = Lunghezza_Taglio / 2;

        ParticleSystem.EmissionModule Prov = Particelle.emission;

        ParticleSystem.Burst Quantit_Par = new ParticleSystem.Burst { count = Mathf.RoundToInt(Lunghezza_Taglio * 40) };
        Prov.SetBurst(0, Quantit_Par);

        Particelle.Play();
    }

}


