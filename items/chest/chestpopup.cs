using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestpopup : MonoBehaviour
{
    public bool canopen;

    public GameObject popup;

    public Transform cam;

    void Update()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        popup.transform.LookAt(popup.transform.position + cam.forward);

        if (canopen)
        {
            popup.SetActive(true);
        }
        else
        {
            popup.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canopen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canopen = false;
        }
    }
}
