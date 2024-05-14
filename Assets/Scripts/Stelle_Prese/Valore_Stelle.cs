using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Valore_Stelle : MonoBehaviour {
    
    Slider Barra_Punti;

    public float Pen = 1.8f;

    float Stato_Barra;
    float Valore_Barra_Finale;

    bool Riempimento = false;
    float Area_Totale_Non_Variabile;

    bool Bronzo = false;
    bool Argento = false;
    bool Oro = false;
    bool Diamante = false;

    public float Area_Totale;

    private void Start()
    {
        Barra_Punti = GetComponent<Slider>();
        Barra_Punti.interactable = false;

        Barra_Punti.value = 0;

        Stato_Barra = 0;
        Valore_Barra_Finale = 0;

        Bronzo = Argento = Oro = Diamante = false;

        Riempimento = false;
        Area_Totale_Non_Variabile = Area_Totale;
    }

    private void Update()
    {
        if(Riempimento == true)
        {
            Riempimento_Barra();
        }
    }

    public void Calcolo_Area_Slider(float Area_Tagliata)
    {
        if(Valore_Barra_Finale <= 1)
        {
            float Valore_Temporaneo = (Area_Tagliata / Area_Totale_Non_Variabile) / Pen;

            if (Area_Tagliata >= Area_Totale / 4)
            {
                Valore_Temporaneo *= 1.1f;
                //Debug.Log("+");
            }
            else if (Area_Tagliata <= Area_Totale / 15) 
            {
                Valore_Temporaneo *= 0.9f;
                //Debug.Log("-");
            }
            else
            {
                //Debug.Log("=");
            }

            Valore_Barra_Finale += Valore_Temporaneo;
            Riempimento = true;
        }
    }

    void Riempimento_Barra()
    {
        Stato_Barra += 0.001f;
        
        Barra_Punti.value = Stato_Barra;

        if (Stato_Barra >= 0.5f && Bronzo == false)
        {
            Debug.Log("Bronzo");
            Bronzo = true;
        }
        else if (Stato_Barra >= 0.75f && Argento == false)
        {
            Debug.Log("Argento");
            Argento = true;
        }
        else if (Stato_Barra >= 0.9f && Oro == false)
        {
            Debug.Log("Oro");
            Oro = true;
        }
        else if (Stato_Barra >= 1f && Diamante == false)  
        {
            Debug.Log("Platino");
            Diamante = true;
        }

        if(Stato_Barra >= Valore_Barra_Finale)
        {
            Riempimento = false;
        }
    }
}
