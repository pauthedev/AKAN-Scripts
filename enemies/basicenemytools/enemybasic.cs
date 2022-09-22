using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemybasic : MonoBehaviour
{
	//stats and element
    public float maxhealth;
    public float health;
    public GameObject slimedrop;
    public GameObject dustdrop;
    public int slimedecs;
    public int dustdecs;
    public int element;

    public bool dead;
    playercont pl;

    public int prob;

    AudioSource aud;
    public AudioClip deadsound;
    void Start()
    {
        aud = GetComponent<AudioSource>();
        health = maxhealth;
        pl = FindObjectOfType<playercont>();

        prob = Random.Range(1, 101);


        if (eventlibrary.waterlevel == 0)
        {
            slimedecs = Random.Range(4, 9);
            dustdecs = Random.Range(2, 6);
        }

        if (eventlibrary.waterlevel == 3)
        {
            slimedecs = Random.Range(4, 9);
            dustdecs = Random.Range(2, 6);
        }

        if (eventlibrary.waterlevel == 2)
        {
            slimedecs = Random.Range(8, 13);
            dustdecs = Random.Range(6, 11);
        }

        if (eventlibrary.waterlevel == 1)
        {
            slimedecs = Random.Range(14, 26);
            dustdecs = Random.Range(15, 31);
        }
        
    }

    void Update()
    {

        if (health < 0)
        {
            health = 0;
        }

        if (health <= 0 && !dead)
        {
            dead = true;



            GameObject drop = Instantiate(slimedrop);
            drop.gameObject.transform.position = transform.position;
            drop.GetComponent<shardspawn>().decs = slimedecs;
            Destroy(drop.gameObject, 0.5f);


		//rewards depending on your luck level
            if (pl.luck == 0 && prob > 70)
            {
                GameObject ddrop = Instantiate(dustdrop);
                ddrop.gameObject.transform.position = transform.position;
                ddrop.GetComponent<dustspawn>().decs = dustdecs;
                ddrop.GetComponent<dustspawn>().element = element;
                Destroy(ddrop.gameObject, 0.5f);
            }

            if (pl.luck == 1 && prob > 50)
            {
                GameObject ddrop = Instantiate(dustdrop);
                ddrop.gameObject.transform.position = transform.position;
                ddrop.GetComponent<dustspawn>().decs = dustdecs;
                ddrop.GetComponent<dustspawn>().element = element;
                Destroy(ddrop.gameObject, 0.5f);
            }

            if (pl.luck == 2 && prob > 25)
            {
                GameObject ddrop = Instantiate(dustdrop);
                ddrop.gameObject.transform.position = transform.position;
                ddrop.GetComponent<dustspawn>().decs = dustdecs;
                ddrop.GetComponent<dustspawn>().element = element;
                Destroy(ddrop.gameObject, 0.5f);
            }

            if (pl.luck == 3)
            {
                GameObject ddrop = Instantiate(dustdrop);
                ddrop.gameObject.transform.position = transform.position;
                ddrop.GetComponent<dustspawn>().decs = dustdecs;
                ddrop.GetComponent<dustspawn>().element = element;
                Destroy(ddrop.gameObject, 0.5f);
            }

            aud.PlayOneShot(deadsound);
            Destroy(gameObject, 1f);

        }
    }

    public void hurted(float damage)
    {
        health -= damage;
    }
}
