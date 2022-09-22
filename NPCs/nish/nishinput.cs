using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class nishinput : MonoBehaviour
{
    dialogueui ui;
    playercont pl;
    towncontroller town;
    inputs inp;
    public npccontroller villager;
    public lookatcontroller nishlook;
    public List <dialoguedata> dialogues;
    public bool freeze;

    public GameObject popup;
    public GameObject Options;
    public GameObject NishOptions;
    public GameObject yesbutton;

    public GameObject pop;
    public npcpopup talk;
    public dialoguetrigger di;
    public bool trigger;

    AudioSource aud;
    public AudioClip doit;
    void Awake()
    {
        aud = GetComponent<AudioSource>();
        di = FindObjectOfType<dialoguetrigger>();
        talk = pop.GetComponent<npcpopup>();
        pl = FindObjectOfType<playercont>();
        ui = FindObjectOfType<dialogueui>();
        town = FindObjectOfType<towncontroller>();
        inp = FindObjectOfType<inputs>();
        villager = GetComponent<npccontroller>();
    }

    void Update()
    {
        if (eventlibrary.firsttimenish)
        {
            villager.dialogue = dialogues[1];
        }

        if (!eventlibrary.firsttimenish && !ui.inDialogue)
        {
            villager.dialogue = dialogues[0];
        }

        if (ui.inDialogue && villager.dialogue == dialogues[0] && talk.cantalk && nishlook.follow && !pl.gameispaused && !trigger)
        {
            yesaud();
            EventSystem.current.SetSelectedGameObject(null);
            trigger = true;
            ui.dialogueIndex = 0;
            ui.canExit = true;
            Invoke("correct",0.1f);
        }

        if ((Input.GetButtonDown(inp.interact) || Input.GetButtonDown(inp.c_interact)) && talk.cantalk && nishlook.follow && eventlibrary.firsttimenish)
        {
            yesaud();
            eventlibrary.firsttimenish = false;
        }



        if (freeze)
        {
            pl.isonevent = true;
        }
    }

    void correct()
    {
        EventSystem.current.SetSelectedGameObject(yesbutton);
        popup.SetActive(true);
        Options.SetActive(true);
        NishOptions.SetActive(true);
    }
    public void Go()
    {
        if (nishlook.follow)
        {
            freeze = true;
            popup.SetActive(false);
            NishOptions.SetActive(false);
            Options.SetActive(false);
            town.LoadDungeon();
            EventSystem.current.SetSelectedGameObject(null);
        }
        
    }
    public void No()
    {
        EventSystem.current.SetSelectedGameObject(null);

        Invoke("exittext", 0.2f);
    }

    public void exittext()
    {
        popup.SetActive(false);
        NishOptions.SetActive(false);
        Options.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        trigger = false;
        
    }

    void yesaud()
    {
        aud.PlayOneShot(doit);
    }
}
