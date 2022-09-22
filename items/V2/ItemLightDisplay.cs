using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class ItemLightDisplay : MonoBehaviour
{
    public ItemObject item;
    public Text objname;
    public Text desc;
    public InventoryObject inventory;
    public int iteminlist;
    public bool done;

    Volume scenevolume;
    Vignette vignette;

    public GameObject button;
    void Start()
    {
        scenevolume = FindObjectOfType<Volume>();
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

    public void lightup()
    {
        if (!done)
        {
            done = true;
            inventory.Container[iteminlist].amount -= 1;
            
            if (scenevolume.profile.TryGet<Vignette>(out vignette))
            {
                    vignette.intensity.value = 0f;
                    vignette.smoothness.value = 0f;
            }
        }
    }
}
