using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rajatshop : MonoBehaviour
{
    playercont pl;
    public Text coins;

    public InventoryObject inventory;
    public ItemObject seaweed;
    public ItemObject lantern;
    public ItemObject fish;

    AudioSource aud;
    public AudioClip doit;
    void Start()
    {
        aud = GetComponent<AudioSource>();
        pl = FindObjectOfType<playercont>();
    }

    void Update()
    {
        coins.text = pl.suncoins.ToString();
    }

    public void Seaweed()
    {
        if (pl.suncoins >= 2)
        {
            yesaud();
            pl.suncoins -= 2;
            inventory.AddItem(seaweed, 1);

        }
    }

    public void Lantern()
    {
        if (pl.suncoins >= 5)
        {
            yesaud();
            pl.suncoins -= 5;
            inventory.AddItem(lantern, 1);

        }
    }

    public void Fish()
    {
        if (pl.suncoins >= 4)
        {
            yesaud();
            pl.suncoins -= 4;
            inventory.AddItem(fish, 1);

        }
    }
    void yesaud()
    {
        aud.PlayOneShot(doit);
    }
}
