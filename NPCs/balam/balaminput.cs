using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class balaminput : MonoBehaviour
{
    dialogueui ui;
    playercont pl;

    inputs inp;
    public npccontroller villager;
    public lookatcontroller balamlook;
    public List<dialoguedata> dialogues;
    public int limit;
    public bool freeze;

    public GameObject popup;
    public GameObject Options;
    public GameObject BalamOptions;
    public GameObject yesbutton;

    public GameObject pop;
    public npcpopup talk;

    public GameObject splashart;
    public GameObject forgecanva;
    public GameObject forgemain;
    public GameObject forgerunes;
    public GameObject forgecoins;

    Volume scenevolume;
    DepthOfField depth;
    dialoguetrigger pltrig;
    public dialoguetrigger di;
    public GameObject coinbutton;
    public GameObject suncoinbutton;
    public GameObject firerunebutton;

    bool trigger;

    AudioSource aud;
    public AudioClip doit;
    void Awake()
    {
        di = FindObjectOfType<dialoguetrigger>();
        talk = pop.GetComponent<npcpopup>();
        pl = FindObjectOfType<playercont>();
        ui = FindObjectOfType<dialogueui>();
        pltrig = FindObjectOfType<dialoguetrigger>();

        inp = FindObjectOfType<inputs>();
        villager = GetComponent<npccontroller>();

        scenevolume = FindObjectOfType<Volume>();
        if (scenevolume.profile.TryGet<DepthOfField>(out depth))
        {
            depth.focalLength.value = 1f;
        }
    }

    void Start()
    {
        aud = GetComponent<AudioSource>();
        limit = dialogues.Count - 1;
    }

    void Update()
    {
        if (eventlibrary.firsttimebalam)
        {
            villager.dialogue = dialogues[limit];
        }

        if (!eventlibrary.firsttimebalam && !ui.inDialogue)
        {
            villager.dialogue = dialogues[0];
        }

        if (ui.inDialogue && villager.dialogue == dialogues[0] && talk.cantalk && balamlook.follow && !trigger)
        {
            EventSystem.current.SetSelectedGameObject(null);
            trigger = true;
            splashart.SetActive(true);
            ui.dialogueIndex = 0;
            ui.canExit = true;
            yesaud();
            Invoke("correct", 0.1f);
        }

        if ((Input.GetButtonDown(inp.interact) || Input.GetButtonDown(inp.c_interact)) && talk.cantalk && balamlook.follow && eventlibrary.firsttimebalam)
        {
            eventlibrary.firsttimebalam = false;
            yesaud();
        }

        if (freeze)
        {
            pl.isonevent = true;
            pltrig.cantalk = false;
        }


        if (pltrig.ismainchar && !ui.inDialogue && !pl.gameispaused)
        {
            depth.focalLength.value = 1f;
        }
    }

//ui actions
    public void OpenShop()
    {
        EventSystem.current.SetSelectedGameObject(null);
        splashart.SetActive(false);
        depth.focalLength.value = 300f;
        freeze = true;
        talk.cantalk = false;
        popup.SetActive(false);
        BalamOptions.SetActive(false);
        Options.SetActive(false);
        Invoke("opendelay", 2f);
    }

    void correct()
    {
        EventSystem.current.SetSelectedGameObject(yesbutton);
        popup.SetActive(true);
        Options.SetActive(true);
        BalamOptions.SetActive(true);
    }


    public void opendelay()
    {
        splashart.SetActive(true);
        forgecanva.SetActive(true);
        ReturnMain();
    }

    public void No()
    {
        EventSystem.current.SetSelectedGameObject(null);

        splashart.SetActive(false);
        popup.SetActive(false);
        BalamOptions.SetActive(false);
        Options.SetActive(false);
        Invoke("ReturnView", 0.3f);
    }

    public void ReturnView()
    {
        depth.focalLength.value = 1f;
        talk.cantalk = true;
        pltrig.cantalk = true;

        freeze = false;
        pl.isonevent = false;
        trigger = false;
    }

    public void LeaveShop()
    {
        yesaud();
        EventSystem.current.SetSelectedGameObject(null);
        forgecanva.SetActive(false);
        splashart.SetActive(false);
        forgemain.SetActive(false);
        
        Invoke("ReturnView", 0.5f);

    }

    public void ReturnMain()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yesaud();
        forgemain.SetActive(true);
        forgecoins.SetActive(false);
        forgerunes.SetActive(false);
        EventSystem.current.SetSelectedGameObject(coinbutton);
    }

    public void GoRunes()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yesaud();
        forgemain.SetActive(false);
        forgecoins.SetActive(false);
        forgerunes.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firerunebutton);
    }

    public void GoCoins()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yesaud();
        forgemain.SetActive(false);
        forgecoins.SetActive(true);
        forgerunes.SetActive(false);
        EventSystem.current.SetSelectedGameObject(suncoinbutton);
    }
    void yesaud()
    {
        aud.PlayOneShot(doit);
    }
}
