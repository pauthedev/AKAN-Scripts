using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class lifecanva : MonoBehaviour
{
    playercont pl;
    public Image healthimg;

    public TextMeshProUGUI health;
    public TextMeshProUGUI maxhealth;

    public Sprite fire;
    public Sprite water;
    public Sprite earth;

    public Image gem;

    public CanvasGroup group;

    public dialogueui di;
    void Start()
    {
        di = FindObjectOfType<dialogueui>();
        pl = FindObjectOfType<playercont>();
    }

    void Update()
    {
        int hp = (int)pl.health;
        health.text = hp.ToString();
        int maxhp = (int)pl.maxhealth;
        maxhealth.text = maxhp.ToString();

        healthimg.fillAmount = pl.health / pl.maxhealth;

        if (pl.gameispaused || di.inDialogue)
        {
            group.alpha = 0;
        }

        if (!pl.firegem && !pl.watergem && !pl.earthgem)
        {
            gem.color= Color.black;
        }
        else
        {
            gem.color = Color.white;
        }

        if (pl.firegem)
        {
            gem.sprite = fire;
        }

        if (pl.watergem)
        {
            gem.sprite = water;
        }

        if (pl.earthgem)
        {
            gem.sprite = earth;
        }
    }
}
