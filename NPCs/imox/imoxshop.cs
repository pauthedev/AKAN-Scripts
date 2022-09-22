using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imoxshop : MonoBehaviour
{
    playercont pl;
    public Text pearls;

    public InventoryObject inventory;
    public ItemObject seaweed;
    public ItemObject key;
    public ItemObject potion;

     int seamax;
     int keymax;
     int potmax;
    public Text l1;
    public Text l2;
    public Text l3;

    AudioSource aud;
    public AudioClip doit;
    void Start()
    {
        aud = GetComponent<AudioSource>();
        pl = FindObjectOfType<playercont>();
        seamax = 8;
        keymax = 2;
        potmax = 3;
}

    void Update()
    {
        pearls.text = pl.moonpearls.ToString();
        l1.text = seamax.ToString();
        l2.text = keymax.ToString();
        l3.text = potmax.ToString();
    }

    public void Seaweed()
    {
        if (pl.moonpearls >= 1 && seamax>0)
        {
            pl.moonpearls -= 1;
            seamax -= 1;
            inventory.AddItem(seaweed, 1);

        }
    }

    public void Key()
    {
        if (pl.moonpearls >= 3 && keymax > 0)
        {
            pl.moonpearls -= 3;
            keymax -= 1;
            inventory.AddItem(key, 1);

        }
    }

    public void Potion()
    {
        if (pl.moonpearls >= 6 && potmax > 0)
        {
            pl.moonpearls -= 6;
            potmax -= 1;
            inventory.AddItem(potion, 1);

        }
    }
    void yesaud()
    {
        aud.PlayOneShot(doit);
    }
}
