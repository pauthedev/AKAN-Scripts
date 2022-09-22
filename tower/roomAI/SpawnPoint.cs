using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int directionspawn;
    //1 ->  up
    //2 ->  down
    //3 ->  right
    //4 ->  left

    private RoomTemplates rooms;
    public int rand;

    public bool spawned;

    public bool dungeonend;

    void Start()
    {
        rooms = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
        Destroy(gameObject,3f);

    }

    void Spawn()
    {
        if (rooms.rooms.Count >= rooms.max)
        {
            dungeonend = true;
        }
        if (!spawned)
        {
            if (directionspawn == 1)
            {
                rand = Random.Range(0, rooms.DownRooms.Length);
                if (dungeonend)
                {
                    Instantiate(rooms.DownRooms[0], transform.position, rooms.DownRooms[0].transform.rotation);
                }
                else
                {
                    Instantiate(rooms.DownRooms[rand], transform.position, rooms.DownRooms[rand].transform.rotation);
                }
                
            }
            else if (directionspawn == 2)
            {
                rand = Random.Range(0, rooms.UpRooms.Length);
                if (dungeonend)
                {
                    Instantiate(rooms.UpRooms[0], transform.position, rooms.UpRooms[0].transform.rotation);
                }
                else
                {
                    Instantiate(rooms.UpRooms[rand], transform.position, rooms.UpRooms[rand].transform.rotation);
                }
                
            }
            else if (directionspawn == 3)
            {
                rand = Random.Range(0, rooms.LeftRooms.Length);
                if (dungeonend)
                {
                    Instantiate(rooms.LeftRooms[0], transform.position, rooms.LeftRooms[0].transform.rotation);
                }
                else
                {
                    Instantiate(rooms.LeftRooms[rand], transform.position, rooms.LeftRooms[rand].transform.rotation);
                }
                
            }
            else if (directionspawn == 4)
            {
                rand = Random.Range(0, rooms.RightRooms.Length);
                if (dungeonend)
                {
                    Instantiate(rooms.RightRooms[0], transform.position, rooms.RightRooms[0].transform.rotation);
                }
                else
                {
                    Instantiate(rooms.RightRooms[rand], transform.position, rooms.RightRooms[rand].transform.rotation);
                }
                
            }
            spawned = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("spawnpoint"))
        {

            if (other.GetComponent<SpawnPoint>().spawned==false && spawned == false)
            {
                Instantiate(rooms.ClosedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
                
            }
            spawned = true;

        }

        if (other.CompareTag("roompref"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("destroyer"))
        {
            Destroy(gameObject);
        }
    }
}
