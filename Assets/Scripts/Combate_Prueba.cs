using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combate_Prueba : MonoBehaviour
{

    //Comentario de prueba

    //Segundo comentario de prueba 
    public bool TurnoIA;
    public TextAlignment textodefallar;

    public bool Combate;
    int bossHealth;


    void Awake()
    {
        TurnoIA = false;
        Combate = false;
        bossHealth = 100;
    }



    void Start()
    {
        HabilidadPerderVida();
    }



    void Update()
    {
        if (bossHealth == 0.0f)
        {
            CharacterController.Hab1Desbloqueada = true;
            Debug.Log("Boss muerto. Vida: " + bossHealth);
            Destroy(gameObject);
        }
    }


    public void HabilidadPerderVida()
    {
            StartCoroutine("RecuperacionDeVida");
    }



    IEnumerator RecuperacionDeVida()
    {
        if (bossHealth != 0)
        {
            Debug.Log(bossHealth);
            bossHealth = bossHealth - 20;
            yield return new WaitForSeconds(1f);
            StartCoroutine("RecuperacionDeVida");
        }

    }


    public void Ataque1()
    {
        if (TurnoIA == false)
        {
            //Se activa la animación 
            int x = Random.Range(0, 100);

            if (x < 80)
            {
                //Enemigo.GetComponent<Vida>() - 20;
                TurnoIA = true;
                TurnoDeLaIA();
            }

            else
            {
                //textodefallar();
                TurnoIA = true;
                TurnoDeLaIA();
            }
        }
    }


    public void Ataque2()
    {
        //Se activa la animación 
        //Enemigo.GetComponent<Vida>() - 30;
        TurnoIA = true;
    }

    public void Ataque3()
    {
        //Se activa la animación 
        //Enemigo.GetComponent<Vida>() - 10;
        TurnoIA = true;
    }
    
    
    /*
    IEnumerator textodefallar()
    {
        textodefallar.enable;
        yield return new WaitForSeconds(3f);
        textodefallar.disable;
    }
    */


    #region ParteDeLaIA

    public void TurnoDeLaIA()
    {
        int ataque = Random.Range(1, 5);

        switch (ataque)
        {
            case 1:
                AtaqueIA1();
                break;

            case 2:
                AtaqueIA2();
                break;

            case 3:
                AtaqueIA3();
                break;

            case 4:
                AtaqueIA4();
                break;

        }
    }

    public void AtaqueIA1()
    {
        //Se activa la animación 
        //Personaje.GetComponent<Vida>() - 30;
        TurnoIA = false;
    }


    public void AtaqueIA2()
    {
        //Se activa la animación 
        //Personaje.GetComponent<Vida>() - 30;
        TurnoIA = false;
    }


    public void AtaqueIA3()
    {
        //Se activa la animación 
        //Personaje.GetComponent<Vida>() - 30;
        TurnoIA = false;
    }



    public void AtaqueIA4()
    {
        //Se activa la animación 
        //Personaje.GetComponent<Vida>() - 30;
        TurnoIA = false;
    }


#endregion


}
