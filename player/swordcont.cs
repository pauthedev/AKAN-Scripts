using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class swordcont : MonoBehaviour
{
    playercont pl;
    public float damage;

    public GameObject vfx;
    public GameObject gem;
    Renderer rend;

    public Material m_fire;
    public Material m_water;
    public Material m_earth;

    void Start()
    {
        pl = FindObjectOfType<playercont>();
        rend = gem.GetComponent<Renderer>();
    }

    void Update()
    {
            if (pl.combo == 0)
            {
                damage = pl.damage1;
            }

            if (pl.combo == 1)
            {
                damage = pl.damage2;
            }

            if (pl.combo == 2)
            {
                damage = pl.damage3;
            }

        if (pl.firegem)
        {
            rend.material = m_fire;
        }

        if (pl.watergem)
        {
            rend.material = m_water;
        }

        if (pl.earthgem)
        {
            rend.material = m_earth;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            if (pl.attackmode)
            {
                other.gameObject.GetComponent<enemybasic>().hurted(damage);
                Instantiate(vfx, transform.position, Quaternion.identity);
            }
        }

        if (other.tag == "boss")
        {
            if (pl.attackmode)
            {
                other.gameObject.GetComponent<bossfight>().hurted(damage);
                Instantiate(vfx, transform.position, Quaternion.identity);
            }
        }
    }
}
