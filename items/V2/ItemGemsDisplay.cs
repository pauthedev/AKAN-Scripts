using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ItemGemsDisplay : MonoBehaviour
{
    public GemItem item;
    public Text objname;
    public Text desc;
    playercont pl;
    public GameObject button;
    void Start()
    {
        pl = FindObjectOfType<playercont>();
    }

    void Update()
    {
        objname = GameObject.FindGameObjectWithTag("obj_name").GetComponent<Text>();
        desc = GameObject.FindGameObjectWithTag("obj_desc").GetComponent<Text>();
        if (EventSystem.current.currentSelectedGameObject == button)
        {
            objname.text = item.itemName;
            desc.text = item.description;
        }
    }

    public void gem()
    {
        if (item.element == GemItem.gemtype.Fire)
        {
            if (pl.firegem == false)
            {
                pl.firegem = true;
                pl.watergem = false;
                pl.earthgem = false;
            }
            else
            {
                pl.firegem = false;
                pl.watergem = false;
                pl.earthgem = false;
            }
        }

        if (item.element == GemItem.gemtype.Water)
        {
            if (pl.watergem == false)
            {
                pl.firegem = false;
                pl.watergem = true;
                pl.earthgem = false;
            }
            else
            {
                pl.firegem = false;
                pl.watergem = false;
                pl.earthgem = false;
            }
        }

        if (item.element == GemItem.gemtype.Earth)
        {
            if (pl.earthgem == false)
            {
                pl.firegem = false;
                pl.watergem = false;
                pl.earthgem = true;
            }
            else
            {
                pl.firegem = false;
                pl.watergem = false;
                pl.earthgem = false;
            }
        }
    }
}
