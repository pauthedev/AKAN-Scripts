using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ItemHealDisplay : MonoBehaviour
{
    public HealItem item;
    public Text objname;
    public Text desc;
    public InventoryObject inventory;
    public int iteminlist;
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
        for (int i = 0; i < inventory.Container.Count; i++)
        {

            if (inventory.Container[i].item == item)
            {
                iteminlist = i;
            }

        }
    }

    public void consume()
    {
        if (inventory.Container[iteminlist].amount > 0 && pl.health < pl.maxhealth)
        {
            pl.health += item.healpower;
            inventory.Container[iteminlist].amount -= 1;
        }
    }
}
