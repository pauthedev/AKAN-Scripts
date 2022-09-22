using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chestcont : MonoBehaviour
{
    public inputs inp;
    public chestpopup pop;
    public GameObject shardspawner;
    public GameObject dustspawner;

    public int sdecs;
    public int edecs;
    Scene m_Scene;
    public bool west;

    public InventoryObject inventory;
    public ItemObject key;

    AudioSource aud;
    public AudioClip chest;
    playercont pl;
    void Start()
    {
        aud = GetComponent<AudioSource>();
        pl = FindObjectOfType<playercont>();
        inp = FindObjectOfType<inputs>();
        if (m_Scene.name == "misore")
        {
            if(!eventlibrary.townchestwest && west)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }

            if (!eventlibrary.townchesteast && !west)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
        else
        {
            if (eventlibrary.waterlevel == 0)
            {
                sdecs = 12;
                edecs = 10;
            }
            if (eventlibrary.waterlevel == 3)
            {
                sdecs = 10;
                edecs = 10;
            }

            if (eventlibrary.waterlevel == 2)
            {
                sdecs = 25;
                edecs = 25;
            }

            if (eventlibrary.waterlevel == 1)
            {
                sdecs = 50;
                edecs = 50;
            }
        }
        
    }

    void Update()
    {


            if ((Input.GetButtonDown(inp.interact) || Input.GetButtonDown(inp.c_interact)) && pop.canopen && !pl.gameispaused)
            {
                for (int i = 0; i < inventory.Container.Count; i++)
                {
                    if (inventory.Container[i].ID == 4 )
                    {
                        if(inventory.Container[i].amount > 0)
                        {
                            pop.canopen = false;
                            inventory.Container[i].amount -= 1;
                            GameObject sdrop = Instantiate(shardspawner);
                            sdrop.gameObject.transform.position = transform.position;
                            sdrop.GetComponent<shardspawn>().decs = sdecs;
                            Destroy(sdrop.gameObject, 0.5f);

                            GameObject fdrop = Instantiate(dustspawner);
                            fdrop.gameObject.transform.position = transform.position;
                            fdrop.GetComponent<dustspawn>().decs = edecs;
                            fdrop.GetComponent<dustspawn>().element = 1;
                            Destroy(fdrop.gameObject, 0.5f);

                            GameObject wdrop = Instantiate(dustspawner);
                            wdrop.gameObject.transform.position = transform.position;
                            wdrop.GetComponent<dustspawn>().decs = edecs;
                            wdrop.GetComponent<dustspawn>().element = 2;
                            Destroy(wdrop.gameObject, 0.5f);

                            GameObject edrop = Instantiate(dustspawner);
                            edrop.gameObject.transform.position = transform.position;
                            edrop.GetComponent<dustspawn>().decs = edecs;
                            edrop.GetComponent<dustspawn>().element = 3;
                            Destroy(edrop.gameObject, 0.5f);

                            if (m_Scene.name == "misore")
                            {
                                if (eventlibrary.townchestwest && west)
                                {
                                    eventlibrary.townchestwest = false;
                                }

                                if (eventlibrary.townchesteast && !west)
                                {
                                    eventlibrary.townchesteast = false;
                                }
                            }

                            aud.PlayOneShot(chest);
                            Destroy(gameObject.transform.parent.gameObject,1.4f);
                        }
                    }

                }

        }
        
    }
}
