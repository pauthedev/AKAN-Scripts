using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.UI;

public class titlecontroller : MonoBehaviour
{
    //ui elements
    public GameObject presstext;
    public GameObject continuebutton;
    public GameObject gameoptions;
    public GameObject newgameoptions;
    public GameObject gamebutton;
    public GameObject newgamebutton;
    public GameObject returnbutton;
    public AudioMixer audiomix;
    public Slider masterslider;
    public fade faded;

    //ui conditions
    public bool openingtext;

    //ui animations
    public GameObject animpoint;
    public Animator anim;
    public Animator ui_anim;

    //inputs and gamecontroller use
    public bool gamepad;
    public inputs inputs;
    public StandaloneInputModule inputmod;

    //inventory settings
    public InventoryObject inventory;
    public ItemObject sword;

    //audio settings
    AudioSource aud;
    public AudioClip doit;
    public AudioClip dont;

    void Start()
    {
        Screen.fullScreen = true;
        aud = GetComponent<AudioSource>();
        inputmod = FindObjectOfType<StandaloneInputModule>().GetComponent<StandaloneInputModule>();
        inputs = FindObjectOfType<inputs>();
        anim = animpoint.GetComponent<Animator>();
        ui_anim = gameObject.GetComponent<Animator>();
        faded = GetComponent<fade>();


        faded.fadeout();
        openingtext = true;
        gameoptions.SetActive(false);
        newgameoptions.SetActive(false);
        presstext.SetActive(true);
        Time.timeScale = 1.0f;

	//create or get volume values
        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 0);
        }
        else
        {
            LoadMasterVolume();
        }
    }

    void Update()
    {
	//ui continue
        if (PlayerPrefs.HasKey("slotsaved"))
        {
            continuebutton.SetActive(true);
        }
        else
        {
            continuebutton.SetActive(false);
        }

        if (Input.anyKeyDown && openingtext)
        {
            anim.Play("inicio");
            ui_anim.Play("cloth_on");
            returnmenu();
        }

        if (gamepad)
        {   //use controller axis
            inputmod.horizontalAxis = inputs.c_movx;
            inputmod.verticalAxis = inputs.c_movz;
            inputmod.submitButton = inputs.c_jump;
            inputmod.cancelButton = inputs.c_attack;
        }
        else
        {   //use keyboard axis
            inputmod.horizontalAxis = inputs.movx;
            inputmod.verticalAxis = inputs.movz;
            inputmod.submitButton = inputs.interact;
            inputmod.cancelButton = inputs.attack;
        }
	
	//ui scaling
        if (EventSystem.current.currentSelectedGameObject == continuebutton)
        {
            continuebutton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        else
        {
            continuebutton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }

        if (EventSystem.current.currentSelectedGameObject == gamebutton)
        {
            gamebutton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        else
        {
            gamebutton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }

        if (EventSystem.current.currentSelectedGameObject == returnbutton)
        {
            returnbutton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        else
        {
            returnbutton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void FixedUpdate()
    {
        //get joystick names
        string[] temp = Input.GetJoystickNames();

        //check whether array contains any element
        if (temp.Length > 0)
        {
            for (int i = 0; i < temp.Length; ++i)
            {
                //check if the string is empty or not
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    //controller temp[i] is connected

                    if (!gamepad)
                    {
                        Debug.Log("Controller " + i + " is connected using: " + temp[i]);


                    }
                    gamepad = true;
                }
                else
                {
		    //controller temp[i] is not connected
                    if (gamepad)
                    {
                        Debug.Log("Controller: " + i + " is disconnected.");


                    }
                    gamepad = false;
                }
            }
        }
    }

    public void returntitle()
    {
        anim.Play("inicio_off");
        ui_anim.Play("cloth_off");
        noaud();
        EventSystem.current.SetSelectedGameObject(null);
        Invoke("able", 1f);
    }
    
    void able()
    {
        gameoptions.SetActive(false);
        newgameoptions.SetActive(false);
        presstext.SetActive(true);
        openingtext = true;
    }

    void yesaud()
    {
        aud.PlayOneShot(doit);
    }

    void noaud()
    {
        aud.PlayOneShot(dont);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void newmenu()
    {
        EventSystem.current.SetSelectedGameObject(newgamebutton);
        openingtext = false;
        gameoptions.SetActive(false);

	//check saved data and choose to create a new one or asks to delete
        if (PlayerPrefs.HasKey("slotsaved"))
        {
            newgameoptions.SetActive(true);
        }
        else
        {
            deletedata();
        }
        
    }

    public void returnmenu()
    {
        yesaud();
        presstext.SetActive(false);
        Invoke("activate", 0.1f);
        newgameoptions.SetActive(false);
        EventSystem.current.SetSelectedGameObject(gamebutton);
        openingtext = false;
    }

    void activate()
    {
        gameoptions.SetActive(true);
    }

    void loadmysore()
    {
        faded.fadein();
        yesaud();
        Invoke("async", 1f);
    }

    void loadintro()
    {
        faded.fadein();
        yesaud();
        Invoke("asyncint", 1f);
    }

    void async()
    {
        SceneManager.LoadSceneAsync("misore");
    }

    void asyncint()
    {
        SceneManager.LoadSceneAsync("intro");
    }

    public void ChangeMasterVolume()
    {
        audiomix.SetFloat("Master", masterslider.value);
        SaveMasterVolume();
    }

    public void SaveMasterVolume()
    {
        PlayerPrefs.SetFloat("masterVolume", masterslider.value);

    }

    public void LoadMasterVolume()
    {
        masterslider.value = PlayerPrefs.GetFloat("masterVolume");
    }

    public void deletedata()
    {
        if (PlayerPrefs.HasKey("slotsaved"))
        {
            PlayerPrefs.DeleteAll();
        }

        eventlibrary.slotsaved = false;
        eventlibrary.waterlevel = 3;
        eventlibrary.baseatk = 10;
        eventlibrary.luck = 0;
        eventlibrary.health = 100;
        eventlibrary.pearls = 0;
        eventlibrary.moonpearls = 0;
        eventlibrary.suncoins = 0;
        eventlibrary.firedust = 0;
        eventlibrary.waterdust = 0;
        eventlibrary.earthdust = 0;
        eventlibrary.firegem = false;
        eventlibrary.watergem = false;
        eventlibrary.earthgem = false;
        eventlibrary.firegemlvl = 0;
        eventlibrary.watergemlvl = 0;
        eventlibrary.earthgemlvl = 0;
        eventlibrary.townchestwest = true;
        eventlibrary.townchesteast = true;
        eventlibrary.firsttimenish = true;
        eventlibrary.firsttimeimox = true;
        eventlibrary.firsttimebalam = true;
        eventlibrary.firsttimerajat = true;
        eventlibrary.firsttimezazil = true;

        inventory.Load();
        inventory.Container.Clear();
        inventory.AddItem(sword, 1);
        inventory.Save();

        loadintro();
    }

    public void loaddata()
    {
        
        eventlibrary.slotsaved = (PlayerPrefs.GetInt("slotsaved") != 0);
        eventlibrary.waterlevel = PlayerPrefs.GetInt("waterlevel");
        eventlibrary.baseatk = PlayerPrefs.GetInt("atk");
        eventlibrary.luck = PlayerPrefs.GetInt("luck");
        eventlibrary.health = PlayerPrefs.GetInt("hp");
        eventlibrary.pearls = PlayerPrefs.GetInt("pearls"); 
        eventlibrary.moonpearls = PlayerPrefs.GetInt("moonpearls"); 
        eventlibrary.suncoins = PlayerPrefs.GetInt("suncoins"); 
        eventlibrary.firedust = PlayerPrefs.GetInt("firedust");
        eventlibrary.waterdust = PlayerPrefs.GetInt("waterdust");
        eventlibrary.earthdust = PlayerPrefs.GetInt("earthdust");
        eventlibrary.firegem = (PlayerPrefs.GetInt("firegem") != 0);
        eventlibrary.watergem = (PlayerPrefs.GetInt("watergem") != 0);
        eventlibrary.earthgem = (PlayerPrefs.GetInt("earthgem") != 0);
        eventlibrary.firegemlvl = PlayerPrefs.GetInt("firegemlvl");
        eventlibrary.watergemlvl = PlayerPrefs.GetInt("watergemlvl");
        eventlibrary.earthgemlvl = PlayerPrefs.GetInt("earthgemlvl");
        eventlibrary.townchestwest = (PlayerPrefs.GetInt("townchestwest") != 0);
        eventlibrary.townchesteast = (PlayerPrefs.GetInt("townchesteast") != 0);
        eventlibrary.firsttimenish = (PlayerPrefs.GetInt("firsttimenish") != 0);
        eventlibrary.firsttimeimox = (PlayerPrefs.GetInt("firsttimeimox") != 0);
        eventlibrary.firsttimebalam = (PlayerPrefs.GetInt("firsttimebalam") != 0);
        eventlibrary.firsttimerajat = (PlayerPrefs.GetInt("firsttimerajat") != 0);
        eventlibrary.firsttimezazil = (PlayerPrefs.GetInt("firsttimezazil") != 0);

        loadmysore();
    }
}
