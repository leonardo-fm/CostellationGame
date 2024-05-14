using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllo_Logic : MonoBehaviour {

    public Controllore GetControllore;
    public Controllore1 GetControllore1;

    public static bool Taglio_Multiplo;
    public static bool Fine_Controllo;

    public void Inizio_Controllo(Vector2 Uno, Vector2 Due)
    {
        Taglio_Multiplo = false;
        Fine_Controllo = false;

        GetControllore.Controllo(Uno, Due);
        GetControllore1.Controllo(Due, Uno);
    }
}
