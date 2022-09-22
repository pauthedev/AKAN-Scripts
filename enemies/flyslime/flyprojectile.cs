using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class flyprojectile : MonoBehaviour
{
    public int element;
    [ColorUsage(true, true)]
    public Color fire;
    [ColorUsage(true, true)]
    public Color water;
    [ColorUsage(true, true)]
    public Color earth;

    public VisualEffect VFXGraph;

    float speed = 0;
    public float damage;
    public float acel = 10;
    public float maxspeed = 24;

    playercont pl;

    void Start()
    {
        pl = FindObjectOfType<playercont>();
//element color
        if (element == 1)
        {
            VFXGraph.SetVector4("color", fire);
        }

        if (element == 2)
        {
            VFXGraph.SetVector4("color", water);
        }

        if (element == 3)
        {
            VFXGraph.SetVector4("color", earth);
        }

        Destroy(gameObject, 4f);
    }

    void FixedUpdate()
    {
        speed += acel * Time.deltaTime;

        speed = Mathf.Min(speed, maxspeed);

        transform.Translate(Vector3.forward * Time.deltaTime * speed);//move to kinich past direction 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pl.Hurt(damage);
            Destroy(gameObject, 0.1f);
        }
        
    }
}
