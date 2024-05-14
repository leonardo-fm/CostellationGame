using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bottoni_Logica : MonoBehaviour {

    public void Tasto_Rifare()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
