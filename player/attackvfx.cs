using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class attackvfx : MonoBehaviour
{
    public VisualEffect vfx;
    playercont pl;

    [ColorUsage(true, true)]
    public Color fire;
    [ColorUsage(true, true)]
    public Color water;
    [ColorUsage(true, true)]
    public Color earth;
    void Start()
    {
        pl = FindObjectOfType<playercont>();
        Destroy(gameObject, 1f);
    }

    void Update()
    {//change vfx color depending on kinich gem power
        if (pl.firegem)
        {
            vfx.SetVector4("color", fire);
        }

        if (pl.watergem)
        {
            vfx.SetVector4("color", water);
        }

        if (pl.earthgem)
        {
            vfx.SetVector4("color", earth);
        }
    }
}
