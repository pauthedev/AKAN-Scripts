using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates rooms;
    public bool isRoom;
    public bool startend;
    public bool u;
    public bool d;
    public bool l;
    public bool r;
    public int random;
    public int r1;
    public int r2;
    public int thisobj;
    public int enemies;
    public bool fight;
    public int thisRoom;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rooms = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomTemplates>();
        rooms.rooms.Add(this.gameObject);
        thisobj = rooms.rooms.Count;
        r1 = Random.Range(1, 101);
        r2= Random.Range(1, 101);

        Invoke("check", 0.3f);
    }


    private void Update()
    {
        for (int i = 0; i < rooms.rooms.Count; i++)
        {
            if (rooms.rooms[i] == this.gameObject)
            {
                thisRoom = i;//know wich room is in list for character
            }
        }
        if (enemies <= 0)
        {
            fight = false;
        }
        else
        {
            fight = true;
        }
    }

    void check()
    {
        random = Mathf.Max(r1, r2);
        if (gameObject == rooms.rooms[rooms.rooms.Count - 1])
        {
            startend = true;
        }
        Invoke("roomtype", 0.2f);

    }

    void roomtype()
    {

        if (!startend)
        {
            if (random <= 20)
            {
                rooms.treasurerooms.Add(this.gameObject);
            }

            if (random > 20 && random <= 50)
            {
                rooms.pacerooms.Add(this.gameObject);
            }
            if (random > 50)
            {
                rooms.enemyrooms.Add(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isRoom = true;
            RoomTemplates.currentRoom = thisRoom;
        }

        if (other.tag == "roompref")
        {
            Debug.Log("check");
            AddRoom otheroom = other.GetComponent<AddRoom>();
            Debug.Log(otheroom.thisobj);

            if (thisobj < otheroom.thisobj)
                {
                    Debug.Log("destroy " + thisobj);
                    Destroy(gameObject);
                }
                if (thisobj > otheroom.thisobj)
                {
                    Debug.Log("destroy " + other.GetComponent<AddRoom>().thisobj);
                    Destroy(other.gameObject);
                }
            

        }
    }


    private void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
        {
            isRoom = false;
        }
    }


}
