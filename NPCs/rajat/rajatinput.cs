using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class rajatinput : MonoBehaviour
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
    public GameObject RajatOptions;
    public GameObject yesbutton;

    public GameObject pop;
    public npcpopup talk;

    public GameObject splashart;
    public GameObject shopcanva;

    Volume scenevolume;
    DepthOfField depth;
    dialoguetrigger pltrig;
    public dialoguetrigger di;
    public GameObject main;

    bool trigger;

    AudioSource aud;
    public AudioClip doit;
    void Awake()
    {
        aud = GetComponent<AudioSource>();
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
        limit = dialogues.Count - 1;
    }

    void Update()
    {
        if (eventlibrary.firsttimerajat)
        {
            villager.dialogue = dialogues[limit];
        }

        if (!eventlibrary.firsttimerajat && !ui.inDialogue)
        {
            villager.dialogue = dialogues[0];
        }

        if (ui.inDialogue && villager.dialogue == dialogues[0] && talk.cantalk && balamlook.follow && !pl.gameispaused && !trigger)
        {
            EventSystem.current.SetSelectedGameObject(null);
            trigger = true;
            splashart.SetActive(true);
            ui.dialogueIndex = 0;
            ui.canExit = true;
            yesaud();

            Invoke("correct", 0.1f);
        }

        if ((Input.GetButtonDown(inp.interact) || Input.GetButtonDown(inp.c_interact)) && talk.cantalk && balamlook.follow && eventlibrary.firsttimerajat)
        {
            eventlibrary.firsttimerajat = false;
            splashart.SetActive(true);
            yesaud();

        }

        if ((Input.GetButtonDown(inp.interact) || Input.GetButtonDown(inp.c_interact)) && talk.cantalk && balamlook.follow && !eventlibrary.firsttimerajat && villager.dialogue == dialogues[1])
        {
            yesaud();
            splashart.SetActive(false);
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
        depth.focalLength.value = 300f;
        freeze = true;
        talk.cantalk = false;
        popup.SetActive(false);
        RajatOptions.SetActive(false);
        Options.SetActive(false);
        splashart.SetActive(false);
        Invoke("opendelay", 2f);
    }

    void correct()
    {
        EventSystem.current.SetSelectedGameObject(yesbutton);
        popup.SetActive(true);
        Options.SetActive(true);
        RajatOptions.SetActive(true);
    }

    public void opendelay()
    {
        splashart.SetActive(true);
        shopcanva.SetActive(true);
        EventSystem.current.SetSelectedGameObject(main);
    }

    public void No()
    {
        EventSystem.current.SetSelectedGameObject(null);
        splashart.SetActive(false);
        popup.SetActive(false);
        RajatOptions.SetActive(false);
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
        EventSystem.current.SetSelectedGameObject(null);
        trigger = false;
    }

    public void LeaveShop()
    {
        yesaud();
        EventSystem.current.SetSelectedGameObject(null);
        shopcanva.SetActive(false);
        splashart.SetActive(false);

        Invoke("ReturnView", 0.5f);

    }

    void yesaud()
    {
        aud.PlayOneShot(doit);
    }

}
