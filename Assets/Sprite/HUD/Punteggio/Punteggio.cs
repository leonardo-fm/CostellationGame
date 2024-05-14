using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Punteggio : MonoBehaviour {

    public Text Testo_Punteggio;

    public float Moltiplicatore_Punti = 100f;

    Valore_Stelle GetValore_Stelle;

    int Punteggio_Totale;
    int Punteggio_Attuale;

    private void Start()
    {
        Punteggio_Totale = 0;
        Punteggio_Attuale = Punteggio_Totale;

        GetValore_Stelle = FindObjectOfType<Valore_Stelle>();
    }

    private void Update()
    {
        Aggiornamento_Punteggio();
    }

    void Aggiornamento_Punteggio()
    {
        if(Punteggio_Totale != Punteggio_Attuale)
        {
            Testo_Punteggio.text = Punteggio_Attuale.ToString();

            int Differenza = Punteggio_Totale - Punteggio_Attuale;

            if (Differenza >= 1800)
            {
                Punteggio_Attuale += 51;
            }
            else if (Differenza < 1800 && Differenza >= 500)
            {
                Punteggio_Attuale += 27;
            }
            else if (Differenza < 500 && Differenza >= 50)
            {
                Punteggio_Attuale += 9;
            }
            else
            {
                Punteggio_Attuale += 1;
            }
        }
    }

    public void Conta_Punteggio(float Area)
    {
        Punteggio_Totale += Mathf.RoundToInt((Area * Moltiplicatore_Punti) / GetValore_Stelle.Pen);
    }

}
