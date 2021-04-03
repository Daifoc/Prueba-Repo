using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vidaI;
    public int vidaR;
    public int velocidad;
    public int ataque;

    public Enemigo(int vidaI, int vidaR, int velocidad, int ataque)
    {
        this.vidaI = vidaI;
        this.vidaR = vidaR;
        this.velocidad = velocidad;
        this.ataque = ataque;
    }
}
