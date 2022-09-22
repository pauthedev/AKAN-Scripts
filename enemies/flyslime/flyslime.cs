using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyslime : MonoBehaviour
{
    public enum states
    {
        sleep,
        follow,
        atk,
        hurt,
        die,
    }

    public states fly_states;

    public enemybasic en;
    public float speed;
    public bool isonattack;
    public bool hurttrigger;
    public bool dead;
    public float timer;
    public float damage;
    playercont pl;
    Transform target;
    Vector3 Movement;
    public Animator anim;
    public GameObject mesh;

    public float distance;
    public float range;

    Rigidbody rb;
    AddRoom thisroom;
    int room;
    bool set;
    bool enter;
    public GameObject spawnpoint;
    public GameObject projectile;
    Collider col;
    IEnumerator States()
    {
        switch (fly_states)
        {
            case states.sleep:
                sleep();
                break;
            case states.follow:
                follow();
                break;
            case states.atk:
                atk();
                break;
            case states.hurt:
                hurt();
                break;
            case states.die:
                die();
                break;
        }
        yield return 0;
    }

    void Start()
    {
        col = GetComponent<Collider>();
        pl = FindObjectOfType<playercont>();
        rb = GetComponent<Rigidbody>();
        anim = mesh.gameObject.GetComponent<Animator>();
        timer = Random.Range(2.1f, 2.4f);
    }

    void Update()
    {
        StartCoroutine(States());
        if (en.health <= 0)
        {
            fly_states = states.die;
        }
        if (set && !enter)
        {//add to room enemies list
            thisroom.enemies += 1;
            set = false;
            enter = true;
        }
        distance = Vector3.Distance(target.position, transform.position);
    }

    void sleep()
    {
        anim.SetBool("move", false);
        target = transform;

        if (room == RoomTemplates.currentRoom && !pl.isonevent)
        {//activate normal routines
            target = pl.transform;

            if (distance < range)
            {
                fly_states = states.atk;
            }
            else
            {
                fly_states = states.follow;
            }

        }


    }

    void follow()
    {

        anim.SetBool("move", true);
        target = pl.transform;
        Vector3 dir = target.position - transform.position;

        Movement = Vector3.Lerp(Movement, dir * speed, Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        transform.position += Movement * Time.deltaTime;

        transform.forward = new Vector3(dir.x, 0, dir.z);
//change of state from follow
        if (distance < range)
        {
            fly_states = states.atk;
        }

        if (pl.dead)
        {
            fly_states = states.sleep;
        }


    }

    void atk()
    {
        Vector3 dir = target.position - transform.position;
        transform.forward = new Vector3(dir.x, 0, dir.z);
        if (!isonattack)
        {
            timer -= Time.deltaTime;
        }

        anim.SetBool("move", false);
        if (timer <= 0 && !isonattack)
        {//spawn projectile animation
            anim.SetTrigger("atk");
            isonattack = true;

            

            Invoke("returnpos", 0.5f);
            timer = Random.Range(2.1f, 2.4f);

        }
//change of state from attack
        if (distance > range && !isonattack)
        {
            fly_states = states.follow;
        }

        if (pl.dead)
        {
            fly_states = states.sleep;
        }
    }

    void hurt()
    {
        if (!hurttrigger)
        {
            anim.SetTrigger("hurt");
            Invoke("returnatk", 0.3f);
            hurttrigger = true;

        }
    }

    void returnpos()
    {//create projectile and face it to the character
        GameObject obj = Instantiate(projectile);

        obj.gameObject.transform.forward = spawnpoint.transform.forward;
        obj.gameObject.transform.position = spawnpoint.transform.position;

        obj.GetComponent<flyprojectile>().element = en.element;
        obj.GetComponent<flyprojectile>().damage = damage * 2;
        isonattack = false;
    }
    void returnatk()
    {

        fly_states = states.atk;
        hurttrigger = false;
    }

    void die()
    {
        target = transform;
        if (!dead)
        {
            thisroom.enemies -= 1;//get enemy out of list
            anim.SetTrigger("die");
            StartCoroutine(Scale());
            col.enabled = false;
            dead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sword")
        {
            if (en.health > 0 && pl.attackmode)
            {
                fly_states = states.hurt;
                
            }

        }

        if (other.tag == "roompref")
        {
            set = true;
            thisroom = other.gameObject.GetComponent<AddRoom>();
            room = thisroom.thisRoom;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (pl.hurted || isonattack)
            {

            }
            else
            {
                //pl.Hurt(damage);decided to do not damage by contact
            }

        }
    }

    IEnumerator Scale()
    {
        float timer = 1f;

        while (timer > 0)
        {
            timer -= Time.deltaTime * 2;
            mesh.gameObject.transform.localScale = new Vector3(timer, timer, timer);
            yield return null;
        }
        timer = 0;

    }
}
