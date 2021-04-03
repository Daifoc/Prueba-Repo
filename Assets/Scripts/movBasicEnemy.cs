using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movBasicEnemy : MonoBehaviour
{
    public float velhoriz;
    public float velverti;

    public GameObject player;

    bool mov1 = true;
    bool mov2 = false;
    bool mov3 = false;
    bool mov4 = false;


    void Start()
    {
        
    }


    void Update()
    {
        transform.position += new Vector3(-velhoriz, velverti, 0.0f) * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Trigger1")
        {
            velhoriz = 0;
            velverti = -1;
        }


        if (col.tag == "Trigger2")
        {
            velhoriz = -1;
            velverti = 0;
        }


        if (col.tag == "Trigger3")
        {
            velhoriz = 0;
            velverti = 1;
        }


        if (col.tag == "Trigger4")
        {
            velhoriz = 1;
            velverti = 0;
        }

    }
}