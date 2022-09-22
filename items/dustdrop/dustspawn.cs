using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dustspawn : MonoBehaviour
{
    public Transform target;
    public GameObject prefab;
    public int decs;
    public int element;
    
    void Start()
    {
        target = FindObjectOfType<playercont>().gameObject.transform;
        for (int i = 0; i < decs; i++)
        {
            CreateDust();
        }

    }


    void CreateDust()
    {
        GameObject go = Instantiate(prefab);
        elementdust dust = go.GetComponent<elementdust>();
        //go.transform.SetParent(gameObject.transform);
        go.transform.position = transform.position;
        dust.element = element;
        dust.target = target;
        
    }
}
