using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using Cinemachine;
using UnityEngine.EventSystems;

public class dialogueui : MonoBehaviour
{
    public bool inDialogue;

    public static dialogueui instance;

    public CanvasGroup canvasGroup;
    public TMP_Animated animatedText;
    public Image nameBubble;
    public TextMeshProUGUI nameTMP;
    [Space]

    [Header("Dialogue")]
    public npccontroller currentVillager;

    public int dialogueIndex;

    public bool canExit;
    public bool nextDialogue;

    [Space]

    [Header("Cameras")]
    public GameObject gameCam;
    public GameObject dialogueCam;

    inputs inp;
    playercont pl;

    AudioSource aud;
    public AudioClip doit;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        aud = GetComponent<AudioSource>();
        pl= FindObjectOfType<playercont>();
        inp = FindObjectOfType<inputs>();
    }
    private void Update()
    {
        if ((Input.GetButtonDown(inp.interact) || Input.GetButtonDown(inp.c_interact)) && inDialogue)
        {
            yesaud();
            if (canExit)
            {
                CameraChange(false);
                Invoke("ResetState", 0.1f);
            }

            if (nextDialogue)
            {
                animatedText.ReadText(currentVillager.dialogue.conversationBlock[dialogueIndex]);
            }
            FinishDialogue();
        }
    }

    public void SetCharNameAndColor()
    {
        dialogueIndex = 0;
        nameTMP.text = currentVillager.data.villagerName;
        nameTMP.color = currentVillager.data.villagerNameColor;
        nameBubble.color = currentVillager.data.villagerColor;
        animatedText.ReadText(currentVillager.dialogue.conversationBlock[dialogueIndex]);
        canvasGroup.alpha = 1;
        inDialogue = true;
    }

    public void CameraChange(bool dialogue)
    {
        gameCam.SetActive(!dialogue);
        dialogueCam.SetActive(dialogue);
    }

    public void ClearText()
    {
        animatedText.text = string.Empty;
    }

    public void ResetState()
    {
        canvasGroup.alpha = 0;
        inDialogue = false;
        canExit = false;
        pl.isonevent = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void FinishDialogue()
    {//continues or ends dialogues
        if (dialogueIndex < currentVillager.dialogue.conversationBlock.Count-1)
        {
            dialogueIndex++;
            nextDialogue = true;
        }
        else
        {
            nextDialogue = false;
            canExit = true;
        }
    }

    void yesaud()
    {
        aud.PlayOneShot(doit);
    }
}
