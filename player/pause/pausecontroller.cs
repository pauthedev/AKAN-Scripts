using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class pausecontroller : MonoBehaviour
{
    public playercont pl;
    public GameObject main;
    public GameObject mainbutton;
    public GameObject options;
    public GameObject optionsbutton;
    public GameObject bag;
    public GameObject bagbutton;
    public GameObject leavedungeon;
    public GameObject backbutton;
    public GameObject returnbutton;
    public GameObject dobutton;
    public GameObject volumeset;
    public GameObject volumesetbutton;
    public GameObject controlset;
    public GameObject graphset;
    public GameObject graphsetbutton;

    public inputs inputs;

    public StandaloneInputModule inputmod;

    public bool onleavemenu;
    public bool leave;

    Volume scenevolume;
    DepthOfField depth;

    public TextMeshProUGUI health;
    public TextMeshProUGUI maxhealth;
    public TextMeshProUGUI slimeshards;
    public TextMeshProUGUI suncoins;
    public TextMeshProUGUI moonpearls;

    public Image healthimg;

    public InventoryObject inventory;
    public InventoryDisplay display;
    public Animator pause_anim;
    public fade faded;
    bool hp;
    public GameObject elements;
    public GameObject info;

    public GameObject earthgem_ui;
    public GameObject firegem_ui;
    public GameObject watergem_ui;

    public Image healthi;

    public TextMeshProUGUI healthtxt;
    public TextMeshProUGUI maxhealthtxt;

    public Sprite fire;
    public Sprite water;
    public Sprite earth;

    public Image gem;

    AudioSource aud;
    public AudioClip doit;
    public AudioClip dont;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        display = FindObjectOfType<InventoryDisplay>();
        faded = GetComponent<fade>();
        pause_anim = pause_anim.gameObject.GetComponent<Animator>();
        scenevolume = FindObjectOfType<Volume>();
        if (scenevolume.profile.TryGet<DepthOfField>(out depth))
        {
            depth.focalLength.value = 1f;//blur effect in pause
        }

        inputmod = FindObjectOfType<StandaloneInputModule>().GetComponent<StandaloneInputModule>();
        inputs = GetComponent<inputs>();
        pl = FindObjectOfType<playercont>();
        EventSystem.current.SetSelectedGameObject(mainbutton);
    }


    void Update()
    {

        int hp = (int)pl.health;

        int maxhp = (int)pl.maxhealth;

        health.text = hp.ToString();
        maxhealth.text = maxhp.ToString();

        healthtxt.text = hp.ToString();
        maxhealthtxt.text = maxhp.ToString();

        slimeshards.text = pl.pearls.ToString();
        suncoins.text = pl.suncoins.ToString();
        moonpearls.text = pl.moonpearls.ToString();

        healthimg.fillAmount = pl.health / pl.maxhealth;
        healthi.fillAmount = pl.health / pl.maxhealth;


        if (!pl.firegem && !pl.watergem && !pl.earthgem)
        {
            gem.color = Color.black;
        }
        else
        {
            gem.color = Color.white;
        }

        if (pl.firegem)
        {
            gem.sprite = fire;
        }

        if (pl.watergem)
        {
            gem.sprite = water;
        }

        if (pl.earthgem)
        {
            gem.sprite = earth;
        }

        if (EventSystem.current.currentSelectedGameObject == bagbutton || EventSystem.current.currentSelectedGameObject == backbutton)
        {
            info.SetActive(false);
        }
        else
        {
            info.SetActive(true);
        }

        if (EventSystem.current.currentSelectedGameObject == backbutton)
        {
            backbutton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        else
        {
            backbutton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }


        if ((Input.GetButtonDown(inputs.attack) || Input.GetButtonDown(inputs.c_attack)) && pl.gameispaused)
        {
            noaud();
            back();
        }
        
        if ((Input.GetButtonDown(inputs.pause) || Input.GetButtonDown(inputs.c_pause)))
        {
            yesaud();
            back();
            
        }

        if (pl.gamepad)
        {
            inputmod.horizontalAxis = inputs.c_movx;
            inputmod.verticalAxis = inputs.c_movz;
            inputmod.submitButton = inputs.c_jump;
            inputmod.cancelButton = inputs.c_attack;
        }
        else
        {
            inputmod.horizontalAxis = inputs.movx;
            inputmod.verticalAxis = inputs.movz;
            inputmod.submitButton = inputs.interact;
            inputmod.cancelButton = inputs.attack;
        }

        if (pl.waterlvl > 0)
        {
            watergem_ui.SetActive(true);
        }
        else
        {
            watergem_ui.SetActive(false);
        }

        if (pl.firelvl > 0)
        {
            firegem_ui.SetActive(true);
        }
        else
        {
            firegem_ui.SetActive(false);
        }

        if (pl.earthlvl > 0)
        {
            earthgem_ui.SetActive(true);
        }
        else
        {
            earthgem_ui.SetActive(false);
        }
    }
    void yesaud()
    {
        aud.PlayOneShot(doit);
    }

    void noaud()
    {
        aud.PlayOneShot(dont);
    }

    public void resume()
    {
        yesaud();
        Time.timeScale = 1;
        Invoke("actualresume", 1f);
        pause_anim.Play("cloth_out");
    }

    public void actualresume()
    {
        depth.focalLength.value = 1f;
        pl.gameispaused = false;
        

        gameObject.SetActive(false);
    }

    public void back()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        leavedungeon.SetActive(false);
        main.SetActive(true);
        volumeset.SetActive(true);
        controlset.SetActive(false);
        graphset.SetActive(false);
        options.SetActive(false);
        bag.SetActive(false);
        backbutton.SetActive(false);
        onleavemenu = false;
        EventSystem.current.SetSelectedGameObject(mainbutton);
    }

    public void dungeon()
    {
        yesaud();
        EventSystem.current.SetSelectedGameObject(null);
        leavedungeon.SetActive(true);
        main.SetActive(false);
        onleavemenu = true;
        EventSystem.current.SetSelectedGameObject(dobutton);
    }

    public void optionsmenu()
    {
        yesaud();
        EventSystem.current.SetSelectedGameObject(null);
        backbutton.SetActive(true);
        main.SetActive(false);
        options.SetActive(true);
        bag.SetActive(false);
        EventSystem.current.SetSelectedGameObject(optionsbutton);
    }

    public void leavedun()
    {
        Time.timeScale = 1f;
        leave = true;
        pl.isonevent = true;
        yesaud();
    }

    public void bagmenu()
    {
        yesaud();
        EventSystem.current.SetSelectedGameObject(null);
        backbutton.SetActive(true);
        main.SetActive(false);
        options.SetActive(false);
        bag.SetActive(true);
        EventSystem.current.SetSelectedGameObject(bagbutton);
    }

    public void hpshow()
    {
        hp = !hp;
        yesaud();
        if (hp==true)
        {
            elements.SetActive(true);
        }
        else
        {
            elements.SetActive(false);
        }
    }

    public void volume()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yesaud();
        volumeset.SetActive(true);
        controlset.SetActive(false);
        graphset.SetActive(false);

        EventSystem.current.SetSelectedGameObject(volumesetbutton);
    }

    public void controls()
    {
        yesaud();
        volumeset.SetActive(false);
        controlset.SetActive(true);
        graphset.SetActive(false);
    }

    public void graphics()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yesaud();
        volumeset.SetActive(false);
        controlset.SetActive(false);
        graphset.SetActive(true);

        EventSystem.current.SetSelectedGameObject(graphsetbutton);
    }

    public void Savequit()
    {
        Time.timeScale = 1;
        yesaud();
        pause_anim.Play("cloth_out");
        inventory.Save();
        faded.fadein();
        pl.isonevent = true;
        eventlibrary.slotsaved = true;//make the game show continue button
        PlayerPrefs.SetInt("slotsaved", (eventlibrary.slotsaved ? 1 : 0));

        PlayerPrefs.SetInt("waterlevel", eventlibrary.waterlevel);


        eventlibrary.baseatk = pl.baseatk;
        PlayerPrefs.SetInt("atk", eventlibrary.baseatk);

        eventlibrary.luck = pl.luck;
        PlayerPrefs.SetInt("luck", eventlibrary.luck);

        eventlibrary.health = pl.maxhealth;
        PlayerPrefs.SetFloat("hp", eventlibrary.health);

        eventlibrary.pearls = pl.pearls;
        PlayerPrefs.SetInt("luck", eventlibrary.luck);

        eventlibrary.moonpearls = pl.moonpearls;
        PlayerPrefs.SetInt("moonpearls", eventlibrary.moonpearls);

        eventlibrary.suncoins = pl.suncoins;
        PlayerPrefs.SetInt("suncoins", eventlibrary.suncoins);

        eventlibrary.firedust = pl.firedust;
        PlayerPrefs.SetInt("firedust", eventlibrary.firedust);

        eventlibrary.waterdust = pl.waterdust;
        PlayerPrefs.SetInt("waterdust", eventlibrary.waterdust);

        eventlibrary.earthdust = pl.earthdust;
        PlayerPrefs.SetInt("earthdust", eventlibrary.earthdust);

        eventlibrary.firegem = pl.firegem;
        PlayerPrefs.SetInt("firegem", (eventlibrary.firegem ? 1 : 0));

        eventlibrary.watergem = pl.watergem;
        PlayerPrefs.SetInt("watergem", (eventlibrary.watergem ? 1 : 0));

        eventlibrary.earthgem = pl.earthgem;
        PlayerPrefs.SetInt("earthgem", (eventlibrary.earthgem ? 1 : 0));
        
        eventlibrary.firegemlvl = pl.firelvl;
        PlayerPrefs.SetInt("firegemlvl", eventlibrary.firegemlvl);

        eventlibrary.watergemlvl = pl.waterlvl;
        PlayerPrefs.SetInt("watergemlvl", eventlibrary.watergemlvl);

        eventlibrary.earthgemlvl = pl.earthlvl;
        PlayerPrefs.SetInt("earthgemlvl", eventlibrary.earthgemlvl);

        PlayerPrefs.SetInt("townchesteast", (eventlibrary.townchesteast ? 1 : 0));

        PlayerPrefs.SetInt("townchestwest", (eventlibrary.townchestwest ? 1 : 0));

        PlayerPrefs.SetInt("firsttimenish", (eventlibrary.firsttimenish ? 1 : 0));

        PlayerPrefs.SetInt("firsttimeimox", (eventlibrary.firsttimeimox ? 1 : 0));

        PlayerPrefs.SetInt("firsttimerajat", (eventlibrary.firsttimerajat ? 1 : 0));

        PlayerPrefs.SetInt("firsttimebalam", (eventlibrary.firsttimebalam ? 1 : 0));

        PlayerPrefs.SetInt("firsttimezazil", (eventlibrary.firsttimezazil ? 1 : 0));

        for (int i = 0; i < inventory.Container.Count; i++)
        {

            if (inventory.Container[i].amount == 0)
            {
                inventory.Container.Remove(inventory.Container[i]);
                Destroy(display.itemDisplayed[inventory.Container[i]]);
            }

        }

        inventory.Save();

        Invoke("loadmysore", 1f);
    }

    public void loadmysore()
    {
        
        SceneManager.LoadSceneAsync("title");
        gameObject.SetActive(false);
    }
}