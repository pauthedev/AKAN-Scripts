using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class towercontroller : MonoBehaviour
{
    public GameObject startcanvas;

    public CanvasGroup results;
    public GameObject wintext;
    public GameObject losetext;
    public GameObject leavetext;


    public Text pearltext;
    public Text r_pearltext;
    public Text firetext;
    public Text r_firetext;
    public Text watertext;
    public Text r_watertext;
    public Text earthtext;
    public Text r_earthtext;


    public Text floortext;
    public static int floor;
    public Animator anim;

    playercont pl;
    pausecontroller pause;
    fade fade;

    int left=2;
    int die=3;
    int win=1;

    float punishpearl;
    float punishfire;
    float punishwater;
    float punishearth;

    int rewpearl;
    int rewfire;
    int rewwater;
    int rewearth;

    public bool won;
    
    public bool returnbut;

    Volume scenevolume;
    Vignette vignette;
    public int random;
    public int r1;
    public int r2;

    AudioSource aud;
    public AudioClip intro;
    public AudioClip lvl1;
    public AudioClip lvl2;
    public AudioClip lvl3;
    public AudioClip winclip;
    public AudioClip loseclip;

    public InventoryObject inventory;
    public InventoryDisplay display;

    Scene m_Scene;
    void Start()
    {
        m_Scene = SceneManager.GetActiveScene();

        display = FindObjectOfType<InventoryDisplay>();
        aud = GetComponent<AudioSource>();
        aud.clip = intro;
        aud.Play();
        Invoke("audioselect",4.4f);
        r1 = Random.Range(1, 100);
        r2 = Random.Range(1, 100);
        random = Mathf.Min(r1, r2);
        scenevolume = FindObjectOfType<Volume>();
        if (scenevolume.profile.TryGet<Vignette>(out vignette))
        {
            if (random <= 80)
            {//normal floors
                vignette.intensity.value = 0.2f;
                vignette.smoothness.value = 0.2f;
            }
            else
            {//dark floors
                vignette.intensity.value = 1f;
                vignette.smoothness.value = 1f;
            }


        }

        pl = FindObjectOfType<playercont>();
        pause = FindObjectOfType<pausecontroller>();
        fade = FindObjectOfType<fade>();
        startcanvas.SetActive(true);
        pl.isonevent = true;

        if (floor == 0)
        {
            floor = 1;
        }

        Invoke("Fade", 2f);
    }

    void Update()
    {
        floortext.text = "B "+floor.ToString()+" F";
        if (pause.leave) 
        {
            aud.loop = false;
            aud.clip = loseclip;
            aud.Play();
            Invoke("return2", 1f);
            
            pause.leave = false;
        }

        rewpearl = eventlibrary.d_pearls - (int)punishpearl;
        rewfire = eventlibrary.d_firedust - (int)punishfire;
        rewwater = eventlibrary.d_waterdust - (int)punishwater;
        rewearth = eventlibrary.d_earthdust - (int)punishearth;


        pearltext.text = eventlibrary.d_pearls.ToString();
        r_pearltext.text = rewpearl.ToString();

        firetext.text = eventlibrary.d_firedust.ToString();
        r_firetext.text = rewfire.ToString();

        watertext.text = eventlibrary.d_waterdust.ToString();
        r_watertext.text = rewwater.ToString();

        earthtext.text = eventlibrary.d_earthdust.ToString();
        r_earthtext.text = rewearth.ToString();


        if (Input.anyKey && returnbut)
        {
            Misore();
            returnbut = false;
        }


        if (won)
        {
            aud.loop = false;
            aud.clip = winclip;
            aud.Play();

            Invoke("return1", 1f);
            won = false;
        }

        if (pl.health <=0 && !pl.dead)
        {
            aud.loop = false;
            aud.clip = loseclip;
            aud.Play();
            Invoke("return3", 1f);
        }

    }

    void audioselect()
    {

        if (!won && !pl.dead && !pause.leave && !pl.isonevent)
        {
            if (eventlibrary.waterlevel == 0)
            {
                aud.clip = lvl3;
                aud.Play();
            }

            if (eventlibrary.waterlevel == 3)
            {
                aud.clip = lvl3;
                aud.Play();
            }

            if (eventlibrary.waterlevel == 2)
            {
                aud.clip = lvl2;
                aud.Play();
            }

            if (eventlibrary.waterlevel == 1)
            {
                aud.clip = lvl1;
                aud.Play();
            }

            aud.loop = true;
        }
        
    }

    void Fade()
    {
        anim.Play("end");
        Invoke("endFade", 1f);
    }

    void endFade()
    {
        pl.isonevent = false;
        
    }

    void return1()
    {
        returntown(win);
    }

    void return2()
    {
        returntown(left);
    }

    void return3()
    {
        returntown(die);
    }

    void returntown(int cause)
    {//change rewards depending on what was your result
        results.alpha = 1;
        if (cause == 1)
        {//win
            punishpearl = 0;
            punishfire = 0;
            punishwater = 0;
            punishearth = 0;
            wintext.SetActive(true);
        }
        if (cause == 2)
        {//forfait
            punishpearl = eventlibrary.d_pearls *0.2f;
            punishfire = eventlibrary.d_firedust * 0.2f;
            punishwater = eventlibrary.d_waterdust * 0.2f;
            punishearth = eventlibrary.d_earthdust * 0.2f;
            leavetext.SetActive(true);
        }
        if (cause == 3)
        {//died
            punishpearl = eventlibrary.d_pearls * 0.4f;
            punishfire = eventlibrary.d_firedust * 0.4f;
            punishwater = eventlibrary.d_waterdust * 0.4f;
            punishearth = eventlibrary.d_earthdust * 0.4f;
            losetext.SetActive(true);
        }
        

        Invoke("canleave", 1);
    }

    void canleave()
    {
        pl.pearls += rewpearl;
        pl.firedust += rewfire;
        pl.waterdust += rewwater;
        pl.earthdust += rewearth;
        returnbut = true;
    }

    void Misore()
    {
        
        fade.fadein();
        for (int i = 0; i < inventory.Container.Count; i++)
        {

                if (inventory.Container[i].amount == 0)
                {
                    inventory.Container.Remove(inventory.Container[i]);
                    Destroy(display.itemDisplayed[inventory.Container[i]]);
                }

        }

        inventory.Save();

        if (m_Scene.name == "D1")
        {
            Invoke("loadend", 1f);
        }
        else
        {
            Invoke("loadmisore", 1f);
        }
        
    }

    void loadmisore()
    {
        eventlibrary.d_pearls = 0;
        eventlibrary.d_firedust = 0;
        eventlibrary.d_waterdust = 0;
        eventlibrary.d_earthdust = 0;
        eventlibrary.pearls = pl.pearls;
        eventlibrary.firedust = pl.firedust;
        eventlibrary.waterdust = pl.waterdust;
        eventlibrary.earthdust = pl.earthdust;
        eventlibrary.suncoins = pl.suncoins;
        eventlibrary.moonpearls = pl.moonpearls;
        SceneManager.LoadSceneAsync("misore");
    }

    void loadend()
    {
        pl.isonevent = true;
        eventlibrary.slotsaved = true;
        eventlibrary.d_pearls = 0;
        eventlibrary.d_firedust = 0;
        eventlibrary.d_waterdust = 0;
        eventlibrary.d_earthdust = 0;
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

        SceneManager.LoadSceneAsync("congrats");
    }
}
