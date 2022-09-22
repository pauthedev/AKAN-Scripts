using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moonsquid : MonoBehaviour
{//copycat with size difference from normal
    public enum states
    {
        sleep,
        follow,
        atk,
        tp,
        hurt,
        die,
    }

    public states fly_states;

    public bossfight boss;
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

    public GameObject spawnpoint;
    public GameObject projectile;
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
            case states.tp:
                tp();
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
        timer = Random.Range(5f, 5.5f);
    }

    void Update()
    {
        StartCoroutine(States());
        if (boss.health <= 0)
        {
            fly_states = states.die;
        }
        transform.position = new Vector3(transform.position.x, 0.67f, transform.position.z);
        distance = Vector3.Distance(target.position, transform.position);
    }

    void tp()
    {
        
    }

    void sleep()
    {
        anim.SetBool("move", false);
        target = transform;
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
        target = pl.transform;
        Vector3 dir = target.position - transform.position;
        transform.forward = new Vector3(dir.x, 0, dir.z);
        if (!isonattack)
        {
            timer -= Time.deltaTime;
        }

        anim.SetBool("move", false);
        if (timer <= 0 && !isonattack)
        {
            anim.SetTrigger("atk");
            isonattack = true;



            Invoke("returnpos", 0.5f);
            timer = Random.Range(3f, 3.2f);

        }

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
    {
        GameObject obj = Instantiate(projectile);
        obj.gameObject.transform.forward = spawnpoint.transform.forward;
        obj.gameObject.transform.position = spawnpoint.transform.position;

        obj.GetComponent<flyprojectile>().element = 2;
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
            anim.SetTrigger("die");
            StartCoroutine(Scale());
            dead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sword")
        {

            if (boss.health > 0 && pl.attackmode &&!isonattack)
            {
                fly_states = states.hurt;

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
                //pl.Hurt(damage);
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
