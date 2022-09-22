using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camrender : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public int limit;

    void Update()
    {
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, transform.forward, 6.0f);

       

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.tag != "cull")
            {
                limit++;
            }
            else
            {
                Debug.Log(hit[i].collider);
            }

            if (hit[i].collider.gameObject.GetComponent<Renderer>() && hit[i].collider.tag == "cull")
            {
                limit = 0;
                if (hit[i].collider.gameObject.GetComponent<Renderer>().enabled == true)
                {
                    gameObjects.Add(hit[i].collider.gameObject);
                    hit[i].collider.gameObject.GetComponent<Renderer>().enabled = false;
                }
            }
            
        }
        for (int i = 0; i < gameObjects.Count; i++)
        {
            
            if (limit == hit.Length)
            {
                gameObjects[i].GetComponent<Renderer>().enabled = true;

                gameObjects.Remove(gameObjects[i]);
            }
        }

    }
}
