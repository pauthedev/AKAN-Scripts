using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerController : MonoBehaviour
{
    public bool exist;
    RoomTemplates room;
    void Start()
    {
        room = FindObjectOfType<RoomTemplates>();
        Destroy(gameObject, 2f);
        Invoke("goexist", 0.05f);
    }

    void goexist()
    {
        exist = true;
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("spawnpoint"))
        {
            Destroy(other.gameObject);
            
        }

        if (other.CompareTag("destroyer"))
        {
            Destroy(gameObject,1f);

            if (exist && other.gameObject.GetComponent<DestroyerController>().exist ==false)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
                Debug.Log(other.gameObject.transform.parent.gameObject.name);
            }

            if (!exist && other.gameObject.GetComponent<DestroyerController>().exist == true)
            {
                Destroy(gameObject.transform.parent.gameObject);
                Debug.Log(gameObject.transform.parent.gameObject.name);
            }

        }
    }
}
