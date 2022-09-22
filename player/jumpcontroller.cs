using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpcontroller : MonoBehaviour
{
    playercont pl;
    void Start()
    {
        pl = FindObjectOfType<playercont>();
    }
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ground") || other.CompareTag("stair"))
        {
            pl.canjump = true;
        }


    }

    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("ground") || other.CompareTag("stair"))
        {
            pl.canjump = false;
        }


    }
}
