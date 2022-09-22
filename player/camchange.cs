using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camchange : MonoBehaviour
{
    public int camposition;
    public inputs inputs;
    public GameObject vcam;
    Animator anim;
    public float camaxis;
    public bool able;
    playercont pl;
    void Start()
    {
        
        pl = FindObjectOfType<playercont>();
        inputs = GetComponent<inputs>();
        anim = vcam.GetComponent<Animator>();
        able = true;
        camposition = 1;
    }

    public void Update()
    {
        if (pl.gamepad)
        {
            camaxis = Input.GetAxis(inputs.c_arrows);
        }
        else
        {
            camaxis = Input.GetAxis(inputs.arrows);
        }
        
        
        if (camaxis !=0 && able&& !pl.isonevent)
        {
            able = false;
            if (camposition == 0)
            {

                if (camaxis > 0)
                {
                    
                    anim.Play("0to1");

                    Invoke("change1", 1f);
                }
            }
            if (camposition == 2)
            {
                if (camaxis < 0)
                {
                    
                    anim.Play("2to1");
                    Invoke("change1", 1f);

                }
            }
            if (camposition == 1)
            {
                
                if (camaxis > 0)
                {
                    anim.Play("1to2");
                    Invoke("change2", 1f);
                }

                if (camaxis < 0)
                {
                    anim.Play("1to0");
                    Invoke("change0", 1f);
                }
            }
            Invoke("change", 1.1f);
        }
    }

    void change()
    {
        able = true;
    }
    void change1()
    {
        camposition = 1;
    }
    void change2()
    {
        camposition = 2;
    }
    void change0()
    {
        camposition = 0;
    }
}
