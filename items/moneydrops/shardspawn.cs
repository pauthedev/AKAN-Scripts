using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shardspawn : MonoBehaviour
{
    public Transform target;
    public GameObject prefab;
    public int decs;
    void Start()
    {
        target = FindObjectOfType<playercont>().gameObject.transform;
        for (int i = 0; i < decs; i++)
        {
            CreateOrb();
        }
        
    }


    void CreateOrb()
    {//make an instance and introduce the direction
        GameObject go = Instantiate(prefab);
        slimeshard slime = go.GetComponent<slimeshard>();
        slime.target = target;
        go.transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
    }
}
