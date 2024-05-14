using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mondo : MonoBehaviour {

    public GameObject[] Nodo_Prefab;
    public GameObject Satellite_Oggetto;
    [Range(0f, 10f)]
    public float MoltiplicatoreAttesa = 0f;
    [Space]
    public GameObject Taglio_Particelle_Oggetto;
    public GameObject Taglio_Oggetto;
    public GameObject Punteggio_Oggetto;

    PolygonCollider2D GetMondo_PolygonCollider;
    LineRenderer GetMondo_LineRenderer;
    Controllo_Logic GetControllo_Logic;
    Taglio_Particelle GetTaglio_Particelle;
    Taglio GetTaglio;
    Punteggio GetPunteggio;
    Valore_Stelle GetValore_Stelle;

    Nodo[] Percorso_Iniziale;

    List<Nodo> Lista_Nodi = new List<Nodo>();

    Vector3 Posizione_Nodo;
    Nodo Nodo_Successivo;

    List<Vector2> Punti = new List<Vector2>();
    bool Fine_Azione_Taglio = true;

    RaycastHit2D Primo_Collider;
    RaycastHit2D Secondo_Collider;

    public static bool Perso = false;

    void Start()
    {
        GetMondo_PolygonCollider = GetComponent<PolygonCollider2D>();
        GetMondo_LineRenderer = GetComponent<LineRenderer>();
        GetControllo_Logic = GetComponentInChildren<Controllo_Logic>();
        GetTaglio_Particelle = Taglio_Particelle_Oggetto.GetComponent<Taglio_Particelle>();
        GetTaglio = Taglio_Oggetto.GetComponent<Taglio>();
        GetPunteggio = Punteggio_Oggetto.GetComponent<Punteggio>();

        GetValore_Stelle = FindObjectOfType<Valore_Stelle>();

        Percorso_Iniziale = GetComponentsInChildren<Nodo>();

        Creazione_Lista_Nodi();
        Aggiornamento_Collider_Mondo();
        Linee_Tra_Nodi();

        Nodo_Successivo = Lista_Nodi[0];
        Posizione_Nodo = Nodo_Successivo.transform.position;

        Fine_Azione_Taglio = true;
        Perso = false;

        Calcolo_Area_Totale();
    }

    private void FixedUpdate()
    {
        Posizionamento_Satellite();
    }

    private void Update()
    {
        Presa_Estremi_Tagli();
    }

    void Creazione_Lista_Nodi()
    {
        for (int i = 0; i < Percorso_Iniziale.Length; i++)
        {
            Lista_Nodi.Add(Percorso_Iniziale[i]);
        }
    }

    void Aggiornamento_Collider_Mondo()
    {
        Vector2[] Posizione_Array_Nodi = Array_Posizione_Nodi();

        GetMondo_PolygonCollider.points = Posizione_Array_Nodi;
    }

    Vector2[] Array_Posizione_Nodi()
    {
        Vector2[] Array_Nodi = new Vector2[Lista_Nodi.Count];

        for (int i = 0; i < Lista_Nodi.Count; i++)
        {
            Array_Nodi[i] = Lista_Nodi[i].transform.position;
        }

        return Array_Nodi;
    }

    void Calcolo_Area_Totale()
    {
        Vector2[] Nodi_Iniziali = Array_Posizione_Nodi();
        GetValore_Stelle.Area_Totale = Calcolo_Area(Nodi_Iniziali);
    }

    void Linee_Tra_Nodi()
    {
        Vector3[] PosizionePerLinea = Array_Posizione_Nodi_Tre();
        GetMondo_LineRenderer.positionCount = Lista_Nodi.Count;
        GetMondo_LineRenderer.SetPositions(PosizionePerLinea);
    }

    Vector3[] Array_Posizione_Nodi_Tre()
    {
        Vector3[] Array_Nodi = new Vector3[Lista_Nodi.Count];

        for (int i = 0; i < Lista_Nodi.Count; i++)
        {
            Array_Nodi[i] = Lista_Nodi[i].transform.position;
        }

        return Array_Nodi;
    }

    void Posizionamento_Satellite()
    {
        Satellite_Oggetto.transform.position = Vector2.MoveTowards(Satellite_Oggetto.transform.position, Posizione_Nodo, Time.fixedDeltaTime * Satellite.Vel_Satellite);

        if (Satellite_Oggetto.transform.position == Posizione_Nodo)
        {
            Controllo_Posizion_Nodi();
        }
    }

    void Controllo_Posizion_Nodi()
    {
        for (int i = 0; i < Lista_Nodi.Count; i++)
        {
            if (Nodo_Successivo.Equals(Lista_Nodi[i]))
            {
                if (i + 1 > Lista_Nodi.Count - 1)
                {
                    Nodo_Successivo = Lista_Nodi[0];
                }
                else
                {
                    Nodo_Successivo = Lista_Nodi[i + 1];
                }

                Posizione_Nodo = Nodo_Successivo.transform.position;
                return;
            }
        }
    }

    void Presa_Estremi_Tagli()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Punti.Clear();
            Vector2 Punto_Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bool Dentro_Poligono = Dentro_Fuori(Punto_Mouse);
            if (Dentro_Poligono == true)
            {
                Punti.Clear();
            }
            else
            {
                Punti.Add(Punto_Mouse);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 Punto_Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bool Dentro_Poligono = Dentro_Fuori(Punto_Mouse);
            if (Dentro_Poligono == true) 
            {
                Punti.Clear();
            }
            else
            {
                Punti.Add(Punto_Mouse);

                if (Punti.Count == 2 && Fine_Azione_Taglio == true)
                {
                    Presa_Estremi(Punti[0], Punti[1]);
                }
                else
                {
                    Punti.Clear();
                }
            }
        }
    }
    bool Dentro_Fuori(Vector2 Punto)
    {
        bool Dentro = false;

        RaycastHit2D PrimaLinea = Physics2D.Raycast(Punto, Vector2.up, 1, 1 << 8);
        RaycastHit2D SecondaLinea = Physics2D.Raycast(Punto, Vector2.down, 1, 1 << 8);

        if (PrimaLinea.collider == SecondaLinea.collider)
        {
            if (PrimaLinea.point != Vector2.zero || SecondaLinea.point != Vector2.zero)
            {
                Dentro = true;
            }
        }

        return Dentro;
    }
    void Presa_Estremi(Vector2 PuntoUno, Vector2 PuntoDue)
    {
        Vector2 Direzione = new Vector2(PuntoDue.x - PuntoUno.x, PuntoDue.y - PuntoUno.y);
        float Distanza = Vector2.Distance(PuntoUno, PuntoDue);

        Primo_Collider = Physics2D.Raycast(PuntoUno, Direzione, Distanza, 1 << 8);
        Secondo_Collider = Physics2D.Raycast(PuntoDue, -Direzione, Distanza, 1 << 8);

        float Distanza_Minima = Vector2.Distance(Primo_Collider.point, Secondo_Collider.point);

        if ((Primo_Collider.point == Vector2.zero && Secondo_Collider.point == Vector2.zero) || Distanza_Minima <= 0.1f)
        {
            return;
        }

        GetControllo_Logic.Inizio_Controllo(Primo_Collider.point, Secondo_Collider.point);

        StartCoroutine(Attesa_Verifica_Tagli_Multipli(Primo_Collider, Secondo_Collider));
    }
    IEnumerator Attesa_Verifica_Tagli_Multipli(RaycastHit2D PrimoCollider, RaycastHit2D SecondoCollider)
    {
        yield return new WaitForSeconds(0.1f);

        if(Controllo_Logic.Fine_Controllo == true)
        {
            if (Controllo_Logic.Taglio_Multiplo == false)
            {
                Fine_Azione_Taglio = false;
                GetTaglio.Creazione_Collider_Perdita(PrimoCollider.point, SecondoCollider.point);
                Inizio_Taglio(PrimoCollider.point, SecondoCollider.point);
            }
            else
            {
                Debug.Log("Tagli molteplici");
            }
        }
        else
        {
            StartCoroutine(Attesa_Verifica_Tagli_Multipli(PrimoCollider, SecondoCollider));
        }
    }
    void Inizio_Taglio(Vector2 PrimoCollider, Vector2 SecondoCollider)
    {
        Nodo Nodo_Primo_Collider = Nodo_Vicino_Collider(PrimoCollider);
        Nodo Nodo_Secondo_Collider = Nodo_Vicino_Collider(SecondoCollider);

        Nodo_Primo_Collider = Creazione_Nodi_Estremi(Nodo_Primo_Collider, PrimoCollider);
        Nodo_Secondo_Collider = Creazione_Nodi_Estremi(Nodo_Secondo_Collider, SecondoCollider);

        Lista_Punti_Intermedi(Nodo_Primo_Collider, Nodo_Secondo_Collider);
    }
    Nodo Nodo_Vicino_Collider(Vector2 Punto)
    {
        Nodo Nodo_Vicino = null;

        float Distanza_Minore = 100f;

        for (int i = 0; i < Lista_Nodi.Count; i++)
        {
            float Distanza = Vector2.Distance(Punto, Lista_Nodi[i].transform.position);

            if (Distanza < Distanza_Minore)
            {
                Vector2 Direzione = new Vector2(Lista_Nodi[i].transform.position.x - Punto.x, Lista_Nodi[i].transform.position.y - Punto.y);
                RaycastHit2D AltroPunto = Physics2D.Raycast(Punto, -Direzione, Mathf.Infinity, 1 << 9);

                if (AltroPunto.collider && AltroPunto.collider.tag == "Nodo")
                {
                    Distanza_Minore = Distanza;
                    Nodo_Vicino = Lista_Nodi[i];
                }
            }           
        }

        return Nodo_Vicino;
    }
    Nodo Creazione_Nodi_Estremi(Nodo Nodo_Piu_Vicino, Vector2 PuntoIncisioneCollider)
    {
        Vector2 Direzione = new Vector2(Nodo_Piu_Vicino.transform.position.x - PuntoIncisioneCollider.x, Nodo_Piu_Vicino.transform.position.y - PuntoIncisioneCollider.y);
        RaycastHit2D Secondo_Nodo = Physics2D.Raycast(PuntoIncisioneCollider, -Direzione, Mathf.Infinity, 1 << 9);

        int Index_Primo_Nodo = 0;
        int Index_Secondo_Nodo = 0;

        for (int i = 0; i < Lista_Nodi.Count; i++)
        {
            if (Nodo_Piu_Vicino.transform.position.Equals(Lista_Nodi[i].transform.position))
            {
                Index_Primo_Nodo = i;
            }

            if (Secondo_Nodo.transform.position.Equals(Lista_Nodi[i].transform.position))
            {
                Index_Secondo_Nodo = i;
            }
        }

        int Nodo_Random = Random.Range(0, 3);

        if ((Index_Primo_Nodo == 0 && Index_Secondo_Nodo == Lista_Nodi.Count - 1) || (Index_Secondo_Nodo == 0 && Index_Primo_Nodo == Lista_Nodi.Count - 1))
        {
            Instantiate(Nodo_Prefab[Nodo_Random], transform);
            Percorso_Iniziale = GetComponentsInChildren<Nodo>();
            Lista_Nodi.Insert(Lista_Nodi.Count, Percorso_Iniziale[Percorso_Iniziale.Length - 1]);
            Nodo_Piu_Vicino = Lista_Nodi[Lista_Nodi.Count - 1];
        }
        else if (Index_Primo_Nodo < Index_Secondo_Nodo)
        {
            Instantiate(Nodo_Prefab[Nodo_Random], transform);
            Percorso_Iniziale = GetComponentsInChildren<Nodo>();
            Lista_Nodi.Insert(Index_Secondo_Nodo, Percorso_Iniziale[Percorso_Iniziale.Length - 1]);
            Nodo_Piu_Vicino = Lista_Nodi[Index_Secondo_Nodo];
        }
        else if (Index_Primo_Nodo > Index_Secondo_Nodo)
        {
            Instantiate(Nodo_Prefab[Nodo_Random], transform);
            Percorso_Iniziale = GetComponentsInChildren<Nodo>();
            Lista_Nodi.Insert(Index_Primo_Nodo, Percorso_Iniziale[Percorso_Iniziale.Length - 1]);
            Nodo_Piu_Vicino = Lista_Nodi[Index_Primo_Nodo];
        }

        Nodo_Piu_Vicino.transform.position = PuntoIncisioneCollider;
        return Nodo_Piu_Vicino;
    }
    void Lista_Punti_Intermedi(Nodo Primo_Estremo, Nodo Secondo_Estremo)
    {
        List<Nodo> Lista_Nodi_Intermedi_Prima = new List<Nodo>();
        List<Nodo> Lista_Nodi_Intermedi_Seconda = new List<Nodo>();

        Lista_Nodi_Intermedi_Prima = Creatore_Lista_Nodi_Intermedia(Primo_Estremo, Secondo_Estremo);
        Lista_Nodi_Intermedi_Seconda = Creatore_Lista_Nodi_Intermedia(Secondo_Estremo, Primo_Estremo);

        Vector2[] Primo_Array_Punti = Conversione_Liste_Area(Lista_Nodi_Intermedi_Prima);
        Vector2[] Secondo_Array_Punti = Conversione_Liste_Area(Lista_Nodi_Intermedi_Seconda);

        float Area_Primo = Calcolo_Area(Primo_Array_Punti);
        float Area_Secondo = Calcolo_Area(Secondo_Array_Punti);

        float Tempo_Totale;
        List<Nodo> Lista_Nodi_Intermedi = new List<Nodo>();

        float Area_finale;

        if (Area_Primo >= Area_Secondo)
        {
            Lista_Nodi_Intermedi = Lista_Nodi_Intermedi_Seconda;
            Tempo_Totale = Area_Secondo * MoltiplicatoreAttesa;
            Area_finale = Area_Secondo;
        }
        else
        {
            Lista_Nodi_Intermedi = Lista_Nodi_Intermedi_Prima;
            Tempo_Totale = Area_Primo * MoltiplicatoreAttesa;
            Area_finale = Area_Primo;
        }

        StartCoroutine(Passaggio(Tempo_Totale, Lista_Nodi_Intermedi, Area_finale));
    }
    List<Nodo> Creatore_Lista_Nodi_Intermedia(Nodo Primo, Nodo Secondo)
    {
        List<Nodo> Lista = new List<Nodo>();

        bool Dentro = false;

        int j = Lista_Nodi.IndexOf(Primo);

        do
        {
            Lista.Add(Lista_Nodi[j]);

            if (Lista_Nodi[j] != Secondo)
            {
                if (j == Lista_Nodi.Count - 1)
                {
                    j = -1;
                }

                j++;
            }
            else
            {
                Dentro = true;
            }

        } while (Dentro == false);

        return Lista;
    }
    Vector2[] Conversione_Liste_Area(List<Nodo> Lista)
    {
        Vector2[] Array_Punti = new Vector2[Lista.Count];

        for (int i = 0; i < Lista.Count; i++)
        {
            Array_Punti[i] = Lista[i].transform.position;
        }

        return Array_Punti;
    }
    float Calcolo_Area(Vector2[] PuntiArray)
    {
        float temp = 0;

        for (int i = 0; i < PuntiArray.Length - 1; i++)
        {
            float Moltiplicazione = PuntiArray[i].x * PuntiArray[i + 1].y;
            temp = temp + Moltiplicazione;
        }
        temp = temp + (PuntiArray[PuntiArray.Length - 1].x * PuntiArray[0].y);

        for (int i = 0; i < PuntiArray.Length - 1; i++)
        {
            float Moltiplicazione = PuntiArray[i].y * PuntiArray[i + 1].x;
            temp = temp - Moltiplicazione;
        }
        temp = temp - (PuntiArray[PuntiArray.Length - 1].y * PuntiArray[0].x);

        return Mathf.Abs(temp / 2);
    }
    IEnumerator Passaggio(float Tempo_Totale, List<Nodo> Lista_Nodi_Intermedi, float Area_Finale)
    {
        yield return new WaitForSecondsRealtime(Tempo_Totale);
        Eliminazione_Punti_Futili(Lista_Nodi_Intermedi);
        Spostamento_Nodi(Lista_Nodi_Intermedi, Area_Finale);
    }
    void Eliminazione_Punti_Futili(List<Nodo> Lista_Nodi_Intermedi)
    {
        if (Lista_Nodi_Intermedi.Count > 2)
        {
            Lista_Nodi_Intermedi.RemoveAt(Lista_Nodi_Intermedi.Count - 1);

            for (int i = 1; i < Lista_Nodi_Intermedi.Count; i++) 
            {
                for (int j = 0; j < Lista_Nodi.Count; j++)
                {
                    if (Lista_Nodi[j] == Lista_Nodi_Intermedi[i])
                    {
                        Lista_Nodi.Remove(Lista_Nodi[j]);
                    }
                }

                for (int k = 0; k < Percorso_Iniziale.Length; k++)
                {
                    if (Percorso_Iniziale[k] == Lista_Nodi_Intermedi[i])
                    {
                        Destroy(Percorso_Iniziale[k].gameObject);
                    }
                }
            }
        }
    }
    void Spostamento_Nodi(List<Nodo> Lista_Nodi_Intermedi, float Area_Finale)
    {
        Percorso_Iniziale = GetComponentsInChildren<Nodo>();
        Aggiustamento_Nodo(Lista_Nodi_Intermedi);
        Aggiornamento_Collider_Mondo();
        GetTaglio_Particelle.Particelle_Taglio(Primo_Collider.point, Secondo_Collider.point);
        Linee_Tra_Nodi();
        Nuova_Perdita();

        if (Perso == false) 
        {
            GetPunteggio.Conta_Punteggio(Area_Finale);

            Calcolo_Area_Totale();
            GetValore_Stelle.Calcolo_Area_Slider(Area_Finale);
        }

        Punti.Clear();
        Fine_Azione_Taglio = true;
    }
    void Nuova_Perdita()
    {
        Vector2 Direzione = new Vector2(GetMondo_PolygonCollider.bounds.center.x - Satellite_Oggetto.transform.position.x, GetMondo_PolygonCollider.bounds.center.y - Satellite_Oggetto.transform.position.y);
        float Distanza = Vector2.Distance(Satellite_Oggetto.transform.position, GetMondo_PolygonCollider.bounds.center);

        RaycastHit2D Perdita = Physics2D.Raycast(Satellite_Oggetto.transform.position, Direzione, Distanza, 1 << 10);

        if (Perdita.collider && Perdita.collider.tag == "Morte")
        {
            Perso = true;
        }
    }
    void Aggiustamento_Nodo(List<Nodo> Lista_Nodi_Intermedia)
    {
        if (Nodo_Successivo == Lista_Nodi_Intermedia[1])
        {
            Nodo_Successivo = Lista_Nodi_Intermedia[0];
            Posizione_Nodo = Nodo_Successivo.transform.position;
        }
    }
}
