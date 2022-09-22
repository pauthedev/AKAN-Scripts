using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zazilinput : MonoBehaviour
{
    inputs inp;
    public lookatcontroller zzlook;
    public GameObject pop;
    public npcpopup talk;
    void Start()
    {
        talk = pop.GetComponent<npcpopup>();
        inp = FindObjectOfType<inputs>();
    }

    void Update()
    {

        if ((Input.GetButtonDown(inp.interact) || Input.GetButtonDown(inp.c_interact)) && talk.cantalk && zzlook.follow && eventlibrary.firsttimezazil)
        {
            eventlibrary.firsttimezazil = false;
        }
    }
}
