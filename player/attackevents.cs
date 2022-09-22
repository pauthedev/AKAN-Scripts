using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackevents : MonoBehaviour
{
    playercont pl;
    void Start()
    {
        pl = FindObjectOfType<playercont>();
    }

    void startcombo()
    {
        pl.startcombo();
    }

    void addcombo()
    {
        pl.addcombo();
    }

    void endcombo()
    {
        pl.endcombo();
        pl.attackmode = false;
    }

    void fallstart()
    {
        pl.isonevent=true;
    }

    void fallend()
    {
        pl.isonevent = false;
    }
}
