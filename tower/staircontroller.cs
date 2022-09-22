using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class staircontroller : MonoBehaviour
{
    inputs inp;
    public bool godown;
    public GameObject popup;
    playercont pl;
    public fade fadeimg;
    public InventoryObject inventory;
    public InventoryDisplay display;
    void Start()
    {
        pl = FindObjectOfType<playercont>();
        display = FindObjectOfType<InventoryDisplay>();
        inp = FindObjectOfType<inputs>();
        fadeimg = FindObjectOfType<fade>();
        
    }

    void Update()
    {
        if (godown)
        {
            if (Input.GetButtonDown(inp.interact)|| Input.GetButtonDown(inp.c_interact) && !pl.gameispaused)
            {
                godown = false;
                DownStairs();
            }
        }
        else
        {
            popup.SetActive(false);
        }
    }

    void DownStairs()
    {//save all kinich data before going down
        pl.isonevent = true;
        towercontroller.floor += 1;
        fadeimg.fadein();

        eventlibrary.baseatk = pl.baseatk;
        eventlibrary.luck = pl.luck;
        eventlibrary.health = pl.health;

        eventlibrary.pearls = pl.pearls;
        eventlibrary.moonpearls = pl.moonpearls;
        eventlibrary.suncoins = pl.suncoins;

        eventlibrary.firedust = pl.firedust;
        eventlibrary.earthdust = pl.earthdust;
        eventlibrary.waterdust = pl.waterdust;

        eventlibrary.firegem = pl.firegem;
        eventlibrary.firegemlvl = pl.firelvl;
        eventlibrary.watergem = pl.watergem;
        eventlibrary.watergemlvl = pl.waterlvl;
        eventlibrary.earthgem = pl.earthgem;
        eventlibrary.earthgemlvl = pl.earthlvl;

        for (int i = 0; i < inventory.Container.Count; i++)
        {

            if (inventory.Container[i].amount == 0)
            {
                inventory.Container.Remove(inventory.Container[i]);
                Destroy(display.itemDisplayed[inventory.Container[i]]);
            }

        }

        inventory.Save();

        if (eventlibrary.waterlevel == 0)
        {
            if (towercontroller.floor > 3)
            {
                Invoke("goEnd", 1f);
            }
            else
            {
                Invoke("goNext", 1f);
            }
        }

        if (eventlibrary.waterlevel == 3)
        {
            if (towercontroller.floor > 3)
            {
                Invoke("goEnd", 1f);
            }
            else
            {
                Invoke("goNext", 1f);
            }
        }

        if (eventlibrary.waterlevel == 2)
        {
            if (towercontroller.floor > 7)
            {
                Invoke("goEnd", 1f);
            }
            else
            {
                Invoke("goNext", 1f);
            }
        }

        if (eventlibrary.waterlevel == 1)
        {
            if (towercontroller.floor > 9)
            {
                Invoke("goEnd", 1f);
            }
            else
            {
                Invoke("goNext", 1f);
            }
        }
    }

    void goEnd()
    {
        if (eventlibrary.waterlevel == 1)
        {
            SceneManager.LoadSceneAsync("D1");
        }

        if (eventlibrary.waterlevel == 2)
        {
            SceneManager.LoadSceneAsync("D2");
        }

        if (eventlibrary.waterlevel == 3)
        {
            SceneManager.LoadSceneAsync("D3");
        }

        if (eventlibrary.waterlevel == 0)
        {
            SceneManager.LoadSceneAsync("D3");
        }
    }

    void goNext()
    {
        SceneManager.LoadSceneAsync("tower");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popup.SetActive(true);
            godown = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            godown = false;
        }
    }
}
