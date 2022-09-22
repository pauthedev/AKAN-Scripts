using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossfight : MonoBehaviour
{
    public float maxhealth;
    public float health;

    public bool dead;
    public GameObject wall;
    public GameObject gui;

    public Image hp;

    playercont pl;
    AudioSource aud;
    public AudioClip deadsound;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        health = maxhealth;
        pl = FindObjectOfType<playercont>();
    }

    void Update()
    {
        hp.fillAmount = health / maxhealth;

        if (health < 0)
        {
            health = 0;
        }

        if (health <= 0 && !dead)
        {
            dead = true;
            aud.PlayOneShot(deadsound);
            gui.SetActive(false);
            wall.SetActive(false);
            Destroy(gameObject, 1f);
        }

        if (!pl.gameispaused && !dead)
        {
            gui.SetActive(true);
        }
        else
        {
            gui.SetActive(false);
        }
    }

    public void hurted(float damage)
    {
        health -= damage;
    }
}
