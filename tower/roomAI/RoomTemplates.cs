using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{//types
    public GameObject[] UpRooms;
    public GameObject[] DownRooms;
    public GameObject[] RightRooms;
    public GameObject[] LeftRooms;
    public GameObject ClosedRoom;
//active rooms
    public List<GameObject> rooms;

    public List<GameObject> enemyrooms;

    public List<GameObject> treasurerooms;

    public List<GameObject> pacerooms;
//floor elements
    public float waittime;
    public bool spawnedstairs;

    public GameObject floorend;
    public GameObject enemyspawner;
    public GameObject shop;
    public GameObject imoxmesh;

    public GameObject chest;

    public int shoproom;

    public int max;

    public static int currentRoom;
    void Start()
    {//change number of rooms with progress
        if (eventlibrary.waterlevel == 3)
        {
            max = 1;
        }

        if (eventlibrary.waterlevel == 2)
        {
            max = 3;
        }

        if (eventlibrary.waterlevel == 1)
        {
            max = 4;
        }
	/*
        if (eventlibrary.waterlevel == 0)
        {
            max = 4;
        }*/
    }

    void Update()
    {
//double rooms solving
        waittime -= Time.deltaTime;
        if (waittime < 0.6 && waittime > 0.4 && !spawnedstairs)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i != 0)
                {
                    rooms[i].SetActive(false);
                }
               
            }
        }

        if (waittime < 0.35 && waittime > 0.15 && !spawnedstairs)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i != 0)
                {
                    rooms[i].SetActive(true);
                }
            }
        }
//room element integration
        if (waittime <= 0 &&!spawnedstairs)
        {


            for (int i =0; i < rooms.Count; i++)
            {
                if (i == rooms.Count-1)
                {
                    Instantiate(floorend, rooms[i].transform.position, Quaternion.identity);
                    if (pacerooms.Count >= 1)
                    {
                        imox();
                    }
                    
                    spawnedstairs = true;
                }
            }

            for (int i = 0; i < enemyrooms.Count; i++)
            {
                Instantiate(enemyspawner, enemyrooms[i].transform.position, Quaternion.identity);
            }

            for (int i = 0; i < treasurerooms.Count; i++)
            {
                Instantiate(chest, new Vector3(treasurerooms[i].transform.position.x, 0.4f, treasurerooms[i].transform.position.z), Quaternion.identity);
            }


        }
        else
        {
            
        }
        
        if (spawnedstairs)
        {
            waittime = 0;
        }
    }

    void imox()
    {
        shoproom = Random.Range(0, pacerooms.Count - 1);

            if (pacerooms[shoproom].GetComponent<AddRoom>().u==false)
            {
                GameObject obj = Instantiate(shop, new Vector3(pacerooms[shoproom].transform.position.x, 0.4f, pacerooms[shoproom].transform.position.z + 7f), Quaternion.identity);
                imoxmesh.SetActive(true);
                imoxmesh.transform.position = new Vector3(obj.transform.position.x, 1.4f, obj.transform.position.z);
            }
    }
}
