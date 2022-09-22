using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class slimeshard : MonoBehaviour
{
    public Transform target;
    Vector3 Movement;

    float speed = 0;
    float turnspeed = 1;
    float acel = 10;
    float maxspeed=18;

    public TrailRenderer trail;

    playercont pl;
    Scene m_Scene;
    void Start()
    {
        pl = FindObjectOfType<playercont>();
        Movement = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)); //random position
        Movement = Movement.normalized * Random.Range(2f, 4f);//round and multiply
        m_Scene = SceneManager.GetActiveScene();


    }

    void Update()
    {
        if (target)
        {
            speed += acel * Time.deltaTime; //increase velocity with time

            speed = Mathf.Min(speed, maxspeed);

            turnspeed += 3*Time.deltaTime;

            Vector3 dir = target.position - transform.position;

            Movement = Vector3.Lerp(Movement, dir*speed, turnspeed*Time.deltaTime); // create a smooth movement to reach the player

            if (dir.magnitude < .4f)
            {//smooth destroy 
                target = null;
                Movement = dir * 2f;
                StartCoroutine(Scale());//decrease scale in time
                if (m_Scene.name == "misore")
                {
                    pl.pearls += 10;
                }
                else
                {
                    eventlibrary.d_pearls += 10;
                }
                
                Destroy(gameObject,0.4f);
            }

        }
        else
        {//spawn effect
            trail.startWidth = Mathf.Lerp(0, 0.2f, Time.deltaTime);
            trail.endWidth = Mathf.Lerp(0, 0.1f, Time.deltaTime);
            transform.localScale = Vector3.Lerp(new Vector3(0,0,0), new Vector3(1,1,1), Time.deltaTime);
        }

        transform.position += Movement*Time.deltaTime;
    }
    IEnumerator Scale()
    {
        float timer = 0.4f;

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                transform.localScale = new Vector3(timer, timer, timer)*0.2f;
                trail.startWidth = 0.2f * timer;
                trail.endWidth = 0.1f * timer;
                yield return null;
            }
            timer = 0;

    }
}
