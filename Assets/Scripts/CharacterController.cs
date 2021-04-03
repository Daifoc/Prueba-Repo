using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject MartinHez;
    //BOOLS COMPAÑEROS
    public bool comp1 = false;
    public bool comp2 = false;
    public bool comp3 = false;
    public bool comp4 = false;
    public bool comp5 = false;
    public bool comp6 = false;
    public List<bool> BoolComp = new List<bool>();

    //CUALIDADES PERSONAJE
    public int health;
    public int Actualhealth;
    int ataqueBase;

    //VARIABLES MOVIMIENTO
    public int vel;
    float movvert;
    float movhoriz;

    //VARIABLES COMBATE
    public static bool ModoCombate;

    float contador;
    public Vector3 PosicionActual;

    public static bool Hab1Desbloqueada;

    bool EmpezarARecuperarVida = false;
    //public bool EnemyDeathPorCollider = false;

    public GameObject EnemigoCombateActual;
    public GameObject CombManager;

    //CAMARAS
    public GameObject MainCamera;
    public GameObject CamaraCombate;

    public int dañoAtaque;

    //VARIABLES CAMBIO ANIMACION
    bool M_Front = true;
    Animator anim;

    void Awake()
    {
        BoolComp.Add(comp1);
        BoolComp.Add(comp2);
        BoolComp.Add(comp3);
        BoolComp.Add(comp4);
        BoolComp.Add(comp5);
        BoolComp.Add(comp6);
        health = 100;
        ModoCombate = false;
        contador = 0.0f;
        Hab1Desbloqueada = false;
        CamaraCombate.SetActive(false);
        anim = GetComponent<Animator>();
    }


    void Start()
    {

    }


    void Update()
    {
        contador = Time.deltaTime;

        movvert = Input.GetAxis("Vertical");
        movhoriz = Input.GetAxis("Horizontal");

        if (movvert > 0.1f || movvert < -0.1f || movhoriz > 0.1f || movhoriz < -0.1f)
        {
            anim.SetBool("Movimiento", true);
        }
        else
        {
            anim.SetBool("Movimiento", false);
        }


        if (movhoriz == -1f)
        {
            anim.SetFloat("SpeedX", -1f);
        }
        else if (movhoriz == 1f)
        {
            anim.SetFloat("SpeedX", 1f);
        }
        else if (movvert == -1f)
        {
            anim.SetFloat("SpeedY", -1f);
        }
        else if (movvert == 1f)
        {
            anim.SetFloat("SpeedY", 1f);
        }
        else
        {
            anim.SetFloat("SpeedY", 0f);
            anim.SetFloat("SpeedX", 0f);
        }
        

        

        if (ModoCombate == false)
        {
            if (movvert != 0.0f)
            {
                transform.position += new Vector3(0.0f, movvert, 0.0f) * Time.deltaTime * vel;
            }
            else if (movhoriz != 0.0f)
            {
                transform.position += new Vector3(movhoriz, 0.0f, 0.0f) * Time.deltaTime * vel;
            }

            CombManager.GetComponent<CombatManager>().CanvasCombate.SetActive(false);
        }


        //Debug.Log(movJugador.Hab1Desbloqueada);

        if (Hab1Desbloqueada == true && health < 100 && ModoCombate == false)
        {
            Hab1Desbloqueada = false;
            HabilidadRecuperarVida();
        }
    }


    public void HabilidadHuir()
    {
        Debug.Log("Has huido del combate");
        health = 100;
        gameObject.transform.position = PosicionActual;
        ModoCombate = false;
        GiroCombate();
        CamaraCombate.SetActive(false);
        CombManager.GetComponent<CombatManager>().CanvasCombate.SetActive(false);
        MainCamera.SetActive(true);
        CombManager.GetComponent<CombatManager>().DestruirEnemiesHuida();
    }


    public void HabilidadRecuperarVida()
    {
        StartCoroutine("RecuperacionDeVida");
    }


    IEnumerator RecuperacionDeVida()
    {
        if (health <= 90)
        {
            health = health + 10;
            Debug.Log("VIDA DEL PERSONAJE: " + health);
            yield return new WaitForSeconds(3f);
            HabilidadRecuperarVida();
        }

        else if (health > 90 && health < 100)
        {
            health = 100;
            Hab1Desbloqueada = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            EnemigoCombateActual = collision.gameObject;
            PosicionActual = new Vector3(transform.position.x, transform.position.y - 5f, 0f);
            ModoCombate = true;
            //EnemyDeathPorCollider = true;
            CombManager.GetComponent<CombatManager>().GenerarEnemigos();
            MainCamera.SetActive(false);
            transform.position = new Vector3(100.5f, -0.2f, 0f);
            GiroCombate();
            CamaraCombate.SetActive(true);
        }
    }


    public void GiroCombate()
    {
        if (M_Front == true)
        {
            anim.SetBool("CambioBack", true);
            M_Front = false;
        }
        else
        {
            anim.SetBool("CambioBack", false);
            M_Front = true;
        }
    }
}