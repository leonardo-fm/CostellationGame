using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllore : MonoBehaviour { 

    Vector2 Arrivo;
    bool Inizio_Test = false;

    private void FixedUpdate()
    {
        if (Inizio_Test == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Arrivo, Time.fixedDeltaTime * 25);

            if(transform.position.Equals(Arrivo))
            {
                Inizio_Test = false;
                Controllo_Logic.Fine_Controllo = true;
            }
        }
    }
       
    public void Controllo(Vector2 PrimoCollider, Vector2 SecondoCollider)
    {
        Inizio_Test = true;
        Arrivo = SecondoCollider;
        transform.position = PrimoCollider;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Mondo")
        {
            Controllo_Logic.Taglio_Multiplo = true;
        }
    }
}
