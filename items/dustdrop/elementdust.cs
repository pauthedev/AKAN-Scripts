using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.SceneManagement;


public class elementdust : MonoBehaviour
{
    public int element;
    [ColorUsage(true, true)]
    public Color fire;
    [ColorUsage(true, true)]
    public Color water;
    [ColorUsage(true, true)]
    public Color earth;

    public VisualEffect VFXGraph;
    public Transform target;
    Vector3 Movement;

    float speed = 0;
    float turnspeed = 1;
    float acel = 10;
    float maxspeed = 18;

    playercont pl;
    Scene m_Scene;
    void Start()
    {
        pl = FindObjectOfType<playercont>();
        Movement = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Movement = Movement.normalized * Random.Range(2f, 4f);
        m_Scene = SceneManager.GetActiveScene();
    }

    void Update()
    {//sets the color of the vfx depending on wich element is the enemy
        if (element==1)
        {
            VFXGraph.SetVector4("color",fire);
        }

        if (element == 2)
        {
            VFXGraph.SetVector4("color", water);
        }

        if (element == 3)
        {
            VFXGraph.SetVector4("color", earth);
        }


        if (target)
        {
            speed += acel * Time.deltaTime;

            speed = Mathf.Min(speed, maxspeed);

            turnspeed += 3 * Time.deltaTime;

            Vector3 dir = target.position - transform.position;

            Movement = Vector3.Lerp(Movement, dir * speed, turnspeed * Time.deltaTime);

            if (dir.magnitude < .3f)
            {
                target = null;
                Movement = dir * 2f;
                StartCoroutine(Scale());

                if (element == 1)
                {
                    if (m_Scene.name == "misore")
                    {
                        pl.firedust += 10;
                    }
                    else
                    {
                        eventlibrary.d_firedust += 5;
                    }
                }

                if (element == 2)
                {
                    if (m_Scene.name == "misore")
                    {
                        pl.waterdust += 10;
                    }
                    else
                    {
                        eventlibrary.d_waterdust += 5;
                    }
                }

                if (element == 3)
                {
                    if (m_Scene.name == "misore")
                    {
                        pl.earthdust += 10;
                    }
                    else
                    {
                        eventlibrary.d_earthdust += 5;
                    }
                }

                Destroy(gameObject, 0.5f);
            }

        }

        transform.position += Movement * Time.deltaTime;
    }
    IEnumerator Scale()
    {
        float timer = 1f;

        while (timer > 0)
        {
            timer -= Time.deltaTime*2;
            transform.localScale = new Vector3(timer, timer, timer);
            yield return null;
        }
        timer = 0;

    }
}
