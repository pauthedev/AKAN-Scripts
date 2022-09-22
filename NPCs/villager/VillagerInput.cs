using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerInput : MonoBehaviour
{
    public npccontroller villager;
    public List<dialoguedata> dialogueslvl1;
    public List<dialoguedata> dialogueslvl2;
    public List<dialoguedata> dialogueslvl3;
    public List<dialoguedata> activedialogues;
    public dialoguedata firstdia;
    public int randomnumber;
    public float timer;
    dialogueui ui;
    towncontroller town;

    public int limit;
    public lookatcontroller villagerlook;
    public GameObject popup;
    public GameObject Options;


    void Start()
    {
        ui = FindObjectOfType<dialogueui>();
        villager = GetComponent<npccontroller>();
        town = FindObjectOfType<towncontroller>();

        

        activedialogues = dialogueslvl1;
    }

    void Update()
    {
        limit = activedialogues.Count;
        if (!eventlibrary.firsttimezazil && !ui.inDialogue)
        {//change random dialogue list on story progress

                timer += Time.deltaTime;
                /*if (eventlibrary.waterlevel == 0)
                {
                    activedialogues = dialogueslvl3;
                }*/
                if (eventlibrary.waterlevel == 1)
                {
                    activedialogues = dialogueslvl1;
                }
                if (eventlibrary.waterlevel == 2)
                {
                    activedialogues = dialogueslvl2;
                }
                if (eventlibrary.waterlevel == 3)
                {
                    activedialogues = dialogueslvl3;
                }

            if (timer >= 6)
            {
                randomnumber = Random.Range(0, limit);
                timer = 0;
            }

            

            villager.dialogue = activedialogues[randomnumber];

            if (villagerlook.follow)
            {
                popup.SetActive(false);
                Options.SetActive(false);
            }
        }

        if (eventlibrary.firsttimezazil)
        {
            villager.dialogue = firstdia;
        }


    }
}
