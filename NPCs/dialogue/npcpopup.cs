using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcpopup : MonoBehaviour
{
    public bool cantalk;

    public GameObject popup;
    public dialogueui ui;

    private void Start()
    {
        ui = FindObjectOfType<dialogueui>();
    }

    void Update()
    {
        if (cantalk && !ui.inDialogue)
        {
            popup.SetActive(true);
        }
        else
        {
            popup.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cantalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            cantalk = false;
        }
    }
}
