using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawn : MonoBehaviour
{
    public List<GameObject> slimes;
    public List<GameObject> flies;

    public int slimehp;
    public int slimedamage;
    public int flyhp;
    public int flydamage;

    public int enemies;

    public int random;
    public int r1;
    public int r2;
    public int element;

    public Vector3 en_position;
    void Start()
    {//set different values for enemy stats depending on stage
        Destroy(gameObject, 2f);
        if (eventlibrary.waterlevel == 0)
        {
            slimehp = 50;
            flyhp = 50;
            slimedamage = 2;
            flydamage = 5;
            enemies = Random.Range(1, 3);
        }

        if (eventlibrary.waterlevel == 3)
        {
            slimehp = 50;
            flyhp = 50;
            slimedamage = 2;
            flydamage = 5;
            enemies = Random.Range(1, 3);
        }

        if (eventlibrary.waterlevel == 2)
        {
            slimehp = 80;
            flyhp = 80;
            slimedamage = 3;
            flydamage = 8;
            enemies = Random.Range(1, 4);
        }

        if (eventlibrary.waterlevel == 1)
        {
            slimehp = 120;
            flyhp = 120;
            slimedamage = 7;
            flydamage = 15;
            enemies = Random.Range(2, 5);
        }
        Invoke("spawn", 0.05f);
    }

    void spawn()
    {
        for (int i = enemies; i > 0 ; i--)
        {
            
            r1 = Random.Range(1, 101);
            r2 = Random.Range(1, 101);
            random = Mathf.Max(r1, r2);//gets the bigger of two numbers increasing the prob for higger ones
            element= Random.Range(0, 3);

            if (i==1)
            {
                en_position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
            }
            if (i == 2)
            {
                en_position = new Vector3(gameObject.transform.position.x -4, 0, gameObject.transform.position.z + 4);
            }
            if (i == 3)
            {
                en_position = new Vector3(gameObject.transform.position.x + 4, 0, gameObject.transform.position.z + 4);
            }
            if (i == 4)
            {
                en_position = new Vector3(gameObject.transform.position.x - 4, 0, gameObject.transform.position.z - 4);
            }
            if (i == 5)
            {
                en_position = new Vector3(gameObject.transform.position.x + 4, 0, gameObject.transform.position.z - 4);
            }


            if (random > 40)
            {
                GameObject en = Instantiate(slimes[element], new Vector3(en_position.x, 0.4f, en_position.z), Quaternion.identity);
                en.GetComponent<enemybasic>().maxhealth = slimehp;
                en.GetComponent<slime>().damage = slimedamage;
            }
            else
            {
                GameObject en = Instantiate(flies[element], new Vector3(en_position.x, 1.2f, en_position.z), Quaternion.identity);
                en.GetComponent<enemybasic>().maxhealth = flyhp;
                en.GetComponent<flyslime>().damage = flydamage;
            }

        }
    }
}
