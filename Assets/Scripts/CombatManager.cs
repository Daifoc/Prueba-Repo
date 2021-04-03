using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public GameObject Enemy; //prefab del enemigo de la fase alpha

    public GameObject CanvasCombate; //Canvas que solo estará activo durante el combate
    bool plumasEnCombate = false; //Booleana para comprobar si se ha usado la habilidad de plumas de dios
    bool turnoJugador; //Booleana para comprobar si es el turno del jugador
    public static int TipoEnemigos; //Entero que variara según la zona en la que se este y permitirá que se instancien enemigos cada vez más fuertes
    int NumEnemigos; //Entero para decretar cuántos enemigos habrá en pantalla

    public List<Enemigo> ListaStatsEnemigos = new List<Enemigo>();//Lista donde se crean los enemigos y sus stats
    public List<GameObject> EnemigosEnCombate = new List<GameObject>();//Lista donde se almacenan los sprites de los enemigos
    public List<GameObject> EnemigosEnEscena = new List<GameObject>();

    //BOOLS COMPAÑEROS
    public List<bool> CompBool;
    

    int PosY = 4;
    int PosX = 93;

    public GameObject Player; 

    public List<Button> botonesAccion = new List<Button>(); //Lista que almacena el número de botones que hay en el canvas

    public bool jugadorMuerto; //Booleana que comprueba si el jugador sigue con vida
    public bool enemigoMuerto; //Booleana que comprueba si los enemigos siguen con vida

    Animator anim; //Variable animator para cambiar las animaciones

    //ENEMIGOS EN LA ESCENA DE PRUEBA
    public GameObject EnemigoEnLaEscenaDePrueba1;
    public GameObject EnemigoEnLaEscenaDePrueba2;
    public GameObject EnemigoEnLaEscenaDePrueba3;

    private void Awake()
    {
        CompBool = Player.GetComponent<CharacterController>().BoolComp;
        CanvasCombate.SetActive(false);
        turnoJugador = true;
        jugadorMuerto = false;
        enemigoMuerto = false;
        anim = Player.GetComponent<Animator>();
        TipoEnemigos = 3;
    }


    void Update()
    {
        if (CharacterController.ModoCombate == true)
        {
            CanvasCombate.SetActive(true);
        }

        if (turnoJugador == false && EnemigosEnCombate.Count > 0)
        {
            TurnoIA();
        }
    }


    #region AparicionEnemigos
    public void GenerarEnemigos() //Método para crear los sprites de los Enemigos y decretar su posición
    {
        NumEnemigos = Random.Range(1, 6);
        for (int i = 0; i <= NumEnemigos; i++)
        {
            int x = Random.Range(1, TipoEnemigos);
            SpawnEnemigos(x);

            if (i <= 2)
            {
                PosX = PosX + 4;
                PosY = 4;
            }
            else if (i == 3)
            {
                PosX = PosX;
                PosY = 7;
            }
            else
            {
                PosX = PosX - 4;
                PosY = 7;
            }

            GameObject EnemigoCreado = Instantiate(Enemy, transform.position, Quaternion.identity);
            EnemigoCreado.transform.position = new Vector3(PosX, PosY, -1f);
            EnemigosEnCombate.Add(EnemigoCreado);

        }
        PosY = 4;
        PosX = 93;
    }



    void SpawnEnemigos(int x) //Método para la creación de los distintos tipos de enemigos y sus stats
    {
        if (x == 1)
        {
            Enemigo Enemigo1 = new Enemigo(100, 100, 50, 30);
            ListaStatsEnemigos.Add(Enemigo1);
        }

        if (x == 2)
        {
            Enemigo Enemigo2 = new Enemigo(100, 100, 50, 30);
            ListaStatsEnemigos.Add(Enemigo2);
        }

        if (x == 3)
        {
            Enemigo Enemigo3 = new Enemigo(100, 100, 50, 30);
            ListaStatsEnemigos.Add(Enemigo3);
        }

        if (x == 4)
        {
            Enemigo Enemigo4 = new Enemigo(100, 100, 50, 30);
            ListaStatsEnemigos.Add(Enemigo4);
        }

        if (x == 5)
        {
            Enemigo Enemigo5 = new Enemigo(100, 100, 50, 30);
            ListaStatsEnemigos.Add(Enemigo5);
        }

        if (x == 6)
        {
            Enemigo Enemigo6 = new Enemigo(100, 100, 50, 30);
            ListaStatsEnemigos.Add(Enemigo6);
        }

        if (x == 7)
        {
            Enemigo Enemigo7 = new Enemigo(100, 100, 50, 30);
            ListaStatsEnemigos.Add(Enemigo7);
        }

        if (x == 8)
        {
            Enemigo Enemigo8 = new Enemigo(100, 100, 50, 30);
            ListaStatsEnemigos.Add(Enemigo8);
        }

        if (x == 9)
        {
            Enemigo Enemigo9 = new Enemigo(100, 100, 50, 30);
            ListaStatsEnemigos.Add(Enemigo9);
        }

    }

    #endregion

    public void AtaqueNormal() //Método para el ataque básico
    {
        for (int i = 0; i < ListaStatsEnemigos.Count; i++)
        {

            for (int c = 0; c < CompBool.Count; c++)
            {
                if (CompBool[c] == true)
                {
                    int ProbCrit = Random.Range(0, 100);
                    if (ProbCrit <= 10)
                    {
                        ListaStatsEnemigos[i].vidaR = ListaStatsEnemigos[i].vidaR - 30;
                    }
                    if (ProbCrit > 10 && ProbCrit < 90)
                    {
                        ListaStatsEnemigos[i].vidaR = ListaStatsEnemigos[i].vidaR - 20;
                    }
                    else
                    {
                        Debug.Log("Ha fallado el compañero " + c);
                    }
                }
            }
            int daño = 35;
            ListaStatsEnemigos[i].vidaR = ListaStatsEnemigos[i].vidaR - daño;
            Debug.Log(ListaStatsEnemigos[i].vidaR);

            if (ListaStatsEnemigos[i].vidaR <= 0)
            {
                enemigoMuerto = true;
                //Destroy(EnemigosEnCombate[i]);
                DestruirEnemiesHuida();
                Destroy(Player.GetComponent<CharacterController>().EnemigoCombateActual);
            }
        }

        if (enemigoMuerto == false)
        {
            turnoJugador = false;
        }

        else if (enemigoMuerto == true)
        {
            Debug.Log("HAS DERROTADO A LOS ENEMIGOS");
            VolverAlModoExploracion();
        }
    }


    public void AtaqueConGarra() //Método para la habilidad de garra en combate
    {
        for (int i = 0; i < ListaStatsEnemigos.Count; i++)
        {
            for (int c = 0; c < CompBool.Count; c++)
            {
                if (CompBool[c] == true)
                {
                    int ProbCrit = Random.Range(0, 100);
                    if (ProbCrit <= 10)
                    {
                        ListaStatsEnemigos[i].vidaR = ListaStatsEnemigos[i].vidaR - 30;
                    }
                    if (ProbCrit > 10 && ProbCrit < 90)
                    {
                        ListaStatsEnemigos[i].vidaR = ListaStatsEnemigos[i].vidaR - 20;
                    }
                    else
                    {
                        Debug.Log("Ha fallado el compañero " + c);
                    }
                }
            }

            anim.Play("AtaqueGarra");
            int daño = 60;
            ListaStatsEnemigos[i].vidaR = ListaStatsEnemigos[i].vidaR - daño;
            Debug.Log(ListaStatsEnemigos[i].vidaR);

            if (ListaStatsEnemigos[i].vidaR <= 0)
            {
                enemigoMuerto = true;
                //Destroy(EnemigosEnCombate[i]);
                DestruirEnemiesHuida();
                Destroy(Player.GetComponent<CharacterController>().EnemigoCombateActual);
            }
        }

        if (enemigoMuerto == false)
        {
            turnoJugador = false;
        }

        else if (enemigoMuerto == true)
        {
            Debug.Log("HAS DERROTADO A LOS ENEMIGOS");
            VolverAlModoExploracion();
        }
    }

    public void PlumasDeDios() //Método para la habilidad Plumas de dios en combate
    {
        plumasEnCombate = true;
        //anim.Play("PlumasDeDios");
        TurnoIA();
    }

    public void AtaqueLluviaAcida() //Método para la habilidad Lluvia Acida en combate
    {
        for (int i = 0; i < ListaStatsEnemigos.Count; i++)
        {
            for (int c = 0; c < CompBool.Count; c++)
            {
                if (CompBool[c] == true)
                {
                    int ProbCrit = Random.Range(0, 100);
                    if (ProbCrit <= 10)
                    {
                        ListaStatsEnemigos[i].vidaR = ListaStatsEnemigos[i].vidaR - 30;
                    }
                    if (ProbCrit > 10 && ProbCrit < 90)
                    {
                        ListaStatsEnemigos[i].vidaR = ListaStatsEnemigos[i].vidaR - 20;
                    }
                    else
                    {
                        Debug.Log("Ha fallado el compañero " + c);
                    }
                }
            }

            //anim.Play("AtaqueLluviaAcida");
            int daño = 75;
            //int dañoResidual = 5; //Daño que dejará la habilidad durante 2-4 turnos
            ListaStatsEnemigos[i].vidaR = ListaStatsEnemigos[i].vidaR - daño;
            Debug.Log(ListaStatsEnemigos[i].vidaR);

            if (ListaStatsEnemigos[i].vidaR <= 0)
            {
                enemigoMuerto = true;
                //Destroy(EnemigosEnCombate[i]);
                DestruirEnemiesHuida();
                Destroy(Player.GetComponent<CharacterController>().EnemigoCombateActual);
            }
        }

        if (enemigoMuerto == false)
        {
            turnoJugador = false;
        }

        else if (enemigoMuerto == true)
        {
            Debug.Log("HAS DERROTADO A LOS ENEMIGOS");
            VolverAlModoExploracion();
        }
    }

    void TurnoIA()
    {
        StartCoroutine("AtaqueDeLaIA");
        turnoJugador = true;
    }


    IEnumerator AtaqueDeLaIA()
    {
        Debug.Log("Comienza el turno del enemigo");
        for (int a = 0; a < botonesAccion.Count; a++) //Desactiva los botones cuando es el turno de la IA
        {
            botonesAccion[a].interactable = !botonesAccion[a].interactable;
        }
        if (plumasEnCombate==true)
        {
            Debug.Log("Has esquivado el ataque");
            plumasEnCombate = false;
        }
        else
        {
            for (int i = 0; i < EnemigosEnCombate.Count; i++)
            {
                Player.GetComponent<CharacterController>().health = Player.GetComponent<CharacterController>().health - 5;
                Debug.Log("¡El enemigo te ha quitado 5 de vida!");
            }
        }
       

        if (Player.GetComponent<CharacterController>().health <= 0)
        {
            Debug.Log("¡¡HAS SIDO DERROTADO POR EL ENEMIGO!!");
            jugadorMuerto = true;
        }

        yield return new WaitForSeconds(2f);

        if (jugadorMuerto == false)
        {
            for (int a = 0; a < botonesAccion.Count; a++)
            {
                botonesAccion[a].interactable = !botonesAccion[a].interactable;
            }
        }

        else if (jugadorMuerto == true)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }


    void VolverAlModoExploracion()
    {
        for (int a = 0; a < botonesAccion.Count; a++)
        {
            botonesAccion[a].interactable = !botonesAccion[a].interactable;
        }
        StartCoroutine("TiempoDeEsperaFinalDelCombate");
    }


    IEnumerator TiempoDeEsperaFinalDelCombate()
    {
        yield return new WaitForSeconds(2f);
        Player.GetComponent<CharacterController>().GiroCombate();
        Player.transform.position = Player.GetComponent<CharacterController>().PosicionActual;
        Player.GetComponent<CharacterController>().CamaraCombate.SetActive(false);
        Player.GetComponent<CharacterController>().MainCamera.SetActive(true);
        CharacterController.ModoCombate = false;
        enemigoMuerto = false;
        for (int a = 0; a < botonesAccion.Count; a++)
        {
            botonesAccion[a].interactable = !botonesAccion[a].interactable;
        }
    }


    public void DestruirEnemiesHuida()
    {
        for (int i = EnemigosEnCombate.Count-1; i >= 0; i--)
        {
            Destroy(EnemigosEnCombate[i]);
            EnemigosEnCombate.RemoveAt(i);
            //Debug.Log("Enemigos "+ i);
        }
        
        for (int a = ListaStatsEnemigos.Count-1; a >= 0; a--)
        {
            ListaStatsEnemigos.RemoveAt(a);
            //Debug.Log("Lista " + a);
        }
    }
}