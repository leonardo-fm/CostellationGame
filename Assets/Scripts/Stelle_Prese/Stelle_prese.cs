using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stelle_prese : MonoBehaviour {

    Animation Animazione_Stelle;

    int Stelle = 0;

    private void Start()
    {
        Animazione_Stelle = GetComponent<Animation>();
        Stelle = 0;
    }

    public void Aggiunta_Stella()
    {
        Stelle++;

        if(Stelle == 1)
        {
            Animazione_Stelle.Play("An_Stella_01");
        }
        else if(Stelle == 2)
        {
            Animazione_Stelle.Play("An_Stella_02");
        }
        else if (Stelle == 3)
        {
            Animazione_Stelle.Play("An_Stella_03");
        }
        else
        {
            return;
        }
    }

}
