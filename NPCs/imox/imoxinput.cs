using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class imoxinput : MonoBehaviour
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

    public GameObject main;
    bool trigger;

    AudioSource aud;
    public AudioClip doit;
    void Awake()
    {
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
        if (eventlibrary.firsttimeimox)
        {
            villager.dialogue = dialogues[limit];
        }

        if (!eventlibrary.firsttimeimox && !ui.inDialogue)
        {
            villager.dialogue = dialogues[0];
        }

        if (ui.inDialogue && villager.dialogue == dialogues[0] && talk.cantalk && balamlook.follow && !pl.gameispaused &&!trigger)
        {
            trigger = true;
            splashart.SetActive(true);
            ui.dialogueIndex = 0;
            ui.nextDialogue = true;
            popup.SetActive(true);
            Options.SetActive(true);
            RajatOptions.SetActive(true);
            yesaud();
            EventSystem.current.SetSelectedGameObject(yesbutton);
        }

        if (ui.inDialogue && talk.cantalk && balamlook.follow && !pl.gameispaused && eventlibrary.firsttimeimox)
        {
            eventlibrary.firsttimeimox = false;
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
        yesaud();
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

    public void opendelay()
    {
        splashart.SetActive(true);
        shopcanva.SetActive(true);
        EventSystem.current.SetSelectedGameObject(main);
    }

    public void No()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yesaud();
        Invoke("ReturnView", 0.1f);

    }
    void yesaud()
    {
        aud.PlayOneShot(doit);
    }
    public void ReturnView()
    {
        trigger = false;
        splashart.SetActive(false);
        popup.SetActive(false);
        RajatOptions.SetActive(false);
        Options.SetActive(false);
        depth.focalLength.value = 1f;
        talk.cantalk = true;
        pltrig.cantalk = true;

        freeze = false;
        pl.isonevent = false;
    }

    public void LeaveShop()
    {
        trigger = false;
        yesaud();
        EventSystem.current.SetSelectedGameObject(null);
        shopcanva.SetActive(false);
        splashart.SetActive(false);

        Invoke("ReturnView", 0.5f);

    }


}
