using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class basicenemyui : MonoBehaviour
{
    public Slider Slider;
    public Vector3 offset;
    public enemybasic enemy;

    public Transform cam;

    void Start()
    {
        
    }

    private void LateUpdate()
    {//smooth ui follow
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        transform.LookAt(transform.position + cam.forward);
    }

    void Update()
    {

        Slider.gameObject.SetActive(enemy.health < enemy.maxhealth);
        Slider.maxValue = enemy.maxhealth;
        Slider.value = enemy.health;

        if (enemy.health == 0)
        {
            Slider.gameObject.SetActive(false);
        }
    }


}
