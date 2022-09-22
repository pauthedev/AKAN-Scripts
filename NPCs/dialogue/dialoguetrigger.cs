using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;
using UnityEngine.Rendering.Universal;

public class dialoguetrigger : MonoBehaviour
{
    playercont pl;
    public bool cantalk;

    inputs inp;
    public CinemachineTargetGroup targetGroup;
    private dialogueui ui;
    npccontroller currentVillager;
    Transform targetVillager;

    public bool ismainchar;

    Volume scenevolume;
    DepthOfField depth;
    void Start()
    {
        targetVillager = null;
        currentVillager = null;
        ui = FindObjectOfType<dialogueui>();
        pl = FindObjectOfType<playercont>();
        scenevolume = FindObjectOfType<Volume>();
        if (scenevolume.profile.TryGet<DepthOfField>(out depth))
        {
            depth.focalLength.value = 1f;
        }
        inp = FindObjectOfType<inputs>();

    }

    void Update()
    {
        if (cantalk)
        {
            targetGroup.m_Targets[1].target = targetVillager;

            if ((Input.GetButtonDown(inp.interact) || Input.GetButtonDown(inp.c_interact)) && !ui.inDialogue && !pl.gameispaused)
            {//restart dialogue values for next character
                pl.isonevent = true;
                ui.ClearText();
                ui.SetCharNameAndColor();
                ui.currentVillager = currentVillager;
                ui.CameraChange(true);
            }
            
        }

        if (ismainchar && ui.inDialogue)
        {
            depth.focalLength.value = 300f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "npc"|| other.tag == "main")
        {

            currentVillager = other.gameObject.GetComponent<npccontroller>();
            targetVillager = other.gameObject.transform;
            ui.currentVillager = currentVillager;

            if (other.tag == "main")
            {
                ismainchar = true;
            }
        }
        if (other.tag == "talk")
        {
            cantalk = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "npc" || other.tag == "main")
        {
            ui.currentVillager = null;
            currentVillager = null;
            targetVillager = null;

            if (other.tag == "main")
            {
                ismainchar=false;
            }
        }

        if (other.tag == "talk")
        {
            cantalk = false;
        }
    }
}
