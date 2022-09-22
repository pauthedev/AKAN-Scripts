using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class forgecontroller : MonoBehaviour
{
    playercont pl;

    public Text shards;
    public Text coins;
    public Text pearls;
    public Text firedust;
    public Text waterdust;
    public Text earthdust;

    public Text firedustreq;
    public Text waterdustreq;
    public Text earthdustreq;
    public Text firesunreq;
    public Text watersunreq;
    public Text earthsunreq;

    public int firedustcost;
    public int waterdustcost;
    public int earthdustcost;
    public int firesuncost;
    public int watersuncost;
    public int earthsuncost;

    public Text sunreq;
    public Text moonreq;

    public int suncost;
    public int mooncost;


    public InventoryObject inventory;
    public ItemObject firegem;
    public ItemObject watergem;
    public ItemObject earthgem;

    AudioSource aud;
    public AudioClip doit;
    void Start()
    {
        aud = GetComponent<AudioSource>();
        pl = FindObjectOfType<playercont>();
    }


    void Update()
    {
        shards.text = pl.pearls.ToString();
        coins.text = pl.suncoins.ToString();
        pearls.text = pl.moonpearls.ToString();
        firedust.text = pl.firedust.ToString();
        waterdust.text = pl.waterdust.ToString();
        earthdust.text = pl.earthdust.ToString();

        firedustreq.text = firedustcost.ToString();
        waterdustreq.text = waterdustcost.ToString();
        earthdustreq.text = earthdustcost.ToString();
        firesunreq.text = firesuncost.ToString();
        watersunreq.text = watersuncost.ToString();
        earthsunreq.text = earthsuncost.ToString();

        sunreq.text = suncost.ToString();
        moonreq.text =mooncost.ToString();


            if (pl.firelvl == 0)
            {
                firedustcost = 50;
                firesuncost = 5;
            }
            if (pl.firelvl == 1)
            {
                firedustcost = 200;
                firesuncost = 15;
            }
            if (pl.firelvl == 2)
            {
                firedustcost = 400;
                firesuncost = 30;
            }
            if (pl.firelvl == 3)
            {
                firedustreq.gameObject.SetActive(false);
                firesunreq.gameObject.SetActive(false);
            }



            if (pl.waterlvl == 0)
            {
                waterdustcost = 50;
                watersuncost = 5;
            }
            if (pl.waterlvl == 1)
            {
                waterdustcost = 200;
                watersuncost = 15;
            }
            if (pl.waterlvl == 2)
            {
                waterdustcost = 400;
                watersuncost = 30;
            }
            if (pl.waterlvl == 3)
            {
                waterdustreq.gameObject.SetActive(false);
                watersunreq.gameObject.SetActive(false);
            }



            if (pl.earthlvl == 0)
            {
                earthdustcost = 50;
                earthsuncost = 5;
            }
            if (pl.earthlvl == 1)
            {
                earthdustcost = 200;
                earthsuncost = 15;
            }
            if (pl.earthlvl == 2)
            {
                earthdustcost = 400;
                earthsuncost = 30;
            }
            if (pl.earthlvl == 3)
            {
                earthdustreq.gameObject.SetActive(false);
                earthsunreq.gameObject.SetActive(false);
            }

    }
    void yesaud()
    {
        aud.PlayOneShot(doit);
    }

    public void Fire()
    {
        

        if (pl.suncoins >= firesuncost && pl.firedust >= firedustcost && pl.firelvl != 3)
        {
            if (pl.firelvl == 0)
            {
                inventory.AddItem(firegem, 1);
                pl.suncoins -= firesuncost;
                pl.firedust -= firedustcost;
                pl.firelvl += 1;
                yesaud();
            }
            else
            {
                pl.suncoins -= firesuncost;
                pl.firedust -= firedustcost;
                pl.firelvl += 1;
                yesaud();
            }
            

        }
       

        

    }

    public void Water()
    {
        if (pl.suncoins >= watersuncost && pl.waterdust >= waterdustcost && pl.waterlvl != 3)
        {
            if (pl.waterlvl == 0)
            {
                inventory.AddItem(watergem, 1);
                pl.suncoins -= watersuncost;
                pl.waterdust -= waterdustcost;
                pl.waterlvl += 1;
                yesaud();
            }
            else
            {
                pl.suncoins -= watersuncost;
                pl.waterdust -= waterdustcost;
                pl.waterlvl += 1;
                yesaud();
            }
            
        }
        
    }

    public void Earth()
    {
        if (pl.suncoins >= earthsuncost && pl.earthdust >= earthdustcost && pl.earthlvl != 3)
        {
            if (pl.earthlvl == 0)
            {
                inventory.AddItem(earthgem, 1);
                pl.suncoins -= earthsuncost;
                pl.earthdust -= earthdustcost;
                pl.earthlvl += 1;
                yesaud();
            }
            else
            {
                pl.suncoins -= earthsuncost;
                pl.earthdust -= earthdustcost;
                pl.earthlvl += 1;
                yesaud();
            }
            
        }
    }

    public void Sun()
    {
        if (pl.pearls >= suncost)
        {
            pl.pearls -= suncost;
            pl.suncoins += 1;
            yesaud();
        }
    }

    public void Moon()
    {
        if (pl.pearls >= mooncost)
        {
            pl.pearls -= mooncost;
            pl.moonpearls += 1;
            yesaud();
        }
    }

}
