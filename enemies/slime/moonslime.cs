using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moonslime : MonoBehaviour
{//copycat with size difference from normal
    public enum states
    {
        sleep,
        follow,
        atk,
        hurt,
        die,
    }

    public states sl_states;

    public bossfight boss;
    public float speed;
    public bool isonattack;
    public bool hurttrigger;
    public bool dead;
    public float timer;
    public float damage;
    playercont pl;
    public Transform target;
    Vector3 Movement;
    public Animator anim;
    public GameObject mesh;

    public float distance;

    public float range;
    CapsuleCollider col;

    Rigidbody rb;
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
        target = pl.transform;
        anim = mesh.gameObject.GetComponent<Animator>();
        timer = Random.Range(2.6f, 2.9f);
        sl_states = states.follow;
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        StartCoroutine(States());
        if (boss.health <= 0)
        {
            sl_states = states.die;
        }
        distance = Vector3.Distance(target.position, transform.position);
    }

    void sleep()
    {
        anim.SetBool("walk", false);
        target = transform;
    }

    void follow()
    {
        anim.SetBool("walk", true);
        target = pl.transform;
        Vector3 dir = target.position - transform.position;

        Movement = Vector3.Lerp(Movement, dir * speed, Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        transform.position += Movement * Time.deltaTime;

        transform.forward = new Vector3(dir.x, 0, dir.z);

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
        target = pl.transform;
        Vector3 dir = target.position - transform.position;
        transform.forward = new Vector3(dir.x, 0, dir.z);
        
        if (isonattack==false)
        {
            timer -= Time.deltaTime;
        }

        anim.SetBool("walk", false);
        if (timer <= 0 && !isonattack &&!pl.isonevent)
        {
            anim.SetTrigger("attack");
            isonattack = true;
            
            col.radius = 0.6f;
            Invoke("returnpos", 0.4f);
            timer = Random.Range(2.6f, 2.9f);

        }
        if (distance > range)
        {
            sl_states = states.follow;
        }
        /*if (Mathf.Abs(transform.position.x - target.transform.position.x) > range && Mathf.Abs(transform.position.z - target.transform.position.z) > range && !isonattack)
        {
            sl_states = states.follow;
        }*/

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
            rb.AddForce(-transform.forward * 300, ForceMode.Acceleration);
            Invoke("returnatk", 0.3f);
            hurttrigger = true;

        }
    }

    void returnpos()
    {
        isonattack = false;
        col.radius = 0.42f;
        
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
            anim.SetTrigger("die");
            StartCoroutine(Scale());
            dead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sword")
        {

            if (boss.health > 0 && pl.attackmode && !isonattack)
            {
                sl_states = states.hurt;

            }

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
        float timer = 4f;

        while (timer > 0)
        {
            timer -= Time.deltaTime * 8f;
            mesh.gameObject.transform.localScale = new Vector3(timer, timer, timer);
            yield return null;
        }
        timer = 0;

    }
}
