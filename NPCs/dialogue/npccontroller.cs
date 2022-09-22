using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class npccontroller : MonoBehaviour
{
    public npcdata data;
    public dialoguedata dialogue;

    private TMP_Animated animatedText;


    void Start()
    {
        animatedText = dialogueui.instance.animatedText;
    }
}
