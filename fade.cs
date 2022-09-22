using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{
    public Animator anim;
    public GameObject chargeimage;
    void Start()
    {
        anim = GetComponent<Animator>();
        chargeimage.SetActive(false);
    }
    public void fadeout()
    {
        anim.Play("fadeout");
    }

    public void fadein()
    {
        chargeimage.SetActive(true);
        anim.Play("fadein");
    }
}
