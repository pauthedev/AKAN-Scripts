using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Rendering;

public class towncontroller : MonoBehaviour
{
    //objects that move during the game
    public GameObject water;
    public GameObject limits3;
    public GameObject limits2;
    public GameObject ship1;
    public GameObject nish;

    public fade faded;

    playercont pl;
    public InventoryObject inventory;
    public InventoryDisplay display;
    
    //changes on appearence
    public Material t1y;
    public Material t1n;
    public Material t2y;
    public Material t2n;
    public Material t3y;
    public Material t3n;

    public GameObject t1;
    public GameObject t2;
    public GameObject t3;

    public GameObject firet1y;
    public GameObject firet1n;
    public GameObject firet2y;
    public GameObject firet2n;
    public GameObject firet3y;
    public GameObject firet3n;
	//ui mission reminder
    public Text mission;
    dialogueui ui;
    void Start()
    {
        pl = FindObjectOfType<playercont>();
        ui = FindObjectOfType<dialogueui>();
        faded = GetComponent<fade>();
        display = FindObjectOfType<InventoryDisplay>();
        faded.fadeout();

        if (eventlibrary.waterlevel == 1)
        {
            water.transform.position = new Vector3(water.transform.position.x, -7.1f, water.transform.position.z);
            ship1.transform.position = new Vector3(-4.5f, -7.4f, 41f);
            nish.transform.position = new Vector3(2f, -5.607f, 40.72f);
            limits2.SetActive(false);
            limits3.SetActive(false);

            t1.GetComponent<Renderer>().material = t1n;
            t2.GetComponent<Renderer>().material = t2y;
            t3.GetComponent<Renderer>().material = t3y;

            firet1y.SetActive(false);
            firet1n.SetActive(true);
            firet2y.SetActive(true);
            firet2n.SetActive(false);
            firet3y.SetActive(true);
            firet3n.SetActive(false);

            mission.text = "Mission: Get ready and save Misore.";
        }
        if (eventlibrary.waterlevel == 2)
        {
            water.transform.position = new Vector3(water.transform.position.x, -3.45f, water.transform.position.z);
            ship1.transform.position = new Vector3(6f, -3.7f, 38f);
            nish.transform.position = new Vector3(10.4f, -1.85f, 32.5f);
            limits2.SetActive(true);
            limits3.SetActive(false);

            t1.GetComponent<Renderer>().material = t1n;
            t2.GetComponent<Renderer>().material = t2n;
            t3.GetComponent<Renderer>().material = t3y;

            firet1y.SetActive(false);
            firet1n.SetActive(true);
            firet2y.SetActive(false);
            firet2n.SetActive(true);
            firet3y.SetActive(true);
            firet3n.SetActive(false);

            mission.text = "Mission: The water flooded, explore the town and keep fighting.";
        }
        if (eventlibrary.waterlevel == 3)
        {
            water.transform.position = new Vector3(water.transform.position.x, -0.23f, water.transform.position.z);
            ship1.transform.position = new Vector3(4.5f, -0.5f, 33f);
            nish.transform.position = new Vector3(1.48f, 1.37f, 27.9f);
            limits2.SetActive(false);
            limits3.SetActive(true);

            t1.GetComponent<Renderer>().material = t1n;
            t2.GetComponent<Renderer>().material = t2n;
            t3.GetComponent<Renderer>().material = t3n;

            firet1y.SetActive(false);
            firet1n.SetActive(true);
            firet2y.SetActive(false);
            firet2n.SetActive(true);
            firet3y.SetActive(false);
            firet3n.SetActive(true);

            mission.text = "Mission: Talk with Soare and Nish.";
}
        if (eventlibrary.waterlevel == 0)
        {
            water.transform.position = new Vector3(water.transform.position.x, -7.1f, water.transform.position.z);
            ship1.transform.position = new Vector3(-4.5f, -7.4f, 41f);
            nish.transform.position = new Vector3(2f, -5.607f, 40.72f);
            limits2.SetActive(false);
            limits3.SetActive(false);

            t1.GetComponent<Renderer>().material = t1y;
            t2.GetComponent<Renderer>().material = t2y;
            t3.GetComponent<Renderer>().material = t3y;

            firet1y.SetActive(true);
            firet1n.SetActive(false);
            firet2y.SetActive(true);
            firet2n.SetActive(false);
            firet3y.SetActive(true);
            firet3n.SetActive(false);
        }
    }
    void Update()
    {
        pl.health = pl.maxhealth;
        if (ui.inDialogue || pl.isonevent)
        {
            mission.gameObject.SetActive(false);
        }
        else
        {
            mission.gameObject.SetActive(true);
        }
    }

    public void LoadDungeon()
    {//set the right floor and save all kinich stats
        faded.fadein();
        pl.isonevent = true;
        towercontroller.floor = 1;

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

        for (int i = 0; i < inventory.Container.Count; i++)
        {

            if (inventory.Container[i].amount == 0)
            {
                inventory.Container.Remove(inventory.Container[i]);
                Destroy(display.itemDisplayed[inventory.Container[i]]);
            }

        }

        inventory.Save();
        Invoke("gotower", 1.1f);

    }

    void gotower()
    {
        SceneManager.LoadSceneAsync("tower");
    }
}
