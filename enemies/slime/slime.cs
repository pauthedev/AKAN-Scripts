using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : MonoBehaviour
{
    public enum states
    {
        sleep,
        follow,
        atk,
        hurt,
        die,
    }

    public states sl_states;

    public enemybasic en;
    public int speed;
    public bool isonattack;
    public bool hurttrigger;
    public bool dead;
    float timer;
    public float damage;
    playercont pl;
    Transform target;
    Vector3 Movement;
    public Animator anim;
    public GameObject mesh;

    public float distance;
    public float range;

    Rigidbody rb;
    BoxCollider col;
    AddRoom thisroom;
    int room;
    bool set;
    bool enter;
    IEnumerator States()
    {
        switch (sl_states)
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
        pl = FindObjectOfType<playercont>();
        rb = GetComponent<Rigidbody>();
        anim = mesh.gameObject.GetComponent<Animator>();
        timer = Random.Range(1.3f, 1.6f);
        col = GetComponent<BoxCollider>();
        
    }

    void Update()
    {
        StartCoroutine(States());
        if (en.health <= 0)
        {
            sl_states = states.die;
        }
        if (set && !enter)
        {//add enemy to list of room enemies
            thisroom.enemies += 1;
            set = false;
            enter = true;
        }
        distance = Vector3.Distance(target.position, transform.position);
    }

    void sleep()
    {
        anim.SetBool("walk",false);
        target = transform;
        if (room == RoomTemplates.currentRoom &&!pl.isonevent)
        {//activate enemy actions once the character is in the room
            target = pl.transform;

            if (distance < range)
            {
                sl_states = states.atk;
            }
            else
            {
                sl_states = states.follow;
            }
            
        }


    }

    void follow()
    {
        anim.SetBool("walk",true);
        Vector3 dir = target.position - transform.position;

        Movement = Vector3.Lerp(Movement, dir * speed, Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        transform.position += Movement * Time.deltaTime;

        transform.forward = new Vector3( dir.x,0, dir.z);
//change of state from follow
        if (distance < range)
        {
            sl_states = states.atk;
        }

        if (pl.dead)
        {
            sl_states = states.sleep;
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
        
        anim.SetBool("walk",false);
        if (timer <= 0 && !isonattack && !pl.gameispaused && !pl.isonevent && !pl.dead)
        {//do unic attack
            anim.SetTrigger("attack");
            isonattack = true;
            rb.AddForce(transform.forward * 300, ForceMode.Acceleration);
            col.size = new Vector3(1.4f, 0.75f, 1.4f);
            Invoke("returnpos", 0.4f);
            timer = Random.Range(1.3f, 1.6f);

        }
//change of state from attack
        if (distance > range)
        {
            sl_states = states.follow;
        }

        if (pl.dead)
        {
            sl_states = states.sleep;
        }
    }

    void hurt()
    {
            if (!hurttrigger)
            {
                anim.SetTrigger("hurt");
                rb.AddForce(-transform.forward * 200, ForceMode.Acceleration);
                Invoke("returnatk", 0.3f);
                hurttrigger = true;

            }
    }

    void returnpos()
    {
        isonattack = false;
        rb.AddForce(-transform.forward * 300, ForceMode.Acceleration);
        col.size = new Vector3(0.85f, 0.75f, 0.85f);
    }
    void returnatk()
    {

        sl_states = states.atk;
        hurttrigger = false;
    }

    void die()
    {
        target = transform;
        if (!dead)
        {
            thisroom.enemies -= 1;
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
            
            if (en.health>0 && pl.attackmode)
            {
                sl_states = states.hurt;
                
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
            if (pl.hurted)
            {
                
            }
            else
            {
                pl.Hurt(damage);
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
