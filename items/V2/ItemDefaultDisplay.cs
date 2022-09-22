using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDefaultDisplay : MonoBehaviour
{
    public ItemObject item;
    public Text objname;
    public Text desc;
    public GameObject button;
    void Start()
    {
        
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
}
