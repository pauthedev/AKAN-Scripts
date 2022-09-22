using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    public InventoryObject inventory;

    public int x_start;
    public int y_start;
    public int x_pacing;
    public int column;
    public int y_pacing;

    public int initcount;

    public Transform placehold;

    public Dictionary<InventorySlot, GameObject> itemDisplayed = new Dictionary<InventorySlot, GameObject>();

    public int actuallength;
    void Start()
    {
        inventory.Load();
        initcount = inventory.Container.Count;
        CreateDisplay();
        
    }

    void Update()
    {
        UpdateDisplay();
    }
        public void CreateDisplay()
    {
        //get every element from inventory and create a ui button w sprite, amount and description of it
        for (int i =0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.SetParent(placehold);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.GetComponent<RectTransform>().localPosition =GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            obj.GetComponentInChildren<Image>().sprite = inventory.Container[i].item.sprite;
            if (inventory.Container[i].amount == 1)
            {
                obj.GetComponentInChildren<TextMeshProUGUI>().color = new Color(024, 024, 024);
            }
            else
            {
                obj.GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 255, 255);
            }
            itemDisplayed.Add(inventory.Container[i], obj);
        }
        initcount = actuallength;
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(x_start+ (x_pacing*(i %column)),y_start+(-y_pacing * (i / column)),0f);
    }

    public void UpdateDisplay()
    {
        actuallength = inventory.Container.Count;
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemDisplayed.ContainsKey(inventory.Container[i]))
            {

                itemDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");

                if (inventory.Container[i].amount > 1)
                {
                    itemDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().alpha = 1;
                }
                else
                {
                    itemDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().alpha = 0;
                }
                

                if (inventory.Container[i].amount <= 0)
                {
                    itemDisplayed[inventory.Container[i]].SetActive(false) ;
                }
                else
                {
                    itemDisplayed[inventory.Container[i]].SetActive(true);
                }
                
            }
            if (actuallength > initcount)
            {
                foreach (Transform child in placehold.transform)
                {
                    GameObject.Destroy(child.gameObject);//only deletes the ui buttons
                }
                itemDisplayed.Clear();
                CreateDisplay();//create new ones
            }
        }
    }
}
