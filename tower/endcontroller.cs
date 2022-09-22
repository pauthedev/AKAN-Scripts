using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endcontroller : MonoBehaviour
{
    bool cango;
    bool trigger;
    inputs inp;
    playercont pl;
    towercontroller tw;
    Animator anim;
    public GameObject popup;
    void Start()
    {
        inp = FindObjectOfType<inputs>();
        pl = FindObjectOfType<playercont>();
        tw = FindObjectOfType<towercontroller>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (cango)
        {
            if (!trigger)
            {
                popup.SetActive(true);
            }
            if ((Input.GetButtonDown(inp.interact) || Input.GetButtonDown(inp.c_interact)) && cango && !trigger && !pl.gameispaused)
            {
                trigger = true;
                anim.Play("palanca");
                Invoke("go", 1.3f);
            }
        }
        else
        {
            popup.SetActive(false);
        }
    }


    void go()
    {
        tw.won = true;
        pl.isonevent = true;
        if(eventlibrary.waterlevel != 1)
        {
            eventlibrary.waterlevel -= 1;//decrease water lvl for town changes
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cango = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            cango = false;
        }
    }
}
